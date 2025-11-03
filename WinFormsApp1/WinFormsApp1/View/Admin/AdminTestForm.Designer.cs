namespace WinFormsApp1.View.Admin
{
    partial class AdminTestForm
    {
        private System.ComponentModel.IContainer components = null;
        private Button btnOpenAdmin;

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
            this.btnOpenAdmin = new Button();
            this.SuspendLayout();
            // 
            // btnOpenAdmin
            // 
            this.btnOpenAdmin.Location = new System.Drawing.Point(100, 100);
            this.btnOpenAdmin.Name = "btnOpenAdmin";
            this.btnOpenAdmin.Size = new System.Drawing.Size(200, 50);
            this.btnOpenAdmin.TabIndex = 0;
            this.btnOpenAdmin.Text = "Mở Admin Dashboard";
            this.btnOpenAdmin.UseVisualStyleBackColor = true;
            this.btnOpenAdmin.Click += new System.EventHandler(this.btnOpenAdmin_Click);
            // 
            // AdminTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 300);
            this.Controls.Add(this.btnOpenAdmin);
            this.Name = "AdminTestForm";
            this.Text = "Test Admin";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
        }
    }
}