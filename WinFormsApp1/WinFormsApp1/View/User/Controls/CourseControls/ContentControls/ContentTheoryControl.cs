using System.Windows.Forms;
using WinFormsApp1.ViewModels;
using System.Drawing;
using WinFormsApp1.Helpers;
using System.IO;
using System.Diagnostics;

namespace WinFormsApp1.View.User.Controls.CourseControls.ContentControls
{
    public partial class ContentTheoryControl : UserControl, IContentControl
    {
        public event Action<object, string>? ContentTypeChanged;
        public event Action<object>? DeleteRequested;
        
        private string? _oldPdfPath = null;
        
        public ContentTheoryControl()
        {
            this.Width = 700; this.Height = 200; this.Margin = new Padding(0, 0, 0, 10);
            this.BorderStyle = BorderStyle.FixedSingle;
            InitializeComponent();

            // Add delete button
            var btnDelete = new Button
            {
                Text = "Xóa",
                Size = new Size(80, 30),
                Location = new Point(this.Width - 90, 5),
                BackColor = Color.FromArgb(220, 53, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Visible = false 
            };
            btnDelete.FlatAppearance.BorderSize = 0;
            btnDelete.Click += (s, e) => DeleteRequested?.Invoke(this);
            this.Controls.Add(btnDelete);

            // default selection
            cboContentType.SelectedIndexChanged += (s, e) => OnContentTypeChanged();
            if (cboContentType.Items.Count > 0) cboContentType.SelectedIndex = 0;
            
            // PDF Browse button event
            btnBrowsePdf.Click += BtnBrowsePdf_Click;
            
            // PDF Preview button event
            btnPreviewPdf.Click += BtnPreviewPdf_Click;
            
            // Initially disable preview button
            btnPreviewPdf.Enabled = false;
        }

        private async void BtnBrowsePdf_Click(object? sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog();
            ofd.Filter = "PDF files|*.pdf";
            ofd.Title = "Chọn file PDF tài liệu";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                // Tạo Progress Form
                Form progressForm = null;
                ProgressBar progressBar = null;
                Label lblStatus = null;

                try
                {
                    // Tạo form hiển thị tiến trình
                    progressForm = new Form
                    {
                        Text = "Đang upload document...",
                        Size = new Size(400, 150),
                        FormBorderStyle = FormBorderStyle.FixedDialog,
                        StartPosition = FormStartPosition.CenterParent,
                        MaximizeBox = false,
                        MinimizeBox = false,
                        ControlBox = false
                    };

                    progressBar = new ProgressBar
                    {
                        Location = new Point(20, 30),
                        Size = new Size(340, 30),
                        Style = ProgressBarStyle.Continuous,
                        Minimum = 0,
                        Maximum = 100
                    };

                    lblStatus = new Label
                    {
                        Location = new Point(20, 70),
                        Size = new Size(340, 40),
                        Text = "Đang chuẩn bị upload...",
                        TextAlign = ContentAlignment.MiddleCenter
                    };

                    progressForm.Controls.Add(progressBar);
                    progressForm.Controls.Add(lblStatus);
                    progressForm.Show();

                    // Disable button
                    btnBrowsePdf.Enabled = false;

                    // Progress reporter
                    var progress = new Progress<int>(percent =>
                    {
                        if (progressForm != null && !progressForm.IsDisposed)
                        {
                            progressForm.Invoke((MethodInvoker)delegate
                            {
                                progressBar.Value = percent;
                                lblStatus.Text = $"Đang upload: {percent}%";
                            });
                        }
                    });

                    // Upload PDF lên Azure
                    var documentUrl = await MediaHelper.CopyPdfToLibraryAsync(ofd.FileName, progress);

                    if (documentUrl != null)
                    {
                        txtPdfPath.Text = documentUrl;
                        btnPreviewPdf.Enabled = true;

                        MessageBox.Show("Upload tài liệu thành công!", "Thành công",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi upload tài liệu: {ex.Message}", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    // Enable button
                    btnBrowsePdf.Enabled = true;

                    // Close progress form
                    if (progressForm != null && !progressForm.IsDisposed)
                    {
                        progressForm.Close();
                        progressForm.Dispose();
                    }
                }
            }
        }

        private async void BtnPreviewPdf_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPdfPath.Text))
            {
                MessageBox.Show("Chưa có file PDF để xem!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string filePath = txtPdfPath.Text;
                string localPath;

                if (MediaHelper.IsAzureUrl(filePath))
                {
                    // Download về temp để mở
                    localPath = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.pdf");

                    btnPreviewPdf.Enabled = false;
                    btnPreviewPdf.Text = "Đang tải...";

                    // Download from Azure
                    using (var httpClient = new System.Net.Http.HttpClient())
                    {
                        var response = await httpClient.GetAsync(filePath);
                        response.EnsureSuccessStatusCode();

                        using (var fs = new FileStream(localPath, FileMode.Create, FileAccess.Write, FileShare.None))
                        {
                            await response.Content.CopyToAsync(fs);
                        }
                    }
                }
                else
                {
                    // Local file
                    localPath = Path.Combine(MediaHelper.GetProjectRoot(), filePath.Replace("/", "\\"));
                }

                if (File.Exists(localPath))
                {
                    var psi = new ProcessStartInfo
                    {
                        FileName = localPath,
                        UseShellExecute = true
                    };
                    Process.Start(psi);
                }
                else
                {
                    MessageBox.Show($"Không tìm thấy file: {localPath}", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở PDF: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnPreviewPdf.Enabled = true;
                btnPreviewPdf.Text = "Xem trước";
            }
        }

        private void OnContentTypeChanged()
		{
			var vietnameseType = cboContentType.SelectedItem?.ToString();

			// ALWAYS FIRE EVENT - chuyển sang tiếng Anh
			if (vietnameseType != null)
			{
				var englishType = ContentTypeHelper.ToEnglish(vietnameseType);
				ContentTypeChanged?.Invoke(this, englishType);
			}

			// local UI change only for Theory control
			if (vietnameseType == "Video")
			{
				//lblBody.Visible = false;
				//txtBody.Visible = false;
				lblPdfPath.Visible = false;
				btnBrowsePdf.Visible = false;
				txtPdfPath.Visible = false;
				btnPreviewPdf.Visible = false;
			}
			else
			{
				//lblBody.Visible = true;
				//txtBody.Visible = true;
				lblPdfPath.Visible = true;
				btnBrowsePdf.Visible = true;
				txtPdfPath.Visible = true;
				btnPreviewPdf.Visible = true;
			}
		}

		public void LoadFromViewModel(LessonContentBuilderViewModel vm)
        {
            if (vm == null) return;
            var contentType = string.IsNullOrEmpty(vm.ContentType) ? "Theory" : vm.ContentType;
            
            // Chuyển sang tiếng Việt để hiển thị
            var vietnameseType = ContentTypeHelper.ToVietnamese(contentType);
            
            // Find and set the correct item
            for (int i = 0; i < cboContentType.Items.Count; i++)
            {
                if (cboContentType.Items[i].ToString() == vietnameseType)
                {
                    cboContentType.SelectedIndex = i;
                    break;
                }
            }
            
            txtTitle.Text = vm.Title ?? string.Empty;
            //txtBody.Text = vm.Body ?? string.Empty;
            txtPdfPath.Text = vm.VideoUrl ?? string.Empty;
            _oldPdfPath = vm.VideoUrl;
            
            // Enable preview button if there's a PDF path
            btnPreviewPdf.Enabled = !string.IsNullOrEmpty(vm.VideoUrl);
        }

        public LessonContentBuilderViewModel SaveToViewModel()
        {
            // Xóa file PDF cũ nếu người dùng đã chọn file mới
            if (!string.IsNullOrEmpty(_oldPdfPath) && _oldPdfPath != txtPdfPath.Text)
            {
                try
                {
                    string fullPath = Path.Combine(MediaHelper.GetProjectRoot(), _oldPdfPath.Replace("/", "\\"));
                    if (File.Exists(fullPath))
                    {
                        File.Delete(fullPath);
                    }
                }
                catch
                {
                    // Ignore deletion errors
                }
            }

            var vietnameseType = cboContentType.SelectedItem?.ToString() ?? "Lý thuyết";
            return new LessonContentBuilderViewModel
            {
                ContentType = ContentTypeHelper.ToEnglish(vietnameseType), // Lưu bằng tiếng Anh
                Title = txtTitle.Text.Trim(),
                //Body = txtBody.Text,
                VideoUrl = txtPdfPath.Text.Trim() // Tạm thời lưu PDF path vào VideoUrl
            };
        }
    }
}