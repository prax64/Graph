using System;
using System.Collections.Generic;
using System.Linq;

namespace graph
{
    public class Tasks : UI
    {
        public void MenuTasks(Graph.Graph g)
        {
            Console.Clear();
            Console.WriteLine("Задание 1. ");

            Console.WriteLine("Задание 2. ");

            Console.WriteLine("Задание 3. ");

            Console.WriteLine("Задание 4. ");
            
            Console.WriteLine("Введите 0, для перехода в основное меню.");

            string digit = Console.ReadLine();
            Console.Clear();
            string u = "";
            switch (digit)
            { 
                case "1":
                    Console.WriteLine("la\n" +
                                      "20) Вывести все вершины орграфа, не смежные с данной.");
                    Console.Write("Введите вершину:  ");
                    u = Console.ReadLine();
                    var resStr = g.GetLabelNodes().Except(g.GetLabelNeighborsNode(u));
                    foreach (var a in resStr)
                        Console.Write($"{a}, ");
                    Console.WriteLine();
                    GoToMenuTasks(g);
                    break;
                case "2":
                    Console.WriteLine("la\n" +
                                      "1) Для данной вершины орграфа вывести все «выходящие» соседние вершины.");
                    Console.Write("Введите вершину:  ");
                    u = Console.ReadLine();
                    if (g.Directed == true)
                    {
                        foreach (var a in g.GetLabelNeighborsNode(u))
                            Console.Write($"{a}, ");
                    }
                    GoToMenuTasks(g);
                    break;
                case "3":
                    Console.WriteLine("lb\n" +
                                      "19) Построить граф, полученный однократным удалением рёбер, соединяющих" +
                                      " вершины одинаковой степени.");
                    g.Task3();
                    GoToMenuTasks(g);
                    break;
                case "4":
                    Console.WriteLine("II\n" +
                                      "18) Проверить граф на ацикличность.");
                    Console.WriteLine($"Ацикличность графа = {g.Acyclic()}");
                    GoToMenuTasks(g);
                    break;
                case "0":
                    break;
                default:
                    Console.WriteLine("Не верный формат ввода. Введите цифру заново.");
                    GoToMenuTasks(g);
                    break;
            }
        }
        private void GoToMenuTasks(Graph.Graph g)
        {
            Console.WriteLine("\nВведите 1, для перехода в меню c заданиями:");
            string tmp = Console.ReadLine();
            if (tmp == "1")
                MenuTasks(g);
            else
                GoToMenuTasks(g);
        }
    }
}