using System;
using System.Windows.Forms;
using WinFormsApp1.View.User.Controls;
using WinFormsApp1.Helpers;
using WinFormsApp1.Models.Entities;

namespace WinFormsApp1.TestForm
{
    public partial class LessonDetailTestForm : Form
    {
        private LessonDetailControl lessonControl;

        public LessonDetailTestForm()
        {
            InitializeComponent();
            InitializeLesson();
        }

        private void InitializeLesson()
        {
            this.Text = "Test LessonDetailControl - N?i dung bài h?c";
            this.Size = new System.Drawing.Size(1400, 800);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = System.Drawing.Color.FromArgb(240, 242, 245);

            lessonControl = new LessonDetailControl
            {
                Dock = DockStyle.Fill
            };

            this.Controls.Add(lessonControl);
        }

        private async void LessonDetailTestForm_Load(object sender, EventArgs e)
        {
            // Simulate login for testing
            if (AuthHelper.CurrentUser == null)
            {
                // Create a test user (you should replace this with actual login)
                AuthHelper.SetTestUser(new User
                {
                    UserId = 5,
                    Username = "student1",
                    Email = "student1@ymedu.vn",
                    FullName = "Nguy?n Lan Anh",
                    RoleId = 2
                });
            }

            // Load a lesson
            try
            {
                // Load first lesson of SQL course
                await lessonControl.LoadLessonAsync("sql-co-ban", 1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error loading lesson:\n\n{ex.Message}\n\nStack Trace:\n{ex.StackTrace}", 
                    "Error", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // LessonDetailTestForm
            // 
            this.ClientSize = new System.Drawing.Size(1400, 800);
            this.Name = "LessonDetailTestForm";
            this.Text = "Test LessonDetailControl";
            this.Load += new System.EventHandler(this.LessonDetailTestForm_Load);
            this.ResumeLayout(false);
        }
    }
}
