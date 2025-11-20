using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp1.View.User.Controls
{
    partial class HomeControl
    {
        private System.ComponentModel.IContainer components = null;

        // UI Components
        private PictureBox pictureBoxAvatar;
        private Panel panelWelcomeBanner;
        private Label lblWelcomeText;
        private Panel panelMotivation;
        private PictureBox pictureBoxMotivation;
        private Label lblMotivationTitle;
        private Label lblMotivationText;
        private Label lblPopular;
        private FlowLayoutPanel flowPopular;
        private Button btnViewAll;
        private Label lblFlashcardSets;
        private Label lblFlashcardDesc;
        private FlowLayoutPanel flowFlashcards;
        private Button btnViewAllFlashcards;

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
			pictureBoxAvatar = new PictureBox();
			panelWelcomeBanner = new Panel();
			lblWelcomeText = new Label();
			panelMotivation = new Panel();
			lblMotivationText = new Label();
			lblMotivationTitle = new Label();
			pictureBoxMotivation = new PictureBox();
			lblPopular = new Label();
			flowPopular = new FlowLayoutPanel();
			btnViewAll = new Button();
			lblFlashcardSets = new Label();
			lblFlashcardDesc = new Label();
			flowFlashcards = new FlowLayoutPanel();
			btnViewAllFlashcards = new Button();
			((System.ComponentModel.ISupportInitialize)pictureBoxAvatar).BeginInit();
			panelWelcomeBanner.SuspendLayout();
			panelMotivation.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)pictureBoxMotivation).BeginInit();
			SuspendLayout();
			// 
			// pictureBoxAvatar
			// 
			pictureBoxAvatar.Location = new Point(30, 25);
			pictureBoxAvatar.Name = "pictureBoxAvatar";
			pictureBoxAvatar.Size = new Size(100, 100);
			pictureBoxAvatar.SizeMode = PictureBoxSizeMode.StretchImage;
			pictureBoxAvatar.TabIndex = 0;
			pictureBoxAvatar.TabStop = false;
			// 
			// panelWelcomeBanner
			// 
			panelWelcomeBanner.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			panelWelcomeBanner.BackColor = Color.FromArgb(230, 240, 250);
			panelWelcomeBanner.Controls.Add(lblWelcomeText);
			panelWelcomeBanner.Controls.Add(pictureBoxAvatar);
			panelWelcomeBanner.Location = new Point(20, 20);
			panelWelcomeBanner.Name = "panelWelcomeBanner";
			panelWelcomeBanner.Size = new Size(1700, 140);
			panelWelcomeBanner.TabIndex = 0;
			// 
			// lblWelcomeText
			// 
			lblWelcomeText.AutoSize = true;
			lblWelcomeText.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
			lblWelcomeText.ForeColor = Color.FromArgb(218, 165, 32);
			lblWelcomeText.Location = new Point(150, 55);
			lblWelcomeText.Name = "lblWelcomeText";
			lblWelcomeText.Size = new Size(302, 45);
			lblWelcomeText.TabIndex = 2;
			lblWelcomeText.Text = "Chào mừng trở lại!";
			// 
			// panelMotivation
			// 
			panelMotivation.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			panelMotivation.BackColor = Color.FromArgb(245, 245, 245);
			panelMotivation.Controls.Add(lblMotivationText);
			panelMotivation.Controls.Add(lblMotivationTitle);
			panelMotivation.Controls.Add(pictureBoxMotivation);
			panelMotivation.Location = new Point(20, 180);
			panelMotivation.Name = "panelMotivation";
			panelMotivation.Size = new Size(1700, 380);
			panelMotivation.TabIndex = 1;
			// 
			// lblMotivationText
			// 
			lblMotivationText.Font = new Font("Segoe UI", 13F);
			lblMotivationText.ForeColor = Color.FromArgb(80, 80, 80);
			lblMotivationText.Location = new Point(40, 170);
			lblMotivationText.MaximumSize = new Size(850, 0);
			lblMotivationText.Name = "lblMotivationText";
			lblMotivationText.Size = new Size(850, 100);
			lblMotivationText.TabIndex = 2;
			lblMotivationText.Text = "Các kỹ năng cho hiện tại (và tương lai của bạn). Hãy bắt đầu học với chúng tôi.";
			// 
			// lblMotivationTitle
			// 
			lblMotivationTitle.AutoSize = true;
			lblMotivationTitle.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
			lblMotivationTitle.Location = new Point(40, 90);
			lblMotivationTitle.MaximumSize = new Size(850, 0);
			lblMotivationTitle.Name = "lblMotivationTitle";
			lblMotivationTitle.Size = new Size(722, 65);
			lblMotivationTitle.TabIndex = 1;
			lblMotivationTitle.Text = "Học những gì bạn có hứng thú";
			// 
			// pictureBoxMotivation
			// 
			pictureBoxMotivation.Location = new Point(1000, 20);
			pictureBoxMotivation.Name = "pictureBoxMotivation";
			pictureBoxMotivation.Size = new Size(680, 340);
			pictureBoxMotivation.SizeMode = PictureBoxSizeMode.Zoom;
			pictureBoxMotivation.TabIndex = 0;
			pictureBoxMotivation.TabStop = false;
			// 
			// lblPopular
			// 
			lblPopular.AutoSize = true;
			lblPopular.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
			lblPopular.Location = new Point(25, 585);
			lblPopular.Name = "lblPopular";
			lblPopular.Size = new Size(396, 48);
			lblPopular.TabIndex = 2;
			lblPopular.Text = "🔥 Khóa học phổ biến";
			// 
			// flowPopular
			// 
			flowPopular.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			flowPopular.Location = new Point(25, 640);
			flowPopular.Name = "flowPopular";
			flowPopular.Size = new Size(1695, 280);
			flowPopular.TabIndex = 3;
			// 
			// btnViewAll
			// 
			btnViewAll.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			btnViewAll.Cursor = Cursors.Hand;
			btnViewAll.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
			btnViewAll.Location = new Point(1530, 580);
			btnViewAll.Name = "btnViewAll";
			btnViewAll.Size = new Size(190, 45);
			btnViewAll.TabIndex = 4;
			btnViewAll.Text = "Xem tất cả";
			btnViewAll.UseVisualStyleBackColor = true;
			btnViewAll.Click += btnViewAll_Click;
			// 
			// lblFlashcardSets
			// 
			lblFlashcardSets.AutoSize = true;
			lblFlashcardSets.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
			lblFlashcardSets.Location = new Point(25, 945);
			lblFlashcardSets.Name = "lblFlashcardSets";
			lblFlashcardSets.Size = new Size(432, 48);
			lblFlashcardSets.TabIndex = 5;
			lblFlashcardSets.Text = "📚 Bộ flashcard nên học";
			// 
			// lblFlashcardDesc
			// 
			lblFlashcardDesc.AutoSize = true;
			lblFlashcardDesc.Font = new Font("Segoe UI", 11F);
			lblFlashcardDesc.ForeColor = Color.Gray;
			lblFlashcardDesc.Location = new Point(25, 995);
			lblFlashcardDesc.Name = "lblFlashcardDesc";
			lblFlashcardDesc.Size = new Size(438, 30);
			lblFlashcardDesc.TabIndex = 6;
			lblFlashcardDesc.Text = "Học từ vựng và kiến thức một cách hiệu quả";
			// 
			// flowFlashcards
			// 
			flowFlashcards.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			flowFlashcards.Location = new Point(25, 1035);
			flowFlashcards.Name = "flowFlashcards";
			flowFlashcards.Size = new Size(1695, 280);
			flowFlashcards.TabIndex = 7;
			// 
			// btnViewAllFlashcards
			// 
			btnViewAllFlashcards.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			btnViewAllFlashcards.Cursor = Cursors.Hand;
			btnViewAllFlashcards.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
			btnViewAllFlashcards.Location = new Point(1530, 940);
			btnViewAllFlashcards.Name = "btnViewAllFlashcards";
			btnViewAllFlashcards.Size = new Size(190, 45);
			btnViewAllFlashcards.TabIndex = 8;
			btnViewAllFlashcards.Text = "Xem tất cả";
			btnViewAllFlashcards.UseVisualStyleBackColor = true;
			btnViewAllFlashcards.Click += btnViewAllFlashcards_Click;
			// 
			// HomeControl
			// 
			AutoScroll = true;
			BackColor = Color.White;
			Controls.Add(btnViewAllFlashcards);
			Controls.Add(flowFlashcards);
			Controls.Add(lblFlashcardDesc);
			Controls.Add(lblFlashcardSets);
			Controls.Add(btnViewAll);
			Controls.Add(flowPopular);
			Controls.Add(lblPopular);
			Controls.Add(panelMotivation);
			Controls.Add(panelWelcomeBanner);
			Name = "HomeControl";
			Size = new Size(1766, 1350);
			Load += HomeControl_Load;
			((System.ComponentModel.ISupportInitialize)pictureBoxAvatar).EndInit();
			panelWelcomeBanner.ResumeLayout(false);
			panelWelcomeBanner.PerformLayout();
			panelMotivation.ResumeLayout(false);
			panelMotivation.PerformLayout();
			((System.ComponentModel.ISupportInitialize)pictureBoxMotivation).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
