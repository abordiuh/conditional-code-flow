using System.Collections.Generic;
using System.Drawing;
using ConditionalCore;

namespace ConditionalCodeFlow.PerceptronTest
{
    public static class NetworkMethods
    {
        public enum LevelIds { lvl0 = 0, lvl1, ENUM_SIZE };
        
        public static CData PerceptronActivation(List<CData> dataList)
        {
            dataList[2].condition = true;
            return dataList[2];
        }

        public static ConditionalMap CreateMap()
        {
            var conditionalMap = new ConditionalMap(LevelIds.lvl0.ToString());
            conditionalMap.AddInput(new ConditionalInput((CData data) => { return data; }, "l0_i1"));
            conditionalMap.AddInput(new ConditionalInput((CData data) => { return data; }, "l0_i2"));
            conditionalMap.AddInput(new ConditionalInput((CData data) => { return data; }, "l0_i3"));

            List<CData> inputData = new List<CData>
            {
                new CData(true),
                new CData(true),
                new CData(false)
            };
            conditionalMap.updateInputsCData(inputData);

            conditionalMap.AddLevel(LevelIds.lvl1.ToString());

            conditionalMap.AddService(LevelIds.lvl1.ToString(),
                new ConditionalService(PerceptronActivation, "l1_n1", new TupleList<string, string, bool> {
                    { LevelIds.lvl0.ToString(),  "l0_i1", true },
                    { LevelIds.lvl0.ToString(),  "l0_i2", true },
                    { LevelIds.lvl0.ToString(),  "l0_i3", true }
                }));

            conditionalMap.AddService(LevelIds.lvl1.ToString(),
                new ConditionalService(PerceptronActivation, "l1_n2", new TupleList<string, string, bool> {
                    { LevelIds.lvl0.ToString(),  "l0_i1", false },
                    { LevelIds.lvl0.ToString(),  "l0_i2", false },
                    { LevelIds.lvl0.ToString(),  "l0_i3", true }
                }));

            conditionalMap.AddService(LevelIds.lvl1.ToString(),
                new ConditionalService(PerceptronActivation, "l1_n3", new TupleList<string, string, bool> {
                    { LevelIds.lvl0.ToString(),  "l0_i1", true },
                    { LevelIds.lvl0.ToString(),  "l0_i2", true },
                    { LevelIds.lvl0.ToString(),  "l0_i3", false }
                })); //"l1_n3" should not execute as "l0_i3" dependency doesn't have soft relation (=false)


            conditionalMap.TryExecute();
            
            return conditionalMap;
        }
    }
}