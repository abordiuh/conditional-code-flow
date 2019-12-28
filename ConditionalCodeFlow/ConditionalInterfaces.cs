using System;
using System.Collections.Generic;
using System.Text;

namespace ConditionalCore
{

    public class TupleList<T1, T2, T3> : List<Tuple<T1, T2, T3>>
    {
        public void Add(T1 levelName, T2 serviceName, T3 isSoftlyRelated)
        {
            Add(new Tuple<T1, T2, T3>(levelName, serviceName, isSoftlyRelated));
        }
    }

    public class CInputLink
    {
        public TupleList<string, string, bool> InputsList;

        // Constructor is private: values are defined within this class only!
        protected CInputLink(TupleList<string, string, bool> inputsList)
        {
            InputsList = inputsList;
        }
    }


    public interface IExecutableCondition
    {
        string getName();
        CData Execute();
        CData getOutputCData();
    }
}
