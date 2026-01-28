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
			label2 = new Label();
			lblTitleError = new Label();
			((System.ComponentModel.ISupportInitialize)numTime).BeginInit();
			((System.ComponentModel.ISupportInitialize)numMaxAttempts).BeginInit();
			SuspendLayout();
			// 
			// cboContentType
			// 
			cboContentType.DropDownStyle = ComboBoxStyle.DropDownList;
			cboContentType.Font = new Font("Segoe UI", 12F);
			cboContentType.FormattingEnabled = true;
            cboContentType.Items.AddRange(new object[] { "Theory", "Video", "Flashcard Set", "Quiz" });
			cboContentType.Location = new Point(16, 48);
			cboContentType.Name = "cboContentType";
			cboContentType.Size = new Size(250, 40);
			cboContentType.TabIndex = 0;
			// 
			// lblTitle
			// 
			lblTitle.AutoSize = true;
			lblTitle.Font = new Font("Segoe UI", 12F);
			lblTitle.Location = new Point(16, 12);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new Size(166, 32);
			lblTitle.TabIndex = 1;
            lblTitle.Text = "Content Type:";
			// 
			// txtTitle
			// 
			txtTitle.Font = new Font("Segoe UI", 12F);
			txtTitle.Location = new Point(16, 132);
			txtTitle.Name = "txtTitle";
			txtTitle.Size = new Size(710, 39);
			txtTitle.TabIndex = 2;
			// 
			// lblInfoDesc
			// 
			lblInfoDesc.AutoSize = true;
			lblInfoDesc.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
			lblInfoDesc.Location = new Point(767, 96);
			lblInfoDesc.Name = "lblInfoDesc";
			lblInfoDesc.Size = new Size(210, 32);
			lblInfoDesc.TabIndex = 5;
            lblInfoDesc.Text = "Quiz Description";
			// 
			// txtInfoDesc
			// 
			txtInfoDesc.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			txtInfoDesc.Font = new Font("Segoe UI", 12F);
			txtInfoDesc.Location = new Point(767, 132);
			txtInfoDesc.Multiline = true;
			txtInfoDesc.Name = "txtInfoDesc";
			txtInfoDesc.Size = new Size(567, 117);
			txtInfoDesc.TabIndex = 6;
			// 
			// lblTime
			// 
			lblTime.AutoSize = true;
			lblTime.Font = new Font("Segoe UI", 12F);
			lblTime.Location = new Point(16, 182);
			lblTime.Name = "lblTime";
			lblTime.Size = new Size(190, 32);
			lblTime.TabIndex = 7;
            lblTime.Text = "Time (minutes):";
			// 
			// numTime
			// 
			numTime.Font = new Font("Segoe UI", 12F);
			numTime.Location = new Point(16, 215);
			numTime.Maximum = new decimal(new int[] { 1440, 0, 0, 0 });
			numTime.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
			numTime.Name = "numTime";
			numTime.Size = new Size(235, 39);
			numTime.TabIndex = 13;
			numTime.Value = new decimal(new int[] { 30, 0, 0, 0 });
			// 
			// lblMaxAttempts
			// 
			lblMaxAttempts.AutoSize = true;
			lblMaxAttempts.Font = new Font("Segoe UI", 12F);
			lblMaxAttempts.Location = new Point(384, 185);
			lblMaxAttempts.Name = "lblMaxAttempts";
			lblMaxAttempts.Size = new Size(177, 32);
			lblMaxAttempts.TabIndex = 8;
            lblMaxAttempts.Text = "Max Attempts: ";
			// 
			// numMaxAttempts
			// 
			numMaxAttempts.Font = new Font("Segoe UI", 12F);
			numMaxAttempts.Location = new Point(384, 220);
			numMaxAttempts.Maximum = new decimal(new int[] { 999, 0, 0, 0 });
			numMaxAttempts.Name = "numMaxAttempts";
			numMaxAttempts.Size = new Size(190, 39);
			numMaxAttempts.TabIndex = 12;
			numMaxAttempts.Value = new decimal(new int[] { 3, 0, 0, 0 });
			// 
			// pnlQuestions
			// 
			pnlQuestions.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			pnlQuestions.AutoScroll = true;
			pnlQuestions.Font = new Font("Segoe UI", 10F);
			pnlQuestions.Location = new Point(16, 269);
			pnlQuestions.Name = "pnlQuestions";
			pnlQuestions.Size = new Size(1318, 453);
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
			btnAddQuestion.Location = new Point(16, 757);
			btnAddQuestion.Name = "btnAddQuestion";
			btnAddQuestion.Size = new Size(190, 40);
			btnAddQuestion.TabIndex = 9;
            btnAddQuestion.Text = "Add Question";
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
			btnDeleteContent.Location = new Point(1238, 9);
			btnDeleteContent.Name = "btnDeleteContent";
			btnDeleteContent.Size = new Size(96, 35);
			btnDeleteContent.TabIndex = 10;
            btnDeleteContent.Text = "Delete";
			btnDeleteContent.UseVisualStyleBackColor = false;
			btnDeleteContent.Visible = false;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new Font("Segoe UI", 12F);
			label1.Location = new Point(16, 99);
			label1.Name = "label1";
			label1.Size = new Size(99, 32);
			label1.TabIndex = 14;
            label1.Text = "Title:";
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Font = new Font("Segoe UI Light", 8F, FontStyle.Regular, GraphicsUnit.Point, 0);
			label2.ForeColor = SystemColors.ActiveCaptionText;
			label2.Location = new Point(555, 191);
			label2.Name = "label2";
			label2.Size = new Size(148, 21);
			label2.TabIndex = 15;
            label2.Text = "(0 = Unlimited)";
			// 
			// lblTitleError
			// 
			lblTitleError.AutoSize = true;
			lblTitleError.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
			lblTitleError.ForeColor = Color.Red;
			lblTitleError.Location = new Point(121, 103);
			lblTitleError.Name = "lblTitleError";
			lblTitleError.Size = new Size(199, 25);
			lblTitleError.TabIndex = 16;
            lblTitleError.Text = "* Cannot be empty";
			lblTitleError.Visible = false;
			// 
			// ContentTestControl
			// 
			AutoScaleMode = AutoScaleMode.None;
			BackColor = Color.White;
			Controls.Add(lblTitleError);
			Controls.Add(label2);
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
			Size = new Size(1350, 800);
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
		private Label label2;
		private Label lblTitleError;
	}
}