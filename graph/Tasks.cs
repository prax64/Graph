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
            
            Console.WriteLine("Задание 5. ");
            
            Console.WriteLine("Задание 6. ");
            
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
                case "5":
                    Console.WriteLine("II\n" +
                                      "32) Вывести кратчайший (по числу рёбер) цикл орграфа, содержащий вершину u.");
                    Console.Write("Введите вершину:  ");
                    u = Console.ReadLine();
                    Console.WriteLine(g.BFSWalk(u));
                    GoToMenuTasks(g);
                    break;
                case "6":
                    Console.WriteLine("III\n" +
                                      "П) Дан взвешенный неориентированный граф из N вершин и M ребер." +
                                      " Требуется найти в нем каркас минимального веса.");
                    GoToMenuTasks(g);
                    break;
                case "7":
                    Console.WriteLine("IV а (Дейкстра)\n" +
                                      "4) Вывести длины кратчайших путей от u до v1 и v2.");
                    GoToMenuTasks(g);
                    break;
                case "8":
                    Console.WriteLine("IV b (Форд-Беллман, Флойд)\n" +
                                      "10) Эксцентриситет вершины — максимальное расстояние из всех минимальных " +
                                      "расстояний от других вершин до данной вершины. Найти радиус графа — " +
                                      "минимальный из эксцентриситетов его вершин.");
                    GoToMenuTasks(g);
                    break;
                case "9":
                    Console.WriteLine("IV с (Форд-Беллман, Флойд)\n" +
                                      "12) Вывести кратчайший путь из вершины u до вершины v.");
                    GoToMenuTasks(g);
                    break;
                case "10":
                    Console.WriteLine("V Нахождение максимального потока\n" +
                                      "Решить задачу на нахождение максимального потока любым алгоритмом.");
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