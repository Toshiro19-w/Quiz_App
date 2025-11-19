using System; using System.Windows.Forms; using System.Linq; using WinFormsApp1.ViewModels; using WinFormsApp1.View.User.Controls.CourseControls;

namespace WinFormsApp1.View.User.Controls.CourseControls.Steps
{
    public partial class Step2_StructureControl : UserControl, IStepControl
    {
        public Step2_StructureControl()
        {
            InitializeComponent();
            
            // Đảm bảo nút được kích hoạt
            btnAddChapter.Enabled = true;
            btnAddChapter.Visible = true;
            
            // Kết nối sự kiện với error handling
            btnAddChapter.Click += BtnAddChapter_Click;
            btnPrev.Click += (s,e)=> OnPrevRequested?.Invoke(this, EventArgs.Empty);
            btnNext.Click += (s,e)=> OnNextRequested?.Invoke(this, EventArgs.Empty);
        }
        
        private void BtnAddChapter_Click(object sender, EventArgs e)
        {
            AddChapter();
        }

        public event EventHandler? OnPrevRequested;
        public event EventHandler? OnNextRequested;

        private int chapterCounter = 0;

        public CourseBuilderViewModel? CurrentCourse { get; set; }
        
		private void AddChapter(ChapterBuilderViewModel fromVm = null)
		{
			ChapterBuilderViewModel chVm;

			if (fromVm != null)
			{
				chVm = fromVm;
			}
			else
			{
				chapterCounter++;
				chVm = new ChapterBuilderViewModel { 
					Title = $"Chương {chapterCounter}",
					OrderIndex = chapterCounter
				};
			}

			var chapterControl = new ChapterItemControl();
			chapterControl.AddLessonClicked += (s, e) => ((ChapterItemControl)s).AddNewLesson();
			chapterControl.LoadFromViewModel(chVm);
			flpChapters.Controls.Add(chapterControl);
		}


		public void LoadFromViewModel(CourseBuilderViewModel vm)
		{
			flpChapters.Controls.Clear();
			chapterCounter = 0;

			if (vm?.Chapters == null || vm.Chapters.Count == 0)
				return;

			foreach (var ch in vm.Chapters)
			{
				// Sử dụng AddChapter để đảm bảo event được kết nối
				AddChapter(ch);
				chapterCounter++;
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