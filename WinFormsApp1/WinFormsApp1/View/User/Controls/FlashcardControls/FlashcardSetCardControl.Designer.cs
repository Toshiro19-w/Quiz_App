namespace WinFormsApp1.View.User.Controls.FlashcardControls
{
	partial class FlashcardSetCardControl
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
			btnDetail = new Button();
			btnStudy = new Button();
			lblLanguage = new Label();
			lblAuthor = new Label();
			lblDescription = new Label();
			lblTitle = new Label();
			lblCount = new Label();
			lblIcon = new Label();
			pnlCard.SuspendLayout();
			SuspendLayout();
			// 
			// pnlCard
			// 
			pnlCard.BackColor = Color.FromArgb(124, 77, 255);
			pnlCard.Controls.Add(btnDetail);
			pnlCard.Controls.Add(btnStudy);
			pnlCard.Controls.Add(lblLanguage);
			pnlCard.Controls.Add(lblAuthor);
			pnlCard.Controls.Add(lblDescription);
			pnlCard.Controls.Add(lblTitle);
			pnlCard.Controls.Add(lblCount);
			pnlCard.Controls.Add(lblIcon);
			pnlCard.Cursor = Cursors.Hand;
			pnlCard.Dock = DockStyle.Fill;
			pnlCard.Location = new Point(0, 0);
			pnlCard.Name = "pnlCard";
			pnlCard.Size = new Size(540, 350);
			pnlCard.TabIndex = 0;
			// 
			// btnDetail
			// 
			btnDetail.BackColor = Color.White;
			btnDetail.Cursor = Cursors.Hand;
			btnDetail.FlatAppearance.BorderSize = 0;
			btnDetail.FlatStyle = FlatStyle.Flat;
			btnDetail.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
			btnDetail.ForeColor = Color.FromArgb(124, 77, 255);
			btnDetail.Image = Properties.Resources.eye;
			btnDetail.Location = new Point(70, 300);
			btnDetail.Name = "btnDetail";
			btnDetail.Size = new Size(185, 38);
			btnDetail.TabIndex = 7;
			btnDetail.Text = "Xem chi tiết";
			btnDetail.TextImageRelation = TextImageRelation.ImageBeforeText;
			btnDetail.UseVisualStyleBackColor = false;
			// 
			// btnStudy
			// 
			btnStudy.BackColor = Color.FromArgb(76, 175, 80);
			btnStudy.Cursor = Cursors.Hand;
			btnStudy.FlatAppearance.BorderSize = 0;
			btnStudy.FlatStyle = FlatStyle.Flat;
			btnStudy.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
			btnStudy.ForeColor = Color.White;
			btnStudy.Location = new Point(285, 300);
			btnStudy.Name = "btnStudy";
			btnStudy.Size = new Size(165, 38);
			btnStudy.TabIndex = 8;
			btnStudy.Text = "▶ Học ngay";
			btnStudy.UseVisualStyleBackColor = false;
			// 
			// lblLanguage
			// 
			lblLanguage.BackColor = Color.FromArgb(150, 255, 255, 255);
			lblLanguage.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
			lblLanguage.ForeColor = Color.White;
			lblLanguage.Location = new Point(450, 25);
			lblLanguage.Name = "lblLanguage";
			lblLanguage.Size = new Size(70, 30);
			lblLanguage.TabIndex = 6;
			lblLanguage.Text = "vi";
			lblLanguage.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// lblAuthor
			// 
			lblAuthor.AutoSize = true;
			lblAuthor.BackColor = Color.Transparent;
			lblAuthor.Font = new Font("Segoe UI", 10F);
			lblAuthor.ForeColor = Color.White;
			lblAuthor.Location = new Point(20, 255);
			lblAuthor.Name = "lblAuthor";
			lblAuthor.Size = new Size(127, 28);
			lblAuthor.TabIndex = 5;
			lblAuthor.Text = "👤 Unknown";
			// 
			// lblDescription
			// 
			lblDescription.BackColor = Color.Transparent;
			lblDescription.Font = new Font("Segoe UI", 10F);
			lblDescription.ForeColor = Color.FromArgb(230, 230, 255);
			lblDescription.Location = new Point(20, 210);
			lblDescription.Name = "lblDescription";
			lblDescription.Size = new Size(500, 45);
			lblDescription.TabIndex = 4;
			lblDescription.Text = "Mô tả";
			lblDescription.TextAlign = ContentAlignment.TopCenter;
			// 
			// lblTitle
			// 
			lblTitle.BackColor = Color.Transparent;
			lblTitle.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
			lblTitle.ForeColor = Color.White;
			lblTitle.Location = new Point(20, 149);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new Size(500, 56);
			lblTitle.TabIndex = 3;
			lblTitle.Text = "Flashcard Title";
			lblTitle.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// lblCount
			// 
			lblCount.BackColor = Color.FromArgb(100, 0, 0, 0);
			lblCount.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
			lblCount.ForeColor = Color.White;
			lblCount.Location = new Point(20, 25);
			lblCount.Name = "lblCount";
			lblCount.Size = new Size(110, 35);
			lblCount.TabIndex = 2;
			lblCount.Text = "📇 0 thẻ";
			lblCount.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// lblIcon
			// 
			lblIcon.BackColor = Color.Transparent;
			lblIcon.Font = new Font("Segoe UI", 36F, FontStyle.Regular, GraphicsUnit.Point, 0);
			lblIcon.ForeColor = Color.White;
			lblIcon.Location = new Point(208, 35);
			lblIcon.Name = "lblIcon";
			lblIcon.Size = new Size(131, 105);
			lblIcon.TabIndex = 1;
			lblIcon.Text = "📚";
			lblIcon.TextAlign = ContentAlignment.TopCenter;
			// 
			// FlashcardSetCardControl
			// 
			AutoScaleMode = AutoScaleMode.None;
			Controls.Add(pnlCard);
			Margin = new Padding(15);
			Name = "FlashcardSetCardControl";
			Size = new Size(540, 350);
			pnlCard.ResumeLayout(false);
			pnlCard.PerformLayout();
			ResumeLayout(false);
		}

		#endregion

		private Panel pnlCard;
		private Button btnDetail;
		private Button btnStudy;
		private Label lblLanguage;
		private Label lblAuthor;
		private Label lblDescription;
		private Label lblTitle;
		private Label lblCount;
		private Label lblIcon;
	}
}
