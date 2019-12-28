using System;
using System.Collections.Generic;
using System.Text;

namespace ConditionalCore
{
    public class ConditionalInput : IExecutableCondition
    {
        protected int orderId = 1000;
        protected Func<CData, CData> tryTriggerAction;
        protected CData inputCData = new CData();
        protected CData outputCData = new CData();

        public string InputName;

        public string getName()
        {
            return InputName;
        }

        public ConditionalInput(string inputName)
        {
            InputName = inputName;
        }

        public ConditionalInput(Func<CData, CData> tryTrigger)
        {
            InputName = this.GetType().Name;
            tryTriggerAction = tryTrigger;
        }

        public ConditionalInput(Func<CData, CData> tryTrigger, string inputName)
        {
            InputName = inputName;
            tryTriggerAction = tryTrigger;
        }

        public void setInputCData(CData inputData)
        {
            inputCData = inputData;
        }

        public void setOrderId(int desireOrderID)
        {
            orderId = desireOrderID;
        }

        public int getOrderId()
        {
            return orderId;
        }

        public CData Execute()
        {
            outputCData = tryTriggerAction(inputCData);
            return outputCData;
        }

        public CData getInputCData()
        {
            return inputCData;
        }

        public CData getOutputCData()
        {
            return outputCData;
        }

        public void Reset() {
            outputCData = new CData(false);
            inputCData = new CData(false);
        }
    }
}
