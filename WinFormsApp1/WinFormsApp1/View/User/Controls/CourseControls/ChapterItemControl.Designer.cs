namespace WinFormsApp1.View.User.Controls.CourseControls
{
    partial class ChapterItemControl
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.PictureBox picHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnAddLesson;
        private System.Windows.Forms.FlowLayoutPanel flpLessons;

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
            this.components = new System.ComponentModel.Container();
            this.picHeader = new System.Windows.Forms.PictureBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnAddLesson = new System.Windows.Forms.Button();
            this.flpLessons = new System.Windows.Forms.FlowLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.picHeader)).BeginInit();
            this.SuspendLayout();
            // 
            // picHeader
            // 
            this.picHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.picHeader.Height = 80;
            this.picHeader.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picHeader.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Height = 36;
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            // 
            // btnAddLesson
            // 
            this.btnAddLesson.Text = "Thêm";
            this.btnAddLesson.Width = 80;
            this.btnAddLesson.Height = 28;
            this.btnAddLesson.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnAddLesson.BackColor = System.Drawing.Color.FromArgb(88,86,233);
            this.btnAddLesson.ForeColor = System.Drawing.Color.White;
            this.btnAddLesson.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddLesson.FlatAppearance.BorderSize = 0;
            this.btnAddLesson.Location = new System.Drawing.Point(620, 90);
            // 
            // flpLessons
            // 
            this.flpLessons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpLessons.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpLessons.WrapContents = false;
            this.flpLessons.AutoScroll = true;
            this.flpLessons.Padding = new System.Windows.Forms.Padding(4);

            // 
            // ChapterItemControl
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Padding = new System.Windows.Forms.Padding(8);
            this.Width = 720;
            this.Controls.Add(this.flpLessons);
            this.Controls.Add(this.btnAddLesson);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.picHeader);
            ((System.ComponentModel.ISupportInitialize)(this.picHeader)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
