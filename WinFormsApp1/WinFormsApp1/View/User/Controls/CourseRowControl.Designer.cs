namespace WinFormsApp1.View.User.Controls
{
    partial class CourseRowControl
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
			lblId = new Label();
			picCover = new PictureBox();
			lblTitle = new Label();
			lblModeration = new Label();
			lblStatus = new Label();
			lblPrice = new Label();
			lblDate = new Label();
			btnSubmit = new Button();
			btnView = new Button();
			btnEdit = new Button();
			btnDelete = new Button();
			lblCategory = new Label();
			((System.ComponentModel.ISupportInitialize)picCover).BeginInit();
			SuspendLayout();
			// 
			// lblId
			// 
			lblId.Font = new Font("Segoe UI", 9F);
			lblId.Location = new Point(12, 1);
			lblId.Name = "lblId";
			lblId.Size = new Size(50, 90);
			lblId.TabIndex = 0;
			lblId.Text = "1";
			lblId.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// picCover
			// 
			picCover.Location = new Point(82, 7);
			picCover.Name = "picCover";
			picCover.Size = new Size(122, 84);
			picCover.SizeMode = PictureBoxSizeMode.Zoom;
			picCover.TabIndex = 1;
			picCover.TabStop = false;
			// 
			// lblTitle
			// 
			lblTitle.AutoEllipsis = true;
			lblTitle.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
			lblTitle.Location = new Point(239, 29);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new Size(301, 40);
			lblTitle.TabIndex = 2;
			lblTitle.Text = "Course Title";
			lblTitle.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// lblModeration
			// 
			lblModeration.BackColor = Color.FromArgb(108, 117, 125);
			lblModeration.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
			lblModeration.ForeColor = Color.White;
			lblModeration.Location = new Point(1210, 29);
			lblModeration.Name = "lblModeration";
			lblModeration.Size = new Size(130, 40);
			lblModeration.TabIndex = 3;
			lblModeration.Text = "Chưa gửi";
			lblModeration.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// lblStatus
			// 
			lblStatus.BackColor = Color.FromArgb(108, 117, 125);
			lblStatus.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
			lblStatus.ForeColor = Color.White;
			lblStatus.Location = new Point(1366, 29);
			lblStatus.Name = "lblStatus";
			lblStatus.Size = new Size(130, 40);
			lblStatus.TabIndex = 4;
			lblStatus.Text = "Nháp";
			lblStatus.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// lblPrice
			// 
			lblPrice.Font = new Font("Segoe UI", 9F);
			lblPrice.Location = new Point(806, 27);
			lblPrice.Name = "lblPrice";
			lblPrice.Size = new Size(197, 38);
			lblPrice.TabIndex = 5;
			lblPrice.Text = "0 VNĐ";
			lblPrice.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// lblDate
			// 
			lblDate.Font = new Font("Segoe UI", 9F);
			lblDate.Location = new Point(989, 27);
			lblDate.Name = "lblDate";
			lblDate.Size = new Size(162, 38);
			lblDate.TabIndex = 6;
			lblDate.Text = "01/01/2024";
			lblDate.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// btnSubmit
			// 
			btnSubmit.BackColor = Color.FromArgb(52, 144, 220);
			btnSubmit.Cursor = Cursors.Hand;
			btnSubmit.FlatAppearance.BorderSize = 0;
			btnSubmit.FlatStyle = FlatStyle.Flat;
			btnSubmit.Font = new Font("Segoe UI", 10F);
			btnSubmit.ForeColor = Color.White;
			btnSubmit.Location = new Point(1538, 29);
			btnSubmit.Name = "btnSubmit";
			btnSubmit.Size = new Size(50, 40);
			btnSubmit.TabIndex = 7;
			btnSubmit.Text = "📤";
			btnSubmit.UseVisualStyleBackColor = false;
			btnSubmit.Click += BtnSubmit_Click;
			// 
			// btnView
			// 
			btnView.BackColor = Color.FromArgb(52, 144, 220);
			btnView.Cursor = Cursors.Hand;
			btnView.FlatAppearance.BorderSize = 0;
			btnView.FlatStyle = FlatStyle.Flat;
			btnView.Font = new Font("Segoe UI", 10F);
			btnView.ForeColor = Color.White;
			btnView.Location = new Point(1594, 29);
			btnView.Name = "btnView";
			btnView.Size = new Size(50, 40);
			btnView.TabIndex = 8;
			btnView.Text = "👁️";
			btnView.UseVisualStyleBackColor = false;
			btnView.Click += BtnView_Click;
			// 
			// btnEdit
			// 
			btnEdit.BackColor = Color.FromArgb(255, 193, 7);
			btnEdit.Cursor = Cursors.Hand;
			btnEdit.FlatAppearance.BorderSize = 0;
			btnEdit.FlatStyle = FlatStyle.Flat;
			btnEdit.Font = new Font("Segoe UI", 10F);
			btnEdit.ForeColor = Color.White;
			btnEdit.Location = new Point(1650, 29);
			btnEdit.Name = "btnEdit";
			btnEdit.Size = new Size(50, 40);
			btnEdit.TabIndex = 9;
			btnEdit.Text = "✏️";
			btnEdit.UseVisualStyleBackColor = false;
			btnEdit.Click += BtnEdit_Click;
			// 
			// btnDelete
			// 
			btnDelete.BackColor = Color.FromArgb(220, 53, 69);
			btnDelete.Cursor = Cursors.Hand;
			btnDelete.FlatAppearance.BorderSize = 0;
			btnDelete.FlatStyle = FlatStyle.Flat;
			btnDelete.Font = new Font("Segoe UI", 10F);
			btnDelete.ForeColor = Color.White;
			btnDelete.Location = new Point(1706, 29);
			btnDelete.Name = "btnDelete";
			btnDelete.Size = new Size(50, 40);
			btnDelete.TabIndex = 10;
			btnDelete.Text = "🗑️";
			btnDelete.UseVisualStyleBackColor = false;
			btnDelete.Click += BtnDelete_Click;
			// 
			// lblCategory
			// 
			lblCategory.AutoEllipsis = true;
			lblCategory.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
			lblCategory.Location = new Point(565, 28);
			lblCategory.Name = "lblCategory";
			lblCategory.Size = new Size(222, 40);
			lblCategory.TabIndex = 11;
			lblCategory.Text = "Course Title";
			lblCategory.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// CourseRowControl
			// 
			AutoScaleDimensions = new SizeF(10F, 25F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.White;
			BorderStyle = BorderStyle.FixedSingle;
			Controls.Add(lblCategory);
			Controls.Add(btnDelete);
			Controls.Add(btnEdit);
			Controls.Add(btnView);
			Controls.Add(btnSubmit);
			Controls.Add(lblDate);
			Controls.Add(lblPrice);
			Controls.Add(lblStatus);
			Controls.Add(lblModeration);
			Controls.Add(lblTitle);
			Controls.Add(picCover);
			Controls.Add(lblId);
			Margin = new Padding(0, 1, 0, 0);
			Name = "CourseRowControl";
			Size = new Size(1814, 90);
			Load += CourseRowControl_Load;
			MouseEnter += CourseRowControl_MouseEnter;
			MouseLeave += CourseRowControl_MouseLeave;
			((System.ComponentModel.ISupportInitialize)picCover).EndInit();
			ResumeLayout(false);
		}

		#endregion

		private Label lblId;
        private PictureBox picCover;
        private Label lblTitle;
        private Label lblModeration;
        private Label lblStatus;
        private Label lblPrice;
        private Label lblDate;
        private Button btnSubmit;
        private Button btnView;
        private Button btnEdit;
        private Button btnDelete;
		private Label lblCategory;
	}
}
