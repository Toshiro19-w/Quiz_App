namespace WinFormsApp1.View.User.Controls.CourseControls.Steps
{
    partial class Step4_PublishControl
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
			pnlCard = new Guna.UI2.WinForms.Guna2ShadowPanel();
			lblHeader = new Label();
			pnlPreview = new Panel();
			btnSaveDraft = new Guna.UI2.WinForms.Guna2Button();
			btnPublish = new Guna.UI2.WinForms.Guna2Button();
			btnCancel = new Guna.UI2.WinForms.Guna2Button();
			pnlCard.SuspendLayout();
			SuspendLayout();
			// 
			// pnlCard
			// 
			pnlCard.BackColor = Color.Transparent;
			pnlCard.Controls.Add(lblHeader);
			pnlCard.Controls.Add(pnlPreview);
			pnlCard.Controls.Add(btnSaveDraft);
			pnlCard.Controls.Add(btnPublish);
			pnlCard.Controls.Add(btnCancel);
			pnlCard.Dock = DockStyle.Fill;
			pnlCard.FillColor = Color.White;
			pnlCard.Location = new Point(0, 0);
			pnlCard.Name = "pnlCard";
			pnlCard.Padding = new Padding(18);
			pnlCard.ShadowColor = Color.Black;
			pnlCard.Size = new Size(778, 508);
			pnlCard.TabIndex = 0;
			// 
			// lblHeader
			// 
			lblHeader.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
			lblHeader.Location = new Point(20, 10);
			lblHeader.Name = "lblHeader";
			lblHeader.Size = new Size(720, 60);
			lblHeader.TabIndex = 0;
			lblHeader.Text = "Xem trước & Xuất bản";
			// 
			// pnlPreview
			// 
			pnlPreview.Location = new Point(20, 88);
			pnlPreview.Name = "pnlPreview";
			pnlPreview.Size = new Size(720, 320);
			pnlPreview.TabIndex = 1;
			// 
			// btnSaveDraft
			// 
			btnSaveDraft.CustomizableEdges = customizableEdges1;
			btnSaveDraft.Font = new Font("Segoe UI", 9F);
			btnSaveDraft.ForeColor = Color.White;
			btnSaveDraft.Location = new Point(20, 428);
			btnSaveDraft.Name = "btnSaveDraft";
			btnSaveDraft.ShadowDecoration.CustomizableEdges = customizableEdges2;
			btnSaveDraft.Size = new Size(140, 36);
			btnSaveDraft.TabIndex = 2;
			btnSaveDraft.Text = "Lưu nháp";
			// 
			// btnPublish
			// 
			btnPublish.CustomizableEdges = customizableEdges3;
			btnPublish.Font = new Font("Segoe UI", 9F);
			btnPublish.ForeColor = Color.White;
			btnPublish.Location = new Point(180, 428);
			btnPublish.Name = "btnPublish";
			btnPublish.ShadowDecoration.CustomizableEdges = customizableEdges4;
			btnPublish.Size = new Size(140, 36);
			btnPublish.TabIndex = 3;
			btnPublish.Text = "Xuất bản";
			// 
			// btnCancel
			// 
			btnCancel.CustomizableEdges = customizableEdges5;
			btnCancel.Font = new Font("Segoe UI", 9F);
			btnCancel.ForeColor = Color.White;
			btnCancel.Location = new Point(340, 428);
			btnCancel.Name = "btnCancel";
			btnCancel.ShadowDecoration.CustomizableEdges = customizableEdges5;
			btnCancel.Size = new Size(140, 36);
			btnCancel.TabIndex = 4;
			btnCancel.Text = "Hủy bỏ";
			// 
			// Step4_PublishControl
			// 
			Controls.Add(pnlCard);
			Name = "Step4_PublishControl";
			Size = new Size(778, 508);
			pnlCard.ResumeLayout(false);
			ResumeLayout(false);
		}

		private Guna.UI2.WinForms.Guna2ShadowPanel pnlCard;
        private System.Windows.Forms.Label lblHeader;
        public System.Windows.Forms.Panel pnlPreview;
        public Guna.UI2.WinForms.Guna2Button btnSaveDraft;
        public Guna.UI2.WinForms.Guna2Button btnPublish;
        public Guna.UI2.WinForms.Guna2Button btnCancel;
    }
}