using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory.Dofactory
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            IFabricaOperacoes fabricaA = new IFabricaConcretaA();
            IFabricaOperacoes fabricaB = new IFabricaConcretaB();


            IProdutoA produtoA = fabricaA.CriarProdutoA();
            produtoA.AcaoA();

            IProdutoB produtoB = fabricaB.CriarProdutoB();
            produtoB.AcaoB();
        }
    }

    public interface IProdutoA
    {
        void AcaoA();
    }

    public class ProdutoConcretoA : IProdutoA
    {
        public void AcaoA()
        {
            Console.WriteLine("Acao A");
        }
    }

    public interface IProdutoB
    {
        void AcaoB();
    }

    public class ProdutoConcretoB : IProdutoB
    {
        public void AcaoB()
        {
            Console.WriteLine("Acao B");
        }
    }

    public interface IFabricaOperacoes
    {
        IProdutoA CriarProdutoA();
        IProdutoB CriarProdutoB();
    }

    public class IFabricaConcretaA : IFabricaOperacoes
    {
        public IProdutoA CriarProdutoA()
        {
            return new ProdutoConcretoA();
        }

        public IProdutoB CriarProdutoB()
        {
            throw new NotImplementedException();
        }
    }

    public class IFabricaConcretaB : IFabricaOperacoes
    {
        public IProdutoA CriarProdutoA()
        {
            throw new NotImplementedException();
        }

        public IProdutoB CriarProdutoB()
        {
            return new ProdutoConcretoB();
        }
    }

}
