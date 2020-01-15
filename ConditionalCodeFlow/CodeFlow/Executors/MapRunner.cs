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
            var resultSignal = node.SignalProcessor?.ProcessSignal(signals);
            return resultSignal;
        }

        private Signal ProcessSignalOnConnection(Connection connection, IEnumerable<Signal> signals)
        {
            var resultSignal = connection.SignalProcessor?.ProcessSignal(signals);
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
            if (_lastSignalIndex >= _map.signals.Count)
            {
                _noSignalsToExecute = true;
                _lastSignalIndex = 0;
                return;
            }

            var currentSignal = _map.signals[_lastSignalIndex];
            var nodeToExecute = _map.signals[_lastSignalIndex].Node;
            var allSignalsOnNode = _map.signals.FindAll(s => s.Node == nodeToExecute);
            
            //combine signals by combining strategy and remove them from the map
            foreach (var signal in allSignalsOnNode) { _map.signals.Remove(signal); }

            //process combined signal
            var resultSignal = ProcessSignalOnNode(nodeToExecute, allSignalsOnNode);

            //save final combined signal on the last point or not do anything
            if (resultSignal == null) return;
            
            if (nodeToExecute.OutputConnections.Count == 0)
            {
                resultSignal.Node = nodeToExecute;
                _map.signals.Insert(_lastSignalIndex, resultSignal);
                _lastSignalIndex++;
            }
            
            //distribute signals through connections
            foreach (var connection in nodeToExecute.OutputConnections)
            {
                Signal newSignal = resultSignal.ShallowClone();
                var connProcessedSignal = connection.SignalProcessor?.ProcessSignal( new List<Signal>() {newSignal} );
                if (connProcessedSignal != null)
                {
                    connProcessedSignal.Node = connection.EndNode;
                    _map.signals.Add(connProcessedSignal);
                }
            }
        }
    }
}