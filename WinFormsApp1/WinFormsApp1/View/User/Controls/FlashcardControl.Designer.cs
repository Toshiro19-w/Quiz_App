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
            this.headerPanel = new System.Windows.Forms.Panel();
            this.lblFlashcardCount = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.flowFlashcards = new System.Windows.Forms.FlowLayoutPanel();
            this.headerPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerPanel
            // 
            this.headerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(242)))), ((int)(((byte)(245)))));
            this.headerPanel.Controls.Add(this.lblFlashcardCount);
            this.headerPanel.Controls.Add(this.lblSubtitle);
            this.headerPanel.Controls.Add(this.lblTitle);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(1750, 140);
            this.headerPanel.TabIndex = 0;
            // 
            // lblFlashcardCount
            // 
            this.lblFlashcardCount.AutoSize = true;
            this.lblFlashcardCount.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblFlashcardCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(56)))), ((int)(((byte)(255)))));
            this.lblFlashcardCount.Location = new System.Drawing.Point(1330, 55);
            this.lblFlashcardCount.Name = "lblFlashcardCount";
            this.lblFlashcardCount.Size = new System.Drawing.Size(170, 28);
            this.lblFlashcardCount.TabIndex = 2;
            this.lblFlashcardCount.Text = "0 bộ flashcard";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.Gray;
            this.lblSubtitle.Location = new System.Drawing.Point(35, 95);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(521, 28);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Học từ vựng và kiến thức một cách hiệu quả với flashcard";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(77)))), ((int)(((byte)(255)))));
            this.lblTitle.Location = new System.Drawing.Point(30, 30);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(437, 62);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Tất cả bộ Flashcard";
            // 
            // flowFlashcards
            // 
            this.flowFlashcards.AutoScroll = true;
            this.flowFlashcards.BackColor = System.Drawing.Color.White;
            this.flowFlashcards.Location = new System.Drawing.Point(20, 160);
            this.flowFlashcards.Name = "flowFlashcards";
            this.flowFlashcards.Padding = new System.Windows.Forms.Padding(10);
            this.flowFlashcards.Size = new System.Drawing.Size(1710, 840);
            this.flowFlashcards.TabIndex = 1;
            // 
            // FlashcardControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.flowFlashcards);
            this.Controls.Add(this.headerPanel);
            this.Name = "FlashcardControl";
            this.Size = new System.Drawing.Size(1750, 1020);
            this.Load += new System.EventHandler(this.FlashcardControl_Load);
            this.Resize += new System.EventHandler(this.FlashcardControl_Resize);
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion
    }
}
