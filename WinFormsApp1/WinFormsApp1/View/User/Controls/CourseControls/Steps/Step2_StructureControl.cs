using System; using System.Windows.Forms; using System.Linq; using WinFormsApp1.ViewModels;

namespace WinFormsApp1.View.User.Controls.CourseControls.Steps
{
    public partial class Step2_StructureControl : UserControl, IStepControl
    {
        public Step2_StructureControl()
        {
            InitializeComponent();
            btnAddChapter.Click += (s,e)=> AddChapter();
            btnPrev.Click += (s,e)=> OnPrevRequested?.Invoke(this, EventArgs.Empty);
            btnNext.Click += (s,e)=> OnNextRequested?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler? OnPrevRequested;
        public event EventHandler? OnNextRequested;

        private int chapterCounter = 0;

        private void AddChapter(ChapterDto fromVm = null)
        {
            chapterCounter++;
            var wrapper = new Guna.UI2.WinForms.Guna2ShadowPanel { Width = 700, AutoSize = true, Margin = new Padding(0, 0, 0, 10) };
            var title = new Guna.UI2.WinForms.Guna2TextBox { Text = fromVm?.Title ?? $"Ch??ng {chapterCounter}", Width = 600, Location = new System.Drawing.Point(10, 10) };
            var btnAddLesson = new Guna.UI2.WinForms.Guna2Button { Text = "Thêm bài h?c", Location = new System.Drawing.Point(620, 10), Size = new System.Drawing.Size(100, 28) };
            var flpLessons = new FlowLayoutPanel { Location = new System.Drawing.Point(10, 48), Size = new System.Drawing.Size(680, 120), AutoScroll = true };

            wrapper.Controls.Add(title); wrapper.Controls.Add(btnAddLesson); wrapper.Controls.Add(flpLessons);

            btnAddLesson.Click += (s, e) => {
                var tb = new Guna.UI2.WinForms.Guna2TextBox { Width = 620, Text = "Bài h?c m?i" };
                flpLessons.Controls.Add(tb);
            };

            // if fromVm provided, populate lessons
            if (fromVm?.Lessons != null)
            {
                foreach (var ls in fromVm.Lessons)
                {
                    var tb = new Guna.UI2.WinForms.Guna2TextBox { Width = 620, Text = ls.Title };
                    flpLessons.Controls.Add(tb);
                }
            }

            flpChapters.Controls.Add(wrapper);
        }

        public void LoadFromViewModel(CourseBuilderViewModel vm)
        {
            flpChapters.Controls.Clear();
            chapterCounter = 0;
            if (vm?.Chapters != null)
            {
                foreach (var ch in vm.Chapters)
                {
                    AddChapter(ch);
                }
            }
        }

        public void SaveToViewModel(CourseBuilderViewModel vm)
        {
            vm.Chapters.Clear();
            foreach (var chPanel in flpChapters.Controls.OfType<Guna.UI2.WinForms.Guna2ShadowPanel>())
            {
                var titleBox = chPanel.Controls.OfType<Guna.UI2.WinForms.Guna2TextBox>().FirstOrDefault();
                var lessonsPanel = chPanel.Controls.OfType<FlowLayoutPanel>().FirstOrDefault();
                var ch = new ChapterDto { Title = titleBox?.Text ?? string.Empty };
                if (lessonsPanel != null)
                {
                    foreach (var l in lessonsPanel.Controls.OfType<Guna.UI2.WinForms.Guna2TextBox>())
                    {
                        ch.Lessons.Add(new LessonDto { Title = l.Text });
                    }
                }
                vm.Chapters.Add(ch);
            }
        }

        public void OnEnter() { }
        public void OnLeaving() { }
    }
}