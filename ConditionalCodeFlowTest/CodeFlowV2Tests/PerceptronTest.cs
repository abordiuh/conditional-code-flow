using System;
using System.Collections;
using Xunit;
using ConditionalCore;
using System.Collections.Generic;
using CodeFlow;
using CodeFlow.Executors;

namespace ConditionalCodeFlowTest.CodeFlowV2Tests
{
    public class Cf2PerceptronTest
    {
        private Map map;
        private IMapRunnable mapRunner;
        MapBuilder builder;
        
        public Cf2PerceptronTest()
        {
            map = new Map();
            builder = new MapBuilder(map);
            mapRunner = MapRunners.DefaultMapRunner;
            mapRunner.SetMap(map);
            
            builder.AddNode(Nodes.DefaultNode, "n1l0");            
            builder.AddNode(Nodes.DefaultNode, "n2l0");
            builder.AddNode(Nodes.DefaultNode, "n3l0");
            builder.AddNode(Nodes.DefaultNode, "n1l1");
            builder.AddNode(Nodes.DefaultNode, "n2l1");
            builder.AddNode(Nodes.DefaultNode, "n3l1");
            builder.AddNode(Nodes.DefaultNode, "n1l2");
            builder.AddNode(Nodes.DefaultNode, "n2l2");
            builder.AddNode(Nodes.DefaultNode, "n3l2");
            
            builder.AddConnections(new string[]{"n1l0"}, new string[]{"n1l1", "n2l1", "n3l1"});
            builder.AddConnections(new string[]{"n2l0"}, new string[]{"n1l1", "n2l1", "n3l1"});
            builder.AddConnections(new string[]{"n3l0"}, new string[]{"n1l1", "n2l1", "n3l1"});
            
            builder.AddConnections(new string[]{"n1l1"}, new string[]{"n1l2", "n2l2", "n3l2"});
            builder.AddConnections(new string[]{"n2l1"}, new string[]{"n1l2", "n2l2", "n3l2"});
            builder.AddConnections(new string[]{"n3l1"}, new string[]{"n1l2", "n2l2", "n3l2"});
            
            builder.AddSignal(new Signal(), "n1l0");
            builder.AddSignal(new Signal(), "n2l0");
            builder.AddSignal(new Signal(), "n3l0");
        }
        
        [Fact]
        public void TestCF2_Perceptron_ElementsCount()
        {
            Assert.Equal(9,map.nodes.Count);
            Assert.Equal(18, map.connections.Count);
            Assert.Equal(3,map.signals.Count);
        }

        [Fact]
        public void TestCF2_Perceptron_SignalDistribution()
        {
            mapRunner.Run();
            Assert.Equal(3, map.signals.Count);
            Assert.Equal(builder.GetNodeByName("n1l2"), map.signals[0].Node);
            Assert.Equal(builder.GetNodeByName("n2l2"), map.signals[1].Node);
            Assert.Equal(builder.GetNodeByName("n3l2"), map.signals[2].Node);
        }
    }
}