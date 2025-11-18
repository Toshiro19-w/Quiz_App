using System.Windows.Forms;
using WinFormsApp1.ViewModels;
using System.Drawing;

namespace WinFormsApp1.View.User.Controls.CourseControls.ContentControls
{
    public partial class ContentTheoryControl : UserControl, IContentControl
    {
        public ContentTheoryControl()
        {
            this.Width = 700; this.Height = 200; this.Margin = new Padding(0, 0, 0, 10);
            this.BorderStyle = BorderStyle.FixedSingle;
            InitializeComponent();

            // default selection
            cboContentType.SelectedIndexChanged += (s, e) => OnContentTypeChanged();
            if (cboContentType.Items.Count > 0) cboContentType.SelectedIndex = 0;
        }

        private void OnContentTypeChanged()
        {
            // For theory control we only show title/body; other types might hide/show controls
            var type = cboContentType.SelectedItem?.ToString();
            if (type == "Video")
            {
                lblBody.Visible = false;
                txtBody.Visible = false;
            }
            else
            {
                lblBody.Visible = true;
                txtBody.Visible = true;
            }
        }

        public void LoadFromViewModel(LessonContentBuilderViewModel vm)
        {
            if (vm == null) return;
            cboContentType.SelectedItem = string.IsNullOrEmpty(vm.ContentType) ? "Theory" : vm.ContentType;
            txtTitle.Text = vm.Title ?? string.Empty;
            txtBody.Text = vm.Body ?? string.Empty;
        }

        public LessonContentBuilderViewModel SaveToViewModel()
        {
            return new LessonContentBuilderViewModel
            {
                ContentType = cboContentType.SelectedItem?.ToString() ?? "Theory",
                Title = txtTitle.Text.Trim(),
                Body = txtBody.Text
            };
        }
    }
}