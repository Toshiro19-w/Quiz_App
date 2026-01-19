using Microsoft.Reporting.WinForms;
using System;

namespace WinFormsApp1.View.Dialogs
{
    partial class CertificateReportForm
    {
        private System.ComponentModel.IContainer components = null;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Button btnExportPDF;
        private System.Windows.Forms.Button btnClose;

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
            this.pnlTop = new System.Windows.Forms.Panel();
            this.btnExportPDF = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.pnlTop.SuspendLayout();
            this.SuspendLayout();
            
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.White;
            this.pnlTop.Controls.Add(this.btnExportPDF);
            this.pnlTop.Controls.Add(this.btnClose);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Padding = new System.Windows.Forms.Padding(10);
            this.pnlTop.Size = new System.Drawing.Size(1200, 60);
            this.pnlTop.TabIndex = 1;
            
            // 
            // btnExportPDF
            // 
            this.btnExportPDF.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnExportPDF.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportPDF.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnExportPDF.ForeColor = System.Drawing.Color.White;
            this.btnExportPDF.Location = new System.Drawing.Point(10, 10);
            this.btnExportPDF.Name = "btnExportPDF";
            this.btnExportPDF.Size = new System.Drawing.Size(150, 40);
            this.btnExportPDF.TabIndex = 0;
            this.btnExportPDF.Text = "📄 Xuất PDF";
            this.btnExportPDF.UseVisualStyleBackColor = false;
            this.btnExportPDF.Click += new System.EventHandler(this.BtnExportPDF_Click);
            
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(1040, 10);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(150, 40);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Đóng";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "";
            this.reportViewer1.Location = new System.Drawing.Point(0, 60);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(1200, 700);
            this.reportViewer1.TabIndex = 0;
            
            // 
            // CertificateReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 760);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.pnlTop);
            this.Name = "CertificateReportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chứng chỉ hoàn thành khóa học";
            this.Load += new System.EventHandler(this.CertificateReportForm_Load);
            this.pnlTop.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private void BtnExportPDF_Click(object sender, System.EventArgs e)
        {
            using (var sfd = new System.Windows.Forms.SaveFileDialog())
            {
                sfd.Filter = "PDF Files|*.pdf";
                sfd.FileName = $"Certificate_{_data.StudentName.Replace(" ", "_")}_{DateTime.Now:yyyyMMdd}.pdf";
                
                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    ExportToPDF(sfd.FileName);
                }
            }
        }

        private void BtnClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void CertificateReportForm_Load(object sender, System.EventArgs e)
        {
            // Load event handled in OnLoad override
        }
    }
}


