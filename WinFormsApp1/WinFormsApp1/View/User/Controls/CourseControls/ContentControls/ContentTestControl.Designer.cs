namespace WinFormsApp1.View.User.Controls.CourseControls.ContentControls
{
    partial class ContentTestControl
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

		#region Component Designer generated code
		private void InitializeComponent()
		{
			cboContentType = new ComboBox();
			lblTitle = new Label();
			txtTitle = new TextBox();
			lblInfoDesc = new Label();
			txtInfoDesc = new TextBox();
			lblTime = new Label();
			numTime = new NumericUpDown();
			lblMaxAttempts = new Label();
			numMaxAttempts = new NumericUpDown();
			pnlQuestions = new Panel();
			btnAddQuestion = new Button();
			btnDeleteContent = new Button();
			label1 = new Label();
			leftPanel = new Panel();
			((System.ComponentModel.ISupportInitialize)numTime).BeginInit();
			((System.ComponentModel.ISupportInitialize)numMaxAttempts).BeginInit();
			SuspendLayout();
			// 
			// cboContentType
			// 
			cboContentType.DropDownStyle = ComboBoxStyle.DropDownList;
			cboContentType.Font = new Font("Segoe UI", 12F);
			cboContentType.FormattingEnabled = true;
			cboContentType.Items.AddRange(new object[] { "Lý thuyết", "Video", "Bộ thẻ ghi nhớ", "Bài kiểm tra" });
			cboContentType.Location = new Point(36, 48);
			cboContentType.Name = "cboContentType";
			cboContentType.Size = new Size(250, 40);
			cboContentType.TabIndex = 0;
			// 
			// lblTitle
			// 
			lblTitle.AutoSize = true;
			lblTitle.Font = new Font("Segoe UI", 12F);
			lblTitle.Location = new Point(36, 12);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new Size(166, 32);
			lblTitle.TabIndex = 1;
			lblTitle.Text = "Loại nội dung:";
			// 
			// txtTitle
			// 
			txtTitle.Font = new Font("Segoe UI", 12F);
			txtTitle.Location = new Point(36, 132);
			txtTitle.Name = "txtTitle";
			txtTitle.Size = new Size(560, 39);
			txtTitle.TabIndex = 2;
			// 
			// lblInfoDesc
			// 
			lblInfoDesc.AutoSize = true;
			lblInfoDesc.Font = new Font("Segoe UI", 12F);
			lblInfoDesc.Location = new Point(735, 96);
			lblInfoDesc.Name = "lblInfoDesc";
			lblInfoDesc.Size = new Size(162, 32);
			lblInfoDesc.TabIndex = 5;
			lblInfoDesc.Text = "Mô tả bài test";
			// 
			// txtInfoDesc
			// 
			txtInfoDesc.Font = new Font("Segoe UI", 12F);
			txtInfoDesc.Location = new Point(735, 132);
			txtInfoDesc.Multiline = true;
			txtInfoDesc.Name = "txtInfoDesc";
			txtInfoDesc.Size = new Size(700, 117);
			txtInfoDesc.TabIndex = 6;
			// 
			// lblTime
			// 
			lblTime.AutoSize = true;
			lblTime.Font = new Font("Segoe UI", 12F);
			lblTime.Location = new Point(36, 180);
			lblTime.Name = "lblTime";
			lblTime.Size = new Size(190, 32);
			lblTime.TabIndex = 7;
			lblTime.Text = "Thời gian (phút):";
			// 
			// numTime
			// 
			numTime.Font = new Font("Segoe UI", 12F);
			numTime.Location = new Point(36, 215);
			numTime.Maximum = new decimal(new int[] { 1440, 0, 0, 0 });
			numTime.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
			numTime.Name = "numTime";
			numTime.Size = new Size(261, 39);
			numTime.TabIndex = 13;
			numTime.Value = new decimal(new int[] { 30, 0, 0, 0 });
			// 
			// lblMaxAttempts
			// 
			lblMaxAttempts.AutoSize = true;
			lblMaxAttempts.Font = new Font("Segoe UI", 12F);
			lblMaxAttempts.Location = new Point(375, 180);
			lblMaxAttempts.Name = "lblMaxAttempts";
			lblMaxAttempts.Size = new Size(199, 32);
			lblMaxAttempts.TabIndex = 8;
			lblMaxAttempts.Text = "Số lần làm tối đa:";
			// 
			// numMaxAttempts
			// 
			numMaxAttempts.Font = new Font("Segoe UI", 12F);
			numMaxAttempts.Location = new Point(375, 215);
			numMaxAttempts.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
			numMaxAttempts.Name = "numMaxAttempts";
			numMaxAttempts.Size = new Size(221, 39);
			numMaxAttempts.TabIndex = 12;
			numMaxAttempts.Value = new decimal(new int[] { 3, 0, 0, 0 });
			// 
			// pnlQuestions
			// 
			pnlQuestions.AutoScroll = true;
			pnlQuestions.Font = new Font("Segoe UI", 10F);
			pnlQuestions.Location = new Point(36, 269);
			pnlQuestions.Name = "pnlQuestions";
			pnlQuestions.Size = new Size(1399, 453);
			pnlQuestions.TabIndex = 11;
			// 
			// btnAddQuestion
			// 
			btnAddQuestion.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			btnAddQuestion.BackColor = Color.FromArgb(33, 150, 243);
			btnAddQuestion.FlatAppearance.BorderSize = 0;
			btnAddQuestion.FlatStyle = FlatStyle.Flat;
			btnAddQuestion.Font = new Font("Segoe UI", 10F);
			btnAddQuestion.ForeColor = Color.White;
			btnAddQuestion.Image = Properties.Resources.add;
			btnAddQuestion.Location = new Point(36, 757);
			btnAddQuestion.Name = "btnAddQuestion";
			btnAddQuestion.Size = new Size(190, 40);
			btnAddQuestion.TabIndex = 9;
			btnAddQuestion.Text = "Thêm câu hỏi";
			btnAddQuestion.TextImageRelation = TextImageRelation.ImageBeforeText;
			btnAddQuestion.UseVisualStyleBackColor = false;
			// 
			// btnDeleteContent
			// 
			btnDeleteContent.BackColor = Color.FromArgb(244, 67, 54);
			btnDeleteContent.FlatAppearance.BorderSize = 0;
			btnDeleteContent.FlatStyle = FlatStyle.Flat;
			btnDeleteContent.Font = new Font("Segoe UI", 10F);
			btnDeleteContent.ForeColor = Color.White;
			btnDeleteContent.Location = new Point(1339, 8);
			btnDeleteContent.Name = "btnDeleteContent";
			btnDeleteContent.Size = new Size(96, 35);
			btnDeleteContent.TabIndex = 10;
			btnDeleteContent.Text = "Xóa";
			btnDeleteContent.UseVisualStyleBackColor = false;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new Font("Segoe UI", 12F);
			label1.Location = new Point(36, 97);
			label1.Name = "label1";
			label1.Size = new Size(99, 32);
			label1.TabIndex = 14;
			label1.Text = "Tiêu đề:";
			// 
			// leftPanel
			// 
			leftPanel.BackColor = Color.FromArgb(255, 128, 128);
			leftPanel.Dock = DockStyle.Left;
			leftPanel.Location = new Point(0, 0);
			leftPanel.Name = "leftPanel";
			leftPanel.Size = new Size(10, 800);
			leftPanel.TabIndex = 15;
			// 
			// ContentTestControl
			// 
			AutoScaleMode = AutoScaleMode.None;
			BackColor = Color.White;
			Controls.Add(leftPanel);
			Controls.Add(label1);
			Controls.Add(btnDeleteContent);
			Controls.Add(btnAddQuestion);
			Controls.Add(pnlQuestions);
			Controls.Add(numMaxAttempts);
			Controls.Add(lblMaxAttempts);
			Controls.Add(numTime);
			Controls.Add(lblTime);
			Controls.Add(txtInfoDesc);
			Controls.Add(lblInfoDesc);
			Controls.Add(txtTitle);
			Controls.Add(lblTitle);
			Controls.Add(cboContentType);
			Name = "ContentTestControl";
			Size = new Size(1450, 800);
			((System.ComponentModel.ISupportInitialize)numTime).EndInit();
			((System.ComponentModel.ISupportInitialize)numMaxAttempts).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
		#endregion

		private System.Windows.Forms.ComboBox cboContentType;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label lblInfoDesc;
        private System.Windows.Forms.TextBox txtInfoDesc;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.NumericUpDown numTime;
        private System.Windows.Forms.Label lblMaxAttempts;
        private System.Windows.Forms.NumericUpDown numMaxAttempts;
        private System.Windows.Forms.Panel pnlQuestions;
        private System.Windows.Forms.Button btnAddQuestion;
        private System.Windows.Forms.Button btnDeleteContent;
		private Label label1;
		private Panel leftPanel;
	}
}