using System;
using CodeFlow.Customization;

namespace CodeFlow
{
    public class Connection
    {
        public int Id = 0;

        public string Name;

        //cross reference
        public Node StartNode;
        public Node EndNode;

        // can contain behavior / decorators objects

        public ISignalProcessable SignalProcessor;
        
        public Connection(string name = "")
        {
        }
        
        public Connection ShallowClone() {
            return (Connection)this.MemberwiseClone();
        }
    }
}