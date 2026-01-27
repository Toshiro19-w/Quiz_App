namespace WinFormsApp1.View.User.Forms
{
    partial class SubscriptionForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private Panel pnlHeader;
        private Label lblTitle;
        private Button btnClose;
        private Panel pnlContent;
        private Panel pnlBenefits;
        private Label lblBenefitsTitle;
        private Label lblChoosePlan;
        private Button btnMonth1;
        private Button btnMonth6;
        private Button btnYear1;
        private Button btnPayment;
        private Button btnCancel;

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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pnlHeader = new Panel();
            btnClose = new Button();
            lblTitle = new Label();
            pnlContent = new Panel();
            btnCancel = new Button();
            btnPayment = new Button();
            lblChoosePlan = new Label();
            pnlBenefits = new Panel();
            lblBenefitsTitle = new Label();
            pnlHeader.SuspendLayout();
            pnlContent.SuspendLayout();
            pnlBenefits.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(255, 153, 51);
            pnlHeader.Controls.Add(btnClose);
            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Margin = new Padding(3, 4, 3, 4);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(1029, 133);
            pnlHeader.TabIndex = 0;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClose.BackColor = Color.Transparent;
            btnClose.Cursor = Cursors.Hand;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            btnClose.ForeColor = Color.White;
            btnClose.Location = new Point(971, 40);
            btnClose.Margin = new Padding(3, 4, 3, 4);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(46, 53);
            btnClose.TabIndex = 1;
            btnClose.Text = "✕";
            btnClose.UseVisualStyleBackColor = false;
            btnClose.Click += btnClose_Click;
            // 
            // lblTitle
            // 
            lblTitle.Dock = DockStyle.Fill;
            lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(0, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(1029, 133);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "📅 YMEDU PLUS";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlContent
            // 
            pnlContent.AutoScroll = true;
            pnlContent.BackColor = Color.White;
            pnlContent.Controls.Add(btnCancel);
            pnlContent.Controls.Add(btnPayment);
            pnlContent.Controls.Add(lblChoosePlan);
            pnlContent.Controls.Add(pnlBenefits);
            pnlContent.Location = new Point(0, 133);
            pnlContent.Margin = new Padding(3, 4, 3, 4);
            pnlContent.Name = "pnlContent";
            pnlContent.Size = new Size(1029, 800);
            pnlContent.TabIndex = 1;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.White;
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.FlatAppearance.BorderColor = Color.LightGray;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 11F);
            btnCancel.ForeColor = Color.Gray;
            btnCancel.Location = new Point(429, 670);
            btnCancel.Margin = new Padding(3, 4, 3, 4);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(171, 60);
            btnCancel.TabIndex = 3;
            btnCancel.Text = "Hủy";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnPayment
            // 
            btnPayment.BackColor = Color.FromArgb(40, 167, 69);
            btnPayment.Cursor = Cursors.Hand;
            btnPayment.FlatAppearance.BorderSize = 0;
            btnPayment.FlatStyle = FlatStyle.Flat;
            btnPayment.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnPayment.ForeColor = Color.White;
            btnPayment.Location = new Point(314, 570);
            btnPayment.Margin = new Padding(3, 4, 3, 4);
            btnPayment.Name = "btnPayment";
            btnPayment.Size = new Size(400, 80);
            btnPayment.TabIndex = 2;
            btnPayment.Text = "💳 THANH TOÁN";
            btnPayment.UseVisualStyleBackColor = false;
            // 
            // lblChoosePlan
            // 
            lblChoosePlan.AutoSize = true;
            lblChoosePlan.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblChoosePlan.ForeColor = Color.FromArgb(50, 50, 50);
            lblChoosePlan.Location = new Point(400, 320);
            lblChoosePlan.Name = "lblChoosePlan";
            lblChoosePlan.Size = new Size(278, 37);
            lblChoosePlan.TabIndex = 1;
            lblChoosePlan.Text = "CHỌN GÓI ĐĂNG KÝ";
            // 
            // pnlBenefits
            // 
            pnlBenefits.BackColor = Color.FromArgb(240, 248, 255);
            pnlBenefits.BorderStyle = BorderStyle.FixedSingle;
            pnlBenefits.Controls.Add(lblBenefitsTitle);
            pnlBenefits.Location = new Point(57, 40);
            pnlBenefits.Margin = new Padding(3, 4, 3, 4);
            pnlBenefits.Name = "pnlBenefits";
            pnlBenefits.Size = new Size(914, 239);
            pnlBenefits.TabIndex = 0;
            // 
            // lblBenefitsTitle
            // 
            lblBenefitsTitle.AutoSize = true;
            lblBenefitsTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblBenefitsTitle.ForeColor = Color.FromArgb(255, 153, 51);
            lblBenefitsTitle.Location = new Point(23, 20);
            lblBenefitsTitle.Name = "lblBenefitsTitle";
            lblBenefitsTitle.Size = new Size(348, 32);
            lblBenefitsTitle.TabIndex = 0;
            lblBenefitsTitle.Text = "🎁 QUYỀN LỢI KHI ĐĂNG KÝ";
            // 
            // SubscriptionForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1029, 933);
            Controls.Add(pnlContent);
            Controls.Add(pnlHeader);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            Name = "SubscriptionForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Đăng ký dịch vụ";
            Load += SubscriptionForm_Load;
            pnlHeader.ResumeLayout(false);
            pnlContent.ResumeLayout(false);
            pnlContent.PerformLayout();
            pnlBenefits.ResumeLayout(false);
            pnlBenefits.PerformLayout();
            ResumeLayout(false);

        }

        #endregion
    }
}
