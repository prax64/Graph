using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Graph;

namespace graph
{
    public class UI
    {
        public static void mainMenu(Graph.Graph g)
        {
           // Console.Clear();
            Console.WriteLine("Введите 1, для вывода списока смежности");
            
            
            Console.WriteLine("Введите 2, для удаления вершины");
            
            
            Console.WriteLine("Введите 3, для удаления ребра");

            string digit = Console.ReadLine();
            switch (digit)
            {
                case "1":
                    printGraph(g);
                    goToMainMenu(g);
                    break;
                case "2":
                    Console.WriteLine("Введите имя вершины которую необходимо удалить:");
                    string s = Console.ReadLine();
                    g.delNode(s);
                    goToMainMenu(g);
                    break;
                case "3":
                    Console.WriteLine("Введите имя вершины из которой удаляем ребро:");
                    string u = Console.ReadLine();
                    Console.WriteLine("Введите имя вершины которую необходимо удалить:");
                    string v = Console.ReadLine();
                    Console.WriteLine("Введите вес данного ребра:");
                    int weigth = int.Parse(Console.ReadLine());
                    g.delDirectedVertex(u,v,weigth);
                    goToMainMenu(g);
                    break;
                default:
                    Console.WriteLine("Не верный формат ввода. Введите цифру заново.");
                    mainMenu(g);
                    break;
            }
        }

        private static void goToMainMenu(Graph.Graph g)
        {
            Console.WriteLine("\nВведите 1, для перехода в главное меню:");
            string tmp = Console.ReadLine();
            if (tmp == "1")
                mainMenu(g);
            else
                goToMainMenu(g);
        }
        public static void printGraph(Graph.Graph g)
        {
            foreach (var a in g.print() )
            {
                Console.WriteLine(a);
            }
        } 
        
        
    }
}