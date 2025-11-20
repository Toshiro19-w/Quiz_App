namespace WinFormsApp1.View.User.Forms
{
    partial class frmCheckout
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
            panelMain = new Panel();
            panelRight = new Panel();
            panelCamKet = new Panel();
            lblCamKetTitle = new Label();
            lblCamKet1 = new Label();
            lblCamKet2 = new Label();
            lblCamKet3 = new Label();
            lblCamKet4 = new Label();
            panelThanhToan = new Panel();
            lblChonPhuongThuc = new Label();
            btnThanhToanMoMo = new Button();
            panelThongBao = new Panel();
            lblThongBao = new Label();
            panelTongKet = new Panel();
            lblTongKet = new Label();
            lblSoKhoaHoc = new Label();
            lblSoKhoaHocValue = new Label();
            lblTamTinh = new Label();
            lblTamTinhValue = new Label();
            lblTongCong = new Label();
            lblTongCongValue = new Label();
            panelLeft = new Panel();
            panelCartItems = new Panel();
            lblGioHang = new Label();
            panelMain.SuspendLayout();
            panelRight.SuspendLayout();
            panelCamKet.SuspendLayout();
            panelThanhToan.SuspendLayout();
            panelThongBao.SuspendLayout();
            panelTongKet.SuspendLayout();
            panelLeft.SuspendLayout();
            SuspendLayout();
            // 
            // panelMain
            // 
            panelMain.BackColor = Color.FromArgb(245, 245, 245);
            panelMain.Controls.Add(panelRight);
            panelMain.Controls.Add(panelLeft);
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(0, 0);
            panelMain.Margin = new Padding(4, 5, 4, 5);
            panelMain.Name = "panelMain";
            panelMain.Padding = new Padding(57, 50, 57, 50);
            panelMain.Size = new Size(2000, 1333);
            panelMain.TabIndex = 0;
            panelMain.Paint += panelMain_Paint;
            // 
            // panelRight
            // 
            panelRight.BackColor = Color.White;
            panelRight.Controls.Add(panelCamKet);
            panelRight.Controls.Add(panelThanhToan);
            panelRight.Controls.Add(panelThongBao);
            panelRight.Controls.Add(panelTongKet);
            panelRight.Dock = DockStyle.Right;
            panelRight.Location = new Point(1429, 50);
            panelRight.Margin = new Padding(4, 5, 4, 5);
            panelRight.Name = "panelRight";
            panelRight.Padding = new Padding(29, 33, 29, 33);
            panelRight.Size = new Size(514, 1233);
            panelRight.TabIndex = 1;
            // 
            // panelCamKet
            // 
            panelCamKet.Controls.Add(lblCamKetTitle);
            panelCamKet.Controls.Add(lblCamKet1);
            panelCamKet.Controls.Add(lblCamKet2);
            panelCamKet.Controls.Add(lblCamKet3);
            panelCamKet.Controls.Add(lblCamKet4);
            panelCamKet.Dock = DockStyle.Top;
            panelCamKet.Location = new Point(29, 816);
            panelCamKet.Margin = new Padding(4, 5, 4, 5);
            panelCamKet.Name = "panelCamKet";
            panelCamKet.Size = new Size(456, 300);
            panelCamKet.TabIndex = 3;
            // 
            // lblCamKetTitle
            // 
            lblCamKetTitle.AutoSize = true;
            lblCamKetTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblCamKetTitle.Location = new Point(0, 17);
            lblCamKetTitle.Margin = new Padding(4, 0, 4, 0);
            lblCamKetTitle.Name = "lblCamKetTitle";
            lblCamKetTitle.Size = new Size(281, 30);
            lblCamKetTitle.TabIndex = 0;
            lblCamKetTitle.Text = "🛡️ Cam kết của chúng tôi";
            // 
            // lblCamKet1
            // 
            lblCamKet1.AutoSize = true;
            lblCamKet1.Font = new Font("Segoe UI", 9F);
            lblCamKet1.Location = new Point(0, 75);
            lblCamKet1.Margin = new Padding(4, 0, 4, 0);
            lblCamKet1.Name = "lblCamKet1";
            lblCamKet1.Size = new Size(170, 25);
            lblCamKet1.TabIndex = 1;
            lblCamKet1.Text = "✓ Truy cập vĩnh viễn";
            // 
            // lblCamKet2
            // 
            lblCamKet2.AutoSize = true;
            lblCamKet2.Font = new Font("Segoe UI", 9F);
            lblCamKet2.Location = new Point(0, 125);
            lblCamKet2.Margin = new Padding(4, 0, 4, 0);
            lblCamKet2.Name = "lblCamKet2";
            lblCamKet2.Size = new Size(175, 25);
            lblCamKet2.TabIndex = 2;
            lblCamKet2.Text = "✓ Cập nhật miễn phí";
            // 
            // lblCamKet3
            // 
            lblCamKet3.AutoSize = true;
            lblCamKet3.Font = new Font("Segoe UI", 9F);
            lblCamKet3.Location = new Point(0, 175);
            lblCamKet3.Margin = new Padding(4, 0, 4, 0);
            lblCamKet3.Name = "lblCamKet3";
            lblCamKet3.Size = new Size(124, 25);
            lblCamKet3.TabIndex = 3;
            lblCamKet3.Text = "✓ Hỗ trợ 24/7";
            // 
            // lblCamKet4
            // 
            lblCamKet4.AutoSize = true;
            lblCamKet4.Font = new Font("Segoe UI", 9F);
            lblCamKet4.Location = new Point(0, 225);
            lblCamKet4.Margin = new Padding(4, 0, 4, 0);
            lblCamKet4.Name = "lblCamKet4";
            lblCamKet4.Size = new Size(205, 25);
            lblCamKet4.TabIndex = 4;
            lblCamKet4.Text = "✓ Chứng chỉ hoàn thành";
            // 
            // panelThanhToan
            // 
            panelThanhToan.Controls.Add(lblChonPhuongThuc);
            panelThanhToan.Controls.Add(btnThanhToanMoMo);
            panelThanhToan.Dock = DockStyle.Top;
            panelThanhToan.Location = new Point(29, 549);
            panelThanhToan.Margin = new Padding(4, 5, 4, 5);
            panelThanhToan.Name = "panelThanhToan";
            panelThanhToan.Size = new Size(456, 267);
            panelThanhToan.TabIndex = 2;
            // 
            // lblChonPhuongThuc
            // 
            lblChonPhuongThuc.AutoSize = true;
            lblChonPhuongThuc.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblChonPhuongThuc.Location = new Point(0, 17);
            lblChonPhuongThuc.Margin = new Padding(4, 0, 4, 0);
            lblChonPhuongThuc.Name = "lblChonPhuongThuc";
            lblChonPhuongThuc.Size = new Size(328, 30);
            lblChonPhuongThuc.TabIndex = 0;
            lblChonPhuongThuc.Text = "Chọn phương thức thanh toán";
            // 
            // btnThanhToanMoMo
            // 
            btnThanhToanMoMo.BackColor = Color.FromArgb(0, 102, 255);
            btnThanhToanMoMo.Cursor = Cursors.Hand;
            btnThanhToanMoMo.FlatAppearance.BorderSize = 0;
            btnThanhToanMoMo.FlatStyle = FlatStyle.Flat;
            btnThanhToanMoMo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnThanhToanMoMo.ForeColor = Color.White;
            btnThanhToanMoMo.Location = new Point(115, 73);
            btnThanhToanMoMo.Margin = new Padding(4, 5, 4, 5);
            btnThanhToanMoMo.Name = "btnThanhToanMoMo";
            btnThanhToanMoMo.Size = new Size(229, 150);
            btnThanhToanMoMo.TabIndex = 1;
            btnThanhToanMoMo.Text = "📱 Thanh toán\r\nMoMo\r\nQuét mã QR hoặc chuyển\r\nkhoản";
            btnThanhToanMoMo.UseVisualStyleBackColor = false;
            btnThanhToanMoMo.Click += btnThanhToanMoMo_Click;
            // 
            // panelThongBao
            // 
            panelThongBao.BackColor = Color.FromArgb(209, 242, 255);
            panelThongBao.Controls.Add(lblThongBao);
            panelThongBao.Dock = DockStyle.Top;
            panelThongBao.Location = new Point(29, 366);
            panelThongBao.Margin = new Padding(4, 5, 4, 5);
            panelThongBao.Name = "panelThongBao";
            panelThongBao.Padding = new Padding(21, 25, 21, 25);
            panelThongBao.Size = new Size(456, 183);
            panelThongBao.TabIndex = 1;
            // 
            // lblThongBao
            // 
            lblThongBao.Dock = DockStyle.Fill;
            lblThongBao.Font = new Font("Segoe UI", 9F);
            lblThongBao.ForeColor = Color.FromArgb(0, 102, 153);
            lblThongBao.Location = new Point(21, 25);
            lblThongBao.Margin = new Padding(4, 0, 4, 0);
            lblThongBao.Name = "lblThongBao";
            lblThongBao.Size = new Size(414, 133);
            lblThongBao.TabIndex = 0;
            lblThongBao.Text = "ℹ️ Sau khi thanh toán thành công, bạn sẽ có quyền truy cập vĩnh viễn vào các khóa học đã mua.";
            // 
            // panelTongKet
            // 
            panelTongKet.Controls.Add(lblTongKet);
            panelTongKet.Controls.Add(lblSoKhoaHoc);
            panelTongKet.Controls.Add(lblSoKhoaHocValue);
            panelTongKet.Controls.Add(lblTamTinh);
            panelTongKet.Controls.Add(lblTamTinhValue);
            panelTongKet.Controls.Add(lblTongCong);
            panelTongKet.Controls.Add(lblTongCongValue);
            panelTongKet.Dock = DockStyle.Top;
            panelTongKet.Location = new Point(29, 33);
            panelTongKet.Margin = new Padding(4, 5, 4, 5);
            panelTongKet.Name = "panelTongKet";
            panelTongKet.Size = new Size(456, 333);
            panelTongKet.TabIndex = 0;
            // 
            // lblTongKet
            // 
            lblTongKet.AutoSize = true;
            lblTongKet.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTongKet.Location = new Point(0, 17);
            lblTongKet.Margin = new Padding(4, 0, 4, 0);
            lblTongKet.Name = "lblTongKet";
            lblTongKet.Size = new Size(156, 32);
            lblTongKet.TabIndex = 0;
            lblTongKet.Text = "\U0001f9fe Tổng kết";
            // 
            // lblSoKhoaHoc
            // 
            lblSoKhoaHoc.AutoSize = true;
            lblSoKhoaHoc.Font = new Font("Segoe UI", 10F);
            lblSoKhoaHoc.Location = new Point(0, 100);
            lblSoKhoaHoc.Margin = new Padding(4, 0, 4, 0);
            lblSoKhoaHoc.Name = "lblSoKhoaHoc";
            lblSoKhoaHoc.Size = new Size(124, 28);
            lblSoKhoaHoc.TabIndex = 1;
            lblSoKhoaHoc.Text = "Số khóa học:";
            // 
            // lblSoKhoaHocValue
            // 
            lblSoKhoaHocValue.AutoSize = true;
            lblSoKhoaHocValue.Font = new Font("Segoe UI", 10F);
            lblSoKhoaHocValue.Location = new Point(400, 100);
            lblSoKhoaHocValue.Margin = new Padding(4, 0, 4, 0);
            lblSoKhoaHocValue.Name = "lblSoKhoaHocValue";
            lblSoKhoaHocValue.Size = new Size(23, 28);
            lblSoKhoaHocValue.TabIndex = 2;
            lblSoKhoaHocValue.Text = "2";
            lblSoKhoaHocValue.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblTamTinh
            // 
            lblTamTinh.AutoSize = true;
            lblTamTinh.Font = new Font("Segoe UI", 10F);
            lblTamTinh.Location = new Point(0, 158);
            lblTamTinh.Margin = new Padding(4, 0, 4, 0);
            lblTamTinh.Name = "lblTamTinh";
            lblTamTinh.Size = new Size(92, 28);
            lblTamTinh.TabIndex = 3;
            lblTamTinh.Text = "Tạm tính:";
            // 
            // lblTamTinhValue
            // 
            lblTamTinhValue.AutoSize = true;
            lblTamTinhValue.Font = new Font("Segoe UI", 10F);
            lblTamTinhValue.Location = new Point(271, 158);
            lblTamTinhValue.Margin = new Padding(4, 0, 4, 0);
            lblTamTinhValue.Name = "lblTamTinhValue";
            lblTamTinhValue.Size = new Size(128, 28);
            lblTamTinhValue.TabIndex = 4;
            lblTamTinhValue.Text = "221,000 VND";
            lblTamTinhValue.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblTongCong
            // 
            lblTongCong.AutoSize = true;
            lblTongCong.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTongCong.Location = new Point(0, 242);
            lblTongCong.Margin = new Padding(4, 0, 4, 0);
            lblTongCong.Name = "lblTongCong";
            lblTongCong.Size = new Size(129, 30);
            lblTongCong.TabIndex = 5;
            lblTongCong.Text = "Tổng cộng:";
            // 
            // lblTongCongValue
            // 
            lblTongCongValue.AutoSize = true;
            lblTongCongValue.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTongCongValue.ForeColor = Color.FromArgb(0, 102, 255);
            lblTongCongValue.Location = new Point(229, 233);
            lblTongCongValue.Margin = new Padding(4, 0, 4, 0);
            lblTongCongValue.Name = "lblTongCongValue";
            lblTongCongValue.Size = new Size(191, 38);
            lblTongCongValue.TabIndex = 6;
            lblTongCongValue.Text = "221,000 VND";
            lblTongCongValue.TextAlign = ContentAlignment.MiddleRight;
            // 
            // panelLeft
            // 
            panelLeft.BackColor = Color.White;
            panelLeft.Controls.Add(panelCartItems);
            panelLeft.Controls.Add(lblGioHang);
            panelLeft.Dock = DockStyle.Fill;
            panelLeft.Location = new Point(57, 50);
            panelLeft.Margin = new Padding(4, 5, 4, 5);
            panelLeft.Name = "panelLeft";
            panelLeft.Padding = new Padding(43, 33, 43, 33);
            panelLeft.Size = new Size(1886, 1233);
            panelLeft.TabIndex = 0;
            // 
            // panelCartItems
            // 
            panelCartItems.AutoScroll = true;
            panelCartItems.Dock = DockStyle.Fill;
            panelCartItems.Location = new Point(43, 104);
            panelCartItems.Margin = new Padding(4, 5, 4, 5);
            panelCartItems.Name = "panelCartItems";
            panelCartItems.Size = new Size(1800, 1096);
            panelCartItems.TabIndex = 1;
            // 
            // lblGioHang
            // 
            lblGioHang.AutoSize = true;
            lblGioHang.Dock = DockStyle.Top;
            lblGioHang.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblGioHang.Location = new Point(43, 33);
            lblGioHang.Margin = new Padding(4, 0, 4, 0);
            lblGioHang.Name = "lblGioHang";
            lblGioHang.Padding = new Padding(0, 0, 0, 33);
            lblGioHang.Size = new Size(293, 71);
            lblGioHang.TabIndex = 0;
            lblGioHang.Text = "\U0001f6d2 Giỏ hàng của bạn";
            // 
            // frmCheckout
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(2000, 1333);
            Controls.Add(panelMain);
            Margin = new Padding(4, 5, 4, 5);
            Name = "frmCheckout";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Thanh toán - YMEDU";
            panelMain.ResumeLayout(false);
            panelRight.ResumeLayout(false);
            panelCamKet.ResumeLayout(false);
            panelCamKet.PerformLayout();
            panelThanhToan.ResumeLayout(false);
            panelThanhToan.PerformLayout();
            panelThongBao.ResumeLayout(false);
            panelTongKet.ResumeLayout(false);
            panelTongKet.PerformLayout();
            panelLeft.ResumeLayout(false);
            panelLeft.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Label lblGioHang;
        private System.Windows.Forms.Panel panelCartItems;
        private System.Windows.Forms.Panel panelTongKet;
        private System.Windows.Forms.Label lblTongKet;
        private System.Windows.Forms.Label lblSoKhoaHoc;
        private System.Windows.Forms.Label lblSoKhoaHocValue;
        private System.Windows.Forms.Label lblTamTinh;
        private System.Windows.Forms.Label lblTamTinhValue;
        private System.Windows.Forms.Label lblTongCong;
        private System.Windows.Forms.Label lblTongCongValue;
        private System.Windows.Forms.Panel panelThongBao;
        private System.Windows.Forms.Label lblThongBao;
        private System.Windows.Forms.Panel panelThanhToan;
        private System.Windows.Forms.Label lblChonPhuongThuc;
        private System.Windows.Forms.Button btnThanhToanMoMo;
        private System.Windows.Forms.Panel panelCamKet;
        private System.Windows.Forms.Label lblCamKetTitle;
        private System.Windows.Forms.Label lblCamKet1;
        private System.Windows.Forms.Label lblCamKet2;
        private System.Windows.Forms.Label lblCamKet3;
        private System.Windows.Forms.Label lblCamKet4;
    }
}
