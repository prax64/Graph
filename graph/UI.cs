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
        private List<Graph.Graph> graphs;
        
        private protected List<Graph.Graph> Graphs
        {
            get => graphs;
        }
        private protected UI(){}
        public UI(List<Graph.Graph> graphs)
        {
            this.graphs = graphs;
        }
        public void MainMenu()
        {
            Console.Clear();
            Console.WriteLine($"Загружено {graphs.Count} для работы.");
            
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
                    Console.Clear();
                    PrintGraphSelection();
                    try
                    {
                        Console.WriteLine(graphs[GraphSelection() -1].Print());
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        goto default;
                    }
                    GoToMainMenu();
                    break;
                case "2":
                    Console.Clear();
                    PrintGraphSelection();
                    int numberDelTmp = GraphSelection();
                    Console.WriteLine("Введите имя вершины которую необходимо удалить:");
                    string s = Console.ReadLine();
                    try
                    {
                        graphs[numberDelTmp -1].DelNode(s);
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        goto default;
                    }
                    GoToMainMenu();
                    break;
                case "3":
                    Console.Clear();
                    PrintGraphSelection();
                    int numberDel = GraphSelection();
                    Console.WriteLine("Введите имя вершины из которой удаляем ребро:");
                    string u = Console.ReadLine();
                    Console.WriteLine("Введите имя вершины которую необходимо удалить:");
                    string v = Console.ReadLine();
                    try
                    {
                        graphs[numberDel -1].DelEdge(u,v);
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        goto default;
                    }
                    GoToMainMenu();
                    break;
                case "4":
                    Console.Clear();
                    PrintGraphSelection();
                    Tasks t = new Tasks();
                    try
                    {
                        t.MenuTasks(graphs[GraphSelection() -1]);
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        Console.WriteLine("ArgumentOutOfRangeException");
                        goto default;
                    }
                    MainMenu();
                    break;
                case "5":
                    break;
                default:
                    Console.WriteLine("Не верный формат ввода. Введите цифру заново.");
                    GoToMainMenu();
                    break;
            }
        }

        private void GoToMainMenu()
        {
            Console.WriteLine("\nВведите 1, для перехода в главное меню:");
            string tmp = Console.ReadLine();
            if (tmp == "1")
                MainMenu();
            else
                GoToMainMenu();
        }

        private void PrintGraphSelection()
        {
            for (int i = 0; i < graphs.Count; i++)
            {
                Console.WriteLine($"Введите {i+1}, для работы с графом {i+1}");
            }
        }

        private int GraphSelection()
        {
            try
            {
                int numberGraph = int.Parse(Console.ReadLine());
                return numberGraph;
            }
            catch (Exception e)
            {
                Console.WriteLine("Неверный формат ввода!!!");
                GoToMainMenu();
            }

            return -1;
        }
        public void PrintEdge()
        {
            foreach (var g in graphs)
            {
                foreach (var a in g.GetEdge() )
                {
                    Console.WriteLine(a);
                }
            }
        }
    }
}