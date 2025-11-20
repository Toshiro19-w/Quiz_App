using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp1.Models.EF;
using WinFormsApp1.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace WinFormsApp1.View.User.Components
{
    public class CategoriesDropdown : Panel
    {
        private Panel menuPanel;
        private System.Windows.Forms.Timer fadeTimer;
        private int targetOpacity = 100;
        private int currentOpacity = 0;

        public event EventHandler<CourseCategory>? OnCategorySelected;

        public CategoriesDropdown()
        {
            InitializeComponent();
            _ = LoadCategoriesAsync();
        }

        private void InitializeComponent()
        {
            this.Width = 260;
            this.Height = 300;
            this.BackColor = Color.White;
            this.Visible = false;
            this.BorderStyle = BorderStyle.FixedSingle;

            menuPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                AutoScroll = true,
                Padding = new Padding(8)
            };

            this.Controls.Add(menuPanel);

            fadeTimer = new System.Windows.Forms.Timer { Interval = 10 };
            fadeTimer.Tick += FadeTimer_Tick;
        }

        private void FadeTimer_Tick(object? sender, EventArgs e)
        {
            if (currentOpacity < targetOpacity)
            {
                currentOpacity += 20;
                if (currentOpacity >= targetOpacity) { currentOpacity = targetOpacity; fadeTimer.Stop(); }
            }
            else if (currentOpacity > targetOpacity)
            {
                currentOpacity -= 20;
                if (currentOpacity <= targetOpacity) { currentOpacity = targetOpacity; fadeTimer.Stop(); if (currentOpacity == 0) this.Visible = false; }
            }
            this.Invalidate();
        }

        public async Task LoadCategoriesAsync()
        {
            try
            {
                using var context = new LearningPlatformContext();
                var cats = await context.CourseCategories.OrderBy(c => c.DisplayOrder).ThenBy(c => c.Name).ToListAsync();

                BuildMenu(cats);
            }
            catch
            {
                // ignore
            }
        }

        private void BuildMenu(List<CourseCategory> cats)
        {
            if (menuPanel == null) return;
            menuPanel.Controls.Clear();

            int y = 0;

            var allBtn = new Button
            {
                Text = "Tất cả khóa học",
                Tag = new CourseCategory { CategoryId = 0, Name = "Tất cả khóa học", Slug = string.Empty },
                Width = menuPanel.Width - 18,
                Height = 40,
                Location = new Point(4, y),
                BackColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft,
                Cursor = Cursors.Hand
            };
            allBtn.FlatAppearance.BorderSize = 0;
            allBtn.Click += (s, e) =>
            {
                var cat = (s as Button)?.Tag as CourseCategory;
                if (cat != null) OnCategorySelected?.Invoke(this, cat);
            };
            menuPanel.Controls.Add(allBtn);
            y += 44;

            foreach (var c in cats)
            {
                var btn = new Button
                {
                    Text = c.Name,
                    Tag = c,
                    Width = menuPanel.Width - 18,
                    Height = 40,
                    Location = new Point(4, y),
                    BackColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 10),
                    TextAlign = ContentAlignment.MiddleLeft,
                    Cursor = Cursors.Hand
                };
                btn.FlatAppearance.BorderSize = 0;
                btn.Click += (s, e) =>
                {
                    var cat = (s as Button)?.Tag as CourseCategory;
                    if (cat != null) OnCategorySelected?.Invoke(this, cat);
                };

                menuPanel.Controls.Add(btn);
                y += 44;
            }

            // adjust height
            this.Height = Math.Min(400, Math.Max(120, y + 8));
        }

        public void ShowDropdown(Control parent)
        {
            if (parent == null || parent.FindForm() == null) return;

            this.Visible = true;
            this.BringToFront();

            Point location = parent.PointToScreen(Point.Empty);
            Point formLocation = parent.FindForm().PointToScreen(Point.Empty);

            // Position to align left edges and show below the button
            int x = location.X - formLocation.X;
            int y = location.Y - formLocation.Y + parent.Height + 5;

            // Ensure dropdown doesn't go off right edge
            var form = parent.FindForm();
            if (form != null)
            {
                int maxX = form.ClientSize.Width - this.Width - 10;
                if (x > maxX) x = maxX;
            }

            this.Location = new Point(x, y);

            currentOpacity = 0;
            targetOpacity = 100;
            fadeTimer.Start();
        }

        public void HideDropdown()
        {
            targetOpacity = 0;
            fadeTimer.Start();
        }
    }
}
