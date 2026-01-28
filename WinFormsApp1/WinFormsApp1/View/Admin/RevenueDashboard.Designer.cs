using System.Drawing;
using System.Windows.Forms;
using WinFormsApp1.Localization;

namespace WinFormsApp1.View.Admin
{
    partial class RevenueDashboard
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Panel filterPanel;
        private System.Windows.Forms.Label fromLabel;
        private System.Windows.Forms.DateTimePicker startDatePicker;
        private System.Windows.Forms.Label toLabel;
        private System.Windows.Forms.DateTimePicker endDatePicker;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.ComboBox statusCombo;
        private System.Windows.Forms.Label providerLabel;
        private System.Windows.Forms.ComboBox providerCombo;
        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.FlowLayoutPanel statsFlowPanel;
        private System.Windows.Forms.Panel monthlyChartPanel;
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
            this.titleLabel = new System.Windows.Forms.Label();
            this.filterPanel = new System.Windows.Forms.Panel();
            this.fromLabel = new System.Windows.Forms.Label();
            this.startDatePicker = new System.Windows.Forms.DateTimePicker();
            this.toLabel = new System.Windows.Forms.Label();
            this.endDatePicker = new System.Windows.Forms.DateTimePicker();
            this.statusLabel = new System.Windows.Forms.Label();
            this.statusCombo = new System.Windows.Forms.ComboBox();
            this.providerLabel = new System.Windows.Forms.Label();
            this.providerCombo = new System.Windows.Forms.ComboBox();
            this.applyButton = new System.Windows.Forms.Button();
            this.resetButton = new System.Windows.Forms.Button();
            this.statsFlowPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.monthlyChartPanel = new System.Windows.Forms.Panel();
            this.chartsFlowPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.filterPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.titleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(55)))), ((int)(((byte)(72)))));
            this.titleLabel.Location = new System.Drawing.Point(20, 20);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(350, 45);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = LanguageHelper.GetString("RevenueAnalytics");
            // 
            // filterPanel
            // 
            this.filterPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filterPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(250)))), ((int)(((byte)(251)))));
            this.filterPanel.Controls.Add(this.resetButton);
            this.filterPanel.Controls.Add(this.applyButton);
            this.filterPanel.Controls.Add(this.providerCombo);
            this.filterPanel.Controls.Add(this.providerLabel);
            this.filterPanel.Controls.Add(this.statusCombo);
            this.filterPanel.Controls.Add(this.statusLabel);
            this.filterPanel.Controls.Add(this.endDatePicker);
            this.filterPanel.Controls.Add(this.toLabel);
            this.filterPanel.Controls.Add(this.startDatePicker);
            this.filterPanel.Controls.Add(this.fromLabel);
            this.filterPanel.Location = new System.Drawing.Point(20, 80);
            this.filterPanel.Name = "filterPanel";
            this.filterPanel.Size = new System.Drawing.Size(1858, 60);
            this.filterPanel.TabIndex = 1;
            // 
            // fromLabel
            // 
            this.fromLabel.AutoSize = true;
            this.fromLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.fromLabel.Location = new System.Drawing.Point(10, 20);
            this.fromLabel.Name = "fromLabel";
            this.fromLabel.Size = new System.Drawing.Size(53, 15);
            this.fromLabel.TabIndex = 0;
            this.fromLabel.Text = LanguageHelper.GetString("FromDateLabel");
            // 
            // startDatePicker
            // 
            this.startDatePicker.CustomFormat = "dd/MM/yyyy";
            this.startDatePicker.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.startDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.startDatePicker.Location = new System.Drawing.Point(70, 17);
            this.startDatePicker.Name = "startDatePicker";
            this.startDatePicker.Size = new System.Drawing.Size(120, 25);
            this.startDatePicker.TabIndex = 1;
            // 
            // toLabel
            // 
            this.toLabel.AutoSize = true;
            this.toLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toLabel.Location = new System.Drawing.Point(200, 20);
            this.toLabel.Name = "toLabel";
            this.toLabel.Size = new System.Drawing.Size(60, 15);
            this.toLabel.TabIndex = 2;
            this.toLabel.Text = LanguageHelper.GetString("ToDateLabel");
            // 
            // endDatePicker
            // 
            this.endDatePicker.CustomFormat = "dd/MM/yyyy";
            this.endDatePicker.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.endDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.endDatePicker.Location = new System.Drawing.Point(270, 17);
            this.endDatePicker.Name = "endDatePicker";
            this.endDatePicker.Size = new System.Drawing.Size(120, 25);
            this.endDatePicker.TabIndex = 3;
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.statusLabel.Location = new System.Drawing.Point(400, 20);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(62, 15);
            this.statusLabel.TabIndex = 4;
            this.statusLabel.Text = LanguageHelper.GetString("FilterStatus");
            // 
            // statusCombo
            // 
            this.statusCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.statusCombo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.statusCombo.FormattingEnabled = true;
            this.statusCombo.Location = new System.Drawing.Point(475, 17);
            this.statusCombo.Name = "statusCombo";
            this.statusCombo.Size = new System.Drawing.Size(150, 25);
            this.statusCombo.TabIndex = 5;
            // 
            // providerLabel
            // 
            this.providerLabel.AutoSize = true;
            this.providerLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.providerLabel.Location = new System.Drawing.Point(635, 20);
            this.providerLabel.Name = "providerLabel";
            this.providerLabel.Size = new System.Drawing.Size(34, 15);
            this.providerLabel.TabIndex = 6;
            this.providerLabel.Text = LanguageHelper.GetString("ProviderLabel");
            // 
            // providerCombo
            // 
            this.providerCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.providerCombo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.providerCombo.FormattingEnabled = true;
            this.providerCombo.Location = new System.Drawing.Point(675, 17);
            this.providerCombo.Name = "providerCombo";
            this.providerCombo.Size = new System.Drawing.Size(120, 25);
            this.providerCombo.TabIndex = 7;
            // 
            // applyButton
            // 
            this.applyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.applyButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.applyButton.FlatAppearance.BorderSize = 0;
            this.applyButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.applyButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.applyButton.ForeColor = System.Drawing.Color.White;
            this.applyButton.Location = new System.Drawing.Point(1658, 15);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(90, 35);
            this.applyButton.TabIndex = 8;
            this.applyButton.Text = LanguageHelper.GetString("Apply");
            this.applyButton.UseVisualStyleBackColor = false;
            // 
            // resetButton
            // 
            this.resetButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.resetButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(163)))), ((int)(((byte)(175)))));
            this.resetButton.FlatAppearance.BorderSize = 0;
            this.resetButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.resetButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.resetButton.ForeColor = System.Drawing.Color.White;
            this.resetButton.Location = new System.Drawing.Point(1758, 15);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(90, 35);
            this.resetButton.TabIndex = 9;
            this.resetButton.Text = LanguageHelper.GetString("Reset");
            this.resetButton.UseVisualStyleBackColor = false;
            // 
            // statsFlowPanel
            // 
            this.statsFlowPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.statsFlowPanel.Location = new System.Drawing.Point(20, 150);
            this.statsFlowPanel.Name = "statsFlowPanel";
            this.statsFlowPanel.Size = new System.Drawing.Size(1858, 145);
            this.statsFlowPanel.TabIndex = 2;
            this.statsFlowPanel.WrapContents = false;
            // 
            // monthlyChartPanel
            // 
            this.monthlyChartPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.monthlyChartPanel.Location = new System.Drawing.Point(20, 315);
            this.monthlyChartPanel.Name = "monthlyChartPanel";
            this.monthlyChartPanel.Size = new System.Drawing.Size(1858, 400);
            this.monthlyChartPanel.TabIndex = 3;
            // 
            // chartsFlowPanel
            // 
            this.chartsFlowPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chartsFlowPanel.AutoSize = true;
            this.chartsFlowPanel.Location = new System.Drawing.Point(20, 735);
            this.chartsFlowPanel.Name = "chartsFlowPanel";
            this.chartsFlowPanel.Size = new System.Drawing.Size(1858, 100);
            this.chartsFlowPanel.TabIndex = 4;
            // 
            // RevenueDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.Controls.Add(this.chartsFlowPanel);
            this.Controls.Add(this.monthlyChartPanel);
            this.Controls.Add(this.statsFlowPanel);
            this.Controls.Add(this.filterPanel);
            this.Controls.Add(this.titleLabel);
            this.Name = "RevenueDashboard";
            this.Size = new System.Drawing.Size(1898, 1024);
            this.Load += new System.EventHandler(this.RevenueDashboard_Load);
            this.filterPanel.ResumeLayout(false);
            this.filterPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
