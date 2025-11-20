using Microsoft.Reporting.WinForms;
using System;
using System.Drawing;
using System.Windows.Forms;
using WinFormsApp1.Helpers;

namespace WinFormsApp1.View.Admin
{
    public partial class ReportViewerForm : Form
    {
        private ReportViewer reportViewer;
        private Panel toolbarPanel;

        public ReportViewerForm()
        {
            InitializeComponent();
            SetupForm();
        }

        private void SetupForm()
        {
            Text = "Xem báo cáo";
            Size = new Size(1200, 800);
            StartPosition = FormStartPosition.CenterScreen;

            toolbarPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 50,
                BackColor = Color.FromArgb(248, 249, 250)
            };

            var exportPdfBtn = new Button
            {
                Text = "📄 Xuất PDF",
                Location = new Point(10, 10),
                Size = new Size(120, 30),
                BackColor = Color.FromArgb(220, 53, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            exportPdfBtn.FlatAppearance.BorderSize = 0;
            exportPdfBtn.Click += ExportPdfBtn_Click;

            var exportExcelBtn = new Button
            {
                Text = "📊 Xuất Excel",
                Location = new Point(140, 10),
                Size = new Size(120, 30),
                BackColor = Color.FromArgb(40, 167, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            exportExcelBtn.FlatAppearance.BorderSize = 0;
            exportExcelBtn.Click += ExportExcelBtn_Click;

            toolbarPanel.Controls.AddRange(new Control[] { exportPdfBtn, exportExcelBtn });

            reportViewer = new ReportViewer
            {
                Dock = DockStyle.Fill,
                ProcessingMode = ProcessingMode.Local
            };

            Controls.Add(reportViewer);
            Controls.Add(toolbarPanel);
        }

        public ReportViewer GetReportViewer()
        {
            return reportViewer;
        }

        private void ExportPdfBtn_Click(object sender, EventArgs e)
        {
            using (var saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "PDF Files|*.pdf";
                saveDialog.Title = "Lưu báo cáo PDF";
                saveDialog.FileName = $"Report_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        ReportHelper.ExportToPDF(reportViewer, saveDialog.FileName);
                        MessageBox.Show("Xuất PDF thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi xuất PDF: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void ExportExcelBtn_Click(object sender, EventArgs e)
        {
            using (var saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "Excel Files|*.xls";
                saveDialog.Title = "Lưu báo cáo Excel";
                saveDialog.FileName = $"Report_{DateTime.Now:yyyyMMdd_HHmmss}.xls";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        ReportHelper.ExportToExcel(reportViewer, saveDialog.FileName);
                        MessageBox.Show("Xuất Excel thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi xuất Excel: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
