using System.Windows.Forms;
using WinFormsApp1.ViewModels;
using System.Drawing;

namespace WinFormsApp1.View.User.Controls.CourseControls.ContentControls
{
    public partial class ContentVideoControl : UserControl, IContentControl
    {
        public ContentVideoControl()
        {
            this.Width = 700; this.Height = 120; this.Margin = new Padding(0, 0, 0, 10);
            this.BorderStyle = BorderStyle.FixedSingle;
            InitializeComponent();

            // wire default browse handler from designer field
            btnBrowse.Click += (s, e) => { using var ofd = new OpenFileDialog(); ofd.Filter = "Video files|*.mp4;*.webm;*.ogg;*.mov;*.avi;*.mkv"; if (ofd.ShowDialog() == DialogResult.OK) txtVideoPath.Text = ofd.FileName; };
        }

        public void LoadFromViewModel(LessonContentBuilderViewModel vm)
        {
            if (vm == null) return;
            txtTitle.Text = vm.Title ?? string.Empty;
            txtVideoPath.Text = vm.VideoUrl ?? string.Empty;
        }

        public LessonContentBuilderViewModel SaveToViewModel()
        {
            return new LessonContentBuilderViewModel
            {
                ContentType = "Video",
                Title = txtTitle.Text.Trim(),
                VideoUrl = txtVideoPath.Text.Trim()
            };
        }
    }
}