using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp1.View.User.Controls
{
    partial class CourseDetailControl
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblBreadcrumb;
        private Label lblTitle;
        private Panel pnlBestseller;
        private Label lblBestseller;
        private Label lblRating;
        private Label lblRatingCount;
        private Label lblStudents;
        private Label lblInstructor;
        private Label lblLastUpdated;
        private PictureBox picCover;
        private Label lblPrice;
        private Button btnAddToCart;
        private Button btnBuyNow;
        private Button btnStartLearning;
        private Label lblContentTitle;
        private LinkLabel lnkExpandAll;
        private Label lblChapterStats;
        private Panel pnlChapters;
        private Label lblDescriptionTitle;
        private RichTextBox rtbDescription;
        private Label lblInstructorTitle;
        private Label lblInstructorName;
        private Label lblInstructorEmail;
        private Label lblRatingSectionTitle;
        private Label lblAvgRating;
        private Label lblTotalRatingCount;
        private ProgressBar[] ratingProgressBars;
        private Label[] ratingPercentLabels;
        private Label lblStar1;
        private Label lblStar2;
        private Label lblStar3;
        private Label lblStar4;
        private Label lblStar5;
        private FlowLayoutPanel flowReviews;

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
			lblBreadcrumb = new Label();
			lblTitle = new Label();
			pnlBestseller = new Panel();
			lblBestseller = new Label();
			lblRating = new Label();
			lblRatingCount = new Label();
			lblStudents = new Label();
			lblInstructor = new Label();
			lblLastUpdated = new Label();
			picCover = new PictureBox();
			lblPrice = new Label();
			btnAddToCart = new Button();
			btnBuyNow = new Button();
			btnStartLearning = new Button();
			lblContentTitle = new Label();
			lnkExpandAll = new LinkLabel();
			lblChapterStats = new Label();
			pnlChapters = new Panel();
			lblDescriptionTitle = new Label();
			rtbDescription = new RichTextBox();
			lblInstructorTitle = new Label();
			lblInstructorName = new Label();
			lblInstructorEmail = new Label();
			lblRatingSectionTitle = new Label();
			lblAvgRating = new Label();
			lblTotalRatingCount = new Label();
			lblStar1 = new Label();
			lblStar2 = new Label();
			lblStar3 = new Label();
			lblStar4 = new Label();
			lblStar5 = new Label();
			flowReviews = new FlowLayoutPanel();
			pnlBestseller.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)picCover).BeginInit();
			SuspendLayout();
			// 
			// lblBreadcrumb
			// 
			lblBreadcrumb.AutoSize = true;
			lblBreadcrumb.Font = new Font("Segoe UI", 9F);
			lblBreadcrumb.ForeColor = Color.Gray;
			lblBreadcrumb.Location = new Point(20, 20);
			lblBreadcrumb.Name = "lblBreadcrumb";
			lblBreadcrumb.Size = new Size(469, 25);
			lblBreadcrumb.TabIndex = 0;
			lblBreadcrumb.Text = "Khóa học / Lập trình / SQL Server từ cơ bản đến nâng cao";
			// 
			// lblTitle
			// 
			lblTitle.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
			lblTitle.ForeColor = Color.FromArgb(0, 102, 102);
			lblTitle.Location = new Point(20, 54);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new Size(700, 72);
			lblTitle.TabIndex = 1;
			// 
			// pnlBestseller
			// 
			pnlBestseller.BackColor = Color.FromArgb(255, 193, 7);
			pnlBestseller.Controls.Add(lblBestseller);
			pnlBestseller.Location = new Point(20, 135);
			pnlBestseller.Name = "pnlBestseller";
			pnlBestseller.Size = new Size(90, 28);
			pnlBestseller.TabIndex = 2;
			// 
			// lblBestseller
			// 
			lblBestseller.Dock = DockStyle.Fill;
			lblBestseller.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
			lblBestseller.ForeColor = Color.FromArgb(102, 51, 0);
			lblBestseller.Location = new Point(0, 0);
			lblBestseller.Name = "lblBestseller";
			lblBestseller.Size = new Size(90, 28);
			lblBestseller.TabIndex = 0;
			lblBestseller.Text = "Bestseller";
			lblBestseller.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// lblRating
			// 
			lblRating.AutoSize = true;
			lblRating.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			lblRating.Location = new Point(120, 140);
			lblRating.Name = "lblRating";
			lblRating.Size = new Size(0, 28);
			lblRating.TabIndex = 3;
			// 
			// lblRatingCount
			// 
			lblRatingCount.AutoSize = true;
			lblRatingCount.Font = new Font("Segoe UI", 9F);
			lblRatingCount.ForeColor = Color.Gray;
			lblRatingCount.Location = new Point(230, 140);
			lblRatingCount.Name = "lblRatingCount";
			lblRatingCount.Size = new Size(0, 25);
			lblRatingCount.TabIndex = 4;
			// 
			// lblStudents
			// 
			lblStudents.AutoSize = true;
			lblStudents.Font = new Font("Segoe UI", 9F);
			lblStudents.Location = new Point(380, 140);
			lblStudents.Name = "lblStudents";
			lblStudents.Size = new Size(0, 25);
			lblStudents.TabIndex = 5;
			// 
			// lblInstructor
			// 
			lblInstructor.AutoSize = true;
			lblInstructor.Font = new Font("Segoe UI", 9F);
			lblInstructor.Location = new Point(20, 135);
			lblInstructor.Name = "lblInstructor";
			lblInstructor.Size = new Size(0, 25);
			lblInstructor.TabIndex = 6;
			// 
			// lblLastUpdated
			// 
			lblLastUpdated.AutoSize = true;
			lblLastUpdated.Font = new Font("Segoe UI", 9F);
			lblLastUpdated.ForeColor = Color.Gray;
			lblLastUpdated.Location = new Point(20, 160);
			lblLastUpdated.Name = "lblLastUpdated";
			lblLastUpdated.Size = new Size(0, 25);
			lblLastUpdated.TabIndex = 7;
			// 
			// picCover
			// 
			picCover.BackColor = Color.FromArgb(248, 249, 250);
			picCover.BorderStyle = BorderStyle.FixedSingle;
			picCover.Location = new Point(750, 80);
			picCover.Name = "picCover";
			picCover.Size = new Size(320, 180);
			picCover.SizeMode = PictureBoxSizeMode.Zoom;
			picCover.TabIndex = 8;
			picCover.TabStop = false;
			// 
			// lblPrice
			// 
			lblPrice.AutoSize = true;
			lblPrice.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
			lblPrice.ForeColor = Color.FromArgb(0, 102, 102);
			lblPrice.Location = new Point(750, 270);
			lblPrice.Name = "lblPrice";
			lblPrice.Size = new Size(0, 74);
			lblPrice.TabIndex = 9;
			// 
			// btnAddToCart
			// 
			btnAddToCart.BackColor = Color.FromArgb(0, 102, 102);
			btnAddToCart.Cursor = Cursors.Hand;
			btnAddToCart.FlatStyle = FlatStyle.Flat;
			btnAddToCart.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
			btnAddToCart.ForeColor = Color.White;
			btnAddToCart.Location = new Point(750, 330);
			btnAddToCart.Name = "btnAddToCart";
			btnAddToCart.Size = new Size(320, 50);
			btnAddToCart.TabIndex = 10;
			btnAddToCart.Text = "Thêm vào giỏ hàng";
			btnAddToCart.UseVisualStyleBackColor = false;
			// 
			// btnBuyNow
			// 
			btnBuyNow.Cursor = Cursors.Hand;
			btnBuyNow.FlatStyle = FlatStyle.Flat;
			btnBuyNow.Font = new Font("Segoe UI", 10F);
			btnBuyNow.Location = new Point(750, 390);
			btnBuyNow.Name = "btnBuyNow";
			btnBuyNow.Size = new Size(320, 40);
			btnBuyNow.TabIndex = 11;
			btnBuyNow.Text = "♥ Mua khóa học";
			// 
			// btnStartLearning
			// 
			btnStartLearning.BackColor = Color.FromArgb(0, 153, 77);
			btnStartLearning.Cursor = Cursors.Hand;
			btnStartLearning.FlatStyle = FlatStyle.Flat;
			btnStartLearning.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
			btnStartLearning.ForeColor = Color.White;
			btnStartLearning.Location = new Point(750, 330);
			btnStartLearning.Name = "btnStartLearning";
			btnStartLearning.Size = new Size(320, 50);
			btnStartLearning.TabIndex = 12;
			btnStartLearning.Text = "Bắt đầu học";
			btnStartLearning.UseVisualStyleBackColor = false;
			btnStartLearning.Visible = false;
			// 
			// lblContentTitle
			// 
			lblContentTitle.AutoSize = true;
			lblContentTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
			lblContentTitle.Location = new Point(20, 400);
			lblContentTitle.Name = "lblContentTitle";
			lblContentTitle.Size = new Size(307, 45);
			lblContentTitle.TabIndex = 13;
			lblContentTitle.Text = "Nội dung khóa học";
			// 
			// lnkExpandAll
			// 
			lnkExpandAll.AutoSize = true;
			lnkExpandAll.Font = new Font("Segoe UI", 9F);
			lnkExpandAll.LinkColor = Color.FromArgb(0, 153, 153);
			lnkExpandAll.Location = new Point(950, 405);
			lnkExpandAll.Name = "lnkExpandAll";
			lnkExpandAll.Size = new Size(130, 25);
			lnkExpandAll.TabIndex = 14;
			lnkExpandAll.TabStop = true;
			lnkExpandAll.Text = "Mở rộng tất cả";
			// 
			// lblChapterStats
			// 
			lblChapterStats.AutoSize = true;
			lblChapterStats.Font = new Font("Segoe UI", 9F);
			lblChapterStats.ForeColor = Color.Gray;
			lblChapterStats.Location = new Point(20, 435);
			lblChapterStats.Name = "lblChapterStats";
			lblChapterStats.Size = new Size(0, 25);
			lblChapterStats.TabIndex = 15;
			// 
			// pnlChapters
			// 
			pnlChapters.AutoSize = true;
			pnlChapters.Location = new Point(20, 465);
			pnlChapters.Name = "pnlChapters";
			pnlChapters.Size = new Size(700, 200);
			pnlChapters.TabIndex = 16;
			// 
			// lblDescriptionTitle
			// 
			lblDescriptionTitle.AutoSize = true;
			lblDescriptionTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
			lblDescriptionTitle.Location = new Point(20, 700);
			lblDescriptionTitle.Name = "lblDescriptionTitle";
			lblDescriptionTitle.Size = new Size(109, 45);
			lblDescriptionTitle.TabIndex = 17;
			lblDescriptionTitle.Text = "Mô tả";
			// 
			// rtbDescription
			// 
			rtbDescription.BackColor = Color.FromArgb(248, 249, 250);
			rtbDescription.BorderStyle = BorderStyle.None;
			rtbDescription.Font = new Font("Segoe UI", 10F);
			rtbDescription.Location = new Point(20, 735);
			rtbDescription.Name = "rtbDescription";
			rtbDescription.ReadOnly = true;
			rtbDescription.Size = new Size(700, 100);
			rtbDescription.TabIndex = 18;
			rtbDescription.Text = "";
			// 
			// lblInstructorTitle
			// 
			lblInstructorTitle.AutoSize = true;
			lblInstructorTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
			lblInstructorTitle.Location = new Point(20, 860);
			lblInstructorTitle.Name = "lblInstructorTitle";
			lblInstructorTitle.Size = new Size(179, 45);
			lblInstructorTitle.TabIndex = 19;
			lblInstructorTitle.Text = "Giảng viên";
			// 
			// lblInstructorName
			// 
			lblInstructorName.AutoSize = true;
			lblInstructorName.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
			lblInstructorName.Location = new Point(80, 895);
			lblInstructorName.Name = "lblInstructorName";
			lblInstructorName.Size = new Size(0, 30);
			lblInstructorName.TabIndex = 20;
			// 
			// lblInstructorEmail
			// 
			lblInstructorEmail.AutoSize = true;
			lblInstructorEmail.Font = new Font("Segoe UI", 9F);
			lblInstructorEmail.ForeColor = Color.Gray;
			lblInstructorEmail.Location = new Point(80, 920);
			lblInstructorEmail.Name = "lblInstructorEmail";
			lblInstructorEmail.Size = new Size(0, 25);
			lblInstructorEmail.TabIndex = 21;
			// 
			// lblRatingSectionTitle
			// 
			lblRatingSectionTitle.AutoSize = true;
			lblRatingSectionTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
			lblRatingSectionTitle.Location = new Point(20, 970);
			lblRatingSectionTitle.Name = "lblRatingSectionTitle";
			lblRatingSectionTitle.Size = new Size(348, 45);
			lblRatingSectionTitle.TabIndex = 22;
			lblRatingSectionTitle.Text = "Đánh giá của học viên";
			// 
			// lblAvgRating
			// 
			lblAvgRating.AutoSize = true;
			lblAvgRating.Font = new Font("Segoe UI", 36F, FontStyle.Bold);
			lblAvgRating.ForeColor = Color.FromArgb(0, 102, 102);
			lblAvgRating.Location = new Point(20, 1005);
			lblAvgRating.Name = "lblAvgRating";
			lblAvgRating.Size = new Size(0, 96);
			lblAvgRating.TabIndex = 23;
			// 
			// lblTotalRatingCount
			// 
			lblTotalRatingCount.AutoSize = true;
			lblTotalRatingCount.Font = new Font("Segoe UI", 9F);
			lblTotalRatingCount.ForeColor = Color.Gray;
			lblTotalRatingCount.Location = new Point(20, 1085);
			lblTotalRatingCount.Name = "lblTotalRatingCount";
			lblTotalRatingCount.Size = new Size(0, 25);
			lblTotalRatingCount.TabIndex = 24;
			// 
			// lblStar1
			// 
			lblStar1.AutoSize = true;
			lblStar1.Font = new Font("Segoe UI", 9F);
			lblStar1.Location = new Point(80, 1015);
			lblStar1.Name = "lblStar1";
			lblStar1.Size = new Size(87, 25);
			lblStar1.TabIndex = 25;
			lblStar1.Text = "★★★★★";
			// 
			// lblStar2
			// 
			lblStar2.AutoSize = true;
			lblStar2.Font = new Font("Segoe UI", 9F);
			lblStar2.Location = new Point(80, 1043);
			lblStar2.Name = "lblStar2";
			lblStar2.Size = new Size(87, 25);
			lblStar2.TabIndex = 28;
			lblStar2.Text = "★★★★☆";
			// 
			// lblStar3
			// 
			lblStar3.AutoSize = true;
			lblStar3.Font = new Font("Segoe UI", 9F);
			lblStar3.Location = new Point(80, 1071);
			lblStar3.Name = "lblStar3";
			lblStar3.Size = new Size(87, 25);
			lblStar3.TabIndex = 31;
			lblStar3.Text = "★★★☆☆";
			// 
			// lblStar4
			// 
			lblStar4.AutoSize = true;
			lblStar4.Font = new Font("Segoe UI", 9F);
			lblStar4.Location = new Point(80, 1099);
			lblStar4.Name = "lblStar4";
			lblStar4.Size = new Size(87, 25);
			lblStar4.TabIndex = 34;
			lblStar4.Text = "★★☆☆☆";
			// 
			// lblStar5
			// 
			lblStar5.AutoSize = true;
			lblStar5.Font = new Font("Segoe UI", 9F);
			lblStar5.Location = new Point(80, 1127);
			lblStar5.Name = "lblStar5";
			lblStar5.Size = new Size(87, 25);
			lblStar5.TabIndex = 37;
			lblStar5.Text = "★☆☆☆☆";
			// 
			// flowReviews
			// 
			flowReviews.AutoSize = true;
			flowReviews.FlowDirection = FlowDirection.TopDown;
			flowReviews.Location = new Point(20, 1180);
			flowReviews.Name = "flowReviews";
			flowReviews.Size = new Size(700, 300);
			flowReviews.TabIndex = 40;
			flowReviews.WrapContents = false;
			// 
			// CourseDetailControl
			// 
			AutoScroll = true;
			BackColor = Color.White;
			Controls.Add(lblBreadcrumb);
			Controls.Add(lblTitle);
			Controls.Add(pnlBestseller);
			Controls.Add(lblRating);
			Controls.Add(lblRatingCount);
			Controls.Add(lblStudents);
			Controls.Add(lblInstructor);
			Controls.Add(lblLastUpdated);
			Controls.Add(picCover);
			Controls.Add(lblPrice);
			Controls.Add(btnAddToCart);
			Controls.Add(btnBuyNow);
			Controls.Add(btnStartLearning);
			Controls.Add(lblContentTitle);
			Controls.Add(lnkExpandAll);
			Controls.Add(lblChapterStats);
			Controls.Add(pnlChapters);
			Controls.Add(lblDescriptionTitle);
			Controls.Add(rtbDescription);
			Controls.Add(lblInstructorTitle);
			Controls.Add(lblInstructorName);
			Controls.Add(lblInstructorEmail);
			Controls.Add(lblRatingSectionTitle);
			Controls.Add(lblAvgRating);
			Controls.Add(lblTotalRatingCount);
			Controls.Add(lblStar1);
			Controls.Add(lblStar2);
			Controls.Add(lblStar3);
			Controls.Add(lblStar4);
			Controls.Add(lblStar5);
			Controls.Add(flowReviews);
			Name = "CourseDetailControl";
			Size = new Size(1074, 774);
			pnlBestseller.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)picCover).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
