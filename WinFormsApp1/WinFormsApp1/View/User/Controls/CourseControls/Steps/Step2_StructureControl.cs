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
            var chVm = fromVm ?? new ChapterBuilderViewModel { Title = $"Chương {chapterCounter}" };
            var chapterControl = new ChapterItemControl { Margin = new Padding(0, 0, 0, 10) };
            chapterControl.LoadFromViewModel(chVm);

            // When AddLesson is requested from the chapter control, use its API to add an editable lesson
            chapterControl.AddLessonClicked += (s, e) => {
                chapterControl.AddNewLesson();
            };

            // When chapter requests removal, remove it from the flow panel
            chapterControl.RemoveChapterClicked += (s, e) => {
                flpChapters.Controls.Remove(chapterControl);
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