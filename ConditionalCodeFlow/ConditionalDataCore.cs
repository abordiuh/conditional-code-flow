using System;
using System.Collections.Generic;
using System.Text;

namespace ConditionalCore
{
    public class ConditionalDataCore
    {
        public Dictionary<string, ConditionalLevel> levelsList { get; }
        public Dictionary<string, ConditionalInput> inputsList { get; }
        private string inputLevelName = "input";

        public ConditionalDataCore()
        {
            levelsList = new Dictionary<string, ConditionalLevel>();
            inputsList = new Dictionary<string, ConditionalInput>();
        }

        public void setInputLevelName(string inputLevelName)
        {
            this.inputLevelName = inputLevelName;
        }

        public bool AddInputInstance(/*string levelName,*/ ConditionalInput input)
        {
            input.setOrderId(levelsList.Count);
            inputsList.Add(input.getName(), input);
            return true;
        }

        public bool AddLevelInstance(string levelName)
        {
            if (!levelsList.ContainsKey(levelName))
            {
                ConditionalLevel level = new ConditionalLevel();
                levelsList.Add(levelName, level);
                return true;
            }
            else
                return false;
        }

        public bool AddLevelInstance(string levelName, ConditionalLevel level)
        {
            if (!levelsList.ContainsKey(levelName))
            {
                levelsList.Add(level.getName(), level);
                return true;
            }
            else
                return false;
        }

        public bool AddServiceInstance(string levelName, ConditionalService service)
        {
            if (levelsList.ContainsKey(levelName))
                return levelsList[levelName].AddService(service);
            return false;
        }

        public void executeInputs()
        {
            foreach (KeyValuePair<string, ConditionalInput> entryInput in inputsList)
            {
                entryInput.Value.Execute();
            }
        }

        public bool updateInputsCData(List<CData> dataList)
        {
            Dictionary<string, ConditionalInput> inputsListTemp = new Dictionary<string, ConditionalInput>(inputsList);
            int i = 0;
            foreach (KeyValuePair<string, ConditionalInput> entryInput in inputsListTemp)
            {
                if (dataList.Count > i)
                {
                    inputsList[entryInput.Key].setInputCData(dataList[i]);
                    i++;
                }
                else return false;
            }
            return true;
        }

        public bool updateInputsCData(CData data, string inputName)
        {
            if (inputsList.ContainsKey(inputName))
            {
                inputsList[inputName].setInputCData(data);
                return true;
            }
            else return false;
        }

        public void tryExecute()
        {
            Dictionary<string, ConditionalLevel> levelsListTemp = new Dictionary<string, ConditionalLevel>(levelsList);
            Dictionary<string, ConditionalService> levelServicesTemp;

            foreach (KeyValuePair<string, ConditionalLevel> LevelKey in levelsListTemp)
            {
                levelServicesTemp = new Dictionary<string, ConditionalService>(LevelKey.Value.levelServices);
                Console.WriteLine("*************************************************************");
                Console.WriteLine("*************************************************************");
                Console.WriteLine("LEVEL " + LevelKey.Key);
                Console.WriteLine("*************************************************************");
                foreach (KeyValuePair<string, ConditionalService> serviceKey in levelServicesTemp)
                {
                    ConditionalService cservice = levelsList[LevelKey.Key].levelServices[serviceKey.Key];
                    Console.WriteLine("*************************************************************");
                    Console.WriteLine("Executing " + cservice.getName());
                    List<CData> data = new List<CData>();
                    if (isAllInputsTriggered(ref cservice, ref data))
                    {
                        cservice.setInputCData(data);
                        cservice.Execute();
                    }
                    else
                    {
                        Console.WriteLine(cservice.getName() + " NOT EXECUTED!");
                    }

                    levelsList[LevelKey.Key].levelServices[serviceKey.Key] = cservice;
                }
            }
        }

        public CData getOutputCData(string level, string inputOrServiceName)
        {
            CData dt = new CData();

            if (level == inputLevelName && inputsList.ContainsKey(inputOrServiceName))
            {
                dt = inputsList[inputOrServiceName].getOutputCData();
                return dt;
            }

            if (levelsList.ContainsKey(level))
            {
                if (levelsList[level].levelServices.ContainsKey(inputOrServiceName))
                {
                    dt = levelsList[level].levelServices[inputOrServiceName].getOutputCData();
                    return dt;
                }
            }
            return dt;
        }

        public bool isAllInputsTriggered(ref ConditionalService service, ref List<CData> InputCData)
        {
            if (service.inputsToService.Count == 0)
            {
                return false;
            }
            bool someInputDiactivated = false;
            foreach (Tuple<string, string, bool> inputStr in service.inputsToService)
            {
                CData data = getOutputCData(inputStr.Item1, inputStr.Item2);
                InputCData.Add(data);
                if (!(data.previousNodeExecutedOk) && !(inputStr.Item3))    //checks for soft/strong relation and then checks if previous input executed ok
                {
                    someInputDiactivated = true;
                    Console.WriteLine("Input " + inputStr.Item2 + " on level " + inputStr.Item1 + " FALSE");
                }
                else
                {
                    if (!(inputStr.Item3))
                    {
                        Console.WriteLine("Input " + inputStr.Item2 + " on level " + inputStr.Item1 + " TRUE");
                    }
                    else if ((inputStr.Item3) && !(data.previousNodeExecutedOk))
                    {
                        Console.WriteLine("Input " + inputStr.Item2 + " on level " + inputStr.Item1 + " FALSE SOFT REL");
                    }
                }
            }
            if (someInputDiactivated)
            {
                return false;
            }
            else
                return true;
        }

        public void resetInputs()
        {
            foreach (KeyValuePair<string, ConditionalInput> entryInput in inputsList)
            {
                entryInput.Value.Reset();
            }
        }

        public void resetServices()
        {
            Dictionary<string, ConditionalLevel> levelsListTemp = new Dictionary<string, ConditionalLevel>(levelsList);
            Dictionary<string, ConditionalService> levelServicesTemp;

            foreach (KeyValuePair<string, ConditionalLevel> LevelKey in levelsListTemp)
            {
                levelServicesTemp = new Dictionary<string, ConditionalService>(LevelKey.Value.levelServices);

                foreach (KeyValuePair<string, ConditionalService> serviceKey in levelServicesTemp)
                {
                    ConditionalService cservice = levelsList[LevelKey.Key].levelServices[serviceKey.Key];

                    cservice.Reset();

                    levelsList[LevelKey.Key].levelServices[serviceKey.Key] = cservice;
                }
            }
        }

    }
}
