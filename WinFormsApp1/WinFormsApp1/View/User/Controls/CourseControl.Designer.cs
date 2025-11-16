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
            this.filterPanel = new System.Windows.Forms.Panel();
            this.lblFilterHeader = new System.Windows.Forms.Label();
            this.lblRatingHeader = new System.Windows.Forms.Label();
            this.chkRating4Plus = new System.Windows.Forms.CheckBox();
            this.chkRating3To4 = new System.Windows.Forms.CheckBox();
            this.chkRating2To3 = new System.Windows.Forms.CheckBox();
            this.chkRating1To2 = new System.Windows.Forms.CheckBox();
            this.lblPriceHeader = new System.Windows.Forms.Label();
            this.chkFree = new System.Windows.Forms.CheckBox();
            this.chkPaid = new System.Windows.Forms.CheckBox();
            this.mainContentPanel = new System.Windows.Forms.Panel();
            this.coursesPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.headerPanel = new System.Windows.Forms.Panel();
            this.lblHeader = new System.Windows.Forms.Label();
            this.lblCourseCount = new System.Windows.Forms.Label();
            this.lblSortLabel = new System.Windows.Forms.Label();
            this.cmbSort = new System.Windows.Forms.ComboBox();
            this.filterPanel.SuspendLayout();
            this.mainContentPanel.SuspendLayout();
            this.headerPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // filterPanel
            // 
            this.filterPanel.BackColor = System.Drawing.Color.White;
            this.filterPanel.Controls.Add(this.chkPaid);
            this.filterPanel.Controls.Add(this.chkFree);
            this.filterPanel.Controls.Add(this.lblPriceHeader);
            this.filterPanel.Controls.Add(this.chkRating1To2);
            this.filterPanel.Controls.Add(this.chkRating2To3);
            this.filterPanel.Controls.Add(this.chkRating3To4);
            this.filterPanel.Controls.Add(this.chkRating4Plus);
            this.filterPanel.Controls.Add(this.lblRatingHeader);
            this.filterPanel.Controls.Add(this.lblFilterHeader);
            this.filterPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.filterPanel.Location = new System.Drawing.Point(20, 80);
            this.filterPanel.Name = "filterPanel";
            this.filterPanel.Padding = new System.Windows.Forms.Padding(20);
            this.filterPanel.Size = new System.Drawing.Size(280, 600);
            this.filterPanel.TabIndex = 0;
            // 
            // lblFilterHeader
            // 
            this.lblFilterHeader.AutoSize = false;
            this.lblFilterHeader.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblFilterHeader.Location = new System.Drawing.Point(20, 20);
            this.lblFilterHeader.Name = "lblFilterHeader";
            this.lblFilterHeader.Size = new System.Drawing.Size(240, 30);
            this.lblFilterHeader.TabIndex = 0;
            this.lblFilterHeader.Text = "Bộ lọc";
            // 
            // lblRatingHeader
            // 
            this.lblRatingHeader.AutoSize = false;
            this.lblRatingHeader.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblRatingHeader.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblRatingHeader.Location = new System.Drawing.Point(20, 70);
            this.lblRatingHeader.Name = "lblRatingHeader";
            this.lblRatingHeader.Size = new System.Drawing.Size(240, 25);
            this.lblRatingHeader.TabIndex = 1;
            this.lblRatingHeader.Text = "Đánh giá";
            // 
            // chkRating4Plus
            // 
            this.chkRating4Plus.AutoSize = true;
            this.chkRating4Plus.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkRating4Plus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.chkRating4Plus.ForeColor = System.Drawing.Color.FromArgb(100, 100, 100);
            this.chkRating4Plus.Location = new System.Drawing.Point(20, 105);
            this.chkRating4Plus.Name = "chkRating4Plus";
            this.chkRating4Plus.Size = new System.Drawing.Size(200, 23);
            this.chkRating4Plus.TabIndex = 2;
            this.chkRating4Plus.Text = "4.0 trở lên ⭐⭐⭐⭐";
            this.chkRating4Plus.UseVisualStyleBackColor = true;
            this.chkRating4Plus.CheckedChanged += FilterChanged;
            // 
            // chkRating3To4
            // 
            this.chkRating3To4.AutoSize = true;
            this.chkRating3To4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkRating3To4.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.chkRating3To4.ForeColor = System.Drawing.Color.FromArgb(100, 100, 100);
            this.chkRating3To4.Location = new System.Drawing.Point(20, 140);
            this.chkRating3To4.Name = "chkRating3To4";
            this.chkRating3To4.Size = new System.Drawing.Size(150, 23);
            this.chkRating3To4.TabIndex = 3;
            this.chkRating3To4.Text = "3.0 - 3.9 ⭐⭐⭐";
            this.chkRating3To4.UseVisualStyleBackColor = true;
            this.chkRating3To4.CheckedChanged += FilterChanged;
            // 
            // chkRating2To3
            // 
            this.chkRating2To3.AutoSize = true;
            this.chkRating2To3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkRating2To3.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.chkRating2To3.ForeColor = System.Drawing.Color.FromArgb(100, 100, 100);
            this.chkRating2To3.Location = new System.Drawing.Point(20, 175);
            this.chkRating2To3.Name = "chkRating2To3";
            this.chkRating2To3.Size = new System.Drawing.Size(130, 23);
            this.chkRating2To3.TabIndex = 4;
            this.chkRating2To3.Text = "2.0 - 2.9 ⭐⭐";
            this.chkRating2To3.UseVisualStyleBackColor = true;
            this.chkRating2To3.CheckedChanged += FilterChanged;
            // 
            // chkRating1To2
            // 
            this.chkRating1To2.AutoSize = true;
            this.chkRating1To2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkRating1To2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.chkRating1To2.ForeColor = System.Drawing.Color.FromArgb(100, 100, 100);
            this.chkRating1To2.Location = new System.Drawing.Point(20, 210);
            this.chkRating1To2.Name = "chkRating1To2";
            this.chkRating1To2.Size = new System.Drawing.Size(100, 23);
            this.chkRating1To2.TabIndex = 5;
            this.chkRating1To2.Text = "1.0 - 1.9 ⭐";
            this.chkRating1To2.UseVisualStyleBackColor = true;
            this.chkRating1To2.CheckedChanged += FilterChanged;
            // 
            // lblPriceHeader
            // 
            this.lblPriceHeader.AutoSize = false;
            this.lblPriceHeader.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblPriceHeader.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblPriceHeader.Location = new System.Drawing.Point(20, 260);
            this.lblPriceHeader.Name = "lblPriceHeader";
            this.lblPriceHeader.Size = new System.Drawing.Size(240, 25);
            this.lblPriceHeader.TabIndex = 6;
            this.lblPriceHeader.Text = "Giá";
            // 
            // chkFree
            // 
            this.chkFree.AutoSize = true;
            this.chkFree.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkFree.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.chkFree.ForeColor = System.Drawing.Color.FromArgb(100, 100, 100);
            this.chkFree.Location = new System.Drawing.Point(20, 295);
            this.chkFree.Name = "chkFree";
            this.chkFree.Size = new System.Drawing.Size(120, 23);
            this.chkFree.TabIndex = 7;
            this.chkFree.Text = "💚 Miễn phí";
            this.chkFree.UseVisualStyleBackColor = true;
            this.chkFree.CheckedChanged += FilterChanged;
            // 
            // chkPaid
            // 
            this.chkPaid.AutoSize = true;
            this.chkPaid.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkPaid.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.chkPaid.ForeColor = System.Drawing.Color.FromArgb(100, 100, 100);
            this.chkPaid.Location = new System.Drawing.Point(20, 330);
            this.chkPaid.Name = "chkPaid";
            this.chkPaid.Size = new System.Drawing.Size(100, 23);
            this.chkPaid.TabIndex = 8;
            this.chkPaid.Text = "💵 Trả phí";
            this.chkPaid.UseVisualStyleBackColor = true;
            this.chkPaid.CheckedChanged += FilterChanged;
            // 
            // mainContentPanel
            // 
            this.mainContentPanel.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.mainContentPanel.Controls.Add(this.coursesPanel);
            this.mainContentPanel.Controls.Add(this.headerPanel);
            this.mainContentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainContentPanel.Location = new System.Drawing.Point(300, 80);
            this.mainContentPanel.Name = "mainContentPanel";
            this.mainContentPanel.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.mainContentPanel.Size = new System.Drawing.Size(880, 600);
            this.mainContentPanel.TabIndex = 1;
            // 
            // headerPanel
            // 
            this.headerPanel.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.headerPanel.Controls.Add(this.cmbSort);
            this.headerPanel.Controls.Add(this.lblSortLabel);
            this.headerPanel.Controls.Add(this.lblCourseCount);
            this.headerPanel.Controls.Add(this.lblHeader);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Location = new System.Drawing.Point(20, 0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(860, 60);
            this.headerPanel.TabIndex = 0;
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = false;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblHeader.Location = new System.Drawing.Point(0, 10);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(300, 35);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "Tất cả khóa học";
            // 
            // lblCourseCount
            // 
            this.lblCourseCount.AutoSize = true;
            this.lblCourseCount.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblCourseCount.ForeColor = System.Drawing.Color.FromArgb(100, 100, 100);
            this.lblCourseCount.Location = new System.Drawing.Point(400, 20);
            this.lblCourseCount.Name = "lblCourseCount";
            this.lblCourseCount.Size = new System.Drawing.Size(80, 20);
            this.lblCourseCount.TabIndex = 1;
            this.lblCourseCount.Text = "0 khóa học";
            // 
            // lblSortLabel
            // 
            this.lblSortLabel.AutoSize = true;
            this.lblSortLabel.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblSortLabel.ForeColor = System.Drawing.Color.FromArgb(100, 100, 100);
            this.lblSortLabel.Location = new System.Drawing.Point(500, 20);
            this.lblSortLabel.Name = "lblSortLabel";
            this.lblSortLabel.Size = new System.Drawing.Size(100, 20);
            this.lblSortLabel.TabIndex = 2;
            this.lblSortLabel.Text = "Sắp xếp theo";
            // 
            // cmbSort
            // 
            this.cmbSort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSort.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbSort.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbSort.FormattingEnabled = true;
            this.cmbSort.Items.AddRange(new object[] {
            "Phổ biến nhất",
            "Đánh giá cao nhất",
            "Mới nhất",
            "Giá thấp đến cao",
            "Giá cao đến thấp"});
            this.cmbSort.Location = new System.Drawing.Point(610, 17);
            this.cmbSort.Name = "cmbSort";
            this.cmbSort.Size = new System.Drawing.Size(200, 25);
            this.cmbSort.TabIndex = 3;
            this.cmbSort.SelectedIndexChanged += SortChanged;
            // 
            // coursesPanel
            // 
            this.coursesPanel.AutoScroll = true;
            this.coursesPanel.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.coursesPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.coursesPanel.Location = new System.Drawing.Point(20, 60);
            this.coursesPanel.Name = "coursesPanel";
            this.coursesPanel.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.coursesPanel.Size = new System.Drawing.Size(860, 540);
            this.coursesPanel.TabIndex = 1;
            // 
            // CourseControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.Controls.Add(this.mainContentPanel);
            this.Controls.Add(this.filterPanel);
            this.Name = "CourseControl";
            this.Padding = new System.Windows.Forms.Padding(20, 80, 20, 20);
            this.Size = new System.Drawing.Size(1200, 700);
            this.filterPanel.ResumeLayout(false);
            this.filterPanel.PerformLayout();
            this.mainContentPanel.ResumeLayout(false);
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            this.ResumeLayout(false);

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
