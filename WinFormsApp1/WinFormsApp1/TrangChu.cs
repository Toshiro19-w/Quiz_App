using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class TrangChu : Form
    {
        public TrangChu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void TrangChu_Load(object sender, EventArgs e)
        {

        }

        private void TrangChu_Load_1(object sender, EventArgs e)
        {

        }

        private void Library_Click(object sender, EventArgs e)
        {

        }

        private void Home_Click(object sender, EventArgs e)
        {

        }

        private void Shop_Click(object sender, EventArgs e)
        {

        }

        private void Lesson_Click(object sender, EventArgs e)
        {

        }

        private void Text_Click(object sender, EventArgs e)
        {

        }

        private void LOGO_Click(object sender, EventArgs e)
        {
            SidebarTransition.Start();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
        bool menuExpand = false;
        bool menuExpand2 = false;
        bool SidebarExpand = false;
        private void MenuTransition_Tick(object sender, EventArgs e)
        {
            if (!menuExpand)
            {
                MenuContainer.Height += 10;
                if (MenuContainer.Height >= 336)
                {
                    menuExpand = true;
                    MenuTransition.Stop();
                }
            }
            else
            {
                MenuContainer.Height -= 10;
                if (MenuContainer.Height <= 84)
                {
                    menuExpand = false;
                    MenuTransition.Stop();
                }
            }
        }

        private void LessonButton_Click(object sender, EventArgs e)
        {
            MenuTransition.Start();
        }

        private void TestButton_Click(object sender, EventArgs e)
        {
            MenuTransition2.Start();
        }

        private void TestCompletedButton_Click(object sender, EventArgs e)
        {

        }

        private void MenuContainer2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void MenuTransition2_Tick(object sender, EventArgs e)
        {
            if (!menuExpand2)
            {
                MenuContainer2.Height += 10;
                if (MenuContainer2.Height >= 336)
                {
                    menuExpand2 = true;
                    MenuTransition2.Stop();
                }
            }
            else
            {
                MenuContainer2.Height -= 10;
                if (MenuContainer2.Height <= 84)
                {
                    menuExpand2 = false;
                    MenuTransition2.Stop();
                }
            }
        }

        private void SidebarTransition_Tick(object sender, EventArgs e)
        {
            if (!SidebarExpand)
            {
                SideBar.Height += 10;
                if (SideBar.Height >= 332)
                {
                    SidebarExpand = true;
                    SidebarTransition.Stop();
                }
            }
            else
            {
                SideBar.Height -= 10;
                if (SideBar.Height <= 80)
                {
                    SidebarExpand = false;
                    SidebarTransition.Stop();
                }
            }
        }
    }
}
