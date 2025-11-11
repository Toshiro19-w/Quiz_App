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
    public partial class formSearch : Form
    {
        public formSearch()
        {
            InitializeComponent();
        }

        private void formSearch_Load(object sender, EventArgs e)
        {
            cmbSearchType.Items.AddRange(new[] { "Tất cả", "Khóa học", "Bài kiểm tra", "Flashcard" });
            cmbSearchType.SelectedIndex = 0;
        }

        private async void searchButton_Click(object sender, EventArgs e)
        {
            string searchText = searchTB.Text.Trim();
            if (string.IsNullOrEmpty(searchText))
            {
                MessageBox.Show("Vui lòng nhập từ khóa tìm kiếm!");
                return;
            }

            await PerformSearch(searchText, cmbSearchType.SelectedItem.ToString());
        }

        private async System.Threading.Tasks.Task PerformSearch(string searchText, string searchType)
        {
            flowLayoutResults.Controls.Clear();
            using var context = new LearningPlatformContext();

            switch (searchType)
            {
                case "Khóa học":
                    await SearchCourses(context, searchText);
                    break;
                case "Bài kiểm tra":
                    await SearchTests(context, searchText);
                    break;
                case "Flashcard":
                    await SearchFlashcards(context, searchText);
                    break;
                default:
                    await SearchAll(context, searchText);
                    break;
            }

            lblResults.Text = $"Kết quả cho: {searchText} ({flowLayoutResults.Controls.Count} kết quả)";
        }

        private async System.Threading.Tasks.Task SearchCourses(LearningPlatformContext context, string searchText)
        {
            var courses = await context.Courses
                .Where(c => c.Title.Contains(searchText) || (c.Summary != null && c.Summary.Contains(searchText)))
                .Take(20)
                .ToListAsync();

            foreach (var course in courses)
            {
                var card = CreateResultCard(course.Title, course.Summary, "Khóa học", course.CourseId);
                flowLayoutResults.Controls.Add(card);
            }
        }

        private async System.Threading.Tasks.Task SearchTests(LearningPlatformContext context, string searchText)
        {
            var tests = await context.Tests
                .Where(t => t.Title.Contains(searchText) || (t.Description != null && t.Description.Contains(searchText)))
                .Take(20)
                .ToListAsync();

            foreach (var test in tests)
            {
                var card = CreateResultCard(test.Title, test.Description, "Bài kiểm tra", test.TestId);
                flowLayoutResults.Controls.Add(card);
            }
        }

        private async System.Threading.Tasks.Task SearchFlashcards(LearningPlatformContext context, string searchText)
        {
            var flashcards = await context.FlashcardSets
                .Where(f => f.Title.Contains(searchText) || (f.Description != null && f.Description.Contains(searchText)))
                .Take(20)
                .ToListAsync();

            foreach (var flashcard in flashcards)
            {
                var card = CreateResultCard(flashcard.Title, flashcard.Description, "Flashcard", flashcard.SetId);
                flowLayoutResults.Controls.Add(card);
            }
        }

        private async System.Threading.Tasks.Task SearchAll(LearningPlatformContext context, string searchText)
        {
            await SearchCourses(context, searchText);
            await SearchTests(context, searchText);
            await SearchFlashcards(context, searchText);
        }

        private Panel CreateResultCard(string title, string description, string type, int id)
        {
            var card = new Panel
            {
                Size = new Size(1050, 100),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(10)
            };

            var lblTitle = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                Location = new Point(10, 10),
                AutoSize = true
            };

            var lblType = new Label
            {
                Text = type,
                Font = new Font("Segoe UI", 9),
                ForeColor = TextSecondary,
                Location = new Point(10, 35),
                AutoSize = true
            };

            var lblDesc = new Label
            {
                Text = description?.Length > 100 ? description.Substring(0, 100) + "..." : description,
                Font = new Font("Segoe UI", 9),
                Location = new Point(10, 55),
                Size = new Size(900, 40)
            };

            var btnView = new Button
            {
                Text = "Xem",
                Location = new Point(950, 30),
                Size = new Size(80, 35),
                BackColor = ButtonSecondary,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Tag = id
            };
            btnView.FlatAppearance.BorderSize = 0;
            btnView.FlatAppearance.MouseOverBackColor = SecondaryDark;
            btnView.Click += (s, e) => MessageBox.Show($"Mở {type} ID: {id}");

            card.Controls.AddRange(new Control[] { lblTitle, lblType, lblDesc, btnView });
            return card;
        }
    }
}
