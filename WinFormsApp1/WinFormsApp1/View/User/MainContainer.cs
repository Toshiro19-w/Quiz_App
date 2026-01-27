using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using WinFormsApp1.Helpers;
using WinFormsApp1.View.User.Components;

namespace WinFormsApp1.View.User
{
    public partial class MainContainer : Form
    {
        private ProfileDropdown? profileDropdown;
        private CartDropdown? cartDropdown;
        private View.User.Components.CategoriesDropdown? categoriesDropdown;
        private UserProfile? currentUserProfile;
        private Panel? subscriptionWarningBanner;

        public MainContainer()
        {
            InitializeComponent();
            SetupUI();
            /*SetupEventHandlers();*/
            SetupProfileDropdown();
            SetupCartDropdown();
            SetupCategoriesDropdown();
            
            // Kiểm tra subscription sắp hết hạn
            CheckSubscriptionExpiry();
        }

        private void CheckSubscriptionExpiry()
        {
            // Kiểm tra sau khi UI đã load xong
            this.Load += (s, e) =>
            {
                if (AuthHelper.CurrentUser == null) return;

                var daysRemaining = AuthHelper.GetSubscriptionDaysRemaining();
                
                // Nếu subscription còn dưới 3 ngày
                if (daysRemaining.HasValue && daysRemaining.Value <= 3 && daysRemaining.Value > 0)
                {
                    ShowSubscriptionExpiryWarning(daysRemaining.Value);
                    ShowSubscriptionWarningBanner(daysRemaining.Value);
                }
            };
        }

        private void ShowSubscriptionWarningBanner(int daysRemaining)
        {
            // Tạo banner cảnh báo sticky
            subscriptionWarningBanner = new Panel
            {
                Height = 50,
                Dock = DockStyle.Top,
                BackColor = daysRemaining == 1 ? Color.FromArgb(211, 47, 47) : // Đỏ cho 1 ngày
                           daysRemaining == 2 ? Color.FromArgb(245, 124, 0) :   // Cam cho 2 ngày
                                                Color.FromArgb(251, 192, 45),   // Vàng cho 3 ngày
                Padding = new Padding(20, 0, 20, 0)
            };

            // Icon warning
            var iconLabel = new Label
            {
                Text = "⚠️",
                Font = new Font("Segoe UI", 18F),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 12)
            };

            // Message text
            var messageLabel = new Label
            {
                Text = daysRemaining == 1 
                    ? "Gói Premium của bạn sẽ hết hạn sau 1 ngày! Gia hạn ngay để không bị gián đoạn."
                    : $"Gói Premium của bạn sẽ hết hạn sau {daysRemaining} ngày. Đừng quên gia hạn!",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(60, 15)
            };

            // Button gia hạn
            var renewButton = new Button
            {
                Text = "Gia hạn ngay",
                Size = new Size(120, 35),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White,
                ForeColor = subscriptionWarningBanner.BackColor,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Location = new Point(subscriptionWarningBanner.Width - 150, 7)
            };
            renewButton.FlatAppearance.BorderSize = 0;
            renewButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            renewButton.Click += async (s, e) => await HandleYmeduPlusClickAsync();

            // Button đóng
            var closeButton = new Button
            {
                Text = "✕",
                Size = new Size(30, 30),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Location = new Point(subscriptionWarningBanner.Width - 40, 10)
            };
            closeButton.FlatAppearance.BorderSize = 0;
            closeButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            closeButton.Click += (s, e) =>
            {
                subscriptionWarningBanner.Visible = false;
                mainContentPanel.Top = topMenuPanel.Bottom;
                mainContentPanel.Height = this.ClientSize.Height - topMenuPanel.Height;
            };

            subscriptionWarningBanner.Controls.Add(iconLabel);
            subscriptionWarningBanner.Controls.Add(messageLabel);
            subscriptionWarningBanner.Controls.Add(renewButton);
            subscriptionWarningBanner.Controls.Add(closeButton);

            // Thêm banner vào form
            this.Controls.Add(subscriptionWarningBanner);
            subscriptionWarningBanner.BringToFront();

            // Điều chỉnh vị trí của mainContentPanel
            mainContentPanel.Top = topMenuPanel.Bottom + subscriptionWarningBanner.Height;
            mainContentPanel.Height = this.ClientSize.Height - topMenuPanel.Height - subscriptionWarningBanner.Height;
        }

