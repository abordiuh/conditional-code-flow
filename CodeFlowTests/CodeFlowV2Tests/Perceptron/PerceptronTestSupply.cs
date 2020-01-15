using System;
using System.Collections.Generic;
using System.Linq;
using CodeFlow;
using CodeFlow.Customization;

namespace ConditionalCodeFlowTest.CodeFlowV2Tests
{

    class SignalFloatData: ICloneable
    {
        public float Value { get; set; }
        public object Clone()
        {
            return (SignalFloatData)this.MemberwiseClone();
        }
    }
        
    class WeightSignalProcessor : ISignalProcessable
    {
        public float Weight;

        public WeightSignalProcessor(float weight)
        {
            this.Weight = weight;
        }
                
        public Signal ProcessSignal(IEnumerable<Signal> signals)
        {
            if (signals.Any() && signals.First().Data is SignalFloatData)
            {
                SignalFloatData signalData = (signals.First().Data as SignalFloatData);
                signalData.Value = this.Weight * signalData.Value;
                return signals.First();
            }
            else
            {
                var sign = new Signal();
                sign.Data = new SignalFloatData() {Value = 0};
                return sign;
            }
        }
    }

    class NeuronSignalProcessor : ISignalProcessable
    {
        public float stepPassValue = 0;

        public NeuronSignalProcessor(float stepPositiveValue)
        {
            this.stepPassValue = stepPositiveValue;
        }
                
        public Signal ProcessSignal(IEnumerable<Signal> signals)
        {
            var resSignal = new Signal();
            resSignal.Data = new SignalFloatData() {Value = 0};
                
            if (signals.Any())
            {
                //this is actually lambda instead of for loop SUM
                float x = signals.Where(signal => signal.Data != null && signal.Data is SignalFloatData).Sum(signal => ((SignalFloatData) signal.Data).Value);
                if (x > stepPassValue)
                {
                    resSignal.Data = new SignalFloatData() {Value = x};
                }
            }

            return resSignal;
        }
    }
    
    class InputNeuronSignalProcessor : ISignalProcessable
    { 
        public Signal ProcessSignal(IEnumerable<Signal> signals)
        {
            return signals.First();
        }
    }
}