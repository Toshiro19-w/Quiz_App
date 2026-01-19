using System;
using System.Drawing;
using System.Windows.Forms;
using WinFormsApp1.Helpers;
using WinFormsApp1.ViewModels;
using System.Threading.Tasks;
using WinFormsApp1.Models.EF;
using Microsoft.EntityFrameworkCore;
using WinFormsApp1.Controllers;

namespace WinFormsApp1.View.User.Controls.CourseControls.Steps
{
	public partial class Step1_InfoControl : UserControl, IStepControl
	{
		private int? _pendingCategoryId = null;
		private int? _currentCourseId = null;
		private CourseBuilderController _controller = new CourseBuilderController();
		private string _coverImageRelativePath = null;

		public Step1_InfoControl()
		{
			InitializeComponent();
			
			// Enable double buffering for smooth scrolling
			EnableDoubleBuffering(this);
			
			HookEvents();
			_ = LoadCategoriesAsync();
		}
		
		/// <summary>
		/// Enable double buffering for a control to reduce flicker
		/// </summary>
		private void EnableDoubleBuffering(Control control)
		{
			typeof(Control).InvokeMember("DoubleBuffered",
				System.Reflection.BindingFlags.SetProperty |
				System.Reflection.BindingFlags.Instance |
				System.Reflection.BindingFlags.NonPublic,
				null, control, new object[] { true });
		}

		private void HookEvents()
		{
			txtTitle.TextChanged += (s, e) => { txtSlug.Text = SlugHelper.GenerateSlug(txtTitle.Text); ValidateTitle(); };
			txtSlug.TextChanged += async (s, e) => await ValidateSlugAsync();
			txtPrice.TextChanged += (s, e) => ValidatePrice();
			btnUploadCover.Click += BtnUploadCover_Click;
			btnNext.Click += async (s, e) => { if (await ValidateAllAsync()) OnNextRequested?.Invoke(this, EventArgs.Empty); };
		}

		private void ValidateTitle()
		{
			if (string.IsNullOrWhiteSpace(txtTitle.Text))
			{
				lblTitleLabel.ForeColor = Color.Red;
				lblTitleLabel.Text = "Tiêu đề (Bắt buộc)";
			}
			else
			{
				lblTitleLabel.ForeColor = Color.Black;
				lblTitleLabel.Text = "Tiêu đề";
			}
		}

		private async Task ValidateSlugAsync()
		{
			if (string.IsNullOrWhiteSpace(txtSlug.Text))
			{
				lblSlugLabel.ForeColor = Color.Red;
				lblSlugLabel.Text = "URL Slug (Bắt buộc)";
				return;
			}

			var isUnique = await _controller.IsSlugUniqueAsync(txtSlug.Text, _currentCourseId);
			if (!isUnique)
			{
				lblSlugLabel.ForeColor = Color.Red;
				lblSlugLabel.Text = "URL Slug (Đã tồn tại)";
			}
			else
			{
				lblSlugLabel.ForeColor = Color.Black;
				lblSlugLabel.Text = "URL Slug";
			}
		}

		private void ValidatePrice()
		{
			if (string.IsNullOrWhiteSpace(txtPrice.Text) || !decimal.TryParse(txtPrice.Text, out var price))
			{
				lblPriceLabel.ForeColor = Color.Red;
				lblPriceLabel.Text = "Giá (VNĐ) - Bắt buộc nhập";
				return;
			}

			if (price < 2000)
			{
				lblPriceLabel.ForeColor = Color.Red;
				lblPriceLabel.Text = "Giá (VNĐ) - Phải lớn hơn 2000";
			}
			else
			{
				lblPriceLabel.ForeColor = Color.Black;
				lblPriceLabel.Text = "Giá (VNĐ)";
			}
		}

		private async Task<bool> ValidateAllAsync()
		{
			ValidateTitle();
			await ValidateSlugAsync();
			ValidatePrice();

			if (string.IsNullOrWhiteSpace(txtTitle.Text)) return false;
			if (string.IsNullOrWhiteSpace(txtSlug.Text)) return false;

			var isUnique = await _controller.IsSlugUniqueAsync(txtSlug.Text, _currentCourseId);
			if (!isUnique) return false;

			if (string.IsNullOrWhiteSpace(txtPrice.Text) || !decimal.TryParse(txtPrice.Text, out var price)) return false;
			if (price <= 2000) return false;

			return true;
		}

