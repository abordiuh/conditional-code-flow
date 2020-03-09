using System;
using System.Collections.Generic;
using CodeFlow.Customization;
using Newtonsoft.Json;

namespace CodeFlow
{
    public class Node
    {
        /// <summary>
        /// Properties
        /// </summary>
        public int Id { get; set; } = -1;
        public string Name { get; set; } = "";
        public int SignalProcessorId { get; set; } = -1;
        public ISignalProcessable SignalProcessor { get; set; }

        /// <summary>
        /// Cross - References (ignored during serialization)
        /// </summary>
        [JsonIgnore]
        public List<Connection> OutputConnections = new List<Connection>();
        [JsonIgnore]
        public List<Connection> InputConnections = new List<Connection>();
    }
}