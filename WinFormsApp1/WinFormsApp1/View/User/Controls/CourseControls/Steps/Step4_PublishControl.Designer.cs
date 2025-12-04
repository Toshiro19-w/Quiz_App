namespace WinFormsApp1.View.User.Controls.CourseControls.Steps
{
    partial class Step4_PublishControl
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }
		private void InitializeComponent()
		{
			Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
			Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
			Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
			Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
			Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
			pnlCard = new Guna.UI2.WinForms.Guna2ShadowPanel();
			lblHeader = new Label();
			pnlPreview = new Panel();
			lblCourseInfoHeader = new Label();
			lblTitleLabel = new Label();
			lblTitleValue = new Label();
			lblSlugLabel = new Label();
			lblSlugValue = new Label();
			lblPriceLabel = new Label();
			lblPriceValue = new Label();
			lblStatusLabel = new Label();
			lblStatusValue = new Label();
			lblStructureHeader = new Label();
			lblChaptersLabel = new Label();
			lblChaptersValue = new Label();
			lblLessonsLabel = new Label();
			lblLessonsValue = new Label();
			lblContentsLabel = new Label();
			lblContentsValue = new Label();
			lblCourseStructureHeader = new Label();
			pnlCourseStructure = new Panel();
			btnSaveDraft = new Guna.UI2.WinForms.Guna2Button();
			btnPublish = new Guna.UI2.WinForms.Guna2Button();
			btnPrev = new Guna.UI2.WinForms.Guna2Button();
			pnlCard.SuspendLayout();
			pnlPreview.SuspendLayout();
			SuspendLayout();
			// 
			// pnlCard
			// 
			pnlCard.BackColor = Color.Transparent;
			pnlCard.Controls.Add(lblHeader);
			pnlCard.Controls.Add(pnlPreview);
			pnlCard.Controls.Add(btnSaveDraft);
			pnlCard.Controls.Add(btnPublish);
			pnlCard.Controls.Add(btnPrev);
			pnlCard.Dock = DockStyle.Fill;
			pnlCard.FillColor = Color.White;
			pnlCard.Location = new Point(0, 0);
			pnlCard.Name = "pnlCard";
			pnlCard.Padding = new Padding(18);
			pnlCard.ShadowColor = Color.Black;
			pnlCard.Size = new Size(1530, 830);
			pnlCard.TabIndex = 0;
			// 
			// lblHeader
			// 
			lblHeader.BackColor = Color.FromArgb(0, 172, 193);
			lblHeader.Dock = DockStyle.Top;
			lblHeader.Font = new Font("Segoe UI", 20F, FontStyle.Bold, GraphicsUnit.Point, 0);
			lblHeader.ForeColor = Color.White;
			lblHeader.Location = new Point(18, 18);
			lblHeader.Name = "lblHeader";
			lblHeader.Size = new Size(1494, 60);
			lblHeader.TabIndex = 0;
			lblHeader.Text = "Xem trước xuất bản";
			lblHeader.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// pnlPreview
			// 
			pnlPreview.AutoScroll = true;
			pnlPreview.Controls.Add(lblCourseInfoHeader);
			pnlPreview.Controls.Add(lblTitleLabel);
			pnlPreview.Controls.Add(lblTitleValue);
			pnlPreview.Controls.Add(lblSlugLabel);
			pnlPreview.Controls.Add(lblSlugValue);
			pnlPreview.Controls.Add(lblPriceLabel);
			pnlPreview.Controls.Add(lblPriceValue);
			pnlPreview.Controls.Add(lblStatusLabel);
			pnlPreview.Controls.Add(lblStatusValue);
			pnlPreview.Controls.Add(lblStructureHeader);
			pnlPreview.Controls.Add(lblChaptersLabel);
			pnlPreview.Controls.Add(lblChaptersValue);
			pnlPreview.Controls.Add(lblLessonsLabel);
			pnlPreview.Controls.Add(lblLessonsValue);
			pnlPreview.Controls.Add(lblContentsLabel);
			pnlPreview.Controls.Add(lblContentsValue);
			pnlPreview.Controls.Add(lblCourseStructureHeader);
			pnlPreview.Controls.Add(pnlCourseStructure);
			pnlPreview.Location = new Point(20, 88);
			pnlPreview.Name = "pnlPreview";
			pnlPreview.Size = new Size(1492, 644);
			pnlPreview.TabIndex = 1;
			pnlPreview.Paint += pnlPreview_Paint;
			// 
			// lblCourseInfoHeader
			// 
			lblCourseInfoHeader.AutoSize = true;
			lblCourseInfoHeader.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
			lblCourseInfoHeader.Location = new Point(20, 20);
			lblCourseInfoHeader.Name = "lblCourseInfoHeader";
			lblCourseInfoHeader.Size = new Size(273, 38);
			lblCourseInfoHeader.TabIndex = 0;
			lblCourseInfoHeader.Text = "Thông tin khóa học";
			// 
			// lblTitleLabel
			// 
			lblTitleLabel.AutoSize = true;
			lblTitleLabel.Font = new Font("Segoe UI", 12F);
			lblTitleLabel.Location = new Point(34, 72);
			lblTitleLabel.Name = "lblTitleLabel";
			lblTitleLabel.Size = new Size(99, 32);
			lblTitleLabel.TabIndex = 1;
			lblTitleLabel.Text = "Tiêu đề:";
			// 
			// lblTitleValue
			// 
			lblTitleValue.AutoSize = true;
			lblTitleValue.Font = new Font("Segoe UI", 12F);
			lblTitleValue.Location = new Point(139, 72);
			lblTitleValue.Name = "lblTitleValue";
			lblTitleValue.Size = new Size(89, 32);
			lblTitleValue.TabIndex = 2;
			lblTitleValue.Text = "tieu de";
			// 
			// lblSlugLabel
			// 
			lblSlugLabel.AutoSize = true;
			lblSlugLabel.Font = new Font("Segoe UI", 12F);
			lblSlugLabel.Location = new Point(706, 72);
			lblSlugLabel.Name = "lblSlugLabel";
			lblSlugLabel.Size = new Size(66, 32);
			lblSlugLabel.TabIndex = 3;
			lblSlugLabel.Text = "Slug:";
			// 
			// lblSlugValue
			// 
			lblSlugValue.AutoSize = true;
			lblSlugValue.Font = new Font("Segoe UI", 12F);
			lblSlugValue.Location = new Point(778, 72);
			lblSlugValue.Name = "lblSlugValue";
			lblSlugValue.Size = new Size(105, 32);
			lblSlugValue.TabIndex = 4;
			lblSlugValue.Text = "slug text";
			// 
			// lblPriceLabel
			// 
			lblPriceLabel.AutoSize = true;
			lblPriceLabel.Font = new Font("Segoe UI", 12F);
			lblPriceLabel.Location = new Point(34, 137);
			lblPriceLabel.Name = "lblPriceLabel";
			lblPriceLabel.Size = new Size(53, 32);
			lblPriceLabel.TabIndex = 5;
			lblPriceLabel.Text = "Giá:";
			// 
			// lblPriceValue
			// 
			lblPriceValue.AutoSize = true;
			lblPriceValue.Font = new Font("Segoe UI", 12F);
			lblPriceValue.Location = new Point(93, 137);
			lblPriceValue.Name = "lblPriceValue";
			lblPriceValue.Size = new Size(86, 32);
			lblPriceValue.TabIndex = 6;
			lblPriceValue.Text = "giatext";
			// 
			// lblStatusLabel
			// 
			lblStatusLabel.AutoSize = true;
			lblStatusLabel.Font = new Font("Segoe UI", 12F);
			lblStatusLabel.Location = new Point(706, 137);
			lblStatusLabel.Name = "lblStatusLabel";
			lblStatusLabel.Size = new Size(125, 32);
			lblStatusLabel.TabIndex = 7;
			lblStatusLabel.Text = "Trạng thái:";
			// 
			// lblStatusValue
			// 
			lblStatusValue.AutoSize = true;
			lblStatusValue.Font = new Font("Segoe UI", 12F);
			lblStatusValue.Location = new Point(837, 137);
			lblStatusValue.Name = "lblStatusValue";
			lblStatusValue.Size = new Size(157, 32);
			lblStatusValue.TabIndex = 8;
			lblStatusValue.Text = "trangthai text";
			// 
			// lblStructureHeader
			// 
			lblStructureHeader.AutoSize = true;
			lblStructureHeader.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
			lblStructureHeader.Location = new Point(20, 200);
			lblStructureHeader.Name = "lblStructureHeader";
			lblStructureHeader.Size = new Size(271, 38);
			lblStructureHeader.TabIndex = 9;
			lblStructureHeader.Text = "Tổng quan cấu trúc";
			// 
			// lblChaptersLabel
			// 
			lblChaptersLabel.AutoSize = true;
			lblChaptersLabel.Font = new Font("Segoe UI", 12F);
			lblChaptersLabel.Location = new Point(40, 254);
			lblChaptersLabel.Name = "lblChaptersLabel";
			lblChaptersLabel.Size = new Size(134, 32);
			lblChaptersLabel.TabIndex = 10;
			lblChaptersLabel.Text = "Số chương:";
			// 
			// lblChaptersValue
			// 
			lblChaptersValue.AutoSize = true;
			lblChaptersValue.Font = new Font("Segoe UI", 12F);
			lblChaptersValue.Location = new Point(180, 254);
			lblChaptersValue.Name = "lblChaptersValue";
			lblChaptersValue.Size = new Size(54, 32);
			lblChaptersValue.TabIndex = 11;
			lblChaptersValue.Text = "text";
			// 
			// lblLessonsLabel
			// 
			lblLessonsLabel.AutoSize = true;
			lblLessonsLabel.Font = new Font("Segoe UI", 12F);
			lblLessonsLabel.Location = new Point(700, 254);
			lblLessonsLabel.Name = "lblLessonsLabel";
			lblLessonsLabel.Size = new Size(131, 32);
			lblLessonsLabel.TabIndex = 12;
			lblLessonsLabel.Text = "Số bài học:";
			// 
			// lblLessonsValue
			// 
			lblLessonsValue.AutoSize = true;
			lblLessonsValue.Font = new Font("Segoe UI", 12F);
			lblLessonsValue.Location = new Point(837, 254);
			lblLessonsValue.Name = "lblLessonsValue";
			lblLessonsValue.Size = new Size(54, 32);
			lblLessonsValue.TabIndex = 13;
			lblLessonsValue.Text = "text";
			// 
			// lblContentsLabel
			// 
			lblContentsLabel.AutoSize = true;
			lblContentsLabel.Font = new Font("Segoe UI", 12F);
			lblContentsLabel.Location = new Point(40, 321);
			lblContentsLabel.Name = "lblContentsLabel";
			lblContentsLabel.Size = new Size(178, 32);
			lblContentsLabel.TabIndex = 14;
			lblContentsLabel.Text = "Tổng nội dung:";
			// 
			// lblContentsValue
			// 
			lblContentsValue.AutoSize = true;
			lblContentsValue.Font = new Font("Segoe UI", 12F);
			lblContentsValue.Location = new Point(224, 321);
			lblContentsValue.Name = "lblContentsValue";
			lblContentsValue.Size = new Size(54, 32);
			lblContentsValue.TabIndex = 15;
			lblContentsValue.Text = "text";
			// 
			// lblCourseStructureHeader
			// 
			lblCourseStructureHeader.AutoSize = true;
			lblCourseStructureHeader.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
			lblCourseStructureHeader.Location = new Point(20, 402);
			lblCourseStructureHeader.Name = "lblCourseStructureHeader";
			lblCourseStructureHeader.Size = new Size(254, 38);
			lblCourseStructureHeader.TabIndex = 16;
			lblCourseStructureHeader.Text = "Cấu trúc khóa học";
			// 
			// pnlCourseStructure
			// 
			pnlCourseStructure.AutoScroll = true;
			pnlCourseStructure.Location = new Point(20, 452);
			pnlCourseStructure.Name = "pnlCourseStructure";
			pnlCourseStructure.Size = new Size(1450, 171);
			pnlCourseStructure.TabIndex = 17;
			// 
			// btnSaveDraft
			// 
			btnSaveDraft.CustomizableEdges = customizableEdges1;
			btnSaveDraft.FillColor = Color.Silver;
			btnSaveDraft.Font = new Font("Segoe UI", 10F);
			btnSaveDraft.ForeColor = Color.White;
			btnSaveDraft.Image = Properties.Resources.draft;
			btnSaveDraft.Location = new Point(192, 773);
			btnSaveDraft.Name = "btnSaveDraft";
			btnSaveDraft.ShadowDecoration.CustomizableEdges = customizableEdges2;
			btnSaveDraft.Size = new Size(150, 40);
			btnSaveDraft.TabIndex = 2;
			btnSaveDraft.Text = "Lưu nháp";
			// 
			// btnPublish
			// 
			btnPublish.CustomizableEdges = customizableEdges3;
			btnPublish.Font = new Font("Segoe UI", 10F);
			btnPublish.ForeColor = Color.White;
			btnPublish.Image = Properties.Resources.publishing;
			btnPublish.Location = new Point(352, 773);
			btnPublish.Name = "btnPublish";
			btnPublish.ShadowDecoration.CustomizableEdges = customizableEdges4;
			btnPublish.Size = new Size(150, 40);
			btnPublish.TabIndex = 3;
			btnPublish.Text = "Xuất bản";
			// 
			// btnPrev
			// 
			btnPrev.CustomizableEdges = customizableEdges5;
			btnPrev.FillColor = Color.FromArgb(255, 128, 0);
			btnPrev.Font = new Font("Segoe UI", 10F);
			btnPrev.ForeColor = Color.White;
			btnPrev.Image = Properties.Resources.previous;
			btnPrev.Location = new Point(31, 773);
			btnPrev.Name = "btnPrev";
			btnPrev.ShadowDecoration.CustomizableEdges = customizableEdges5;
			btnPrev.Size = new Size(150, 40);
			btnPrev.TabIndex = 4;
			btnPrev.Text = "Quay lại";
			// 
			// Step4_PublishControl
			// 
			Controls.Add(pnlCard);
			Name = "Step4_PublishControl";
			Size = new Size(1530, 830);
			pnlCard.ResumeLayout(false);
			pnlPreview.ResumeLayout(false);
			pnlPreview.PerformLayout();
			ResumeLayout(false);
		}

		private Guna.UI2.WinForms.Guna2ShadowPanel pnlCard;
        private System.Windows.Forms.Label lblHeader;
        public System.Windows.Forms.Panel pnlPreview;
        private System.Windows.Forms.Label lblCourseInfoHeader;
        private System.Windows.Forms.Label lblTitleLabel;
        private System.Windows.Forms.Label lblTitleValue;
        private System.Windows.Forms.Label lblSlugLabel;
        private System.Windows.Forms.Label lblSlugValue;
        private System.Windows.Forms.Label lblPriceLabel;
        private System.Windows.Forms.Label lblPriceValue;
        private System.Windows.Forms.Label lblStatusLabel;
        private System.Windows.Forms.Label lblStatusValue;
        private System.Windows.Forms.Label lblStructureHeader;
        private System.Windows.Forms.Label lblChaptersLabel;
        private System.Windows.Forms.Label lblChaptersValue;
        private System.Windows.Forms.Label lblLessonsLabel;
        private System.Windows.Forms.Label lblLessonsValue;
        private System.Windows.Forms.Label lblContentsLabel;
        private System.Windows.Forms.Label lblContentsValue;
        private System.Windows.Forms.Label lblCourseStructureHeader;
        private System.Windows.Forms.Panel pnlCourseStructure;
        public Guna.UI2.WinForms.Guna2Button btnSaveDraft;
        public Guna.UI2.WinForms.Guna2Button btnPublish;
        public Guna.UI2.WinForms.Guna2Button btnPrev;
    }
}