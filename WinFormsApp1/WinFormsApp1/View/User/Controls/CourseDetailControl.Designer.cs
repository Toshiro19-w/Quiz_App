namespace WinFormsApp1.View.User.Controls
{
	partial class CourseDetailControl
	{
		private System.ComponentModel.IContainer components = null;

		private Label lblBreadcrumb;
		private Label lblTitle;
		private Panel pnlBestseller;
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
		private ProgressBar[] ratingProgressBars = new ProgressBar[5];
		private Label[] ratingPercentLabels = new Label[5];
		private FlowLayoutPanel flowReviews;

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null) components.Dispose();
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
			this.AutoScaleMode = AutoScaleMode.Font;
			this.Font = new Font("Segoe UI", 9F);
			this.BackColor = Color.White;
			this.Width = 1100;
			this.Height = 1500;

			int y = 20;

			// Breadcrumb
			lblBreadcrumb = new Label { Location = new Point(20, y), AutoSize = true, ForeColor = Color.Gray };
			y += 30;

			// Title
			lblTitle = new Label { Location = new Point(20, y), Width = 700, Font = new Font("Segoe UI", 24F, FontStyle.Bold), ForeColor = Color.FromArgb(0, 102, 102) };
			y += 50;

			// Meta
			pnlBestseller = new Panel { Location = new Point(20, y), Size = new Size(90, 28), BackColor = Color.FromArgb(255, 193, 7) };
			var lblBest = new Label { Text = "Bestseller", Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleCenter, ForeColor = Color.FromArgb(102, 51, 0), Font = new Font("Segoe UI", 9F, FontStyle.Bold) };
			pnlBestseller.Controls.Add(lblBest);

			lblRating = new Label { Location = new Point(120, y + 5), AutoSize = true, Font = new Font("Segoe UI", 10F, FontStyle.Bold) };
			lblRatingCount = new Label { Location = new Point(180, y + 5), AutoSize = true, ForeColor = Color.Gray };
			lblStudents = new Label { Location = new Point(300, y + 5), AutoSize = true };
			y += 35;

			lblInstructor = new Label { Location = new Point(20, y), AutoSize = true };
			y += 25;

			lblLastUpdated = new Label { Location = new Point(20, y), AutoSize = true, ForeColor = Color.Gray };
			y += 40;

			// Right panel
			picCover = new PictureBox { Location = new Point(750, 80), Size = new Size(320, 180), SizeMode = PictureBoxSizeMode.Zoom, BorderStyle = BorderStyle.FixedSingle };
			((System.ComponentModel.ISupportInitialize)(picCover)).BeginInit();

			lblPrice = new Label { Location = new Point(750, 270), Font = new Font("Segoe UI", 28F, FontStyle.Bold), ForeColor = Color.FromArgb(0, 102, 102) };

			btnAddToCart = new Button { Text = "Thêm vào giỏ hàng", Location = new Point(750, 330), Size = new Size(320, 50), BackColor = Color.FromArgb(0, 128, 128), ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Font = new Font("Segoe UI", 11F, FontStyle.Bold) };
			btnBuyNow = new Button { Text = "Heart Mua khóa học", Location = new Point(750, 390), Size = new Size(320, 40), FlatStyle = FlatStyle.Flat };
			btnStartLearning = new Button { Text = "Bắt đầu học", Location = new Point(750, 330), Size = new Size(320, 50), BackColor = Color.FromArgb(0, 153, 76), ForeColor = Color.White, Font = new Font("Segoe UI", 11F, FontStyle.Bold) };

			btnAddToCart.Click += btnAddToCart_Click;
			btnBuyNow.Click += btnBuyNow_Click;
			btnStartLearning.Click += btnStartLearning_Click;

			y = 400;

			// Nội dung
			lblContentTitle = new Label { Text = "Nội dung khóa học", Location = new Point(20, y), Font = new Font("Segoe UI", 16F, FontStyle.Bold) };
			lnkExpandAll = new LinkLabel { Text = "Mở rộng tất cả", Location = new Point(950, y), AutoSize = true, LinkColor = Color.FromArgb(0, 153, 153) };
			y += 35;

			lblChapterStats = new Label { Location = new Point(20, y), AutoSize = true, ForeColor = Color.Gray };
			y += 30;

			pnlChapters = new Panel { Location = new Point(20, y), Width = 700, AutoSize = true, AutoSizeMode = AutoSizeMode.GrowAndShrink };
			y += 50;

			// Mô tả
			lblDescriptionTitle = new Label { Text = "Mô tả", Location = new Point(20, y), Font = new Font("Segoe UI", 16F, FontStyle.Bold) };
			y += 35;

			rtbDescription = new RichTextBox { Location = new Point(20, y), Width = 700, Height = 100, BorderStyle = BorderStyle.None, BackColor = Color.FromArgb(248, 249, 250), ReadOnly = true };
			y += 120;

			// Giảng viên
			lblInstructorTitle = new Label { Text = "Giảng viên", Location = new Point(20, y), Font = new Font("Segoe UI", 16F, FontStyle.Bold) };
			y += 35;

			lblInstructorName = new Label { Location = new Point(80, y), AutoSize = true, Font = new Font("Segoe UI", 11F, FontStyle.Bold) };
			lblInstructorEmail = new Label { Location = new Point(80, y + 25), AutoSize = true, ForeColor = Color.Gray };
			y += 80;

			// Đánh giá
			lblRatingSectionTitle = new Label { Text = "Đánh giá của học viên", Location = new Point(20, y), Font = new Font("Segoe UI", 16F, FontStyle.Bold) };
			y += 35;

			lblAvgRating = new Label { Location = new Point(20, y), Font = new Font("Segoe UI", 36F), ForeColor = Color.FromArgb(0, 102, 102) };
			lblTotalRatingCount = new Label { Location = new Point(20, y + 50), AutoSize = true, ForeColor = Color.Gray };
			y += 80;

			for (int i = 0; i < 5; i++)
			{
				ratingProgressBars[i] = new ProgressBar { Location = new Point(150, y + i * 28), Width = 200, Height = 15, Maximum = 100 };
				ratingPercentLabels[i] = new Label { Location = new Point(360, y + i * 28), AutoSize = true };
				var star = new Label { Text = $"{5 - i} Star", Location = new Point(120, y + i * 28), Width = 30, TextAlign = ContentAlignment.MiddleRight };
				this.Controls.AddRange(new Control[] { ratingProgressBars[i], ratingPercentLabels[i], star });
			}
			y += 160;

			flowReviews = new FlowLayoutPanel { Location = new Point(20, y), Width = 700, AutoSize = true, FlowDirection = FlowDirection.TopDown };

			// Add all
			this.Controls.AddRange(new Control[] {
				lblBreadcrumb, lblTitle, pnlBestseller, lblRating, lblRatingCount, lblStudents,
				lblInstructor, lblLastUpdated, picCover, lblPrice, btnAddToCart, btnBuyNow, btnStartLearning,
				lblContentTitle, lnkExpandAll, lblChapterStats, pnlChapters,
				lblDescriptionTitle, rtbDescription,
				lblInstructorTitle, lblInstructorName, lblInstructorEmail,
				lblRatingSectionTitle, lblAvgRating, lblTotalRatingCount, flowReviews
			});

			((System.ComponentModel.ISupportInitialize)(picCover)).EndInit();
		}
	}
}