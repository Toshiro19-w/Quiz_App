namespace WinFormsApp1.View.User.Controls.CourseControls.Steps
{
	partial class ContentItemControl
	{
		private System.ComponentModel.IContainer components = null;

		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			lblIcon = new Label();
			lblTitle = new Label();
			lblType = new Label();
			btnDelete = new Button();
			SuspendLayout();
			// 
			// lblIcon
			// 
			lblIcon.Cursor = Cursors.Hand;
			lblIcon.Font = new Font("Segoe UI", 14F);
			lblIcon.Location = new Point(16, 10);
			lblIcon.Margin = new Padding(4, 0, 4, 0);
			lblIcon.Name = "lblIcon";
			lblIcon.Size = new Size(36, 67);
			lblIcon.TabIndex = 0;
			lblIcon.Text = "📄";
			lblIcon.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// lblTitle
			// 
			lblTitle.AutoEllipsis = true;
			lblTitle.Cursor = Cursors.Hand;
			lblTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			lblTitle.Location = new Point(58, 13);
			lblTitle.Margin = new Padding(4, 0, 4, 0);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new Size(214, 33);
			lblTitle.TabIndex = 1;
			lblTitle.Text = "Nội dung";
			// 
			// lblType
			// 
			lblType.Cursor = Cursors.Hand;
			lblType.Font = new Font("Segoe UI", 8F);
			lblType.ForeColor = Color.Gray;
			lblType.Location = new Point(58, 51);
			lblType.Margin = new Padding(4, 0, 4, 0);
			lblType.Name = "lblType";
			lblType.Size = new Size(214, 25);
			lblType.TabIndex = 2;
			lblType.Text = "Loại nội dung";
			// 
			// btnDelete
			// 
			btnDelete.BackColor = Color.Transparent;
			btnDelete.Cursor = Cursors.Hand;
			btnDelete.FlatAppearance.BorderSize = 0;
			btnDelete.FlatStyle = FlatStyle.Flat;
			btnDelete.Font = new Font("Segoe UI", 10F);
			btnDelete.ForeColor = Color.FromArgb(220, 53, 69);
			btnDelete.Image = Properties.Resources.delete;
			btnDelete.Location = new Point(279, 21);
			btnDelete.Margin = new Padding(4, 5, 4, 5);
			btnDelete.Name = "btnDelete";
			btnDelete.Size = new Size(37, 50);
			btnDelete.TabIndex = 3;
			btnDelete.UseVisualStyleBackColor = false;
			// 
			// ContentItemControl
			// 
			AutoScaleDimensions = new SizeF(10F, 25F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.White;
			BorderStyle = BorderStyle.FixedSingle;
			Controls.Add(btnDelete);
			Controls.Add(lblType);
			Controls.Add(lblTitle);
			Controls.Add(lblIcon);
			Cursor = Cursors.Hand;
			Margin = new Padding(7, 3, 7, 3);
			Name = "ContentItemControl";
			Size = new Size(320, 88);
			ResumeLayout(false);
		}

		private Label lblIcon;
		private Label lblTitle;
		private Label lblType;
		private Button btnDelete;
	}
}
