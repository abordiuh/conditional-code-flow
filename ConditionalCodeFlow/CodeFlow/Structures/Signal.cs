using System;
using System.IO;
using System.Xml.Serialization;

namespace CodeFlow
{
    public class Signal
    {
        public Node Node { get; set; }
        
        // can contain behavior / decorators objects
        public ICloneable Data { get; set; }
        
        public Signal ShallowClone()
        {
            Signal clone = (Signal) this.MemberwiseClone();
            clone.Data = (ICloneable)this.Data.Clone();
            return clone;
        }
    }
}