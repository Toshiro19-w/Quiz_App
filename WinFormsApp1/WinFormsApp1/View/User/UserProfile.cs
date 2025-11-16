using System;
using System.Drawing;
using System.Windows.Forms;
using WinFormsApp1.Helpers;
using WinFormsApp1.View.User.Controls.ProfileTabs;

namespace WinFormsApp1.View.User
{
    public partial class UserProfile : UserControl
    {
        private UserControl? currentTabControl;

        public UserProfile()
        {
            InitializeComponent();
            CenterContainer();
            LoadTab(new AccountSettingsTab());
        }

        // Constructor với tab index
        public UserProfile(int tabIndex)
        {
            InitializeComponent();
            CenterContainer();
            SwitchToTab(tabIndex);
        }

        private void CenterContainer()
        {
            // Center containerPanel horizontally
            int x = (this.Width - containerPanel.Width) / 2;
            containerPanel.Location = new Point(x, 75);
        }

        private void UserProfile_Resize(object sender, EventArgs e)
        {
            CenterContainer();
        }

        // Method public để switch tab từ bên ngoài
        public void SwitchToTab(int tabIndex)
        {
            switch (tabIndex)
            {
                case 0:
                    SetActiveTab(btnCaiDat, 0);
                    LoadTab(new AccountSettingsTab());
                    break;
                case 1:
                    SetActiveTab(btnChinhSua, 270);
                    LoadTab(new EditProfileTab());
                    break;
                case 2:
                    SetActiveTab(btnLichSu, 540);
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

        private void SetActiveTab(Button activeButton, int underlineX)
        {
            // Reset all buttons to inactive state
            btnCaiDat.Font = new Font("Segoe UI", 15F);
            btnCaiDat.ForeColor = Color.Gray;
            btnChinhSua.Font = new Font("Segoe UI", 15F);
            btnChinhSua.ForeColor = Color.Gray;
            btnLichSu.Font = new Font("Segoe UI", 15F);
            btnLichSu.ForeColor = Color.Gray;

            // Set active button
            activeButton.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            activeButton.ForeColor = ColorPalette.TextPrimary;

            // Move underline
            tabUnderline.Location = new Point(underlineX, 83);
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
