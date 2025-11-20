namespace WinFormsApp1.View.User
{
    partial class MainContainer
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

		#region Windows Form Designer generated code

		private void InitializeComponent()
		{
			topMenuPanel = new Panel();
			btnGiangVien = new Button();
			btnHocTap = new Button();
			btnCart = new Button();
			profilePanel = new Panel();
			btnProfile = new Button();
			lblUserName = new Label();
			searchPanel = new Panel();
			txtSearch = new TextBox();
			btnSearch = new Button();
			btnKhamPha = new Button();
			logoPanel = new Panel();
			lblLogo = new Label();
			categoriesPanel = new Panel();
			btnTriTueNhanTao = new Button();
			btnPhanTichDuLieu = new Button();
			btnLapTrinh = new Button();
			btnCoSoDuLieu = new Button();
			mainContentPanel = new Panel();
			topMenuPanel.SuspendLayout();
			profilePanel.SuspendLayout();
			searchPanel.SuspendLayout();
			logoPanel.SuspendLayout();
			categoriesPanel.SuspendLayout();
			SuspendLayout();
			// 
			// topMenuPanel
			// 
			topMenuPanel.BackColor = Color.White;
			topMenuPanel.Controls.Add(btnGiangVien);
			topMenuPanel.Controls.Add(btnHocTap);
			topMenuPanel.Controls.Add(btnCart);
			topMenuPanel.Controls.Add(profilePanel);
			topMenuPanel.Controls.Add(searchPanel);
			topMenuPanel.Controls.Add(btnKhamPha);
			topMenuPanel.Controls.Add(logoPanel);
			topMenuPanel.Dock = DockStyle.Top;
			topMenuPanel.Location = new Point(0, 0);
			topMenuPanel.Name = "topMenuPanel";
			topMenuPanel.Padding = new Padding(30, 0, 30, 0);
			topMenuPanel.Size = new Size(1898, 120);
			topMenuPanel.TabIndex = 0;
			// 
			// btnGiangVien
			// 
			btnGiangVien.Dock = DockStyle.Right;
			btnGiangVien.FlatAppearance.BorderSize = 0;
			btnGiangVien.FlatStyle = FlatStyle.Flat;
			btnGiangVien.Font = new Font("Segoe UI", 15F);
			btnGiangVien.Location = new Point(1010, 0);
			btnGiangVien.Name = "btnGiangVien";
			btnGiangVien.Size = new Size(235, 120);
			btnGiangVien.TabIndex = 5;
			btnGiangVien.Text = "Giảng viên";
			btnGiangVien.UseVisualStyleBackColor = true;
			btnGiangVien.Click += btnGiangVien_Click;
			// 
			// btnHocTap
			// 
			btnHocTap.Dock = DockStyle.Right;
			btnHocTap.FlatAppearance.BorderSize = 0;
			btnHocTap.FlatStyle = FlatStyle.Flat;
			btnHocTap.Font = new Font("Segoe UI", 15F);
			btnHocTap.Location = new Point(1245, 0);
			btnHocTap.Name = "btnHocTap";
			btnHocTap.Size = new Size(165, 120);
			btnHocTap.TabIndex = 4;
			btnHocTap.Text = "Học tập";
			btnHocTap.UseVisualStyleBackColor = true;
			btnHocTap.Click += btnHocTap_Click;
			// 
			// btnCart
			// 
			btnCart.Dock = DockStyle.Right;
			btnCart.FlatAppearance.BorderSize = 0;
			btnCart.FlatStyle = FlatStyle.Flat;
			btnCart.Font = new Font("Segoe UI", 24F);
			btnCart.Location = new Point(1410, 0);
			btnCart.Name = "btnCart";
			btnCart.Size = new Size(75, 120);
			btnCart.TabIndex = 3;
			btnCart.Text = "\U0001f6d2";
			btnCart.UseVisualStyleBackColor = true;
			btnCart.Click += btnCart_Click;
			// 
			// profilePanel
			// 
			profilePanel.Controls.Add(btnProfile);
			profilePanel.Controls.Add(lblUserName);
			profilePanel.Dock = DockStyle.Right;
			profilePanel.Location = new Point(1485, 0);
			profilePanel.Name = "profilePanel";
			profilePanel.Size = new Size(383, 120);
			profilePanel.TabIndex = 6;
			profilePanel.Paint += profilePanel_Paint;
			// 
			// btnProfile
			// 
			btnProfile.BackColor = Color.FromArgb(64, 64, 64);
			btnProfile.FlatAppearance.BorderSize = 0;
			btnProfile.FlatStyle = FlatStyle.Flat;
			btnProfile.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
			btnProfile.ForeColor = Color.White;
			btnProfile.Location = new Point(293, 27);
			btnProfile.Name = "btnProfile";
			btnProfile.Size = new Size(60, 60);
			btnProfile.TabIndex = 1;
			btnProfile.Text = "TM";
			btnProfile.UseVisualStyleBackColor = false;
			btnProfile.Click += btnProfile_Click;
			// 
			// lblUserName
			// 
			lblUserName.AutoSize = true;
			lblUserName.Font = new Font("Segoe UI", 13.5F);
			lblUserName.Location = new Point(15, 42);
			lblUserName.Name = "lblUserName";
			lblUserName.Size = new Size(117, 37);
			lblUserName.TabIndex = 0;
			lblUserName.Text = "Tên User";
			// 
			// searchPanel
			// 
			searchPanel.BackColor = Color.FromArgb(248, 249, 250);
			searchPanel.Controls.Add(txtSearch);
			searchPanel.Controls.Add(btnSearch);
			searchPanel.Location = new Point(544, 20);
			searchPanel.Name = "searchPanel";
			searchPanel.Size = new Size(945, 72);
			searchPanel.TabIndex = 2;
			// 
			// txtSearch
			// 
			txtSearch.BackColor = Color.FromArgb(248, 249, 250);
			txtSearch.BorderStyle = BorderStyle.None;
			txtSearch.Font = new Font("Segoe UI", 16.5F);
			txtSearch.ForeColor = Color.Gray;
			txtSearch.Location = new Point(68, 15);
			txtSearch.Name = "txtSearch";
			txtSearch.PlaceholderText = "Tìm kiếm khóa học...";
			txtSearch.Size = new Size(1109, 44);
			txtSearch.TabIndex = 1;
			txtSearch.KeyPress += txtSearch_KeyPress;
			// 
			// btnSearch
			// 
			btnSearch.BackColor = Color.FromArgb(248, 249, 250);
			btnSearch.FlatAppearance.BorderSize = 0;
			btnSearch.FlatStyle = FlatStyle.Flat;
			btnSearch.Font = new Font("Segoe UI", 18F);
			btnSearch.Location = new Point(8, 8);
			btnSearch.Name = "btnSearch";
			btnSearch.Size = new Size(53, 60);
			btnSearch.TabIndex = 0;
			btnSearch.Text = "🔍";
			btnSearch.UseVisualStyleBackColor = false;
			btnSearch.Click += btnSearch_Click;
			// 
			// btnKhamPha
			// 
			btnKhamPha.Dock = DockStyle.Left;
			btnKhamPha.FlatAppearance.BorderSize = 0;
			btnKhamPha.FlatStyle = FlatStyle.Flat;
			btnKhamPha.Font = new Font("Segoe UI", 15F);
			btnKhamPha.Location = new Point(263, 0);
			btnKhamPha.Name = "btnKhamPha";
			btnKhamPha.Size = new Size(275, 120);
			btnKhamPha.TabIndex = 1;
			btnKhamPha.Text = "Khám phá";
			btnKhamPha.UseVisualStyleBackColor = true;
			btnKhamPha.Click += btnKhamPha_Click;
			// 
			// logoPanel
			// 
			logoPanel.Controls.Add(lblLogo);
			logoPanel.Dock = DockStyle.Left;
			logoPanel.Location = new Point(30, 0);
			logoPanel.Name = "logoPanel";
			logoPanel.Size = new Size(233, 120);
			logoPanel.TabIndex = 0;
			logoPanel.Click += logoPanel_Click;
			// 
			// lblLogo
			// 
			lblLogo.AutoSize = true;
			lblLogo.Cursor = Cursors.Hand;
			lblLogo.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
			lblLogo.ForeColor = Color.FromArgb(214, 188, 132);
			lblLogo.Location = new Point(15, 27);
			lblLogo.Name = "lblLogo";
			lblLogo.Size = new Size(199, 65);
			lblLogo.TabIndex = 0;
			lblLogo.Text = "YMEDU";
			lblLogo.Click += logoPanel_Click;
			// 
			// categoriesPanel
			// 
			categoriesPanel.BackColor = Color.White;
			categoriesPanel.BorderStyle = BorderStyle.FixedSingle;
			categoriesPanel.Controls.Add(btnTriTueNhanTao);
			categoriesPanel.Controls.Add(btnPhanTichDuLieu);
			categoriesPanel.Controls.Add(btnLapTrinh);
			categoriesPanel.Controls.Add(btnCoSoDuLieu);
			categoriesPanel.Dock = DockStyle.Top;
			categoriesPanel.Location = new Point(0, 120);
			categoriesPanel.Name = "categoriesPanel";
			categoriesPanel.Padding = new Padding(30, 0, 30, 0);
			categoriesPanel.Size = new Size(1898, 75);
			categoriesPanel.TabIndex = 1;
			// 
			// btnTriTueNhanTao
			// 
			btnTriTueNhanTao.Dock = DockStyle.Left;
			btnTriTueNhanTao.FlatAppearance.BorderSize = 0;
			btnTriTueNhanTao.FlatStyle = FlatStyle.Flat;
			btnTriTueNhanTao.Font = new Font("Segoe UI", 14.25F);
			btnTriTueNhanTao.Location = new Point(962, 0);
			btnTriTueNhanTao.Name = "btnTriTueNhanTao";
			btnTriTueNhanTao.Size = new Size(371, 73);
			btnTriTueNhanTao.TabIndex = 3;
			btnTriTueNhanTao.Text = "Trí tuệ nhân tạo";
			btnTriTueNhanTao.UseVisualStyleBackColor = true;
			btnTriTueNhanTao.Click += btnCategory_Click;
			// 
			// btnPhanTichDuLieu
			// 
			btnPhanTichDuLieu.Dock = DockStyle.Left;
			btnPhanTichDuLieu.FlatAppearance.BorderSize = 0;
			btnPhanTichDuLieu.FlatStyle = FlatStyle.Flat;
			btnPhanTichDuLieu.Font = new Font("Segoe UI", 14.25F);
			btnPhanTichDuLieu.Location = new Point(651, 0);
			btnPhanTichDuLieu.Name = "btnPhanTichDuLieu";
			btnPhanTichDuLieu.Size = new Size(311, 73);
			btnPhanTichDuLieu.TabIndex = 2;
			btnPhanTichDuLieu.Text = "Phân tích dữ liệu";
			btnPhanTichDuLieu.UseVisualStyleBackColor = true;
			btnPhanTichDuLieu.Click += btnCategory_Click;
			// 
			// btnLapTrinh
			// 
			btnLapTrinh.Dock = DockStyle.Left;
			btnLapTrinh.FlatAppearance.BorderSize = 0;
			btnLapTrinh.FlatStyle = FlatStyle.Flat;
			btnLapTrinh.Font = new Font("Segoe UI", 14.25F);
			btnLapTrinh.Location = new Point(341, 0);
			btnLapTrinh.Name = "btnLapTrinh";
			btnLapTrinh.Size = new Size(310, 73);
			btnLapTrinh.TabIndex = 1;
			btnLapTrinh.Text = "Lập trình";
			btnLapTrinh.UseVisualStyleBackColor = true;
			btnLapTrinh.Click += btnCategory_Click;
			// 
			// btnCoSoDuLieu
			// 
			btnCoSoDuLieu.Dock = DockStyle.Left;
			btnCoSoDuLieu.FlatAppearance.BorderSize = 0;
			btnCoSoDuLieu.FlatStyle = FlatStyle.Flat;
			btnCoSoDuLieu.Font = new Font("Segoe UI", 14.25F);
			btnCoSoDuLieu.Location = new Point(30, 0);
			btnCoSoDuLieu.Name = "btnCoSoDuLieu";
			btnCoSoDuLieu.Size = new Size(311, 73);
			btnCoSoDuLieu.TabIndex = 0;
			btnCoSoDuLieu.Text = "Cơ sở dữ liệu";
			btnCoSoDuLieu.UseVisualStyleBackColor = true;
			btnCoSoDuLieu.Click += btnCategory_Click;
			// 
			// mainContentPanel
			// 
			mainContentPanel.BackColor = Color.FromArgb(248, 249, 250);
			mainContentPanel.Dock = DockStyle.Fill;
			mainContentPanel.Location = new Point(0, 195);
			mainContentPanel.Name = "mainContentPanel";
			mainContentPanel.Size = new Size(1898, 829);
			mainContentPanel.TabIndex = 2;
			// 
			// MainContainer
			// 
			AutoScaleDimensions = new SizeF(10F, 25F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(1898, 1024);
			Controls.Add(mainContentPanel);
			Controls.Add(categoriesPanel);
			Controls.Add(topMenuPanel);
			Name = "MainContainer";
			Text = "Learning Platform - YMEDU";
			WindowState = FormWindowState.Maximized;
			topMenuPanel.ResumeLayout(false);
			profilePanel.ResumeLayout(false);
			profilePanel.PerformLayout();
			searchPanel.ResumeLayout(false);
			searchPanel.PerformLayout();
			logoPanel.ResumeLayout(false);
			logoPanel.PerformLayout();
			categoriesPanel.ResumeLayout(false);
			ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.Panel topMenuPanel;
        private System.Windows.Forms.Panel logoPanel;
        private System.Windows.Forms.Label lblLogo;
        private System.Windows.Forms.Button btnKhamPha;
        private System.Windows.Forms.Panel searchPanel;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnCart;
        private System.Windows.Forms.Button btnHocTap;
        private System.Windows.Forms.Button btnGiangVien;
        private System.Windows.Forms.Panel profilePanel;
        private System.Windows.Forms.Button btnProfile;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Panel categoriesPanel;
        private System.Windows.Forms.Button btnCoSoDuLieu;
        private System.Windows.Forms.Button btnLapTrinh;
        private System.Windows.Forms.Button btnPhanTichDuLieu;
        private System.Windows.Forms.Button btnTriTueNhanTao;
        private System.Windows.Forms.Panel mainContentPanel;
    }
}
