namespace WinFormsApp1.View.User.Controls.CourseControls.ContentControls
{
    partial class ContentTheoryControl
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

		private void InitializeComponent()
		{
			cboContentType = new ComboBox();
			lblTitle = new Label();
			txtTitle = new TextBox();
			lblBody = new Label();
			txtBody = new TextBox();
			label1 = new Label();
			SuspendLayout();
			// 
			// cboContentType
			// 
			cboContentType.DropDownStyle = ComboBoxStyle.DropDownList;
			cboContentType.FormattingEnabled = true;
			cboContentType.Items.AddRange(new object[] { "Theory", "Video", "FlashcardSet", "Test" });
			cboContentType.Location = new Point(15, 47);
			cboContentType.Name = "cboContentType";
			cboContentType.Size = new Size(302, 33);
			cboContentType.TabIndex = 0;
			// 
			// lblTitle
			// 
			lblTitle.AutoSize = true;
			lblTitle.Location = new Point(15, 96);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new Size(73, 25);
			lblTitle.TabIndex = 1;
			lblTitle.Text = "Tiêu đề:";
			// 
			// txtTitle
			// 
			txtTitle.Location = new Point(15, 130);
			txtTitle.Name = "txtTitle";
			txtTitle.Size = new Size(692, 31);
			txtTitle.TabIndex = 2;
			// 
			// lblBody
			// 
			lblBody.AutoSize = true;
			lblBody.Location = new Point(12, 176);
			lblBody.Name = "lblBody";
			lblBody.Size = new Size(91, 25);
			lblBody.TabIndex = 3;
			lblBody.Text = "Nội dung:";
			// 
			// txtBody
			// 
			txtBody.Location = new Point(12, 204);
			txtBody.Multiline = true;
			txtBody.Name = "txtBody";
			txtBody.ScrollBars = ScrollBars.Vertical;
			txtBody.Size = new Size(764, 100);
			txtBody.TabIndex = 4;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(15, 13);
			label1.Name = "label1";
			label1.Size = new Size(125, 25);
			label1.TabIndex = 5;
			label1.Text = "Loại nội dung:";
			// 
			// ContentTheoryControl
			// 
			AutoScaleMode = AutoScaleMode.None;
			Controls.Add(label1);
			Controls.Add(txtBody);
			Controls.Add(lblBody);
			Controls.Add(txtTitle);
			Controls.Add(lblTitle);
			Controls.Add(cboContentType);
			Name = "ContentTheoryControl";
			Size = new Size(824, 337);
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private System.Windows.Forms.ComboBox cboContentType;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label lblBody;
        private System.Windows.Forms.TextBox txtBody;
		private Label label1;
	}
}