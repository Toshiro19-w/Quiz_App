using System;
using System.Windows.Forms;
using WinFormsApp1.ControllersWin;
using WinFormsApp1.ViewModels;
using WinFormsApp1.Helpers;

namespace WinFormsApp1.View.User.Controls.CourseControls
{
    public partial class CourseBuilderControl : UserControl
    {
        private readonly CourseControllerWin _controller = new CourseControllerWin();
        private CourseBuilderViewModel vm = new CourseBuilderViewModel();

        public CourseBuilderControl()
        {
            InitializeComponent();
            HookEvents();
        }

        private void HookEvents()
        {
            txtTitle.TextChanged += (s, e) => txtSlug.Text = SlugHelper.GenerateSlug(txtTitle.Text);
            txtSlug.TextChanged += (s, e) => HideFieldError(lblSlugError);
            btnNext.Click += (s, e) => NextStep();
            btnPrev.Click += (s, e) => PrevStep();
            btnAddChapter.Click += (s, e) => AddChapter();
            btnSaveDraft.Click += async (s, e) => await SaveCourse(false);
            btnPublish.Click += async (s, e) => await SaveCourse(true);
            btnUploadCover.Click += BtnUploadCover_Click;
        }

        private void BtnUploadCover_Click(object? sender, EventArgs e)
        {
            using var dlg = new OpenFileDialog();
            dlg.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtCoverUrl.Text = dlg.FileName;
                picCover.ImageLocation = dlg.FileName;
            }
        }

        private void AddChapter()
        {
            var chapterPanel = new Panel { Width = 700, Height = 120, BackColor = System.Drawing.Color.White, BorderStyle = BorderStyle.FixedSingle, Margin = new Padding(0, 0, 0, 10) };
            var tb = new TextBox { Width = 600, Text = "Ch??ng m?i" };
            chapterPanel.Controls.Add(tb);
            flpChapters.Controls.Add(chapterPanel);
        }

        private void NextStep() { /* simplified: move to next tab */ tabControl1.SelectedIndex = Math.Min(3, tabControl1.SelectedIndex + 1); }
        private void PrevStep() { tabControl1.SelectedIndex = Math.Max(0, tabControl1.SelectedIndex - 1); }

        private void ShowFieldError(Label lbl, string message)
        {
            lbl.Text = message; lbl.Visible = true;
        }
        private void HideFieldError(Label lbl)
        {
            lbl.Text = string.Empty; lbl.Visible = false;
        }

        private async System.Threading.Tasks.Task SaveCourse(bool publish)
        {
            if (!ValidateStep1()) return;
            vm.Title = txtTitle.Text;
            vm.Slug = txtSlug.Text;
            vm.Summary = txtSummary.Text;
            vm.Price = decimal.TryParse(txtPrice.Text, out var p) ? p : 0;
            vm.CoverUrl = txtCoverUrl.Text;
            vm.IsPublished = publish;
            try
            {
                var id = await _controller.SaveCourseAsync(vm);
                MessageBox.Show("L?u thành công: " + id);
            }
            catch (Exception ex) { MessageBox.Show("L?i khi l?u: " + ex.Message); }
        }

        private bool ValidateStep1()
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text)) { ShowFieldError(lblTitleError, "Tiêu ?? b?t bu?c"); return false; }
            if (string.IsNullOrWhiteSpace(txtSlug.Text)) { ShowFieldError(lblSlugError, "Slug b?t bu?c"); return false; }
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtSlug.Text, "^[a-z0-9-]+$")) { ShowFieldError(lblSlugError, "Slug không h?p l?"); return false; }
            // server-side slug check
            var ok = Task.Run(() => _controller.IsSlugUniqueAsync(txtSlug.Text)).Result;
            if (!ok) { ShowFieldError(lblSlugError, "Slug ?ã t?n t?i"); return false; }
            return true;
        }
    }
}
