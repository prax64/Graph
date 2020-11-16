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
        private LinkedList<Node> nodes = new LinkedList<Node>();
        private bool? directed = null ;
        public bool? Directed => directed;
        private delegate void delVertex(string u, string v);

        #region  Constructors

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
                            AddNode(node);
                        }
                        if (lines[++i] == "directed")
                        {
                            directed = true;
                            while (true)
                            {
                                try
                                {
                                    var tmp = lines[++i].Split(',');
                                    AddDirectedVertex(tmp[0], tmp[1], int.Parse(tmp[2]));
                                }
                                catch (IndexOutOfRangeException)
                                {
                                    break;
                                }
                            }
                        }
                        else if (lines[i] == "nondirected")
                        {
                            directed = false;
                            while (true)
                            {
                                try
                                {
                                    var tmp = lines[++i].Split(',');
                                    AddNonDirectedVertex(tmp[0], tmp[1], int.Parse(tmp[2]));
                                }
                                catch (IndexOutOfRangeException)
                                {
                                    break;
                                }
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

        #endregion

        #region Methods with nodes

        public void AddNode(string label, int data)
        {
            nodes.AddLast(new Node(data,label));
        }

        public void AddNode(string label)
        {
            AddNode(label,0);
        }
        public void DelNode(string u)
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
        
        #endregion

        #region Methods with vertex(edges)

        public void AddNonDirectedVertex(string u, string v)
        {
            AddNonDirectedVertex(u, v, 0);
        }

        private bool CheckingAdding(Node inNode, Node outNOde)
        {
            foreach (var ver in inNode.neighbors)
                if (ver.node.label == outNOde.label)
                    throw new Exception("Неверный формат данных! Ребро уже существует!");
            return true;
        }
        public void AddNonDirectedVertex(string u, string v,int weigth)
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
            
            if (x == null || y == null || !CheckingAdding(x, y))
            {
                return;
            }
            
            if (u == v)
            {
                x.neighbors.AddLast(new Vertex(y, weigth));
                return;
            }
            x.neighbors.AddLast(new Vertex(y, weigth));
            y.neighbors.AddLast(new Vertex(x, weigth));
        }
        private void DelNonDirectedVertex(string u, string v)
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
                if (e.node.label == v)
                {
                    vertex1 = e;
                    break;
                }
            }

            foreach (var e in y.neighbors)
            {
                if (e.node.label == u)
                {
                    vertex2 = e;
                    break;
                }
            }
            if (vertex1 == null || vertex2 == null)
            {
                return;
            }
            x.neighbors.Remove(vertex1);
            y.neighbors.Remove(vertex2);
        }
        private void DelNonDirectedVertex(Node u, Node v)
        {
            Vertex vertex1 = null, vertex2 = null;
            foreach (var e in u.neighbors)
            {
                if (e.node == u)
                {
                    vertex1 = e;
                }
                if (e.node == v)
                {
                    vertex2 = e;
                }
            }
            if (vertex1 == null || vertex2 == null)
            {
                return;
            }
            u.neighbors.Remove(vertex2);
            v.neighbors.Remove(vertex1);
        }

        public void AddDirectedVertex(string u, string v)
        {
            AddDirectedVertex(u, v, 0);
        }

        public void AddDirectedVertex(string u, string v,int weigth)
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
            if (x == null || y == null || !CheckingAdding(x, y))
            {
                return;
            }
            x.neighbors.AddLast(new Vertex(y, weigth));
        }
        private void DelDirectedVertex(string u, string v)
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
        private void DelDirectedVertex(Node u, Vertex vertex)
        {
            u.neighbors.Remove(vertex);
        }
        
        public void DelEdge(string u, string v)
        {
            if (directed == true)
            {
                delVertex delEdge = new delVertex(DelDirectedVertex);
                delEdge(u, v);
            }
            else if (directed == false)
            {
                delVertex delEdge = new delVertex(DelNonDirectedVertex);
                delEdge(u, v);
            }
            else
            {
                throw new Exception("Не определена напраленность графа!");
            }
        }
        
        #endregion
        
        private void ClearAllMarks()
        {
            foreach (Node a in nodes)
            {
    		     a.mark = 0;
    	    }    	 
        }
        
        private Node GetNodeByName(String u){
    	    foreach(Node a in nodes){
        		if (a.label.Equals(u)){
    			    return a;
    		    } 
    	    }
    	    return null;
        }
        
        // returns a List neighbors of a node
        public LinkedList<string> GetLabelNeighborsNode(string u)
        {
            LinkedList<string> labelNeighbors = new LinkedList<string>();
            var uNode = GetNodeByName(u);
            foreach (Vertex a in uNode.neighbors)
                labelNeighbors.AddLast(a.node.label);
            return labelNeighbors;
        }
        
        public LinkedList<string> GetLabelNodes() 
        {
            LinkedList<string> labelNodes = new LinkedList<string>();
            foreach (Node a in nodes)
                labelNodes.AddLast(a.label);
            return labelNodes;
        }

        #region Methods for tasks

        public void Task3()
        {
            ClearAllMarks();
            foreach (var node in nodes)
            {
                int deg = node.neighbors.Count;
                foreach (var vertex in node.neighbors)
                {
                    if (deg == vertex.node.neighbors.Count && vertex.node.mark != 1)
                    {
                        DelEdge(node.label,vertex.node.label);
                        Console.WriteLine(Print());
                        node.mark = 1;
                        break;
                    }
                }
            }
        }

        #endregion

        #region Walk

        public String DFSWalk(String u)
        {
            ClearAllMarks();
            Node start = GetNodeByName(u);
            if (start == null)
            {
                return "";
            }
            return DFSWalk(start, "");
        }
        
        private String DFSWalk(Node u, String aux)
        {
            aux = aux + " " + u.label;
            u.mark = 1;
            foreach(Vertex a in u.neighbors)
            {
                if(a.node.mark == 0)
                {
                    aux = DFSWalk(a.node, aux);
                }
            }    	 
            return aux;
        }

        #endregion
        
        #region View
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
        
        public string Print()
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
        
        public IEnumerable GetEdge()
        {
            foreach (Node a in nodes)
            {
                foreach (Vertex b in a.neighbors)
                {
                    yield return $"{a.label},{b.node.label},{b.weight}";
                }
            }
        }
        
        #endregion
    }
}
