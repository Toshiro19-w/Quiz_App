using Microsoft.EntityFrameworkCore;
using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp1.Helpers;
using WinFormsApp1.Models.EF;

namespace WinFormsApp1.View.User.Controls.TestControls
{
    public partial class StudentTestDetailForm : Form
    {
        private int _testId;
        private int _userId;

        public StudentTestDetailForm(int testId, int userId)
        {
            InitializeComponent();
            _testId = testId;
            _userId = userId;

            btnClose.Click += (s, e) => this.Close();
            this.Load += StudentTestDetailForm_Load;
        }

        private async void StudentTestDetailForm_Load(object sender, EventArgs e)
        {
            await LoadStudentAttempts();
        }

        private async Task LoadStudentAttempts()
        {
            try
            {
                using var context = new LearningPlatformContext();

                var student = await context.Users.FindAsync(_userId);
                var test = await context.Tests.FindAsync(_testId);

                if (student == null || test == null) return;

                lblStudentInfo.Text = $"Học viên: {student.FullName} ({student.Email})";
                this.Text = $"Chi tiết: {student.FullName} - {test.Title}";

                var attempts = await context.TestAttempts
                    .Where(ta => ta.TestId == _testId && ta.UserId == _userId)
                    .OrderByDescending(ta => ta.SubmittedAt ?? ta.StartedAt)
                    .ToListAsync();

                flowAttempts.Controls.Clear();

                if (attempts.Count == 0)
                {
                    var lblEmpty = new Label
                    {
                        Text = "Học viên chưa làm bài",
                        Font = new Font("Segoe UI", 12),
                        ForeColor = Color.Gray,
                        AutoSize = true,
                        Padding = new Padding(10)
                    };
                    flowAttempts.Controls.Add(lblEmpty);
                    return;
                }

                for (int i = 0; i < attempts.Count; i++)
                {
                    var card = CreateAttemptCard(attempts[i], i + 1);
                    flowAttempts.Controls.Add(card);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Panel CreateAttemptCard(Models.Entities.TestAttempt attempt, int number)
        {
            var panel = new Panel
            {
                Width = 820,
                Height = 140,
                BackColor = Color.White,
                Margin = new Padding(0, 0, 0, 15),
                BorderStyle = BorderStyle.FixedSingle
            };

            // Attempt number
            var lblNumber = new Label
            {
                Text = $"Lần {number}",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = ColorPalette.Primary,
                AutoSize = true,
                Location = new Point(20, 15)
            };

            // Date
            var lblDate = new Label
            {
                Text = $"🕒 {(attempt.SubmittedAt ?? attempt.StartedAt):dd/MM/yyyy HH:mm}",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.Gray,
                AutoSize = true,
                Location = new Point(20, 45)
            };

            // Score
            var percentage = attempt.MaxScore > 0 ? (attempt.Score / attempt.MaxScore) * 100 : 0;
            var lblScore = new Label
            {
                Text = $"Điểm: {attempt.Score:F2}/{attempt.MaxScore:F2} ({percentage:F1}%)",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = percentage >= 60 ? ColorPalette.Success : ColorPalette.Error,
                AutoSize = true,
                Location = new Point(20, 75)
            };

            // Time spent
            var minutes = (attempt.TimeSpentSec ?? 0) / 60;
            var seconds = (attempt.TimeSpentSec ?? 0) % 60;
            var lblTime = new Label
            {
                Text = $"⏱️ Thời gian: {minutes}:{seconds:D2}",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.Gray,
                AutoSize = true,
                Location = new Point(20, 105)
            };

            // Status
            var lblStatus = new Label
            {
                Text = GetStatusText(attempt.Status),
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = GetStatusColor(attempt.Status),
                AutoSize = true,
                Padding = new Padding(10, 5, 10, 5),
                Location = new Point(280, 20)
            };

            // View detail button
            var btnView = new Button
            {
                Text = "Xem chi tiết →",
                Size = new Size(140, 40),
                Location = new Point(660, 50),
                BackColor = ColorPalette.Primary,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnView.FlatAppearance.BorderSize = 0;
            btnView.Click += (s, e) => ViewAttemptDetail(attempt.AttemptId);

            panel.Controls.AddRange(new Control[] {
                lblNumber, lblDate, lblScore, lblTime, lblStatus, btnView
            });

            return panel;
        }

        private string GetStatusText(string status)
        {
            return status switch
            {
                "Graded" => "✓ Đã chấm",
                "Submitted" => "📝 Đã nộp",
                "InProgress" => "⏳ Đang làm",
                _ => status
            };
        }

        private Color GetStatusColor(string status)
        {
            return status switch
            {
                "Graded" => ColorPalette.Success,
                "Submitted" => Color.FromArgb(255, 193, 7),
                "InProgress" => ColorPalette.Primary,
                _ => Color.Gray
            };
        }

        private void ViewAttemptDetail(int attemptId)
        {
            try
            {
                // Open review form in a new window
                var reviewForm = new Form
                {
                    Text = "Xem chi tiết bài làm",
                    Size = new Size(1200, 800),
                    StartPosition = FormStartPosition.CenterParent
                };

                var reviewControl = new TestReviewControl
                {
                    Dock = DockStyle.Fill
                };

                reviewForm.Controls.Add(reviewControl);
                _ = reviewControl.LoadAttemptAsync(attemptId);

                reviewForm.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
