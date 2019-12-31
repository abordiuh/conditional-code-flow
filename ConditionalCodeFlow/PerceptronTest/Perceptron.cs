using System;
using System.Collections.Generic;
using ConditionalCore;

namespace ConditionalCodeFlow.PerceptronTest
{
    public class Perceptron : ConditionalService
    {
        public List<float> ConnectionWeights = new List<float>();
        
        public Perceptron(Func<List<CData>, CData> onExecute) : base(onExecute)
        {
        }

        public Perceptron(Func<List<CData>, CData> onExecute, string serviceName) : base(onExecute, serviceName)
        {
        }

        public Perceptron(Func<List<CData>, CData> onExecute, TupleList<string, string, bool> levelServicePair) : base(onExecute, levelServicePair)
        {
        }

        public Perceptron(Func<List<CData>, CData> onExecute, string serviceName, TupleList<string, string, bool> levelServicePair) : base(onExecute, serviceName, levelServicePair)
        {
        }
        
        public void setInputs(TupleListX4<string, string, bool, float> levelServicePair) 
        {
            //base.setInputs(levelServicePair);   
        }
    }
}