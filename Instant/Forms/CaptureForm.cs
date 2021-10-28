using Instant.Tasks;

namespace Instant.Forms
{
    public partial class CaptureForm : Form
    {
        private bool mouseDragging;
        private Rectangle selectionArea;
        private Snapshot snapshot;

        private Point mousePosition;

        public CaptureForm()
        {
            InitializeComponent();
            KeyPress += CaptureForm_KeyPress;
            snapshotPictureBox.MouseMove += SnapshotPictureBox_MouseMove;
            snapshotPictureBox.MouseDown += SnapshotPictureBox_MouseDown;
            snapshotPictureBox.MouseUp += SnapshotPictureBox_MouseUp;
            snapshotPictureBox.Paint += SnapshotPictureBox_Paint;
        }

        public CaptureForm(Snapshot snapshot) : this()
        {
            this.snapshot = snapshot;

            Width = snapshot.Image.Width;
            Height = snapshot.Image.Height;
            snapshotPictureBox.Image = snapshot.Image;

            selectionArea = new Rectangle(0, 0, Width, Height);
        }

        private void SnapshotPictureBox_MouseUp(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseDragging = false;

                try
                {
                    var width = selectionArea.Width <= 0 ? 1 : selectionArea.Width;
                    var height = selectionArea.Height <= 0 ? 1 : selectionArea.Height;

                    var img = new Bitmap(width, height);
                    using (var g = Graphics.FromImage(img))
                    {
                        g.DrawImage(snapshot.Image, -selectionArea.X, -selectionArea.Y);
                    }

                    _ = new Snapshot(img, new List<ITask>()
                    {
                        new ClipboardTask()
                    });
                }
                catch (Exception ex)
                {
                    Close();
                    MessageBox.Show($"Failed to take snapshot!\n{ex.Message}", "Instant", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                Close();
            }
        }

        private void SnapshotPictureBox_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseDragging = true;
                selectionArea = new Rectangle(e.X, e.Y, 0, 0);
            }
        }

        private void SnapshotPictureBox_MouseMove(object? sender, MouseEventArgs e)
        {
            if (mouseDragging)
            {
                mousePosition = e.Location;

                if (mousePosition.X < selectionArea.Left) mousePosition.X = selectionArea.Left;
                if (mousePosition.Y < selectionArea.Top) mousePosition.Y = selectionArea.Top;

                snapshotPictureBox.Invalidate();
            }
        }

        private void SnapshotPictureBox_Paint(object? sender, PaintEventArgs e)
        {
            if (mouseDragging)
            {
                using (var g = Graphics.FromImage(snapshotPictureBox.Image))
                {
                    var width = mousePosition.X - selectionArea.X;
                    var heigth = mousePosition.Y - selectionArea.Y;

                    selectionArea = new Rectangle(selectionArea.Left, selectionArea.Top, width, heigth);

                    ControlPaint.DrawReversibleFrame(selectionArea, Color.White, FrameStyle.Dashed);
                }
            }
        }

        private void CaptureForm_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                Close();
            }
        }
    }
}