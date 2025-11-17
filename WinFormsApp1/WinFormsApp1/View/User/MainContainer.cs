using System;
using System.Drawing;
using System.Windows.Forms;
using WinFormsApp1.Helpers;
using WinFormsApp1.View.User.Components;

namespace WinFormsApp1.View.User
{
    public partial class MainContainer : Form
    {
        private ProfileDropdown? profileDropdown;
        private CartDropdown? cartDropdown;
        private UserProfile? currentUserProfile;

        public MainContainer()
        {
            InitializeComponent();
            SetupUI();
            SetupEventHandlers();
            SetupProfileDropdown();
            SetupCartDropdown();
        }

        private void SetupUI()
        {
            this.Text = "Learning Platform - YMEDU";
            this.Size = new Size(1200, 700);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Set màu sắc theo ColorPalette
            mainContentPanel.BackColor = ColorPalette.Background;

            // Set màu cho search panel border
            searchPanel.BorderStyle = BorderStyle.FixedSingle;

            // Load user name nếu có
            if (AuthHelper.CurrentUser != null)
            {
                lblUserName.Text = AuthHelper.CurrentUser.FullName;
                // Lấy chữ cái đầu của tên để hiển thị trong profile button
                btnProfile.Text = GetInitials(AuthHelper.CurrentUser.FullName);
            }

            // Navigate to home by default
            NavigateToControl(new Controls.HomeControl());
        }

        private void SetupCartDropdown()
        {
            cartDropdown = new CartDropdown();

            // Setup checkout event
            cartDropdown.OnCheckoutClick += (s, e) =>
            {
                cartDropdown.HideDropdown();
                NavigateToControl(new Controls.ShopControl());
            };

            // Add to form
            this.Controls.Add(cartDropdown);
            cartDropdown.BringToFront();

            // Click outside to close
            this.Click += (s, e) =>
            {
                if (cartDropdown.Visible)
                    cartDropdown.HideDropdown();
            };

            mainContentPanel.Click += (s, e) =>
            {
                if (cartDropdown.Visible)
                    cartDropdown.HideDropdown();
            };
        }

        private void SetupProfileDropdown()
        {
            profileDropdown = new ProfileDropdown();
            
            // Setup event handlers
            profileDropdown.OnHocTapClick += (s, e) =>
            {
                profileDropdown.HideDropdown();
                NavigateToControl(new Controls.LibraryControl());
            };

            profileDropdown.OnGioHangClick += (s, e) =>
            {
                profileDropdown.HideDropdown();
                NavigateToControl(new Controls.ShopControl());
            };

            profileDropdown.OnBangDieuKhienClick += (s, e) =>
            {
                profileDropdown.HideDropdown();
                ToastHelper.Show(this, "Chức năng Bảng điều khiển đang được phát triển");
            };

            // Event mới: Khi click vào menu profile (Cài đặt, Chỉnh sửa, Lịch sử)
            profileDropdown.OnProfileTabClick += (s, tabIndex) =>
            {
                profileDropdown.HideDropdown();
                
                // Tạo UserProfile mới với tab index hoặc switch tab nếu đã tồn tại
                if (currentUserProfile == null || !mainContentPanel.Controls.Contains(currentUserProfile))
                {
                    currentUserProfile = new UserProfile(tabIndex);
                    NavigateToControl(currentUserProfile);
                }
                else
                {
                    currentUserProfile.SwitchToTab(tabIndex);
                }
            };

            profileDropdown.OnDangXuatClick += (s, e) =>
            {
                profileDropdown.HideDropdown();
                Logout();
            };

            // Add to form
            this.Controls.Add(profileDropdown);
            profileDropdown.BringToFront();

            // Click outside to close
            this.Click += (s, e) =>
            {
                if (profileDropdown.Visible)
                    profileDropdown.HideDropdown();
            };

            mainContentPanel.Click += (s, e) =>
            {
                if (profileDropdown.Visible)
                    profileDropdown.HideDropdown();
            };
        }

        private string GetInitials(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                return "U";

            var parts = fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length >= 2)
                return $"{parts[0][0]}{parts[parts.Length - 1][0]}".ToUpper();
            else if (parts.Length == 1)
                return parts[0][0].ToString().ToUpper();

            return "U";
        }

        private void SetupEventHandlers()
        {
            // Hover effects cho các button
            SetupButtonHoverEffect(btnKhamPha);
            SetupButtonHoverEffect(btnGiangVien);
            SetupButtonHoverEffect(btnHocTap);
            SetupButtonHoverEffect(btnCart);
            SetupButtonHoverEffect(btnCoSoDuLieu);
            SetupButtonHoverEffect(btnLapTrinh);
            SetupButtonHoverEffect(btnPhanTichDuLieu);
            SetupButtonHoverEffect(btnTriTueNhanTao);
        }

        private void SetupButtonHoverEffect(Button btn)
        {
            Color originalColor = btn.BackColor;

            btn.MouseEnter += (s, e) =>
            {
                btn.BackColor = ColorPalette.Background;
            };

            btn.MouseLeave += (s, e) =>
            {
                btn.BackColor = originalColor;
            };
        }

        private void NavigateToControl(UserControl control)
        {
            mainContentPanel.Controls.Clear();
            control.Dock = DockStyle.Fill;
            mainContentPanel.Controls.Add(control);
            
            // Reset currentUserProfile nếu navigate đến control khác
            if (!(control is UserProfile))
            {
                currentUserProfile = null;
            }
        }

        // Event Handlers

        private void logoPanel_Click(object sender, EventArgs e)
        {
            NavigateToControl(new Controls.HomeControl());
        }

        private void btnKhamPha_Click(object sender, EventArgs e)
        {
            NavigateToControl(new Controls.HomeControl());
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            PerformSearch();
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                PerformSearch();
                e.Handled = true;
            }
        }

        private void PerformSearch()
        {
            string searchQuery = txtSearch.Text.Trim();
            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                var searchControl = new Controls.SearchControl();
                NavigateToControl(searchControl);
                // TODO: Pass search query to SearchControl
            }
        }

        private void btnGiangVien_Click(object sender, EventArgs e)
        {
            // TODO: Navigate to instructor page
            ToastHelper.Show(this, "Chức năng Giảng viên đang được phát triển");
        }

        private void btnHocTap_Click(object sender, EventArgs e)
        {
            NavigateToControl(new Controls.LibraryControl());
        }

        private void btnCart_Click(object sender, EventArgs e)
        {
            // Toggle cart dropdown
            if (cartDropdown != null)
            {
                // Close profile dropdown if open
                if (profileDropdown != null && profileDropdown.Visible)
                {
                    profileDropdown.HideDropdown();
                }

                if (cartDropdown.Visible)
                {
                    cartDropdown.HideDropdown();
                }
                else
                {
                    cartDropdown.ShowDropdown(btnCart);
                }
            }
        }

        private void btnProfile_Click(object sender, EventArgs e)
        {
            if (profileDropdown != null)
            {
                // Close cart dropdown if open
                if (cartDropdown != null && cartDropdown.Visible)
                {
                    cartDropdown.HideDropdown();
                }

                if (profileDropdown.Visible)
                {
                    profileDropdown.HideDropdown();
                }
                else
                {
                    profileDropdown.ShowDropdown(btnProfile);
                }
            }
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                string category = btn.Text;
                // TODO: Filter courses by category
                var homeControl = new Controls.HomeControl();
                NavigateToControl(homeControl);
                // TODO: Pass category filter to HomeControl
            }
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

        private void profilePanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
