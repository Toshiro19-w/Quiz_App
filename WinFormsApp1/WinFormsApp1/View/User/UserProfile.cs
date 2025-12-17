using System;
using System.Drawing;
using System.Windows.Forms;
using WinFormsApp1.Helpers;
using WinFormsApp1.Localization;
using WinFormsApp1.View.User.Controls.ProfileTabs;

namespace WinFormsApp1.View.User
{
    public partial class UserProfile : UserControl
    {
        private UserControl? currentTabControl;
        private int currentTabIndex = 0;
        private const int TAB_COUNT = 3;

        public UserProfile()
        {
            InitializeComponent();
            ApplyLocalization();
            LoadTab(new AccountSettingsTab());
            SetActiveTab(btnCaiDat, 0);
            
            // Subscribe to language change event
            LanguageHelper.LanguageChanged += OnLanguageChanged;
        }

        // Constructor với tab index
        public UserProfile(int tabIndex)
        {
            InitializeComponent();
            ApplyLocalization();
            SwitchToTab(tabIndex);
            
            // Subscribe to language change event
            LanguageHelper.LanguageChanged += OnLanguageChanged;
        }

        private void OnLanguageChanged(object? sender, EventArgs e)
        {
            ApplyLocalization();
        }

        private void ApplyLocalization()
        {
            btnCaiDat.Text = LanguageHelper.GetString("Settings");
            btnChinhSua.Text = LanguageHelper.GetString("EditProfile");
            btnLichSu.Text = LanguageHelper.GetString("PurchaseHistory");
        }

        private void UserProfile_Resize(object sender, EventArgs e)
        {
            UpdateTabLayout();
        }

        private void UpdateTabLayout()
        {
            if (tabPanel == null || tabPanel.Width <= 0) return;

            // Tính toán chiều rộng mỗi tab button
            int availableWidth = tabPanel.Width;
            int tabWidth = Math.Max(150, availableWidth / TAB_COUNT); // Minimum 150px

            // Giới hạn tối đa để không quá rộng
            tabWidth = Math.Min(tabWidth, 250);

            // Update kích thước và vị trí các tab buttons
            btnCaiDat.Size = new Size(tabWidth, 73);
            btnCaiDat.Location = new Point(0, 0);

            btnChinhSua.Size = new Size(tabWidth, 73);
            btnChinhSua.Location = new Point(tabWidth, 0);

            btnLichSu.Size = new Size(tabWidth, 73);
            btnLichSu.Location = new Point(tabWidth * 2, 0);

            // Update underline
            UpdateUnderlinePosition();
        }

        private void UpdateUnderlinePosition()
        {
            if (tabUnderline == null) return;

            int tabWidth = btnCaiDat.Width;
            tabUnderline.Size = new Size(tabWidth, 4);
            tabUnderline.Location = new Point(tabWidth * currentTabIndex, 73);
        }

        // Method public để switch tab từ bên ngoài
        public void SwitchToTab(int tabIndex)
        {
            currentTabIndex = tabIndex;

            switch (tabIndex)
            {
                case 0:
                    SetActiveTab(btnCaiDat, 0);
                    LoadTab(new AccountSettingsTab());
                    break;
                case 1:
                    SetActiveTab(btnChinhSua, 1);
                    LoadTab(new EditProfileTab());
                    break;
                case 2:
                    SetActiveTab(btnLichSu, 2);
                    LoadTab(new PurchaseHistoryTab());
                    break;
                default:
                    SetActiveTab(btnCaiDat, 0);
                    LoadTab(new AccountSettingsTab());
                    break;
            }
        }

        private void btnCaiDat_Click(object sender, EventArgs e)
        {
            SwitchToTab(0);
        }

        private void btnChinhSua_Click(object sender, EventArgs e)
        {
            SwitchToTab(1);
        }

        private void btnLichSu_Click(object sender, EventArgs e)
        {
            SwitchToTab(2);
        }

        private void SetActiveTab(Button activeButton, int tabIndex)
        {
            currentTabIndex = tabIndex;

            // Reset all buttons to inactive state
            btnCaiDat.Font = new Font("Segoe UI", 12F);
            btnCaiDat.ForeColor = Color.Gray;
            btnChinhSua.Font = new Font("Segoe UI", 12F);
            btnChinhSua.ForeColor = Color.Gray;
            btnLichSu.Font = new Font("Segoe UI", 12F);
            btnLichSu.ForeColor = Color.Gray;

            // Set active button
            activeButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            activeButton.ForeColor = ColorPalette.TextPrimary;

            // Update underline position
            UpdateUnderlinePosition();
        }

        private void LoadTab(UserControl tabControl)
        {
            // Remove current tab if exists
            if (currentTabControl != null)
            {
                contentPanel.Controls.Remove(currentTabControl);
                currentTabControl.Dispose();
            }

            // Add new tab
            currentTabControl = tabControl;
            currentTabControl.Dock = DockStyle.Fill;
            contentPanel.Controls.Add(currentTabControl);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            UpdateTabLayout();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            UpdateTabLayout();
        }

        private void UnsubscribeLanguageEvent()
        {
            LanguageHelper.LanguageChanged -= OnLanguageChanged;
        }

        // Call this method when the control is being disposed
        public void Cleanup()
        {
            UnsubscribeLanguageEvent();
        }
    }
}
