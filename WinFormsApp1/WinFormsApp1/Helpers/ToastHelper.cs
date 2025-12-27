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
            ShowInternal(owner, message, durationMs, Color.FromArgb(40, 40, 40));
        }

        // Convenience helpers for success/error messages
        public static void ShowError(Control owner, string message, int durationMs = 3000)
        {
            ShowInternal(owner, message, durationMs, Color.FromArgb(220, 53, 69)); // Bootstrap danger red
        }

        public static void ShowError(string message, int durationMs = 3000)
        {
            ShowInternal(null, message, durationMs, Color.FromArgb(220, 53, 69));
        }

        public static void ShowSuccess(Control owner, string message, int durationMs = 3000)
        {
            ShowInternal(owner, message, durationMs, Color.FromArgb(25, 135, 84)); // Bootstrap success green
        }

        public static void ShowSuccess(string message, int durationMs = 3000)
        {
            ShowInternal(null, message, durationMs, Color.FromArgb(25, 135, 84));
        }

        public static void ShowWarning(Control owner, string message, int durationMs = 3000)
        {
            ShowInternal(owner, message, durationMs, Color.FromArgb(255, 193, 7)); // Bootstrap warning yellow
        }

        public static void ShowWarning(string message, int durationMs = 3000)
        {
            ShowInternal(null, message, durationMs, Color.FromArgb(255, 193, 7));
        }

        public static void ShowInfo(Control owner, string message, int durationMs = 3000)
        {
            ShowInternal(owner, message, durationMs, Color.FromArgb(13, 110, 253)); // Bootstrap info blue
        }

        public static void ShowInfo(string message, int durationMs = 3000)
        {
            ShowInternal(null, message, durationMs, Color.FromArgb(13, 110, 253));
        }

        private static void ShowInternal(Control owner, string message, int durationMs, Color backgroundColor)
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
                BackColor = backgroundColor,
                Opacity = 0.95,
                Size = new Size(450, 70),
                TopMost = true
            };

            // Add rounded corners
            toast.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, toast.Width, toast.Height, 12, 12));

            var lbl = new Label
            {
                Text = message,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleLeft,
                Dock = DockStyle.Fill,
                Padding = new Padding(20, 10, 20, 10)
            };

            toast.Controls.Add(lbl);

            // position: bottom-right of owner client area
            var ownerRect = owner.RectangleToScreen(owner.ClientRectangle);
            toast.Location = new Point(ownerRect.Right - toast.Width - 20, ownerRect.Bottom - toast.Height - 20);

            // Add fade in animation
            var fadeInTimer = new System.Windows.Forms.Timer { Interval = 20 };
            double currentOpacity = 0.0;
            fadeInTimer.Tick += (s, e) =>
            {
                currentOpacity += 0.1;
                if (currentOpacity >= 0.95)
                {
                    toast.Opacity = 0.95;
                    fadeInTimer.Stop();
                    fadeInTimer.Dispose();
                }
                else
                {
                    toast.Opacity = currentOpacity;
                }
            };

            // Auto-close timer
            var closeTimer = new System.Windows.Forms.Timer { Interval = durationMs };
            closeTimer.Tick += (s, e) =>
            {
                closeTimer.Stop();
                
                // Add fade out animation
                var fadeOutTimer = new System.Windows.Forms.Timer { Interval = 20 };
                fadeOutTimer.Tick += (s2, e2) =>
                {
                    toast.Opacity -= 0.1;
                    if (toast.Opacity <= 0)
                    {
                        fadeOutTimer.Stop();
                        fadeOutTimer.Dispose();
                        toast.Close();
                        toast.Dispose();
                    }
                };
                fadeOutTimer.Start();
                
                closeTimer.Dispose();
            };

            toast.Shown += (s, e) =>
            {
                fadeInTimer.Start();
                closeTimer.Start();
            };

            // Allow click to close
            toast.Click += (s, e) =>
            {
                closeTimer.Stop();
                toast.Close();
                toast.Dispose();
            };

            lbl.Click += (s, e) =>
            {
                closeTimer.Stop();
                toast.Close();
                toast.Dispose();
            };

            // show non-modally; Control implements IWin32Window so this works for UserControl or Form
            toast.Show(owner);
        }

        // Import for rounded corners
        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
        );
    }
}
