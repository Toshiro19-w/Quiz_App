using System.Windows.Forms;
using WinFormsApp1.ViewModels;
using System.Drawing;

namespace WinFormsApp1.View.User.Controls.CourseControls.ContentControls
{
    public partial class ContentFlashcardControl : UserControl, IContentControl
    {
        public event Action<object, string>? ContentTypeChanged;
        public event Action<object>? DeleteRequested;
        
        public ContentFlashcardControl()
        {
            this.Width = 700; this.Height = 260; this.Margin = new Padding(0, 0, 0, 10);
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
            cboContentType.SelectedIndex = 2; // FlashcardSet
            cboContentType.SelectedIndexChanged += (s, e) => {
                var type = cboContentType.SelectedItem?.ToString();
                if (type != null && type != "FlashcardSet")
                {
                    ContentTypeChanged?.Invoke(this, type);
                }
            };
        }

        public void LoadFromViewModel(LessonContentBuilderViewModel vm)
        {
            if (vm == null) return;
            txtTitle.Text = vm.Title ?? string.Empty;
        }

        public LessonContentBuilderViewModel SaveToViewModel()
        {
            return new LessonContentBuilderViewModel
            {
                ContentType = "FlashcardSet",
                Title = txtTitle.Text.Trim()
            };
        }
    }
}