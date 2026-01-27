using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp1.View.User.Controls
{
    partial class FlashcardControl
    {
        private System.ComponentModel.IContainer components = null;

        private Panel headerPanel;
        private Label lblTitle;
        private Label lblSubtitle;
        private Label lblFlashcardCount;
        private FlowLayoutPanel flowFlashcards;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

		#region Component Designer generated code

		private void InitializeComponent()
		{
			headerPanel = new Panel();
			lblFlashcardCount = new Label();
			lblSubtitle = new Label();
			lblTitle = new Label();
			flowFlashcards = new FlowLayoutPanel();
			headerPanel.SuspendLayout();
			SuspendLayout();
			// 
			// headerPanel
			// 
			headerPanel.BackColor = Color.FromArgb(240, 242, 245);
			headerPanel.Controls.Add(lblFlashcardCount);
			headerPanel.Controls.Add(lblSubtitle);
			headerPanel.Controls.Add(lblTitle);
			headerPanel.Dock = DockStyle.Top;
			headerPanel.Location = new Point(0, 0);
			headerPanel.Margin = new Padding(4);
			headerPanel.Name = "headerPanel";
			headerPanel.Size = new Size(2188, 175);
			headerPanel.TabIndex = 0;
			// 
			// lblFlashcardCount
			// 
			lblFlashcardCount.AutoSize = true;
			lblFlashcardCount.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
			lblFlashcardCount.ForeColor = Color.FromArgb(88, 56, 255);
			lblFlashcardCount.Location = new Point(1662, 69);
			lblFlashcardCount.Margin = new Padding(4, 0, 4, 0);
			lblFlashcardCount.Name = "lblFlashcardCount";
			lblFlashcardCount.Size = new Size(176, 32);
			lblFlashcardCount.TabIndex = 2;
			lblFlashcardCount.Text = "0 bộ flashcard";
			// 
			// lblSubtitle
			// 
			lblSubtitle.AutoSize = true;
			lblSubtitle.Font = new Font("Segoe UI", 12F);
			lblSubtitle.ForeColor = Color.Gray;
			lblSubtitle.Location = new Point(44, 119);
			lblSubtitle.Margin = new Padding(4, 0, 4, 0);
			lblSubtitle.Name = "lblSubtitle";
			lblSubtitle.Size = new Size(630, 32);
			lblSubtitle.TabIndex = 1;
			lblSubtitle.Text = "Học từ vựng và kiến thức một cách hiệu quả với flashcard";
			// 
			// lblTitle
			// 
			lblTitle.AutoSize = true;
			lblTitle.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
			lblTitle.ForeColor = Color.FromArgb(124, 77, 255);
			lblTitle.Location = new Point(38, 38);
			lblTitle.Margin = new Padding(4, 0, 4, 0);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new Size(536, 74);
			lblTitle.TabIndex = 0;
			lblTitle.Text = "Tất cả bộ Flashcard";
			// 
			// flowFlashcards
			// 
			flowFlashcards.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
			flowFlashcards.AutoScroll = true;
			flowFlashcards.BackColor = Color.White;
			flowFlashcards.Location = new Point(26, 183);
			flowFlashcards.Margin = new Padding(4);
			flowFlashcards.Name = "flowFlashcards";
			flowFlashcards.Padding = new Padding(12);
			flowFlashcards.Size = new Size(2138, 797);
			flowFlashcards.TabIndex = 1;
			// 
			// FlashcardControl
			// 
			AutoScaleDimensions = new SizeF(10F, 25F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.White;
			Controls.Add(flowFlashcards);
			Controls.Add(headerPanel);
			Margin = new Padding(4);
			Name = "FlashcardControl";
			Size = new Size(2188, 1000);
			Load += FlashcardControl_Load;
			Resize += FlashcardControl_Resize;
			headerPanel.ResumeLayout(false);
			headerPanel.PerformLayout();
			ResumeLayout(false);
		}

		#endregion
	}
}
