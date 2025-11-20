using System;
using System.Drawing;
using System.Windows.Forms;
using WinFormsApp1.Helpers;
using Timer = System.Windows.Forms.Timer;

namespace WinFormsApp1.View.Components
{
    public partial class ModernToast : Form
    {
        private Timer fadeTimer;
        private Timer displayTimer;
        private double opacity = 0.0;
        private bool isClosing = false;

        public enum ToastType
        {
            Success,
            Error,
            Warning,
            Info
        }

        public ModernToast(string message, ToastType type = ToastType.Info)
        {
            InitializeComponent();
            SetupToast(message, type);
        }

        private void SetupToast(string message, ToastType type)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.Size = new Size(350, 80);
            this.BackColor = GetBackgroundColor(type);
            this.Opacity = 0;

            // Tạo label cho message
            var lblMessage = new Label
            {
                Text = message,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                AutoSize = false,
                Size = new Size(300, 60),
                Location = new Point(40, 10),
                TextAlign = ContentAlignment.MiddleLeft
            };

            // Tạo icon
            var lblIcon = new Label
            {
                Text = GetIcon(type),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                Size = new Size(30, 60),
                Location = new Point(5, 10),
                TextAlign = ContentAlignment.MiddleCenter
            };

            this.Controls.Add(lblMessage);
            this.Controls.Add(lblIcon);

            // Đặt vị trí ở góc phải dưới màn hình
            var screen = Screen.PrimaryScreen.WorkingArea;
            this.Location = new Point(screen.Right - this.Width - 20, screen.Bottom - this.Height - 20);

            // Tạo border radius effect
            this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, this.Width, this.Height, 10, 10));

            SetupTimers();
        }

        private Color GetBackgroundColor(ToastType type)
        {
            return type switch
            {
                ToastType.Success => Color.FromArgb(76, 175, 80),
                ToastType.Error => Color.FromArgb(244, 67, 54),
                ToastType.Warning => Color.FromArgb(255, 152, 0),
                ToastType.Info => ColorPalette.ButtonPrimary,
                _ => ColorPalette.ButtonPrimary
            };
        }

        private string GetIcon(ToastType type)
        {
            return type switch
            {
                ToastType.Success => "✓",
                ToastType.Error => "✕",
                ToastType.Warning => "⚠",
                ToastType.Info => "ℹ",
                _ => "ℹ"
            };
        }

        private void SetupTimers()
        {
            // Timer để fade in
            fadeTimer = new Timer { Interval = 50 };
            fadeTimer.Tick += FadeTimer_Tick;

            // Timer để hiển thị
            displayTimer = new Timer { Interval = 3000 };
            displayTimer.Tick += DisplayTimer_Tick;

            fadeTimer.Start();
            displayTimer.Start();
        }

        private void FadeTimer_Tick(object sender, EventArgs e)
        {
            if (!isClosing)
            {
                if (opacity < 0.9)
                {
                    opacity += 0.1;
                    this.Opacity = opacity;
                }
                else
                {
                    fadeTimer.Stop();
                }
            }
            else
            {
                if (opacity > 0)
                {
                    opacity -= 0.1;
                    this.Opacity = opacity;
                }
                else
                {
                    fadeTimer.Stop();
                    this.Close();
                }
            }
        }

        private void DisplayTimer_Tick(object sender, EventArgs e)
        {
            displayTimer.Stop();
            isClosing = true;
            fadeTimer.Start();
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            isClosing = true;
            displayTimer.Stop();
            fadeTimer.Start();
        }

        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        public static void Show(Form parent, string message, ToastType type = ToastType.Info)
        {
            var toast = new ModernToast(message, type);
            toast.Show();
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(350, 80);
            Name = "ModernToast";
            ResumeLayout(false);
        }
    }
}