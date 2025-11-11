using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinFormsApp1.Helpers;
using WinFormsApp1.Models.EF;
using Microsoft.EntityFrameworkCore;

namespace WinFormsApp1.View.User
{
    public partial class formHome : Form
    {
        private Panel navPanel;

        public formHome()
        {
            InitializeComponent();
            CreateNavigationBar();
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            string searchText = searchTB.Text.Trim();
            if (!string.IsNullOrEmpty(searchText))
            {
                SearchCourses(searchText);
            }
        }

        private void CreateNavigationBar()
        {
            navPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = ColorPalette.NavBackground
            };

            var btnHome = CreateNavButton("🏠 Trang chủ", 20, "home");
            var btnLibrary = CreateNavButton("📖 Thư viện", 150, "library");
            var btnShop = CreateNavButton("🛒 Cửa hàng", 280, "shop");
            var btnSearch = CreateNavButton("🔍 Tìm kiếm", 410, "search");
            var btnLogout = CreateNavButton("🚪 Đăng xuất", 1000, "logout");

            var lblUser = new Label
            {
                Text = AuthHelper.CurrentUser?.FullName ?? "User",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = ColorPalette.NavText,
                Location = new Point(850, 20),
                AutoSize = true
            };

            navPanel.Controls.AddRange(new Control[] { btnHome, btnLibrary, btnShop, btnSearch, btnLogout, lblUser });
            this.Controls.Add(navPanel);
            navPanel.BringToFront();
        }

        private Button CreateNavButton(string text, int x, string tag)
        {
            var btn = new Button
            {
                Text = text,
                Location = new Point(x, 15),
                Size = new Size(120, 35),
                BackColor = Color.Transparent,
                ForeColor = ColorPalette.NavText,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Tag = tag
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = ColorPalette.NavHover;
            btn.Click += NavButton_Click;
            return btn;
        }

        private async void NavButton_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            string tag = btn?.Tag?.ToString();

            switch (tag)
            {
                case "home":
                    ReloadHome();
                    break;
                case "library":
                    var libForm = new formLibrary();
                    libForm.Show();
                    this.Close();
                    break;
                case "shop":
                    var shopForm = new formShop();
                    shopForm.Show();
                    this.Close();
                    break;
                case "search":
                    var searchForm = new formSearch();
                    searchForm.Show();
                    this.Close();
                    break;
                case "logout":
                    Logout();
                    break;
            }
        }

        private async void ReloadHome()
        {
            panelMain.Controls.Clear();
            panelMain.Controls.AddRange(new Control[] { label2, label1, flowLayoutPanel2, flowLayoutPanel1, searchButton, searchTB });
            await LoadRecentCourses();
            await LoadPopularCourses();
        }

        private void OpenForm(Form form)
        {
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            panelMain.Controls.Clear();
            panelMain.Controls.Add(form);
            form.Show();
        }

        private void Logout()
        {
            var result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                AuthHelper.Logout();
                var loginForm = new dangnhap();
                loginForm.Show();
                this.Close();
            }
        }

        private async void formHome_Load(object sender, EventArgs e)
        {
            FormLayoutHelper.SetupCompleteAutoLayout(
                this, 
                panelMain, 
                searchTB, 
                searchButton, 
                new FlowLayoutPanel[] { flowLayoutPanel1, flowLayoutPanel2 }
            );
            
            panelMain.Padding = new Padding(0, 70, 0, 0);
            await LoadRecentCourses();
            await LoadPopularCourses();
        }

        private async System.Threading.Tasks.Task LoadRecentCourses()
        {
            using var context = new LearningPlatformContext();
            var userId = AuthHelper.CurrentUser?.UserId;
            
            if (userId.HasValue)
            {
                var recentCourses = await context.CourseProgresses
                    .Where(cp => cp.UserId == userId.Value)
                    .OrderByDescending(cp => cp.LastViewedAt)
                    .Take(8)
                    .Include(cp => cp.Course)
                    .Select(cp => cp.Course)
                    .ToListAsync();

                flowLayoutPanel1.Controls.Clear();
                foreach (var course in recentCourses)
                {
                    var btn = CreateCourseButton(course.Title, course.CourseId);
                    flowLayoutPanel1.Controls.Add(btn);
                }
            }
        }

        private async System.Threading.Tasks.Task LoadPopularCourses()
        {
            using var context = new LearningPlatformContext();
            var popularCourses = await context.Courses
                .OrderByDescending(c => c.TotalReviews)
                .Take(8)
                .ToListAsync();

            flowLayoutPanel2.Controls.Clear();
            foreach (var course in popularCourses)
            {
                var btn = CreateCourseButton(course.Title, course.CourseId);
                flowLayoutPanel2.Controls.Add(btn);
            }
        }

        private Panel CreateCourseButton(string title, int courseId)
        {
            var panel = new Panel
            {
                Size = new Size(273, 145),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Cursor = Cursors.Hand,
                Tag = courseId,
                Margin = new Padding(5)
            };

            var lblTitle = new Label
            {
                Text = title,
                Location = new Point(10, 50),
                Size = new Size(250, 60),
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.Black,
                TextAlign = ContentAlignment.MiddleCenter
            };

            var btnView = new Button
            {
                Text = "Xem khóa học",
                Location = new Point(70, 110),
                Size = new Size(130, 30),
                BackColor = ColorPalette.ButtonSecondary,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Tag = courseId
            };
            btnView.FlatAppearance.BorderSize = 0;
            btnView.FlatAppearance.MouseOverBackColor = ColorPalette.SecondaryDark;
            btnView.Click += CourseButton_Click;

            panel.Controls.AddRange(new Control[] { lblTitle, btnView });
            return panel;
        }

        private void CourseButton_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            int courseId = (int)btn.Tag;
            var lessonForm = new formLesson();
            lessonForm.Show();
        }

        private async void SearchCourses(string searchText)
        {
            using var context = new LearningPlatformContext();
            var courses = await context.Courses
                .Where(c => c.Title.Contains(searchText) || (c.Summary != null && c.Summary.Contains(searchText)))
                .Take(8)
                .ToListAsync();

            flowLayoutPanel1.Controls.Clear();
            label1.Text = $"Kết quả tìm kiếm: {searchText}";
            
            foreach (var course in courses)
            {
                var btn = CreateCourseButton(course.Title, course.CourseId);
                flowLayoutPanel1.Controls.Add(btn);
            }
        }
    }
}