		private void BtnUploadCover_Click(object? sender, EventArgs e)
		{
			try
			{
				System.Diagnostics.Debug.WriteLine("[Step1_InfoControl] Upload Cover clicked");
				
				using var dlg = new OpenFileDialog();
				dlg.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
				dlg.Title = "Chọn ảnh bìa khóa học";
				
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					System.Diagnostics.Debug.WriteLine($"[Step1_InfoControl] Selected file: {dlg.FileName}");
					
					// Generate custom file name based on course slug or title
					var customFileName = !string.IsNullOrWhiteSpace(txtSlug.Text) 
						? txtSlug.Text 
						: SlugHelper.GenerateSlug(txtTitle.Text);

					System.Diagnostics.Debug.WriteLine($"[Step1_InfoControl] Custom file name: {customFileName}");

					// Save and optimize image to Library/Image
					_coverImageRelativePath = ImageHelper.SaveAndOptimizeImage(
						dlg.FileName, 
						maxWidth: 1920, 
						maxHeight: 1080, 
						customFileName: customFileName
					);

					System.Diagnostics.Debug.WriteLine($"[Step1_InfoControl] Saved relative path: {_coverImageRelativePath}");

					// Display image in PictureBox
					var fullPath = ImageHelper.GetFullPath(_coverImageRelativePath);
					System.Diagnostics.Debug.WriteLine($"[Step1_InfoControl] Full path: {fullPath}");
					
					picCover.ImageLocation = fullPath;

					MessageBox.Show(
						$"Ảnh đã được lưu tại:\n{_coverImageRelativePath}\n\nFull path:\n{fullPath}", 
						"Thành công", 
						MessageBoxButtons.OK, 
						MessageBoxIcon.Information
					);
				}
				else
				{
					System.Diagnostics.Debug.WriteLine("[Step1_InfoControl] Dialog cancelled");
				}
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine($"[Step1_InfoControl] ERROR: {ex.Message}");
				System.Diagnostics.Debug.WriteLine($"[Step1_InfoControl] Stack trace: {ex.StackTrace}");
				
				MessageBox.Show(
					$"Lỗi khi upload ảnh: {ex.Message}\n\nStack trace:\n{ex.StackTrace}", 
					"Lỗi", 
					MessageBoxButtons.OK, 
					MessageBoxIcon.Error
				);
			}
		}

		public event EventHandler? OnNextRequested;

		public void OnEnter() { /* focus first control */ txtTitle.Focus(); }
		public void OnLeaving() { /* nothing */ }

		public void LoadFromViewModel(CourseBuilderViewModel vm)
		{
			if (vm == null) return;
			_currentCourseId = vm.CourseId;
			txtTitle.Text = vm.Title ?? string.Empty;
			txtSlug.Text = vm.Slug ?? string.Empty;
			txtSummary.Text = vm.Summary ?? string.Empty;
			// Format price without trailing .00
			txtPrice.Text = vm.Price.HasValue ? vm.Price.Value.ToString("F0") : string.Empty;
			
			// Load cover image from relative path
			if (!string.IsNullOrEmpty(vm.CoverUrl))
			{
				_coverImageRelativePath = vm.CoverUrl;
				
				// Check if it's already a full path or relative path
				if (ImageHelper.ImageExists(vm.CoverUrl))
				{
					var fullPath = ImageHelper.GetFullPath(vm.CoverUrl);
					picCover.ImageLocation = fullPath;
				}
				else if (System.IO.File.Exists(vm.CoverUrl))
				{
					// If it's a full path that exists
					picCover.ImageLocation = vm.CoverUrl;
				}
			}

			if (vm.CategoryId.HasValue)
			{
				_pendingCategoryId = vm.CategoryId.Value;
				try { cmbCategory.SelectedValue = vm.CategoryId.Value; } catch { }
			}
			else
			{
				_pendingCategoryId = null;
				if (cmbCategory.Items.Count > 0)
				{
					try { cmbCategory.SelectedIndex = 0; } catch { }
				}
			}
		}

		public void SaveToViewModel(CourseBuilderViewModel vm)
		{
			if (vm == null) return;
			vm.Title = txtTitle.Text.Trim();
			vm.Slug = txtSlug.Text.Trim();
			vm.Summary = txtSummary.Text;
			vm.Price = decimal.TryParse(txtPrice.Text, out var p) ? p : 0;
			
			// Save relative path to ViewModel
			vm.CoverUrl = _coverImageRelativePath ?? picCover.ImageLocation;
			
			if (cmbCategory.SelectedValue is int id) vm.CategoryId = id;
		}

		private async Task LoadCategoriesAsync()
		{
			try
			{
				using var ctx = new LearningPlatformContext();
				var cats = await ctx.CourseCategories.OrderBy(c => c.DisplayOrder).ThenBy(c => c.Name).ToListAsync();
				if (cats == null || cats.Count == 0)
				{
					this.BeginInvoke((Action)(() => MessageBox.Show("Không tìm thấy danh mục khóa học.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information)));
					return;
				}

				this.BeginInvoke((Action)(() =>
				{
					cmbCategory.DisplayMember = "Name";
					cmbCategory.ValueMember = "CategoryId";
					cmbCategory.DataSource = cats;

					// apply pending selection if any
					if (_pendingCategoryId.HasValue)
					{
						try { cmbCategory.SelectedValue = _pendingCategoryId.Value; } catch { }
					}
					else if (cmbCategory.Items.Count > 0)
					{
						try { cmbCategory.SelectedIndex = 0; } catch { }
					}
				}));
			}
			catch (Exception ex)
			{
				this.BeginInvoke((Action)(() => MessageBox.Show("Lỗi khi tải danh mục: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error)));
			}
		}

		private void pnlCard_Paint(object sender, PaintEventArgs e)
		{

		}
	}
}