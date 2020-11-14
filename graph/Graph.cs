using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.CompilerServices;

namespace Graph
{
    public class Graph
    {
        LinkedList<Node> nodes = new LinkedList<Node>();
        private bool? directed = null ;

        public Graph() { }

        public Graph(string where)
        {
            string[] lines = File.ReadAllLines(where);
            try
            {
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i] == "Graph")
                    {
                        var s = lines[++i].Split(',');
                        var arrNode = s.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
                        foreach (var node in arrNode)
                        {
                            addNode(node);
                        }
                        if (lines[++i] == "directed")
                        {
                            directed = true;
                        }
                        else if (lines[i] == "nondirected")
                        {
                            directed = false;
                        }

                        while (true)
                        {
                            try
                            {
                                var tmp = lines[++i].Split(',');
                                addDirectedVertex(tmp[0], tmp[1], int.Parse(tmp[2]));
                            }
                            catch (IndexOutOfRangeException)
                            {
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("Error");
                throw;
            }
        }
        // Copy constructor.
        public Graph(Graph g)
        {
            nodes = g.nodes;
            directed = g.directed;
        }


        public void addNode(string label, int data)
        {
            nodes.AddLast(new Node(data,label));
        }

        public void addNode(string label)
        {
            addNode(label,0);
        }
        public void delNode(string u)
        {
            Node x = null;
            foreach (Node a in nodes)
            {
                if (a.label.Equals(u))
                {
                    x = a;
                }
            }
            if (x == null)
            {
                return;
            }
            nodes.Remove(x);
        }

        public void addNonDirectedVertex(string u, string v)
        {
            addNonDirectedVertex(u, v, 0);
        }

        public void addNonDirectedVertex(string u, string v,int weigth)
        {
            Node x = null, y = null;
            foreach (Node a in nodes)
            {
                if (a.label.Equals(u))
                {
                    x = a;
                }
                if (a.label.Equals(v))
                {
                    y = a;
                }
            }
            if (x == null || y == null)
            {
                return;
            }
            x.neighbors.AddLast(new Vertex(y, weigth));
            y.neighbors.AddLast(new Vertex(x, weigth));
        }
        public void delNonDirectedVertex(string u, string v,int weigth)
        {
            Node x = null, y = null;
            foreach (Node a in nodes)
            {
                if (a.label.Equals(u))
                {
                    x = a;
                }
                if (a.label.Equals(v))
                {
                    y = a;
                }
            }
            if (x == null || y == null)
            {
                return;
            }
            Vertex vertex1 = null, vertex2 = null;
            foreach (var e in x.neighbors)
            {
                if (e.node.label == u)
                {
                    vertex1 = e;
                }
                if (e.node.label == v)
                {
                    vertex2 = e;
                }
            }
            if (vertex1 == null || vertex2 == null)
            {
                return;
            }
            x.neighbors.Remove(vertex2);
            y.neighbors.Remove(vertex1);
        }

        public String neighbors(string u)
        {
            StringBuilder result = new StringBuilder();
            foreach (Node a in nodes) {
                if (a.label.Equals(u)) {
                    foreach (Vertex b in a.neighbors)
                    {
                        result.Append(" " + b.node.label);
                    }
                    return result.ToString();
                }            
            }            
            return result.ToString();
        }

        public void addDirectedVertex(string u, string v)
        {
            addDirectedVertex(u, v, 0);
        }

        public void addDirectedVertex(string u, string v,int weigth)
        {
            Node x = null, y = null;
            foreach (Node a in nodes)
            {
                if (a.label.Equals(u))
                {
                    x = a;
                }
                if (a.label.Equals(v))
                {
                    y = a;
                }
            }
            if (x == null || y == null)
            {
                return;
            }
            x.neighbors.AddLast(new Vertex(y, weigth));
        }
        public void delDirectedVertex(string u, string v, int weigth)
        {
            Node x = null, y = null;
            foreach (Node a in nodes)
            {
                if (a.label.Equals(u))
                {
                    x = a;
                }
                if (a.label.Equals(v))
                {
                    y = a;
                }
            }
            if (x == null || y == null)
            {
                return;
            }

            Vertex vertex = null;
            foreach (var e in x.neighbors)
            {
                if (e.node.label == v)
                {
                    vertex = e;
                }
            }
            if (vertex == null)
            {
                return;
            }
            x.neighbors.Remove(vertex);
        }
        
        private void clearAllMarks()
        {
            foreach (Node a in nodes)
            {
    		     a.mark = 0;
    	    }    	 
        }
        
        private Node getNodeByName(String u){
    	    foreach(Node a in nodes){
        		if (a.label.Equals(u)){
    			    return a;
    		    } 
    	    }
    	    return null;
        }    
        
        public void CreateGraphVizFile(string where)
        {
            StreamWriter writer = new StreamWriter(where, false);
            writer.WriteLine("Graph");
            foreach (Node a in nodes)
            {
                writer.Write($"{a.label},");
            }
            writer.WriteLine();
            if (directed != null)
            {
                if (directed == true)
                    writer.WriteLine("directed");
                else
                    writer.WriteLine("nondirected");
            }
            else
            {
                throw new Exception("Не определена напраленность графа!");
            }
            foreach (Node a in nodes)
            {
                foreach (Vertex b in a.neighbors)
                {
                    writer.WriteLine(a.label + "," + b.node.label+","+b.weight);
                }
            }
            writer.Close();
        }
        
        public string print()
        {
            StringBuilder res = new StringBuilder();
            if (directed != null)
            {
                if (directed == true)
                    res.Append("Graph = directed\n");
                else
                    res.Append("Graph = nondirected\n");
            }
            else
            {
                throw new Exception("Не определена напраленность графа!");
            }
            foreach (Node a in nodes)
            {
                res.Append($"{a.label}: [");
                foreach (Vertex b in a.neighbors)
                {
                    res.Append($"{{{b.node.label}, {b.weight}}}, ");
                }
                res.Append("]\n");
            }
            return res.ToString();
        }
        
        public IEnumerable getEdge()
        {
            foreach (Node a in nodes)
            {
                foreach (Vertex b in a.neighbors)
                {
                    yield return $"{a.label},{b.node.label},{b.weight}";
                }
            }
        }

        public override string ToString()
        {
            StringBuilder res = new StringBuilder();
            foreach (Node a in nodes)
            {
                res.Append($"{a.label}: [");
                foreach (Vertex b in a.neighbors)
                {
                    res.Append($"{{{b.node.label}, {b.weight}}}, ");
                }
                res.Append("]\n");
            }
            return res.ToString();
        }
    }
}
