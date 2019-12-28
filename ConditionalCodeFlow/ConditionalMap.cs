using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConditionalCore;

namespace ConditionalCore
{
    public class ConditionalMap
    {
        private ConditionalDataCore dataCore = new ConditionalDataCore();
        protected string inputLevelName = "input";
        protected int InputCountNumber = 0;

        public int InputCount()
        {
            return InputCountNumber;
        }

        public List<KeyValuePair<string, ConditionalLevel>> getLevelsList()
        {
            return dataCore.levelsList.ToList();
        }

        public Dictionary<string, ConditionalLevel> getLevels()
        {
            return dataCore.levelsList;
        }

        public List<KeyValuePair<string, ConditionalInput>> getInputsList()
        {
            return dataCore.inputsList.ToList();
        }

        public Dictionary<string, ConditionalInput> getInputs()
        {
            return dataCore.inputsList;
        }

        public ConditionalMap(string inputLevelName)
        {
            this.inputLevelName = inputLevelName;
            dataCore.setInputLevelName(this.inputLevelName);
        }

        public bool AddService(string levelName, ConditionalService service)
        {
            return dataCore.AddServiceInstance(levelName, service);
        }

        public void AddLevel(string levelName)
        {
            dataCore.AddLevelInstance(levelName);
        }

        public void AddLevel(string levelName, ConditionalLevel level)
        {
            dataCore.AddLevelInstance(levelName, level);
        }

        public void AddInput(ConditionalInput input)
        {
            if (dataCore.AddInputInstance(input))
            {
                InputCountNumber++;
            }
        }

        public bool updateInputsCData(List<CData> dataList)
        {
            if (dataCore.updateInputsCData(dataList))
            {
                return true;
            }
            else return false;
        }

        public bool updateInputsCData(CData data, string name)
        {
            if (dataCore.updateInputsCData(data, name))
            {
                return true;
            }
            else return false;
        }

        public void resetInputs()
        {
            dataCore.resetInputs();
        }

        public void resetServices()
        {
            dataCore.resetServices();
        }

        public CData getCData(string level, string ServiceInputName)
        {
            return dataCore.getOutputCData(level, ServiceInputName);
        }

        public void TryExecute()
        {
            dataCore.executeInputs();
            dataCore.tryExecute();
        }
    }
}
