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
			lblInfoTitle = new Label();
			txtInfoTitle = new TextBox();
			lblInfoDesc = new Label();
			txtInfoDesc = new TextBox();
			lblTime = new Label();
			numTime = new NumericUpDown();
			lblMaxAttempts = new Label();
			numMaxAttempts = new NumericUpDown();
			pnlQuestions = new Panel();
			btnAddQuestion = new Button();
			btnDeleteContent = new Button();
			SuspendLayout();
			// 
			// cboContentType
			// 
			cboContentType.DropDownStyle = ComboBoxStyle.DropDownList;
			cboContentType.FormattingEnabled = true;
			cboContentType.Items.AddRange(new object[] { "Theory", "Video", "FlashcardSet", "Test" });
			cboContentType.Location = new Point(8, 8);
			cboContentType.Name = "cboContentType";
			cboContentType.Size = new Size(314, 33);
			cboContentType.TabIndex = 0;
			// 
			// lblTitle
			// 
			lblTitle.AutoSize = true;
			lblTitle.Location = new Point(8, 44);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new Size(73, 25);
			lblTitle.TabIndex = 1;
			lblTitle.Text = "Tiêu đề:";
			// 
			// txtTitle
			// 
			txtTitle.Location = new Point(96, 40);
			txtTitle.Name = "txtTitle";
			txtTitle.Size = new Size(560, 31);
			txtTitle.TabIndex = 2;
			// 
			// lblInfoTitle
			// 
			lblInfoTitle.AutoSize = true;
			lblInfoTitle.Location = new Point(8, 76);
			lblInfoTitle.Name = "lblInfoTitle";
			lblInfoTitle.Size = new Size(160, 25);
			lblInfoTitle.TabIndex = 3;
			lblInfoTitle.Text = "Tiêu đề bài test";
			// 
			// txtInfoTitle
			// 
			txtInfoTitle.Location = new Point(8, 104);
			txtInfoTitle.Name = "txtInfoTitle";
			txtInfoTitle.Size = new Size(648, 31);
			txtInfoTitle.TabIndex = 4;
			// 
			// lblInfoDesc
			// 
			lblInfoDesc.AutoSize = true;
			lblInfoDesc.Location = new Point(8, 136);
			lblInfoDesc.Name = "lblInfoDesc";
			lblInfoDesc.Size = new Size(160, 25);
			lblInfoDesc.TabIndex = 5;
			lblInfoDesc.Text = "Mô tả bài test";
			// 
			// txtInfoDesc
			// 
			txtInfoDesc.Location = new Point(8, 164);
			txtInfoDesc.Multiline = true;
			txtInfoDesc.Name = "txtInfoDesc";
			txtInfoDesc.Size = new Size(648, 80);
			txtInfoDesc.TabIndex = 6;
			// 
			// lblTime
			// 
			lblTime.AutoSize = true;
			lblTime.Location = new Point(8, 256);
			lblTime.Name = "lblTime";
			lblTime.Size = new Size(160, 25);
			lblTime.TabIndex = 7;
			lblTime.Text = "Thời gian (phút):";
			// 
			// numTime
			// 
			numTime.Location = new Point(8, 284);
			numTime.Name = "numTime";
			numTime.Size = new Size(160, 30);
			numTime.Minimum = 1;
			numTime.Maximum = 1440;
			numTime.Value = 30;
			// 
			// lblMaxAttempts
			// 
			lblMaxAttempts.AutoSize = true;
			lblMaxAttempts.Location = new Point(184, 256);
			lblMaxAttempts.Name = "lblMaxAttempts";
			lblMaxAttempts.Size = new Size(160, 25);
			lblMaxAttempts.TabIndex = 8;
			lblMaxAttempts.Text = "Số lần làm tối đa:";
			// 
			// numMaxAttempts
			// 
			numMaxAttempts.Location = new Point(184, 284);
			numMaxAttempts.Name = "numMaxAttempts";
			numMaxAttempts.Size = new Size(160, 30);
			numMaxAttempts.Minimum = 1;
			numMaxAttempts.Maximum = 100;
			numMaxAttempts.Value = 3;
			// 
			// pnlQuestions
			// 
			pnlQuestions.Location = new Point(8, 320);
			pnlQuestions.Name = "pnlQuestions";
			pnlQuestions.Size = new Size(648, 300);
			pnlQuestions.AutoScroll = true;
			// 
			// btnAddQuestion
			// 
			btnAddQuestion.BackColor = Color.FromArgb(33, 150, 243);
			btnAddQuestion.FlatAppearance.BorderSize = 0;
			btnAddQuestion.FlatStyle = FlatStyle.Flat;
			btnAddQuestion.ForeColor = Color.White;
			btnAddQuestion.Location = new Point(8, 632);
			btnAddQuestion.Name = "btnAddQuestion";
			btnAddQuestion.Size = new Size(648, 40);
			btnAddQuestion.TabIndex = 9;
			btnAddQuestion.Text = "+ Thêm câu hỏi";
			btnAddQuestion.UseVisualStyleBackColor = false;
			// 
			// btnDeleteContent
			// 
			btnDeleteContent.BackColor = Color.FromArgb(244, 67, 54);
			btnDeleteContent.FlatAppearance.BorderSize = 0;
			btnDeleteContent.FlatStyle = FlatStyle.Flat;
			btnDeleteContent.ForeColor = Color.White;
			btnDeleteContent.Location = new Point(576, 8);
			btnDeleteContent.Name = "btnDeleteContent";
			btnDeleteContent.Size = new Size(80, 30);
			btnDeleteContent.TabIndex = 10;
			btnDeleteContent.Text = "Xóa";
			btnDeleteContent.UseVisualStyleBackColor = false;
			// 
			// ContentTestControl
			// 
			AutoScaleMode = AutoScaleMode.None;
			BackColor = Color.White;
			Controls.Add(btnDeleteContent);
			Controls.Add(btnAddQuestion);
			Controls.Add(pnlQuestions);
			Controls.Add(numMaxAttempts);
			Controls.Add(lblMaxAttempts);
			Controls.Add(numTime);
			Controls.Add(lblTime);
			Controls.Add(txtInfoDesc);
			Controls.Add(lblInfoDesc);
			Controls.Add(txtInfoTitle);
			Controls.Add(lblInfoTitle);
			Controls.Add(txtTitle);
			Controls.Add(lblTitle);
			Controls.Add(cboContentType);
			Name = "ContentTestControl";
			Size = new Size(670, 680);
			ResumeLayout(false);
			PerformLayout();
		}
		#endregion

		private System.Windows.Forms.ComboBox cboContentType;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label lblInfoTitle;
        private System.Windows.Forms.TextBox txtInfoTitle;
        private System.Windows.Forms.Label lblInfoDesc;
        private System.Windows.Forms.TextBox txtInfoDesc;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.NumericUpDown numTime;
        private System.Windows.Forms.Label lblMaxAttempts;
        private System.Windows.Forms.NumericUpDown numMaxAttempts;
        private System.Windows.Forms.Panel pnlQuestions;
        private System.Windows.Forms.Button btnAddQuestion;
        private System.Windows.Forms.Button btnDeleteContent;
    }
}