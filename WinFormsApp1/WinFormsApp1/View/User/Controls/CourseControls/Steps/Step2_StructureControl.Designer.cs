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
			pnlCard.Size = new Size(1830, 800);
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
			lblHeader.Size = new Size(1794, 55);
			lblHeader.TabIndex = 0;
            lblHeader.Text = "Course Structure";
			lblHeader.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// btnAddChapter
			// 
			btnAddChapter.CustomizableEdges = customizableEdges1;
			btnAddChapter.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
			btnAddChapter.ForeColor = Color.White;
			btnAddChapter.Image = Properties.Resources.add;
			btnAddChapter.Location = new Point(18, 105);
			btnAddChapter.Name = "btnAddChapter";
			btnAddChapter.ShadowDecoration.CustomizableEdges = customizableEdges2;
			btnAddChapter.Size = new Size(186, 40);
			btnAddChapter.TabIndex = 1;
            btnAddChapter.Text = "Add Chapter";
			// 
			// flpChapters
			// 
			flpChapters.AutoScroll = true;
			flpChapters.Location = new Point(18, 170);
			flpChapters.Name = "flpChapters";
			flpChapters.Size = new Size(1791, 522);
			flpChapters.TabIndex = 2;
			// 
			// btnPrev
			// 
			btnPrev.CustomizableEdges = customizableEdges3;
			btnPrev.FillColor = Color.FromArgb(255, 128, 0);
			btnPrev.Font = new Font("Segoe UI", 10F);
			btnPrev.ForeColor = Color.White;
			btnPrev.Image = Properties.Resources.previous;
			btnPrev.Location = new Point(25, 743);
			btnPrev.Name = "btnPrev";
			btnPrev.ShadowDecoration.CustomizableEdges = customizableEdges4;
			btnPrev.Size = new Size(150, 40);
			btnPrev.TabIndex = 3;
            btnPrev.Text = "Previous";
			// 
			// btnNext
			// 
			btnNext.CustomizableEdges = customizableEdges5;
			btnNext.Font = new Font("Segoe UI", 10F);
			btnNext.ForeColor = Color.White;
			btnNext.Image = Properties.Resources.next;
			btnNext.Location = new Point(183, 743);
			btnNext.Name = "btnNext";
			btnNext.ShadowDecoration.CustomizableEdges = customizableEdges6;
			btnNext.Size = new Size(150, 40);
			btnNext.TabIndex = 4;
            btnNext.Text = "Next";
			// 
			// Step2_StructureControl
			// 
			Controls.Add(pnlCard);
			Name = "Step2_StructureControl";
			Size = new Size(1830, 800);
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