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
    public partial class formTest : Form
    {
        public formTest()
        {
            InitializeComponent();
        }

        private void formTest_Load(object sender, EventArgs e)
        {
            // Sử dụng FormLayoutHelper để setup đầy đủ layout cho formTest
            FormLayoutHelper.SetupTestLayoutAutoCenter(
                this,           // form
                mainPanel,      // container panel
                label1,         // "Bài tập đã giao"
                label2,         // "Bài tập sắp tới hạn"  
                label17,        // "Bài tập quá hạn"
                AssignedPanel,  // assigned tests panel
                dueDatePanel,   // due date tests panel
                overDuePanel    // overdue tests panel
            );
        }
    }
}
