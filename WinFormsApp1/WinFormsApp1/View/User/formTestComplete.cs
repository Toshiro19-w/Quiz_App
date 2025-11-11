using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinFormsApp1.Helpers;
using WinFormsApp1.Models.EF;
using Microsoft.EntityFrameworkCore;
using static WinFormsApp1.Helpers.ColorPalette;

namespace WinFormsApp1.View.User
{
    public partial class formTestComplete : Form
    {
        private FlowLayoutPanel flowPanel;

        public formTestComplete()
        {
            InitializeComponent();
            SetupUI();
        }

        private void SetupUI()
        {
            this.Size = new Size(1200, 700);
            this.StartPosition = FormStartPosition.CenterScreen;

            var lblTitle = new Label
            {
                Text = "Bài kiểm tra đã hoàn thành",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                Location = new Point(20, 80),
                AutoSize = true
            };

            flowPanel = new FlowLayoutPanel
            {
                Location = new Point(20, 130),
                Size = new Size(1100, 450),
                AutoScroll = true
            };

            this.Controls.AddRange(new Control[] { lblTitle, flowPanel });
            LoadTests();
        }

        private async void LoadTests()
        {
            using var context = new LearningPlatformContext();
            var userId = AuthHelper.CurrentUser?.UserId;
            if (!userId.HasValue) return;

            var tests = await context.TestAttempts
                .Where(ta => ta.UserId == userId.Value && ta.Status == "Completed")
                .Include(ta => ta.Test)
                .OrderByDescending(ta => ta.SubmittedAt)
                .ToListAsync();

            flowPanel.Controls.Clear();
            foreach (var test in tests)
            {
                var card = CreateTestCard(test);
                flowPanel.Controls.Add(card);
            }
        }

        private Panel CreateTestCard(Models.Entities.TestAttempt attempt)
        {
            var card = new Panel
            {
                Size = new Size(350, 170),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(10)
            };

            var lblTitle = new Label
            {
                Text = attempt.Test.Title,
                Location = new Point(10, 10),
                Size = new Size(330, 40),
                Font = new Font("Segoe UI", 11, FontStyle.Bold)
            };

            var lblScore = new Label
            {
                Text = $"Điểm: {attempt.Score}/{attempt.MaxScore}",
                Location = new Point(10, 55),
                AutoSize = true,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = ButtonPrimary
            };

            var lblDate = new Label
            {
                Text = $"Hoàn thành: {attempt.SubmittedAt?.ToString("dd/MM/yyyy HH:mm")}",
                Location = new Point(10, 85),
                AutoSize = true,
                ForeColor = TextSecondary
            };

            var lblTime = new Label
            {
                Text = $"Thời gian làm: {attempt.TimeSpentSec / 60} phút",
                Location = new Point(10, 110),
                AutoSize = true,
                ForeColor = TextSecondary
            };

            var btnView = new Button
            {
                Text = "Xem chi tiết",
                Location = new Point(10, 135),
                Size = new Size(330, 30),
                BackColor = ButtonSecondary,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnView.FlatAppearance.BorderSize = 0;
            btnView.Click += (s, e) => MessageBox.Show($"Xem kết quả bài kiểm tra: {attempt.Test.Title}");

            card.Controls.AddRange(new Control[] { lblTitle, lblScore, lblDate, lblTime, btnView });
            return card;
        }
    }
}
