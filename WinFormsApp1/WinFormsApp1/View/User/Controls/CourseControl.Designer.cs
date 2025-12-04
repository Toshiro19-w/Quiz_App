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
			filterPanel = new Panel();
			chkPaid = new CheckBox();
			chkFree = new CheckBox();
			lblPriceHeader = new Label();
			chkRating1To2 = new CheckBox();
			chkRating2To3 = new CheckBox();
			chkRating3To4 = new CheckBox();
			chkRating4Plus = new CheckBox();
			lblRatingHeader = new Label();
			lblFilterHeader = new Label();
			mainContentPanel = new Panel();
			coursesPanel = new FlowLayoutPanel();
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
			filterPanel.Controls.Add(chkPaid);
			filterPanel.Controls.Add(chkFree);
			filterPanel.Controls.Add(lblPriceHeader);
			filterPanel.Controls.Add(chkRating1To2);
			filterPanel.Controls.Add(chkRating2To3);
			filterPanel.Controls.Add(chkRating3To4);
			filterPanel.Controls.Add(chkRating4Plus);
			filterPanel.Controls.Add(lblRatingHeader);
			filterPanel.Controls.Add(lblFilterHeader);
			filterPanel.Dock = DockStyle.Left;
			filterPanel.Location = new Point(29, 133);
			filterPanel.Margin = new Padding(4, 5, 4, 5);
			filterPanel.Name = "filterPanel";
			filterPanel.Padding = new Padding(29, 33, 29, 33);
			filterPanel.Size = new Size(400, 1001);
			filterPanel.TabIndex = 0;
			// 
			// chkPaid
			// 
			chkPaid.AutoSize = true;
			chkPaid.Cursor = Cursors.Hand;
			chkPaid.Font = new Font("Segoe UI", 10F);
			chkPaid.ForeColor = Color.FromArgb(100, 100, 100);
			chkPaid.Location = new Point(29, 550);
			chkPaid.Margin = new Padding(4, 5, 4, 5);
			chkPaid.Name = "chkPaid";
			chkPaid.Size = new Size(128, 32);
			chkPaid.TabIndex = 8;
			chkPaid.Text = "💵 Trả phí";
			chkPaid.UseVisualStyleBackColor = true;
			chkPaid.CheckedChanged += FilterChanged;
			// 
			// chkFree
			// 
			chkFree.AutoSize = true;
			chkFree.Cursor = Cursors.Hand;
			chkFree.Font = new Font("Segoe UI", 10F);
			chkFree.ForeColor = Color.FromArgb(100, 100, 100);
			chkFree.Location = new Point(29, 492);
			chkFree.Margin = new Padding(4, 5, 4, 5);
			chkFree.Name = "chkFree";
			chkFree.Size = new Size(147, 32);
			chkFree.TabIndex = 7;
			chkFree.Text = "💚 Miễn phí";
			chkFree.UseVisualStyleBackColor = true;
			chkFree.CheckedChanged += FilterChanged;
			// 
			// lblPriceHeader
			// 
			lblPriceHeader.Cursor = Cursors.Hand;
			lblPriceHeader.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
			lblPriceHeader.Location = new Point(29, 433);
			lblPriceHeader.Margin = new Padding(4, 0, 4, 0);
			lblPriceHeader.Name = "lblPriceHeader";
			lblPriceHeader.Size = new Size(343, 42);
			lblPriceHeader.TabIndex = 6;
			lblPriceHeader.Text = "Giá";
			// 
			// chkRating1To2
			// 
			chkRating1To2.AutoSize = true;
			chkRating1To2.Cursor = Cursors.Hand;
			chkRating1To2.Font = new Font("Segoe UI", 10F);
			chkRating1To2.ForeColor = Color.FromArgb(100, 100, 100);
			chkRating1To2.Location = new Point(29, 350);
			chkRating1To2.Margin = new Padding(4, 5, 4, 5);
			chkRating1To2.Name = "chkRating1To2";
			chkRating1To2.Size = new Size(130, 32);
			chkRating1To2.TabIndex = 5;
			chkRating1To2.Text = "1.0 - 1.9 ⭐";
			chkRating1To2.UseVisualStyleBackColor = true;
			chkRating1To2.CheckedChanged += FilterChanged;
			// 
			// chkRating2To3
			// 
			chkRating2To3.AutoSize = true;
			chkRating2To3.Cursor = Cursors.Hand;
			chkRating2To3.Font = new Font("Segoe UI", 10F);
			chkRating2To3.ForeColor = Color.FromArgb(100, 100, 100);
			chkRating2To3.Location = new Point(29, 292);
			chkRating2To3.Margin = new Padding(4, 5, 4, 5);
			chkRating2To3.Name = "chkRating2To3";
			chkRating2To3.Size = new Size(147, 32);
			chkRating2To3.TabIndex = 4;
			chkRating2To3.Text = "2.0 - 2.9 ⭐⭐";
			chkRating2To3.UseVisualStyleBackColor = true;
			chkRating2To3.CheckedChanged += FilterChanged;
			// 
			// chkRating3To4
			// 
			chkRating3To4.AutoSize = true;
			chkRating3To4.Cursor = Cursors.Hand;
			chkRating3To4.Font = new Font("Segoe UI", 10F);
			chkRating3To4.ForeColor = Color.FromArgb(100, 100, 100);
			chkRating3To4.Location = new Point(29, 233);
			chkRating3To4.Margin = new Padding(4, 5, 4, 5);
			chkRating3To4.Name = "chkRating3To4";
			chkRating3To4.Size = new Size(164, 32);
			chkRating3To4.TabIndex = 3;
			chkRating3To4.Text = "3.0 - 3.9 ⭐⭐⭐";
			chkRating3To4.UseVisualStyleBackColor = true;
			chkRating3To4.CheckedChanged += FilterChanged;
			// 
			// chkRating4Plus
			// 
			chkRating4Plus.AutoSize = true;
			chkRating4Plus.Cursor = Cursors.Hand;
			chkRating4Plus.Font = new Font("Segoe UI", 10F);
			chkRating4Plus.ForeColor = Color.FromArgb(100, 100, 100);
			chkRating4Plus.Location = new Point(29, 175);
			chkRating4Plus.Margin = new Padding(4, 5, 4, 5);
			chkRating4Plus.Name = "chkRating4Plus";
			chkRating4Plus.Size = new Size(199, 32);
			chkRating4Plus.TabIndex = 2;
			chkRating4Plus.Text = "4.0 trở lên ⭐⭐⭐⭐";
			chkRating4Plus.UseVisualStyleBackColor = true;
			chkRating4Plus.CheckedChanged += FilterChanged;
			// 
			// lblRatingHeader
			// 
			lblRatingHeader.Cursor = Cursors.Hand;
			lblRatingHeader.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
			lblRatingHeader.Location = new Point(29, 117);
			lblRatingHeader.Margin = new Padding(4, 0, 4, 0);
			lblRatingHeader.Name = "lblRatingHeader";
			lblRatingHeader.Size = new Size(343, 42);
			lblRatingHeader.TabIndex = 1;
			lblRatingHeader.Text = "Đánh giá";
			// 
			// lblFilterHeader
			// 
			lblFilterHeader.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
			lblFilterHeader.Location = new Point(29, 33);
			lblFilterHeader.Margin = new Padding(4, 0, 4, 0);
			lblFilterHeader.Name = "lblFilterHeader";
			lblFilterHeader.Size = new Size(343, 50);
			lblFilterHeader.TabIndex = 0;
			lblFilterHeader.Text = "Bộ lọc";
			// 
			// mainContentPanel
			// 
			mainContentPanel.BackColor = Color.FromArgb(248, 249, 250);
			mainContentPanel.Controls.Add(coursesPanel);
			mainContentPanel.Controls.Add(headerPanel);
			mainContentPanel.Dock = DockStyle.Fill;
			mainContentPanel.Location = new Point(429, 133);
			mainContentPanel.Margin = new Padding(4, 5, 4, 5);
			mainContentPanel.Name = "mainContentPanel";
			mainContentPanel.Padding = new Padding(29, 0, 0, 0);
			mainContentPanel.Size = new Size(1256, 1001);
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
			coursesPanel.Size = new Size(1227, 901);
			coursesPanel.TabIndex = 1;
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
			Padding = new Padding(29, 133, 29, 33);
			Size = new Size(1714, 1167);
			filterPanel.ResumeLayout(false);
			filterPanel.PerformLayout();
			mainContentPanel.ResumeLayout(false);
			headerPanel.ResumeLayout(false);
			headerPanel.PerformLayout();
			ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel filterPanel;
        private System.Windows.Forms.Label lblFilterHeader;
        private System.Windows.Forms.Label lblRatingHeader;
        private System.Windows.Forms.CheckBox chkRating4Plus;
        private System.Windows.Forms.CheckBox chkRating3To4;
        private System.Windows.Forms.CheckBox chkRating2To3;
        private System.Windows.Forms.CheckBox chkRating1To2;
        private System.Windows.Forms.Label lblPriceHeader;
        private System.Windows.Forms.CheckBox chkFree;
        private System.Windows.Forms.CheckBox chkPaid;
        private System.Windows.Forms.Panel mainContentPanel;
        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblCourseCount;
        private System.Windows.Forms.Label lblSortLabel;
        private System.Windows.Forms.ComboBox cmbSort;
        private System.Windows.Forms.FlowLayoutPanel coursesPanel;
    }
}
