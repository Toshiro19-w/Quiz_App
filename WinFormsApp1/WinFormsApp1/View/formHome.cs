using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp1.View;
using WinFormsApp1.Helpers;

namespace WinFormsApp1.View
{
    public partial class formHome : Form
    {
        formSearch formSearch;
        formMenu formMenu;
        public formHome()
        {
            InitializeComponent();
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();
            if (formSearch == null)
            {
                formSearch = new formSearch();
                formSearch.TopLevel = false;
                formSearch.FormBorderStyle = FormBorderStyle.None;
                formSearch.Dock = DockStyle.Fill;
                this.Controls.Add(formSearch);
                formSearch.Show();
            }
            else
            {
                formSearch.TopLevel = false;
                formSearch.FormBorderStyle = FormBorderStyle.None;
                formSearch.Dock = DockStyle.Fill;
                this.Controls.Add(formSearch);
                formSearch.Show();
            }
        }

        private void formHome_Load(object sender, EventArgs e)
        {
            // Sử dụng FormLayoutHelper để setup tự động căn chỉnh
            FormLayoutHelper.SetupCompleteAutoLayout(
                this, 
                panelMain, 
                searchTB, 
                searchButton, 
                new FlowLayoutPanel[] { flowLayoutPanel1, flowLayoutPanel2 }
            );
        }
    }
}
