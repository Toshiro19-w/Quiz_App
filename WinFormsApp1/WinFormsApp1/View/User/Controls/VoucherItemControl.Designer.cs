namespace WinFormsApp1.View.User.Controls
{
    partial class VoucherItemControl
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

		private void InitializeComponent()
		{
			panelMain = new Panel();
			panelRight = new Panel();
			rbSelect = new RadioButton();
			lblQuantity = new Label();
			lblConditions = new Label();
			lblExpiryInfo = new Label();
			lblExpiry = new Label();
			lblMinOrder = new Label();
			lblDiscount = new Label();
			panelLeft = new Panel();
			lblFreeShip = new Label();
			panelMain.SuspendLayout();
			panelRight.SuspendLayout();
			panelLeft.SuspendLayout();
			SuspendLayout();
			// 
			// panelMain
			// 
			panelMain.BackColor = Color.White;
			panelMain.BorderStyle = BorderStyle.FixedSingle;
			panelMain.Controls.Add(panelRight);
			panelMain.Controls.Add(panelLeft);
			panelMain.Cursor = Cursors.Hand;
			panelMain.Dock = DockStyle.Fill;
			panelMain.Location = new Point(0, 0);
			panelMain.Margin = new Padding(10);
			panelMain.Name = "panelMain";
			panelMain.Padding = new Padding(0, 0, 0, 10);
			panelMain.Size = new Size(680, 140);
			panelMain.TabIndex = 0;
			panelMain.Click += panelMain_Click;
			// 
			// panelRight
			// 
			panelRight.BackColor = Color.White;
			panelRight.Controls.Add(rbSelect);
			panelRight.Controls.Add(lblQuantity);
			panelRight.Controls.Add(lblConditions);
			panelRight.Controls.Add(lblExpiryInfo);
			panelRight.Controls.Add(lblExpiry);
			panelRight.Controls.Add(lblMinOrder);
			panelRight.Controls.Add(lblDiscount);
			panelRight.Dock = DockStyle.Fill;
			panelRight.Location = new Point(153, 0);
			panelRight.Name = "panelRight";
			panelRight.Padding = new Padding(15, 10, 15, 10);
			panelRight.Size = new Size(525, 128);
			panelRight.TabIndex = 1;
			// 
			// rbSelect
			// 
			rbSelect.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			rbSelect.Location = new Point(485, 10);
			rbSelect.Name = "rbSelect";
			rbSelect.Size = new Size(25, 25);
			rbSelect.TabIndex = 6;
			rbSelect.TabStop = true;
			rbSelect.UseVisualStyleBackColor = true;
			rbSelect.CheckedChanged += rbSelect_CheckedChanged;
			// 
			// lblQuantity
			// 
			lblQuantity.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			lblQuantity.Font = new Font("Segoe UI", 9F);
			lblQuantity.ForeColor = Color.FromArgb(238, 77, 45);
			lblQuantity.Location = new Point(375, 13);
			lblQuantity.Name = "lblQuantity";
			lblQuantity.Size = new Size(100, 20);
			lblQuantity.TabIndex = 5;
			lblQuantity.Text = "x 10";
			lblQuantity.TextAlign = ContentAlignment.MiddleRight;
			lblQuantity.Click += panelMain_Click;
			// 
			// lblConditions
			// 
			lblConditions.Font = new Font("Segoe UI", 8F);
			lblConditions.ForeColor = Color.Gray;
			lblConditions.Location = new Point(15, 80);
			lblConditions.Name = "lblConditions";
			lblConditions.Size = new Size(463, 35);
			lblConditions.TabIndex = 4;
			lblConditions.Text = "⚠️ Vui lòng chọn sản phẩm trong giỏ hàng để áp dụng Voucher này";
			lblConditions.Click += panelMain_Click;
			// 
			// lblExpiryInfo
			// 
			lblExpiryInfo.AutoSize = true;
			lblExpiryInfo.Font = new Font("Segoe UI", 8.5F);
			lblExpiryInfo.ForeColor = Color.Gray;
			lblExpiryInfo.Location = new Point(15, 60);
			lblExpiryInfo.Name = "lblExpiryInfo";
			lblExpiryInfo.Size = new Size(76, 23);
			lblExpiryInfo.TabIndex = 3;
			lblExpiryInfo.Text = "Đã dùng";
			lblExpiryInfo.Click += panelMain_Click;
			// 
			// lblExpiry
			// 
			lblExpiry.AutoSize = true;
			lblExpiry.Font = new Font("Segoe UI", 8.5F);
			lblExpiry.ForeColor = Color.Gray;
			lblExpiry.Location = new Point(200, 60);
			lblExpiry.Name = "lblExpiry";
			lblExpiry.Size = new Size(47, 23);
			lblExpiry.TabIndex = 2;
			lblExpiry.Text = "HSD:";
			lblExpiry.Click += panelMain_Click;
			// 
			// lblMinOrder
			// 
			lblMinOrder.AutoSize = true;
			lblMinOrder.Font = new Font("Segoe UI", 9F);
			lblMinOrder.Location = new Point(15, 37);
			lblMinOrder.Name = "lblMinOrder";
			lblMinOrder.Size = new Size(148, 25);
			lblMinOrder.TabIndex = 1;
			lblMinOrder.Text = "Đơn Tối Thiểu 0₫";
			lblMinOrder.Click += panelMain_Click;
			// 
			// lblDiscount
			// 
			lblDiscount.AutoSize = true;
			lblDiscount.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			lblDiscount.Location = new Point(15, 13);
			lblDiscount.Name = "lblDiscount";
			lblDiscount.Size = new Size(164, 28);
			lblDiscount.TabIndex = 0;
			lblDiscount.Text = "Giảm tối đa 50k";
			lblDiscount.Click += panelMain_Click;
			// 
			// panelLeft
			// 
			panelLeft.BackColor = Color.FromArgb(0, 174, 173);
			panelLeft.Controls.Add(lblFreeShip);
			panelLeft.Dock = DockStyle.Left;
			panelLeft.Location = new Point(0, 0);
			panelLeft.Name = "panelLeft";
			panelLeft.Size = new Size(153, 128);
			panelLeft.TabIndex = 0;
			// 
			// lblFreeShip
			// 
			lblFreeShip.Dock = DockStyle.Left;
			lblFreeShip.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
			lblFreeShip.ForeColor = Color.White;
			lblFreeShip.Location = new Point(0, 0);
			lblFreeShip.Name = "lblFreeShip";
			lblFreeShip.Padding = new Padding(0, 10, 0, 0);
			lblFreeShip.Size = new Size(150, 128);
			lblFreeShip.TabIndex = 0;
			lblFreeShip.Text = "FREE\r\nSHIP";
			lblFreeShip.TextAlign = ContentAlignment.MiddleCenter;
			lblFreeShip.Click += panelMain_Click;
			// 
			// VoucherItemControl
			// 
			AutoScaleDimensions = new SizeF(10F, 25F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(panelMain);
			Margin = new Padding(10);
			Name = "VoucherItemControl";
			Size = new Size(680, 140);
			panelMain.ResumeLayout(false);
			panelRight.ResumeLayout(false);
			panelRight.PerformLayout();
			panelLeft.ResumeLayout(false);
			ResumeLayout(false);
		}

		private Panel panelMain;
        private Panel panelLeft;
        private Label lblFreeShip;
        private Panel panelRight;
        private Label lblDiscount;
        private Label lblMinOrder;
        private Label lblExpiry;
        private Label lblExpiryInfo;
        private Label lblConditions;
        private Label lblQuantity;
        private RadioButton rbSelect;
    }
}
