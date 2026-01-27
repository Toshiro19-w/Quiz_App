namespace WinFormsApp1.View.User.Controls.FlashcardControls
{
	partial class FlashcardItemDisplayControl
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

		#region Component Designer generated code

		private void InitializeComponent()
		{
			lblFront = new Label();
			lblBack = new Label();
			lblStatus = new Label();
			SuspendLayout();
			// 
			// lblFront
			// 
			lblFront.AutoEllipsis = true;
			lblFront.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
			lblFront.ForeColor = Color.FromArgb(0, 102, 102);
			lblFront.Location = new Point(39, 18);
			lblFront.Name = "lblFront";
			lblFront.Size = new Size(850, 35);
			lblFront.TabIndex = 0;
			lblFront.Text = "Front";
			// 
			// lblBack
			// 
			lblBack.AutoEllipsis = true;
			lblBack.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
			lblBack.ForeColor = Color.Gray;
			lblBack.Location = new Point(39, 58);
			lblBack.Name = "lblBack";
			lblBack.Size = new Size(850, 55);
			lblBack.TabIndex = 1;
			lblBack.Text = "Back";
			// 
			// lblStatus
			// 
			lblStatus.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
			lblStatus.ForeColor = Color.FromArgb(76, 175, 80);
			lblStatus.Location = new Point(1000, 45);
			lblStatus.Name = "lblStatus";
			lblStatus.Size = new Size(50, 50);
			lblStatus.TabIndex = 2;
			lblStatus.Text = "✓";
			lblStatus.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// FlashcardItemDisplayControl
			// 
			AutoScaleMode = AutoScaleMode.None;
			BackColor = Color.FromArgb(248, 249, 250);
			BorderStyle = BorderStyle.FixedSingle;
			Controls.Add(lblStatus);
			Controls.Add(lblBack);
			Controls.Add(lblFront);
			Cursor = Cursors.Hand;
			Margin = new Padding(0, 8, 0, 8);
			Name = "FlashcardItemDisplayControl";
			Size = new Size(1150, 130);
			ResumeLayout(false);
		}

		#endregion

		private Label lblFront;
		private Label lblBack;
		private Label lblStatus;
	}
}
