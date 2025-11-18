using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp1.View.User.Controls.CourseControls
{
    partial class CourseDetailControl
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblBreadcrumb;
        private Label lblTitle;
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
        private Button btnEditCourse;
        private Button btnViewCourse;
        private Button btnStatistics;
        private FlowLayoutPanel pnlActions;
        private FlowLayoutPanel pnlOwnerActions;
        private Label lblContentTitle;
        private LinkLabel lnkExpandAll;
        private Label lblChapterStats;
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
			btnEditCourse = new Button();
			btnViewCourse = new Button();
			btnStatistics = new Button();
			pnlActions = new FlowLayoutPanel();
			pnlOwnerActions = new FlowLayoutPanel();
			lblContentTitle = new Label();
			lnkExpandAll = new LinkLabel();
			lblChapterStats = new Label();
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
			panel1 = new Panel();
			// CHANGE: use FlowLayoutPanel for chapters so multiple chapter panels layout vertically without overlap
			pnlChapters = new FlowLayoutPanel();
			((System.ComponentModel.ISupportInitialize)picCover).BeginInit();
			pnlActions.SuspendLayout();
			pnlOwnerActions.SuspendLayout();
			panel1.SuspendLayout();
			SuspendLayout();
			// 
			// lblBreadcrumb
			// 
			lblBreadcrumb.AutoSize = true;
			lblBreadcrumb.Font = new Font("Segoe UI", 9F);
			lblBreadcrumb.ForeColor = Color.Gray;
			lblBreadcrumb.Location = new Point(77, 16);
			lblBreadcrumb.Name = "lblBreadcrumb";
			lblBreadcrumb.Size = new Size(469, 25);
			lblBreadcrumb.TabIndex = 0;
			lblBreadcrumb.Text = "Khóa học / Lập trình / SQL Server từ cơ bản đến nâng cao";
			// 
			// lblTitle
			// 
			lblTitle.BackColor = Color.Teal;
			lblTitle.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
			lblTitle.ForeColor = Color.White;
			lblTitle.Location = new Point(53, 10);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new Size(700, 65);
			lblTitle.TabIndex = 1;
			lblTitle.Text = "tieu de";
			// 
			// lblRating
			// 
			lblRating.AutoSize = true;
			lblRating.BackColor = Color.Teal;
			lblRating.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
			lblRating.ForeColor = Color.Gold;
			lblRating.Location = new Point(53, 120);
			lblRating.Name = "lblRating";
			lblRating.Size = new Size(83, 32);
			lblRating.TabIndex = 3;
			lblRating.Text = "rating";
			// 
			// lblRatingCount
			// 
			lblRatingCount.AutoSize = true;
			lblRatingCount.BackColor = Color.Teal;
			lblRatingCount.Font = new Font("Segoe UI", 11F);
			lblRatingCount.ForeColor = Color.White;
			lblRatingCount.Location = new Point(202, 122);
			lblRatingCount.Name = "lblRatingCount";
			lblRatingCount.Size = new Size(69, 30);
			lblRatingCount.TabIndex = 4;
			lblRatingCount.Text = "rating";
			// 
			// lblStudents
			// 
			lblStudents.AutoSize = true;
			lblStudents.BackColor = Color.Teal;
			lblStudents.Font = new Font("Segoe UI", 11F);
			lblStudents.ForeColor = Color.White;
			lblStudents.Location = new Point(349, 121);
			lblStudents.Name = "lblStudents";
			lblStudents.Size = new Size(120, 30);
			lblStudents.TabIndex = 5;
			lblStudents.Tag = "";
			lblStudents.Text = "so hoc sinh";
			// 
			// lblInstructor
			// 
			lblInstructor.AutoSize = true;
			lblInstructor.BackColor = Color.Teal;
			lblInstructor.Font = new Font("Segoe UI", 10F);
			lblInstructor.ForeColor = Color.White;
			lblInstructor.Location = new Point(53, 193);
			lblInstructor.Name = "lblInstructor";
			lblInstructor.Size = new Size(250, 28);
			lblInstructor.TabIndex = 6;
			lblInstructor.Text = "Giảng viên: Trần Minh Khoa";
			// 
			// lblLastUpdated
			// 
			lblLastUpdated.AutoSize = true;
			lblLastUpdated.BackColor = Color.Teal;
			lblLastUpdated.Font = new Font("Segoe UI", 10F);
			lblLastUpdated.ForeColor = Color.White;
			lblLastUpdated.Location = new Point(53, 227);
			lblLastUpdated.Name = "lblLastUpdated";
			lblLastUpdated.Size = new Size(173, 28);
			lblLastUpdated.TabIndex = 7;
			lblLastUpdated.Text = "Cập nhật: 11/2025";
			// 
			// picCover
			// 
			picCover.BackColor = Color.FromArgb(248, 249, 250);
			picCover.BorderStyle = BorderStyle.FixedSingle;
			picCover.Location = new Point(1352, 72);
			picCover.Name = "picCover";
			picCover.Size = new Size(320, 180);
			picCover.SizeMode = PictureBoxSizeMode.Zoom;
			picCover.TabIndex = 8;
			picCover.TabStop = false;
			// 
			// lblPrice
			// 
			lblPrice.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
			lblPrice.ForeColor = Color.FromArgb(0, 102, 102);
			lblPrice.Location = new Point(3, 0);
			lblPrice.Name = "lblPrice";
			lblPrice.Size = new Size(320, 74);
			lblPrice.TabIndex = 9;
			lblPrice.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// btnAddToCart
			// 
			btnAddToCart.BackColor = Color.FromArgb(0, 102, 102);
			btnAddToCart.Cursor = Cursors.Hand;
			btnAddToCart.FlatStyle = FlatStyle.Flat;
			btnAddToCart.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
			btnAddToCart.ForeColor = Color.White;
			btnAddToCart.Location = new Point(3, 77);
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
			btnBuyNow.Location = new Point(3, 133);
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
			btnStartLearning.Location = new Point(3, 179);
			btnStartLearning.Name = "btnStartLearning";
			btnStartLearning.Size = new Size(320, 50);
			btnStartLearning.TabIndex = 12;
			btnStartLearning.Text = "Bắt đầu học";
			btnStartLearning.UseVisualStyleBackColor = false;
			btnStartLearning.Visible = false;
			// 
			// btnEditCourse
			// 
			btnEditCourse.BackColor = Color.FromArgb(0, 102, 102);
			btnEditCourse.Cursor = Cursors.Hand;
			btnEditCourse.FlatAppearance.BorderSize = 0;
			btnEditCourse.FlatStyle = FlatStyle.Flat;
			btnEditCourse.Font = new Font("Segoe UI", 10F);
			btnEditCourse.ForeColor = Color.White;
			btnEditCourse.Location = new Point(0, 5);
			btnEditCourse.Margin = new Padding(0, 5, 0, 0);
			btnEditCourse.Name = "btnEditCourse";
			btnEditCourse.Size = new Size(320, 40);
			btnEditCourse.TabIndex = 41;
			btnEditCourse.Text = "Chỉnh sửa khóa học";
			btnEditCourse.UseVisualStyleBackColor = false;
			btnEditCourse.Visible = false;
			// 
			// btnViewCourse
			// 
			btnViewCourse.BackColor = Color.FromArgb(0, 153, 153);
			btnViewCourse.Cursor = Cursors.Hand;
			btnViewCourse.FlatAppearance.BorderSize = 0;
			btnViewCourse.FlatStyle = FlatStyle.Flat;
			btnViewCourse.Font = new Font("Segoe UI", 10F);
			btnViewCourse.ForeColor = Color.White;
			btnViewCourse.Location = new Point(0, 50);
			btnViewCourse.Margin = new Padding(0, 5, 0, 0);
			btnViewCourse.Name = "btnViewCourse";
			btnViewCourse.Size = new Size(320, 40);
			btnViewCourse.TabIndex = 42;
			btnViewCourse.Text = "Xem khóa học";
			btnViewCourse.UseVisualStyleBackColor = false;
			btnViewCourse.Visible = false;
			// 
			// btnStatistics
			// 
			btnStatistics.BackColor = Color.FromArgb(255, 193, 7);
		 btnStatistics.Cursor = Cursors.Hand;
		 btnStatistics.FlatAppearance.BorderSize = 0;
		 btnStatistics.FlatStyle = FlatStyle.Flat;
		 btnStatistics.Font = new Font("Segoe UI", 10F);
		 btnStatistics.ForeColor = Color.FromArgb(102, 51, 0);
		 btnStatistics.Location = new Point(0, 95);
		 btnStatistics.Margin = new Padding(0, 5, 0, 0);
		 btnStatistics.Name = "btnStatistics";
		 btnStatistics.Size = new Size(320, 40);
		 btnStatistics.TabIndex = 43;
		 btnStatistics.Text = "Xem thống kê";
		 btnStatistics.UseVisualStyleBackColor = false;
		 btnStatistics.Visible = false;
		 // 
		 // pnlActions
		 // 
		 pnlActions.Controls.Add(lblPrice);
		 pnlActions.Controls.Add(btnAddToCart);
		 pnlActions.Controls.Add(btnBuyNow);
		 pnlActions.Controls.Add(btnStartLearning);
		 pnlActions.Controls.Add(pnlOwnerActions);
		 pnlActions.FlowDirection = FlowDirection.TopDown;
		 pnlActions.Location = new Point(1352, 262);
		 pnlActions.Name = "pnlActions";
		 pnlActions.Size = new Size(320, 260);
		 pnlActions.TabIndex = 9;
		 pnlActions.WrapContents = false;
		 // 
		 // pnlOwnerActions
		 // 
		 pnlOwnerActions.Controls.Add(btnEditCourse);
		 pnlOwnerActions.Controls.Add(btnViewCourse);
		 pnlOwnerActions.Controls.Add(btnStatistics);
		 pnlOwnerActions.FlowDirection = FlowDirection.TopDown;
		 pnlOwnerActions.Location = new Point(3, 235);
		 pnlOwnerActions.Name = "pnlOwnerActions";
		 pnlOwnerActions.Size = new Size(320, 140);
		 pnlOwnerActions.TabIndex = 13;
		 pnlOwnerActions.WrapContents = false;
		 // 
		 // lblContentTitle
		 // 
		 lblContentTitle.AutoSize = true;
		 lblContentTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
		 lblContentTitle.Location = new Point(80, 421);
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
		 lnkExpandAll.Location = new Point(1552, 397);
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
		 lblChapterStats.Location = new Point(426, 437);
		 lblChapterStats.Name = "lblChapterStats";
		 lblChapterStats.Size = new Size(108, 25);
		 lblChapterStats.TabIndex = 15;
		 lblChapterStats.Text = "chapterstats";
		 // 
		 // lblDescriptionTitle
		 // 
		 lblDescriptionTitle.AutoSize = true;
		 lblDescriptionTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
		 lblDescriptionTitle.Location = new Point(77, 715);
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
		 rtbDescription.Location = new Point(80, 777);
		 rtbDescription.Name = "rtbDescription";
		 rtbDescription.ReadOnly = true;
		 rtbDescription.Size = new Size(1194, 100);
		 rtbDescription.TabIndex = 18;
		 rtbDescription.Text = "";
		 // 
		 // lblInstructorTitle
		 // 
		 lblInstructorTitle.AutoSize = true;
		 lblInstructorTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
		 lblInstructorTitle.Location = new Point(77, 907);
		 lblInstructorTitle.Name = "lblInstructorTitle";
		 lblInstructorTitle.Size = new Size(179, 45);
		 lblInstructorTitle.TabIndex = 19;
		 lblInstructorTitle.Text = "Giảng viên";
		 // 
		 // lblInstructorName
		 // 
		 lblInstructorName.AutoSize = true;
		 lblInstructorName.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
		 lblInstructorName.Location = new Point(130, 993);
		 lblInstructorName.Name = "lblInstructorName";
		 lblInstructorName.Size = new Size(0, 30);
		 lblInstructorName.TabIndex = 20;
		 // 
		 // lblInstructorEmail
		 // 
		 lblInstructorEmail.AutoSize = true;
		 lblInstructorEmail.Font = new Font("Segoe UI", 9F);
		 lblInstructorEmail.ForeColor = Color.Gray;
		 lblInstructorEmail.Location = new Point(130, 1018);
		 lblInstructorEmail.Name = "lblInstructorEmail";
		 lblInstructorEmail.Size = new Size(0, 25);
		 lblInstructorEmail.TabIndex = 21;
		 // 
		 // lblRatingSectionTitle
		 // 
		 lblRatingSectionTitle.AutoSize = true;
		 lblRatingSectionTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
		 lblRatingSectionTitle.Location = new Point(77, 1083);
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
		 lblAvgRating.Location = new Point(70, 1144);
		 lblAvgRating.Name = "lblAvgRating";
		 lblAvgRating.Size = new Size(0, 96);
		 lblAvgRating.TabIndex = 23;
		 // 
		 // lblTotalRatingCount
		 // 
		 lblTotalRatingCount.AutoSize = true;
		 lblTotalRatingCount.Font = new Font("Segoe UI", 9F);
		 lblTotalRatingCount.ForeColor = Color.Gray;
		 lblTotalRatingCount.Location = new Point(70, 1224);
		 lblTotalRatingCount.Name = "lblTotalRatingCount";
		 lblTotalRatingCount.Size = new Size(0, 25);
		 lblTotalRatingCount.TabIndex = 24;
		 // 
		 // lblStar1
		 // 
		 lblStar1.AutoSize = true;
		 lblStar1.Font = new Font("Segoe UI", 9F);
		 lblStar1.Location = new Point(130, 1154);
		 lblStar1.Name = "lblStar1";
		 lblStar1.Size = new Size(87, 25);
		 lblStar1.TabIndex = 25;
		 lblStar1.Text = "★★★★★";
		 // 
		 // lblStar2
		 // 
		 lblStar2.AutoSize = true;
		 lblStar2.Font = new Font("Segoe UI", 9F);
		 lblStar2.Location = new Point(130, 1182);
		 lblStar2.Name = "lblStar2";
		 lblStar2.Size = new Size(87, 25);
		 lblStar2.TabIndex = 28;
		 lblStar2.Text = "★★★★☆";
		 // 
		 // lblStar3
		 // 
		 lblStar3.AutoSize = true;
		 lblStar3.Font = new Font("Segoe UI", 9F);
		 lblStar3.Location = new Point(130, 1210);
		 lblStar3.Name = "lblStar3";
		 lblStar3.Size = new Size(87, 25);
		 lblStar3.TabIndex = 31;
		 lblStar3.Text = "★★★☆☆";
		 // 
		 // lblStar4
		 // 
		 lblStar4.AutoSize = true;
		 lblStar4.Font = new Font("Segoe UI", 9F);
		 lblStar4.Location = new Point(130, 1238);
		 lblStar4.Name = "lblStar4";
		 lblStar4.Size = new Size(87, 25);
		 lblStar4.TabIndex = 34;
		 lblStar4.Text = "★★☆☆☆";
		 // 
		 // lblStar5
		 // 
		 lblStar5.AutoSize = true;
		 lblStar5.Font = new Font("Segoe UI", 9F);
		 lblStar5.Location = new Point(130, 1266);
		 lblStar5.Name = "lblStar5";
		 lblStar5.Size = new Size(87, 25);
		 lblStar5.TabIndex = 37;
		 lblStar5.Text = "★☆☆☆☆";
		 // 
		 // flowReviews
		 // 
		 flowReviews.AutoSize = true;
		 flowReviews.FlowDirection = FlowDirection.TopDown;
		 flowReviews.Location = new Point(77, 1308);
		 flowReviews.Name = "flowReviews";
		 flowReviews.Size = new Size(1197, 300);
		 flowReviews.TabIndex = 40;
		 flowReviews.WrapContents = false;
		 // 
		 // panel1
		 // 
		 panel1.BackColor = Color.Teal;
		 panel1.Controls.Add(lblTitle);
		 panel1.Controls.Add(lblRating);
		 panel1.Controls.Add(lblRatingCount);
		 panel1.Controls.Add(lblStudents);
		 panel1.Controls.Add(lblInstructor);
		 panel1.Controls.Add(lblLastUpdated);
		 panel1.Location = new Point(77, 72);
		 panel1.Name = "panel1";
		 panel1.Size = new Size(1194, 330);
		 panel1.TabIndex = 41;
		 // 
		 // pnlChapters
		 // 
		 // Changed to FlowLayoutPanel so chapter panels are laid out vertically without overlapping
		 pnlChapters.AutoScroll = true;
		 pnlChapters.FlowDirection = FlowDirection.TopDown;
		 pnlChapters.WrapContents = false;
		 pnlChapters.Location = new Point(80, 486);
		 pnlChapters.Name = "pnlChapters";
		 pnlChapters.Size = new Size(1191, 200);
		 pnlChapters.TabIndex = 16;
		 // 
		 // CourseDetailControl
		 // 
		 AutoScroll = true;
		 BackColor = Color.White;
		 Controls.Add(panel1);
		 Controls.Add(lblBreadcrumb);
		 Controls.Add(picCover);
		 Controls.Add(pnlActions);
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
		 Size = new Size(1778, 1615);
		 ((System.ComponentModel.ISupportInitialize)picCover).EndInit();
		 pnlActions.ResumeLayout(false);
		 pnlOwnerActions.ResumeLayout(false);
		 panel1.ResumeLayout(false);
		 panel1.PerformLayout();
		 ResumeLayout(false);
		 PerformLayout();
		}
		private Panel panel1;
		private FlowLayoutPanel pnlChapters;
	}
}
