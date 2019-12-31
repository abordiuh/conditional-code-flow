using System.Collections.Generic;

namespace CodeFlow
{
    public class Node
    {
        //cross reference
        private List<Connection> inputConnetctions; // DENDRITES
        private List<Connection> outputConnetctions; // AXONS

        // can contain behavior / decorators objects
        
        //need to store a link to execution method
        
        public Node ShallowClone() {
            return (Node)this.MemberwiseClone();
        }
    }
}