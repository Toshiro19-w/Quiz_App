using System;
using System.Windows.Forms;

namespace WinFormsApp1.View.Admin
{
    public partial class AdminTestForm : Form
    {
        public AdminTestForm()
        {
            InitializeComponent();
        }

        private void btnOpenAdmin_Click(object sender, EventArgs e)
        {
            var adminDashboard = new AdminDashboard();
            adminDashboard.Show();
        }
    }
}