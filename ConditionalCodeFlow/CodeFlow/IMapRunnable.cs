namespace CodeFlow
{
    public interface IMapRunnable
    {
        void SetMap(Map map);
        void Run();
        void RunStep();
    }
}