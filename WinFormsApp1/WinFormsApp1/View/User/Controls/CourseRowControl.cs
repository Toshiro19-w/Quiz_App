using System;
using System.Drawing;
using System.Windows.Forms;
using WinFormsApp1.Helpers;
using WinFormsApp1.Models.Entities;

namespace WinFormsApp1.View.User.Controls
{
	public partial class CourseRowControl : UserControl
	{
		private Course _course;
		private int _index;

		public event EventHandler<Course> SubmitClicked;
		public event EventHandler<Course> ViewClicked;
		public event EventHandler<Course> EditClicked;
		public event EventHandler<Course> DeleteClicked;

		public CourseRowControl()
		{
			InitializeComponent();
		}

		public void SetData(Course course, int index)
		{
			_course = course;
			_index = index;

			lblId.Text = index.ToString();
			lblTitle.Text = course.Title;

			// Load course cover image
			LoadCoverImage(course.CoverUrl);

			var moderationStatus = GetModerationStatusDisplay(course.ModerationStatus);
			lblModeration.Text = moderationStatus.Text;
			lblModeration.BackColor = moderationStatus.Color;
			lblCategory.Text = course.Category?.Name ?? "Chưa phân loại";
			lblStatus.Text = course.IsPublished ? "Đã xuất bản" : "Nháp";
			lblStatus.BackColor = course.IsPublished
				? Color.FromArgb(40, 167, 69)
				: Color.FromArgb(108, 117, 125);

			lblPrice.Text = $"{course.Price:N0} VNĐ";
			lblDate.Text = course.CreatedAt.ToString("dd/MM/yyyy");

			btnSubmit.Visible = (course.ModerationStatus != "Pending" && course.ModerationStatus != "Approved");
		}

		private async void LoadCoverImage(string coverUrl)
		{
			try
			{
				if (string.IsNullOrEmpty(coverUrl))
				{
					// Set default placeholder image
					picCover.Image = CreatePlaceholderImage();
					return;
				}

				// Check if it's a URL or local path
				if (coverUrl.StartsWith("http://") || coverUrl.StartsWith("https://"))
				{
					// Load from URL
					using var httpClient = new System.Net.Http.HttpClient();
					var imageBytes = await httpClient.GetByteArrayAsync(coverUrl);
					using var ms = new System.IO.MemoryStream(imageBytes);
					picCover.Image = Image.FromStream(ms);
				}
				else
				{
					// Load from local path using MediaHelper
					string projectRoot = MediaHelper.GetProjectRoot();
					string relativePath = coverUrl.Replace("/", "\\").TrimStart('\\');
					string fullPath = System.IO.Path.Combine(projectRoot, relativePath);

					if (System.IO.File.Exists(fullPath))
					{
						picCover.Image = Image.FromFile(fullPath);
					}
					else
					{
						picCover.Image = CreatePlaceholderImage();
					}
				}
			}
			catch
			{
				picCover.Image = CreatePlaceholderImage();
			}
		}

		private Image CreatePlaceholderImage()
		{
			// Create a simple colored placeholder image
			var bmp = new Bitmap(60, 40);
			using (var g = Graphics.FromImage(bmp))
			{
				g.Clear(Color.FromArgb(230, 230, 230));
				using (var font = new Font("Segoe UI", 12, FontStyle.Bold))
				using (var brush = new SolidBrush(Color.Gray))
				{
					var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
					g.DrawString("📚", font, brush, new RectangleF(0, 0, 60, 40), sf);
				}
			}
			return bmp;
		}

		private (string Text, Color Color) GetModerationStatusDisplay(string status)
		{
			return status switch
			{
				"Pending" => ("Chờ duyệt", Color.FromArgb(255, 193, 7)),
				"Approved" => ("Đã duyệt", Color.FromArgb(40, 167, 69)),
				"Rejected" => ("Từ chối", Color.FromArgb(220, 53, 69)),
				"NeedsRevision" => ("Cần sửa", Color.FromArgb(255, 152, 0)),
				_ => ("Chưa gửi", Color.FromArgb(108, 117, 125))
			};
		}

		private void BtnSubmit_Click(object sender, EventArgs e)
		{
			SubmitClicked?.Invoke(this, _course);
		}

		private void BtnView_Click(object sender, EventArgs e)
		{
			ViewClicked?.Invoke(this, _course);
		}

		private void BtnEdit_Click(object sender, EventArgs e)
		{
			EditClicked?.Invoke(this, _course);
		}

		private void BtnDelete_Click(object sender, EventArgs e)
		{
			DeleteClicked?.Invoke(this, _course);
		}

		private void CourseRowControl_MouseEnter(object sender, EventArgs e)
		{
			this.BackColor = ColorPalette.Background;
		}

		private void CourseRowControl_MouseLeave(object sender, EventArgs e)
		{
			this.BackColor = Color.White;
		}

		private void CourseRowControl_Load(object sender, EventArgs e)
		{

		}
	}
}
