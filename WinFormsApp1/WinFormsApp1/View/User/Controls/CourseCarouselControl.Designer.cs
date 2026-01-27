namespace WinFormsApp1.View.User.Controls
{
    partial class CourseCarouselControl
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.FlowLayoutPanel flowPanel;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Button btnNext;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

		private void InitializeComponent()
		{
			flowPanel = new FlowLayoutPanel();
			btnPrevious = new Button();
			btnNext = new Button();
			SuspendLayout();
			// 
			// flowPanel
			// 
			flowPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			flowPanel.BackColor = Color.Transparent;
			flowPanel.Location = new Point(60, 0);
			flowPanel.Name = "flowPanel";
			flowPanel.Size = new Size(1575, 420);
			flowPanel.TabIndex = 0;
			flowPanel.WrapContents = false;
			// 
			// btnPrevious
			// 
			btnPrevious.BackColor = Color.FromArgb(250, 250, 250);
			btnPrevious.Cursor = Cursors.Hand;
			btnPrevious.FlatAppearance.BorderSize = 0;
			btnPrevious.FlatStyle = FlatStyle.Flat;
			btnPrevious.Font = new Font("Arial", 24F, FontStyle.Bold);
			btnPrevious.ForeColor = Color.FromArgb(100, 100, 100);
			btnPrevious.Image = Properties.Resources.left_arrow;
			btnPrevious.Location = new Point(10, 180);
			btnPrevious.Name = "btnPrevious";
			btnPrevious.Size = new Size(40, 60);
			btnPrevious.TabIndex = 1;
			btnPrevious.UseVisualStyleBackColor = false;
			// 
			// btnNext
			// 
			btnNext.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			btnNext.BackColor = Color.FromArgb(250, 250, 250);
			btnNext.Cursor = Cursors.Hand;
			btnNext.FlatAppearance.BorderSize = 0;
			btnNext.FlatStyle = FlatStyle.Flat;
			btnNext.Font = new Font("Arial", 24F, FontStyle.Bold);
			btnNext.ForeColor = Color.FromArgb(100, 100, 100);
			btnNext.Image = Properties.Resources.right_arrow;
			btnNext.Location = new Point(1645, 180);
			btnNext.Name = "btnNext";
			btnNext.Size = new Size(40, 60);
			btnNext.TabIndex = 2;
			btnNext.UseVisualStyleBackColor = false;
			// 
			// CourseCarouselControl
			// 
			AutoScaleMode = AutoScaleMode.None;
			BackColor = Color.Transparent;
			Controls.Add(btnNext);
			Controls.Add(btnPrevious);
			Controls.Add(flowPanel);
			Name = "CourseCarouselControl";
			Size = new Size(1695, 420);
			ResumeLayout(false);
		}
	}
}
