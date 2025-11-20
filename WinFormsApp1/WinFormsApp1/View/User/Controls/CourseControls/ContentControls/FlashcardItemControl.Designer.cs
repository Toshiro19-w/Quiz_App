namespace WinFormsApp1.View.User.Controls.CourseControls.ContentControls
{
    partial class FlashcardItemControl
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

		#region Component Designer generated code
		private void InitializeComponent()
		{
			lblIndex = new Label();
			txtFront = new TextBox();
			txtBack = new TextBox();
			txtHint = new TextBox();
			btnDelete = new Button();
			lblFront = new Label();
			lblBack = new Label();
			lblHint = new Label();
			SuspendLayout();
			// 
			// lblIndex
			// 
			lblIndex.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
			lblIndex.ForeColor = Color.DarkCyan;
			lblIndex.Location = new Point(37, 8);
			lblIndex.Name = "lblIndex";
			lblIndex.Size = new Size(120, 28);
			lblIndex.TabIndex = 0;
			lblIndex.Text = "Thẻ #1";
			// 
			// txtFront
			// 
			txtFront.Font = new Font("Microsoft Sans Serif", 10F);
			txtFront.Location = new Point(37, 82);
			txtFront.Multiline = true;
			txtFront.Name = "txtFront";
			txtFront.Size = new Size(600, 28);
			txtFront.TabIndex = 5;
			// 
			// txtBack
			// 
			txtBack.Font = new Font("Microsoft Sans Serif", 10F);
			txtBack.Location = new Point(756, 82);
			txtBack.Multiline = true;
			txtBack.Name = "txtBack";
			txtBack.Size = new Size(600, 28);
			txtBack.TabIndex = 3;
			// 
			// txtHint
			// 
			txtHint.Font = new Font("Microsoft Sans Serif", 10F);
			txtHint.Location = new Point(37, 149);
			txtHint.Multiline = true;
			txtHint.Name = "txtHint";
			txtHint.Size = new Size(1319, 28);
			txtHint.TabIndex = 1;
			// 
			// btnDelete
			// 
			btnDelete.FlatAppearance.BorderSize = 0;
			btnDelete.FlatStyle = FlatStyle.Flat;
			btnDelete.Location = new Point(1324, 12);
			btnDelete.Name = "btnDelete";
			btnDelete.Size = new Size(32, 28);
			btnDelete.TabIndex = 0;
			btnDelete.Text = "🗑";
			// 
			// lblFront
			// 
			lblFront.Font = new Font("Segoe UI", 10F);
			lblFront.Location = new Point(37, 49);
			lblFront.Name = "lblFront";
			lblFront.Size = new Size(114, 30);
			lblFront.TabIndex = 6;
			lblFront.Text = "Mặt trước:";
			// 
			// lblBack
			// 
			lblBack.Font = new Font("Segoe UI", 10F);
			lblBack.Location = new Point(756, 49);
			lblBack.Name = "lblBack";
			lblBack.Size = new Size(96, 30);
			lblBack.TabIndex = 4;
			lblBack.Text = "Mặt sau:";
			// 
			// lblHint
			// 
			lblHint.Font = new Font("Segoe UI", 10F);
			lblHint.Location = new Point(37, 116);
			lblHint.Name = "lblHint";
			lblHint.Size = new Size(171, 30);
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
			Controls.Add(lblIndex);
			Name = "FlashcardItemControl";
			Size = new Size(1450, 206);
			ResumeLayout(false);
			PerformLayout();
		}
		#endregion

		private System.Windows.Forms.Label lblIndex;
		private System.Windows.Forms.TextBox txtFront;
        private System.Windows.Forms.TextBox txtBack;
        private System.Windows.Forms.TextBox txtHint;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label lblFront;
        private System.Windows.Forms.Label lblBack;
        private System.Windows.Forms.Label lblHint;
    }
}
