using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp1.View.User
{
    public partial class formMenu : Form
    {
        formHome formHome;
        formLibrary formLibrary;
        formShop formShop;
        formLesson formLesson;
        formTest formTest;
        formTestComplete formTestComplete;
        formTestAssign formTestAssign;
        formInProgressLesson formInProgressLesson;
        formCreateLesson formCreateLesson;
        formTestOverDue formTestOverDue;
        
        public formMenu()
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
            panelShow.Controls.Clear();
            if (formHome == null)
            {
                formHome = new formHome();
                formHome.TopLevel = false;
                formHome.FormBorderStyle = FormBorderStyle.None;
                formHome.Dock = DockStyle.Fill;
                panelShow.Controls.Add(formHome);
                formHome.Show();
            }
            else
            {
                formHome.TopLevel = false;
                formHome.FormBorderStyle = FormBorderStyle.None;
                formHome.Dock = DockStyle.Fill;
                panelShow.Controls.Add(formHome);
                formHome.Show();
            }
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
            ToggleSidebar();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
        bool menuExpand = false;
        bool menuExpand2 = false;
        bool SidebarExpand = false;
        
        private void ToggleMenuContainer()
        {
            if (!menuExpand)
            {
                MenuContainer.Height = 250;
                MenuContainer2.Top = MenuContainer.Bottom + 10;
                menuExpand = true;
            }
            else
            {
                MenuContainer.Height = 65;
                MenuContainer2.Top = MenuContainer.Bottom + 10;
                menuExpand = false;
            }
        }
        
        private void MenuTransition_Tick(object sender, EventArgs e)
        {
            // This method is kept for compatibility but no longer used for animation
        }

        private void LessonButton_Click(object sender, EventArgs e)
        {
            ToggleMenuContainer();
            panelShow.Controls.Clear();
            if (formLesson == null)
            {
                formLesson = new formLesson();
                formLesson.TopLevel = false;
                formLesson.FormBorderStyle = FormBorderStyle.None;
                formLesson.Dock = DockStyle.Fill;
                panelShow.Controls.Add(formLesson);
                formLesson.Show();
            }
            else
            {
                formLesson.TopLevel = false;
                formLesson.FormBorderStyle = FormBorderStyle.None;
                formLesson.Dock = DockStyle.Fill;
                panelShow.Controls.Add(formLesson);
                formLesson.Show();
            }
        }

        private void TestButton_Click(object sender, EventArgs e)
        {
            ToggleMenuContainer2();
            panelShow.Controls.Clear();
            if (formTest == null)
            {
                formTest = new formTest();
                formTest.TopLevel = false;
                formTest.FormBorderStyle = FormBorderStyle.None;
                formTest.Dock = DockStyle.Fill;
                panelShow.Controls.Add(formTest);
                formTest.Show();
            }
            else
            {
                formTest.TopLevel = false;
                formTest.FormBorderStyle = FormBorderStyle.None;
                formTest.Dock = DockStyle.Fill;
                panelShow.Controls.Add(formTest);
                formTest.Show();
            }
        }

        private void TestCompletedButton_Click(object sender, EventArgs e)
        {
            panelShow.Controls.Clear();
            if (formTestComplete == null)
            {
                formTestComplete = new formTestComplete();
                formTestComplete.TopLevel = false;
                formTestComplete.FormBorderStyle = FormBorderStyle.None;
                formTestComplete.Dock = DockStyle.Fill;
                panelShow.Controls.Add(formTestComplete);
                formTestComplete.Show();
            }
            else
            {
                formTestComplete.TopLevel = false;
                formTestComplete.FormBorderStyle = FormBorderStyle.None;
                formTestComplete.Dock = DockStyle.Fill;
                panelShow.Controls.Add(formTestComplete);
                formTestComplete.Show();
            }
        }

        private void MenuContainer2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ToggleMenuContainer2()
        {
            if (!menuExpand2)
            {
                MenuContainer2.Height = 336;
                menuExpand2 = true;
            }
            else
            {
                MenuContainer2.Height = 65;
                menuExpand2 = false;
            }
        }
        
        private void MenuTransition2_Tick(object sender, EventArgs e)
        {
            // This method is kept for compatibility but no longer used for animation
        }

        private void ToggleSidebar()
        {
            if (!SidebarExpand)
            {
                sideBarPanel.Width = 84;
                panelShow.Left = 84;
                panelShow.Width = this.ClientSize.Width - 84;
                SidebarExpand = true;
            }
            else
            {
                sideBarPanel.Width = 266;
                panelShow.Left = 266;
                panelShow.Width = this.ClientSize.Width - 266;
                SidebarExpand = false;
            }
        }
        
        private void SidebarTransition_Tick(object sender, EventArgs e) 
        { 
            // This method is kept for compatibility but no longer used for animation
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            ToggleSidebar();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void LibraryButton_Click(object sender, EventArgs e)
        {
            panelShow.Controls.Clear();
            if (formLibrary == null)
            {
                formLibrary = new formLibrary();
                formLibrary.TopLevel = false;
                formLibrary.FormBorderStyle = FormBorderStyle.None;
                formLibrary.Dock = DockStyle.Fill;
                panelShow.Controls.Add(formLibrary);
                formLibrary.Show();
            }
            else
            {
                formLibrary.TopLevel = false;
                formLibrary.FormBorderStyle = FormBorderStyle.None;
                formLibrary.Dock = DockStyle.Fill;
                panelShow.Controls.Add(formLibrary);
                formLibrary.Show();
            }
        }

        private void LessonInProgressButton_Click(object sender, EventArgs e)
        {
            panelShow.Controls.Clear();
            if (formInProgressLesson == null)
            {
                formInProgressLesson = new formInProgressLesson();
                formInProgressLesson.TopLevel = false;
                formInProgressLesson.FormBorderStyle = FormBorderStyle.None;
                formInProgressLesson.Dock = DockStyle.Fill;
                panelShow.Controls.Add(formInProgressLesson);
                formInProgressLesson.Show();
            }
            else
            {
                formInProgressLesson.TopLevel = false;
                formInProgressLesson.FormBorderStyle = FormBorderStyle.None;
                formInProgressLesson.Dock = DockStyle.Fill;
                panelShow.Controls.Add(formInProgressLesson);
                formInProgressLesson.Show();
            }
        }

        private void panel10_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ShopButton_Click(object sender, EventArgs e)
        {
            panelShow.Controls.Clear();
            // Option 1: Load Course Control directly
            var courseControl = new Controls.CourseControl();
            courseControl.Dock = DockStyle.Fill;
            panelShow.Controls.Add(courseControl);
            
            // Option 2: Load formShop (commented out)
            /*
            if (formShop == null)
            {
                formShop = new formShop();
                formShop.TopLevel = false;
                formShop.FormBorderStyle = FormBorderStyle.None;
                formShop.Dock = DockStyle.Fill;
                panelShow.Controls.Add(formShop);
                formShop.Show();
            }
            else
            {
                formShop.TopLevel = false;
                formShop.FormBorderStyle = FormBorderStyle.None;
                formShop.Dock = DockStyle.Fill;
                panelShow.Controls.Add(formShop);
                formShop.Show();
            }
            */
        }

        private void LessonCreationButton_Click(object sender, EventArgs e)
        {
            panelShow.Controls.Clear();
            if (formCreateLesson == null)
            {
                formCreateLesson = new formCreateLesson();
                formCreateLesson.TopLevel = false;
                formCreateLesson.FormBorderStyle = FormBorderStyle.None;
                formCreateLesson.Dock = DockStyle.Fill;
                panelShow.Controls.Add(formCreateLesson);
                formCreateLesson.Show();
            }
            else
            {
                formCreateLesson.TopLevel = false;
                formCreateLesson.FormBorderStyle = FormBorderStyle.None;
                formCreateLesson.Dock = DockStyle.Fill;
                panelShow.Controls.Add(formCreateLesson);
                formCreateLesson.Show();
            }
        }

        private void TestAssigned_Click(object sender, EventArgs e)
        {
            panelShow.Controls.Clear();
            if (formTestAssign == null)
            {
                formTestAssign = new formTestAssign();
                formTestAssign.TopLevel = false;
                formTestAssign.FormBorderStyle = FormBorderStyle.None;
                formTestAssign.Dock = DockStyle.Fill;
                panelShow.Controls.Add(formTestAssign);
                formTestAssign.Show();
            }
            else
            {
                formTestAssign.TopLevel = false;
                formTestAssign.FormBorderStyle = FormBorderStyle.None;
                formTestAssign.Dock = DockStyle.Fill;
                panelShow.Controls.Add(formTestAssign);
                formTestAssign.Show();
            }
        }

        private void TestOverdueButton_Click(object sender, EventArgs e)
        {
            panelShow.Controls.Clear();
            if (formTestOverDue == null)
            {
                formTestOverDue = new formTestOverDue();
                formTestOverDue.TopLevel = false;
                formTestOverDue.FormBorderStyle = FormBorderStyle.None;
                formTestOverDue.Dock = DockStyle.Fill;
                panelShow.Controls.Add(formTestOverDue);
                formTestOverDue.Show();
            }
            else
            {
                formTestOverDue.TopLevel = false;
                formTestOverDue.FormBorderStyle = FormBorderStyle.None;
                formTestOverDue.Dock = DockStyle.Fill;
                panelShow.Controls.Add(formTestOverDue);
                formTestOverDue.Show();
            }
        }
    }
}
