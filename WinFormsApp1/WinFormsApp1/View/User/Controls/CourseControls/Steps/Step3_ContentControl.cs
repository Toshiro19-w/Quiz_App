using System; using System.Linq; using System.Windows.Forms; using WinFormsApp1.ViewModels;

namespace WinFormsApp1.View.User.Controls.CourseControls.Steps
{
    public partial class Step3_ContentControl : UserControl, IStepControl
    {
        public Step3_ContentControl()
        {
            InitializeComponent();
            btnAddContent.Click += (s,e)=> AddContent();
            btnPrev.Click += (s,e)=> OnPrevRequested?.Invoke(this, EventArgs.Empty);
            btnNext.Click += (s,e)=> OnNextRequested?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler? OnPrevRequested;
        public event EventHandler? OnNextRequested;

        private int contentCounter = 0;

        private void AddContent(string? title = null)
        {
            contentCounter++;
            var panel = new Guna.UI2.WinForms.Guna2ShadowPanel { Width = 700, Height = 120, Margin = new Padding(0,0,0,10) };
            var tb = new Guna.UI2.WinForms.Guna2TextBox { Text = title ?? $"N?i dung {contentCounter}", Width = 600, Location = new System.Drawing.Point(10,10) };
            panel.Controls.Add(tb);
            flpContents.Controls.Add(panel);
        }

        public void LoadFromViewModel(CourseBuilderViewModel vm)
        {
            flpContents.Controls.Clear();
            contentCounter = 0;

            // populate flat list of contents from first lesson if exists
            var firstLesson = vm?.Chapters?.FirstOrDefault()?.Lessons?.FirstOrDefault();
            if (firstLesson != null && firstLesson.Contents != null)
            {
                foreach (var c in firstLesson.Contents)
                {
                    AddContent(c.Title);
                }
            }
        }

        public void SaveToViewModel(CourseBuilderViewModel vm)
        {
            // For simplicity, map back to first lesson
            if (vm?.Chapters == null || vm.Chapters.Count == 0) return;
            var firstLesson = vm.Chapters[0].Lessons.Count > 0 ? vm.Chapters[0].Lessons[0] : null;
            if (firstLesson == null) return;

            firstLesson.Contents.Clear();
            foreach (var p in flpContents.Controls.OfType<Guna.UI2.WinForms.Guna2ShadowPanel>())
            {
                var tb = p.Controls.OfType<Guna.UI2.WinForms.Guna2TextBox>().FirstOrDefault();
                if (tb != null)
                {
                    firstLesson.Contents.Add(new LessonContentBuilderViewModel { Title = tb.Text });
                }
            }
        }

        public void OnEnter() { }
        public void OnLeaving() { }
    }
}