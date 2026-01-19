namespace WinFormsApp1.View.User.Controls
{
    partial class CourseControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CourseControl));
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
			filterPanel = new Panel();
			btnClear = new Guna.UI2.WinForms.Guna2Button();
			btnApply = new Guna.UI2.WinForms.Guna2Button();
			txtFilterToPrice = new Guna.UI2.WinForms.Guna2TextBox();
			txtFilterFromPrice = new Guna.UI2.WinForms.Guna2TextBox();
			cbbFilterRate = new Guna.UI2.WinForms.Guna2ComboBox();
			cbbFilterCategory = new Guna.UI2.WinForms.Guna2ComboBox();
			label1 = new Label();
			lblPriceHeader = new Label();
			lblRatingHeader = new Label();
			lblFilterHeader = new Label();
			mainContentPanel = new Panel();
			coursesPanel = new FlowLayoutPanel();
			paginationControl1 = new PaginationControl();
			headerPanel = new Panel();
			cmbSort = new ComboBox();
			lblSortLabel = new Label();
			lblCourseCount = new Label();
			lblHeader = new Label();
			filterPanel.SuspendLayout();
			mainContentPanel.SuspendLayout();
			headerPanel.SuspendLayout();
			SuspendLayout();
			// 
			// filterPanel
			// 
			filterPanel.BackColor = Color.White;
			filterPanel.Controls.Add(btnClear);
			filterPanel.Controls.Add(btnApply);
			filterPanel.Controls.Add(txtFilterToPrice);
			filterPanel.Controls.Add(txtFilterFromPrice);
			filterPanel.Controls.Add(cbbFilterRate);
			filterPanel.Controls.Add(cbbFilterCategory);
			filterPanel.Controls.Add(label1);
			filterPanel.Controls.Add(lblPriceHeader);
			filterPanel.Controls.Add(lblRatingHeader);
			filterPanel.Controls.Add(lblFilterHeader);
			filterPanel.Dock = DockStyle.Left;
			filterPanel.Location = new Point(29, 33);
			filterPanel.Margin = new Padding(4, 5, 4, 5);
			filterPanel.Name = "filterPanel";
			filterPanel.Padding = new Padding(29, 33, 29, 33);
			filterPanel.Size = new Size(400, 855);
			filterPanel.TabIndex = 0;
			// 
			// btnClear
			// 
			btnClear.BorderRadius = 8;
			btnClear.CustomizableEdges = customizableEdges1;
			btnClear.DisabledState.BorderColor = Color.DarkGray;
			btnClear.DisabledState.CustomBorderColor = Color.DarkGray;
			btnClear.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
			btnClear.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
			btnClear.FillColor = Color.FromArgb(108, 117, 125);
			btnClear.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			btnClear.ForeColor = Color.White;
			btnClear.Image = (Image)resources.GetObject("btnClear.Image");
			btnClear.Location = new Point(206, 538);
			btnClear.Name = "btnClear";
			btnClear.ShadowDecoration.CustomizableEdges = customizableEdges2;
			btnClear.Size = new Size(154, 45);
			btnClear.TabIndex = 15;
			btnClear.Text = "Làm mới";
			btnClear.Click += BtnClear_Click;
			// 
			// btnApply
			// 
			btnApply.BorderRadius = 8;
			btnApply.CustomizableEdges = customizableEdges3;
			btnApply.DisabledState.BorderColor = Color.DarkGray;
			btnApply.DisabledState.CustomBorderColor = Color.DarkGray;
			btnApply.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
			btnApply.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
			btnApply.FillColor = Color.FromArgb(13, 110, 253);
			btnApply.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			btnApply.ForeColor = Color.White;
			btnApply.Image = Properties.Resources.filter;
			btnApply.Location = new Point(31, 538);
			btnApply.Name = "btnApply";
			btnApply.ShadowDecoration.CustomizableEdges = customizableEdges4;
			btnApply.Size = new Size(154, 45);
			btnApply.TabIndex = 14;
			btnApply.Text = "Áp dụng";
			btnApply.Click += BtnApply_Click;
			// 
			// txtFilterToPrice
			// 
			txtFilterToPrice.BorderRadius = 8;
			txtFilterToPrice.CustomizableEdges = customizableEdges5;
			txtFilterToPrice.DefaultText = "";
			txtFilterToPrice.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
			txtFilterToPrice.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
			txtFilterToPrice.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
			txtFilterToPrice.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
			txtFilterToPrice.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
			txtFilterToPrice.Font = new Font("Segoe UI", 10F);
			txtFilterToPrice.ForeColor = Color.Black;
			txtFilterToPrice.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
			txtFilterToPrice.Location = new Point(31, 456);
			txtFilterToPrice.Margin = new Padding(4, 5, 4, 5);
			txtFilterToPrice.Name = "txtFilterToPrice";
			txtFilterToPrice.PlaceholderForeColor = Color.FromArgb(125, 137, 149);
			txtFilterToPrice.PlaceholderText = "Đến giá";
			txtFilterToPrice.SelectedText = "";
			txtFilterToPrice.ShadowDecoration.CustomizableEdges = customizableEdges6;
			txtFilterToPrice.Size = new Size(329, 45);
			txtFilterToPrice.TabIndex = 13;
			// 
			// txtFilterFromPrice
			// 
			txtFilterFromPrice.BorderRadius = 8;
			txtFilterFromPrice.CustomizableEdges = customizableEdges7;
			txtFilterFromPrice.DefaultText = "";
			txtFilterFromPrice.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
			txtFilterFromPrice.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
			txtFilterFromPrice.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
			txtFilterFromPrice.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
			txtFilterFromPrice.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
			txtFilterFromPrice.Font = new Font("Segoe UI", 10F);
			txtFilterFromPrice.ForeColor = Color.Black;
			txtFilterFromPrice.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
			txtFilterFromPrice.Location = new Point(31, 399);
			txtFilterFromPrice.Margin = new Padding(4, 5, 4, 5);
			txtFilterFromPrice.Name = "txtFilterFromPrice";
			txtFilterFromPrice.PlaceholderForeColor = Color.FromArgb(125, 137, 149);
			txtFilterFromPrice.PlaceholderText = "Từ giá";
			txtFilterFromPrice.SelectedText = "";
			txtFilterFromPrice.ShadowDecoration.CustomizableEdges = customizableEdges8;
			txtFilterFromPrice.Size = new Size(329, 45);
			txtFilterFromPrice.TabIndex = 12;
			// 
			// cbbFilterRate
			// 
			cbbFilterRate.BackColor = Color.Transparent;
			cbbFilterRate.BorderRadius = 8;
			cbbFilterRate.CustomizableEdges = customizableEdges9;
			cbbFilterRate.DrawMode = DrawMode.OwnerDrawFixed;
			cbbFilterRate.DropDownStyle = ComboBoxStyle.DropDownList;
			cbbFilterRate.FocusedColor = Color.FromArgb(94, 148, 255);
			cbbFilterRate.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
			cbbFilterRate.Font = new Font("Segoe UI", 10F);
			cbbFilterRate.ForeColor = Color.FromArgb(68, 88, 112);
			cbbFilterRate.ItemHeight = 30;
			cbbFilterRate.Location = new Point(25, 267);
			cbbFilterRate.Name = "cbbFilterRate";
			cbbFilterRate.ShadowDecoration.CustomizableEdges = customizableEdges10;
			cbbFilterRate.Size = new Size(343, 36);
			cbbFilterRate.TabIndex = 11;
			// 
			// cbbFilterCategory
			// 
			cbbFilterCategory.BackColor = Color.Transparent;
			cbbFilterCategory.BorderRadius = 8;
			cbbFilterCategory.CustomizableEdges = customizableEdges11;
			cbbFilterCategory.DrawMode = DrawMode.OwnerDrawFixed;
			cbbFilterCategory.DropDownStyle = ComboBoxStyle.DropDownList;
			cbbFilterCategory.FocusedColor = Color.FromArgb(94, 148, 255);
			cbbFilterCategory.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
			cbbFilterCategory.Font = new Font("Segoe UI", 10F);
			cbbFilterCategory.ForeColor = Color.FromArgb(68, 88, 112);
			cbbFilterCategory.ItemHeight = 30;
			cbbFilterCategory.Location = new Point(24, 145);
			cbbFilterCategory.Name = "cbbFilterCategory";
			cbbFilterCategory.ShadowDecoration.CustomizableEdges = customizableEdges12;
			cbbFilterCategory.Size = new Size(343, 36);
			cbbFilterCategory.TabIndex = 10;
			// 
			// label1
			// 
			label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
			label1.ForeColor = Color.FromArgb(52, 58, 64);
			label1.Location = new Point(24, 100);
			label1.Margin = new Padding(4, 0, 4, 0);
			label1.Name = "label1";
			label1.Size = new Size(343, 42);
			label1.TabIndex = 9;
			label1.Text = "Danh mục";
			// 
			// lblPriceHeader
			// 
			lblPriceHeader.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
			lblPriceHeader.ForeColor = Color.FromArgb(52, 58, 64);
			lblPriceHeader.Location = new Point(24, 341);
			lblPriceHeader.Margin = new Padding(4, 0, 4, 0);
			lblPriceHeader.Name = "lblPriceHeader";
			lblPriceHeader.Size = new Size(343, 42);
			lblPriceHeader.TabIndex = 6;
			lblPriceHeader.Text = "Giá";
			// 
			// lblRatingHeader
			// 
			lblRatingHeader.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
			lblRatingHeader.ForeColor = Color.FromArgb(52, 58, 64);
			lblRatingHeader.Location = new Point(24, 222);
			lblRatingHeader.Margin = new Padding(4, 0, 4, 0);
			lblRatingHeader.Name = "lblRatingHeader";
			lblRatingHeader.Size = new Size(343, 42);
			lblRatingHeader.TabIndex = 1;
			lblRatingHeader.Text = "Đánh giá";
			// 
			// lblFilterHeader
			// 
			lblFilterHeader.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
			lblFilterHeader.ForeColor = Color.FromArgb(33, 37, 41);
			lblFilterHeader.Location = new Point(24, 33);
			lblFilterHeader.Margin = new Padding(4, 0, 4, 0);
			lblFilterHeader.Name = "lblFilterHeader";
			lblFilterHeader.Size = new Size(343, 50);
			lblFilterHeader.TabIndex = 0;
			lblFilterHeader.Text = "Bộ lọc";
			lblFilterHeader.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// mainContentPanel
			// 
			mainContentPanel.BackColor = Color.FromArgb(248, 249, 250);
			mainContentPanel.Controls.Add(coursesPanel);
			mainContentPanel.Controls.Add(paginationControl1);
			mainContentPanel.Controls.Add(headerPanel);
			mainContentPanel.Dock = DockStyle.Fill;
			mainContentPanel.Location = new Point(429, 33);
			mainContentPanel.Margin = new Padding(4, 5, 4, 5);
			mainContentPanel.Name = "mainContentPanel";
			mainContentPanel.Padding = new Padding(29, 0, 0, 0);
			mainContentPanel.Size = new Size(1256, 855);
			mainContentPanel.TabIndex = 1;
			// 
			// coursesPanel
			// 
			coursesPanel.AutoScroll = true;
			coursesPanel.BackColor = Color.FromArgb(248, 249, 250);
			coursesPanel.Dock = DockStyle.Fill;
			coursesPanel.Location = new Point(29, 100);
			coursesPanel.Margin = new Padding(4, 5, 4, 5);
			coursesPanel.Name = "coursesPanel";
			coursesPanel.Padding = new Padding(0, 17, 0, 0);
			coursesPanel.Size = new Size(1227, 705);
			coursesPanel.TabIndex = 1;
			// 
			// paginationControl1
			// 
			paginationControl1.BackColor = Color.FromArgb(248, 249, 250);
			paginationControl1.Dock = DockStyle.Bottom;
			paginationControl1.Location = new Point(29, 805);
			paginationControl1.Margin = new Padding(4);
			paginationControl1.Name = "paginationControl1";
			paginationControl1.Size = new Size(1227, 50);
			paginationControl1.TabIndex = 2;
			// 
			// headerPanel
			// 
			headerPanel.BackColor = Color.FromArgb(248, 249, 250);
			headerPanel.Controls.Add(cmbSort);
			headerPanel.Controls.Add(lblSortLabel);
			headerPanel.Controls.Add(lblCourseCount);
			headerPanel.Controls.Add(lblHeader);
			headerPanel.Dock = DockStyle.Top;
			headerPanel.Location = new Point(29, 0);
			headerPanel.Margin = new Padding(4, 5, 4, 5);
			headerPanel.Name = "headerPanel";
			headerPanel.Size = new Size(1227, 100);
			headerPanel.TabIndex = 0;
			// 
			// cmbSort
			// 
			cmbSort.DropDownStyle = ComboBoxStyle.DropDownList;
			cmbSort.FlatStyle = FlatStyle.Flat;
			cmbSort.Font = new Font("Segoe UI", 10F);
			cmbSort.FormattingEnabled = true;
			cmbSort.Items.AddRange(new object[] { "Phổ biến nhất", "Đánh giá cao nhất", "Mới nhất", "Giá thấp đến cao", "Giá cao đến thấp" });
			cmbSort.Location = new Point(871, 28);
			cmbSort.Margin = new Padding(4, 5, 4, 5);
			cmbSort.Name = "cmbSort";
			cmbSort.Size = new Size(284, 36);
			cmbSort.TabIndex = 3;
			cmbSort.SelectedIndexChanged += SortChanged;
			// 
			// lblSortLabel
			// 
			lblSortLabel.AutoSize = true;
			lblSortLabel.Font = new Font("Segoe UI", 11F);
			lblSortLabel.ForeColor = Color.FromArgb(100, 100, 100);
			lblSortLabel.Location = new Point(714, 33);
			lblSortLabel.Margin = new Padding(4, 0, 4, 0);
			lblSortLabel.Name = "lblSortLabel";
			lblSortLabel.Size = new Size(140, 30);
			lblSortLabel.TabIndex = 2;
			lblSortLabel.Text = "Sắp xếp theo";
			// 
			// lblCourseCount
			// 
			lblCourseCount.AutoSize = true;
			lblCourseCount.Font = new Font("Segoe UI", 11F);
			lblCourseCount.ForeColor = Color.FromArgb(100, 100, 100);
			lblCourseCount.Location = new Point(571, 33);
			lblCourseCount.Margin = new Padding(4, 0, 4, 0);
			lblCourseCount.Name = "lblCourseCount";
			lblCourseCount.Size = new Size(119, 30);
			lblCourseCount.TabIndex = 1;
			lblCourseCount.Text = "0 khóa học";
			// 
			// lblHeader
			// 
			lblHeader.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
			lblHeader.Location = new Point(4, 17);
			lblHeader.Margin = new Padding(4, 0, 4, 0);
			lblHeader.Name = "lblHeader";
			lblHeader.Size = new Size(425, 58);
			lblHeader.TabIndex = 0;
			lblHeader.Text = "Tất cả khóa học";
			// 
			// CourseControl
			// 
			AutoScaleDimensions = new SizeF(10F, 25F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.FromArgb(248, 249, 250);
			Controls.Add(mainContentPanel);
			Controls.Add(filterPanel);
			Margin = new Padding(4, 5, 4, 5);
			Name = "CourseControl";
			Padding = new Padding(29, 33, 29, 33);
			Size = new Size(1714, 921);
			filterPanel.ResumeLayout(false);
			mainContentPanel.ResumeLayout(false);
			headerPanel.ResumeLayout(false);
			headerPanel.PerformLayout();
			ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel filterPanel;
        private System.Windows.Forms.Label lblFilterHeader;
        private System.Windows.Forms.Label lblRatingHeader;
        private System.Windows.Forms.Label lblPriceHeader;
        private System.Windows.Forms.Panel mainContentPanel;
        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblCourseCount;
        private System.Windows.Forms.Label lblSortLabel;
        private System.Windows.Forms.ComboBox cmbSort;
        private System.Windows.Forms.FlowLayoutPanel coursesPanel;
        private PaginationControl paginationControl1;
		private Guna.UI2.WinForms.Guna2ComboBox cbbFilterRate;
		private Guna.UI2.WinForms.Guna2ComboBox cbbFilterCategory;
		private Label label1;
		private Guna.UI2.WinForms.Guna2TextBox txtFilterToPrice;
		private Guna.UI2.WinForms.Guna2TextBox txtFilterFromPrice;
		private Guna.UI2.WinForms.Guna2Button btnClear;
		private Guna.UI2.WinForms.Guna2Button btnApply;
	}
}
