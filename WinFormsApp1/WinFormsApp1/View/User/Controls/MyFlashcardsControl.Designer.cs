namespace WinFormsApp1.View.User.Controls
{
    partial class MyFlashcardsControl
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
            btnCreateFlashcard = new Button();
            btnBack = new Button();
            pnlFilters = new Panel();
            lblShowLabel = new Label();
            cmbPageSize = new ComboBox();
            lblEntriesLabel = new Label();
            lblSearchLabel = new Label();
            txtSearch = new TextBox();
            pnlTable = new Panel();
            flowFlashcards = new FlowLayoutPanel();
            pnlTableHeader = new Panel();
            lblHeaderId = new Label();
            lblHeaderTitle = new Label();
            lblHeaderCardCount = new Label();
            lblHeaderVisibility = new Label();
            lblHeaderLanguage = new Label();
            lblHeaderDate = new Label();
            lblHeaderActions = new Label();
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
            lblTitle.Size = new Size(400, 54);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Bộ Flashcard của tôi";
            // 
            // pnlActions
            // 
            pnlActions.Controls.Add(btnCreateFlashcard);
            pnlActions.Controls.Add(btnBack);
            pnlActions.Dock = DockStyle.Right;
            pnlActions.Location = new Point(1214, 33);
            pnlActions.Margin = new Padding(4, 5, 4, 5);
            pnlActions.Name = "pnlActions";
            pnlActions.Size = new Size(457, 101);
            pnlActions.TabIndex = 1;
            // 
            // btnCreateFlashcard
            // 
            btnCreateFlashcard.BackColor = Color.FromArgb(23, 162, 184);
            btnCreateFlashcard.Cursor = Cursors.Hand;
            btnCreateFlashcard.FlatAppearance.BorderSize = 0;
            btnCreateFlashcard.FlatStyle = FlatStyle.Flat;
            btnCreateFlashcard.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCreateFlashcard.ForeColor = Color.White;
            btnCreateFlashcard.Location = new Point(14, 17);
            btnCreateFlashcard.Margin = new Padding(4, 5, 4, 5);
            btnCreateFlashcard.Name = "btnCreateFlashcard";
            btnCreateFlashcard.Size = new Size(214, 67);
            btnCreateFlashcard.TabIndex = 0;
            btnCreateFlashcard.Text = "➕ Tạo bộ Flashcard";
            btnCreateFlashcard.UseVisualStyleBackColor = false;
            btnCreateFlashcard.Click += BtnCreateFlashcard_Click;
            // 
            // btnBack
            // 
            btnBack.BackColor = Color.FromArgb(108, 117, 125);
            btnBack.Cursor = Cursors.Hand;
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnBack.ForeColor = Color.White;
            btnBack.Location = new Point(243, 17);
            btnBack.Margin = new Padding(4, 5, 4, 5);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(200, 67);
            btnBack.TabIndex = 1;
            btnBack.Text = "⬅️ Quay lại";
            btnBack.UseVisualStyleBackColor = false;
            btnBack.Click += BtnBack_Click;
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
            txtSearch.PlaceholderText = "Nhập tìm kiếm...";
            txtSearch.Size = new Size(477, 34);
            txtSearch.TabIndex = 4;
            txtSearch.TextChanged += TxtSearch_TextChanged;
            // 
            // pnlTable
            // 
            pnlTable.BackColor = Color.White;
            pnlTable.Controls.Add(flowFlashcards);
            pnlTable.Controls.Add(pnlTableHeader);
            pnlTable.Dock = DockStyle.Fill;
            pnlTable.Location = new Point(0, 267);
            pnlTable.Margin = new Padding(4, 5, 4, 5);
            pnlTable.Name = "pnlTable";
            pnlTable.Padding = new Padding(43, 17, 43, 17);
            pnlTable.Size = new Size(1714, 733);
            pnlTable.TabIndex = 2;
            // 
            // flowFlashcards
            // 
            flowFlashcards.AutoScroll = true;
            flowFlashcards.Dock = DockStyle.Fill;
            flowFlashcards.FlowDirection = FlowDirection.TopDown;
            flowFlashcards.Location = new Point(43, 99);
            flowFlashcards.Margin = new Padding(4, 5, 4, 5);
            flowFlashcards.Name = "flowFlashcards";
            flowFlashcards.Size = new Size(1628, 617);
            flowFlashcards.TabIndex = 1;
            flowFlashcards.WrapContents = false;
            flowFlashcards.Paint += flowFlashcards_Paint;
            // 
            // pnlTableHeader
            // 
            pnlTableHeader.BackColor = Color.FromArgb(248, 249, 250);
            pnlTableHeader.BorderStyle = BorderStyle.FixedSingle;
            pnlTableHeader.Controls.Add(lblHeaderId);
            pnlTableHeader.Controls.Add(lblHeaderTitle);
            pnlTableHeader.Controls.Add(lblHeaderCardCount);
            pnlTableHeader.Controls.Add(lblHeaderVisibility);
            pnlTableHeader.Controls.Add(lblHeaderLanguage);
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
            lblHeaderId.Location = new Point(20, 27);
            lblHeaderId.Margin = new Padding(4, 0, 4, 0);
            lblHeaderId.Name = "lblHeaderId";
            lblHeaderId.Size = new Size(33, 28);
            lblHeaderId.TabIndex = 0;
            lblHeaderId.Text = "ID";
            // 
            // lblHeaderTitle
            // 
            lblHeaderTitle.AutoSize = true;
            lblHeaderTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblHeaderTitle.Location = new Point(175, 27);
            lblHeaderTitle.Margin = new Padding(4, 0, 4, 0);
            lblHeaderTitle.Name = "lblHeaderTitle";
            lblHeaderTitle.Size = new Size(83, 28);
            lblHeaderTitle.TabIndex = 1;
            lblHeaderTitle.Text = "Tiêu đề";
            // 
            // lblHeaderCardCount
            // 
            lblHeaderCardCount.AutoSize = true;
            lblHeaderCardCount.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblHeaderCardCount.Location = new Point(629, 27);
            lblHeaderCardCount.Margin = new Padding(4, 0, 4, 0);
            lblHeaderCardCount.Name = "lblHeaderCardCount";
            lblHeaderCardCount.Size = new Size(72, 28);
            lblHeaderCardCount.TabIndex = 2;
            lblHeaderCardCount.Text = "Số thẻ";
            // 
            // lblHeaderVisibility
            // 
            lblHeaderVisibility.AutoSize = true;
            lblHeaderVisibility.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblHeaderVisibility.Location = new Point(765, 27);
            lblHeaderVisibility.Margin = new Padding(4, 0, 4, 0);
            lblHeaderVisibility.Name = "lblHeaderVisibility";
            lblHeaderVisibility.Size = new Size(88, 28);
            lblHeaderVisibility.TabIndex = 3;
            lblHeaderVisibility.Text = "Hiển thị";
            // 
            // lblHeaderLanguage
            // 
            lblHeaderLanguage.AutoSize = true;
            lblHeaderLanguage.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblHeaderLanguage.Location = new Point(899, 27);
            lblHeaderLanguage.Margin = new Padding(4, 0, 4, 0);
            lblHeaderLanguage.Name = "lblHeaderLanguage";
            lblHeaderLanguage.Size = new Size(107, 28);
            lblHeaderLanguage.TabIndex = 4;
            lblHeaderLanguage.Text = "Ngôn ngữ";
            // 
            // lblHeaderDate
            // 
            lblHeaderDate.AutoSize = true;
            lblHeaderDate.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblHeaderDate.Location = new Point(1077, 27);
            lblHeaderDate.Margin = new Padding(4, 0, 4, 0);
            lblHeaderDate.Name = "lblHeaderDate";
            lblHeaderDate.Size = new Size(81, 28);
            lblHeaderDate.TabIndex = 5;
            lblHeaderDate.Text = "Tạo lúc";
            // 
            // lblHeaderActions
            // 
            lblHeaderActions.AutoSize = true;
            lblHeaderActions.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblHeaderActions.Location = new Point(1434, 27);
            lblHeaderActions.Margin = new Padding(4, 0, 4, 0);
            lblHeaderActions.Name = "lblHeaderActions";
            lblHeaderActions.Size = new Size(117, 28);
            lblHeaderActions.TabIndex = 6;
            lblHeaderActions.Text = "Hành động";
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
            lblPageInfo.Text = "Hiển thị 1 tới 1 của 1 dữ liệu";
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
            // MyFlashcardsControl
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 249, 250);
            Controls.Add(pnlTable);
            Controls.Add(pnlFooter);
            Controls.Add(pnlFilters);
            Controls.Add(pnlHeader);
            Margin = new Padding(4, 5, 4, 5);
            Name = "MyFlashcardsControl";
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

        private Panel pnlHeader;
        private Label lblTitle;
        private Panel pnlActions;
        private Button btnCreateFlashcard;
        private Button btnBack;
        private Panel pnlFilters;
        private Label lblShowLabel;
        private ComboBox cmbPageSize;
        private Label lblEntriesLabel;
        private Label lblSearchLabel;
        private TextBox txtSearch;
        private Panel pnlTable;
        private Panel pnlTableHeader;
        private Label lblHeaderId;
        private Label lblHeaderTitle;
        private Label lblHeaderCardCount;
        private Label lblHeaderVisibility;
        private Label lblHeaderLanguage;
        private Label lblHeaderDate;
        private Label lblHeaderActions;
        private FlowLayoutPanel flowFlashcards;
        private Panel pnlFooter;
        private Label lblPageInfo;
        private Panel pnlPagination;
        private Button btnFirstPage;
        private Button btnPrevPage;
        private Label lblCurrentPage;
        private Button btnNextPage;
        private Button btnLastPage;
    }
}
