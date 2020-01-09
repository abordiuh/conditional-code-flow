using System;
using Xunit;
using ConditionalCore;
using System.Collections.Generic;

namespace ConditionalCodeFlowTest
{
    public class ConditionalMap1LvlTestStuck
    {
        public enum LevelIds1 { input_level = 0, level1, ENUM_SIZE };
        ConditionalMap conditionalMap1;

        private CData lvl1_n_method(List<CData> dataList)
        {
            dataList[2].condition = true;
            return dataList[2];
        }

        public ConditionalMap1LvlTestStuck()
        {
            conditionalMap1 = new ConditionalMap(LevelIds1.input_level.ToString());
                     conditionalMap1.AddInput(new ConditionalInput((CData data) => { return data; }, "l0_i1"));
                     conditionalMap1.AddInput(new ConditionalInput((CData data) => { return data; }, "l0_i2"));
                     conditionalMap1.AddInput(new ConditionalInput((CData data) => { return data; }, "l0_i3"));
         
                     List<CData> inputData = new List<CData>
                     {
                         new CData(true),
                         new CData(true),
                         new CData(false)
                     };
                     conditionalMap1.updateInputsCData(inputData);
         
                     conditionalMap1.AddLevel(LevelIds1.level1.ToString());
         
                     conditionalMap1.AddService(LevelIds1.level1.ToString(),
                         new ConditionalService(lvl1_n_method, "l1_n1", new TupleList<string, string, bool> {
                             { LevelIds1.input_level.ToString(),  "l0_i1", true },
                             { LevelIds1.input_level.ToString(),  "l0_i2", true },
                             { LevelIds1.input_level.ToString(),  "l0_i3", true }
                         }));
         
                     conditionalMap1.AddService(LevelIds1.level1.ToString(),
                         new ConditionalService(lvl1_n_method, "l1_n2", new TupleList<string, string, bool> {
                             { LevelIds1.input_level.ToString(),  "l0_i1", false },
                             { LevelIds1.input_level.ToString(),  "l0_i2", false },
                             { LevelIds1.input_level.ToString(),  "l0_i3", true }
                         }));
         
                     conditionalMap1.AddService(LevelIds1.level1.ToString(),
                         new ConditionalService(lvl1_n_method, "l1_n3", new TupleList<string, string, bool> {
                             { LevelIds1.input_level.ToString(),  "l0_i1", true },
                             { LevelIds1.input_level.ToString(),  "l0_i2", true },
                             { LevelIds1.input_level.ToString(),  "l0_i3", false }
                         })); //"l1_n3" should not execute as "l0_i3" dependency doesn't have soft relation (=false)
         
         
                     conditionalMap1.TryExecute();
                 }


        [Fact]
        public void Test_CMap_3InputOnCreation()
        {
            Assert.Equal(3, conditionalMap1.InputCount());
        }


        [Fact]
        public void Test_CMap_1stInput_TrueInp_TrueOut()
        {
            Assert.True(conditionalMap1.getCData(LevelIds1.input_level.ToString(), "l0_i1").previousNodeExecutedOk);
        }


        [Fact]
        public void Test_CMap_2ndInput_TrueInp_FalseOut()
        {
            Assert.True(conditionalMap1.getCData(LevelIds1.input_level.ToString(), "l0_i2").previousNodeExecutedOk);
        }


        [Fact]
        public void Test_CMap_3rdInput_FalseInp_TrueOut()
        {
            Assert.False(conditionalMap1.getCData(LevelIds1.input_level.ToString(), "l0_i3").previousNodeExecutedOk);
        }


        [Fact]
        public void Test_CMap_2ndLvl_Node1Execution()
        {
            Assert.Equal(true, conditionalMap1.getCData(LevelIds1.level1.ToString(), "l1_n1").condition);
            Assert.False(conditionalMap1.getCData(LevelIds1.level1.ToString(), "l1_n1").previousNodeExecutedOk);
        }


        [Fact]
        public void Test_CMap_2ndLvl_Node2Execution()
        {
            Assert.Equal(true, conditionalMap1.getCData(LevelIds1.level1.ToString(), "l1_n2").condition);
            Assert.False(conditionalMap1.getCData(LevelIds1.level1.ToString(), "l1_n2").previousNodeExecutedOk);
        }


        [Fact]
        public void Test_CMap_2ndLvl_Node3Execution()
        {
            Assert.Equal(false, conditionalMap1.getCData(LevelIds1.level1.ToString(), "l1_n3").condition);
            Assert.False(conditionalMap1.getCData(LevelIds1.level1.ToString(), "l1_n3").previousNodeExecutedOk);
        }
    }

    public class ConditionalMap2LvlTestStuck
    {
        public enum LevelIds2 { input_level = 0, level1, level2, ENUM_SIZE };
        ConditionalMap conditionalMap2;

        public ConditionalMap2LvlTestStuck()
        {
            conditionalMap2 = new ConditionalMap(LevelIds2.input_level.ToString());
        }


    }
}
