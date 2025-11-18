using System; using System.Windows.Forms; using System.Linq; using WinFormsApp1.ViewModels; using WinFormsApp1.View.User.Controls.CourseControls;

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

        private void AddChapter(ChapterBuilderViewModel fromVm = null)
        {
            chapterCounter++;
            var chVm = fromVm ?? new ChapterBuilderViewModel { Title = $"Ch??ng {chapterCounter}" };
            var chapterControl = new ChapterItemControl { Margin = new Padding(0, 0, 0, 10) };
            chapterControl.LoadFromViewModel(chVm);
            chapterControl.AddLessonClicked += (s, e) => {
                // add a new lesson UI row
                var lessonPanel = new Panel { Width = 660, Height = 36, BackColor = System.Drawing.Color.Transparent, Margin = new Padding(0, 0, 0, 6) };
                var lbl = new Label { Text = "Bài h?c m?i", AutoSize = false, Width = 560, Height = 32, Location = new System.Drawing.Point(4, 2), TextAlign = ContentAlignment.MiddleLeft };
                var btnDel = new Button { Text = "Xóa", Width = 80, Height = 28, Location = new System.Drawing.Point(560, 2), BackColor = System.Drawing.Color.FromArgb(242, 75, 75), ForeColor = System.Drawing.Color.White, FlatStyle = FlatStyle.Flat };
                btnDel.FlatAppearance.BorderSize = 0;
                btnDel.Click += (ss, ee) => { chapterControl.Controls.OfType<FlowLayoutPanel>().FirstOrDefault()?.Controls.Remove(lessonPanel); };
                lessonPanel.Controls.Add(lbl); lessonPanel.Controls.Add(btnDel);
                chapterControl.Controls.OfType<FlowLayoutPanel>().FirstOrDefault()?.Controls.Add(lessonPanel);
            };

            flpChapters.Controls.Add(chapterControl);
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
            foreach (var chControl in flpChapters.Controls.OfType<ChapterItemControl>())
            {
                vm.Chapters.Add(chControl.ToViewModel());
            }
        }

        public void OnEnter() { }
        public void OnLeaving() { }
    }
}