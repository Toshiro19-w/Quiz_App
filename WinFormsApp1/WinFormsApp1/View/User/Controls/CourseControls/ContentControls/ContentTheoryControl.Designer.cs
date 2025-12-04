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
			panel1 = new Panel();
			SuspendLayout();
			// 
			// cboContentType
			// 
			cboContentType.DropDownStyle = ComboBoxStyle.DropDownList;
			cboContentType.Font = new Font("Segoe UI", 12F);
			cboContentType.FormattingEnabled = true;
			cboContentType.Items.AddRange(new object[] { "Lý thuyết", "Video", "Bộ thẻ ghi nhớ", "Bài kiểm tra" });
			cboContentType.Location = new Point(54, 68);
			cboContentType.Name = "cboContentType";
			cboContentType.Size = new Size(301, 40);
			cboContentType.TabIndex = 0;
			// 
			// lblTitle
			// 
			lblTitle.AutoSize = true;
			lblTitle.Font = new Font("Segoe UI", 12F);
			lblTitle.Location = new Point(54, 120);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new Size(99, 32);
			lblTitle.TabIndex = 1;
			lblTitle.Text = "Tiêu đề:";
			// 
			// txtTitle
			// 
			txtTitle.Font = new Font("Segoe UI", 12F);
			txtTitle.Location = new Point(54, 160);
			txtTitle.Name = "txtTitle";
			txtTitle.Size = new Size(1344, 39);
			txtTitle.TabIndex = 2;
			// 
			// lblBody
			// 
			lblBody.AutoSize = true;
			lblBody.Font = new Font("Segoe UI", 12F);
			lblBody.Location = new Point(54, 213);
			lblBody.Name = "lblBody";
			lblBody.Size = new Size(120, 32);
			lblBody.TabIndex = 3;
			lblBody.Text = "Nội dung:";
			// 
			// txtBody
			// 
			txtBody.Font = new Font("Segoe UI", 12F);
			txtBody.Location = new Point(54, 255);
			txtBody.Multiline = true;
			txtBody.Name = "txtBody";
			txtBody.ScrollBars = ScrollBars.Vertical;
			txtBody.Size = new Size(1347, 116);
			txtBody.TabIndex = 4;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new Font("Segoe UI", 12F);
			label1.Location = new Point(54, 27);
			label1.Name = "label1";
			label1.Size = new Size(166, 32);
			label1.TabIndex = 5;
			label1.Text = "Loại nội dung:";
			// 
			// panel1
			// 
			panel1.BackColor = Color.Teal;
			panel1.Dock = DockStyle.Left;
			panel1.Location = new Point(0, 0);
			panel1.Name = "panel1";
			panel1.Size = new Size(10, 400);
			panel1.TabIndex = 6;
			// 
			// ContentTheoryControl
			// 
			AutoScaleMode = AutoScaleMode.None;
			BackColor = Color.White;
			Controls.Add(panel1);
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
		private Panel panel1;
	}
}