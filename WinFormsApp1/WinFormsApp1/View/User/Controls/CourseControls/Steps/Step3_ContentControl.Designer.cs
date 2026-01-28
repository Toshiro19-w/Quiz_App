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
			pnlCard = new Guna.UI2.WinForms.Guna2ShadowPanel();
			btnAddContent = new Guna.UI2.WinForms.Guna2Button();
			lblHeader = new Label();
			cmbLessonSelector = new Guna.UI2.WinForms.Guna2ComboBox();
			splitContainer = new SplitContainer();
			pnlLeftHeader = new Panel();
			lblLeftTitle = new Label();
			flpContentList = new FlowLayoutPanel();
			pnlRightHeader = new Panel();
			lblRightTitle = new Label();
			pnlEditor = new Panel();
			btnPrev = new Guna.UI2.WinForms.Guna2Button();
			btnNext = new Guna.UI2.WinForms.Guna2Button();
			pnlCard.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
			splitContainer.Panel1.SuspendLayout();
			splitContainer.Panel2.SuspendLayout();
			splitContainer.SuspendLayout();
			pnlLeftHeader.SuspendLayout();
			pnlRightHeader.SuspendLayout();
			SuspendLayout();
			// 
			// pnlCard
			// 
			pnlCard.BackColor = Color.Transparent;
			pnlCard.Controls.Add(btnAddContent);
			pnlCard.Controls.Add(lblHeader);
			pnlCard.Controls.Add(cmbLessonSelector);
			pnlCard.Controls.Add(splitContainer);
			pnlCard.Controls.Add(btnPrev);
			pnlCard.Controls.Add(btnNext);
			pnlCard.Dock = DockStyle.Fill;
			pnlCard.FillColor = Color.White;
			pnlCard.Location = new Point(0, 0);
			pnlCard.Name = "pnlCard";
			pnlCard.Padding = new Padding(18);
			pnlCard.ShadowColor = Color.Black;
			pnlCard.Size = new Size(1830, 830);
			pnlCard.TabIndex = 0;
			// 
			// btnAddContent
			// 
			btnAddContent.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			btnAddContent.CustomizableEdges = customizableEdges1;
			btnAddContent.Font = new Font("Segoe UI", 9F);
			btnAddContent.ForeColor = Color.White;
			btnAddContent.Image = Properties.Resources.add;
			btnAddContent.ImageSize = new Size(16, 16);
			btnAddContent.Location = new Point(410, 83);
			btnAddContent.Name = "btnAddContent";
			btnAddContent.ShadowDecoration.CustomizableEdges = customizableEdges2;
			btnAddContent.Size = new Size(150, 36);
			btnAddContent.TabIndex = 1;
            btnAddContent.Text = "Add Lesson";
			// 
			// lblHeader
			// 
			lblHeader.BackColor = Color.FromArgb(0, 172, 193);
			lblHeader.Dock = DockStyle.Top;
			lblHeader.Font = new Font("Segoe UI", 20F, FontStyle.Bold, GraphicsUnit.Point, 0);
			lblHeader.ForeColor = Color.White;
			lblHeader.Location = new Point(18, 18);
			lblHeader.Name = "lblHeader";
			lblHeader.Size = new Size(1794, 55);
			lblHeader.TabIndex = 0;
            lblHeader.Text = "Lesson Content";
			lblHeader.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// cmbLessonSelector
			// 
			cmbLessonSelector.BackColor = Color.Transparent;
			cmbLessonSelector.CustomizableEdges = customizableEdges3;
			cmbLessonSelector.DrawMode = DrawMode.Normal;
			cmbLessonSelector.DropDownStyle = ComboBoxStyle.DropDownList;
			cmbLessonSelector.FocusedColor = Color.Empty;
			cmbLessonSelector.Font = new Font("Segoe UI", 12F);
			cmbLessonSelector.ForeColor = Color.FromArgb(68, 88, 112);
			cmbLessonSelector.ItemHeight = 30;
			cmbLessonSelector.Location = new Point(19, 83);
			cmbLessonSelector.Name = "cmbLessonSelector";
			cmbLessonSelector.ShadowDecoration.CustomizableEdges = customizableEdges4;
			cmbLessonSelector.Size = new Size(350, 36);
			cmbLessonSelector.TabIndex = 1;
			// 
			// splitContainer
			// 
			splitContainer.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			splitContainer.Location = new Point(19, 133);
			splitContainer.Name = "splitContainer";
			// 
			// splitContainer.Panel1
			// 
			splitContainer.Panel1.Controls.Add(pnlLeftHeader);
			splitContainer.Panel1.Controls.Add(flpContentList);
			// 
			// splitContainer.Panel2
			// 
			splitContainer.Panel2.Controls.Add(pnlRightHeader);
			splitContainer.Panel2.Controls.Add(pnlEditor);
			splitContainer.Size = new Size(1793, 608);
			splitContainer.SplitterDistance = 347;
			splitContainer.TabIndex = 2;
			// 
			// pnlLeftHeader
			// 
			pnlLeftHeader.BackColor = Color.FromArgb(240, 248, 255);
			pnlLeftHeader.Controls.Add(lblLeftTitle);
			pnlLeftHeader.Dock = DockStyle.Top;
			pnlLeftHeader.Location = new Point(0, 0);
			pnlLeftHeader.Name = "pnlLeftHeader";
			pnlLeftHeader.Size = new Size(347, 60);
			pnlLeftHeader.TabIndex = 0;
			// 
			// lblLeftTitle
			// 
			lblLeftTitle.AutoSize = true;
			lblLeftTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
			lblLeftTitle.ForeColor = Color.FromArgb(50, 50, 50);
			lblLeftTitle.Location = new Point(10, 20);
			lblLeftTitle.Name = "lblLeftTitle";
			lblLeftTitle.Size = new Size(242, 32);
			lblLeftTitle.TabIndex = 0;
            lblLeftTitle.Text = "Content List";
			// 
			// flpContentList
			// 
			flpContentList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			flpContentList.AutoScroll = true;
			flpContentList.BackColor = Color.FromArgb(248, 249, 250);
			flpContentList.Location = new Point(0, 60);
			flpContentList.Name = "flpContentList";
			flpContentList.Padding = new Padding(5);
			flpContentList.Size = new Size(347, 548);
			flpContentList.TabIndex = 1;
			// 
			// pnlRightHeader
			// 
			pnlRightHeader.BackColor = Color.FromArgb(240, 248, 255);
			pnlRightHeader.Controls.Add(lblRightTitle);
			pnlRightHeader.Dock = DockStyle.Top;
			pnlRightHeader.Location = new Point(0, 0);
			pnlRightHeader.Name = "pnlRightHeader";
			pnlRightHeader.Size = new Size(1442, 60);
			pnlRightHeader.TabIndex = 0;
			// 
			// lblRightTitle
			// 
			lblRightTitle.AutoSize = true;
			lblRightTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
			lblRightTitle.ForeColor = Color.FromArgb(50, 50, 50);
			lblRightTitle.Location = new Point(10, 20);
			lblRightTitle.Name = "lblRightTitle";
			lblRightTitle.Size = new Size(301, 32);
			lblRightTitle.TabIndex = 0;
            lblRightTitle.Text = "Edit Content (0/0)";
			// 
			// pnlEditor
			// 
			pnlEditor.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			pnlEditor.AutoScroll = true;
			pnlEditor.BackColor = Color.White;
			pnlEditor.Location = new Point(0, 60);
			pnlEditor.Name = "pnlEditor";
			pnlEditor.Padding = new Padding(10);
			pnlEditor.Size = new Size(1442, 548);
			pnlEditor.TabIndex = 1;
			// 
			// btnPrev
			// 
			btnPrev.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			btnPrev.CustomizableEdges = customizableEdges5;
			btnPrev.FillColor = Color.FromArgb(255, 128, 0);
			btnPrev.Font = new Font("Segoe UI", 10F);
			btnPrev.ForeColor = Color.White;
			btnPrev.Image = Properties.Resources.previous;
			btnPrev.Location = new Point(21, 769);
			btnPrev.Name = "btnPrev";
			btnPrev.ShadowDecoration.CustomizableEdges = customizableEdges6;
			btnPrev.Size = new Size(150, 40);
			btnPrev.TabIndex = 3;
            btnPrev.Text = "Previous";
			// 
			// btnNext
			// 
			btnNext.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			btnNext.CustomizableEdges = customizableEdges5;
			btnNext.Font = new Font("Segoe UI", 10F);
			btnNext.ForeColor = Color.White;
			btnNext.Image = Properties.Resources.next;
			btnNext.ImageAlign = HorizontalAlignment.Left;
			btnNext.Location = new Point(177, 769);
			btnNext.Name = "btnNext";
			btnNext.ShadowDecoration.CustomizableEdges = customizableEdges6;
			btnNext.Size = new Size(149, 40);
			btnNext.TabIndex = 4;
            btnNext.Text = "Preview";
			btnNext.TextAlign = HorizontalAlignment.Left;
			// 
			// Step3_ContentControl
			// 
			Controls.Add(pnlCard);
			Name = "Step3_ContentControl";
			Size = new Size(1830, 830);
			pnlCard.ResumeLayout(false);
			splitContainer.Panel1.ResumeLayout(false);
			splitContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
			splitContainer.ResumeLayout(false);
			pnlLeftHeader.ResumeLayout(false);
			pnlLeftHeader.PerformLayout();
			pnlRightHeader.ResumeLayout(false);
			pnlRightHeader.PerformLayout();
			ResumeLayout(false);
		}

		private Guna.UI2.WinForms.Guna2ShadowPanel pnlCard;
        private System.Windows.Forms.Label lblHeader;
        public Guna.UI2.WinForms.Guna2ComboBox cmbLessonSelector;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Panel pnlLeftHeader;
        public Guna.UI2.WinForms.Guna2Button btnAddContent;
        private System.Windows.Forms.Label lblLeftTitle;
        public System.Windows.Forms.FlowLayoutPanel flpContentList;
        private System.Windows.Forms.Panel pnlRightHeader;
        private System.Windows.Forms.Label lblRightTitle;
        public System.Windows.Forms.Panel pnlEditor;
        public Guna.UI2.WinForms.Guna2Button btnPrev;
        public Guna.UI2.WinForms.Guna2Button btnNext;
    }
}