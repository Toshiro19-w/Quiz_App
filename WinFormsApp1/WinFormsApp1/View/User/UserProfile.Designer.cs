namespace WinFormsApp1.View.User
{
    partial class UserProfile
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
            this.containerPanel = new System.Windows.Forms.Panel();
            this.contentPanel = new System.Windows.Forms.Panel();
            this.tabPanel = new System.Windows.Forms.Panel();
            this.tabUnderline = new System.Windows.Forms.Panel();
            this.btnLichSu = new System.Windows.Forms.Button();
            this.btnChinhSua = new System.Windows.Forms.Button();
            this.btnCaiDat = new System.Windows.Forms.Button();
            this.titleLabel = new System.Windows.Forms.Label();
            this.containerPanel.SuspendLayout();
            this.tabPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // containerPanel
            // 
            this.containerPanel.Controls.Add(this.contentPanel);
            this.containerPanel.Controls.Add(this.tabPanel);
            this.containerPanel.Controls.Add(this.titleLabel);
            this.containerPanel.Location = new System.Drawing.Point(300, 75);
            this.containerPanel.Name = "containerPanel";
            this.containerPanel.Size = new System.Drawing.Size(1140, 1050);
            this.containerPanel.TabIndex = 3;
            // 
            // contentPanel
            // 
            this.contentPanel.BackColor = System.Drawing.Color.White;
            this.contentPanel.Location = new System.Drawing.Point(0, 210);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Size = new System.Drawing.Size(1140, 825);
            this.contentPanel.TabIndex = 2;
            // 
            // tabPanel
            // 
            this.tabPanel.BackColor = System.Drawing.Color.White;
            this.tabPanel.Controls.Add(this.tabUnderline);
            this.tabPanel.Controls.Add(this.btnLichSu);
            this.tabPanel.Controls.Add(this.btnChinhSua);
            this.tabPanel.Controls.Add(this.btnCaiDat);
            this.tabPanel.Location = new System.Drawing.Point(0, 120);
            this.tabPanel.Name = "tabPanel";
            this.tabPanel.Size = new System.Drawing.Size(1140, 90);
            this.tabPanel.TabIndex = 1;
            // 
            // tabUnderline
            // 
            this.tabUnderline.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(56)))), ((int)(((byte)(255)))));
            this.tabUnderline.Location = new System.Drawing.Point(0, 83);
            this.tabUnderline.Name = "tabUnderline";
            this.tabUnderline.Size = new System.Drawing.Size(270, 5);
            this.tabUnderline.TabIndex = 3;
            // 
            // btnLichSu
            // 
            this.btnLichSu.BackColor = System.Drawing.Color.White;
            this.btnLichSu.FlatAppearance.BorderSize = 0;
            this.btnLichSu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLichSu.Font = new System.Drawing.Font("Segoe UI", 15F);
            this.btnLichSu.ForeColor = System.Drawing.Color.Gray;
            this.btnLichSu.Location = new System.Drawing.Point(540, 0);
            this.btnLichSu.Name = "btnLichSu";
            this.btnLichSu.Size = new System.Drawing.Size(270, 83);
            this.btnLichSu.TabIndex = 2;
            this.btnLichSu.Text = "Lịch sử mua hàng";
            this.btnLichSu.UseVisualStyleBackColor = false;
            this.btnLichSu.Click += new System.EventHandler(this.btnLichSu_Click);
            // 
            // btnChinhSua
            // 
            this.btnChinhSua.BackColor = System.Drawing.Color.White;
            this.btnChinhSua.FlatAppearance.BorderSize = 0;
            this.btnChinhSua.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChinhSua.Font = new System.Drawing.Font("Segoe UI", 15F);
            this.btnChinhSua.ForeColor = System.Drawing.Color.Gray;
            this.btnChinhSua.Location = new System.Drawing.Point(270, 0);
            this.btnChinhSua.Name = "btnChinhSua";
            this.btnChinhSua.Size = new System.Drawing.Size(270, 83);
            this.btnChinhSua.TabIndex = 1;
            this.btnChinhSua.Text = "Chỉnh sửa hồ sơ";
            this.btnChinhSua.UseVisualStyleBackColor = false;
            this.btnChinhSua.Click += new System.EventHandler(this.btnChinhSua_Click);
            // 
            // btnCaiDat
            // 
            this.btnCaiDat.BackColor = System.Drawing.Color.White;
            this.btnCaiDat.FlatAppearance.BorderSize = 0;
            this.btnCaiDat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCaiDat.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.btnCaiDat.Location = new System.Drawing.Point(0, 0);
            this.btnCaiDat.Name = "btnCaiDat";
            this.btnCaiDat.Size = new System.Drawing.Size(270, 83);
            this.btnCaiDat.TabIndex = 0;
            this.btnCaiDat.Text = "Cài đặt tài khoản";
            this.btnCaiDat.UseVisualStyleBackColor = false;
            this.btnCaiDat.Click += new System.EventHandler(this.btnCaiDat_Click);
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Segoe UI", 30F, System.Drawing.FontStyle.Bold);
            this.titleLabel.Location = new System.Drawing.Point(0, 15);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(270, 67);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "Tài khoản";
            // 
            // UserProfile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.containerPanel);
            this.Name = "UserProfile";
            this.Size = new System.Drawing.Size(1476, 801);
            this.Resize += new System.EventHandler(this.UserProfile_Resize);
            this.containerPanel.ResumeLayout(false);
            this.containerPanel.PerformLayout();
            this.tabPanel.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel containerPanel;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Panel tabPanel;
        private System.Windows.Forms.Button btnCaiDat;
        private System.Windows.Forms.Button btnChinhSua;
        private System.Windows.Forms.Button btnLichSu;
        private System.Windows.Forms.Panel tabUnderline;
        private System.Windows.Forms.Panel contentPanel;
    }
}
