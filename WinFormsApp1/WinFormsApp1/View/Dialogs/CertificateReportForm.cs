using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using WinFormsApp1.ViewModels;

namespace WinFormsApp1.View.Dialogs
{
    public partial class CertificateReportForm : Form
    {
        private CertificateReportViewModel _data;

        public CertificateReportForm(CertificateReportViewModel data)
        {
            _data = data;
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LoadReport();
        }

        private void LoadReport()
        {
            try
            {
                // Clear existing data sources
                reportViewer1.LocalReport.DataSources.Clear();

                // Set report path
                string reportPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reports", "CertificateReport.rdlc");
                
                if (!File.Exists(reportPath))
                {
                    MessageBox.Show($"Không tìm thấy file report:\n{reportPath}\n\nVui lòng đảm bảo file CertificateReport.rdlc nằm trong thư mục Reports.", 
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                reportViewer1.LocalReport.ReportPath = reportPath;

                // Enable external images
                reportViewer1.LocalReport.EnableExternalImages = true;

                // Create data source
                var dataSource = new List<CertificateReportViewModel> { _data };
                var reportDataSource = new ReportDataSource("CertificateDataSet", dataSource);
                
                reportViewer1.LocalReport.DataSources.Add(reportDataSource);

                // Set display mode
                reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.PageWidth;

                // Refresh report
                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi load report:\n\n{ex.Message}\n\nStack trace:\n{ex.StackTrace}", 
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ExportToPDF(string filePath)
        {
            try
            {
                Warning[] warnings;
                string[] streamids;
                string mimeType;
                string encoding;
                string extension;

                byte[] bytes = reportViewer1.LocalReport.Render(
                    "PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);

                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    fs.Write(bytes, 0, bytes.Length);
                }

                MessageBox.Show($"Đã xuất chứng chỉ thành công!\n\nFile lưu tại:\n{filePath}", 
                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                // Mở file PDF
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = filePath,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xuất PDF:\n\n{ex.Message}", 
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}



