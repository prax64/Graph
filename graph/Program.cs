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
            
            Graph.Graph g3 = new Graph.Graph("/home/jconda/RiderProjects/graph/Test data/input3");
            
            Graph.Graph g4 = new Graph.Graph("/home/jconda/RiderProjects/graph/Test data/input4");
            
            Graph.Graph g5 = new Graph.Graph("/home/jconda/RiderProjects/graph/Test data/input5");
            
            Graph.Graph g6 = new Graph.Graph("/home/jconda/RiderProjects/graph/Test data/input6");
            
            g.CreateGraphVizFile("/home/jconda/RiderProjects/graph/Test data/output.txt");
            
            List<Graph.Graph> graphs = new List<Graph.Graph>();
            graphs.Add(g);
            graphs.Add(g2);
            graphs.Add(g3);
            graphs.Add(g4);
            graphs.Add(g5);
            graphs.Add(g6);
            
            UI ui = new UI(graphs);
            ui.MainMenu();

        }
    }
}
