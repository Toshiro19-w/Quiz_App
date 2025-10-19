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
    public partial class formShop : Form
    {
        public formShop()
        {
            InitializeComponent();
        }

        private void formShop_Load(object sender, EventArgs e)
        {
            // Sử dụng FormLayoutHelper để setup đầy đủ layout cho Shop
            FormLayoutHelper.SetupShopCompleteLayout(
                this,           // form
                panel1,         // container panel
                searchTB,       // search textbox
                searchButton,   // search button
                label1,         // title label "Các chủ đề phổ biến"
                tagPanel,       // tag buttons panel
                coursesPanel    // courses panel
            );
        }
    }
}
