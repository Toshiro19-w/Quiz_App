using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp1.View.Admin
{
    partial class UserAnalyticsDashboard
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Panel filterPanel;
        private System.Windows.Forms.Label fromLabel;
        private System.Windows.Forms.DateTimePicker startDatePicker;
        private System.Windows.Forms.Label toLabel;
        private System.Windows.Forms.DateTimePicker endDatePicker;
        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.FlowLayoutPanel statsFlowPanel;
        private System.Windows.Forms.FlowLayoutPanel chartsFlowPanel;

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
            titleLabel = new Label();
            filterPanel = new Panel();
            resetButton = new Button();
            applyButton = new Button();
            endDatePicker = new DateTimePicker();
            toLabel = new Label();
            startDatePicker = new DateTimePicker();
            fromLabel = new Label();
            statsFlowPanel = new FlowLayoutPanel();
            chartsFlowPanel = new FlowLayoutPanel();
            filterPanel.SuspendLayout();
            SuspendLayout();
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            titleLabel.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            titleLabel.ForeColor = Color.FromArgb(45, 55, 72);
            titleLabel.Location = new Point(18, 15);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(398, 45);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "👥 Phân tích người dùng";
            // 
            // filterPanel
            // 
            filterPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            filterPanel.BackColor = Color.FromArgb(249, 250, 251);
            filterPanel.Controls.Add(resetButton);
            filterPanel.Controls.Add(applyButton);
            filterPanel.Controls.Add(endDatePicker);
            filterPanel.Controls.Add(toLabel);
            filterPanel.Controls.Add(startDatePicker);
            filterPanel.Controls.Add(fromLabel);
            filterPanel.Location = new Point(18, 60);
            filterPanel.Margin = new Padding(3, 2, 3, 2);
            filterPanel.Name = "filterPanel";
            filterPanel.Size = new Size(1626, 45);
            filterPanel.TabIndex = 1;
            // 
            // resetButton
            // 
            resetButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            resetButton.BackColor = Color.FromArgb(156, 163, 175);
            resetButton.FlatAppearance.BorderSize = 0;
            resetButton.FlatStyle = FlatStyle.Flat;
            resetButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            resetButton.ForeColor = Color.White;
            resetButton.Location = new Point(1538, 11);
            resetButton.Margin = new Padding(3, 2, 3, 2);
            resetButton.Name = "resetButton";
            resetButton.Size = new Size(79, 26);
            resetButton.TabIndex = 9;
            resetButton.Text = "Đặt lại";
            resetButton.UseVisualStyleBackColor = false;
            // 
            // applyButton
            // 
            applyButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            applyButton.BackColor = Color.FromArgb(59, 130, 246);
            applyButton.FlatAppearance.BorderSize = 0;
            applyButton.FlatStyle = FlatStyle.Flat;
            applyButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            applyButton.ForeColor = Color.White;
            applyButton.Location = new Point(1451, 11);
            applyButton.Margin = new Padding(3, 2, 3, 2);
            applyButton.Name = "applyButton";
            applyButton.Size = new Size(79, 26);
            applyButton.TabIndex = 8;
            applyButton.Text = "Áp dụng";
            applyButton.UseVisualStyleBackColor = false;
            // 
            // endDatePicker
            // 
            endDatePicker.CustomFormat = "dd/MM/yyyy";
            endDatePicker.Font = new Font("Segoe UI", 10F);
            endDatePicker.Format = DateTimePickerFormat.Custom;
            endDatePicker.Location = new Point(252, 9);
            endDatePicker.Margin = new Padding(3, 2, 3, 2);
            endDatePicker.Name = "endDatePicker";
            endDatePicker.Size = new Size(106, 25);
            endDatePicker.TabIndex = 3;
            // 
            // toLabel
            // 
            toLabel.AutoSize = true;
            toLabel.Font = new Font("Segoe UI", 9F);
            toLabel.Location = new Point(186, 13);
            toLabel.Name = "toLabel";
            toLabel.Size = new Size(60, 15);
            toLabel.TabIndex = 2;
            toLabel.Text = "Đến ngày:";
            // 
            // startDatePicker
            // 
            startDatePicker.CustomFormat = "dd/MM/yyyy";
            startDatePicker.Font = new Font("Segoe UI", 10F);
            startDatePicker.Format = DateTimePickerFormat.Custom;
            startDatePicker.Location = new Point(68, 9);
            startDatePicker.Margin = new Padding(3, 2, 3, 2);
            startDatePicker.Name = "startDatePicker";
            startDatePicker.Size = new Size(106, 25);
            startDatePicker.TabIndex = 1;
            // 
            // fromLabel
            // 
            fromLabel.AutoSize = true;
            fromLabel.Font = new Font("Segoe UI", 9F);
            fromLabel.Location = new Point(9, 15);
            fromLabel.Name = "fromLabel";
            fromLabel.Size = new Size(53, 15);
            fromLabel.TabIndex = 0;
            fromLabel.Text = "Từ ngày:";
            // 
            // statsFlowPanel
            // 
            statsFlowPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            statsFlowPanel.Location = new Point(18, 112);
            statsFlowPanel.Margin = new Padding(3, 2, 3, 2);
            statsFlowPanel.Name = "statsFlowPanel";
            statsFlowPanel.Size = new Size(1626, 109);
            statsFlowPanel.TabIndex = 2;
            statsFlowPanel.WrapContents = false;
            // 
            // chartsFlowPanel
            // 
            chartsFlowPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            chartsFlowPanel.AutoSize = true;
            chartsFlowPanel.Location = new Point(18, 236);
            chartsFlowPanel.Margin = new Padding(3, 2, 3, 2);
            chartsFlowPanel.Name = "chartsFlowPanel";
            chartsFlowPanel.Size = new Size(1626, 300);
            chartsFlowPanel.TabIndex = 3;
            // 
            // UserAnalyticsDashboard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = Color.FromArgb(248, 249, 250);
            Controls.Add(chartsFlowPanel);
            Controls.Add(statsFlowPanel);
            Controls.Add(filterPanel);
            Controls.Add(titleLabel);
            Margin = new Padding(3, 2, 3, 2);
            Name = "UserAnalyticsDashboard";
            Size = new Size(1661, 768);
            Load += UserAnalyticsDashboard_Load;
            filterPanel.ResumeLayout(false);
            filterPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }
    }
}
