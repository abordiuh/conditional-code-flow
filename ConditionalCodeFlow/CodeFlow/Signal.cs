namespace CodeFlow
{
    public class Signal
    {
        // can contain behavior / decorators objects

        public Signal ShallowClone() {
            return (Signal)this.MemberwiseClone();
        }
    }
}