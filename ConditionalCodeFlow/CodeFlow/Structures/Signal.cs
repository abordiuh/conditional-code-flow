using Newtonsoft.Json;
using System;
using System.IO;
using System.Xml.Serialization;

namespace CodeFlow
{
    public class Signal
    {
        /// <summary>
        /// Properties
        /// </summary>
        public int Id { get; set; } = -1;
        public int AtNodeId { get; set; } = -1;
        public int AtConnectionId { get; set; } = -1;
        public ICloneable Data { get; set; }

        /// <summary>
        /// Cross - Refernces, ignored during serialization
        /// </summary>
        [JsonIgnore]
        public Node AtNode;

        public Signal ShallowClone()
        {
            Signal clone = (Signal) this.MemberwiseClone();
            if(Data != null)
                clone.Data = (ICloneable)this.Data.Clone();
            return clone;
        }
    }
}