using System; using System.Windows.Forms; using WinFormsApp1.Helpers; using WinFormsApp1.ViewModels;
using System.Threading.Tasks;
using WinFormsApp1.Models.EF;
using Microsoft.EntityFrameworkCore;

namespace WinFormsApp1.View.User.Controls.CourseControls.Steps
{
    public partial class Step1_InfoControl : UserControl, IStepControl
    {
        private int? _pendingCategoryId = null;

        public Step1_InfoControl()
        {
            InitializeComponent();
            HookEvents();
            _ = LoadCategoriesAsync();
        }

        private void HookEvents()
        {
            txtTitle.TextChanged += (s,e)=> txtSlug.Text = SlugHelper.GenerateSlug(txtTitle.Text);
            btnUploadCover.Click += BtnUploadCover_Click;
            // ensure there's a Next button and wire it
            btnNext.Click += (s,e)=> OnNextRequested?.Invoke(this, EventArgs.Empty);
        }

        private void BtnUploadCover_Click(object? sender, EventArgs e)
        {
            using var dlg = new OpenFileDialog();
            dlg.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                picCover.ImageLocation = dlg.FileName;
            }
        }

        public event EventHandler? OnNextRequested;

        public void OnEnter() { /* focus first control */ txtTitle.Focus(); }
        public void OnLeaving() { /* nothing */ }

        public void LoadFromViewModel(CourseBuilderViewModel vm)
        {
            if (vm == null) return;
            txtTitle.Text = vm.Title ?? string.Empty;
            txtSlug.Text = vm.Slug ?? string.Empty;
            txtSummary.Text = vm.Summary ?? string.Empty;
            txtPrice.Text = vm.Price?.ToString() ?? string.Empty;
            // category and cover can be mapped by index/url if needed
            if (!string.IsNullOrEmpty(vm.CoverUrl)) picCover.ImageLocation = vm.CoverUrl;

            if (vm.CategoryId.HasValue)
            {
                _pendingCategoryId = vm.CategoryId.Value;
                try { cmbCategory.SelectedValue = vm.CategoryId.Value; } catch { }
            }
            else
            {
                _pendingCategoryId = null;
                // if categories already loaded, select first
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
            vm.CoverUrl = picCover.ImageLocation;
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

                this.BeginInvoke((Action)(() => {
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
    }
}