using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp1.View.User.Controls;

namespace WinFormsApp1.TestForm
{
    public partial class FormTest : Form
    {
        private CourseDetailControl courseDetailControl;

        public FormTest()
        {
            InitializeComponent();
            LoadCourseDetail();
        }
        private void LoadCourseDetail()
        {
            // Tạo instance của CourseDetailControl
            courseDetailControl = new CourseDetailControl();
            courseDetailControl.LoadCourse(1); // Load course với ID 1
                                               // Đặt Dock để fill toàn bộ Form
            courseDetailControl.Dock = DockStyle.Fill;

            // Thêm vào Form
            this.Controls.Add(courseDetailControl);

            // (Tùy chọn) Load course cụ thể
            //courseDetailControl.LoadCourse("sql-co-ban");
        }

        private void FormTest_Load(object sender, EventArgs e)
        {

        }
    }
}
