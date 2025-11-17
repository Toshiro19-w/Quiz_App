using System; using System.Windows.Forms; using WinFormsApp1.Helpers; using WinFormsApp1.ViewModels;

namespace WinFormsApp1.View.User.Controls.CourseControls.Steps
{
    public partial class Step1_InfoControl : UserControl, IStepControl
    {
        public Step1_InfoControl()
        {
            InitializeComponent();
            HookEvents();
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
            txtPrice.Text = vm.Price.ToString();
            // category and cover can be mapped by index/url if needed
            if (!string.IsNullOrEmpty(vm.CoverUrl)) picCover.ImageLocation = vm.CoverUrl;
        }

        public void SaveToViewModel(CourseBuilderViewModel vm)
        {
            if (vm == null) return;
            vm.Title = txtTitle.Text.Trim();
            vm.Slug = txtSlug.Text.Trim();
            vm.Summary = txtSummary.Text;
            vm.Price = decimal.TryParse(txtPrice.Text, out var p) ? p : 0;
            vm.CoverUrl = picCover.ImageLocation;
        }
    }
}