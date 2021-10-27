namespace Instant.Tasks
{
    public interface ITask
    {
        void Execute(Snapshot snapshot);
    }
}