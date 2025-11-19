using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp1.View.User.Controls.CourseControls.Steps;
using WinFormsApp1.ControllersWin;
using WinFormsApp1.View.User.Controls.CourseControls;
using WinFormsApp1.ViewModels;
using WinFormsApp1.Helpers;

namespace WinFormsApp1.View.User.Forms
{
    public partial class CourseBuilderForm : Form
    {
        private readonly CourseBuilderController _controller = new CourseBuilderController();
        private UserControl[] steps;
        private int currentStep = 0;
        private CourseBuilderViewModel vm = new CourseBuilderViewModel { OwnerId = AuthHelper.CurrentUser.UserId };
        private int? editingCourseId = null; // holds course id when editing

        public CourseBuilderForm()
        {
            InitializeComponent();
            InitializeForm();

            // default behavior: load step 0 now that steps are initialized and vm is default
            LoadStep(0, animate: false);
        }

        // original constructor: load by id asynchronously
        public CourseBuilderForm(int courseId) : this()
        {
            editingCourseId = courseId;
            _ = LoadExistingCourseAsync(courseId);
        }

        // constructor that accepts a preloaded VM
        public CourseBuilderForm(CourseBuilderViewModel preloadedVm, int courseId) : this()
        {
            if (preloadedVm != null)
            {
                // replace vm before loading UI so controls receive data
                vm = preloadedVm;
                editingCourseId = courseId;

                // load current step control with new vm
                if (pnlContent.Controls.Count > 0 && pnlContent.Controls[0] is IStepControl scCurrent)
                {
                    scCurrent.LoadFromViewModel(vm);
                }
                else
                {
                    LoadStep(0, animate: false);
                }
            }
        }

        private void InitializeForm()
        {
            InitSteps();
            HookEvents();
        }

