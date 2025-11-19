namespace WinFormsApp1.View.User.Controls.CourseControls.ContentControls
{
    partial class FlashcardItemControl
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

		#region Component Designer generated code
		private void InitializeComponent()
		{
			txtFront = new TextBox();
			txtBack = new TextBox();
			txtHint = new TextBox();
			btnDelete = new Button();
			lblFront = new Label();
			lblBack = new Label();
			lblHint = new Label();
			SuspendLayout();
			// 
			// txtFront
			// 
			txtFront.Location = new Point(8, 45);
			txtFront.Multiline = true;
			txtFront.Name = "txtFront";
			txtFront.Size = new Size(600, 28);
			txtFront.TabIndex = 5;
			// 
			// txtBack
			// 
			txtBack.Location = new Point(8, 107);
			txtBack.Multiline = true;
			txtBack.Name = "txtBack";
			txtBack.Size = new Size(600, 28);
			txtBack.TabIndex = 3;
			// 
			// txtHint
			// 
			txtHint.Location = new Point(8, 171);
			txtHint.Multiline = true;
			txtHint.Name = "txtHint";
			txtHint.Size = new Size(600, 20);
			txtHint.TabIndex = 1;
			// 
			// btnDelete
			// 
			btnDelete.FlatAppearance.BorderSize = 0;
			btnDelete.FlatStyle = FlatStyle.Flat;
			btnDelete.Location = new Point(613, 6);
			btnDelete.Name = "btnDelete";
			btnDelete.Size = new Size(32, 28);
			btnDelete.TabIndex = 0;
			btnDelete.Text = "🗑";
			// 
			// lblFront
			// 
			lblFront.Location = new Point(8, 4);
			lblFront.Name = "lblFront";
			lblFront.Size = new Size(100, 30);
			lblFront.TabIndex = 6;
			lblFront.Text = "Mặt trước:";
			// 
			// lblBack
			// 
			lblBack.Location = new Point(18, 76);
			lblBack.Name = "lblBack";
			lblBack.Size = new Size(100, 30);
			lblBack.TabIndex = 4;
			lblBack.Text = "Mặt sau:";
			// 
			// lblHint
			// 
			lblHint.Location = new Point(8, 138);
			lblHint.Name = "lblHint";
			lblHint.Size = new Size(100, 30);
			lblHint.TabIndex = 2;
			lblHint.Text = "Gợi ý (tùy chọn):";
			// 
			// FlashcardItemControl
			// 
			AutoScaleMode = AutoScaleMode.None;
			Controls.Add(btnDelete);
			Controls.Add(txtHint);
			Controls.Add(lblHint);
			Controls.Add(txtBack);
			Controls.Add(lblBack);
			Controls.Add(txtFront);
			Controls.Add(lblFront);
			Name = "FlashcardItemControl";
			Size = new Size(660, 204);
			ResumeLayout(false);
			PerformLayout();
		}
		#endregion

		private System.Windows.Forms.TextBox txtFront;
        private System.Windows.Forms.TextBox txtBack;
        private System.Windows.Forms.TextBox txtHint;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label lblFront;
        private System.Windows.Forms.Label lblBack;
        private System.Windows.Forms.Label lblHint;
    }
}
