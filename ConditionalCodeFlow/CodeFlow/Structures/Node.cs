using System;
using System.Collections.Generic;

namespace CodeFlow
{
    public class Node
    {
        private string _name;
        public string Name
        {
            get => _name;
            set => _name = (string.IsNullOrEmpty(_name))? value : throw new Exception("Node already has a name: "+ _name + " trying to set " + value);
        }

        //cross reference
        private List<Connection> _inputConnetctions = new List<Connection>(); // DENDRITES
        private List<Connection> _outputConnetctions = new List<Connection>(); // AXONS

        public List<Connection> OutputConnetctions => _outputConnetctions;

        public List<Connection> InputConnetctions => _inputConnetctions;

        // can contain behavior / decorators objects
        //need to store a link to execution method
        public void UpdateInputConnections(Connection connection)
        {
            if (!_inputConnetctions.Exists(c => c == connection))
                _inputConnetctions.Add(connection);
        }

        public void UpdateOutputConnections(Connection connection)
        {
            if (!_outputConnetctions.Exists(c => c == connection))
                _outputConnetctions.Add(connection);
        }

        public Node ShallowClone()
        {
            return (Node)this.MemberwiseClone();
        }
    }
}