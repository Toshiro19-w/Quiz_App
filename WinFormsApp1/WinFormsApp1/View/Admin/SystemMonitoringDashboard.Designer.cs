using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp1.View.Admin
{
    partial class SystemMonitoringDashboard
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

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // SystemMonitoringDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.Name = "SystemMonitoringDashboard";
            this.Size = new System.Drawing.Size(1103, 800);
            this.Load += new System.EventHandler(this.SystemMonitoringDashboard_Load);
            this.ResumeLayout(false);
        }
    }
}
