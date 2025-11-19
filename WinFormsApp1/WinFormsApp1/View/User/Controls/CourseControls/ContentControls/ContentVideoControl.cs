using System.Windows.Forms;
using WinFormsApp1.ViewModels;
using System.Drawing;
using WinFormsApp1.Helpers;

namespace WinFormsApp1.View.User.Controls.CourseControls.ContentControls
{
    public partial class ContentVideoControl : UserControl, IContentControl
    {
        public event Action<object, string>? ContentTypeChanged;
        public event Action<object>? DeleteRequested;
        
        public ContentVideoControl()
        {
            this.Width = 700; this.Height = 120; this.Margin = new Padding(0, 0, 0, 10);
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

            // Set default selection and add event handler
            cboContentType.SelectedIndex = 1; // Video
            cboContentType.SelectedIndexChanged += (s, e) => {
                var type = cboContentType.SelectedItem?.ToString();
                if (type != null && type != "Video")
                {
                    ContentTypeChanged?.Invoke(this, type);
                }
            };

            // wire default browse handler from designer field
            btnBrowse.Click += (s, e) => {
                using var ofd = new OpenFileDialog();
                ofd.Filter = "Video files|*.mp4;*.webm;*.ogg;*.mov;*.avi;*.mkv";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    var libraryPath = MediaHelper.CopyVideoToLibrary(ofd.FileName);
                    if (libraryPath != null)
                        txtVideoPath.Text = libraryPath;
                }
            };
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