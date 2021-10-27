using Instant.Tasks;

namespace Instant
{
    public class Snapshot
    {
        public Image Image { get; private set; }
        // public event EventHandler SnapshotTaken;

        public Snapshot(Image image, IEnumerable<ITask> tasks)
        {
            Image = image;

            foreach (var task in tasks)
            {
                task.Execute(this);
            }
        }

        public static Snapshot Take(Rectangle bounds, IEnumerable<ITask> tasks)
        {
            var bitmap = new Bitmap(bounds.Width, bounds.Height);

            using var graphics = Graphics.FromImage(bitmap);
            graphics.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
            
            var snapshot = new Snapshot(bitmap, tasks);
            return snapshot;
        }
    }
}