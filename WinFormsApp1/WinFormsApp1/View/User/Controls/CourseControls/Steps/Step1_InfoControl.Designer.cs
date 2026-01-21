namespace WinFormsApp1.View.User.Controls.CourseControls.Steps
{
    partial class Step1_InfoControl
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
			Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
			Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
			Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
			Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
			Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
			Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
			Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
			pnlCard = new Guna.UI2.WinForms.Guna2ShadowPanel();
			lblHeader = new Label();
			lblTitleLabel = new Label();
			txtTitle = new Guna.UI2.WinForms.Guna2TextBox();
			lblSlugLabel = new Label();
			txtSlug = new Guna.UI2.WinForms.Guna2TextBox();
			lblSummaryLabel = new Label();
			txtSummary = new RichTextBox();
			lblCategoryLabel = new Label();
			cmbCategory = new Guna.UI2.WinForms.Guna2ComboBox();
			lblPriceLabel = new Label();
			txtPrice = new Guna.UI2.WinForms.Guna2TextBox();
			lblCoverLabel = new Label();
			btnUploadCover = new Guna.UI2.WinForms.Guna2Button();
			picCover = new PictureBox();
			btnNext = new Guna.UI2.WinForms.Guna2Button();
			pnlCard.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)picCover).BeginInit();
			SuspendLayout();
			// 
			// pnlCard
			// 
			pnlCard.BackColor = Color.Transparent;
			pnlCard.Controls.Add(lblHeader);
			pnlCard.Controls.Add(lblTitleLabel);
			pnlCard.Controls.Add(txtTitle);
			pnlCard.Controls.Add(lblSlugLabel);
			pnlCard.Controls.Add(txtSlug);
			pnlCard.Controls.Add(lblSummaryLabel);
			pnlCard.Controls.Add(txtSummary);
			pnlCard.Controls.Add(lblCategoryLabel);
			pnlCard.Controls.Add(cmbCategory);
			pnlCard.Controls.Add(lblPriceLabel);
			pnlCard.Controls.Add(txtPrice);
			pnlCard.Controls.Add(lblCoverLabel);
			pnlCard.Controls.Add(btnUploadCover);
			pnlCard.Controls.Add(picCover);
			pnlCard.Controls.Add(btnNext);
			pnlCard.Dock = DockStyle.Fill;
			pnlCard.FillColor = Color.White;
			pnlCard.Location = new Point(0, 0);
			pnlCard.Name = "pnlCard";
			pnlCard.Padding = new Padding(18);
			pnlCard.ShadowColor = Color.Black;
			pnlCard.Size = new Size(1530, 830);
			pnlCard.TabIndex = 0;
			pnlCard.Paint += pnlCard_Paint;
			// 
			// lblHeader
			// 
			lblHeader.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			lblHeader.BackColor = Color.FromArgb(0, 172, 193);
			lblHeader.Font = new Font("Segoe UI", 20F, FontStyle.Bold, GraphicsUnit.Point, 0);
			lblHeader.ForeColor = SystemColors.Window;
			lblHeader.Location = new Point(40, 11);
			lblHeader.Margin = new Padding(0);
			lblHeader.Name = "lblHeader";
			lblHeader.Size = new Size(1457, 57);
			lblHeader.TabIndex = 0;
			lblHeader.Text = "Thông tin khóa học";
			lblHeader.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// lblTitleLabel
			// 
			lblTitleLabel.Font = new Font("Segoe UI", 12F);
			lblTitleLabel.Location = new Point(39, 90);
			lblTitleLabel.Name = "lblTitleLabel";
			lblTitleLabel.Size = new Size(1457, 29);
			lblTitleLabel.TabIndex = 1;
			lblTitleLabel.Text = "Tiêu đề";
			// 
			// txtTitle
			// 
			txtTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			txtTitle.CustomizableEdges = customizableEdges1;
			txtTitle.DefaultText = "";
			txtTitle.Font = new Font("Segoe UI", 12F);
			txtTitle.ForeColor = Color.Black;
			txtTitle.Location = new Point(40, 127);
			txtTitle.Margin = new Padding(4, 5, 4, 5);
			txtTitle.Name = "txtTitle";
			txtTitle.PlaceholderText = "Tiêu đề khóa học";
			txtTitle.SelectedText = "";
			txtTitle.ShadowDecoration.CustomizableEdges = customizableEdges2;
			txtTitle.Size = new Size(1457, 36);
			txtTitle.TabIndex = 1;
			// 
			// lblSlugLabel
			// 
			lblSlugLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			lblSlugLabel.Font = new Font("Segoe UI", 12F);
			lblSlugLabel.Location = new Point(39, 183);
			lblSlugLabel.Name = "lblSlugLabel";
			lblSlugLabel.Size = new Size(1457, 35);
			lblSlugLabel.TabIndex = 2;
			lblSlugLabel.Text = "URL Slug";
			// 
			// txtSlug
			// 
			txtSlug.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			txtSlug.CustomizableEdges = customizableEdges3;
			txtSlug.DefaultText = "";
			txtSlug.Font = new Font("Segoe UI", 12F);
			txtSlug.Location = new Point(40, 224);
			txtSlug.Margin = new Padding(4, 5, 4, 5);
			txtSlug.Name = "txtSlug";
			txtSlug.PlaceholderText = "URL slug";
			txtSlug.SelectedText = "";
			txtSlug.ShadowDecoration.CustomizableEdges = customizableEdges4;
			txtSlug.Size = new Size(1457, 36);
			txtSlug.TabIndex = 2;
			// 
			// lblSummaryLabel
			// 
			lblSummaryLabel.Font = new Font("Microsoft Sans Serif", 12F);
			lblSummaryLabel.Location = new Point(38, 280);
			lblSummaryLabel.Name = "lblSummaryLabel";
			lblSummaryLabel.Size = new Size(739, 29);
			lblSummaryLabel.TabIndex = 3;
			lblSummaryLabel.Text = "Mô tả ngắn";
			// 
			// txtSummary
			// 
			txtSummary.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			txtSummary.Font = new Font("Microsoft Sans Serif", 12F);
			txtSummary.Location = new Point(39, 315);
			txtSummary.Name = "txtSummary";
			txtSummary.Size = new Size(1457, 120);
			txtSummary.TabIndex = 3;
			txtSummary.Text = "";
			// 
			// lblCategoryLabel
			// 
			lblCategoryLabel.Font = new Font("Microsoft Sans Serif", 12F);
			lblCategoryLabel.Location = new Point(40, 456);
			lblCategoryLabel.Name = "lblCategoryLabel";
			lblCategoryLabel.Size = new Size(466, 28);
			lblCategoryLabel.TabIndex = 4;
			lblCategoryLabel.Text = "Danh mục";
			// 
			// cmbCategory
			// 
			cmbCategory.BackColor = Color.Transparent;
			cmbCategory.CustomizableEdges = customizableEdges5;
			cmbCategory.DrawMode = DrawMode.OwnerDrawFixed;
			cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
			cmbCategory.FocusedColor = Color.Empty;
			cmbCategory.Font = new Font("Microsoft Sans Serif", 12F);
			cmbCategory.ForeColor = Color.FromArgb(68, 88, 112);
			cmbCategory.ItemHeight = 30;
			cmbCategory.Location = new Point(40, 492);
			cmbCategory.Name = "cmbCategory";
			cmbCategory.ShadowDecoration.CustomizableEdges = customizableEdges6;
			cmbCategory.Size = new Size(506, 36);
			cmbCategory.TabIndex = 4;
			// 
			// lblPriceLabel
			// 
			lblPriceLabel.Font = new Font("Microsoft Sans Serif", 12F);
			lblPriceLabel.Location = new Point(38, 547);
			lblPriceLabel.Name = "lblPriceLabel";
			lblPriceLabel.Size = new Size(508, 28);
			lblPriceLabel.TabIndex = 5;
			lblPriceLabel.Text = "Giá (VNĐ)";
			// 
			// txtPrice
			// 
			txtPrice.CustomizableEdges = customizableEdges7;
			txtPrice.DefaultText = "";
			txtPrice.Font = new Font("Microsoft Sans Serif", 12F);
			txtPrice.Location = new Point(38, 583);
			txtPrice.Margin = new Padding(4, 5, 4, 5);
			txtPrice.Name = "txtPrice";
			txtPrice.PlaceholderText = "Giá (VNĐ)";
			txtPrice.SelectedText = "";
			txtPrice.ShadowDecoration.CustomizableEdges = customizableEdges8;
			txtPrice.Size = new Size(508, 36);
			txtPrice.TabIndex = 5;
			// 
			// lblCoverLabel
			// 
			lblCoverLabel.Font = new Font("Microsoft Sans Serif", 12F);
			lblCoverLabel.Location = new Point(839, 455);
			lblCoverLabel.Name = "lblCoverLabel";
			lblCoverLabel.Size = new Size(98, 31);
			lblCoverLabel.TabIndex = 6;
			lblCoverLabel.Text = "Ảnh bìa";
			// 
			// btnUploadCover
			// 
			btnUploadCover.CustomizableEdges = customizableEdges9;
			btnUploadCover.Font = new Font("Microsoft Sans Serif", 10F);
			btnUploadCover.ForeColor = Color.White;
			btnUploadCover.Image = Properties.Resources.up_loading;
			btnUploadCover.Location = new Point(1006, 450);
			btnUploadCover.Name = "btnUploadCover";
			btnUploadCover.ShadowDecoration.CustomizableEdges = customizableEdges10;
			btnUploadCover.Size = new Size(155, 40);
			btnUploadCover.TabIndex = 6;
			btnUploadCover.Text = "Upload ảnh";
			// 
			// picCover
			// 
			picCover.BorderStyle = BorderStyle.FixedSingle;
			picCover.Location = new Point(839, 496);
			picCover.Name = "picCover";
			picCover.Size = new Size(322, 233);
			picCover.SizeMode = PictureBoxSizeMode.Zoom;
			picCover.TabIndex = 7;
			picCover.TabStop = false;
			// 
			// btnNext
			// 
			btnNext.CustomizableEdges = customizableEdges11;
			btnNext.Font = new Font("Segoe UI", 10F);
			btnNext.ForeColor = Color.White;
			btnNext.Image = Properties.Resources.next;
			btnNext.Location = new Point(39, 750);
			btnNext.Name = "btnNext";
			btnNext.ShadowDecoration.CustomizableEdges = customizableEdges12;
			btnNext.Size = new Size(140, 40);
			btnNext.TabIndex = 0;
			btnNext.Text = "Tiếp tục";
			// 
			// Step1_InfoControl
			// 
			Controls.Add(pnlCard);
			Name = "Step1_InfoControl";
			Size = new Size(1530, 830);
			pnlCard.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)picCover).EndInit();
			ResumeLayout(false);
		}
		private Guna.UI2.WinForms.Guna2ShadowPanel pnlCard;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblTitleLabel;
        public Guna.UI2.WinForms.Guna2TextBox txtTitle;
        private System.Windows.Forms.Label lblSlugLabel;
        public Guna.UI2.WinForms.Guna2TextBox txtSlug;
        private System.Windows.Forms.Label lblSummaryLabel;
        public System.Windows.Forms.RichTextBox txtSummary;
        private System.Windows.Forms.Label lblCategoryLabel;
        public Guna.UI2.WinForms.Guna2ComboBox cmbCategory;
        private System.Windows.Forms.Label lblPriceLabel;
        public Guna.UI2.WinForms.Guna2TextBox txtPrice;
        private System.Windows.Forms.Label lblCoverLabel;
        public Guna.UI2.WinForms.Guna2Button btnUploadCover;
        public System.Windows.Forms.PictureBox picCover;
        public Guna.UI2.WinForms.Guna2Button btnNext;
    }
}