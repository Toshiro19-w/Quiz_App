namespace WinFormsApp1.View.User.Controls
{
    partial class FlashcardRowControl
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
			lblTitle = new Label();
			lblCardCount = new Label();
			lblVisibility = new Label();
			lblLanguage = new Label();
			lblDate = new Label();
			btnView = new Button();
			btnStudy = new Button();
			btnEdit = new Button();
			btnDelete = new Button();
			SuspendLayout();
			// 
			// lblId
			// 
			lblId.Font = new Font("Segoe UI", 9F);
			lblId.Location = new Point(25, 5);
			lblId.Name = "lblId";
			lblId.Size = new Size(40, 58);
			lblId.TabIndex = 0;
			lblId.Text = "1";
			lblId.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// lblTitle
			// 
			lblTitle.AutoEllipsis = true;
			lblTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			lblTitle.Location = new Point(180, 19);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new Size(400, 30);
			lblTitle.TabIndex = 1;
			lblTitle.Text = "Flashcard Title";
			lblTitle.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// lblCardCount
			// 
			lblCardCount.BackColor = Color.FromArgb(23, 162, 184);
			lblCardCount.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
			lblCardCount.ForeColor = Color.White;
			lblCardCount.Location = new Point(626, 14);
			lblCardCount.Name = "lblCardCount";
			lblCardCount.Size = new Size(80, 40);
			lblCardCount.TabIndex = 2;
			lblCardCount.Text = "0 thẻ";
			lblCardCount.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// lblVisibility
			// 
			lblVisibility.BackColor = Color.FromArgb(108, 117, 125);
			lblVisibility.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
			lblVisibility.ForeColor = Color.White;
			lblVisibility.Location = new Point(749, 14);
			lblVisibility.Name = "lblVisibility";
			lblVisibility.Size = new Size(100, 40);
			lblVisibility.TabIndex = 3;
			lblVisibility.Text = "Riêng tư";
			lblVisibility.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// lblLanguage
			// 
			lblLanguage.Font = new Font("Segoe UI", 9F);
			lblLanguage.Location = new Point(904, 24);
			lblLanguage.Name = "lblLanguage";
			lblLanguage.Size = new Size(128, 20);
			lblLanguage.TabIndex = 4;
			lblLanguage.Text = "vi";
			lblLanguage.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// lblDate
			// 
			lblDate.Font = new Font("Segoe UI", 9F);
			lblDate.Location = new Point(1110, 24);
			lblDate.Name = "lblDate";
			lblDate.Size = new Size(155, 20);
			lblDate.TabIndex = 5;
			lblDate.Text = "01/01/2024";
			lblDate.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// btnView
			// 
			btnView.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			btnView.BackColor = Color.FromArgb(52, 144, 220);
			btnView.Cursor = Cursors.Hand;
			btnView.FlatAppearance.BorderSize = 0;
			btnView.FlatStyle = FlatStyle.Flat;
			btnView.Font = new Font("Segoe UI", 10F);
			btnView.ForeColor = Color.White;
			btnView.Image = Properties.Resources.view;
			btnView.Location = new Point(1333, 12);
			btnView.Name = "btnView";
			btnView.Size = new Size(50, 40);
			btnView.TabIndex = 6;
			btnView.UseVisualStyleBackColor = false;
			btnView.Click += BtnView_Click;
			// 
			// btnStudy
			// 
			btnStudy.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			btnStudy.BackColor = Color.FromArgb(52, 144, 220);
			btnStudy.Cursor = Cursors.Hand;
			btnStudy.FlatAppearance.BorderSize = 0;
			btnStudy.FlatStyle = FlatStyle.Flat;
			btnStudy.Font = new Font("Segoe UI", 10F);
			btnStudy.ForeColor = Color.White;
			btnStudy.Location = new Point(1389, 12);
			btnStudy.Name = "btnStudy";
			btnStudy.Size = new Size(50, 40);
			btnStudy.TabIndex = 7;
			btnStudy.Text = "▶️";
			btnStudy.UseVisualStyleBackColor = false;
			btnStudy.Click += BtnStudy_Click;
			// 
			// btnEdit
			// 
			btnEdit.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			btnEdit.BackColor = Color.FromArgb(255, 193, 7);
			btnEdit.Cursor = Cursors.Hand;
			btnEdit.FlatAppearance.BorderSize = 0;
			btnEdit.FlatStyle = FlatStyle.Flat;
			btnEdit.Font = new Font("Segoe UI", 10F);
			btnEdit.ForeColor = Color.White;
			btnEdit.Image = Properties.Resources.edit;
			btnEdit.Location = new Point(1445, 12);
			btnEdit.Name = "btnEdit";
			btnEdit.Size = new Size(50, 40);
			btnEdit.TabIndex = 8;
			btnEdit.UseVisualStyleBackColor = false;
			btnEdit.Click += BtnEdit_Click;
			// 
			// btnDelete
			// 
			btnDelete.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			btnDelete.BackColor = Color.FromArgb(220, 53, 69);
			btnDelete.Cursor = Cursors.Hand;
			btnDelete.FlatAppearance.BorderSize = 0;
			btnDelete.FlatStyle = FlatStyle.Flat;
			btnDelete.Font = new Font("Segoe UI", 10F);
			btnDelete.ForeColor = Color.White;
			btnDelete.Image = Properties.Resources.delete_white;
			btnDelete.Location = new Point(1501, 12);
			btnDelete.Name = "btnDelete";
			btnDelete.Size = new Size(50, 40);
			btnDelete.TabIndex = 9;
			btnDelete.UseVisualStyleBackColor = false;
			btnDelete.Click += BtnDelete_Click;
			// 
			// FlashcardRowControl
			// 
			AutoScaleDimensions = new SizeF(10F, 25F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.White;
			BorderStyle = BorderStyle.FixedSingle;
			Controls.Add(btnDelete);
			Controls.Add(btnEdit);
			Controls.Add(btnStudy);
			Controls.Add(btnView);
			Controls.Add(lblDate);
			Controls.Add(lblLanguage);
			Controls.Add(lblVisibility);
			Controls.Add(lblCardCount);
			Controls.Add(lblTitle);
			Controls.Add(lblId);
			Margin = new Padding(0, 1, 0, 0);
			Name = "FlashcardRowControl";
			Size = new Size(1628, 68);
			MouseEnter += FlashcardRowControl_MouseEnter;
			MouseLeave += FlashcardRowControl_MouseLeave;
			ResumeLayout(false);
		}

		#endregion

		private Label lblId;
        private Label lblTitle;
        private Label lblCardCount;
        private Label lblVisibility;
        private Label lblLanguage;
        private Label lblDate;
        private Button btnView;
        private Button btnStudy;
        private Button btnEdit;
        private Button btnDelete;
    }
}
