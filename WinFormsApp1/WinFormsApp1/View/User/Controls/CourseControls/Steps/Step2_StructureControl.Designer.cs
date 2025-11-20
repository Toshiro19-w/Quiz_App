namespace WinFormsApp1.View.User.Controls.CourseControls.Steps
{
    partial class Step2_StructureControl
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }
		private void InitializeComponent()
		{
			Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
			Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
			Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
			Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
			Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
			Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
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
			pnlCard.Size = new Size(1530, 800);
			pnlCard.TabIndex = 0;
			// 
			// lblHeader
			// 
			lblHeader.BackColor = Color.FromArgb(0, 172, 193);
			lblHeader.Dock = DockStyle.Top;
			lblHeader.Font = new Font("Segoe UI", 20F, FontStyle.Bold, GraphicsUnit.Point, 0);
			lblHeader.ForeColor = SystemColors.Window;
			lblHeader.Location = new Point(18, 18);
			lblHeader.Name = "lblHeader";
			lblHeader.Size = new Size(1494, 55);
			lblHeader.TabIndex = 0;
			lblHeader.Text = "Cấu trúc khóa học";
			lblHeader.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// btnAddChapter
			// 
			btnAddChapter.CustomizableEdges = customizableEdges7;
			btnAddChapter.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
			btnAddChapter.ForeColor = Color.White;
			btnAddChapter.Location = new Point(18, 105);
			btnAddChapter.Name = "btnAddChapter";
			btnAddChapter.ShadowDecoration.CustomizableEdges = customizableEdges8;
			btnAddChapter.Size = new Size(171, 36);
			btnAddChapter.TabIndex = 1;
			btnAddChapter.Text = "Thêm chương";
			// 
			// flpChapters
			// 
			flpChapters.AutoScroll = true;
			flpChapters.Location = new Point(18, 170);
			flpChapters.Name = "flpChapters";
			flpChapters.Size = new Size(1500, 508);
			flpChapters.TabIndex = 2;
			// 
			// btnPrev
			// 
			btnPrev.CustomizableEdges = customizableEdges9;
			btnPrev.FillColor = Color.FromArgb(255, 128, 0);
			btnPrev.Font = new Font("Segoe UI", 10F);
			btnPrev.ForeColor = Color.White;
			btnPrev.Location = new Point(25, 743);
			btnPrev.Name = "btnPrev";
			btnPrev.ShadowDecoration.CustomizableEdges = customizableEdges10;
			btnPrev.Size = new Size(120, 36);
			btnPrev.TabIndex = 3;
			btnPrev.Text = "Quay lại";
			// 
			// btnNext
			// 
			btnNext.CustomizableEdges = customizableEdges11;
			btnNext.Font = new Font("Segoe UI", 10F);
			btnNext.ForeColor = Color.White;
			btnNext.Location = new Point(165, 743);
			btnNext.Name = "btnNext";
			btnNext.ShadowDecoration.CustomizableEdges = customizableEdges12;
			btnNext.Size = new Size(140, 36);
			btnNext.TabIndex = 4;
			btnNext.Text = "Tiếp tục";
			// 
			// Step2_StructureControl
			// 
			Controls.Add(pnlCard);
			Name = "Step2_StructureControl";
			Size = new Size(1530, 800);
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