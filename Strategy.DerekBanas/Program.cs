using static System.Console;

namespace Strategy.DerekBranas
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine(new Catchoro().TryToFly());
            WriteLine(new Passarineo().TryToFly());

            //Interchange at runtime
            var catchoro = new Catchoro();
            catchoro.SetFlingAbility(new ItFlys());

            WriteLine("Now i can fly...");
            WriteLine(catchoro.TryToFly());
        }
    }

    interface IFlys
    {
        string Fly();
    }

    class ItFlys : IFlys
    {
        public string Fly()
        {
            return "I can fly!";
        }
    }

    class CantFly : IFlys
    {
        public string Fly()
        {
            return "Can't fly!";
        }
    }


    class Animal
    {
        private IFlys iFlyType;

        public string TryToFly()
        {
            return iFlyType.Fly();
        }

        public void SetFlingAbility(IFlys newIFlyType)
        {
            this.iFlyType = newIFlyType;
        }
    }

    class Catchoro : Animal
    {
        public Catchoro()
        {
            SetFlingAbility(new CantFly());
        }
    }

    class Passarineo : Animal
    {
        public Passarineo()
        {
            SetFlingAbility(new ItFlys());
        }
    }
}
