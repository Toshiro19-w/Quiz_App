namespace WinFormsApp1.View.User.Controls
{
    partial class MyCoursesControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            pnlHeader = new Panel();
            lblTitle = new Label();
            pnlActions = new Panel();
            btnCreateCourse = new Button();
            btnRevenue = new Button();
            btnFlashcards = new Button();
            pnlFilters = new Panel();
            lblShowLabel = new Label();
            cmbPageSize = new ComboBox();
            lblEntriesLabel = new Label();
            lblSearchLabel = new Label();
            txtSearch = new TextBox();
            pnlTable = new Panel();
            pnlTableHeader = new Panel();
            lblHeaderId = new Label();
            lblHeaderTitle = new Label();
            lblHeaderStatus = new Label();
            lblHeaderPrice = new Label();
            lblHeaderDate = new Label();
            lblHeaderActions = new Label();
            flowCourses = new FlowLayoutPanel();
            pnlFooter = new Panel();
            lblPageInfo = new Label();
            pnlPagination = new Panel();
            btnFirstPage = new Button();
            btnPrevPage = new Button();
            lblCurrentPage = new Label();
            btnNextPage = new Button();
            btnLastPage = new Button();
            pnlHeader.SuspendLayout();
            pnlActions.SuspendLayout();
            pnlFilters.SuspendLayout();
            pnlTable.SuspendLayout();
            pnlTableHeader.SuspendLayout();
            pnlFooter.SuspendLayout();
            pnlPagination.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.White;
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Controls.Add(pnlActions);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Margin = new Padding(4, 5, 4, 5);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Padding = new Padding(43, 33, 43, 33);
            pnlHeader.Size = new Size(1714, 167);
            pnlHeader.TabIndex = 0;
            pnlHeader.Paint += pnlHeader_Paint;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTitle.Location = new Point(43, 50);
            lblTitle.Margin = new Padding(4, 0, 4, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(335, 54);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Khóa học của tôi";
            // 
            // pnlActions
            // 
            pnlActions.Controls.Add(btnCreateCourse);
            pnlActions.Controls.Add(btnRevenue);
            pnlActions.Controls.Add(btnFlashcards);
            pnlActions.Dock = DockStyle.Right;
            pnlActions.Location = new Point(1000, 33);
            pnlActions.Margin = new Padding(4, 5, 4, 5);
            pnlActions.Name = "pnlActions";
            pnlActions.Size = new Size(671, 101);
            pnlActions.TabIndex = 1;
            // 
            // btnCreateCourse
            // 
            btnCreateCourse.BackColor = Color.FromArgb(52, 144, 220);
            btnCreateCourse.Cursor = Cursors.Hand;
            btnCreateCourse.FlatAppearance.BorderSize = 0;
            btnCreateCourse.FlatStyle = FlatStyle.Flat;
            btnCreateCourse.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCreateCourse.ForeColor = Color.White;
            btnCreateCourse.Location = new Point(14, 17);
            btnCreateCourse.Margin = new Padding(4, 5, 4, 5);
            btnCreateCourse.Name = "btnCreateCourse";
            btnCreateCourse.Size = new Size(200, 67);
            btnCreateCourse.TabIndex = 0;
            btnCreateCourse.Text = "➕ Tạo khóa học";
            btnCreateCourse.UseVisualStyleBackColor = false;
            btnCreateCourse.Click += BtnCreateCourse_Click;
            // 
            // btnRevenue
            // 
            btnRevenue.BackColor = Color.FromArgb(40, 167, 69);
            btnRevenue.Cursor = Cursors.Hand;
            btnRevenue.FlatAppearance.BorderSize = 0;
            btnRevenue.FlatStyle = FlatStyle.Flat;
            btnRevenue.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnRevenue.ForeColor = Color.White;
            btnRevenue.Location = new Point(229, 17);
            btnRevenue.Margin = new Padding(4, 5, 4, 5);
            btnRevenue.Name = "btnRevenue";
            btnRevenue.Size = new Size(200, 67);
            btnRevenue.TabIndex = 1;
            btnRevenue.Text = "📊 Doanh thu";
            btnRevenue.UseVisualStyleBackColor = false;
            btnRevenue.Click += BtnRevenue_Click;
            // 
            // btnFlashcards
            // 
            btnFlashcards.BackColor = Color.FromArgb(23, 162, 184);
            btnFlashcards.Cursor = Cursors.Hand;
            btnFlashcards.FlatAppearance.BorderSize = 0;
            btnFlashcards.FlatStyle = FlatStyle.Flat;
            btnFlashcards.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnFlashcards.ForeColor = Color.White;
            btnFlashcards.Location = new Point(443, 17);
            btnFlashcards.Margin = new Padding(4, 5, 4, 5);
            btnFlashcards.Name = "btnFlashcards";
            btnFlashcards.Size = new Size(214, 67);
            btnFlashcards.TabIndex = 2;
            btnFlashcards.Text = "🗂️ Flashcard của tôi";
            btnFlashcards.UseVisualStyleBackColor = false;
            btnFlashcards.Click += BtnFlashcards_Click;
            // 
            // pnlFilters
            // 
            pnlFilters.BackColor = Color.White;
            pnlFilters.Controls.Add(lblShowLabel);
            pnlFilters.Controls.Add(cmbPageSize);
            pnlFilters.Controls.Add(lblEntriesLabel);
            pnlFilters.Controls.Add(lblSearchLabel);
            pnlFilters.Controls.Add(txtSearch);
            pnlFilters.Dock = DockStyle.Top;
            pnlFilters.Location = new Point(0, 167);
            pnlFilters.Margin = new Padding(4, 5, 4, 5);
            pnlFilters.Name = "pnlFilters";
            pnlFilters.Padding = new Padding(43, 25, 43, 25);
            pnlFilters.Size = new Size(1714, 100);
            pnlFilters.TabIndex = 1;
            // 
            // lblShowLabel
            // 
            lblShowLabel.AutoSize = true;
            lblShowLabel.Font = new Font("Segoe UI", 10F);
            lblShowLabel.Location = new Point(43, 37);
            lblShowLabel.Margin = new Padding(4, 0, 4, 0);
            lblShowLabel.Name = "lblShowLabel";
            lblShowLabel.Size = new Size(80, 28);
            lblShowLabel.TabIndex = 0;
            lblShowLabel.Text = "Hiển thị";
            // 
            // cmbPageSize
            // 
            cmbPageSize.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPageSize.Font = new Font("Segoe UI", 10F);
            cmbPageSize.FormattingEnabled = true;
            cmbPageSize.Items.AddRange(new object[] { "10", "25", "50", "100" });
            cmbPageSize.Location = new Point(143, 30);
            cmbPageSize.Margin = new Padding(4, 5, 4, 5);
            cmbPageSize.Name = "cmbPageSize";
            cmbPageSize.Size = new Size(98, 36);
            cmbPageSize.TabIndex = 1;
            cmbPageSize.SelectedIndexChanged += CmbPageSize_SelectedIndexChanged;
            // 
            // lblEntriesLabel
            // 
            lblEntriesLabel.AutoSize = true;
            lblEntriesLabel.Font = new Font("Segoe UI", 10F);
            lblEntriesLabel.Location = new Point(251, 37);
            lblEntriesLabel.Margin = new Padding(4, 0, 4, 0);
            lblEntriesLabel.Name = "lblEntriesLabel";
            lblEntriesLabel.Size = new Size(72, 28);
            lblEntriesLabel.TabIndex = 2;
            lblEntriesLabel.Text = "dữ liệu";
            // 
            // lblSearchLabel
            // 
            lblSearchLabel.AutoSize = true;
            lblSearchLabel.Font = new Font("Segoe UI", 10F);
            lblSearchLabel.Location = new Point(1071, 37);
            lblSearchLabel.Margin = new Padding(4, 0, 4, 0);
            lblSearchLabel.Name = "lblSearchLabel";
            lblSearchLabel.Size = new Size(95, 28);
            lblSearchLabel.TabIndex = 3;
            lblSearchLabel.Text = "Tìm kiếm:";
            // 
            // txtSearch
            // 
            txtSearch.Font = new Font("Segoe UI", 10F);
            txtSearch.Location = new Point(1193, 30);
            txtSearch.Margin = new Padding(4, 5, 4, 5);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(477, 34);
            txtSearch.TabIndex = 4;
            txtSearch.TextChanged += TxtSearch_TextChanged;
            // 
            // pnlTable
            // 
            pnlTable.BackColor = Color.White;
            pnlTable.Controls.Add(pnlTableHeader);
            pnlTable.Controls.Add(flowCourses);
            pnlTable.Dock = DockStyle.Fill;
            pnlTable.Location = new Point(0, 267);
            pnlTable.Margin = new Padding(4, 5, 4, 5);
            pnlTable.Name = "pnlTable";
            pnlTable.Padding = new Padding(43, 17, 43, 17);
            pnlTable.Size = new Size(1714, 733);
            pnlTable.TabIndex = 2;
            // 
            // pnlTableHeader
            // 
            pnlTableHeader.BackColor = Color.FromArgb(248, 249, 250);
            pnlTableHeader.BorderStyle = BorderStyle.FixedSingle;
            pnlTableHeader.Controls.Add(lblHeaderId);
            pnlTableHeader.Controls.Add(lblHeaderTitle);
            pnlTableHeader.Controls.Add(lblHeaderStatus);
            pnlTableHeader.Controls.Add(lblHeaderPrice);
            pnlTableHeader.Controls.Add(lblHeaderDate);
            pnlTableHeader.Controls.Add(lblHeaderActions);
            pnlTableHeader.Dock = DockStyle.Top;
            pnlTableHeader.Location = new Point(43, 17);
            pnlTableHeader.Margin = new Padding(4, 5, 4, 5);
            pnlTableHeader.Name = "pnlTableHeader";
            pnlTableHeader.Size = new Size(1628, 82);
            pnlTableHeader.TabIndex = 0;
            // 
            // lblHeaderId
            // 
            lblHeaderId.AutoSize = true;
            lblHeaderId.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblHeaderId.Location = new Point(20, 25);
            lblHeaderId.Name = "lblHeaderId";
            lblHeaderId.Size = new Size(33, 28);
            lblHeaderId.TabIndex = 0;
            lblHeaderId.Text = "ID";
            // 
            // lblHeaderTitle
            // 
            lblHeaderTitle.AutoSize = true;
            lblHeaderTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblHeaderTitle.Location = new Point(175, 25);
            lblHeaderTitle.Name = "lblHeaderTitle";
            lblHeaderTitle.Size = new Size(83, 28);
            lblHeaderTitle.TabIndex = 1;
            lblHeaderTitle.Text = "Tiêu đề";
            // 
            // lblHeaderStatus
            // 
            lblHeaderStatus.AutoSize = true;
            lblHeaderStatus.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblHeaderStatus.Location = new Point(629, 25);
            lblHeaderStatus.Name = "lblHeaderStatus";
            lblHeaderStatus.Size = new Size(108, 28);
            lblHeaderStatus.TabIndex = 2;
            lblHeaderStatus.Text = "Trạng thái";
            // 
            // lblHeaderPrice
            // 
            lblHeaderPrice.AutoSize = true;
            lblHeaderPrice.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblHeaderPrice.Location = new Point(792, 25);
            lblHeaderPrice.Name = "lblHeaderPrice";
            lblHeaderPrice.Size = new Size(43, 28);
            lblHeaderPrice.TabIndex = 3;
            lblHeaderPrice.Text = "Giá";
            // 
            // lblHeaderDate
            // 
            lblHeaderDate.AutoSize = true;
            lblHeaderDate.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblHeaderDate.Location = new Point(1017, 25);
            lblHeaderDate.Name = "lblHeaderDate";
            lblHeaderDate.Size = new Size(81, 28);
            lblHeaderDate.TabIndex = 4;
            lblHeaderDate.Text = "Tạo lúc";
            // 
            // lblHeaderActions
            // 
            lblHeaderActions.AutoSize = true;
            lblHeaderActions.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblHeaderActions.Location = new Point(1346, 25);
            lblHeaderActions.Name = "lblHeaderActions";
            lblHeaderActions.Size = new Size(117, 28);
            lblHeaderActions.TabIndex = 5;
            lblHeaderActions.Text = "Hành động";
            // 
            // flowCourses
            // 
            flowCourses.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            flowCourses.AutoScroll = true;
            flowCourses.FlowDirection = FlowDirection.TopDown;
            flowCourses.Location = new Point(43, 98);
            flowCourses.Margin = new Padding(4, 5, 4, 5);
            flowCourses.Name = "flowCourses";
            flowCourses.Size = new Size(1628, 618);
            flowCourses.TabIndex = 1;
            flowCourses.WrapContents = false;
            flowCourses.Paint += flowCourses_Paint;
            // 
            // pnlFooter
            // 
            pnlFooter.BackColor = Color.White;
            pnlFooter.Controls.Add(lblPageInfo);
            pnlFooter.Controls.Add(pnlPagination);
            pnlFooter.Dock = DockStyle.Bottom;
            pnlFooter.Location = new Point(0, 1000);
            pnlFooter.Margin = new Padding(4, 5, 4, 5);
            pnlFooter.Name = "pnlFooter";
            pnlFooter.Padding = new Padding(43, 25, 43, 25);
            pnlFooter.Size = new Size(1714, 117);
            pnlFooter.TabIndex = 3;
            // 
            // lblPageInfo
            // 
            lblPageInfo.AutoSize = true;
            lblPageInfo.Font = new Font("Segoe UI", 9F);
            lblPageInfo.Location = new Point(43, 45);
            lblPageInfo.Margin = new Padding(4, 0, 4, 0);
            lblPageInfo.Name = "lblPageInfo";
            lblPageInfo.Size = new Size(235, 25);
            lblPageInfo.TabIndex = 0;
            lblPageInfo.Text = "Hiển thị 1 tới 4 của 4 dữ liệu";
            // 
            // pnlPagination
            // 
            pnlPagination.Controls.Add(btnFirstPage);
            pnlPagination.Controls.Add(btnPrevPage);
            pnlPagination.Controls.Add(lblCurrentPage);
            pnlPagination.Controls.Add(btnNextPage);
            pnlPagination.Controls.Add(btnLastPage);
            pnlPagination.Dock = DockStyle.Right;
            pnlPagination.Location = new Point(1214, 25);
            pnlPagination.Margin = new Padding(4, 5, 4, 5);
            pnlPagination.Name = "pnlPagination";
            pnlPagination.Size = new Size(457, 67);
            pnlPagination.TabIndex = 1;
            // 
            // btnFirstPage
            // 
            btnFirstPage.BackColor = Color.FromArgb(108, 117, 125);
            btnFirstPage.Cursor = Cursors.Hand;
            btnFirstPage.FlatAppearance.BorderSize = 0;
            btnFirstPage.FlatStyle = FlatStyle.Flat;
            btnFirstPage.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnFirstPage.ForeColor = Color.White;
            btnFirstPage.Location = new Point(0, 8);
            btnFirstPage.Margin = new Padding(4, 5, 4, 5);
            btnFirstPage.Name = "btnFirstPage";
            btnFirstPage.Size = new Size(100, 50);
            btnFirstPage.TabIndex = 0;
            btnFirstPage.Text = "Đầu tiên";
            btnFirstPage.UseVisualStyleBackColor = false;
            btnFirstPage.Click += BtnFirstPage_Click;
            // 
            // btnPrevPage
            // 
            btnPrevPage.BackColor = Color.FromArgb(108, 117, 125);
            btnPrevPage.Cursor = Cursors.Hand;
            btnPrevPage.FlatAppearance.BorderSize = 0;
            btnPrevPage.FlatStyle = FlatStyle.Flat;
            btnPrevPage.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnPrevPage.ForeColor = Color.White;
            btnPrevPage.Location = new Point(107, 8);
            btnPrevPage.Margin = new Padding(4, 5, 4, 5);
            btnPrevPage.Name = "btnPrevPage";
            btnPrevPage.Size = new Size(86, 50);
            btnPrevPage.TabIndex = 1;
            btnPrevPage.Text = "Trước";
            btnPrevPage.UseVisualStyleBackColor = false;
            btnPrevPage.Click += BtnPrevPage_Click;
            // 
            // lblCurrentPage
            // 
            lblCurrentPage.BackColor = Color.FromArgb(52, 144, 220);
            lblCurrentPage.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblCurrentPage.ForeColor = Color.White;
            lblCurrentPage.Location = new Point(200, 8);
            lblCurrentPage.Margin = new Padding(4, 0, 4, 0);
            lblCurrentPage.Name = "lblCurrentPage";
            lblCurrentPage.Size = new Size(57, 50);
            lblCurrentPage.TabIndex = 2;
            lblCurrentPage.Text = "1";
            lblCurrentPage.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnNextPage
            // 
            btnNextPage.BackColor = Color.FromArgb(108, 117, 125);
            btnNextPage.Cursor = Cursors.Hand;
            btnNextPage.FlatAppearance.BorderSize = 0;
            btnNextPage.FlatStyle = FlatStyle.Flat;
            btnNextPage.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnNextPage.ForeColor = Color.White;
            btnNextPage.Location = new Point(264, 8);
            btnNextPage.Margin = new Padding(4, 5, 4, 5);
            btnNextPage.Name = "btnNextPage";
            btnNextPage.Size = new Size(86, 50);
            btnNextPage.TabIndex = 3;
            btnNextPage.Text = "Sau";
            btnNextPage.UseVisualStyleBackColor = false;
            btnNextPage.Click += BtnNextPage_Click;
            // 
            // btnLastPage
            // 
            btnLastPage.BackColor = Color.FromArgb(108, 117, 125);
            btnLastPage.Cursor = Cursors.Hand;
            btnLastPage.FlatAppearance.BorderSize = 0;
            btnLastPage.FlatStyle = FlatStyle.Flat;
            btnLastPage.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnLastPage.ForeColor = Color.White;
            btnLastPage.Location = new Point(357, 8);
            btnLastPage.Margin = new Padding(4, 5, 4, 5);
            btnLastPage.Name = "btnLastPage";
            btnLastPage.Size = new Size(86, 50);
            btnLastPage.TabIndex = 4;
            btnLastPage.Text = "Cuối cùng";
            btnLastPage.UseVisualStyleBackColor = false;
            btnLastPage.Click += BtnLastPage_Click;
            // 
            // MyCoursesControl
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 249, 250);
            Controls.Add(pnlTable);
            Controls.Add(pnlFooter);
            Controls.Add(pnlFilters);
            Controls.Add(pnlHeader);
            Margin = new Padding(4, 5, 4, 5);
            Name = "MyCoursesControl";
            Size = new Size(1714, 1117);
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlActions.ResumeLayout(false);
            pnlFilters.ResumeLayout(false);
            pnlFilters.PerformLayout();
            pnlTable.ResumeLayout(false);
            pnlTableHeader.ResumeLayout(false);
            pnlTableHeader.PerformLayout();
            pnlFooter.ResumeLayout(false);
            pnlFooter.PerformLayout();
            pnlPagination.ResumeLayout(false);
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlActions;
        private System.Windows.Forms.Button btnCreateCourse;
        private System.Windows.Forms.Button btnRevenue;
        private System.Windows.Forms.Button btnFlashcards;
        private System.Windows.Forms.Panel pnlFilters;
        private System.Windows.Forms.Label lblShowLabel;
        private System.Windows.Forms.ComboBox cmbPageSize;
        private System.Windows.Forms.Label lblEntriesLabel;
        private System.Windows.Forms.Label lblSearchLabel;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Panel pnlTable;
        private System.Windows.Forms.Panel pnlTableHeader;
        private System.Windows.Forms.Label lblHeaderId;
        private System.Windows.Forms.Label lblHeaderTitle;
        private System.Windows.Forms.Label lblHeaderStatus;
        private System.Windows.Forms.Label lblHeaderPrice;
        private System.Windows.Forms.Label lblHeaderDate;
        private System.Windows.Forms.Label lblHeaderActions;
        private System.Windows.Forms.FlowLayoutPanel flowCourses;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Label lblPageInfo;
        private System.Windows.Forms.Panel pnlPagination;
        private System.Windows.Forms.Button btnFirstPage;
        private System.Windows.Forms.Button btnPrevPage;
        private System.Windows.Forms.Label lblCurrentPage;
        private System.Windows.Forms.Button btnNextPage;
        private System.Windows.Forms.Button btnLastPage;
    }
}