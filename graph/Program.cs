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
            
            Graph.Graph g2 = new Graph.Graph("/home/jconda/RiderProjects/graph/Test data/input2");
            
            g.CreateGraphVizFile("/home/jconda/RiderProjects/graph/Test data/output.txt");
            
            List<Graph.Graph> graphs = new List<Graph.Graph>();
            graphs.Add(g);
            graphs.Add(g2);
            
            UI ui = new UI(graphs);
            ui.MainMenu();

        }
    }
}
