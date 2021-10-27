using System.Diagnostics;
using System.Runtime.InteropServices;
using Instant.Forms;
using Instant.Native;
using Instant.Properties;
using Instant.Tasks;

namespace Instant
{
    internal class InstantApplicationContext : ApplicationContext
    {
        private readonly NotifyIcon icon;

        private const int WM_KEYDOWN = 0x0100;
        private const int VK_SNAPSHOT = 0x2C;

        private event EventHandler<EventArgs> SnapshotTaken;

        private IntPtr hookId;
        private NativeMethods.HookProc keyboardCallback;

        public InstantApplicationContext()
        {
            icon = new NotifyIcon()
            {
                Text = "Instant",
                Icon = Resources.Instant,
                ContextMenuStrip = new ContextMenuStrip(),
                Visible = true
            };

            icon.ContextMenuStrip.Items.Add("Close", null, (sender, args) =>
            {
                icon.Visible = false;
                NativeMethods.UnhookWindowsHookEx(hookId);
                Application.Exit();
            });

            keyboardCallback = OnGlobalKeyboardEvent;

            using var process = Process.GetCurrentProcess();
            using var module = process.MainModule;

            if (module == null) Application.Exit();

            var handle = NativeMethods.GetModuleHandle(module.FileName);
            hookId = NativeMethods.SetWindowsHookEx(HookType.WH_KEYBOARD_LL, keyboardCallback, handle, 0);

            SnapshotTaken += OnSnapshotTaken;
        }

        private void OnSnapshotTaken(object? sender, EventArgs e)
        {
            var snapshot = Snapshot.Take(Screen.PrimaryScreen.Bounds, new List<ITask>
            {
                // new ImgurTask()
            });

            new CaptureForm(snapshot).Show();
        }

        private IntPtr OnGlobalKeyboardEvent(int code, IntPtr wParam, IntPtr lParam)
        {
            if (code < 0) return NativeMethods.CallNextHookEx(IntPtr.Zero, code, wParam, lParam);

            if (wParam == (IntPtr)WM_KEYDOWN)
            {
                var keyCode = Marshal.ReadInt32(lParam);
                if (keyCode == VK_SNAPSHOT)
                {
                    SnapshotTaken(this, EventArgs.Empty);
                }
            }

            return NativeMethods.CallNextHookEx(hookId, code, wParam, lParam);
        }
    }
}