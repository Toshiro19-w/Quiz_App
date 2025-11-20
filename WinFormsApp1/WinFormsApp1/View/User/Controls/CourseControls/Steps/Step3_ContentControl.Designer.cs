namespace WinFormsApp1.View.User.Controls.CourseControls.Steps
{
    partial class Step3_ContentControl
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }
		private void InitializeComponent()
		{
			Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
			Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
			Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
			Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
			Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
			Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
			Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
			Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
			pnlCard = new Guna.UI2.WinForms.Guna2ShadowPanel();
			lblHeader = new Label();
			cmbLessonSelector = new Guna.UI2.WinForms.Guna2ComboBox();
			btnAddContent = new Guna.UI2.WinForms.Guna2Button();
			flpContents = new FlowLayoutPanel();
			btnPrev = new Guna.UI2.WinForms.Guna2Button();
			btnNext = new Guna.UI2.WinForms.Guna2Button();
			pnlCard.SuspendLayout();
			SuspendLayout();
			// 
			// pnlCard
			// 
			pnlCard.BackColor = Color.Transparent;
			pnlCard.Controls.Add(lblHeader);
			pnlCard.Controls.Add(cmbLessonSelector);
			pnlCard.Controls.Add(btnAddContent);
			pnlCard.Controls.Add(flpContents);
			pnlCard.Controls.Add(btnPrev);
			pnlCard.Controls.Add(btnNext);
			pnlCard.Dock = DockStyle.Fill;
			pnlCard.FillColor = Color.White;
			pnlCard.Location = new Point(0, 0);
			pnlCard.Name = "pnlCard";
			pnlCard.Padding = new Padding(18);
			pnlCard.ShadowColor = Color.Black;
			pnlCard.Size = new Size(1530, 800);
			pnlCard.TabIndex = 0;
			// 
			// lblHeader
			// 
			lblHeader.BackColor = Color.FromArgb(0, 172, 193);
			lblHeader.Dock = DockStyle.Top;
			lblHeader.Font = new Font("Segoe UI", 20F, FontStyle.Bold, GraphicsUnit.Point, 0);
			lblHeader.ForeColor = Color.White;
			lblHeader.Location = new Point(18, 18);
			lblHeader.Name = "lblHeader";
			lblHeader.Size = new Size(1494, 55);
			lblHeader.TabIndex = 0;
			lblHeader.Text = "Nội dung bài học";
			lblHeader.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// cmbLessonSelector
			// 
			cmbLessonSelector.BackColor = Color.Transparent;
			cmbLessonSelector.CustomizableEdges = customizableEdges1;
			cmbLessonSelector.DrawMode = DrawMode.OwnerDrawFixed;
			cmbLessonSelector.DropDownStyle = ComboBoxStyle.DropDownList;
			cmbLessonSelector.FocusedColor = Color.Empty;
			cmbLessonSelector.Font = new Font("Segoe UI", 10F);
			cmbLessonSelector.ForeColor = Color.FromArgb(68, 88, 112);
			cmbLessonSelector.ItemHeight = 30;
			cmbLessonSelector.Location = new Point(19, 83);
			cmbLessonSelector.Name = "cmbLessonSelector";
			cmbLessonSelector.ShadowDecoration.CustomizableEdges = customizableEdges2;
			cmbLessonSelector.Size = new Size(422, 36);
			cmbLessonSelector.TabIndex = 1;
			// 
			// btnAddContent
			// 
			btnAddContent.CustomizableEdges = customizableEdges3;
			btnAddContent.Font = new Font("Segoe UI", 9F);
			btnAddContent.ForeColor = Color.White;
			btnAddContent.Location = new Point(484, 83);
			btnAddContent.Name = "btnAddContent";
			btnAddContent.ShadowDecoration.CustomizableEdges = customizableEdges4;
			btnAddContent.Size = new Size(170, 36);
			btnAddContent.TabIndex = 2;
			btnAddContent.Text = "Thêm nội dung";
			// 
			// flpContents
			// 
			flpContents.AutoScroll = true;
			flpContents.Location = new Point(19, 133);
			flpContents.Name = "flpContents";
			flpContents.Size = new Size(1493, 569);
			flpContents.TabIndex = 3;
			// 
			// btnPrev
			// 
			btnPrev.CustomizableEdges = customizableEdges5;
			btnPrev.FillColor = Color.FromArgb(255, 128, 0);
			btnPrev.Font = new Font("Segoe UI", 10F);
			btnPrev.ForeColor = Color.White;
			btnPrev.Location = new Point(29, 743);
			btnPrev.Name = "btnPrev";
			btnPrev.ShadowDecoration.CustomizableEdges = customizableEdges6;
			btnPrev.Size = new Size(120, 36);
			btnPrev.TabIndex = 4;
			btnPrev.Text = "Quay lại";
			// 
			// btnNext
			// 
			btnNext.CustomizableEdges = customizableEdges7;
			btnNext.Font = new Font("Segoe UI", 10F);
			btnNext.ForeColor = Color.White;
			btnNext.Location = new Point(167, 743);
			btnNext.Name = "btnNext";
			btnNext.ShadowDecoration.CustomizableEdges = customizableEdges8;
			btnNext.Size = new Size(140, 36);
			btnNext.TabIndex = 5;
			btnNext.Text = "Xem trước & Xuất bản";
			// 
			// Step3_ContentControl
			// 
			Controls.Add(pnlCard);
			Name = "Step3_ContentControl";
			Size = new Size(1530, 800);
			pnlCard.ResumeLayout(false);
			ResumeLayout(false);
		}

		private Guna.UI2.WinForms.Guna2ShadowPanel pnlCard;
        private System.Windows.Forms.Label lblHeader;
        public Guna.UI2.WinForms.Guna2ComboBox cmbLessonSelector;
        public Guna.UI2.WinForms.Guna2Button btnAddContent;
        public System.Windows.Forms.FlowLayoutPanel flpContents;
        public Guna.UI2.WinForms.Guna2Button btnPrev;
        public Guna.UI2.WinForms.Guna2Button btnNext;
    }
}