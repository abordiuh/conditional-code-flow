using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace CodeFlow.Executors
{
    public class MapRunner : IMapRunnable
    {
        public Map _map;
        public int _lastSignalIndex;
        public bool _noSignalsToExecute;
        
        public void SetMap(Map map)
        {
            _map = map;
            _lastSignalIndex = 0;
            _noSignalsToExecute = false;
        }

        private Signal ProcessSignalOnNode(Node node, IEnumerable<Signal> signals)
        {
            var resultSignal = node.SignalProcessor?.ProcessSignal(signals, _map);
            return resultSignal;
        }

        private Signal ProcessSignalOnConnection(Connection connection, IEnumerable<Signal> signals)
        {
            var resultSignal = connection.SignalProcessor?.ProcessSignal(signals, _map);
            return resultSignal;
        }
        
        public void Run()
        {
            while (!_noSignalsToExecute)
            {
                RunStep();
            }
        }

        public void RunStep()
        {
            if (_lastSignalIndex >= _map.Signals.Count)
            {
                _noSignalsToExecute = true;
                _lastSignalIndex = 0;
                return;
            }

            var currentSignal = _map.Signals[_lastSignalIndex];
            var nodeToExecute = _map.Signals[_lastSignalIndex].AtNode;
            var allSignalsOnNode = _map.Signals.FindAll(s => s.AtNode == nodeToExecute);
            
            //combine signals by combining strategy and remove them from the map
            foreach (var signal in allSignalsOnNode) { _map.Signals.Remove(signal); }

            //process combined signal
            var resultSignal = ProcessSignalOnNode(nodeToExecute, allSignalsOnNode);

            //save final combined signal on the last point or not do anything
            if (resultSignal == null) return;
            
            if (nodeToExecute.OutputConnections.Count == 0)
            {
                resultSignal.AtNode = nodeToExecute;
                _map.Signals.Insert(_lastSignalIndex, resultSignal);
                _lastSignalIndex++;
            }
            
            //distribute signals through connections
            foreach (var connection in nodeToExecute.OutputConnections)
            {
                Signal newSignal = resultSignal.ShallowClone();
                var connProcessedSignal = connection.SignalProcessor?.ProcessSignal( new List<Signal>() {newSignal} , _map);
                if (connProcessedSignal != null)
                {
                    connProcessedSignal.AtNode = connection.EndNode;
                    _map.Signals.Add(connProcessedSignal);
                }
            }
        }
    }
}