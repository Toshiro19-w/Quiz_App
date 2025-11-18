using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinFormsApp1.Helpers
{
    public static class ExportImportHelper
    {
        public static void ExportToCSV(DataGridView dataGridView, string defaultFileName = "export")
        {
            try
            {
                using (var saveDialog = new SaveFileDialog())
                {
                    saveDialog.Filter = "CSV files (*.csv)|*.csv";
                    saveDialog.FileName = $"{defaultFileName}_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
                    
                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        var csv = new StringBuilder();
                        
                        // Headers
                        var headers = dataGridView.Columns.Cast<DataGridViewColumn>()
                            .Where(c => c.Visible)
                            .Select(c => EscapeCsvField(c.HeaderText));
                        csv.AppendLine(string.Join(",", headers));
                        
                        // Data rows
                        foreach (DataGridViewRow row in dataGridView.Rows)
                        {
                            if (row.IsNewRow) continue;
                            
                            var values = dataGridView.Columns.Cast<DataGridViewColumn>()
                                .Where(c => c.Visible)
                                .Select(c => EscapeCsvField(row.Cells[c.Index].Value?.ToString() ?? ""));
                            csv.AppendLine(string.Join(",", values));
                        }
                        
                        File.WriteAllText(saveDialog.FileName, csv.ToString(), Encoding.UTF8);
                        MessageBox.Show($"Xuất dữ liệu thành công!\nFile: {saveDialog.FileName}", "Thành công", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi xuất dữ liệu: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void ExportToExcel(DataGridView dataGridView, string defaultFileName = "export")
        {
            try
            {
                // Simple HTML table export that Excel can open
                using (var saveDialog = new SaveFileDialog())
                {
                    saveDialog.Filter = "Excel files (*.xls)|*.xls";
                    saveDialog.FileName = $"{defaultFileName}_{DateTime.Now:yyyyMMdd_HHmmss}.xls";
                    
                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        var html = new StringBuilder();
                        html.AppendLine("<html><body><table border='1'>");
                        
                        // Headers
                        html.AppendLine("<tr>");
                        foreach (DataGridViewColumn col in dataGridView.Columns)
                        {
                            if (col.Visible)
                                html.AppendLine($"<th>{col.HeaderText}</th>");
                        }
                        html.AppendLine("</tr>");
                        
                        // Data rows
                        foreach (DataGridViewRow row in dataGridView.Rows)
                        {
                            if (row.IsNewRow) continue;
                            
                            html.AppendLine("<tr>");
                            foreach (DataGridViewColumn col in dataGridView.Columns)
                            {
                                if (col.Visible)
                                    html.AppendLine($"<td>{row.Cells[col.Index].Value?.ToString() ?? ""}</td>");
                            }
                            html.AppendLine("</tr>");
                        }
                        
                        html.AppendLine("</table></body></html>");
                        File.WriteAllText(saveDialog.FileName, html.ToString(), Encoding.UTF8);
                        
                        MessageBox.Show($"Xuất dữ liệu thành công!\nFile: {saveDialog.FileName}", "Thành công", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi xuất dữ liệu: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static Button CreateExportButton(DataGridView dataGridView, string entityName)
        {
            var exportBtn = new Button
            {
                Text = "📤 Xuất",
                Size = new Size(80, 28),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(34, 197, 94),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9)
            };
            exportBtn.FlatAppearance.BorderSize = 0;
            
            exportBtn.Click += (s, e) =>
            {
                var contextMenu = new ContextMenuStrip();
                contextMenu.Items.Add("CSV", null, (sender, args) => ExportToCSV(dataGridView, entityName));
                contextMenu.Items.Add("Excel", null, (sender, args) => ExportToExcel(dataGridView, entityName));
                contextMenu.Show(exportBtn, new Point(0, exportBtn.Height));
            };
            
            return exportBtn;
        }

        private static string EscapeCsvField(string field)
        {
            if (string.IsNullOrEmpty(field)) return "";
            
            if (field.Contains(",") || field.Contains("\"") || field.Contains("\n"))
            {
                return "\"" + field.Replace("\"", "\"\"") + "\"";
            }
            return field;
        }
    }
}