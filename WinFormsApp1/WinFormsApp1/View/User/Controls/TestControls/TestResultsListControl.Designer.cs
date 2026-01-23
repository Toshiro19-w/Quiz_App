namespace WinFormsApp1.View.User.Controls.TestControls
{
    partial class TestResultsListControl
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
			pnlScrollWrapper = new Panel();
			pnlMain = new Panel();
			pnlContent = new Panel();
			dgvResults = new DataGridView();
			pnlFilters = new Panel();
			cboSortBy = new ComboBox();
			lblSortBy = new Label();
			txtSearch = new TextBox();
			lblSearch = new Label();
			pnlHeader = new Panel();
			lblTitle = new Label();
			lblStats = new Label();
			btnBack = new Button();
			btnExport = new Button();
			pnlScrollWrapper.SuspendLayout();
			pnlMain.SuspendLayout();
			pnlContent.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dgvResults).BeginInit();
			pnlFilters.SuspendLayout();
			pnlHeader.SuspendLayout();
			SuspendLayout();
			// 
			// pnlScrollWrapper
			// 
			pnlScrollWrapper.AutoScroll = true;
			pnlScrollWrapper.BackColor = Color.FromArgb(248, 249, 250);
			pnlScrollWrapper.Controls.Add(pnlMain);
			pnlScrollWrapper.Dock = DockStyle.Fill;
			pnlScrollWrapper.Location = new Point(0, 0);
			pnlScrollWrapper.Margin = new Padding(4, 5, 4, 5);
			pnlScrollWrapper.Name = "pnlScrollWrapper";
			pnlScrollWrapper.Size = new Size(1714, 1167);
			pnlScrollWrapper.TabIndex = 0;
			// 
			// pnlMain
			// 
			pnlMain.AutoSize = true;
			pnlMain.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			pnlMain.Controls.Add(pnlContent);
			pnlMain.Controls.Add(pnlFilters);
			pnlMain.Controls.Add(pnlHeader);
			pnlMain.Dock = DockStyle.Top;
			pnlMain.Location = new Point(0, 0);
			pnlMain.Margin = new Padding(4, 5, 4, 5);
			pnlMain.Name = "pnlMain";
			pnlMain.Size = new Size(1688, 1217);
			pnlMain.TabIndex = 0;
			// 
			// pnlContent
			// 
			pnlContent.BackColor = Color.White;
			pnlContent.Controls.Add(dgvResults);
			pnlContent.Dock = DockStyle.Top;
			pnlContent.Location = new Point(0, 300);
			pnlContent.Margin = new Padding(4, 5, 4, 5);
			pnlContent.Name = "pnlContent";
			pnlContent.Padding = new Padding(29, 33, 29, 133);
			pnlContent.Size = new Size(1688, 870);
			pnlContent.TabIndex = 2;
			// 
			// dgvResults
			// 
			dgvResults.AllowUserToAddRows = false;
			dgvResults.AllowUserToDeleteRows = false;
			dgvResults.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
			dgvResults.BackgroundColor = Color.White;
			dgvResults.BorderStyle = BorderStyle.None;
			dgvResults.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
			dgvResults.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgvResults.Dock = DockStyle.Fill;
			dgvResults.Location = new Point(29, 33);
			dgvResults.Margin = new Padding(4, 5, 4, 5);
			dgvResults.Name = "dgvResults";
			dgvResults.ReadOnly = true;
			dgvResults.RowHeadersVisible = false;
			dgvResults.RowHeadersWidth = 62;
			dgvResults.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			dgvResults.Size = new Size(1630, 700);
			dgvResults.TabIndex = 0;
			// 
			// pnlFilters
			// 
			pnlFilters.BackColor = Color.FromArgb(248, 249, 250);
			pnlFilters.Controls.Add(cboSortBy);
			pnlFilters.Controls.Add(lblSortBy);
			pnlFilters.Controls.Add(txtSearch);
			pnlFilters.Controls.Add(lblSearch);
			pnlFilters.Dock = DockStyle.Top;
			pnlFilters.Location = new Point(0, 167);
			pnlFilters.Margin = new Padding(4, 5, 4, 5);
			pnlFilters.Name = "pnlFilters";
			pnlFilters.Padding = new Padding(29, 33, 29, 33);
			pnlFilters.Size = new Size(1688, 133);
			pnlFilters.TabIndex = 1;
			// 
			// cboSortBy
			// 
			cboSortBy.DropDownStyle = ComboBoxStyle.DropDownList;
			cboSortBy.Font = new Font("Segoe UI", 10F);
			cboSortBy.FormattingEnabled = true;
			cboSortBy.Location = new Point(857, 47);
			cboSortBy.Margin = new Padding(4, 5, 4, 5);
			cboSortBy.Name = "cboSortBy";
			cboSortBy.Size = new Size(355, 36);
			cboSortBy.TabIndex = 3;
			// 
			// lblSortBy
			// 
			lblSortBy.AutoSize = true;
			lblSortBy.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			lblSortBy.Location = new Point(686, 52);
			lblSortBy.Margin = new Padding(4, 0, 4, 0);
			lblSortBy.Name = "lblSortBy";
			lblSortBy.Size = new Size(140, 28);
			lblSortBy.TabIndex = 2;
			lblSortBy.Text = "Sắp xếp theo:";
			// 
			// txtSearch
			// 
			txtSearch.Font = new Font("Segoe UI", 10F);
			txtSearch.Location = new Point(171, 47);
			txtSearch.Margin = new Padding(4, 5, 4, 5);
			txtSearch.Name = "txtSearch";
			txtSearch.PlaceholderText = "Nhập tên học viên...";
			txtSearch.Size = new Size(455, 34);
			txtSearch.TabIndex = 1;
			// 
			// lblSearch
			// 
			lblSearch.AutoSize = true;
			lblSearch.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			lblSearch.Location = new Point(33, 52);
			lblSearch.Margin = new Padding(4, 0, 4, 0);
			lblSearch.Name = "lblSearch";
			lblSearch.Size = new Size(139, 28);
			lblSearch.TabIndex = 0;
			lblSearch.Text = "🔍 Tìm kiếm:";
			// 
			// pnlHeader
			// 
			pnlHeader.BackColor = Color.White;
			pnlHeader.Controls.Add(lblTitle);
			pnlHeader.Controls.Add(lblStats);
			pnlHeader.Controls.Add(btnBack);
			pnlHeader.Controls.Add(btnExport);
			pnlHeader.Dock = DockStyle.Top;
			pnlHeader.Location = new Point(0, 0);
			pnlHeader.Margin = new Padding(4, 5, 4, 5);
			pnlHeader.Name = "pnlHeader";
			pnlHeader.Padding = new Padding(29, 33, 29, 33);
			pnlHeader.Size = new Size(1688, 167);
			pnlHeader.TabIndex = 0;
			// 
			// lblTitle
			// 
			lblTitle.AutoSize = true;
			lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
			lblTitle.ForeColor = Color.FromArgb(0, 102, 153);
			lblTitle.Location = new Point(33, 38);
			lblTitle.Margin = new Padding(4, 0, 4, 0);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new Size(294, 45);
			lblTitle.TabIndex = 0;
			lblTitle.Text = "Danh sách kết quả";
			// 
			// lblStats
			// 
			lblStats.AutoSize = true;
			lblStats.Font = new Font("Segoe UI", 10F);
			lblStats.ForeColor = Color.Gray;
			lblStats.Location = new Point(33, 100);
			lblStats.Margin = new Padding(4, 0, 4, 0);
			lblStats.Name = "lblStats";
			lblStats.Size = new Size(292, 28);
			lblStats.TabIndex = 1;
			lblStats.Text = "Tổng: 0 học viên | Điểm TB: 0/10";
			// 
			// btnBack
			// 
			btnBack.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			btnBack.BackColor = Color.FromArgb(108, 117, 125);
			btnBack.Cursor = Cursors.Hand;
			btnBack.FlatAppearance.BorderSize = 0;
			btnBack.FlatStyle = FlatStyle.Flat;
			btnBack.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			btnBack.ForeColor = Color.White;
			btnBack.Location = new Point(1474, 50);
			btnBack.Margin = new Padding(4, 5, 4, 5);
			btnBack.Name = "btnBack";
			btnBack.Size = new Size(171, 67);
			btnBack.TabIndex = 2;
			btnBack.Text = "← Quay lại";
			btnBack.UseVisualStyleBackColor = false;
			// 
			// btnExport
			// 
			btnExport.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			btnExport.BackColor = Color.FromArgb(40, 167, 69);
			btnExport.Cursor = Cursors.Hand;
			btnExport.FlatAppearance.BorderSize = 0;
			btnExport.FlatStyle = FlatStyle.Flat;
			btnExport.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			btnExport.ForeColor = Color.White;
			btnExport.Location = new Point(1260, 50);
			btnExport.Margin = new Padding(4, 5, 4, 5);
			btnExport.Name = "btnExport";
			btnExport.Size = new Size(186, 67);
			btnExport.TabIndex = 3;
			btnExport.Text = "📥 Xuất Excel";
			btnExport.UseVisualStyleBackColor = false;
			// 
			// TestResultsListControl
			// 
			AutoScaleDimensions = new SizeF(10F, 25F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.FromArgb(248, 249, 250);
			Controls.Add(pnlScrollWrapper);
			Margin = new Padding(4, 5, 4, 5);
			Name = "TestResultsListControl";
			Size = new Size(1714, 1167);
			pnlScrollWrapper.ResumeLayout(false);
			pnlScrollWrapper.PerformLayout();
			pnlMain.ResumeLayout(false);
			pnlContent.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)dgvResults).EndInit();
			pnlFilters.ResumeLayout(false);
			pnlFilters.PerformLayout();
			pnlHeader.ResumeLayout(false);
			pnlHeader.PerformLayout();
			ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnlScrollWrapper;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblStats;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Panel pnlFilters;
        private System.Windows.Forms.ComboBox cboSortBy;
        private System.Windows.Forms.Label lblSortBy;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.DataGridView dgvResults;
    }
}
