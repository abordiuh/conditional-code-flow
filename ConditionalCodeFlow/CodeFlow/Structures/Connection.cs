using System;
using CodeFlow.Customization;
using Newtonsoft.Json;

namespace CodeFlow
{
    public class Connection
    {
        /// <summary>
        /// Properties
        /// </summary>
        public int Id { get; set; } = -1;
        public string Name { get; set; }
        public int StartNodeId { get; set; } = -1;
        public int EndNodeId { get; set; } = -1;
        public ISignalProcessable SignalProcessor { get; set; }

        /// <summary>
        /// Cross - References for simplification of usability (Ignored During serialization)
        /// </summary>
        [JsonIgnore]
        public Node StartNode;
        [JsonIgnore]
        public Node EndNode;
    }
}