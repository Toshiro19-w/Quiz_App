using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp1.View.Admin
{
    partial class OverviewDashboard
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Panel topPanel;
        private System.Windows.Forms.ComboBox filterCombo;
        private System.Windows.Forms.DateTimePicker startDatePicker;
        private System.Windows.Forms.DateTimePicker endDatePicker;
        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.FlowLayoutPanel statsFlowPanel;
        private System.Windows.Forms.Panel chartPanel;

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
            this.titleLabel = new System.Windows.Forms.Label();
            this.topPanel = new System.Windows.Forms.Panel();
            this.filterCombo = new System.Windows.Forms.ComboBox();
            this.startDatePicker = new System.Windows.Forms.DateTimePicker();
            this.endDatePicker = new System.Windows.Forms.DateTimePicker();
            this.applyButton = new System.Windows.Forms.Button();
            this.statsFlowPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.chartPanel = new System.Windows.Forms.Panel();
            this.topPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.titleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(55)))), ((int)(((byte)(72)))));
            this.titleLabel.Location = new System.Drawing.Point(20, 15);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(350, 45);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "📊 Tổng quan hệ thống";
            // 
            // topPanel
            // 
            this.topPanel.BackColor = System.Drawing.Color.Transparent;
            this.topPanel.Controls.Add(this.applyButton);
            this.topPanel.Controls.Add(this.endDatePicker);
            this.topPanel.Controls.Add(this.startDatePicker);
            this.topPanel.Controls.Add(this.filterCombo);
            this.topPanel.Controls.Add(this.titleLabel);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Height = 80;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Padding = new System.Windows.Forms.Padding(20);
            this.topPanel.Size = new System.Drawing.Size(1898, 80);
            this.topPanel.TabIndex = 1;
            // 
            // filterCombo
            // 
            this.filterCombo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.filterCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.filterCombo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.filterCombo.FormattingEnabled = true;
            this.filterCombo.Items.AddRange(new object[] {
            "Hôm nay",
            "Tuần này",
            "Tháng này",
            "Tùy chọn"});
            this.filterCombo.Location = new System.Drawing.Point(1728, 25);
            this.filterCombo.Name = "filterCombo";
            this.filterCombo.Size = new System.Drawing.Size(150, 25);
            this.filterCombo.TabIndex = 1;
            // 
            // startDatePicker
            // 
            this.startDatePicker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.startDatePicker.CustomFormat = "dd/MM/yyyy";
            this.startDatePicker.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.startDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.startDatePicker.Location = new System.Drawing.Point(1458, 25);
            this.startDatePicker.Name = "startDatePicker";
            this.startDatePicker.Size = new System.Drawing.Size(120, 25);
            this.startDatePicker.TabIndex = 2;
            this.startDatePicker.Visible = false;
            // 
            // endDatePicker
            // 
            this.endDatePicker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.endDatePicker.CustomFormat = "dd/MM/yyyy";
            this.endDatePicker.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.endDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.endDatePicker.Location = new System.Drawing.Point(1588, 25);
            this.endDatePicker.Name = "endDatePicker";
            this.endDatePicker.Size = new System.Drawing.Size(120, 25);
            this.endDatePicker.TabIndex = 3;
            this.endDatePicker.Visible = false;
            // 
            // applyButton
            // 
            this.applyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.applyButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(178)))), ((int)(((byte)(172)))));
            this.applyButton.FlatAppearance.BorderSize = 0;
            this.applyButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.applyButton.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.applyButton.ForeColor = System.Drawing.Color.White;
            this.applyButton.Location = new System.Drawing.Point(1718, 23);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(80, 30);
            this.applyButton.TabIndex = 4;
            this.applyButton.Text = "Áp dụng";
            this.applyButton.UseVisualStyleBackColor = false;
            this.applyButton.Visible = false;
            // 
            // statsFlowPanel
            // 
            this.statsFlowPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.statsFlowPanel.Location = new System.Drawing.Point(20, 100);
            this.statsFlowPanel.Name = "statsFlowPanel";
            this.statsFlowPanel.Size = new System.Drawing.Size(1858, 145);
            this.statsFlowPanel.TabIndex = 2;
            this.statsFlowPanel.WrapContents = false;
            // 
            // chartPanel
            // 
            this.chartPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chartPanel.Location = new System.Drawing.Point(20, 265);
            this.chartPanel.Name = "chartPanel";
            this.chartPanel.Size = new System.Drawing.Size(1858, 400);
            this.chartPanel.TabIndex = 3;
            // 
            // OverviewDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.chartPanel);
            this.Controls.Add(this.statsFlowPanel);
            this.Controls.Add(this.topPanel);
            this.Name = "OverviewDashboard";
            this.Size = new System.Drawing.Size(1898, 1024);
            this.Load += new System.EventHandler(this.OverviewDashboard_Load);
            this.topPanel.ResumeLayout(false);
            this.topPanel.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}
