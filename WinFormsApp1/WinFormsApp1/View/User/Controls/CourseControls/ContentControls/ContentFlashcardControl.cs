using System.Windows.Forms;
using WinFormsApp1.ViewModels;
using System.Drawing;

namespace WinFormsApp1.View.User.Controls.CourseControls.ContentControls
{
    public partial class ContentFlashcardControl : UserControl, IContentControl
    {
        public ContentFlashcardControl()
        {
            this.Width = 700; this.Height = 100; this.Margin = new Padding(0, 0, 0, 10);
            this.BorderStyle = BorderStyle.FixedSingle;
            InitializeComponent();
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