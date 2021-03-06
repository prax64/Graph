﻿using System;
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
        private List<List<int>> helper = new List<List<int>>();
        private List<List<int>> distance = new List<List<int>>();

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
                                    if (tmp.Length <= 3)
                                        AddDirectedVertex(tmp[0], tmp[1], int.Parse(tmp[2]));
                                    else
                                    {
                                        AddDirectedVertexFlow(tmp[0], tmp[1], int.Parse(tmp[2]),
                                            int.Parse(tmp[3]),int.Parse(tmp[4]));
                                    }
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
                                    if(tmp.Length<=3)
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
        
        public void AddNonDirectedVertexFlow(string u, string v,int weigth, int flow, int flowCapacity)
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
            x.neighbors.AddLast(new Vertex(y, weigth, flow, flowCapacity));
            y.neighbors.AddLast(new Vertex(x, weigth, flow, flowCapacity));
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
        public void AddDirectedVertexFlow(string u, string v,int weigth, int flow, int flowCapacity)
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
            x.neighbors.AddLast(new Vertex(y, weigth, flow, flowCapacity));
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

        //Horrible piece of shit
        private Dictionary<string,Dictionary<string,int>> ToMatrix()
        {
            Dictionary<string,Dictionary<string,int>>  matr = new Dictionary<string, Dictionary<string, int>>();
            ClearAllMarks();
            string nodeLabel_i = "";
            for (int i = 0; i < nodes.Count; i++)
            {
                Dictionary<string,int> tmp = new Dictionary<string, int>();

                foreach (var node in nodes)
                {
                    tmp.Add(node.label,int.MaxValue);
                }

                
                foreach (var node in nodes)
                {
                    if (node.mark != 1)
                    {
                        nodeLabel_i = node.label;
                        node.mark = 1;
                        break;
                    }
                }
                matr.Add(nodeLabel_i,tmp);
            }
            
            foreach (var node in nodes)
            {
                foreach (var vertex in node.neighbors)
                {
                    matr[node.label][vertex.node.label] = vertex.weight;
                }
            }
            
            return matr;
        }
        class EdgeForBellmanFord { 
            public int v, u, weight; 
            public EdgeForBellmanFord(int v, int u, int weight)
            {
                this.v = v;
                this.u = u;
                this.weight = weight;
            } 
        }; 
        private List<EdgeForBellmanFord> ToListForBellmanFord()
        {
            List<EdgeForBellmanFord>  array = new List<EdgeForBellmanFord>();
            ClearAllMarks();
            int i = 0, j = 0;
            foreach (var node in nodes)
            {
                j = 0;
                foreach (var vertex in node.neighbors)
                {
                    array.Add(new EdgeForBellmanFord(i,j,vertex.weight));
                    j++;
                }

                i++;
            }
            return array;
        }

        public int SearchRadius()
        {
            FloydWarshall();
            int radius = Int32.MaxValue;
            var d = distance;
            for(int i = 0; i < d.Count; i++)
            {
                int eccentricity = Int32.MinValue;
                for (int j = 0; j < d.Count; j++)
                {
                    eccentricity = Math.Max(eccentricity, d[i][j]);
                }
                radius = Math.Min(radius, eccentricity);
            }
            distance.Clear();
            return radius;
        }
        
        
        public string ShortestPath_FloydWarshall(string u, string v)
        {
            FloydWarshall();
            int num_u = 0, num_v = 0;
            int k = 0;
            bool flag1 = false, flag2 = false;
            foreach (var node in nodes)
            {
                if (node.label == u)
                {
                    num_u = k;
                    flag1 = true;
                }
                if (node.label == v)
                {
                    num_v = k;
                    flag2 = true;
                }
                k++;
            }
            if (!flag1 || !flag2)
                return "vertex do not exist";
            string path = "";
            GetPath(num_u, num_v, ref path);
            distance.Clear();
            helper.Clear();
            return path;
        }
        
        private void GetPath(int u, int v, ref string path)
        {
            List<int> list = new List<int>();
            int ver = helper[u][v];
            list.Add(v);
            while (ver != -1)
            {
                list.Add(ver);
                ver = helper[u][ver];
            }
            list.Add(u);
            {
                list.Reverse();
                foreach (var el in list)
                {
                    int i = 0;
                    foreach (var node in nodes)
                    {
                        if (i == el)
                            path += " -> " + node.label;
                        i++;
                    }
                }
            }
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

        #region Walks

        private void InitializePathFinder(Node start) 
        {
            foreach (Node a in nodes) 
            {
                if (a == start)
                    a.distanceToMe = 0;
                else
                    a.distanceToMe = int.MaxValue;
                a.Father = null;
                a.mark = 0;
            }
        }
        
        private Node Lowest(LinkedList<Node> l) 
        {
            int lowest = int.MaxValue;
            Node lower = null;
            foreach (Node a in l) 
            {
                if (a.distanceToMe < lowest && a.mark == 0)
                {
                    //lowest = a.distanceToMe;
                    lower = a;
                }
            }
            return lower;
        }
        
        private void FloydWarshall()
        {
            var d = ToMatrix();
            //Kludge -_-
            distance.Clear();
            helper.Clear();
            for (int i = 0; i < nodes.Count; i++)
            {
                List<int> tmp1 = new List<int>();
                List<int> tmp2 = new List<int>();

                for (int j = 0; j < nodes.Count; j++)
                {
                    tmp1.Add(d.ElementAt(i).Value.ElementAt(j).Value);
                    tmp2.Add(-1);
                }
                distance.Add(tmp1);
                helper.Add(tmp2);
            }
            //-_-
            for (int k = 0; k < nodes.Count; k++) 
            {
                for (int i = 0; i < nodes.Count; i++) 
                {
                    for (int j = 0; j < nodes.Count; j++)
                    {
                        if (d.ElementAt(i).Value.ElementAt(k).Value < int.MaxValue &&
                            d.ElementAt(k).Value.ElementAt(j).Value < int.MaxValue &&
                            d.ElementAt(i).Value.ElementAt(k).Value +
                            d.ElementAt(k).Value.ElementAt(j).Value < 
                            d.ElementAt(i).Value.ElementAt(j).Value
                            )
                        {
                            distance[i][j] = distance[i][k] + distance[k][j];
                            helper[i][j] = k;
                        }
                    }
                }
            }

            for (int i = 0; i < nodes.Count; i++)
            {
                distance[i][i] = 0;
            }
        }

        public string BellmanFord(string u, string v)
        {
            var e = ToListForBellmanFord();
            List<int> d = new List<int>();
            List<int> p = new List<int>();
            int c1 = 0;
            int c2 = 0;
            foreach (var node in nodes)
            {
                d.Add(Int32.MaxValue);
            }
            d.Add(Int32.MaxValue);
            foreach (var node in nodes)
            {
                if (node.label != u )
                    c1++;
                else
                    break;
            }
            foreach (var node in nodes)
            {
                if (node.label != v )
                    c2++;
                else
                    break;
            }
            for (int i = 0; i < nodes.Count+1; i++)
            {
                p.Add(-1);
            }

            d[c1] = 0;
            for (;;)
            {
                bool any = false;
                for (int j = 0; j < e.Count; ++j)
                    if (d[e[j].v] < Int32.MaxValue)
                    {
                        if (d[e[j].u] > d[e[j].v] + e[j].weight)
                        {
                            d[e[j].u] = d[e[j].v] + e[j].weight;
                            p[e[j].u] = e[j].v;
                            any = true;
                        }
                    }

                if (!any) 
                    break;
            }
            if (d[c2] == Int32.MaxValue)
                 return string.Format($"No path from {u} to {v}");

            string pathStr = "";
            List<int> path = new List<int>();
            for (int cur=c2; cur!=-1; cur=p[cur])
                path.Add(cur);

            foreach (var el in path)
            {
                int i = 0;
                foreach (var node in nodes)
                {
                    if (i == el)
                        pathStr = " -> " + node.label + pathStr;
                    i++;
                }
            }

            return pathStr;

        }

        #region DFSWalk

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
        
        public String BFSWalk(String u){
            ClearAllMarks();
            String aux;    	 
            LinkedList<Node> list = new LinkedList<Node>();
            Node start = GetNodeByName(u);
            if (start == null ){
                return "";
            }

            aux = start.label;
            start.mark = 1;
            list.AddLast(start);
            while(list.Count != 0){
                Node v = list.First();
                list.RemoveFirst();
                foreach(Vertex a in v.neighbors){
                    if (a.node.mark == 0){
                        aux = aux+" "+a.node.label;
                        a.node.mark = 1;
                        list.AddLast(a.node);
                    }    			 
                }
            }
            return aux;
        }
        

        #region Dijkstra

        private void relaxDijkstra(Node u, Node v, int weight) {
            if (v.distanceToMe > u.distanceToMe + weight) {
                v.distanceToMe = u.distanceToMe + weight;
                v.Father = u;
            }
        }

        public String Dijkstra(String u, String v) {
            InitializePathFinder(GetNodeByName(u));
            Node x = null;
            while (true)  
            {
                x = Lowest(nodes);
                if (x == null) {
                    break;
                }
                x.mark = 1;
                foreach (Vertex a in x.neighbors)
                {
                    relaxDijkstra(x, a.node, a.weight);
                }
            }
            Node Finish = GetNodeByName(v);
            String distance = "";
            int totalWeight = Finish.distanceToMe;
            while (Finish != null) {
                distance = " -> "+Finish.label + distance;
                Finish = Finish.Father;            
            }
            return distance+" Total Weight: "+totalWeight;
        }

        #endregion
        
        public String Prim(string u) 
        {
            ClearAllMarks();
            SortedSet<string> nodeLabel = new SortedSet<string>();
            Node node_u = GetNodeByName(u);
            node_u.mark = 1;
            string distance = "";
            int totalWeight = 0;
            List<Node> nodesArr = new List<Node>();
            List<Node> nodesArr_fa = new List<Node>();
            nodesArr.Add(node_u);
            int min;
            Node tmp = null;
            Node tmp_fa = null;
            while (nodes.Count != nodeLabel.Count)
            {
                min = Int32.MaxValue;
                foreach (var node in nodesArr)
                {
                    foreach (var vertex in node.neighbors)
                    {
                        if (vertex.weight < min && vertex.node.mark != 1)
                        {
                            min = vertex.weight;
                            tmp = vertex.node;
                            tmp_fa = node;
                        }
                    }
                }
                tmp.mark = 1;
                nodesArr_fa.Add(tmp_fa);
                nodesArr_fa.Add(tmp);
                nodesArr.Add(tmp);
                totalWeight += min;
                nodeLabel.Add(tmp_fa.label);
                nodeLabel.Add(tmp.label);
            }
            foreach (var node in nodesArr_fa)
            {
                distance += node.label + " -> ";
            }
            return distance+$" Total Distance: {totalWeight}" ;
        }
        
        public String Kruskal() 
        {
            ClearAllMarks();
            SortedSet<string> nodeLabel = new SortedSet<string>();
            string distance = "";
            int totalWeight = 0;
            List<Node> nodesArr = new List<Node>();
            int min;
            int color = 0;
            Node tmp = null;
            Node tmp_fa = null;
            // while (nodeLabel.Count != nodes.Count )
            // {
            //     min = Int32.MaxValue;
            //     foreach (var node in nodes)
            //     {
            //         foreach (var vertex in node.neighbors)
            //         {
            //             if (vertex.weight < min && vertex.node.mark != 1)
            //             {
            //                 min = vertex.weight;
            //                 tmp = vertex.node;
            //                 tmp_fa = node;
            //             }
            //         }
            //     }
            //     tmp.mark = 1;
            //     tmp_fa.mark = 1;
            //     nodesArr.Add(tmp_fa);
            //     nodesArr.Add(tmp);
            //     totalWeight += min;
            //     nodeLabel.Add(tmp.label);
            //     nodeLabel.Add(tmp_fa.label);
            // }

            for (int i = 0; i < nodes.Count - 1; i++)
            {
                min = Int32.MaxValue;
                foreach (var node in nodes)
                {
                    foreach (var vertex in node.neighbors)
                    {
                        if (vertex.weight < min && vertex.node.mark != 1 )
                        {
                            min = vertex.weight;
                            tmp = vertex.node;
                            tmp_fa = node;
                        }
                    }
                }
                tmp.mark = 1;
                //tmp_fa.mark = 1;
                nodesArr.Add(tmp_fa);
                nodesArr.Add(tmp);
                totalWeight += min;
                nodeLabel.Add(tmp.label);
                nodeLabel.Add(tmp_fa.label);
            }
            
            foreach (var node in nodesArr)
            {
                distance += node.label + " -> ";
            }
            return distance+$" Total Distance: {totalWeight}" ;
        }
        
        public String Boruvka() 
        {
            ClearAllMarks();
            SortedSet<string> nodeLabel = new SortedSet<string>();
            string distance = "";
            int totalWeight = 0;
            List<Node> nodesArr = new List<Node>();
            int min;
            Node tmp = null;
            Node tmp_fa = null;
            while (nodeLabel.Count != nodes.Count)
            {
                min = Int32.MaxValue;
                foreach (var node in nodes)
                {
                    foreach (var vertex in node.neighbors)
                    {
                        if (vertex.weight < min && vertex.node.mark != 1)
                        {
                            min = vertex.weight;
                            tmp = vertex.node;
                            tmp_fa = node;
                        }
                    }
                }
                tmp.mark = 1;
                tmp_fa.mark = 1;
                nodesArr.Add(tmp_fa);
                nodesArr.Add(tmp);
                totalWeight += min;
                nodeLabel.Add(tmp.label);
                nodeLabel.Add(tmp_fa.label);
            }
            foreach (var node in nodesArr)
            {
                distance += node.label + " -> ";
            }
            return distance+$" Total Distance: {totalWeight}" ;
        }
        #endregion
        
        
        private Vertex getEdge(Node u, Node v)
        {
            foreach (var vertex in u.neighbors)
            {
                if (vertex.node.label == v.label)
                    return vertex;
            }
            // foreach (var vertex in v.neighbors)
            // {
            //     if (vertex.node.label == u.label)
            //         return vertex;
            // }
            return null;
        }
        private bool DFSWalk(Node u, Node v, ref LinkedList<Vertex> path
        ,ref LinkedList<string> visited)
        {
            if (u == v)
                return true;
            foreach (var vis in visited)
            {
                if (vis == u.label)
                    return false;
            }
            //bool fl = true;
            foreach(Vertex a in u.neighbors)
            {
                if(a.residualFlow() <= 0)
                    continue;
                visited.AddFirst(u.label);
                if (DFSWalk(a.node, v, ref path, ref visited))
                {
                    a.node.Father = u;//////////////////////////
                    path.AddFirst(a);
                    return true;
                }
            }
            return false;
        }

        public int FordFulkerson(string u, string v)
        {
            Node nodeU = GetNodeByName(u);
            Node nodeV = GetNodeByName(v);
            int maxFlow = 0;
            while (true)
            {

                LinkedList<Vertex> path = new LinkedList<Vertex>();
                LinkedList<string> visited = new LinkedList<string>();
                
                if (!DFSWalk(nodeU, nodeV, ref path, ref visited))
                {
                    break;
                }

                int minFlow = Int32.MaxValue;
                foreach (var p in path)
                {
                    if (p.residualFlow() < minFlow)
                        minFlow = p.residualFlow();
                }
                maxFlow += minFlow;
                foreach (var p in path)
                {
                    p.flow += minFlow;
                    Vertex antiEdge = getEdge(p.node, p.node.Father);
                    if (antiEdge != null)
                        antiEdge.flow -= minFlow;
                }
            }

            return maxFlow;
        }
        
        
        #region Checking for acyclicity(DFS)

        private bool DFSWalk(Node u)
        {
            u.mark = 1;
            bool cycle = false;
            foreach(Vertex a in u.neighbors)
            {
                if(a.node.mark == 0)
                {
                    DFSWalk(a.node); 
                }
                else
                {
                    cycle = true;
                }
            }
            return cycle;
        }


        public bool Acyclic()
        {
            foreach (var node in nodes)
            {
                ClearAllMarks();
                if (DFSWalk(node))
                    return false;
            }
            return true;
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
            StringBuilder distance = new StringBuilder();
            foreach (Node a in nodes)
            {
                distance.Append($"{a.label}: [");
                foreach (Vertex b in a.neighbors)
                {
                    distance.Append($"{{{b.node.label}, {b.weight}}}, ");
                }
                distance.Append("]\n");
            }
            return distance.ToString();
        }
        
        public string Print()
        {
            StringBuilder distance = new StringBuilder();
            if (directed != null)
            {
                if (directed == true)
                    distance.Append("Graph = directed\n");
                else
                    distance.Append("Graph = nondirected\n");
            }
            else
            {
                throw new Exception("Не определена напраленность графа!");
            }
            foreach (Node a in nodes)
            {
                distance.Append($"{a.label}: [");
                foreach (Vertex b in a.neighbors)
                {
                    distance.Append($"{{{b.node.label}, {b.weight}}}, ");
                }
                distance.Append("]\n");
            }
            return distance.ToString();
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
