using System;
using System.Drawing;
using System.Windows.Forms;
using WinFormsApp1.Helpers;
using WinFormsApp1.Localization;
using WinFormsApp1.View.Admin.Controls.ProfileTabs;

namespace WinFormsApp1.View.Admin
{
    public partial class AdminProfile : UserControl
    {
        private UserControl? currentTabControl;

        public AdminProfile()
        {
            InitializeComponent();
            ApplyLocalization();
            LoadTab(new AdminAccountSettingsTab());
        }

        // Constructor với tab index
        public AdminProfile(int tabIndex)
        {
            InitializeComponent();
            ApplyLocalization();
            SwitchToTab(tabIndex);
        }

        private void ApplyLocalization()
        {
            titleLabel.Text = LanguageHelper.GetString("AdminAccount");
            btnCaiDat.Text = LanguageHelper.GetString("AccountSettingsTab");
            btnChinhSua.Text = LanguageHelper.GetString("EditProfileTab");
        }

        private void AdminProfile_Resize(object sender, EventArgs e)
        {
            // Anchor handles resizing automatically now
        }

        // Method public để switch tab từ bên ngoài
        public void SwitchToTab(int tabIndex)
        {
            switch (tabIndex)
            {
                case 0:
                    SetActiveTab(btnCaiDat, 0);
                    LoadTab(new AdminAccountSettingsTab());
                    break;
                case 1:
                    SetActiveTab(btnChinhSua, 200);
                    LoadTab(new AdminEditProfileTab());
                    break;
                default:
                    SetActiveTab(btnCaiDat, 0);
                    LoadTab(new AdminAccountSettingsTab());
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

        private void SetActiveTab(Button activeButton, int underlineX)
        {
            // Reset all buttons to inactive state
            btnCaiDat.Font = new Font("Segoe UI", 12F);
            btnCaiDat.ForeColor = Color.Gray;
            btnChinhSua.Font = new Font("Segoe UI", 12F);
            btnChinhSua.ForeColor = Color.Gray;

            // Set active button
            activeButton.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            activeButton.ForeColor = ColorPalette.TextPrimary;

            // Move underline
            tabUnderline.Location = new Point(underlineX, 73);
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
    }
}
