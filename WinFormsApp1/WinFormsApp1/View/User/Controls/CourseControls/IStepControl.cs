using WinFormsApp1.ViewModels;

namespace WinFormsApp1.View.User.Controls.CourseControls
{
    public interface IStepControl
    {
        void OnEnter();
        void OnLeaving();
        void LoadFromViewModel(CourseBuilderViewModel vm);
        void SaveToViewModel(CourseBuilderViewModel vm);
    }
}