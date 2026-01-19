namespace WinFormsApp1.View.User.Components
{
    partial class CheckoutCartItem
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
            if (disposing)
            {
                // Clean up image resources
                if (picCourseImage?.Image != null)
                {
                    picCourseImage.Image.Dispose();
                    picCourseImage.Image = null;
                }
                
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			imgPanel = new Panel();
			picCourseImage = new PictureBox();
			lblImageIcon = new Label();
			lblTitle = new Label();
			lblInstructor = new Label();
			lblDate = new Label();
			lblPrice = new Label();
			btnRemove = new Button();
			imgPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)picCourseImage).BeginInit();
			SuspendLayout();
			// 
			// imgPanel
			// 
			imgPanel.BackColor = Color.FromArgb(240, 240, 240);
			imgPanel.Controls.Add(picCourseImage);
			imgPanel.Controls.Add(lblImageIcon);
			imgPanel.Location = new Point(30, 20);
			imgPanel.Margin = new Padding(4, 5, 4, 5);
			imgPanel.Name = "imgPanel";
			imgPanel.Size = new Size(139, 133);
			imgPanel.TabIndex = 0;
			// 
			// picCourseImage
			// 
			picCourseImage.Dock = DockStyle.Fill;
			picCourseImage.Location = new Point(0, 0);
			picCourseImage.Name = "picCourseImage";
			picCourseImage.Size = new Size(139, 133);
			picCourseImage.SizeMode = PictureBoxSizeMode.Zoom;
			picCourseImage.TabIndex = 1;
			picCourseImage.TabStop = false;
			// 
			// lblImageIcon
			// 
			lblImageIcon.Dock = DockStyle.Fill;
			lblImageIcon.Font = new Font("Segoe UI", 32F);
			lblImageIcon.Location = new Point(0, 0);
			lblImageIcon.Margin = new Padding(4, 0, 4, 0);
			lblImageIcon.Name = "lblImageIcon";
			lblImageIcon.Size = new Size(139, 133);
			lblImageIcon.TabIndex = 0;
			lblImageIcon.Text = "📚";
			lblImageIcon.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// lblTitle
			// 
			lblTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
			lblTitle.ForeColor = Color.FromArgb(33, 33, 33);
			lblTitle.Location = new Point(224, 29);
			lblTitle.Margin = new Padding(4, 0, 4, 0);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new Size(550, 54);
			lblTitle.TabIndex = 1;
			lblTitle.Text = "Course Title";
			// 
			// lblInstructor
			// 
			lblInstructor.AutoSize = true;
			lblInstructor.Font = new Font("Segoe UI", 9F);
			lblInstructor.ForeColor = Color.Gray;
			lblInstructor.Location = new Point(224, 87);
			lblInstructor.Margin = new Padding(4, 0, 4, 0);
			lblInstructor.Name = "lblInstructor";
			lblInstructor.Size = new Size(118, 25);
			lblInstructor.TabIndex = 2;
			lblInstructor.Text = "👤 Instructor";
			// 
			// lblDate
			// 
			lblDate.AutoSize = true;
			lblDate.Font = new Font("Segoe UI", 8F);
			lblDate.ForeColor = Color.LightGray;
			lblDate.Location = new Point(224, 129);
			lblDate.Margin = new Padding(4, 0, 4, 0);
			lblDate.Name = "lblDate";
			lblDate.Size = new Size(139, 21);
			lblDate.TabIndex = 3;
			lblDate.Text = "Thêm vào giỏ: N/A";
			// 
			// lblPrice
			// 
			lblPrice.AutoSize = true;
			lblPrice.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
			lblPrice.ForeColor = Color.FromArgb(0, 102, 255);
			lblPrice.Location = new Point(831, 29);
			lblPrice.Margin = new Padding(4, 0, 4, 0);
			lblPrice.Name = "lblPrice";
			lblPrice.Size = new Size(103, 38);
			lblPrice.TabIndex = 4;
			lblPrice.Text = "0 VND";
			// 
			// btnRemove
			// 
			btnRemove.BackColor = Color.White;
			btnRemove.Cursor = Cursors.Hand;
			btnRemove.FlatAppearance.BorderColor = Color.FromArgb(220, 53, 69);
			btnRemove.FlatStyle = FlatStyle.Flat;
			btnRemove.Font = new Font("Segoe UI", 9F);
			btnRemove.ForeColor = Color.FromArgb(220, 53, 69);
			btnRemove.Image = Properties.Resources.delete;
			btnRemove.Location = new Point(1076, 22);
			btnRemove.Margin = new Padding(4, 5, 4, 5);
			btnRemove.Name = "btnRemove";
			btnRemove.Size = new Size(86, 50);
			btnRemove.TabIndex = 5;
			btnRemove.Text = " Xóa";
			btnRemove.TextImageRelation = TextImageRelation.ImageBeforeText;
			btnRemove.UseVisualStyleBackColor = false;
			btnRemove.Click += btnRemove_Click;
			btnRemove.MouseEnter += btnRemove_MouseEnter;
			btnRemove.MouseLeave += btnRemove_MouseLeave;
			// 
			// CheckoutCartItem
			// 
			AutoScaleDimensions = new SizeF(10F, 25F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.White;
			BorderStyle = BorderStyle.None;
			Controls.Add(btnRemove);
			Controls.Add(lblPrice);
			Controls.Add(lblDate);
			Controls.Add(lblInstructor);
			Controls.Add(lblTitle);
			Controls.Add(imgPanel);
			Margin = new Padding(10, 10, 10, 15);
			Name = "CheckoutCartItem";
			Padding = new Padding(5);
			Size = new Size(1178, 170);
			Paint += CheckoutCartItem_Paint;
			imgPanel.ResumeLayout(false);
			imgPanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)picCourseImage).EndInit();
			ResumeLayout(false);
			PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel imgPanel;
		private System.Windows.Forms.PictureBox picCourseImage;
        private System.Windows.Forms.Label lblImageIcon;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblInstructor;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Button btnRemove;
    }
}
