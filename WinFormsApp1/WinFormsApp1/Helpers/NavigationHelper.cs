using System;
using System.Drawing;
using System.Windows.Forms;
using WinFormsApp1.View;
using static WinFormsApp1.Helpers.ColorPalette;

namespace WinFormsApp1.Helpers
{
    public static class NavigationHelper
    {
        private static Panel mainContainer;
        private static Panel navPanel;

        public static Panel CreateNavigationBar(Panel container)
        {
            mainContainer = container;
            
            navPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = NavBackground
            };

            var btnHome = CreateNavButton("🏠 Trang chủ", 20);
            btnHome.Click += (s, e) => NavigateTo("home");

            var btnLibrary = CreateNavButton("📖 Thư viện", 150);
            btnLibrary.Click += (s, e) => NavigateTo("library");

            var btnShop = CreateNavButton("🛒 Cửa hàng", 280);
            btnShop.Click += (s, e) => NavigateTo("shop");

            var btnCourse = CreateNavButton("📚 Khóa học", 410);
            btnCourse.Click += (s, e) => NavigateTo("course");

            var btnSearch = CreateNavButton("🔍 Tìm kiếm", 540);
            btnSearch.Click += (s, e) => NavigateTo("search");

            var btnTest = CreateNavButton("📝 Bài test", 670);
            btnTest.Click += (s, e) => NavigateTo("test");

            var btnLogout = CreateNavButton("🚪 Đăng xuất", 1000);
            btnLogout.Click += (s, e) => Logout();

            var lblUser = new Label
            {
                Text = AuthHelper.CurrentUser?.FullName ?? "User",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = NavText,
                Location = new Point(850, 20),
                AutoSize = true
            };

            navPanel.Controls.AddRange(new Control[] { btnHome, btnLibrary, btnShop, btnCourse, btnSearch, btnTest, btnLogout, lblUser });
            return navPanel;
        }

        private static Button CreateNavButton(string text, int x)
        {
            var btn = new Button
            {
                Text = text,
                Location = new Point(x, 15),
                Size = new Size(120, 35),
                BackColor = Color.Transparent,
                ForeColor = NavText,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = NavHover;
            return btn;
        }

        public static void NavigateTo(string page)
        {
            if (mainContainer == null) return;

            mainContainer.Controls.Clear();
            mainContainer.Controls.Add(navPanel);

            UserControl control = page switch
            {
                "home" => new View.User.Controls.HomeControl(),
                "library" => new View.User.Controls.LibraryControl(),
                "shop" => new View.User.Controls.ShopControl(),
                "course" => new View.User.Controls.CourseControl(),
                "search" => new View.User.Controls.SearchControl(),
                "test" => new View.User.Controls.TestControl(),
                "lesson" => new View.User.Controls.LessonControl(),
                _ => new View.User.Controls.HomeControl()
            };

            control.Dock = DockStyle.Fill;
            mainContainer.Controls.Add(control);
            navPanel.BringToFront();
        }

        private static void Logout()
        {
            var result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                AuthHelper.Logout();
                var loginForm = new dangnhap();
                loginForm.Show();
                Application.OpenForms[0]?.Close();
            }
            else
            {
                // show a small toast to acknowledge cancellation
                var mainForm = Application.OpenForms.Count > 0 ? Application.OpenForms[0] : null;
                if (mainForm != null) ToastHelper.Show(mainForm, "Hủy đăng xuất");
            }
        }
    }
}
