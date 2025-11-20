using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp1.Helpers
{
    public static class ToastHelper
    {
        // Accept a Control so callers can pass UserControl or Form
        public static void Show(Control owner, string message, int durationMs = 3000)
        {
            if (owner == null)
            {
                // fallback to simple message if no owner
                MessageBox.Show(message);
                return;
            }

            var toast = new Form
            {
                FormBorderStyle = FormBorderStyle.None,
                StartPosition = FormStartPosition.Manual,
                ShowInTaskbar = false,
                BackColor = Color.FromArgb(40, 40, 40),
                Opacity = 0.95,
                Size = new Size(300, 60),
                TopMost = true
            };

            var lbl = new Label
            {
                Text = message,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9F),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleLeft,
                Dock = DockStyle.Fill,
                Padding = new Padding(10)
            };

            toast.Controls.Add(lbl);

            // position: bottom-right of owner client area
            var ownerRect = owner.RectangleToScreen(owner.ClientRectangle);
            toast.Location = new Point(ownerRect.Right - toast.Width - 20, ownerRect.Bottom - toast.Height - 20);

            var t = new System.Windows.Forms.Timer { Interval = durationMs };
            t.Tick += (s, e) =>
            {
                t.Stop();
                toast.Close();
                toast.Dispose();
                t.Dispose();
            };

            toast.Shown += (s, e) => t.Start();

            // show non-modally; Control implements IWin32Window so this works for UserControl or Form
            toast.Show(owner);
        }

        // Convenience helpers for success/error messages
        public static void ShowError(Control owner, string message, int durationMs = 3000)
        {
            Show(owner, message, durationMs);
        }

        public static void ShowError(string message, int durationMs = 3000)
        {
            Show(null, message, durationMs);
        }

        public static void ShowSuccess(Control owner, string message, int durationMs = 3000)
        {
            Show(owner, message, durationMs);
        }

        public static void ShowSuccess(string message, int durationMs = 3000)
        {
            Show(null, message, durationMs);
        }
    }
}
