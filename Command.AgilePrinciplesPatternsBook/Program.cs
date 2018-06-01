using System;
using System.Collections;

namespace Command.AgilePrinciplesPatternsBook
{
    //Command and Active Object
    
    public class ActiveObjectEngine
    {
        ArrayList itsCommands = new ArrayList();

        public void AddCommand(ICommand c)
        {
            itsCommands.Add(c);
        }

        public void Run()
        {
            while (itsCommands.Count > 0)
            {
                ICommand c = (ICommand)itsCommands[0];
                itsCommands.RemoveAt(0);
                c.Execute();
            }
        }
    }


    public interface ICommand
    {
        void Execute();
    }

    public class SleepCommand : ICommand
    {
        private ICommand wakeupCommand = null;
        private ActiveObjectEngine engine = null;
        private long sleepTime = 0;
        private DateTime startTime;
        private bool started = false;

        public SleepCommand(long milliseconds, ActiveObjectEngine e, ICommand wakeupCommand)
        {
            sleepTime = milliseconds;
            engine = e;
            this.wakeupCommand = wakeupCommand;
        }

        public void Execute()
        {
            DateTime currentTime = DateTime.Now;
            if (started == false)
            {
                started = true;
                startTime = currentTime;
                engine.AddCommand(this);
            }
            else
            {
                TimeSpan elapsedTime = currentTime - startTime;
                if (elapsedTime.TotalMilliseconds < sleepTime)
                {
                    engine.AddCommand(this);
                }
                else
                {
                    engine.AddCommand(wakeupCommand);
                }
            }
        }
    }

    public class DelayedTyper : ICommand
    {
        private long itsDelay;
        private char itsChar;
        private static bool stop = false;
        private static ActiveObjectEngine engine = new ActiveObjectEngine();

        private class StopCommand : ICommand
        {
            public void Execute()
            {
                DelayedTyper.stop = true;
            }
        }

        public DelayedTyper(long delay, char c)
        {
            itsDelay = delay;
            itsChar = c;
        }

        static void Main(string[] args)
        {
            engine.AddCommand(new DelayedTyper(100, '1'));
            engine.AddCommand(new DelayedTyper(300, '3'));
            engine.AddCommand(new DelayedTyper(500, '5'));
            engine.AddCommand(new DelayedTyper(700, '7'));
            engine.AddCommand(new SleepCommand(5000, engine, new StopCommand()));
            engine.Run();
        }

        public void Execute()
        {
            Console.Write(itsChar);
            if (stop == false)
            {
                DelayAndRepeat();
            }
        }

        private void DelayAndRepeat()
        {
            engine.AddCommand(new SleepCommand(itsDelay, engine, this));
        }
    }

}
