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
    public partial class formTestOverDue : Form
    {
        private FlowLayoutPanel flowPanel;

        public formTestOverDue()
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
                Text = "Bài kiểm tra quá hạn",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = Error,
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

            var now = DateTime.Now;
            var tests = await context.TestAttempts
                .Where(ta => ta.UserId == userId.Value && 
                             ta.Test.CloseAt.HasValue && 
                             ta.Test.CloseAt.Value < now)
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
                BackColor = Color.FromArgb(255, 240, 240),
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

            var lblOverdue = new Label
            {
                Text = "⚠️ QUÁ HẠN",
                Location = new Point(10, 55),
                AutoSize = true,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Error
            };

            var lblDate = new Label
            {
                Text = $"Hạn nộp: {attempt.Test.CloseAt?.ToString("dd/MM/yyyy HH:mm")}",
                Location = new Point(10, 80),
                AutoSize = true,
                ForeColor = TextSecondary
            };

            var btnView = new Button
            {
                Text = "Xem chi tiết",
                Location = new Point(10, 110),
                Size = new Size(330, 30),
                BackColor = Error,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Tag = attempt.TestId
            };
            btnView.FlatAppearance.BorderSize = 0;
            btnView.Click += BtnView_Click;

            card.Controls.AddRange(new Control[] { lblTitle, lblOverdue, lblDate, btnView });
            return card;
        }

        private void BtnView_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            int testId = (int)btn.Tag;
            MessageBox.Show($"Mở bài kiểm tra ID: {testId}");
        }
    }
}
