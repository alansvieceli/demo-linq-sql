using System;
using System.Linq;
using Entities;
using System.Collections.Generic;

namespace demo_linq_sql
{
    class Program
    {

        private static List<Produto> Produtos;
        private static Categoria c1, c2, c3;


        static void Print<T>(string message, IEnumerable<T> collection)
        {
            Console.WriteLine("-------------------------------------------------------------------------");
            Console.WriteLine(message);
            Console.WriteLine("-------------------------------------------------------------------------");
            foreach (T obj in collection)
            {
                Console.WriteLine(obj);
            }
            Console.WriteLine("");
            Console.WriteLine("");
        }


        private static void IniciarDados()
        {

            c1 = new Categoria() { Id = 1, Nome = "Tools", Camada = 2 };
            c2 = new Categoria() { Id = 2, Nome = "Computers", Camada = 1 };
            c3 = new Categoria() { Id = 3, Nome = "Electronics", Camada = 1 };

            Program.Produtos = new List<Produto>() {
                new Produto() { Id = 1, Nome = "Computer", Preco = 1100.0, Categoria = c2 },
                new Produto() { Id = 2, Nome = "Hammer", Preco = 90.0, Categoria = c1 },
                new Produto() { Id = 3, Nome = "TV", Preco = 1700.0, Categoria = c3 },
                new Produto() { Id = 4, Nome = "Notebook", Preco = 1300.0, Categoria = c2 },
                new Produto() { Id = 5, Nome = "Saw", Preco = 80.0, Categoria = c1 },
                new Produto() { Id = 6, Nome = "Tablet", Preco = 700.0, Categoria = c2 },
                new Produto() { Id = 7, Nome = "Camera", Preco = 700.0, Categoria = c3 },
                new Produto() { Id = 8, Nome = "Printer", Preco = 350.0, Categoria = c3 },
                new Produto() { Id = 9, Nome = "MacBook", Preco = 1800.0, Categoria = c2 },
                new Produto() { Id = 10, Nome = "Sound Bar", Preco = 700.0, Categoria = c3 },
                new Produto() { Id = 11, Nome = "Level", Preco = 70.0, Categoria = c1 }
            };

        }

        static void Main(string[] args)
        {
            IniciarDados();

            var r1 = Produtos.Where(p => p.Categoria.Camada == 1 && p.Preco < 900.0);
            Print("CAMADA 1 AND PREÇO < 900:", r1);

            var r2 = Produtos.Where(p => p.Categoria.Nome == "Tools").Select(p => p.Nome);
            Print("NOMES DOS PRODUTOS DA CATEGORIA 'TOOLS'", r2);

            var r3 = Produtos.Where(p => p.Nome[0] == 'C').Select(p => new { p.Nome, p.Preco, NomeCategoria = p.Categoria.Nome });
            Print("NOMES INICIADOS COM 'C' E UM OBJETO ANÔNIMO (ANONYMOUS OBJECT)", r3);

            var r4 = Produtos.Where(p => p.Categoria.Camada == 1).OrderBy(p => p.Preco).ThenBy(p => p.Nome);
            Print("CAMADA 1 ORDENADO POR PRECO E NOME", r4);

            var r5 = r4.Skip(2).Take(4);
            Print("CAMADA 1 ORDENADO POR PRECO E NOME PULA(SKIP) 2 PEGA(TAKE) 4", r5);


            //usando o Default evita dar exeção
            var r6 = Produtos.FirstOrDefault();
            Console.WriteLine("First or default test1: " + r6);
            var r7 = Produtos.Where(p => p.Preco > 3000.0).FirstOrDefault();
            Console.WriteLine("First or default test2: " + r7);
            Console.WriteLine();

            var r8 = Produtos.Where(p => p.Id == 3).SingleOrDefault();
            Console.WriteLine("Single or default test1: " + r8);
            var r9 = Produtos.Where(p => p.Id == 30).SingleOrDefault();
            Console.WriteLine("Single or default test2: " + r9);
            Console.WriteLine();

            //Operações agregação
            var r10 = Produtos.Max(p => p.Preco);
            Console.WriteLine("Maior Preço: " + r10);
            var r11 = Produtos.Min(p => p.Preco);
            Console.WriteLine("Menor Preço: " + r11);
            var r12 = Produtos.Where(p => p.Categoria.Id == 1).Sum(p => p.Preco);
            Console.WriteLine("Soma dos Preços da Categoria 1: " + r12);
            var r13 = Produtos.Where(p => p.Categoria.Id == 1).Average(p => p.Preco);
            Console.WriteLine("Média dos preços Categoria 1: " + r13);
            var r14 = Produtos.Where(p => p.Categoria.Id == 5).Select(p => p.Preco).DefaultIfEmpty(0.0).Average(); // não tem elementos
            Console.WriteLine("Média dos preços Categoria 5: " + r14);
            var r15 = Produtos.Where(p => p.Categoria.Id == 1).Select(p => p.Preco).Aggregate(0.0, (x, y) => x + y);
            Console.WriteLine("MapReduce (Agregação personalizada) da Categoria 1: " + r15); //mapReduce
            var r152 = Produtos.Where(p => p.Categoria.Id == 5).Select(p => p.Preco).Aggregate(0.0, (x, y) => x + y);
            Console.WriteLine("MapReduce (Agregação personalizada) da Categoria 5: " + r152); //mapReduce

            //Map = Select
            //Reduce = Aggregate

            Console.WriteLine();

            var r16 = Produtos.GroupBy(p => p.Categoria);
            foreach (IGrouping<Categoria, Produto> group in r16)
            {
                Console.WriteLine("Categoria " + group.Key.Nome + ":");
                foreach (Produto p in group)
                {
                    Console.WriteLine(p);
                }
                Console.WriteLine();
            }

        }
    }
}
