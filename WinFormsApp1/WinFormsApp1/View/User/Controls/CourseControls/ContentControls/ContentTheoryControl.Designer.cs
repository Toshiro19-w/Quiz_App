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
			cboContentType.Font = new Font("Segoe UI", 10F);
			cboContentType.FormattingEnabled = true;
			cboContentType.Items.AddRange(new object[] { "Theory", "Video", "FlashcardSet", "Test" });
			cboContentType.Location = new Point(54, 76);
			cboContentType.Name = "cboContentType";
			cboContentType.Size = new Size(885, 36);
			cboContentType.TabIndex = 0;
			// 
			// lblTitle
			// 
			lblTitle.AutoSize = true;
			lblTitle.Font = new Font("Segoe UI", 10F);
			lblTitle.Location = new Point(54, 129);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new Size(79, 28);
			lblTitle.TabIndex = 1;
			lblTitle.Text = "Tiêu đề:";
			// 
			// txtTitle
			// 
			txtTitle.Font = new Font("Segoe UI", 10F);
			txtTitle.Location = new Point(54, 163);
			txtTitle.Name = "txtTitle";
			txtTitle.Size = new Size(1344, 34);
			txtTitle.TabIndex = 2;
			// 
			// lblBody
			// 
			lblBody.AutoSize = true;
			lblBody.Font = new Font("Segoe UI", 10F);
			lblBody.Location = new Point(54, 217);
			lblBody.Name = "lblBody";
			lblBody.Size = new Size(99, 28);
			lblBody.TabIndex = 3;
			lblBody.Text = "Nội dung:";
			// 
			// txtBody
			// 
			txtBody.Font = new Font("Segoe UI", 10F);
			txtBody.Location = new Point(54, 257);
			txtBody.Multiline = true;
			txtBody.Name = "txtBody";
			txtBody.ScrollBars = ScrollBars.Vertical;
			txtBody.Size = new Size(1347, 116);
			txtBody.TabIndex = 4;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new Font("Segoe UI", 10F);
			label1.Location = new Point(54, 35);
			label1.Name = "label1";
			label1.Size = new Size(136, 28);
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
			Size = new Size(1450, 400);
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