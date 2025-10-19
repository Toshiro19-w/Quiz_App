using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp1.Helpers;

namespace WinFormsApp1.View
{
    public partial class formSearch : Form
    {
        public formSearch()
        {
            InitializeComponent();
        }

        private void formSearch_Load(object sender, EventArgs e)
        {
            // Sử dụng FormLayoutHelper để setup tự động căn chỉnh search controls
            FormLayoutHelper.SetupSearchControlsAutoCenter(this, panel1, searchTB, searchButton);
        }
    }
}
