using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeFlow
{
    public class MapBuilder
    {
        private Map _map;
        
        public Map map
        {
            get => _map;
            set => _map = value;
        }

        public MapBuilder (Map map = null)
        {
            if (map != null) this.map = map;
        }
        
        public void AddNode(Node node, string name = "")
        {
            if (!string.IsNullOrEmpty(name))
                node.Name = name;
            _map.nodes.Add(node);
        }

        public void AddConnections(string[] startNames, string[] endNames, Connection connection = null)
        {
            foreach (var startName in startNames)
            {
                foreach (var endName in endNames)
                {
                    AddConnection(startName, endName);
                }
            }
        }

        public void AddConnection(string startName, string endName, Connection connection = null)
        {
            var startNode = _map.nodes.Find(n => n.Name == startName);
            var endNode = _map.nodes.Find(n => n.Name == endName);

            if (startNode == null || endNode == null)
            {
                throw new Exception("Can't create connection. Node with name " + startName + " or " + endName + " not found exception");
            }
            
            if(connection == null)
            {
                connection = new Connection();
            }

            connection.StartNode = startNode;
            connection.EndNode = endNode;
            AddConnection(connection);
        }
        
        public void AddConnection(Node start, Node end)
        {
            var conn = new Connection() {StartNode = start, EndNode = end};
            AddConnection(conn);
        }

        public void AddConnection(Connection connection)
        {
            _map.connections.Add(connection);
            var startNode = _map.nodes.Find(n => n == connection.StartNode);
            var endNode = _map.nodes.Find(n => n == connection.EndNode);
            startNode?.UpdateOutputConnections(connection);
            endNode?.UpdateInputConnections(connection);
        }

        public void AddSignal(Signal signal, string nodeName)
        {
            var node = _map.nodes.FirstOrDefault(n => n.Name == nodeName);
            if (node != null)
            {
                signal.Node = node;
                _map.signals.Add(signal);
            } else throw new Exception("Can't add signal. Node with name " + nodeName + " not found exception");
        }

        public Node GetNodeByName(string name)
        {
           return _map.nodes.FirstOrDefault(n => n.Name == name);
        }
    }
}