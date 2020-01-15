using System;
using System.Collections.Generic;
using CodeFlow.Customization;

namespace CodeFlow
{
    public class Node
    {
        //cross reference
        private List<Connection> _inputConnections = new List<Connection>(); // DENDRITES
        private List<Connection> _outputConnections = new List<Connection>(); // AXONS

        // properties
        private string _name;
        public string Name
        {
            get => _name;
            set => _name = (string.IsNullOrEmpty(_name))? value : throw new Exception("Node already has a name: "+ _name + " trying to set " + value);
        }

        public ISignalProcessable SignalProcessor;
        
        public List<Connection> OutputConnections => _outputConnections;

        public List<Connection> InputConnections => _inputConnections;

        // can contain behavior / decorators objects
        //need to store a link to execution method
        public void UpdateInputConnections(Connection connection)
        {
            if (!_inputConnections.Exists(c => c == connection))
                _inputConnections.Add(connection);
        }

        public void UpdateOutputConnections(Connection connection)
        {
            if (!_outputConnections.Exists(c => c == connection))
                _outputConnections.Add(connection);
        }

        public Node ShallowClone()
        {
            return (Node)this.MemberwiseClone();
        }
    }
}