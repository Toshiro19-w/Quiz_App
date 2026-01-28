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
        private Label lblRecommended;
        private Label lblRecommendedDesc;
        private CourseCarouselControl carouselRecommended;
        private Button btnViewAllRecommended;
        private Label lblPopular;
        private CourseCarouselControl carouselPopular;
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
            lblRecommended = new Label();
            lblRecommendedDesc = new Label();
            carouselRecommended = new CourseCarouselControl();
            btnViewAllRecommended = new Button();
            lblPopular = new Label();
            carouselPopular = new CourseCarouselControl();
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
            pictureBoxAvatar.SizeMode = PictureBoxSizeMode.Zoom;
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
            lblWelcomeText.Size = new Size(258, 37);
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
            lblMotivationTitle.Size = new Size(601, 54);
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
            // lblRecommended
            // 
            lblRecommended.AutoSize = true;
            lblRecommended.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblRecommended.Location = new Point(25, 585);
            lblRecommended.Name = "lblRecommended";
            lblRecommended.Size = new Size(339, 41);
            lblRecommended.TabIndex = 2;
            lblRecommended.Text = "🎯 Gợi ý dành cho bạn";
            // 
            // lblRecommendedDesc
            // 
            lblRecommendedDesc.AutoSize = true;
            lblRecommendedDesc.Font = new Font("Segoe UI", 11F);
            lblRecommendedDesc.ForeColor = Color.Gray;
            lblRecommendedDesc.Location = new Point(25, 635);
            lblRecommendedDesc.Name = "lblRecommendedDesc";
            lblRecommendedDesc.Size = new Size(453, 25);
            lblRecommendedDesc.TabIndex = 3;
            lblRecommendedDesc.Text = "Khóa học được chọn riêng dựa trên sở thích của bạn";
            // 
            // carouselRecommended
            // 
            carouselRecommended.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            carouselRecommended.BackColor = Color.Transparent;
            carouselRecommended.Location = new Point(3, 675);
            carouselRecommended.Name = "carouselRecommended";
            carouselRecommended.Size = new Size(1760, 420);
            carouselRecommended.TabIndex = 4;
            // 
            // btnViewAllRecommended
            // 
            btnViewAllRecommended.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnViewAllRecommended.Cursor = Cursors.Hand;
            btnViewAllRecommended.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            btnViewAllRecommended.Location = new Point(1530, 580);
            btnViewAllRecommended.Name = "btnViewAllRecommended";
            btnViewAllRecommended.Size = new Size(190, 45);
            btnViewAllRecommended.TabIndex = 5;
            btnViewAllRecommended.Text = "Xem tất cả";
            btnViewAllRecommended.UseVisualStyleBackColor = true;
            btnViewAllRecommended.Click += btnViewAll_Click;
            // 
            // lblPopular
            // 
            lblPopular.AutoSize = true;
            lblPopular.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblPopular.Location = new Point(25, 1120);
            lblPopular.Name = "lblPopular";
            lblPopular.Size = new Size(330, 41);
            lblPopular.TabIndex = 6;
            lblPopular.Text = "🔥 Khóa học phổ biến";
            // 
            // carouselPopular
            // 
            carouselPopular.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            carouselPopular.BackColor = Color.Transparent;
            carouselPopular.Location = new Point(3, 1175);
            carouselPopular.Name = "carouselPopular";
            carouselPopular.Size = new Size(1760, 420);
            carouselPopular.TabIndex = 7;
            // 
            // btnViewAll
            // 
            btnViewAll.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnViewAll.Cursor = Cursors.Hand;
            btnViewAll.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            btnViewAll.Location = new Point(1530, 1115);
            btnViewAll.Name = "btnViewAll";
            btnViewAll.Size = new Size(190, 45);
            btnViewAll.TabIndex = 8;
            btnViewAll.Text = "Xem tất cả";
            btnViewAll.UseVisualStyleBackColor = true;
            btnViewAll.Click += btnViewAll_Click;
            // 
            // lblFlashcardSets
            // 
            lblFlashcardSets.AutoSize = true;
            lblFlashcardSets.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblFlashcardSets.Location = new Point(25, 1627);
            lblFlashcardSets.Name = "lblFlashcardSets";
            lblFlashcardSets.Size = new Size(360, 41);
            lblFlashcardSets.TabIndex = 9;
            lblFlashcardSets.Text = "📚 Bộ flashcard nên học";
            // 
            // lblFlashcardDesc
            // 
            lblFlashcardDesc.AutoSize = true;
            lblFlashcardDesc.Font = new Font("Segoe UI", 11F);
            lblFlashcardDesc.ForeColor = Color.Gray;
            lblFlashcardDesc.Location = new Point(25, 1677);
            lblFlashcardDesc.Name = "lblFlashcardDesc";
            lblFlashcardDesc.Size = new Size(381, 25);
            lblFlashcardDesc.TabIndex = 10;
            lblFlashcardDesc.Text = "Học từ vựng và kiến thức một cách hiệu quả";
            // 
            // flowFlashcards
            // 
            flowFlashcards.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            flowFlashcards.Location = new Point(25, 1717);
            flowFlashcards.Name = "flowFlashcards";
            flowFlashcards.Size = new Size(1695, 280);
            flowFlashcards.TabIndex = 11;
            // 
            // btnViewAllFlashcards
            // 
            btnViewAllFlashcards.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnViewAllFlashcards.Cursor = Cursors.Hand;
            btnViewAllFlashcards.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            btnViewAllFlashcards.Location = new Point(1530, 1622);
            btnViewAllFlashcards.Name = "btnViewAllFlashcards";
            btnViewAllFlashcards.Size = new Size(190, 45);
            btnViewAllFlashcards.TabIndex = 12;
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
            Controls.Add(carouselPopular);
            Controls.Add(lblPopular);
            Controls.Add(btnViewAllRecommended);
            Controls.Add(carouselRecommended);
            Controls.Add(lblRecommendedDesc);
            Controls.Add(lblRecommended);
            Controls.Add(panelMotivation);
            Controls.Add(panelWelcomeBanner);
            Name = "HomeControl";
            Size = new Size(1766, 2050);
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
