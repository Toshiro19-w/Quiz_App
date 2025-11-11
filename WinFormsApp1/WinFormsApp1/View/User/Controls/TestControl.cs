using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinFormsApp1.Helpers;
using WinFormsApp1.Models.EF;
using Microsoft.EntityFrameworkCore;
using static WinFormsApp1.Helpers.ColorPalette;

namespace WinFormsApp1.View.User.Controls
{
    public partial class TestControl : UserControl
    {
        private FlowLayoutPanel assignedPanel;
        private FlowLayoutPanel dueDatePanel;
        private FlowLayoutPanel overDuePanel;

        public TestControl()
        {
            InitializeComponent();
            SetupUI();
            LoadTests();
        }

        private void SetupUI()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = ColorPalette.Background;
            this.Padding = new Padding(0, 70, 0, 0);

            var lblAssigned = new Label { Text = "Đã hoàn thành", Font = new Font("Segoe UI", 14, FontStyle.Bold), Location = new Point(20, 20), AutoSize = true };
            assignedPanel = new FlowLayoutPanel { Location = new Point(20, 60), Size = new Size(1150, 150), AutoScroll = true };

            var lblDueDate = new Label { Text = "Đang làm", Font = new Font("Segoe UI", 14, FontStyle.Bold), Location = new Point(20, 230), AutoSize = true };
            dueDatePanel = new FlowLayoutPanel { Location = new Point(20, 270), Size = new Size(1150, 150), AutoScroll = true };

            var lblOverDue = new Label { Text = "Quá hạn", Font = new Font("Segoe UI", 14, FontStyle.Bold), Location = new Point(20, 440), AutoSize = true, ForeColor = Error };
            overDuePanel = new FlowLayoutPanel { Location = new Point(20, 480), Size = new Size(1150, 150), AutoScroll = true };

            this.Controls.AddRange(new Control[] { lblAssigned, assignedPanel, lblDueDate, dueDatePanel, lblOverDue, overDuePanel });
        }

        private async void LoadTests()
        {
            using var context = new LearningPlatformContext();
            var userId = AuthHelper.CurrentUser?.UserId;
            if (!userId.HasValue) return;

            var now = DateTime.Now;
            var tests = await context.TestAttempts.Where(ta => ta.UserId == userId.Value).Include(ta => ta.Test).ToListAsync();

            assignedPanel.Controls.Clear();
            dueDatePanel.Controls.Clear();
            overDuePanel.Controls.Clear();

            foreach (var attempt in tests)
            {
                var card = CreateTestCard(attempt);
                if (attempt.Status == "Completed")
                    assignedPanel.Controls.Add(card);
                else if (attempt.Test.CloseAt.HasValue && attempt.Test.CloseAt.Value < now)
                    overDuePanel.Controls.Add(card);
                else
                    dueDatePanel.Controls.Add(card);
            }
        }

        private Panel CreateTestCard(Models.Entities.TestAttempt attempt)
        {
            var card = new Panel { Size = new Size(300, 150), BackColor = Color.White, BorderStyle = BorderStyle.FixedSingle, Margin = new Padding(10) };
            var lblTitle = new Label { Text = attempt.Test.Title, Location = new Point(10, 10), Size = new Size(280, 40), Font = new Font("Segoe UI", 11, FontStyle.Bold) };
            var lblStatus = new Label { Text = $"Trạng thái: {attempt.Status}", Location = new Point(10, 55), AutoSize = true, ForeColor = TextSecondary };
            var lblScore = new Label { Text = attempt.Score.HasValue ? $"Điểm: {attempt.Score}/{attempt.MaxScore}" : "Chưa có điểm", Location = new Point(10, 80), AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = ButtonPrimary };
            var btnStart = new Button { Text = attempt.Status == "Completed" ? "Xem kết quả" : "Bắt đầu", Location = new Point(10, 110), Size = new Size(280, 30), BackColor = ButtonSecondary, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Cursor = Cursors.Hand };
            btnStart.FlatAppearance.BorderSize = 0;
            btnStart.Click += (s, e) => MessageBox.Show($"Mở bài kiểm tra: {attempt.Test.Title}");
            card.Controls.AddRange(new Control[] { lblTitle, lblStatus, lblScore, btnStart });
            return card;
        }
    }
}
