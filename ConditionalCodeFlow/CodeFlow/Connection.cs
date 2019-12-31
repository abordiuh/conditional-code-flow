namespace CodeFlow
{
    public class Connection
    {
        //cross reference
        public Node StartNode;
        public Node EndNode;

        // can contain behavior / decorators objects
        
        public Node ShallowClone() {
            return (Node)this.MemberwiseClone();
        }
    }
}