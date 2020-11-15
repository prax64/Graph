using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using Graph;


namespace graph
{
    public class UI
    {
        public static void MainMenu(Graph.Graph g)
        {
            Console.Clear();
            Console.WriteLine("Введите 1, для вывода списока смежности");
            
            Console.WriteLine("Введите 2, для удаления вершины");

            Console.WriteLine("Введите 3, для удаления ребра");
            
            Console.WriteLine("Введите 4, для перехода к заданиям");
            
            Console.WriteLine("Введите 5, для выхода из меню");
            
            string digit = Console.ReadLine();
            Console.Clear();
            switch (digit)
            {
                case "1":
                    Console.WriteLine(g.print());
                    GoToMainMenu(g);
                    break;
                case "2":
                    Console.WriteLine("Введите имя вершины которую необходимо удалить:");
                    string s = Console.ReadLine();
                    g.DelNode(s);
                    GoToMainMenu(g);
                    break;
                case "3":
                    Console.WriteLine("Введите имя вершины из которой удаляем ребро:");
                    string u = Console.ReadLine();
                    Console.WriteLine("Введите имя вершины которую необходимо удалить:");
                    string v = Console.ReadLine();
                    Console.WriteLine("Введите вес данного ребра:");
                    int weigth = int.Parse(Console.ReadLine());
                    g.DelEdge(u,v,weigth);
                    GoToMainMenu(g);
                    break;
                case "4":
                    Tasks.MenuTasks(g);
                    break;
                case "5":
                    break;
                default:
                    Console.WriteLine("Не верный формат ввода. Введите цифру заново.");
                    GoToMainMenu(g);
                    break;
            }
        }

        private static void GoToMainMenu(Graph.Graph g)
        {
            Console.WriteLine("\nВведите 1, для перехода в главное меню:");
            string tmp = Console.ReadLine();
            if (tmp == "1")
                MainMenu(g);
            else
                GoToMainMenu(g);
        }
        public static void PrintEdge(Graph.Graph g)
        {
            foreach (var a in g.getEdge() )
            {
                Console.WriteLine(a);
            }
        }
    }
}