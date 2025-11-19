using WinFormsApp1.ViewModels;
using System.Windows.Forms;

namespace WinFormsApp1.View.User.Controls.CourseControls.ContentControls
{
    public interface IContentControl
    {
        void LoadFromViewModel(LessonContentBuilderViewModel vm);
        LessonContentBuilderViewModel SaveToViewModel();
        event Action<object>? DeleteRequested;
    }
}