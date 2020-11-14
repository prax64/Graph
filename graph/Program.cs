using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using graph;
using Graph;

namespace Graph_main
{
    class Program
    {
        static void Main(string[] args)
        {

            Graph.Graph g = new Graph.Graph("/home/jconda/RiderProjects/graph/Test data/input1");

            Graph.Graph g1 = new Graph.Graph(g);
            
            Console.WriteLine("\n");
            g.CreateGraphVizFile("/home/jconda/RiderProjects/graph/Test data/output.txt");
            
            UI.mainMenu(g1); 

        }
    }
}
