using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    class Node
    {
        public int data = 0;
        public short mark = 0;
        public string label;
        public int distanceToMe = 0;
        public Node Father = null;
        public LinkedList<Vertex> neighbors = new LinkedList<Vertex>();

        public Node(int data, string label) {
            this.data = data;
            this.label = label;
        }

    }
}
