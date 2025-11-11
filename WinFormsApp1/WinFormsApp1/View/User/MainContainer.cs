using System;
using System.Drawing;
using System.Windows.Forms;
using WinFormsApp1.Helpers;

namespace WinFormsApp1.View.User
{
    public partial class MainContainer : Form
    {
        private Panel containerPanel;

        public MainContainer()
        {
            InitializeComponent();
            SetupUI();
        }

        private void SetupUI()
        {
            this.Text = "Learning Platform";
            this.Size = new Size(1200, 700);
            this.StartPosition = FormStartPosition.CenterScreen;

            containerPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = ColorPalette.Background
            };

            this.Controls.Add(containerPanel);

            var navBar = NavigationHelper.CreateNavigationBar(containerPanel);
            containerPanel.Controls.Add(navBar);

            NavigationHelper.NavigateTo("home");
        }
    }
}