        private void ShowSubscriptionExpiryWarning(int daysRemaining)
        {
            string message = daysRemaining switch
            {
                1 => "⚠️ Gói Premium của bạn sẽ hết hạn sau 1 ngày!\n\n" +
                     "Hãy gia hạn ngay để tiếp tục truy cập không giới hạn tất cả khóa học.",
                2 => "⚠️ Gói Premium của bạn sẽ hết hạn sau 2 ngày!\n\n" +
                     "Đừng quên gia hạn để tiếp tục học tập không bị gián đoạn.",
                _ => $"⚠️ Gói Premium của bạn sẽ hết hạn sau {daysRemaining} ngày!\n\n" +
                     "Gia hạn sớm để được giảm giá đặc biệt!"
            };

            var result = MessageBox.Show(
                message,
                "Nhắc nhở gia hạn Premium",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button1);

            if (result == DialogResult.Yes)
            {
                // Mở form đăng ký subscription
                _ = HandleYmeduPlusClickAsync();
            }
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
                var checkoutForm = new Forms.frmCheckout();
                checkoutForm.ShowDialog();
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
                var checkoutForm = new Forms.frmCheckout();
                checkoutForm.ShowDialog();
            };

            profileDropdown.OnYmeduPlusClick += async (s, e) =>
            {
                profileDropdown.HideDropdown();
                await HandleYmeduPlusClickAsync();
            };

            profileDropdown.OnBangDieuKhienClick += (s, e) =>
            {
                profileDropdown.HideDropdown();
                if (AuthHelper.IsAdmin())
                {
                    this.Hide();
                    var adminDashboard = new Admin.AdminDashboard();
                    adminDashboard.FormClosed += (s2, args) => this.Show();
                    adminDashboard.Show();
                }
                else
                {
                    ToastHelper.Show(this, "Bạn không có quyền truy cập!");
                }
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

        private void SetupCategoriesDropdown()
        {
            categoriesDropdown = new View.User.Components.CategoriesDropdown();
            categoriesDropdown.OnCategorySelected += (s, cat) =>
            {
                // Hide dropdown and navigate to filtered home
                categoriesDropdown.HideDropdown();
                // Open CourseControl and filter by selected category slug
                var courseControl = new Controls.CourseControl();
                NavigateToControl(courseControl);
                // fire-and-forget filter
                _ = courseControl.FilterByCategory(cat.Slug);
                ToastHelper.Show(this, $"Lọc danh mục: {cat.Name}");
            };

            this.Controls.Add(categoriesDropdown);
            categoriesDropdown.BringToFront();

            // Click outside to close
            this.Click += (s, e) => { if (categoriesDropdown.Visible) categoriesDropdown.HideDropdown(); };
            mainContentPanel.Click += (s, e) => { if (categoriesDropdown.Visible) categoriesDropdown.HideDropdown(); };
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

        /*private void SetupEventHandlers()
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
        }*/

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

        // Public method để có thể gọi từ các control khác
        public void NavigateToCourseDetail(int courseId)
        {
            var courseDetailControl = new Controls.CourseControls.CourseDetailControl(courseId);
            NavigateToControl(courseDetailControl);
        }

        // Public method để navigate về home
        public void NavigateToHome()
        {
            NavigateToControl(new Controls.HomeControl());
        }

        // Public method để navigate sang FlashcardControl
        public void NavigateToFlashcards()
        {
            NavigateToControl(new Controls.FlashcardControl());
        }

        // Event Handlers

        private void logoPanel_Click(object sender, EventArgs e)
        {
            NavigateToControl(new Controls.HomeControl());
        }

        private void btnKhamPha_Click(object sender, EventArgs e)
        {
            // Toggle categories dropdown
            if (categoriesDropdown != null)
            {
                if (categoriesDropdown.Visible)
                    categoriesDropdown.HideDropdown();
                else
                    categoriesDropdown.ShowDropdown(btnKhamPha);
            }
            else
            {
                NavigateToControl(new Controls.HomeControl());
            }
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
                var courseControl = new Controls.CourseControl();
                NavigateToControl(courseControl);
                _ = courseControl.SearchCourses(searchQuery);
            }
        }

        private void btnGiangVien_Click(object sender, EventArgs e)
        {
            NavigateToControl(new Controls.MyCoursesControl());
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

        private async System.Threading.Tasks.Task HandleYmeduPlusClickAsync()
        {
            try
            {
                var currentUser = AuthHelper.CurrentUser;
                if (currentUser == null)
                {
                    ToastHelper.Show(this, "Vui lòng đăng nhập để sử dụng tính năng này!");
                    return;
                }

                // Luôn mở SubscriptionForm (cho phép đăng ký mới hoặc gia hạn)
                // Form sẽ tự động detect xem là renewal hay subscription mới
                using var subscriptionForm = new Forms.SubscriptionForm();
                subscriptionForm.StartPosition = FormStartPosition.CenterParent;
                subscriptionForm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void profilePanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
