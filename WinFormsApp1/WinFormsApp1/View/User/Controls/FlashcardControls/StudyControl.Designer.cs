using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp1.View.User.Controls.FlashcardControls
{
    partial class StudyControl
    {
        private System.ComponentModel.IContainer components = null;

        private ProgressBar progressBar;
        private Label lblProgressPercent;
        private Label lblProgress;
        private Panel cardPanel;
        private Label lblCardText;
        private Button btnPrevious;
        private Button btnNext;
        private Label lblHint;
        private Button btnFinish;
        private Panel spaceKeyPanel;
        private Label lblSpaceKey;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            progressBar = new ProgressBar();
            lblProgressPercent = new Label();
            lblProgress = new Label();
            cardPanel = new Panel();
            lblCardText = new Label();
            btnPrevious = new Button();
            btnNext = new Button();
            lblHint = new Label();
            btnFinish = new Button();
            spaceKeyPanel = new Panel();
            lblSpaceKey = new Label();
            cardPanel.SuspendLayout();
            spaceKeyPanel.SuspendLayout();
            SuspendLayout();
            // 
            // progressBar
            // 
            progressBar.ForeColor = Color.FromArgb(255, 140, 0);
            progressBar.Location = new Point(550, 80);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(700, 15);
            progressBar.Style = ProgressBarStyle.Continuous;
            progressBar.TabIndex = 0;
            // 
            // lblProgressPercent
            // 
            lblProgressPercent.AutoSize = true;
            lblProgressPercent.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblProgressPercent.ForeColor = Color.White;
            lblProgressPercent.Location = new Point(1260, 75);
            lblProgressPercent.Name = "lblProgressPercent";
            lblProgressPercent.Size = new Size(80, 45);
            lblProgressPercent.TabIndex = 1;
            lblProgressPercent.Text = "20%";
            // 
            // lblProgress
            // 
            lblProgress.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblProgress.ForeColor = Color.White;
            lblProgress.Location = new Point(650, 115);
            lblProgress.Name = "lblProgress";
            lblProgress.Size = new Size(500, 40);
            lblProgress.TabIndex = 2;
            lblProgress.Text = "1 / 5 Flashcards";
            lblProgress.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // cardPanel
            // 
            cardPanel.BackColor = Color.White;
            cardPanel.BorderStyle = BorderStyle.None;
            cardPanel.Controls.Add(lblCardText);
            cardPanel.Cursor = Cursors.Hand;
            cardPanel.Location = new Point(400, 200);
            cardPanel.Name = "cardPanel";
            cardPanel.Size = new Size(1000, 400);
            cardPanel.TabIndex = 3;
            cardPanel.Click += cardPanel_Click;
            // Add rounded corner effect
            cardPanel.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                var rect = new Rectangle(0, 0, cardPanel.Width - 1, cardPanel.Height - 1);
                using (var pen = new Pen(Color.FromArgb(255, 140, 0), 4))
                {
                    e.Graphics.DrawRectangle(pen, rect);
                }
            };
            // 
            // lblCardText
            // 
            lblCardText.Dock = DockStyle.Fill;
            lblCardText.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblCardText.ForeColor = Color.FromArgb(255, 140, 0);
            lblCardText.Location = new Point(0, 0);
            lblCardText.Name = "lblCardText";
            lblCardText.Size = new Size(1000, 400);
            lblCardText.TabIndex = 0;
            lblCardText.Text = "SELECT là gì?";
            lblCardText.TextAlign = ContentAlignment.MiddleCenter;
            lblCardText.Click += lblCardText_Click;
            // 
            // btnPrevious
            // 
            btnPrevious.BackColor = Color.White;
            btnPrevious.Cursor = Cursors.Hand;
            btnPrevious.FlatAppearance.BorderColor = Color.FromArgb(255, 140, 0);
            btnPrevious.FlatAppearance.BorderSize = 3;
            btnPrevious.FlatStyle = FlatStyle.Flat;
            btnPrevious.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            btnPrevious.ForeColor = Color.FromArgb(255, 140, 0);
            btnPrevious.Location = new Point(280, 370);
            btnPrevious.Name = "btnPrevious";
            btnPrevious.Size = new Size(80, 80);
            btnPrevious.TabIndex = 4;
            btnPrevious.Text = "❮";
            btnPrevious.UseVisualStyleBackColor = false;
            btnPrevious.Click += btnPrevious_Click;
            // Hover effect
            btnPrevious.MouseEnter += (s, e) => btnPrevious.BackColor = Color.FromArgb(255, 245, 230);
            btnPrevious.MouseLeave += (s, e) => btnPrevious.BackColor = Color.White;
            // 
            // btnNext
            // 
            btnNext.BackColor = Color.White;
            btnNext.Cursor = Cursors.Hand;
            btnNext.FlatAppearance.BorderColor = Color.FromArgb(255, 140, 0);
            btnNext.FlatAppearance.BorderSize = 3;
            btnNext.FlatStyle = FlatStyle.Flat;
            btnNext.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            btnNext.ForeColor = Color.FromArgb(255, 140, 0);
            btnNext.Location = new Point(1440, 370);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(80, 80);
            btnNext.TabIndex = 5;
            btnNext.Text = "❯";
            btnNext.UseVisualStyleBackColor = false;
            btnNext.Click += btnNext_Click;
            // Hover effect
            btnNext.MouseEnter += (s, e) => btnNext.BackColor = Color.FromArgb(255, 245, 230);
            btnNext.MouseLeave += (s, e) => btnNext.BackColor = Color.White;
            // 
            // lblHint
            // 
            lblHint.Font = new Font("Segoe UI", 13F);
            lblHint.ForeColor = Color.FromArgb(200, 200, 200);
            lblHint.Location = new Point(550, 620);
            lblHint.Name = "lblHint";
            lblHint.Size = new Size(700, 35);
            lblHint.TabIndex = 6;
            lblHint.Text = "Nhấn vào flashcard hoặc gõ phím";
            lblHint.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // spaceKeyPanel
            // 
            spaceKeyPanel.BackColor = Color.FromArgb(100, 80, 150);
            spaceKeyPanel.BorderStyle = BorderStyle.FixedSingle;
            spaceKeyPanel.Controls.Add(lblSpaceKey);
            spaceKeyPanel.Location = new Point(825, 660);
            spaceKeyPanel.Name = "spaceKeyPanel";
            spaceKeyPanel.Size = new Size(150, 40);
            spaceKeyPanel.TabIndex = 8;
            // 
            // lblSpaceKey
            // 
            lblSpaceKey.Dock = DockStyle.Fill;
            lblSpaceKey.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblSpaceKey.ForeColor = Color.White;
            lblSpaceKey.Location = new Point(0, 0);
            lblSpaceKey.Name = "lblSpaceKey";
            lblSpaceKey.Size = new Size(148, 38);
            lblSpaceKey.TabIndex = 0;
            lblSpaceKey.Text = "Space";
            lblSpaceKey.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnFinish
            // 
            btnFinish.BackColor = Color.FromArgb(76, 175, 80);
            btnFinish.Cursor = Cursors.Hand;
            btnFinish.FlatAppearance.BorderSize = 0;
            btnFinish.FlatStyle = FlatStyle.Flat;
            btnFinish.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            btnFinish.ForeColor = Color.White;
            btnFinish.Location = new Point(725, 720);
            btnFinish.Name = "btnFinish";
            btnFinish.Size = new Size(350, 60);
            btnFinish.TabIndex = 7;
            btnFinish.Text = "✓ Hoàn thành";
            btnFinish.UseVisualStyleBackColor = false;
            btnFinish.Visible = false;
            btnFinish.Click += btnFinish_Click;
            // Hover effect
            btnFinish.MouseEnter += (s, e) => btnFinish.BackColor = Color.FromArgb(96, 195, 100);
            btnFinish.MouseLeave += (s, e) => btnFinish.BackColor = Color.FromArgb(76, 175, 80);
            // 
            // StudyControl
            // 
            BackColor = Color.FromArgb(40, 20, 100);
            Controls.Add(spaceKeyPanel);
            Controls.Add(btnFinish);
            Controls.Add(lblHint);
            Controls.Add(btnNext);
            Controls.Add(btnPrevious);
            Controls.Add(cardPanel);
            Controls.Add(lblProgress);
            Controls.Add(lblProgressPercent);
            Controls.Add(progressBar);
            Name = "StudyControl";
            Size = new Size(1800, 1000);
            KeyDown += StudyControl_KeyDown;
            // Center controls when resized
            this.Resize += (s, e) =>
            {
                CenterControls();
            };
            cardPanel.ResumeLayout(false);
            spaceKeyPanel.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        private void CenterControls()
        {
            int centerX = this.Width / 2;
            int centerY = this.Height / 2;

            // Progress bar
            progressBar.Location = new Point(centerX - progressBar.Width / 2, 80);
            lblProgressPercent.Location = new Point(centerX + progressBar.Width / 2 + 10, 75);
            lblProgress.Location = new Point(centerX - lblProgress.Width / 2, 115);

            // Card panel
            cardPanel.Location = new Point(centerX - cardPanel.Width / 2, centerY - cardPanel.Height / 2 - 50);

            // Navigation buttons
            btnPrevious.Location = new Point(centerX - cardPanel.Width / 2 - 120, centerY - btnPrevious.Height / 2 - 50);
            btnNext.Location = new Point(centerX + cardPanel.Width / 2 + 40, centerY - btnNext.Height / 2 - 50);

            // Hint and Space key
            lblHint.Location = new Point(centerX - lblHint.Width / 2, cardPanel.Bottom + 20);
            spaceKeyPanel.Location = new Point(centerX - spaceKeyPanel.Width / 2, lblHint.Bottom + 5);

            // Finish button
            btnFinish.Location = new Point(centerX - btnFinish.Width / 2, spaceKeyPanel.Bottom + 20);
        }
    }
}
