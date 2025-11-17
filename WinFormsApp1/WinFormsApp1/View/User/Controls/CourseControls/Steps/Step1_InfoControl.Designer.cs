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
			txtTitle = new Guna.UI2.WinForms.Guna2TextBox();
			txtSlug = new Guna.UI2.WinForms.Guna2TextBox();
			txtSummary = new RichTextBox();
			cmbCategory = new Guna.UI2.WinForms.Guna2ComboBox();
			txtPrice = new Guna.UI2.WinForms.Guna2TextBox();
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
			pnlCard.Controls.Add(txtTitle);
			pnlCard.Controls.Add(txtSlug);
			pnlCard.Controls.Add(txtSummary);
			pnlCard.Controls.Add(cmbCategory);
			pnlCard.Controls.Add(txtPrice);
			pnlCard.Controls.Add(btnUploadCover);
			pnlCard.Controls.Add(picCover);
			pnlCard.Dock = DockStyle.Fill;
			pnlCard.FillColor = Color.White;
			pnlCard.Location = new Point(0, 0);
			pnlCard.Name = "pnlCard";
			pnlCard.Padding = new Padding(18);
			pnlCard.ShadowColor = Color.Black;
			pnlCard.Size = new Size(763, 489);
			pnlCard.TabIndex = 0;
			// 
			// lblHeader
			// 
			lblHeader.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
			lblHeader.Location = new Point(20, 10);
			lblHeader.Name = "lblHeader";
			lblHeader.Size = new Size(705, 54);
			lblHeader.TabIndex = 0;
			lblHeader.Text = "Thông tin khóa h?c";
			// 
			// txtTitle
			// 
			txtTitle.CustomizableEdges = customizableEdges1;
			txtTitle.DefaultText = "";
			txtTitle.Font = new Font("Segoe UI", 9F);
			txtTitle.Location = new Point(25, 100);
			txtTitle.Margin = new Padding(4, 5, 4, 5);
			txtTitle.Name = "txtTitle";
			txtTitle.PlaceholderText = "Tiêu ?? khóa h?c";
			txtTitle.SelectedText = "";
			txtTitle.ShadowDecoration.CustomizableEdges = customizableEdges2;
			txtTitle.Size = new Size(520, 36);
			txtTitle.TabIndex = 1;
			// 
			// txtSlug
			// 
			txtSlug.CustomizableEdges = customizableEdges3;
			txtSlug.DefaultText = "";
			txtSlug.Font = new Font("Segoe UI", 9F);
			txtSlug.Location = new Point(25, 146);
			txtSlug.Margin = new Padding(4, 5, 4, 5);
			txtSlug.Name = "txtSlug";
			txtSlug.PlaceholderText = "URL slug";
			txtSlug.SelectedText = "";
			txtSlug.ShadowDecoration.CustomizableEdges = customizableEdges4;
			txtSlug.Size = new Size(520, 36);
			txtSlug.TabIndex = 2;
			// 
			// txtSummary
			// 
			txtSummary.Location = new Point(25, 190);
			txtSummary.Name = "txtSummary";
			txtSummary.Size = new Size(520, 120);
			txtSummary.TabIndex = 3;
			txtSummary.Text = "";
			// 
			// cmbCategory
			// 
			cmbCategory.BackColor = Color.Transparent;
			cmbCategory.CustomizableEdges = customizableEdges5;
			cmbCategory.DrawMode = DrawMode.OwnerDrawFixed;
			cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
			cmbCategory.FocusedColor = Color.Empty;
			cmbCategory.Font = new Font("Segoe UI", 10F);
			cmbCategory.ForeColor = Color.FromArgb(68, 88, 112);
			cmbCategory.ItemHeight = 30;
			cmbCategory.Location = new Point(25, 320);
			cmbCategory.Name = "cmbCategory";
			cmbCategory.ShadowDecoration.CustomizableEdges = customizableEdges6;
			cmbCategory.Size = new Size(240, 36);
			cmbCategory.TabIndex = 4;
			// 
			// txtPrice
			// 
			txtPrice.CustomizableEdges = customizableEdges7;
			txtPrice.DefaultText = "";
			txtPrice.Font = new Font("Segoe UI", 9F);
			txtPrice.Location = new Point(285, 320);
			txtPrice.Margin = new Padding(4, 5, 4, 5);
			txtPrice.Name = "txtPrice";
			txtPrice.PlaceholderText = "Giá (VN?)";
			txtPrice.SelectedText = "";
			txtPrice.ShadowDecoration.CustomizableEdges = customizableEdges8;
			txtPrice.Size = new Size(260, 36);
			txtPrice.TabIndex = 5;
			// 
			// btnUploadCover
			// 
			btnUploadCover.CustomizableEdges = customizableEdges9;
			btnUploadCover.Font = new Font("Segoe UI", 9F);
			btnUploadCover.ForeColor = Color.White;
			btnUploadCover.Location = new Point(583, 100);
			btnUploadCover.Name = "btnUploadCover";
			btnUploadCover.ShadowDecoration.CustomizableEdges = customizableEdges10;
			btnUploadCover.Size = new Size(120, 36);
			btnUploadCover.TabIndex = 6;
			btnUploadCover.Text = "Upload ?nh";
			// 
			// picCover
			// 
			picCover.Location = new Point(565, 146);
			picCover.Name = "picCover";
			picCover.Size = new Size(160, 208);
			picCover.TabIndex = 7;
			picCover.TabStop = false;
			// 
			// btnNext
			// 
			btnNext.CustomizableEdges = customizableEdges11;
			btnNext.Font = new Font("Segoe UI", 9F);
			btnNext.ForeColor = Color.White;
			btnNext.Location = new Point(560, 420);
			btnNext.Name = "btnNext";
			btnNext.ShadowDecoration.CustomizableEdges = customizableEdges12;
			btnNext.Size = new Size(140, 40);
			btnNext.TabIndex = 0;
			btnNext.Text = "Ti?p t?c";
			// 
			// Step1_InfoControl
			// 
			Controls.Add(pnlCard);
			Name = "Step1_InfoControl";
			Size = new Size(763, 489);
			pnlCard.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)picCover).EndInit();
			ResumeLayout(false);
		}
		private Guna.UI2.WinForms.Guna2ShadowPanel pnlCard;
        private System.Windows.Forms.Label lblHeader;
        public Guna.UI2.WinForms.Guna2TextBox txtTitle;
        public Guna.UI2.WinForms.Guna2TextBox txtSlug;
        public System.Windows.Forms.RichTextBox txtSummary;
        public Guna.UI2.WinForms.Guna2ComboBox cmbCategory;
        public Guna.UI2.WinForms.Guna2TextBox txtPrice;
        public Guna.UI2.WinForms.Guna2Button btnUploadCover;
        public System.Windows.Forms.PictureBox picCover;
        public Guna.UI2.WinForms.Guna2Button btnNext;
    }
}