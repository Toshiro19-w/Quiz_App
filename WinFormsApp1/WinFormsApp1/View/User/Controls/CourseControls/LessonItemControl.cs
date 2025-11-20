using System;
using System.Windows.Forms;

namespace WinFormsApp1.View.User.Controls.CourseControls
{
    public partial class LessonItemControl : UserControl
    {
        public event Action<LessonItemControl>? DeleteRequested;

        public LessonItemControl()
        {
            InitializeComponent();
            btnDelete.Click += (s, e) => DeleteRequested?.Invoke(this);
            
            txtTitle.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    this.Parent?.Focus();
                }
            };
        }

        public string LessonTitle
        {
            get => txtTitle.Text;
            set => txtTitle.Text = value ?? "(Không tên)";
        }
    }
}
