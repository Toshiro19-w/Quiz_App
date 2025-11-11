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
    public partial class formTest : Form
    {
        public formTest()
        {
            InitializeComponent();
        }

        private async void formTest_Load(object sender, EventArgs e)
        {
            FormLayoutHelper.SetupTestLayoutAutoCenter(
                this, mainPanel, label1, label2, label17, AssignedPanel, dueDatePanel, overDuePanel
            );
            await LoadTests();
        }

        private async System.Threading.Tasks.Task LoadTests()
        {
            using var context = new LearningPlatformContext();
            var userId = AuthHelper.CurrentUser?.UserId;
            if (!userId.HasValue) return;

            var now = DateTime.Now;
            var tests = await context.TestAttempts
                .Where(ta => ta.UserId == userId.Value)
                .Include(ta => ta.Test)
                .ToListAsync();

            AssignedPanel.Controls.Clear();
            dueDatePanel.Controls.Clear();
            overDuePanel.Controls.Clear();

            foreach (var attempt in tests)
            {
                var card = CreateTestCard(attempt);
                
                if (attempt.Status == "Completed")
                    AssignedPanel.Controls.Add(card);
                else if (attempt.Test.CloseAt.HasValue && attempt.Test.CloseAt.Value < now)
                    overDuePanel.Controls.Add(card);
                else
                    dueDatePanel.Controls.Add(card);
            }
        }

        private Panel CreateTestCard(Models.Entities.TestAttempt attempt)
        {
            var card = new Panel
            {
                Size = new Size(300, 150),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(10)
            };

            var lblTitle = new Label
            {
                Text = attempt.Test.Title,
                Location = new Point(10, 10),
                Size = new Size(280, 40),
                Font = new Font("Segoe UI", 11, FontStyle.Bold)
            };

            var lblStatus = new Label
            {
                Text = $"Trạng thái: {attempt.Status}",
                Location = new Point(10, 55),
                AutoSize = true,
                ForeColor = TextSecondary
            };

            var lblScore = new Label
            {
                Text = attempt.Score.HasValue ? $"Điểm: {attempt.Score}/{attempt.MaxScore}" : "Chưa có điểm",
                Location = new Point(10, 80),
                AutoSize = true,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = ButtonPrimary
            };

            var btnStart = new Button
            {
                Text = attempt.Status == "Completed" ? "Xem kết quả" : "Bắt đầu",
                Location = new Point(10, 110),
                Size = new Size(280, 30),
                BackColor = ButtonSecondary,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Tag = new { AttemptId = attempt.AttemptId, Status = attempt.Status }
            };
            btnStart.FlatAppearance.BorderSize = 0;
            btnStart.Click += BtnStart_Click;

            card.Controls.AddRange(new Control[] { lblTitle, lblStatus, lblScore, btnStart });
            return card;
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            dynamic tag = btn.Tag;
            
            if (tag.Status == "Completed")
            {
                var form = new formTestComplete();
                form.Show();
            }
            else
            {
                var form = new formTestAssign();
                form.Show();
            }
        }
    }
}
