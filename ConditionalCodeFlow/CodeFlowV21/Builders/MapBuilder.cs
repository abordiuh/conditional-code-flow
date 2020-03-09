using CodeFlowV21.Structure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeFlowV21.Builders
{
    public class MapBuilder
    {
        private Map _map;

        public Map map
        {
            get => _map;
            set => _map = value;
        }

        public MapBuilder(Map map = null)
        {
            if (map != null) this.map = map;
        }

        public void AddNode(Node node, string name = "")
        {
            node.Name = name;

            int maxId = 1;
            if (_map.Nodes.Count > 0)
                maxId = _map.Nodes.Max(n => n.Id);
            node.Id = maxId + 1;
            _map.Nodes.Add(node);
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
            var startNode = _map.Nodes.Find(n => n.Name == startName);
            var endNode = _map.Nodes.Find(n => n.Name == endName);

            if (startNode == null || endNode == null)
            {
                throw new Exception("Can't create connection. Node with name " + startName + " or " + endName + " not found exception");
            }

            if (connection == null)
            {
                connection = new Connection();
            }

            connection.StartNodeId = startNode.Id;
            connection.EndNodeId = endNode.Id;
            connection.StartNode = startNode;
            connection.EndNode = endNode;
            AddConnection(connection);
        }

        public void AddConnection(Node start, Node end)
        {
            var conn = new Connection() { StartNode = start, EndNode = end };
            AddConnection(conn);
        }

        public void AddConnection(Connection connection)
        {
            int maxId = 1;
            if (_map.Connections.Count > 0)
                maxId = _map.Connections.Max(c => c.Id);
            connection.Id = maxId + 1;
            _map.Connections.Add(connection);
            var startNode = _map.Nodes.Find(n => n == connection.StartNode);
            var endNode = _map.Nodes.Find(n => n == connection.EndNode);
            startNode?.OutputConnections.Add(connection);
            endNode?.InputConnections.Add(connection);
        }

        public void AddSignal(Signal signal, string nodeName)
        {
            var node = _map.Nodes.FirstOrDefault(n => n.Name == nodeName);
            if (node != null)
            {
                signal.AtNodeId = node.Id;
                signal.AtNode = node;
                _map.Signals.Add(signal);
            }
            else throw new Exception("Can't add signal. Node with name " + nodeName + " not found exception");
        }

        public void ClearSignals()
        {
            map.Signals.Clear();
        }

        public Node GetNodeByName(string name)
        {
            return _map.Nodes.FirstOrDefault(n => n.Name == name);
        }

        public Map CreateMapFromJson(string mapJson)
        {
            _map.Signals.Clear();
            _map.Nodes.Clear();
            _map.Nodes.Clear();

            //_map = JsonConvert.DeserializeObject<Map>(mapJson);
            return _map;
        }
    }
}