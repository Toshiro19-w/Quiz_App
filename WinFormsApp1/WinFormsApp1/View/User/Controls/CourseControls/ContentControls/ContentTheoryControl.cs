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
                FlatStyle = FlatStyle.Flat
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

        private void BtnBrowsePdf_Click(object? sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog();
            ofd.Filter = "PDF files|*.pdf";
            ofd.Title = "Chọn file PDF tài liệu";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                var relativePath = MediaHelper.CopyPdfToLibrary(ofd.FileName);
                
                if (relativePath != null)
                {
                    txtPdfPath.Text = relativePath;
                    btnPreviewPdf.Enabled = true; // Enable preview button
                    MessageBox.Show("Đã tải lên file PDF thành công!", "Thành công", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void BtnPreviewPdf_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtPdfPath.Text))
            {
                MessageBox.Show("Chưa có file PDF để xem!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Tạo đường dẫn đầy đủ
                string fullPath = Path.Combine(MediaHelper.GetProjectRoot(), txtPdfPath.Text.Replace("/", "\\"));

                if (!File.Exists(fullPath))
                {
                    MessageBox.Show($"Không tìm thấy file PDF:\n{fullPath}", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Mở PDF bằng ứng dụng mặc định
                var psi = new ProcessStartInfo
                {
                    FileName = fullPath,
                    UseShellExecute = true // Quan trọng: cho phép Windows chọn app mặc định
                };
                Process.Start(psi);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở PDF:\n{ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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