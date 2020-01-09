using System;
using System.Collections.Generic;

namespace CodeFlow
{
    public class Map
    {
        // Global storage for nodes and connections
        public List<Node> nodes;
        public List<Connection> connections;
        public List<Signal> signals;

        public Map()
        {
            this.nodes = new List<Node>();
            this.connections = new List<Connection>();
            this.signals = new List<Signal>();
        }
    }
}