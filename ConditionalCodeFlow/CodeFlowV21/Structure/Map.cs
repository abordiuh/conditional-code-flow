using System;
using System.Collections.Generic;
using System.Text;

namespace CodeFlowV21.Structure
{
    public class Map
    {
        // Global storage for nodes and connections
        public List<Node> Nodes;
        public List<Connection> Connections;
        public List<Signal> Signals;

        public Map()
        {
            this.Nodes = new List<Node>();
            this.Connections = new List<Connection>();
            this.Signals = new List<Signal>();
        }
    }
}
