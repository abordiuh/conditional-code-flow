using System;

namespace CodeFlow
{
    public class Connection
    {
        private string _name;
        public string Name
        {
            get => _name;
            set => _name = (string.IsNullOrEmpty(_name))? value : throw new Exception("Connection already has a name: "+ _name + " trying to set " + value);
        }
        
        //cross reference
        public Node StartNode;
        public Node EndNode;

        // can contain behavior / decorators objects

        public Connection(string name = "")
        {
            if (!string.IsNullOrEmpty(name))
                Name = name;
        }
        
        public Connection ShallowClone() {
            return (Connection)this.MemberwiseClone();
        }
    }
}