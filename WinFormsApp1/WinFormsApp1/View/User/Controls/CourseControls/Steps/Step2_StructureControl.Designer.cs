namespace WinFormsApp1.View.User.Controls.CourseControls.Steps
{
    partial class Step2_StructureControl
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
			pnlCard = new Guna.UI2.WinForms.Guna2ShadowPanel();
			lblHeader = new Label();
			btnAddChapter = new Guna.UI2.WinForms.Guna2Button();
			flpChapters = new FlowLayoutPanel();
			btnPrev = new Guna.UI2.WinForms.Guna2Button();
			btnNext = new Guna.UI2.WinForms.Guna2Button();
			pnlCard.SuspendLayout();
			SuspendLayout();
			// 
			// pnlCard
			// 
			pnlCard.BackColor = Color.Transparent;
			pnlCard.Controls.Add(lblHeader);
			pnlCard.Controls.Add(btnAddChapter);
			pnlCard.Controls.Add(flpChapters);
			pnlCard.Controls.Add(btnPrev);
			pnlCard.Controls.Add(btnNext);
			pnlCard.Dock = DockStyle.Fill;
			pnlCard.FillColor = Color.White;
			pnlCard.Location = new Point(0, 0);
			pnlCard.Name = "pnlCard";
			pnlCard.Padding = new Padding(18);
			pnlCard.ShadowColor = Color.Black;
			pnlCard.Size = new Size(913, 554);
			pnlCard.TabIndex = 0;
			// 
			// lblHeader
			// 
			lblHeader.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
			lblHeader.Location = new Point(20, 10);
			lblHeader.Name = "lblHeader";
			lblHeader.Size = new Size(707, 43);
			lblHeader.TabIndex = 0;
			lblHeader.Text = "Cấu trúc khóa học";
			// 
			// btnAddChapter
			// 
			btnAddChapter.CustomizableEdges = customizableEdges1;
			btnAddChapter.Font = new Font("Segoe UI", 9F);
			btnAddChapter.ForeColor = Color.White;
			btnAddChapter.Location = new Point(19, 68);
			btnAddChapter.Name = "btnAddChapter";
			btnAddChapter.ShadowDecoration.CustomizableEdges = customizableEdges2;
			btnAddChapter.Size = new Size(171, 36);
			btnAddChapter.TabIndex = 1;
			btnAddChapter.Text = "Thêm chương";
			// 
			// flpChapters
			// 
			flpChapters.AutoScroll = true;
			flpChapters.Location = new Point(19, 118);
			flpChapters.Name = "flpChapters";
			flpChapters.Size = new Size(873, 369);
			flpChapters.TabIndex = 2;
			// 
			// btnPrev
			// 
			btnPrev.CustomizableEdges = customizableEdges3;
			btnPrev.Font = new Font("Segoe UI", 9F);
			btnPrev.ForeColor = Color.White;
			btnPrev.Location = new Point(24, 501);
			btnPrev.Name = "btnPrev";
			btnPrev.ShadowDecoration.CustomizableEdges = customizableEdges4;
			btnPrev.Size = new Size(120, 36);
			btnPrev.TabIndex = 3;
			btnPrev.Text = "Quay lại";
			// 
			// btnNext
			// 
			btnNext.CustomizableEdges = customizableEdges5;
			btnNext.Font = new Font("Segoe UI", 9F);
			btnNext.ForeColor = Color.White;
			btnNext.Location = new Point(164, 501);
			btnNext.Name = "btnNext";
			btnNext.ShadowDecoration.CustomizableEdges = customizableEdges6;
			btnNext.Size = new Size(140, 36);
			btnNext.TabIndex = 4;
			btnNext.Text = "Tiếp tục";
			// 
			// Step2_StructureControl
			// 
			Controls.Add(pnlCard);
			Name = "Step2_StructureControl";
			Size = new Size(913, 554);
			pnlCard.ResumeLayout(false);
			ResumeLayout(false);
		}

		private Guna.UI2.WinForms.Guna2ShadowPanel pnlCard;
        private System.Windows.Forms.Label lblHeader;
        public Guna.UI2.WinForms.Guna2Button btnAddChapter;
        public System.Windows.Forms.FlowLayoutPanel flpChapters;
        public Guna.UI2.WinForms.Guna2Button btnPrev;
        public Guna.UI2.WinForms.Guna2Button btnNext;
    }
}