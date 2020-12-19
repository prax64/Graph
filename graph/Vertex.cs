using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
     class Vertex
    {
        public Node node = null;
        public int weight = 0;
        public int flow = 0;
        public  int flowCapacity = 0;

        public Vertex(Node v, int w) {
            this.node = v;
            this.weight = w;
        }
        
        public Vertex(Node v, int w, int flow, int flowCapacity) {
            this.node = v;
            this.weight = w;
            this.flow = flow;
            this.flowCapacity = flowCapacity;
        }
        
        public int residualFlow() {
            return flowCapacity - flow;
        }

    }
}
