using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WinFormsApp1.View.User.Controls
{
    public partial class CourseCarouselControl : UserControl
    {
        private int currentPage = 0;
        private int itemsPerPage = 5;
        private int totalItems = 0;

        public CourseCarouselControl()
        {
            InitializeComponent();
            SetupEvents();
        }

        private void SetupEvents()
        {
            btnPrevious.Click += BtnPrevious_Click;
            btnNext.Click += BtnNext_Click;

            this.Resize += (s, e) => UpdateButtonPositions();
            
            UpdateButtonPositions();
            UpdateNavigationButtons();
        }

        private void UpdateButtonPositions()
        {
            int centerY = (flowPanel.Height / 2) - (btnPrevious.Height / 2);
            btnPrevious.Top = centerY;
            btnNext.Top = centerY;
        }

        public void AddCourseCard(CourseCardControl card)
        {
            card.Margin = new Padding(10, 0, 10, 0);
            flowPanel.Controls.Add(card);
            totalItems = flowPanel.Controls.Count;
            
            if (totalItems > 0 && currentPage == 0)
            {
                UpdateDisplay();
            }
            
            UpdateNavigationButtons();
        }

        public void ClearCards()
        {
            flowPanel.Controls.Clear();
            currentPage = 0;
            totalItems = 0;
            UpdateNavigationButtons();
        }

        private void BtnPrevious_Click(object sender, EventArgs e)
        {
            if (currentPage > 0)
            {
                currentPage--;
                UpdateDisplay();
            }
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            int totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);
            if (currentPage < totalPages - 1)
            {
                currentPage++;
                UpdateDisplay();
            }
        }

        private void UpdateDisplay()
        {
            flowPanel.SuspendLayout();

            for (int i = 0; i < flowPanel.Controls.Count; i++)
            {
                var control = flowPanel.Controls[i];
                int pageIndex = i / itemsPerPage;
                control.Visible = (pageIndex == currentPage);
            }

            flowPanel.ResumeLayout();
            UpdateNavigationButtons();
        }

        private void UpdateNavigationButtons()
        {
            UpdateButtonPositions();

            int totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);
            
            bool hasPrevious = currentPage > 0;
            bool hasNext = currentPage < totalPages - 1 && totalItems > itemsPerPage;

            btnPrevious.Enabled = hasPrevious;
            btnNext.Enabled = hasNext;

            btnPrevious.Visible = totalItems > 0;
            btnNext.Visible = totalItems > 0;

            UpdateButtonAppearance(btnPrevious, hasPrevious);
            UpdateButtonAppearance(btnNext, hasNext);

            btnPrevious.BringToFront();
            btnNext.BringToFront();
        }

        private void UpdateButtonAppearance(Button btn, bool enabled)
        {
            if (!enabled)
            {
                btn.ForeColor = Color.FromArgb(220, 220, 220);
                btn.BackColor = Color.FromArgb(250, 250, 250);
            }
            else
            {
                btn.ForeColor = Color.FromArgb(100, 100, 100);
                btn.BackColor = Color.FromArgb(250, 250, 250);
            }
        }
    }
}
