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
    public partial class SearchControl : UserControl
    {
        private TextBox searchTB;
        private FlowLayoutPanel flowResults;

        public SearchControl()
        {
            InitializeComponent();
            SetupUI();
        }

        private void SetupUI()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = ColorPalette.Background;
            this.Padding = new Padding(0, 70, 0, 0);

            var lblTitle = new Label { Text = "🔍 Tìm kiếm", Font = new Font("Segoe UI", 18, FontStyle.Bold), Location = new Point(20, 10), AutoSize = true };
            searchTB = new TextBox { Location = new Point(20, 60), Size = new Size(500, 30), Font = new Font("Segoe UI", 11), PlaceholderText = "Nhập từ khóa tìm kiếm..." };
            searchTB.KeyPress += (s, e) => { if (e.KeyChar == (char)13) BtnSearch_Click(s, e); };

            var btnSearch = new Button { Text = "Tìm kiếm", Location = new Point(530, 60), Size = new Size(120, 30), BackColor = ButtonSecondary, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Cursor = Cursors.Hand };
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.Click += BtnSearch_Click;

            var cmbType = new ComboBox { Location = new Point(660, 60), Size = new Size(150, 30), DropDownStyle = ComboBoxStyle.DropDownList };
            cmbType.Items.AddRange(new[] { "Tất cả", "Khóa học", "Bài kiểm tra", "Flashcard" });
            cmbType.SelectedIndex = 0;
            cmbType.Tag = "searchType";

            var lblResults = new Label { Text = "", Location = new Point(20, 110), AutoSize = true, Font = new Font("Segoe UI", 10), ForeColor = TextSecondary };
            lblResults.Tag = "resultsLabel";

            flowResults = new FlowLayoutPanel { Location = new Point(20, 140), Size = new Size(1150, 480), AutoScroll = true };

            this.Controls.AddRange(new Control[] { lblTitle, searchTB, btnSearch, cmbType, lblResults, flowResults });
        }

        private async void BtnSearch_Click(object sender, EventArgs e)
        {
            string searchText = searchTB.Text.Trim();
            if (string.IsNullOrEmpty(searchText)) { MessageBox.Show("Vui lòng nhập từ khóa!"); return; }

            flowResults.Controls.Clear();
            using var context = new LearningPlatformContext();

            var cmbType = this.Controls.OfType<ComboBox>().FirstOrDefault(c => c.Tag?.ToString() == "searchType");
            var searchType = cmbType?.SelectedItem?.ToString() ?? "Tất cả";

            var courses = await context.Courses.Where(c => c.Title.Contains(searchText) || (c.Summary != null && c.Summary.Contains(searchText))).Take(20).ToListAsync();
            var tests = searchType == "Tất cả" || searchType == "Bài kiểm tra" ? await context.Tests.Where(t => t.Title.Contains(searchText)).Take(20).ToListAsync() : new System.Collections.Generic.List<Models.Entities.Test>();
            var flashcards = searchType == "Tất cả" || searchType == "Flashcard" ? await context.FlashcardSets.Where(f => f.Title.Contains(searchText)).Take(20).ToListAsync() : new System.Collections.Generic.List<Models.Entities.FlashcardSet>();

            int totalResults = courses.Count + tests.Count + flashcards.Count;
            var lblResults = this.Controls.OfType<Label>().FirstOrDefault(l => l.Tag?.ToString() == "resultsLabel");
            if (lblResults != null) lblResults.Text = $"Tìm thấy {totalResults} kết quả cho \"{searchText}\"";

            foreach (var course in courses)
            {
                var card = new Panel { Size = new Size(1100, 100), BackColor = Color.White, BorderStyle = BorderStyle.FixedSingle, Margin = new Padding(5) };
                card.MouseEnter += (s, ev) => card.BackColor = Color.FromArgb(245, 245, 245);
                card.MouseLeave += (s, ev) => card.BackColor = Color.White;

                var lblType = new Label { Text = "📚 Khóa học", Location = new Point(10, 10), AutoSize = true, Font = new Font("Segoe UI", 9), ForeColor = ButtonPrimary };
                var lblTitle = new Label { Text = course.Title, Location = new Point(10, 35), Size = new Size(900, 25), Font = new Font("Segoe UI", 12, FontStyle.Bold) };
                var lblDesc = new Label { Text = course.Summary?.Length > 80 ? course.Summary.Substring(0, 80) + "..." : course.Summary, Location = new Point(10, 65), Size = new Size(900, 25), ForeColor = TextSecondary };
                var btnView = new Button { Text = "Xem", Location = new Point(1000, 35), Size = new Size(80, 30), BackColor = ButtonSecondary, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Cursor = Cursors.Hand };
                btnView.FlatAppearance.BorderSize = 0;
                btnView.Click += (s, ev) => NavigationHelper.NavigateTo("lesson");
                card.Controls.AddRange(new Control[] { lblType, lblTitle, lblDesc, btnView });
                flowResults.Controls.Add(card);
            }

            foreach (var test in tests)
            {
                var card = new Panel { Size = new Size(1100, 100), BackColor = Color.White, BorderStyle = BorderStyle.FixedSingle, Margin = new Padding(5) };
                card.MouseEnter += (s, ev) => card.BackColor = Color.FromArgb(245, 245, 245);
                card.MouseLeave += (s, ev) => card.BackColor = Color.White;

                var lblType = new Label { Text = "📝 Bài kiểm tra", Location = new Point(10, 10), AutoSize = true, Font = new Font("Segoe UI", 9), ForeColor = ButtonSecondary };
                var lblTitle = new Label { Text = test.Title, Location = new Point(10, 35), Size = new Size(900, 25), Font = new Font("Segoe UI", 12, FontStyle.Bold) };
                var lblDesc = new Label { Text = test.Description?.Length > 80 ? test.Description.Substring(0, 80) + "..." : test.Description, Location = new Point(10, 65), Size = new Size(900, 25), ForeColor = TextSecondary };
                var btnView = new Button { Text = "Xem", Location = new Point(1000, 35), Size = new Size(80, 30), BackColor = ButtonSecondary, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Cursor = Cursors.Hand };
                btnView.FlatAppearance.BorderSize = 0;
                btnView.Click += (s, ev) => NavigationHelper.NavigateTo("test");
                card.Controls.AddRange(new Control[] { lblType, lblTitle, lblDesc, btnView });
                flowResults.Controls.Add(card);
            }

            foreach (var flashcard in flashcards)
            {
                var card = new Panel { Size = new Size(1100, 100), BackColor = Color.White, BorderStyle = BorderStyle.FixedSingle, Margin = new Padding(5) };
                card.MouseEnter += (s, ev) => card.BackColor = Color.FromArgb(245, 245, 245);
                card.MouseLeave += (s, ev) => card.BackColor = Color.White;

                var lblType = new Label { Text = "🎴 Flashcard", Location = new Point(10, 10), AutoSize = true, Font = new Font("Segoe UI", 9), ForeColor = Color.Purple };
                var lblTitle = new Label { Text = flashcard.Title, Location = new Point(10, 35), Size = new Size(900, 25), Font = new Font("Segoe UI", 12, FontStyle.Bold) };
                var lblDesc = new Label { Text = flashcard.Description?.Length > 80 ? flashcard.Description.Substring(0, 80) + "..." : flashcard.Description, Location = new Point(10, 65), Size = new Size(900, 25), ForeColor = TextSecondary };
                var btnView = new Button { Text = "Xem", Location = new Point(1000, 35), Size = new Size(80, 30), BackColor = ButtonSecondary, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Cursor = Cursors.Hand };
                btnView.FlatAppearance.BorderSize = 0;
                btnView.Click += (s, ev) => MessageBox.Show($"Xem flashcard: {flashcard.Title}");
                card.Controls.AddRange(new Control[] { lblType, lblTitle, lblDesc, btnView });
                flowResults.Controls.Add(card);
            }
        }
    }
}
