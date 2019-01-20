using System;
using System.Collections.Generic;
using System.Text;

namespace ConditionalCore
{
    
    public class ConditionalService : IExecutableCondition
    {
        private int orderId = 1000;
        private Func<List<CData>, CData> onExecuteAction;
        private List<CData> iDataList = new List<CData>();
        private CData oData = new CData();

        public string InputName;
        public List<Tuple<string, string, bool>> inputsToService = new List<Tuple<string, string, bool>>();

        public ConditionalService(Func<List<CData>, CData> onExecute) {
            InputName = this.GetType().Name;
            onExecuteAction = onExecute;
        }

        public ConditionalService(Func<List<CData>, CData> onExecute, string serviceName)
        {
            InputName = serviceName;
            onExecuteAction = onExecute;
        }

        public ConditionalService(Func<List<CData>, CData> onExecute, TupleList<string, string, bool> levelServicePair)
        {
            InputName = this.GetType().Name;
            inputsToService = levelServicePair;
            onExecuteAction = onExecute;
        }

        public ConditionalService(Func<List<CData>, CData> onExecute, string serviceName, TupleList<string, string, bool> levelServicePair)
        {
            InputName = serviceName;
            inputsToService = levelServicePair;
            onExecuteAction = onExecute;
        }

        public string getName()
        {
            return InputName;
        }

        public void setInputs(TupleList<string, string, bool> levelServicePair) {
            inputsToService = levelServicePair;
        }

        public void setInputCData(CData inputData, int inputCount) {
            iDataList[inputCount] = inputData;
        }

        public void setInputCData(List<CData> inputDataList)
        {
            foreach(CData data in inputDataList) { 
                iDataList.Add(data);
            }
        }

        public void setOrderId(int desireOrderID) {
            orderId = desireOrderID;
        }

        public int getOrderId() {
            return orderId;
        }

        public CData Execute()
        {
            oData = onExecuteAction(iDataList);
            return oData;
        }

        public CData getOutputCData() {
            return oData;
        }

        public void Reset()
        {
            oData = new CData(false);
            iDataList.Clear(); 
        }
    }
}
