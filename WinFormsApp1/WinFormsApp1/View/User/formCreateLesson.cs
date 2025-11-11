using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using WinFormsApp1.Helpers;
using WinFormsApp1.Models.EF;
using static WinFormsApp1.Helpers.ColorPalette;

namespace WinFormsApp1.View.User
{
    public partial class formCreateLesson : Form
    {
        private TextBox txtTitle;
        private TextBox txtDescription;
        private ComboBox cmbChapter;

        public formCreateLesson()
        {
            InitializeComponent();
            SetupUI();
        }

        private void SetupUI()
        {
            this.Size = new Size(1200, 700);
            this.StartPosition = FormStartPosition.CenterScreen;

            var lblTitle = new Label
            {
                Text = "Tạo bài học mới",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                Location = new Point(50, 80),
                AutoSize = true
            };

            var lblLessonTitle = new Label
            {
                Text = "Tiêu đề bài học:",
                Location = new Point(50, 140),
                AutoSize = true,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };

            txtTitle = new TextBox
            {
                Location = new Point(50, 170),
                Size = new Size(500, 30),
                Font = new Font("Segoe UI", 11)
            };

            var lblDesc = new Label
            {
                Text = "Mô tả:",
                Location = new Point(50, 220),
                AutoSize = true,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };

            txtDescription = new TextBox
            {
                Location = new Point(50, 250),
                Size = new Size(500, 100),
                Font = new Font("Segoe UI", 11),
                Multiline = true
            };

            var lblChapter = new Label
            {
                Text = "Chương:",
                Location = new Point(50, 370),
                AutoSize = true,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };

            cmbChapter = new ComboBox
            {
                Location = new Point(50, 400),
                Size = new Size(500, 30),
                Font = new Font("Segoe UI", 11),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            var btnCreate = new Button
            {
                Text = "Tạo bài học",
                Location = new Point(50, 460),
                Size = new Size(200, 40),
                BackColor = ButtonSecondary,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnCreate.FlatAppearance.BorderSize = 0;
            btnCreate.Click += BtnCreate_Click;

            var btnCancel = new Button
            {
                Text = "Hủy",
                Location = new Point(270, 460),
                Size = new Size(150, 40),
                BackColor = Color.Gray,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Click += (s, e) => this.Close();

            this.Controls.AddRange(new Control[] { lblTitle, lblLessonTitle, txtTitle, lblDesc, txtDescription, lblChapter, cmbChapter, btnCreate, btnCancel });
            LoadChapters();
        }

        private async void LoadChapters()
        {
            using var context = new LearningPlatformContext();
            var chapters = await context.CourseChapters.ToListAsync();
            cmbChapter.DataSource = chapters;
            cmbChapter.DisplayMember = "Title";
            cmbChapter.ValueMember = "ChapterId";
        }

        private async void BtnCreate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                MessageBox.Show("Vui lòng nhập tiêu đề!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var context = new LearningPlatformContext();
            var lesson = new Models.Entities.Lesson
            {
                ChapterId = (int)cmbChapter.SelectedValue,
                Title = txtTitle.Text,
                Description = txtDescription.Text,
                OrderIndex = 0,
                Visibility = "Course",
                CreatedAt = DateTime.Now
            };

            context.Lessons.Add(lesson);
            await context.SaveChangesAsync();
            MessageBox.Show("Tạo bài học thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}
