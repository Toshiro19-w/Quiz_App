namespace WinFormsApp1.View.User.Controls
{
    partial class CourseCardControl
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
			pnlCard = new Panel();
			picCover = new PictureBox();
			pnlBody = new Panel();
			lblTitle = new Label();
			lblInstructor = new Label();
			lblRating = new Label();
			lblPrice = new Label();
			pnlActions = new Panel();
			btnAddToCart = new Button();
			btnView = new Button();
			pnlCard.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)picCover).BeginInit();
			pnlBody.SuspendLayout();
			pnlActions.SuspendLayout();
			SuspendLayout();
			// 
			// pnlCard
			// 
			pnlCard.BackColor = Color.White;
			pnlCard.BorderStyle = BorderStyle.FixedSingle;
			pnlCard.Controls.Add(picCover);
			pnlCard.Controls.Add(pnlBody);
			pnlCard.Cursor = Cursors.Hand;
			pnlCard.Location = new Point(0, 0);
			pnlCard.Name = "pnlCard";
			pnlCard.Size = new Size(330, 380);
			pnlCard.TabIndex = 0;
			// 
			// picCover
			// 
			picCover.Location = new Point(1, 1);
			picCover.Name = "picCover";
			picCover.Size = new Size(328, 180);
			picCover.SizeMode = PictureBoxSizeMode.Zoom;
			picCover.TabIndex = 0;
			picCover.TabStop = false;
			// 
			// pnlBody
			// 
			pnlBody.Controls.Add(lblTitle);
			pnlBody.Controls.Add(lblInstructor);
			pnlBody.Controls.Add(lblRating);
			pnlBody.Controls.Add(lblPrice);
			pnlBody.Controls.Add(pnlActions);
			pnlBody.Location = new Point(1, 182);
			pnlBody.Name = "pnlBody";
			pnlBody.Padding = new Padding(12);
			pnlBody.Size = new Size(328, 196);
			pnlBody.TabIndex = 1;
			// 
			// lblTitle
			// 
			lblTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
			lblTitle.Location = new Point(10, 6);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new Size(310, 35);
			lblTitle.TabIndex = 0;
			lblTitle.Text = "Tiêu đề khóa học";
			// 
			// lblInstructor
			// 
			lblInstructor.Font = new Font("Segoe UI", 9F);
			lblInstructor.ForeColor = Color.Gray;
			lblInstructor.Location = new Point(10, 41);
			lblInstructor.Name = "lblInstructor";
			lblInstructor.Size = new Size(200, 29);
			lblInstructor.TabIndex = 1;
			lblInstructor.Text = "Giảng viên: ...";
			// 
			// lblRating
			// 
			lblRating.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
			lblRating.ForeColor = Color.FromArgb(255, 193, 7);
			lblRating.Location = new Point(10, 74);
			lblRating.Name = "lblRating";
			lblRating.Size = new Size(200, 28);
			lblRating.TabIndex = 2;
			lblRating.Text = "4.8 ★★★★☆ (4)";
			// 
			// lblPrice
			// 
			lblPrice.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
			lblPrice.ForeColor = Color.FromArgb(33, 150, 243);
			lblPrice.Location = new Point(10, 102);
			lblPrice.Name = "lblPrice";
			lblPrice.Size = new Size(200, 31);
			lblPrice.TabIndex = 3;
			lblPrice.Text = "299,000 đ";
			// 
			// pnlActions
			// 
			pnlActions.Controls.Add(btnAddToCart);
			pnlActions.Controls.Add(btnView);
			pnlActions.Dock = DockStyle.Bottom;
			pnlActions.Location = new Point(12, 136);
			pnlActions.Name = "pnlActions";
			pnlActions.Size = new Size(304, 48);
			pnlActions.TabIndex = 4;
			// 
			// btnAddToCart
			// 
			btnAddToCart.BackColor = Color.FromArgb(33, 150, 243);
			btnAddToCart.FlatAppearance.BorderSize = 0;
			btnAddToCart.FlatStyle = FlatStyle.Flat;
			btnAddToCart.ForeColor = Color.White;
			btnAddToCart.Location = new Point(265, 3);
			btnAddToCart.Name = "btnAddToCart";
			btnAddToCart.Size = new Size(36, 36);
			btnAddToCart.TabIndex = 1;
			btnAddToCart.Text = "\U0001f6d2";
			btnAddToCart.UseVisualStyleBackColor = false;
			// 
			// btnView
			// 
			btnView.BackColor = Color.White;
			btnView.FlatAppearance.BorderColor = Color.FromArgb(33, 150, 243);
			btnView.FlatAppearance.BorderSize = 2;
			btnView.FlatStyle = FlatStyle.Flat;
			btnView.ForeColor = Color.FromArgb(33, 150, 243);
			btnView.Location = new Point(29, 4);
			btnView.Name = "btnView";
			btnView.Size = new Size(222, 35);
			btnView.TabIndex = 0;
			btnView.Text = "Xem chi tiết";
			btnView.UseVisualStyleBackColor = false;
			// 
			// CourseCardControl
			// 
			AutoScaleMode = AutoScaleMode.None;
			Controls.Add(pnlCard);
			Name = "CourseCardControl";
			Size = new Size(330, 380);
			pnlCard.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)picCover).EndInit();
			pnlBody.ResumeLayout(false);
			pnlActions.ResumeLayout(false);
			ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnlCard;
        private System.Windows.Forms.PictureBox picCover;
        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblInstructor;
        private System.Windows.Forms.Label lblRating;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Panel pnlActions;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.Button btnAddToCart;
    }
}
