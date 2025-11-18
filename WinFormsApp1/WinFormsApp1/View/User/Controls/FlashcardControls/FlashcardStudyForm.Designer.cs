using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp1.View.User.Controls.FlashcardControls
{
    partial class FlashcardStudyForm
    {
        private System.ComponentModel.IContainer components = null;
        private Panel contentPanel;

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
            contentPanel = new Panel();
            SuspendLayout();
            // 
            // contentPanel
            // 
            contentPanel.BackColor = Color.FromArgb(40, 20, 100);
            contentPanel.Dock = DockStyle.Fill;
            contentPanel.Location = new Point(0, 0);
            contentPanel.Name = "contentPanel";
            contentPanel.Size = new Size(1400, 900);
            contentPanel.TabIndex = 0;
            // 
            // FlashcardStudyForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1400, 900);
            Controls.Add(contentPanel);
            Name = "FlashcardStudyForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Học Flashcard";
            WindowState = FormWindowState.Maximized;
            ResumeLayout(false);
        }
    }
}
