using System;
using System.Collections.Generic;

namespace CodeFlow
{
    public class Map
    {
        // Global storage for nodes and connections
        public List<Node> Nodes { get; set; }
        public List<Connection> Connections { get; set; }
        public List<Signal> Signals { get; set; }

        public Map()
        {
            this.Nodes = new List<Node>();
            this.Connections = new List<Connection>();
            this.Signals = new List<Signal>();
        }
    }
}