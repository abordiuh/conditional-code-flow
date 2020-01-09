using System;
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

        private Signal CombineSignalsNeedToBeInStrategyObject(List<Signal> signals){ return new Signal();}
        
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
            var resultSignal = CombineSignalsNeedToBeInStrategyObject(allSignalsOnNode);
            foreach (var signal in allSignalsOnNode) { _map.signals.Remove(signal); }
            //_map.signals.Remove(currentSignal);
            
            //save final combined signal on the last point
            if (nodeToExecute.OutputConnetctions.Count == 0)
            {
                resultSignal.Node = nodeToExecute;
                _map.signals.Insert(_lastSignalIndex, resultSignal);
                _lastSignalIndex++;
            }
            
            //distribute signals through connections
            foreach (var connection in nodeToExecute.OutputConnetctions)
            {
                var newSignal = resultSignal.ShallowClone();
                newSignal.Node = connection.EndNode;
                _map.signals.Add(newSignal);
            }
        }
    }
}