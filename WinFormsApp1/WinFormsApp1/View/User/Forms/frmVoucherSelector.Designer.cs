namespace WinFormsApp1.View.User.Forms
{
    partial class frmVoucherSelector
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
			panelTop = new Panel();
			btnClose = new Button();
			lblTitle = new Label();
			panelCenter = new Panel();
			panelVoucherList = new Panel();
			flowLayoutVouchers = new FlowLayoutPanel();
			lblVoucherListTitle = new Label();
			panelVoucherInput = new Panel();
			btnApplyCode = new Button();
			txtVoucherCode = new TextBox();
			lblVoucherCode = new Label();
			panelBottom = new Panel();
			btnConfirm = new Button();
			btnCancel = new Button();
			panelTop.SuspendLayout();
			panelCenter.SuspendLayout();
			panelVoucherList.SuspendLayout();
			panelVoucherInput.SuspendLayout();
			panelBottom.SuspendLayout();
			SuspendLayout();
			// 
			// panelTop
			// 
			panelTop.BackColor = Color.White;
			panelTop.BorderStyle = BorderStyle.FixedSingle;
			panelTop.Controls.Add(btnClose);
			panelTop.Controls.Add(lblTitle);
			panelTop.Dock = DockStyle.Top;
			panelTop.Location = new Point(0, 0);
			panelTop.Name = "panelTop";
			panelTop.Size = new Size(835, 60);
			panelTop.TabIndex = 0;
			// 
			// btnClose
			// 
			btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			btnClose.BackColor = Color.Transparent;
			btnClose.Cursor = Cursors.Hand;
			btnClose.FlatAppearance.BorderSize = 0;
			btnClose.FlatStyle = FlatStyle.Flat;
			btnClose.Font = new Font("Segoe UI", 16F);
			btnClose.ForeColor = Color.Gray;
			btnClose.Location = new Point(775, 5);
			btnClose.Name = "btnClose";
			btnClose.Size = new Size(50, 50);
			btnClose.TabIndex = 1;
			btnClose.Text = "×";
			btnClose.UseVisualStyleBackColor = false;
			btnClose.Click += btnClose_Click;
			// 
			// lblTitle
			// 
			lblTitle.AutoSize = true;
			lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
			lblTitle.Location = new Point(20, 15);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new Size(200, 38);
			lblTitle.TabIndex = 0;
			lblTitle.Text = "Chọn Voucher";
			// 
			// panelCenter
			// 
			panelCenter.BackColor = Color.FromArgb(245, 245, 245);
			panelCenter.Controls.Add(panelVoucherList);
			panelCenter.Controls.Add(panelVoucherInput);
			panelCenter.Dock = DockStyle.Fill;
			panelCenter.Location = new Point(0, 60);
			panelCenter.Name = "panelCenter";
			panelCenter.Padding = new Padding(20);
			panelCenter.Size = new Size(835, 540);
			panelCenter.TabIndex = 1;
			// 
			// panelVoucherList
			// 
			panelVoucherList.BackColor = Color.White;
			panelVoucherList.Controls.Add(flowLayoutVouchers);
			panelVoucherList.Controls.Add(lblVoucherListTitle);
			panelVoucherList.Dock = DockStyle.Fill;
			panelVoucherList.Location = new Point(20, 100);
			panelVoucherList.Name = "panelVoucherList";
			panelVoucherList.Padding = new Padding(20, 15, 20, 15);
			panelVoucherList.Size = new Size(795, 420);
			panelVoucherList.TabIndex = 1;
			// 
			// flowLayoutVouchers
			// 
			flowLayoutVouchers.AutoScroll = true;
			flowLayoutVouchers.Dock = DockStyle.Fill;
			flowLayoutVouchers.FlowDirection = FlowDirection.TopDown;
			flowLayoutVouchers.Location = new Point(20, 55);
			flowLayoutVouchers.Name = "flowLayoutVouchers";
			flowLayoutVouchers.Padding = new Padding(0, 10, 0, 10);
			flowLayoutVouchers.Size = new Size(755, 350);
			flowLayoutVouchers.TabIndex = 1;
			flowLayoutVouchers.WrapContents = false;
			// 
			// lblVoucherListTitle
			// 
			lblVoucherListTitle.AutoSize = true;
			lblVoucherListTitle.Dock = DockStyle.Top;
			lblVoucherListTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
			lblVoucherListTitle.Location = new Point(20, 15);
			lblVoucherListTitle.Name = "lblVoucherListTitle";
			lblVoucherListTitle.Padding = new Padding(0, 0, 0, 10);
			lblVoucherListTitle.Size = new Size(272, 40);
			lblVoucherListTitle.TabIndex = 0;
			lblVoucherListTitle.Text = "Mã Miễn Phí Vận Chuyển";
			// 
			// panelVoucherInput
			// 
			panelVoucherInput.BackColor = Color.White;
			panelVoucherInput.Controls.Add(btnApplyCode);
			panelVoucherInput.Controls.Add(txtVoucherCode);
			panelVoucherInput.Controls.Add(lblVoucherCode);
			panelVoucherInput.Dock = DockStyle.Top;
			panelVoucherInput.Location = new Point(20, 20);
			panelVoucherInput.Name = "panelVoucherInput";
			panelVoucherInput.Padding = new Padding(20, 15, 20, 15);
			panelVoucherInput.Size = new Size(795, 80);
			panelVoucherInput.TabIndex = 0;
			// 
			// btnApplyCode
			// 
			btnApplyCode.BackColor = Color.FromArgb(238, 77, 45);
			btnApplyCode.Cursor = Cursors.Hand;
			btnApplyCode.Dock = DockStyle.Right;
			btnApplyCode.FlatAppearance.BorderSize = 0;
			btnApplyCode.FlatStyle = FlatStyle.Flat;
			btnApplyCode.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
			btnApplyCode.ForeColor = Color.White;
			btnApplyCode.Location = new Point(655, 15);
			btnApplyCode.Name = "btnApplyCode";
			btnApplyCode.Size = new Size(120, 50);
			btnApplyCode.TabIndex = 2;
			btnApplyCode.Text = "Áp dụng";
			btnApplyCode.UseVisualStyleBackColor = false;
			btnApplyCode.Click += btnApplyCode_Click;
			// 
			// txtVoucherCode
			// 
			txtVoucherCode.BorderStyle = BorderStyle.FixedSingle;
			txtVoucherCode.CharacterCasing = CharacterCasing.Upper;
			txtVoucherCode.Dock = DockStyle.Left;
			txtVoucherCode.Font = new Font("Segoe UI", 11F);
			txtVoucherCode.Location = new Point(156, 15);
			txtVoucherCode.Name = "txtVoucherCode";
			txtVoucherCode.PlaceholderText = "Nhập mã voucher...";
			txtVoucherCode.Size = new Size(350, 37);
			txtVoucherCode.TabIndex = 1;
			// 
			// lblVoucherCode
			// 
			lblVoucherCode.AutoSize = true;
			lblVoucherCode.Dock = DockStyle.Left;
			lblVoucherCode.Font = new Font("Segoe UI", 10F);
			lblVoucherCode.Location = new Point(20, 15);
			lblVoucherCode.Name = "lblVoucherCode";
			lblVoucherCode.Padding = new Padding(0, 5, 20, 0);
			lblVoucherCode.Size = new Size(136, 33);
			lblVoucherCode.TabIndex = 0;
			lblVoucherCode.Text = "Mã Voucher";
			lblVoucherCode.TextAlign = ContentAlignment.TopCenter;
			// 
			// panelBottom
			// 
			panelBottom.BackColor = Color.White;
			panelBottom.BorderStyle = BorderStyle.FixedSingle;
			panelBottom.Controls.Add(btnConfirm);
			panelBottom.Controls.Add(btnCancel);
			panelBottom.Dock = DockStyle.Bottom;
			panelBottom.Location = new Point(0, 600);
			panelBottom.Name = "panelBottom";
			panelBottom.Padding = new Padding(20, 10, 20, 10);
			panelBottom.Size = new Size(835, 70);
			panelBottom.TabIndex = 2;
			// 
			// btnConfirm
			// 
			btnConfirm.BackColor = Color.FromArgb(238, 77, 45);
			btnConfirm.Cursor = Cursors.Hand;
			btnConfirm.Dock = DockStyle.Right;
			btnConfirm.FlatAppearance.BorderSize = 0;
			btnConfirm.FlatStyle = FlatStyle.Flat;
			btnConfirm.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			btnConfirm.ForeColor = Color.White;
			btnConfirm.Location = new Point(665, 10);
			btnConfirm.Name = "btnConfirm";
			btnConfirm.Size = new Size(148, 48);
			btnConfirm.TabIndex = 1;
			btnConfirm.Text = "Đồng ý";
			btnConfirm.UseVisualStyleBackColor = false;
			btnConfirm.Click += btnConfirm_Click;
			// 
			// btnCancel
			// 
			btnCancel.BackColor = Color.White;
			btnCancel.Cursor = Cursors.Hand;
			btnCancel.Dock = DockStyle.Left;
			btnCancel.FlatAppearance.BorderColor = Color.LightGray;
			btnCancel.FlatStyle = FlatStyle.Flat;
			btnCancel.Font = new Font("Segoe UI", 10F);
			btnCancel.Location = new Point(20, 10);
			btnCancel.Name = "btnCancel";
			btnCancel.Size = new Size(150, 48);
			btnCancel.TabIndex = 0;
			btnCancel.Text = "Trở lại";
			btnCancel.UseVisualStyleBackColor = false;
			btnCancel.Click += btnCancel_Click;
			// 
			// frmVoucherSelector
			// 
			AutoScaleDimensions = new SizeF(10F, 25F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(835, 670);
			Controls.Add(panelCenter);
			Controls.Add(panelBottom);
			Controls.Add(panelTop);
			FormBorderStyle = FormBorderStyle.None;
			Name = "frmVoucherSelector";
			StartPosition = FormStartPosition.CenterParent;
			Text = "Chọn Voucher";
			panelTop.ResumeLayout(false);
			panelTop.PerformLayout();
			panelCenter.ResumeLayout(false);
			panelVoucherList.ResumeLayout(false);
			panelVoucherList.PerformLayout();
			panelVoucherInput.ResumeLayout(false);
			panelVoucherInput.PerformLayout();
			panelBottom.ResumeLayout(false);
			ResumeLayout(false);
		}

		private Panel panelTop;
        private Label lblTitle;
        private Button btnClose;
        private Panel panelCenter;
        private Panel panelVoucherInput;
        private Label lblVoucherCode;
        private TextBox txtVoucherCode;
        private Button btnApplyCode;
        private Panel panelVoucherList;
        private Label lblVoucherListTitle;
        private FlowLayoutPanel flowLayoutVouchers;
        private Panel panelBottom;
        private Button btnCancel;
        private Button btnConfirm;
    }
}
