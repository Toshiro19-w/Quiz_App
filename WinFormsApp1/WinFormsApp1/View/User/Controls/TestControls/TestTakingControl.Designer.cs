namespace WinFormsApp1.View.User.Controls.TestControls
{
    partial class TestTakingControl
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) 
        { 
            if (disposing && (components != null)) 
                components.Dispose(); 
            base.Dispose(disposing); 
        }

		#region Component Designer generated code
		private void InitializeComponent()
		{
			pnlTestInfo = new Panel();
			lblTestTitle = new Label();
			lblTestDescription = new Label();
			pnlStats = new Panel();
			lblTimeLimit = new Label();
			lblAttempts = new Label();
			lblHighScore = new Label();
			pnlButtons = new Panel();
			btnStartTest = new Button();
			btnReviewTest = new Button();
			btnViewResults = new Button();
			pnlTestContent = new Panel();
			flowQuestions = new FlowLayoutPanel();
			pnlTestFooter = new Panel();
			lblTimer = new Label();
			btnSubmitTest = new Button();
			pnlTestInfo.SuspendLayout();
			pnlStats.SuspendLayout();
			pnlButtons.SuspendLayout();
			pnlTestContent.SuspendLayout();
			pnlTestFooter.SuspendLayout();
			SuspendLayout();
			// 
			// pnlTestInfo
			// 
			pnlTestInfo.BackColor = Color.FromArgb(240, 248, 255);
			pnlTestInfo.BorderStyle = BorderStyle.FixedSingle;
			pnlTestInfo.Controls.Add(lblTestTitle);
			pnlTestInfo.Controls.Add(lblTestDescription);
			pnlTestInfo.Controls.Add(pnlStats);
			pnlTestInfo.Controls.Add(pnlButtons);
			pnlTestInfo.Dock = DockStyle.Top;
			pnlTestInfo.Location = new Point(0, 0);
			pnlTestInfo.Name = "pnlTestInfo";
			pnlTestInfo.Padding = new Padding(30, 30, 30, 20);
			pnlTestInfo.Size = new Size(1100, 350);
			pnlTestInfo.TabIndex = 0;
			// 
			// lblTestTitle
			// 
			lblTestTitle.AutoSize = true;
			lblTestTitle.Dock = DockStyle.Top;
			lblTestTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
			lblTestTitle.ForeColor = Color.FromArgb(0, 102, 153);
			lblTestTitle.Location = new Point(30, 268);
			lblTestTitle.Name = "lblTestTitle";
			lblTestTitle.Padding = new Padding(0, 0, 0, 15);
			lblTestTitle.Size = new Size(313, 69);
			lblTestTitle.TabIndex = 0;
			lblTestTitle.Text = "Tiêu đề bài test";
			// 
			// lblTestDescription
			// 
			lblTestDescription.AutoSize = true;
			lblTestDescription.Dock = DockStyle.Top;
			lblTestDescription.Font = new Font("Segoe UI", 11F);
			lblTestDescription.ForeColor = Color.FromArgb(50, 50, 50);
			lblTestDescription.Location = new Point(30, 218);
			lblTestDescription.MaximumSize = new Size(1000, 0);
			lblTestDescription.Name = "lblTestDescription";
			lblTestDescription.Padding = new Padding(0, 0, 0, 20);
			lblTestDescription.Size = new Size(190, 50);
			lblTestDescription.TabIndex = 1;
			lblTestDescription.Text = "Mô tả bài kiểm tra";
			// 
			// pnlStats
			// 
			pnlStats.Controls.Add(lblTimeLimit);
			pnlStats.Controls.Add(lblAttempts);
			pnlStats.Controls.Add(lblHighScore);
			pnlStats.Dock = DockStyle.Top;
			pnlStats.Location = new Point(30, 118);
			pnlStats.Name = "pnlStats";
			pnlStats.Padding = new Padding(0, 10, 0, 20);
			pnlStats.Size = new Size(1038, 100);
			pnlStats.TabIndex = 2;
			// 
			// lblTimeLimit
			// 
			lblTimeLimit.AutoSize = true;
			lblTimeLimit.Font = new Font("Segoe UI", 10F);
			lblTimeLimit.ForeColor = Color.FromArgb(0, 102, 153);
			lblTimeLimit.Location = new Point(0, 10);
			lblTimeLimit.Name = "lblTimeLimit";
			lblTimeLimit.Size = new Size(202, 28);
			lblTimeLimit.TabIndex = 0;
			lblTimeLimit.Text = "⏱️ Thời gian: 30 phút";
			// 
			// lblAttempts
			// 
			lblAttempts.AutoSize = true;
			lblAttempts.Font = new Font("Segoe UI", 10F);
			lblAttempts.ForeColor = Color.FromArgb(0, 102, 153);
			lblAttempts.Location = new Point(0, 42);
			lblAttempts.Name = "lblAttempts";
			lblAttempts.Size = new Size(174, 28);
			lblAttempts.TabIndex = 1;
			lblAttempts.Text = "🔄 Số lần làm: 0/3";
			// 
			// lblHighScore
			// 
			lblHighScore.AutoSize = true;
			lblHighScore.Font = new Font("Segoe UI", 10F);
			lblHighScore.ForeColor = Color.FromArgb(40, 167, 69);
			lblHighScore.Location = new Point(357, 42);
			lblHighScore.Name = "lblHighScore";
			lblHighScore.Size = new Size(209, 28);
			lblHighScore.TabIndex = 2;
			lblHighScore.Text = "🏆 Điểm cao nhất: 0/0";
			lblHighScore.Visible = false;
			// 
			// pnlButtons
			// 
			pnlButtons.Controls.Add(btnStartTest);
			pnlButtons.Controls.Add(btnReviewTest);
			pnlButtons.Controls.Add(btnViewResults);
			pnlButtons.Dock = DockStyle.Top;
			pnlButtons.Location = new Point(30, 30);
			pnlButtons.Name = "pnlButtons";
			pnlButtons.Size = new Size(1038, 88);
			pnlButtons.TabIndex = 3;
			// 
			// btnStartTest
			// 
			btnStartTest.BackColor = Color.FromArgb(40, 167, 69);
			btnStartTest.FlatAppearance.BorderSize = 0;
			btnStartTest.FlatStyle = FlatStyle.Flat;
			btnStartTest.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
			btnStartTest.ForeColor = Color.White;
			btnStartTest.Location = new Point(0, 10);
			btnStartTest.Name = "btnStartTest";
			btnStartTest.Size = new Size(200, 50);
			btnStartTest.TabIndex = 0;
			btnStartTest.Text = "▶️ Bắt đầu làm bài";
			btnStartTest.UseVisualStyleBackColor = false;
			// 
			// btnReviewTest
			// 
			btnReviewTest.BackColor = Color.FromArgb(52, 144, 220);
			btnReviewTest.FlatAppearance.BorderSize = 0;
			btnReviewTest.FlatStyle = FlatStyle.Flat;
			btnReviewTest.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
			btnReviewTest.ForeColor = Color.White;
			btnReviewTest.Location = new Point(220, 10);
			btnReviewTest.Name = "btnReviewTest";
			btnReviewTest.Size = new Size(220, 50);
			btnReviewTest.TabIndex = 1;
			btnReviewTest.Text = "📋 Xem lại bài làm";
			btnReviewTest.UseVisualStyleBackColor = false;
			btnReviewTest.Visible = false;
			// 
			// btnViewResults
			// 
			btnViewResults.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			btnViewResults.BackColor = Color.FromArgb(255, 193, 7);
			btnViewResults.FlatAppearance.BorderSize = 0;
			btnViewResults.FlatStyle = FlatStyle.Flat;
			btnViewResults.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
			btnViewResults.ForeColor = Color.White;
			btnViewResults.Location = new Point(765, 10);
			btnViewResults.Name = "btnViewResults";
			btnViewResults.Size = new Size(260, 50);
			btnViewResults.TabIndex = 2;
			btnViewResults.Text = "📊 Xem danh sách kết quả";
			btnViewResults.UseVisualStyleBackColor = false;
			btnViewResults.Visible = false;
			// 
			// pnlTestContent
			// 
			pnlTestContent.AutoScroll = true;
			pnlTestContent.BackColor = Color.White;
			pnlTestContent.Controls.Add(flowQuestions);
			pnlTestContent.Dock = DockStyle.Fill;
			pnlTestContent.Location = new Point(0, 350);
			pnlTestContent.Name = "pnlTestContent";
			pnlTestContent.Padding = new Padding(30);
			pnlTestContent.Size = new Size(1100, 460);
			pnlTestContent.TabIndex = 1;
			pnlTestContent.Visible = false;
			// 
			// flowQuestions
			// 
			flowQuestions.AutoSize = true;
			flowQuestions.Dock = DockStyle.Top;
			flowQuestions.FlowDirection = FlowDirection.TopDown;
			flowQuestions.Location = new Point(30, 30);
			flowQuestions.Name = "flowQuestions";
			flowQuestions.Size = new Size(1040, 0);
			flowQuestions.TabIndex = 0;
			flowQuestions.WrapContents = false;
			// 
			// pnlTestFooter
			// 
			pnlTestFooter.BackColor = Color.FromArgb(248, 249, 250);
			pnlTestFooter.BorderStyle = BorderStyle.FixedSingle;
			pnlTestFooter.Controls.Add(lblTimer);
			pnlTestFooter.Controls.Add(btnSubmitTest);
			pnlTestFooter.Dock = DockStyle.Bottom;
			pnlTestFooter.Location = new Point(0, 730);
			pnlTestFooter.Name = "pnlTestFooter";
			pnlTestFooter.Padding = new Padding(30, 15, 30, 15);
			pnlTestFooter.Size = new Size(1100, 80);
			pnlTestFooter.TabIndex = 2;
			pnlTestFooter.Visible = false;
			// 
			// lblTimer
			// 
			lblTimer.AutoSize = true;
			lblTimer.Dock = DockStyle.Left;
			lblTimer.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
			lblTimer.ForeColor = Color.FromArgb(220, 53, 69);
			lblTimer.Location = new Point(30, 15);
			lblTimer.Name = "lblTimer";
			lblTimer.Padding = new Padding(0, 10, 0, 10);
			lblTimer.Size = new Size(370, 58);
			lblTimer.TabIndex = 0;
			lblTimer.Text = "⏰ Thời gian còn lại: 30:00";
			lblTimer.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// btnSubmitTest
			// 
			btnSubmitTest.BackColor = Color.FromArgb(40, 167, 69);
			btnSubmitTest.Dock = DockStyle.Right;
			btnSubmitTest.FlatAppearance.BorderSize = 0;
			btnSubmitTest.FlatStyle = FlatStyle.Flat;
			btnSubmitTest.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
			btnSubmitTest.ForeColor = Color.White;
			btnSubmitTest.Location = new Point(888, 15);
			btnSubmitTest.Name = "btnSubmitTest";
			btnSubmitTest.Size = new Size(180, 48);
			btnSubmitTest.TabIndex = 1;
			btnSubmitTest.Text = "✓ Nộp bài";
			btnSubmitTest.UseVisualStyleBackColor = false;
			// 
			// TestTakingControl
			// 
			AutoScaleMode = AutoScaleMode.None;
			BackColor = Color.White;
			Controls.Add(pnlTestFooter);
			Controls.Add(pnlTestContent);
			Controls.Add(pnlTestInfo);
			Name = "TestTakingControl";
			Size = new Size(1100, 810);
			pnlTestInfo.ResumeLayout(false);
			pnlTestInfo.PerformLayout();
			pnlStats.ResumeLayout(false);
			pnlStats.PerformLayout();
			pnlButtons.ResumeLayout(false);
			pnlTestContent.ResumeLayout(false);
			pnlTestContent.PerformLayout();
			pnlTestFooter.ResumeLayout(false);
			pnlTestFooter.PerformLayout();
			ResumeLayout(false);
		}
		#endregion

		private Panel pnlTestInfo;
        private Label lblTestTitle;
        private Label lblTestDescription;
        private Panel pnlStats;
        private Label lblTimeLimit;
        private Label lblAttempts;
        private Label lblHighScore;
        private Panel pnlButtons;
        private Button btnStartTest;
        private Button btnReviewTest;
        private Button btnViewResults;
        private Panel pnlTestContent;
        private FlowLayoutPanel flowQuestions;
        private Panel pnlTestFooter;
        private Label lblTimer;
        private Button btnSubmitTest;
    }
}
