namespace Instant.Tasks
{
    internal class ClipboardTask : ITask
    {
        public void Execute(Snapshot snapshot)
        {
            Clipboard.SetImage(snapshot.Image);
        }
    }
}