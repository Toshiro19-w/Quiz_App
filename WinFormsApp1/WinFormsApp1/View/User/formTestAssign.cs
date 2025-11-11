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
    public partial class formTestAssign : Form
    {
        private FlowLayoutPanel flowPanel;

        public formTestAssign()
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
                Text = "Bài kiểm tra được giao",
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
                .Where(ta => ta.UserId == userId.Value && ta.Status == "InProgress")
                .Include(ta => ta.Test)
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
                Size = new Size(350, 150),
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

            var lblTime = new Label
            {
                Text = attempt.Test.TimeLimitSec.HasValue ? $"Thời gian: {attempt.Test.TimeLimitSec / 60} phút" : "Không giới hạn",
                Location = new Point(10, 55),
                AutoSize = true,
                ForeColor = TextSecondary
            };

            var lblQuestions = new Label
            {
                Text = $"Số câu: {attempt.Test.Questions.Count}",
                Location = new Point(10, 80),
                AutoSize = true,
                ForeColor = TextSecondary
            };

            var btnStart = new Button
            {
                Text = "Bắt đầu làm bài",
                Location = new Point(10, 110),
                Size = new Size(330, 30),
                BackColor = ButtonSecondary,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnStart.FlatAppearance.BorderSize = 0;
            btnStart.Click += (s, e) => MessageBox.Show($"Bắt đầu bài kiểm tra: {attempt.Test.Title}");

            card.Controls.AddRange(new Control[] { lblTitle, lblTime, lblQuestions, btnStart });
            return card;
        }
    }
}
