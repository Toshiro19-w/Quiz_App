using System;
using System.Drawing;
using System.Windows.Forms;
using WinFormsApp1.Models.Entities;

namespace WinFormsApp1.View.User.Components
{
    public class CheckoutCartItem : Panel
    {
        private Course course;
        public event EventHandler<int>? OnRemoveClick;

        public CheckoutCartItem(Course course)
        {
            this.course = course;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Size = new Size(880, 120);
            this.BackColor = Color.White;
            this.BorderStyle = BorderStyle.FixedSingle;
            this.Margin = new Padding(0, 0, 0, 15);

            // Course image placeholder
            var imgPanel = new Panel
            {
                Location = new Point(20, 20),
                Size = new Size(80, 80),
                BackColor = Color.FromArgb(240, 240, 240)
            };
            var imgLabel = new Label
            {
                Text = "📚",
                Font = new Font("Segoe UI", 32),
                Location = new Point(15, 15),
                AutoSize = true
            };
            imgPanel.Controls.Add(imgLabel);
            this.Controls.Add(imgPanel);

            // Course title
            var lblTitle = new Label
            {
                Text = course.Title,
                Location = new Point(120, 25),
                Size = new Size(450, 30),
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(33, 33, 33)
            };
            this.Controls.Add(lblTitle);

            // Instructor
            var lblInstructor = new Label
            {
                Text = $"👤 {course.Owner?.FullName ?? "N/A"}",
                Location = new Point(120, 60),
                AutoSize = true,
                Font = new Font("Segoe UI", 9),
                ForeColor = Color.Gray
            };
            this.Controls.Add(lblInstructor);

            // Added date
            var lblDate = new Label
            {
                Text = $"Thêm vào giỏ: {DateTime.Now:dd/MM/yyyy HH:mm}",
                Location = new Point(120, 85),
                AutoSize = true,
                Font = new Font("Segoe UI", 8),
                ForeColor = Color.LightGray
            };
            this.Controls.Add(lblDate);

            // Price
            var lblPrice = new Label
            {
                Text = $"{course.Price:N0} VND",
                Location = new Point(650, 35),
                AutoSize = true,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 102, 255)
            };
            this.Controls.Add(lblPrice);

            // Remove button
            var btnRemove = new Button
            {
                Text = "🗑 Xóa",
                Location = new Point(800, 15),
                Size = new Size(60, 30),
                BackColor = Color.White,
                ForeColor = Color.FromArgb(220, 53, 69),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9),
                Cursor = Cursors.Hand
            };
            btnRemove.FlatAppearance.BorderColor = Color.FromArgb(220, 53, 69);
            btnRemove.Click += (s, e) => OnRemoveClick?.Invoke(this, course.CourseId);
            this.Controls.Add(btnRemove);
        }
    }
}
