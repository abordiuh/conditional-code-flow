namespace CodeFlow
{
    public class Signal
    {
        public Node Node { get; set; }
        
        // can contain behavior / decorators objects

        public Signal ShallowClone() {
            return (Signal)this.MemberwiseClone();
        }
    }
}