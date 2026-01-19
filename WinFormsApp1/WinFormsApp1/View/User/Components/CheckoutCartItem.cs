using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Net.Http;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using WinFormsApp1.Models.Entities;
using WinFormsApp1.Helpers;

namespace WinFormsApp1.View.User.Components
{
    public partial class CheckoutCartItem : UserControl
    {
        private Course _course;
        public event EventHandler<int>? OnRemoveClick;

        public CheckoutCartItem(Course course)
        {
            _course = course;
            InitializeComponent();
            LoadCourseData();
        }

        private async void LoadCourseData()
        {
            lblTitle.Text = _course.Title;
            lblInstructor.Text = $"👤 {_course.Owner?.FullName ?? "N/A"}";
            lblDate.Text = $"Thêm vào giỏ: {DateTime.Now:dd/MM/yyyy HH:mm}";
            lblPrice.Text = $"{_course.Price:N0} VND";

            // Load course image
            await LoadCourseImageAsync();
        }

        private async Task LoadCourseImageAsync()
        {
            try
            {
                Debug.WriteLine($"[CheckoutCartItem] CoverUrl: {_course.CoverUrl ?? "NULL"}");
                
                if (!string.IsNullOrEmpty(_course.CoverUrl))
                {
                    // Check if it's a local relative path first
                    if (ImageHelper.ImageExists(_course.CoverUrl))
                    {
                        Debug.WriteLine($"[CheckoutCartItem] Loading local image: {_course.CoverUrl}");
                        
                        var fullPath = ImageHelper.GetFullPath(_course.CoverUrl);
                        if (System.IO.File.Exists(fullPath))
                        {
                            using (var stream = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
                            {
                                var image = Image.FromStream(stream);
                                picCourseImage.Image = new Bitmap(image);
                                image.Dispose();
                            }
                            
                            picCourseImage.Visible = true;
                            lblImageIcon.Visible = false;
                            Debug.WriteLine("[CheckoutCartItem] Local image loaded successfully");
                            return;
                        }
                    }
                    
                    // If not local path, try as URL
                    if (_course.CoverUrl.StartsWith("http://", StringComparison.OrdinalIgnoreCase) || 
                        _course.CoverUrl.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
                    {
                        Debug.WriteLine($"[CheckoutCartItem] Loading image from URL: {_course.CoverUrl}");
                        
                        using (var httpClient = new HttpClient())
                        {
                            httpClient.Timeout = TimeSpan.FromSeconds(10);
                            var imageBytes = await httpClient.GetByteArrayAsync(_course.CoverUrl);
                            
                            Debug.WriteLine($"[CheckoutCartItem] Image loaded, size: {imageBytes.Length} bytes");
                            
                            using (var ms = new MemoryStream(imageBytes))
                            {
                                var image = Image.FromStream(ms);
                                picCourseImage.Image = new Bitmap(image);
                                image.Dispose();
                            }
                            
                            picCourseImage.Visible = true;
                            lblImageIcon.Visible = false;
                            Debug.WriteLine("[CheckoutCartItem] URL image loaded successfully");
                            return;
                        }
                    }
                    
                    // If it's an absolute local path
                    if (System.IO.File.Exists(_course.CoverUrl))
                    {
                        Debug.WriteLine($"[CheckoutCartItem] Loading from absolute path: {_course.CoverUrl}");
                        using (var stream = new FileStream(_course.CoverUrl, FileMode.Open, FileAccess.Read))
                        {
                            var image = Image.FromStream(stream);
                            picCourseImage.Image = new Bitmap(image);
                            image.Dispose();
                        }
                        
                        picCourseImage.Visible = true;
                        lblImageIcon.Visible = false;
                        Debug.WriteLine("[CheckoutCartItem] Absolute path image loaded successfully");
                        return;
                    }
                }
                
                // No valid image found, show icon
                Debug.WriteLine("[CheckoutCartItem] No valid image, showing icon");
                picCourseImage.Visible = false;
                lblImageIcon.Visible = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[CheckoutCartItem] Error loading image: {ex.Message}");
                // If image loading fails, show icon
                picCourseImage.Visible = false;
                lblImageIcon.Visible = true;
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            OnRemoveClick?.Invoke(this, _course.CourseId);
        }

        private void btnRemove_MouseEnter(object sender, EventArgs e)
        {
            btnRemove.BackColor = System.Drawing.Color.FromArgb(220, 53, 69);
            btnRemove.ForeColor = System.Drawing.Color.White;
        }

        private void btnRemove_MouseLeave(object sender, EventArgs e)
        {
            btnRemove.BackColor = System.Drawing.Color.White;
            btnRemove.ForeColor = System.Drawing.Color.FromArgb(220, 53, 69);
        }

        private void CheckoutCartItem_Paint(object sender, PaintEventArgs e)
        {
            // Draw rounded rectangle border
            using (GraphicsPath path = new GraphicsPath())
            {
                int radius = 10;
                Rectangle rect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
                
                path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
                path.AddArc(rect.X + rect.Width - radius, rect.Y, radius, radius, 270, 90);
                path.AddArc(rect.X + rect.Width - radius, rect.Y + rect.Height - radius, radius, radius, 0, 90);
                path.AddArc(rect.X, rect.Y + rect.Height - radius, radius, radius, 90, 90);
                path.CloseAllFigures();

                this.Region = new Region(path);

                using (Pen pen = new Pen(Color.FromArgb(230, 230, 230), 2))
                {
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    e.Graphics.DrawPath(pen, path);
                }
            }
        }
    }
}