        private async Task LoadExistingCourseAsync(int courseId)
        {
            try
            {
                var loaded = await _controller.LoadCourseAsync(courseId);
                if (loaded != null)
                {
                    vm = loaded;
                    editingCourseId = courseId;
                    // reload current step control with new vm on UI thread
                    if (pnlContent.Controls.Count > 0 && pnlContent.Controls[0] is IStepControl existing)
                    {
                        this.BeginInvoke((Action)(() => existing.LoadFromViewModel(vm)));
                    }
                    else
                    {
                        this.BeginInvoke((Action)(() => LoadStep(currentStep, animate: false)));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải khóa học: " + ex.Message);
            }
        }

        private void InitSteps()
        {
            steps = new UserControl[] {
                new Step1_InfoControl(),
                new Step2_StructureControl(),
                new Step3_ContentControl(),
                new Step4_PublishControl()
            };

            // subscribe to navigation events from steps
            if (steps[0] is Step1_InfoControl s1) s1.OnNextRequested += async (s, e) => await StepNextRequestedAsync(0);
            if (steps[1] is Step2_StructureControl s2) { s2.OnPrevRequested += async (s,e)=> await StepPrevRequestedAsync(1); s2.OnNextRequested += async (s,e)=> await StepNextRequestedAsync(1); }
            if (steps[2] is Step3_ContentControl s3) { s3.OnPrevRequested += async (s,e)=> await StepPrevRequestedAsync(2); s3.OnNextRequested += async (s,e)=> await StepNextRequestedAsync(2); }
            if (steps[3] is Step4_PublishControl s4) { s4.OnSaveDraftRequested += async (s,e)=> await SaveCourseAsync(false); s4.OnPublishRequested += async (s,e)=> await SaveCourseAsync(true); s4.OnCancelRequested += (s,e)=> { this.DialogResult = DialogResult.Cancel; this.Close(); }; }
        }

        private void HookEvents()
        {
            btnStep1.Click += (s, e) => LoadStep(0);
            btnStep2.Click += (s, e) => LoadStep(1);
            btnStep3.Click += (s, e) => LoadStep(2);
            btnStep4.Click += (s, e) => LoadStep(3);
        }

        private async Task StepNextRequestedAsync(int stepIndex)
        {
            if (stepIndex != currentStep) return;
            var cur = steps[currentStep] as IStepControl;
            cur?.SaveToViewModel(vm);
            var ok = await ValidateStepAsync(currentStep);
            if (!ok) return;
            var next = currentStep + 1;
            LoadStep(next);
        }

        private async Task StepPrevRequestedAsync(int stepIndex)
        {
            if (stepIndex != currentStep) return;
            var cur = steps[currentStep] as IStepControl;
            cur?.SaveToViewModel(vm);
            var prev = Math.Max(0, currentStep - 1);
            LoadStep(prev);
        }

        private void LoadStep(int stepIndex, bool animate = true)
        {
            if (stepIndex < 0 || stepIndex >= steps.Length) return;
            var newControl = steps[stepIndex];
            newControl.Dock = DockStyle.Fill;

            // call save current step if exists
            if (pnlContent.Controls.Count > 0)
            {
                var old = pnlContent.Controls[0] as IStepControl;
                old?.OnLeaving();
                old?.SaveToViewModel(vm);
            }

            // highlight stepper
            HighlightStep(stepIndex);

            // load data into new control
            if (newControl is IStepControl sc) sc.LoadFromViewModel(vm);

            if (animate && pnlContent.Controls.Count > 0)
            {
                var oldCtrl = pnlContent.Controls[0];
                oldCtrl.Left = 0;
                newControl.Left = pnlContent.Width;
                pnlContent.Controls.Add(newControl);
                var t = new System.Windows.Forms.Timer { Interval = 15 };
                t.Tick += (s, e) => {
                    int step = 40;
                    if (newControl.Left <= 0) { t.Stop(); pnlContent.Controls.Remove(oldCtrl); t.Dispose(); ((IStepControl)newControl).OnEnter(); return; }
                    newControl.Left -= step; oldCtrl.Left -= step;
                };
                t.Start();
            }
            else
            {
                pnlContent.Controls.Clear();
                pnlContent.Controls.Add(newControl);
                ((IStepControl)newControl).OnEnter();
            }

            currentStep = stepIndex;
        }

        private void HighlightStep(int stepIndex)
        {
            var inactiveColor = System.Drawing.Color.LightGray;
            var activeColor = System.Drawing.Color.FromArgb(88, 86, 233);
            btnStep1.FillColor = inactiveColor; btnStep2.FillColor = inactiveColor; btnStep3.FillColor = inactiveColor; btnStep4.FillColor = inactiveColor;
            sep12.FillColor = inactiveColor; sep23.FillColor = inactiveColor; sep34.FillColor = inactiveColor;
            switch (stepIndex)
            {
                case 0: btnStep1.FillColor = activeColor; break;
                case 1: btnStep1.FillColor = activeColor; sep12.FillColor = activeColor; btnStep2.FillColor = activeColor; break;
                case 2: btnStep1.FillColor = activeColor; sep12.FillColor = activeColor; btnStep2.FillColor = activeColor; sep23.FillColor = activeColor; btnStep3.FillColor = activeColor; break;
                case 3: btnStep1.FillColor = activeColor; sep12.FillColor = activeColor; btnStep2.FillColor = activeColor; sep23.FillColor = activeColor; btnStep3.FillColor = activeColor; sep34.FillColor = activeColor; btnStep4.FillColor = activeColor; break;
            }
        }

        private async Task<bool> ValidateStepAsync(int stepIndex)
        {
            if (stepIndex == 0)
            {
                var title = vm.Title?.Trim() ?? string.Empty;
                var slug = vm.Slug?.Trim() ?? string.Empty;
                if (string.IsNullOrEmpty(title)) { MessageBox.Show("Tiêu đề là bắt buộc"); return false; }
                if (string.IsNullOrEmpty(slug)) { MessageBox.Show("Slug bắt buộc"); return false; }
                if (!System.Text.RegularExpressions.Regex.IsMatch(slug, "^[a-z0-9-]+$")) { MessageBox.Show("Slug không hợp lệ"); return false; }
                var ok = await _controller.IsSlugUniqueAsync(slug, editingCourseId);
                if (!ok) { MessageBox.Show("Slug đã tồn tại"); return false; }
            }

            if (stepIndex == 1)
            {
                if (vm.Chapters == null || vm.Chapters.Count == 0) { MessageBox.Show("Vui lòng thêm ít nhất một chương"); return false; }
                bool hasLesson = false;
                foreach (var ch in vm.Chapters) { if (ch.Lessons != null && ch.Lessons.Count > 0) { hasLesson = true; break; } }
                if (!hasLesson) { MessageBox.Show("Vui lòng thêm ít nhất một bài học"); return false; }
            }

            return true;
        }

        private async Task SaveCourseAsync(bool publish)
        {
            if (pnlContent.Controls.Count > 0)
            {
                var cur = pnlContent.Controls[0] as IStepControl;
                cur?.SaveToViewModel(vm);
            }

            vm.IsPublished = publish;
            try
            {
                var id = await _controller.SaveCourseAsync(vm, editingCourseId);
                MessageBox.Show("Lưu thành công: " + id);
                // close dialog when save/publish is successful
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex) { MessageBox.Show("Lỗi khi lưu: " + ex.Message); }
        }
    }
}
