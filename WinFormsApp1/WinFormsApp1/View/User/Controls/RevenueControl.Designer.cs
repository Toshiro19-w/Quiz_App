namespace WinFormsApp1.View.User.Controls
{
    partial class RevenueControl
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
            btnBack = new Button();
            pnlOverview = new Panel();
            cardPurchases = new Panel();
            lblTotalPurchases = new Label();
            lblPurchasesLabel = new Label();
            iconPurchases = new Label();
            cardGrossRevenue = new Panel();
            lblTotalGrossRevenue = new Label();
            lblGrossRevenueLabel = new Label();
            iconGrossRevenue = new Label();
            cardInstructorRevenue = new Panel();
            lblInstructorRevenue = new Label();
            lblInstructorRevenueLabel = new Label();
            iconInstructorRevenue = new Label();
            cardPlatformFee = new Panel();
            lblPlatformFee = new Label();
            lblPlatformFeeLabel = new Label();
            iconPlatformFee = new Label();
            pnlFilters = new Panel();
            lblShowLabel = new Label();
            cmbPageSize = new ComboBox();
            lblEntriesLabel = new Label();
            lblSearchLabel = new Label();
            txtSearch = new TextBox();
            pnlTable = new Panel();
            flowRevenues = new FlowLayoutPanel();
            pnlTableHeader = new Panel();
            lblHeaderCourse = new Label();
            lblHeaderPrice = new Label();
            lblHeaderPurchases = new Label();
            lblHeaderGrossRevenue = new Label();
            lblHeaderInstructorRevenue = new Label();
            lblHeaderPlatformFee = new Label();
            pnlFooter = new Panel();
            lblPageInfo = new Label();
            pnlPagination = new Panel();
            btnFirstPage = new Button();
            btnPrevPage = new Button();
            lblCurrentPage = new Label();
            btnNextPage = new Button();
            btnLastPage = new Button();
            pnlHeader.SuspendLayout();
            pnlOverview.SuspendLayout();
            cardPurchases.SuspendLayout();
            cardGrossRevenue.SuspendLayout();
            cardInstructorRevenue.SuspendLayout();
            cardPlatformFee.SuspendLayout();
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
            pnlHeader.Controls.Add(btnBack);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Margin = new Padding(4, 5, 4, 5);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Padding = new Padding(43, 33, 43, 33);
            pnlHeader.Size = new Size(2000, 133);
            pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitle.Location = new Point(43, 42);
            lblTitle.Margin = new Padding(4, 0, 4, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(422, 48);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "📊 Thống kê doanh thu";
            // 
            // btnBack
            // 
            btnBack.BackColor = Color.FromArgb(108, 117, 125);
            btnBack.Cursor = Cursors.Hand;
            btnBack.FlatAppearance.BorderSize = 0;
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnBack.ForeColor = Color.White;
            btnBack.Location = new Point(2200, 31);
            btnBack.Margin = new Padding(4, 5, 4, 5);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(243, 83);
            btnBack.TabIndex = 1;
            btnBack.Text = "⬅️ Quay lại";
            btnBack.UseVisualStyleBackColor = false;
            btnBack.Click += BtnBack_Click;
            // 
            // pnlOverview
            // 
            pnlOverview.BackColor = Color.FromArgb(248, 249, 250);
            pnlOverview.Controls.Add(cardPurchases);
            pnlOverview.Controls.Add(cardGrossRevenue);
            pnlOverview.Controls.Add(cardInstructorRevenue);
            pnlOverview.Controls.Add(cardPlatformFee);
            pnlOverview.Dock = DockStyle.Top;
            pnlOverview.Location = new Point(0, 133);
            pnlOverview.Margin = new Padding(4, 5, 4, 5);
            pnlOverview.Name = "pnlOverview";
            pnlOverview.Padding = new Padding(43, 33, 43, 33);
            pnlOverview.Size = new Size(2000, 233);
            pnlOverview.TabIndex = 1;
            // 
            // cardPurchases
            // 
            cardPurchases.BackColor = Color.White;
            cardPurchases.BorderStyle = BorderStyle.FixedSingle;
            cardPurchases.Controls.Add(lblTotalPurchases);
            cardPurchases.Controls.Add(lblPurchasesLabel);
            cardPurchases.Controls.Add(iconPurchases);
            cardPurchases.Location = new Point(43, 33);
            cardPurchases.Margin = new Padding(4, 5, 4, 5);
            cardPurchases.Name = "cardPurchases";
            cardPurchases.Size = new Size(442, 165);
            cardPurchases.TabIndex = 0;
            // 
            // lblTotalPurchases
            // 
            lblTotalPurchases.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblTotalPurchases.ForeColor = Color.FromArgb(52, 144, 220);
            lblTotalPurchases.Location = new Point(114, 25);
            lblTotalPurchases.Margin = new Padding(4, 0, 4, 0);
            lblTotalPurchases.Name = "lblTotalPurchases";
            lblTotalPurchases.Size = new Size(314, 67);
            lblTotalPurchases.TabIndex = 0;
            lblTotalPurchases.Text = "0";
            lblTotalPurchases.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblPurchasesLabel
            // 
            lblPurchasesLabel.Font = new Font("Segoe UI", 10F);
            lblPurchasesLabel.ForeColor = Color.Gray;
            lblPurchasesLabel.Location = new Point(114, 100);
            lblPurchasesLabel.Margin = new Padding(4, 0, 4, 0);
            lblPurchasesLabel.Name = "lblPurchasesLabel";
            lblPurchasesLabel.Size = new Size(314, 42);
            lblPurchasesLabel.TabIndex = 1;
            lblPurchasesLabel.Text = "Tổng số lượt mua";
            // 
            // iconPurchases
            // 
            iconPurchases.Font = new Font("Segoe UI", 32F);
            iconPurchases.Location = new Point(21, 42);
            iconPurchases.Margin = new Padding(4, 0, 4, 0);
            iconPurchases.Name = "iconPurchases";
            iconPurchases.Size = new Size(86, 100);
            iconPurchases.TabIndex = 2;
            iconPurchases.Text = "\U0001f6d2";
            iconPurchases.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // cardGrossRevenue
            // 
            cardGrossRevenue.BackColor = Color.White;
            cardGrossRevenue.BorderStyle = BorderStyle.FixedSingle;
            cardGrossRevenue.Controls.Add(lblTotalGrossRevenue);
            cardGrossRevenue.Controls.Add(lblGrossRevenueLabel);
            cardGrossRevenue.Controls.Add(iconGrossRevenue);
            cardGrossRevenue.Location = new Point(514, 33);
            cardGrossRevenue.Margin = new Padding(4, 5, 4, 5);
            cardGrossRevenue.Name = "cardGrossRevenue";
            cardGrossRevenue.Size = new Size(442, 165);
            cardGrossRevenue.TabIndex = 1;
            // 
            // lblTotalGrossRevenue
            // 
            lblTotalGrossRevenue.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblTotalGrossRevenue.ForeColor = Color.FromArgb(23, 162, 184);
            lblTotalGrossRevenue.Location = new Point(114, 33);
            lblTotalGrossRevenue.Margin = new Padding(4, 0, 4, 0);
            lblTotalGrossRevenue.Name = "lblTotalGrossRevenue";
            lblTotalGrossRevenue.Size = new Size(314, 58);
            lblTotalGrossRevenue.TabIndex = 0;
            lblTotalGrossRevenue.Text = "0 VNĐ";
            lblTotalGrossRevenue.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblGrossRevenueLabel
            // 
            lblGrossRevenueLabel.Font = new Font("Segoe UI", 10F);
            lblGrossRevenueLabel.ForeColor = Color.Gray;
            lblGrossRevenueLabel.Location = new Point(114, 100);
            lblGrossRevenueLabel.Margin = new Padding(4, 0, 4, 0);
            lblGrossRevenueLabel.Name = "lblGrossRevenueLabel";
            lblGrossRevenueLabel.Size = new Size(314, 42);
            lblGrossRevenueLabel.TabIndex = 1;
            lblGrossRevenueLabel.Text = "Tổng doanh thu";
            // 
            // iconGrossRevenue
            // 
            iconGrossRevenue.Font = new Font("Segoe UI", 32F);
            iconGrossRevenue.Location = new Point(21, 42);
            iconGrossRevenue.Margin = new Padding(4, 0, 4, 0);
            iconGrossRevenue.Name = "iconGrossRevenue";
            iconGrossRevenue.Size = new Size(86, 100);
            iconGrossRevenue.TabIndex = 2;
            iconGrossRevenue.Text = "💰";
            iconGrossRevenue.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // cardInstructorRevenue
            // 
            cardInstructorRevenue.BackColor = Color.White;
            cardInstructorRevenue.BorderStyle = BorderStyle.FixedSingle;
            cardInstructorRevenue.Controls.Add(lblInstructorRevenue);
            cardInstructorRevenue.Controls.Add(lblInstructorRevenueLabel);
            cardInstructorRevenue.Controls.Add(iconInstructorRevenue);
            cardInstructorRevenue.Location = new Point(986, 33);
            cardInstructorRevenue.Margin = new Padding(4, 5, 4, 5);
            cardInstructorRevenue.Name = "cardInstructorRevenue";
            cardInstructorRevenue.Size = new Size(442, 165);
            cardInstructorRevenue.TabIndex = 2;
            // 
            // lblInstructorRevenue
            // 
            lblInstructorRevenue.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblInstructorRevenue.ForeColor = Color.FromArgb(40, 167, 69);
            lblInstructorRevenue.Location = new Point(114, 33);
            lblInstructorRevenue.Margin = new Padding(4, 0, 4, 0);
            lblInstructorRevenue.Name = "lblInstructorRevenue";
            lblInstructorRevenue.Size = new Size(314, 58);
            lblInstructorRevenue.TabIndex = 0;
            lblInstructorRevenue.Text = "0 VNĐ";
            lblInstructorRevenue.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblInstructorRevenueLabel
            // 
            lblInstructorRevenueLabel.Font = new Font("Segoe UI", 10F);
            lblInstructorRevenueLabel.ForeColor = Color.Gray;
            lblInstructorRevenueLabel.Location = new Point(114, 100);
            lblInstructorRevenueLabel.Margin = new Padding(4, 0, 4, 0);
            lblInstructorRevenueLabel.Name = "lblInstructorRevenueLabel";
            lblInstructorRevenueLabel.Size = new Size(314, 42);
            lblInstructorRevenueLabel.TabIndex = 1;
            lblInstructorRevenueLabel.Text = "Thu nhập của bạn (60%)";
            // 
            // iconInstructorRevenue
            // 
            iconInstructorRevenue.Font = new Font("Segoe UI", 32F);
            iconInstructorRevenue.Location = new Point(21, 42);
            iconInstructorRevenue.Margin = new Padding(4, 0, 4, 0);
            iconInstructorRevenue.Name = "iconInstructorRevenue";
            iconInstructorRevenue.Size = new Size(86, 100);
            iconInstructorRevenue.TabIndex = 2;
            iconInstructorRevenue.Text = "💵";
            iconInstructorRevenue.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // cardPlatformFee
            // 
            cardPlatformFee.BackColor = Color.White;
            cardPlatformFee.BorderStyle = BorderStyle.FixedSingle;
            cardPlatformFee.Controls.Add(lblPlatformFee);
            cardPlatformFee.Controls.Add(lblPlatformFeeLabel);
            cardPlatformFee.Controls.Add(iconPlatformFee);
            cardPlatformFee.Location = new Point(1457, 33);
            cardPlatformFee.Margin = new Padding(4, 5, 4, 5);
            cardPlatformFee.Name = "cardPlatformFee";
            cardPlatformFee.Size = new Size(442, 165);
            cardPlatformFee.TabIndex = 3;
            // 
            // lblPlatformFee
            // 
            lblPlatformFee.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblPlatformFee.ForeColor = Color.FromArgb(255, 193, 7);
            lblPlatformFee.Location = new Point(114, 33);
            lblPlatformFee.Margin = new Padding(4, 0, 4, 0);
            lblPlatformFee.Name = "lblPlatformFee";
            lblPlatformFee.Size = new Size(314, 58);
            lblPlatformFee.TabIndex = 0;
            lblPlatformFee.Text = "0 VNĐ";
            lblPlatformFee.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblPlatformFeeLabel
            // 
            lblPlatformFeeLabel.Font = new Font("Segoe UI", 10F);
            lblPlatformFeeLabel.ForeColor = Color.Gray;
            lblPlatformFeeLabel.Location = new Point(114, 100);
            lblPlatformFeeLabel.Margin = new Padding(4, 0, 4, 0);
            lblPlatformFeeLabel.Name = "lblPlatformFeeLabel";
            lblPlatformFeeLabel.Size = new Size(314, 42);
            lblPlatformFeeLabel.TabIndex = 1;
            lblPlatformFeeLabel.Text = "Phí nền tảng (40%)";
            // 
            // iconPlatformFee
            // 
            iconPlatformFee.Font = new Font("Segoe UI", 32F);
            iconPlatformFee.Location = new Point(21, 42);
            iconPlatformFee.Margin = new Padding(4, 0, 4, 0);
            iconPlatformFee.Name = "iconPlatformFee";
            iconPlatformFee.Size = new Size(86, 100);
            iconPlatformFee.TabIndex = 2;
            iconPlatformFee.Text = "\U0001f9ee";
            iconPlatformFee.TextAlign = ContentAlignment.MiddleCenter;
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
            pnlFilters.Location = new Point(0, 366);
            pnlFilters.Margin = new Padding(4, 5, 4, 5);
            pnlFilters.Name = "pnlFilters";
            pnlFilters.Padding = new Padding(43, 25, 43, 25);
            pnlFilters.Size = new Size(2000, 117);
            pnlFilters.TabIndex = 2;
            // 
            // lblShowLabel
            // 
            lblShowLabel.AutoSize = true;
            lblShowLabel.Font = new Font("Segoe UI", 10F);
            lblShowLabel.Location = new Point(43, 42);
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
            cmbPageSize.Location = new Point(143, 33);
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
            lblEntriesLabel.Location = new Point(257, 42);
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
            lblSearchLabel.Location = new Point(1286, 42);
            lblSearchLabel.Margin = new Padding(4, 0, 4, 0);
            lblSearchLabel.Name = "lblSearchLabel";
            lblSearchLabel.Size = new Size(95, 28);
            lblSearchLabel.TabIndex = 3;
            lblSearchLabel.Text = "Tìm kiếm:";
            // 
            // txtSearch
            // 
            txtSearch.Font = new Font("Segoe UI", 10F);
            txtSearch.Location = new Point(1407, 33);
            txtSearch.Margin = new Padding(4, 5, 4, 5);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Nhập tìm kiếm...";
            txtSearch.Size = new Size(548, 34);
            txtSearch.TabIndex = 4;
            txtSearch.TextChanged += TxtSearch_TextChanged;
            // 
            // pnlTable
            // 
            pnlTable.BackColor = Color.White;
            pnlTable.Controls.Add(flowRevenues);
            pnlTable.Controls.Add(pnlTableHeader);
            pnlTable.Dock = DockStyle.Fill;
            pnlTable.Location = new Point(0, 483);
            pnlTable.Margin = new Padding(4, 5, 4, 5);
            pnlTable.Name = "pnlTable";
            pnlTable.Padding = new Padding(43, 17, 43, 17);
            pnlTable.Size = new Size(2000, 834);
            pnlTable.TabIndex = 3;
            // 
            // flowRevenues
            // 
            flowRevenues.AutoScroll = true;
            flowRevenues.Dock = DockStyle.Fill;
            flowRevenues.FlowDirection = FlowDirection.TopDown;
            flowRevenues.Location = new Point(43, 116);
            flowRevenues.Margin = new Padding(4, 5, 4, 5);
            flowRevenues.Name = "flowRevenues";
            flowRevenues.Size = new Size(1914, 701);
            flowRevenues.TabIndex = 1;
            flowRevenues.WrapContents = false;
            // 
            // pnlTableHeader
            // 
            pnlTableHeader.BackColor = Color.FromArgb(248, 249, 250);
            pnlTableHeader.BorderStyle = BorderStyle.FixedSingle;
            pnlTableHeader.Controls.Add(lblHeaderCourse);
            pnlTableHeader.Controls.Add(lblHeaderPrice);
            pnlTableHeader.Controls.Add(lblHeaderPurchases);
            pnlTableHeader.Controls.Add(lblHeaderGrossRevenue);
            pnlTableHeader.Controls.Add(lblHeaderInstructorRevenue);
            pnlTableHeader.Controls.Add(lblHeaderPlatformFee);
            pnlTableHeader.Dock = DockStyle.Top;
            pnlTableHeader.Location = new Point(43, 17);
            pnlTableHeader.Margin = new Padding(4, 5, 4, 5);
            pnlTableHeader.Name = "pnlTableHeader";
            pnlTableHeader.Size = new Size(1914, 99);
            pnlTableHeader.TabIndex = 0;
            // 
            // lblHeaderCourse
            // 
            lblHeaderCourse.AutoSize = true;
            lblHeaderCourse.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblHeaderCourse.Location = new Point(29, 33);
            lblHeaderCourse.Margin = new Padding(4, 0, 4, 0);
            lblHeaderCourse.Name = "lblHeaderCourse";
            lblHeaderCourse.Size = new Size(100, 28);
            lblHeaderCourse.TabIndex = 0;
            lblHeaderCourse.Text = "Khóa học";
            // 
            // lblHeaderPrice
            // 
            lblHeaderPrice.AutoSize = true;
            lblHeaderPrice.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblHeaderPrice.Location = new Point(664, 33);
            lblHeaderPrice.Margin = new Padding(4, 0, 4, 0);
            lblHeaderPrice.Name = "lblHeaderPrice";
            lblHeaderPrice.Size = new Size(43, 28);
            lblHeaderPrice.TabIndex = 1;
            lblHeaderPrice.Text = "Giá";
            // 
            // lblHeaderPurchases
            // 
            lblHeaderPurchases.AutoSize = true;
            lblHeaderPurchases.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblHeaderPurchases.Location = new Point(857, 33);
            lblHeaderPurchases.Margin = new Padding(4, 0, 4, 0);
            lblHeaderPurchases.Name = "lblHeaderPurchases";
            lblHeaderPurchases.Size = new Size(128, 28);
            lblHeaderPurchases.TabIndex = 2;
            lblHeaderPurchases.Text = "Số lượt mua";
            // 
            // lblHeaderGrossRevenue
            // 
            lblHeaderGrossRevenue.AutoSize = true;
            lblHeaderGrossRevenue.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblHeaderGrossRevenue.Location = new Point(1043, 33);
            lblHeaderGrossRevenue.Margin = new Padding(4, 0, 4, 0);
            lblHeaderGrossRevenue.Name = "lblHeaderGrossRevenue";
            lblHeaderGrossRevenue.Size = new Size(163, 28);
            lblHeaderGrossRevenue.TabIndex = 3;
            lblHeaderGrossRevenue.Text = "Tổng doanh thu";
            // 
            // lblHeaderInstructorRevenue
            // 
            lblHeaderInstructorRevenue.AutoSize = true;
            lblHeaderInstructorRevenue.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblHeaderInstructorRevenue.Location = new Point(1357, 33);
            lblHeaderInstructorRevenue.Margin = new Padding(4, 0, 4, 0);
            lblHeaderInstructorRevenue.Name = "lblHeaderInstructorRevenue";
            lblHeaderInstructorRevenue.Size = new Size(162, 28);
            lblHeaderInstructorRevenue.TabIndex = 4;
            lblHeaderInstructorRevenue.Text = "Thu nhập (60%)";
            // 
            // lblHeaderPlatformFee
            // 
            lblHeaderPlatformFee.AutoSize = true;
            lblHeaderPlatformFee.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblHeaderPlatformFee.Location = new Point(1629, 33);
            lblHeaderPlatformFee.Margin = new Padding(4, 0, 4, 0);
            lblHeaderPlatformFee.Name = "lblHeaderPlatformFee";
            lblHeaderPlatformFee.Size = new Size(193, 28);
            lblHeaderPlatformFee.TabIndex = 5;
            lblHeaderPlatformFee.Text = "Phí nền tảng (40%)";
            // 
            // pnlFooter
            // 
            pnlFooter.BackColor = Color.White;
            pnlFooter.Controls.Add(lblPageInfo);
            pnlFooter.Controls.Add(pnlPagination);
            pnlFooter.Dock = DockStyle.Bottom;
            pnlFooter.Location = new Point(0, 1317);
            pnlFooter.Margin = new Padding(4, 5, 4, 5);
            pnlFooter.Name = "pnlFooter";
            pnlFooter.Padding = new Padding(43, 25, 43, 25);
            pnlFooter.Size = new Size(2000, 133);
            pnlFooter.TabIndex = 4;
            // 
            // lblPageInfo
            // 
            lblPageInfo.AutoSize = true;
            lblPageInfo.Font = new Font("Segoe UI", 9F);
            lblPageInfo.Location = new Point(43, 50);
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
            pnlPagination.Location = new Point(1357, 25);
            pnlPagination.Margin = new Padding(4, 5, 4, 5);
            pnlPagination.Name = "pnlPagination";
            pnlPagination.Size = new Size(600, 83);
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
            btnFirstPage.Size = new Size(129, 67);
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
            btnPrevPage.Location = new Point(143, 8);
            btnPrevPage.Margin = new Padding(4, 5, 4, 5);
            btnPrevPage.Name = "btnPrevPage";
            btnPrevPage.Size = new Size(100, 67);
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
            lblCurrentPage.Location = new Point(257, 8);
            lblCurrentPage.Margin = new Padding(4, 0, 4, 0);
            lblCurrentPage.Name = "lblCurrentPage";
            lblCurrentPage.Size = new Size(71, 67);
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
            btnNextPage.Location = new Point(343, 8);
            btnNextPage.Margin = new Padding(4, 5, 4, 5);
            btnNextPage.Name = "btnNextPage";
            btnNextPage.Size = new Size(100, 67);
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
            btnLastPage.Location = new Point(457, 8);
            btnLastPage.Margin = new Padding(4, 5, 4, 5);
            btnLastPage.Name = "btnLastPage";
            btnLastPage.Size = new Size(129, 67);
            btnLastPage.TabIndex = 4;
            btnLastPage.Text = "Cuối cùng";
            btnLastPage.UseVisualStyleBackColor = false;
            btnLastPage.Click += BtnLastPage_Click;
            // 
            // RevenueControl
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 249, 250);
            Controls.Add(pnlTable);
            Controls.Add(pnlFooter);
            Controls.Add(pnlFilters);
            Controls.Add(pnlOverview);
            Controls.Add(pnlHeader);
            Margin = new Padding(4, 5, 4, 5);
            Name = "RevenueControl";
            Size = new Size(2000, 1450);
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlOverview.ResumeLayout(false);
            cardPurchases.ResumeLayout(false);
            cardGrossRevenue.ResumeLayout(false);
            cardInstructorRevenue.ResumeLayout(false);
            cardPlatformFee.ResumeLayout(false);
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
        private Button btnBack;
        private Panel pnlOverview;
        private Panel cardPurchases;
        private Label lblTotalPurchases;
        private Label lblPurchasesLabel;
        private Label iconPurchases;
        private Panel cardGrossRevenue;
        private Label lblTotalGrossRevenue;
        private Label lblGrossRevenueLabel;
        private Label iconGrossRevenue;
        private Panel cardInstructorRevenue;
        private Label lblInstructorRevenue;
        private Label lblInstructorRevenueLabel;
        private Label iconInstructorRevenue;
        private Panel cardPlatformFee;
        private Label lblPlatformFee;
        private Label lblPlatformFeeLabel;
        private Label iconPlatformFee;
        private Panel pnlFilters;
        private Label lblShowLabel;
        private ComboBox cmbPageSize;
        private Label lblEntriesLabel;
        private Label lblSearchLabel;
        private TextBox txtSearch;
        private Panel pnlTable;
        private FlowLayoutPanel flowRevenues;
        private Panel pnlTableHeader;
        private Label lblHeaderCourse;
        private Label lblHeaderPrice;
        private Label lblHeaderPurchases;
        private Label lblHeaderGrossRevenue;
        private Label lblHeaderInstructorRevenue;
        private Label lblHeaderPlatformFee;
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
