using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp1.View.User.Controls.FlashcardControls
{
    partial class FinishControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private Panel centerPanel;
        private Label lblTitle;
        private Label lblMessage;
        private Button btnRestart;
        private Button btnViewOther;
        private Button btnGoHome;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            centerPanel = new Panel();
            lblTitle = new Label();
            lblMessage = new Label();
            btnRestart = new Button();
            btnViewOther = new Button();
            btnGoHome = new Button();
            centerPanel.SuspendLayout();
            SuspendLayout();
            // 
            // centerPanel
            // 
            centerPanel.BackColor = Color.FromArgb(60, 40, 120);
            centerPanel.BorderStyle = BorderStyle.FixedSingle;
            centerPanel.Controls.Add(lblTitle);
            centerPanel.Controls.Add(lblMessage);
            centerPanel.Controls.Add(btnRestart);
            centerPanel.Controls.Add(btnViewOther);
            centerPanel.Controls.Add(btnGoHome);
            centerPanel.Location = new Point(400, 180);
            centerPanel.Name = "centerPanel";
            centerPanel.Size = new Size(900, 600);
            centerPanel.TabIndex = 0;
            // Add rounded corners effect
            centerPanel.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using (var path = new System.Drawing.Drawing2D.GraphicsPath())
                {
                    int radius = 20;
                    var rect = new Rectangle(0, 0, centerPanel.Width - 1, centerPanel.Height - 1);
                    path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
                    path.AddArc(rect.X + rect.Width - radius, rect.Y, radius, radius, 270, 90);
                    path.AddArc(rect.X + rect.Width - radius, rect.Y + rect.Height - radius, radius, radius, 0, 90);
                    path.AddArc(rect.X, rect.Y + rect.Height - radius, radius, radius, 90, 90);
                    path.CloseFigure();
                    centerPanel.Region = new System.Drawing.Region(path);
                }
            };
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Segoe UI", 42F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(100, 80);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(700, 100);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "Làm Tốt Lắm!";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblMessage
            // 
            lblMessage.Font = new Font("Segoe UI", 15F);
            lblMessage.ForeColor = Color.FromArgb(220, 220, 255);
            lblMessage.Location = new Point(80, 190);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(740, 100);
            lblMessage.TabIndex = 2;
            lblMessage.Text = "Bạn đã hoàn thành tất cả các flashcards trong bộ này.\r\nHãy tiếp tục học tập để nâng cao kiến thức của bạn!";
            lblMessage.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnRestart
            // 
            btnRestart.BackColor = Color.FromArgb(255, 140, 0);
            btnRestart.Cursor = Cursors.Hand;
            btnRestart.FlatAppearance.BorderSize = 0;
            btnRestart.FlatStyle = FlatStyle.Flat;
            btnRestart.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            btnRestart.ForeColor = Color.White;
            btnRestart.Location = new Point(225, 320);
            btnRestart.Name = "btnRestart";
            btnRestart.Size = new Size(450, 60);
            btnRestart.TabIndex = 3;
            btnRestart.Text = "🔄 Bắt đầu lại";
            btnRestart.UseVisualStyleBackColor = false;
            btnRestart.Click += btnRestart_Click;
            // Hover effect
            btnRestart.MouseEnter += (s, e) => btnRestart.BackColor = Color.FromArgb(255, 160, 20);
            btnRestart.MouseLeave += (s, e) => btnRestart.BackColor = Color.FromArgb(255, 140, 0);
            // 
            // btnViewOther
            // 
            btnViewOther.BackColor = Color.FromArgb(76, 175, 80);
            btnViewOther.Cursor = Cursors.Hand;
            btnViewOther.FlatAppearance.BorderSize = 0;
            btnViewOther.FlatStyle = FlatStyle.Flat;
            btnViewOther.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            btnViewOther.ForeColor = Color.White;
            btnViewOther.Location = new Point(225, 395);
            btnViewOther.Name = "btnViewOther";
            btnViewOther.Size = new Size(450, 60);
            btnViewOther.TabIndex = 4;
            btnViewOther.Text = "📚 Xem thêm các flashcard khác";
            btnViewOther.UseVisualStyleBackColor = false;
            btnViewOther.Click += btnViewOther_Click;
            // Hover effect
            btnViewOther.MouseEnter += (s, e) => btnViewOther.BackColor = Color.FromArgb(96, 195, 100);
            btnViewOther.MouseLeave += (s, e) => btnViewOther.BackColor = Color.FromArgb(76, 175, 80);
            // 
            // btnGoHome
            // 
            btnGoHome.BackColor = Color.FromArgb(0, 180, 216);
            btnGoHome.Cursor = Cursors.Hand;
            btnGoHome.FlatAppearance.BorderSize = 0;
            btnGoHome.FlatStyle = FlatStyle.Flat;
            btnGoHome.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            btnGoHome.ForeColor = Color.White;
            btnGoHome.Location = new Point(225, 470);
            btnGoHome.Name = "btnGoHome";
            btnGoHome.Size = new Size(450, 60);
            btnGoHome.TabIndex = 5;
            btnGoHome.Text = "🏠 Trang chủ";
            btnGoHome.UseVisualStyleBackColor = false;
            btnGoHome.Click += btnGoHome_Click;
            // Hover effect
            btnGoHome.MouseEnter += (s, e) => btnGoHome.BackColor = Color.FromArgb(20, 200, 236);
            btnGoHome.MouseLeave += (s, e) => btnGoHome.BackColor = Color.FromArgb(0, 180, 216);
            // 
            // FinishControl
            // 
            BackColor = Color.FromArgb(40, 20, 100);
            Controls.Add(centerPanel);
            Name = "FinishControl";
            Size = new Size(1700, 1000);
            Load += FinishControl_Load;
            // Center the panel when control is resized
            this.Resize += (s, e) =>
            {
                centerPanel.Location = new Point(
                    (this.Width - centerPanel.Width) / 2,
                    (this.Height - centerPanel.Height) / 2
                );
            };
            centerPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
    }
}
