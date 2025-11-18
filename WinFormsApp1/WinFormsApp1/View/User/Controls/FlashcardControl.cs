using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using WinFormsApp1.Controllers;
using WinFormsApp1.View.User.Controls.FlashcardControls;
using WinFormsApp1.Models.Entities;

namespace WinFormsApp1.View.User.Controls
{
    public partial class FlashcardControl : UserControl
    {
        private readonly FlashcardController _flashcardController;

        public FlashcardControl()
        {
            InitializeComponent();
            _flashcardController = new FlashcardController();
        }

        private void FlashcardControl_Load(object sender, EventArgs e)
        {
            LoadAllFlashcardSets();
        }

        private void btnCreateFlashcard_Click(object sender, EventArgs e)
        {
            var form = this.FindForm();
            if (form == null) return;

            var mainPanel = FindControlRecursive(form, "mainContentPanel") as Panel;

            if (mainPanel == null)
            {
                mainPanel = this.Parent as Panel;
            }

            if (mainPanel == null) return;

            mainPanel.Controls.Clear();

            var createControl = new CreateFlashcardControl();
            createControl.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(createControl);
        }

        private async void LoadAllFlashcardSets()
        {
            flowFlashcards.Controls.Clear();

            try
            {
                // Use controller to get all public flashcard sets
                var flashcardSets = await _flashcardController.GetAllPublicFlashcardSetsAsync();

                if (flashcardSets.Count == 0)
                {
                    ShowEmptyState();
                    return;
                }

                // Update count label
                lblFlashcardCount.Text = $"{flashcardSets.Count} bộ flashcard";

                // Create card for each flashcard set
                foreach (var flashcardSet in flashcardSets)
                {
                    var card = CreateFlashcardCard(flashcardSet);
                    flowFlashcards.Controls.Add(card);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải flashcards: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Panel CreateFlashcardCard(FlashcardSet flashcardSet)
        {
            // Card size: 3 cards per row
            // Total width available: ~1700px, divided by 3 = ~567px per card
            // With margins: 540px card width, height increased for better spacing
            var card = new Panel
            {
                Size = new Size(540, 350),
                BackColor = Color.FromArgb(124, 77, 255),
                BorderStyle = BorderStyle.None,
                Margin = new Padding(15),
                Cursor = Cursors.Hand
            };

            card.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (var path = GetRoundedRectPath(new Rectangle(0, 0, card.Width, card.Height), 12))
                {
                    card.Region = new Region(path);
                }
            };

            // Icon placeholder - centered
            var lblIcon = new Label
            {
                Text = "📚",
                Font = new Font("Segoe UI", 56),
                ForeColor = Color.White,
                Location = new Point(235, 35),
                Size = new Size(70, 70),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent
            };
            card.Controls.Add(lblIcon);

            // Card count badge - top left
            var lblCount = new Label
            {
                Text = $"📇 {flashcardSet.Flashcards.Count} thẻ",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(100, 0, 0, 0),
                Location = new Point(20, 25),
                Size = new Size(110, 35),
                TextAlign = ContentAlignment.MiddleCenter
            };
            card.Controls.Add(lblCount);

            // Title - centered, larger
            var lblTitle = new Label
            {
                Text = flashcardSet.Title,
                Font = new Font("Segoe UI", 15F, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(20, 135),
                Size = new Size(500, 70),
                TextAlign = ContentAlignment.TopCenter,
                BackColor = Color.Transparent
            };
            card.Controls.Add(lblTitle);

            // Description (if available)
            if (!string.IsNullOrEmpty(flashcardSet.Description))
            {
                var lblDescription = new Label
                {
                    Text = flashcardSet.Description.Length > 70 
                        ? flashcardSet.Description.Substring(0, 70) + "..." 
                        : flashcardSet.Description,
                    Font = new Font("Segoe UI", 10F),
                    ForeColor = Color.FromArgb(230, 230, 255),
                    Location = new Point(20, 210),
                    Size = new Size(500, 45),
                    TextAlign = ContentAlignment.TopCenter,
                    BackColor = Color.Transparent
                };
                card.Controls.Add(lblDescription);
            }

            // Bottom section - Author
            var lblAuthor = new Label
            {
                Text = $"👤 {flashcardSet.Owner?.FullName ?? "Unknown"}",
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.White,
                Location = new Point(20, 270),
                AutoSize = true,
                BackColor = Color.Transparent
            };
            card.Controls.Add(lblAuthor);

            // Language tag - top right
            if (!string.IsNullOrEmpty(flashcardSet.Language))
            {
                var lblLanguage = new Label
                {
                    Text = flashcardSet.Language,
                    Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                    ForeColor = Color.White,
                    BackColor = Color.FromArgb(150, 255, 255, 255),
                    Location = new Point(450, 25),
                    Size = new Size(70, 30),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                card.Controls.Add(lblLanguage);
            }

            // Buttons panel - bottom section with better spacing
            var btnDetail = new Button
            {
                Text = "👁 Xem chi tiết",
                Location = new Point(90, 300),
                Size = new Size(165, 38),
                BackColor = Color.White,
                ForeColor = Color.FromArgb(124, 77, 255),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold)
            };
            btnDetail.FlatAppearance.BorderSize = 0;
            btnDetail.Click += (s, e) => ShowFlashcardDetail(flashcardSet.SetId);
            card.Controls.Add(btnDetail);

            var btnStudy = new Button
            {
                Text = "▶ Học ngay",
                Location = new Point(285, 300),
                Size = new Size(165, 38),
                BackColor = Color.FromArgb(76, 175, 80),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold)
            };
            btnStudy.FlatAppearance.BorderSize = 0;
            btnStudy.Click += (s, e) => StartStudying(flashcardSet.SetId);
            card.Controls.Add(btnStudy);

            // Click event for the whole card
            card.Click += (s, e) => ShowFlashcardDetail(flashcardSet.SetId);
            
            // Make child controls also trigger the card click (except buttons)
            foreach (Control ctrl in card.Controls)
            {
                if (!(ctrl is Button))
                {
                    ctrl.Click += (s, e) => ShowFlashcardDetail(flashcardSet.SetId);
                }
            }

            // Hover effects
            card.MouseEnter += (s, e) => card.BackColor = Color.FromArgb(140, 95, 255);
            card.MouseLeave += (s, e) => card.BackColor = Color.FromArgb(124, 77, 255);

            return card;
        }

        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            var path = new GraphicsPath();
            int diameter = radius * 2;
            var arc = new Rectangle(rect.Location, new Size(diameter, diameter));

            path.AddArc(arc, 180, 90);
            arc.X = rect.Right - diameter;
            path.AddArc(arc, 270, 90);
            arc.Y = rect.Bottom - diameter;
            path.AddArc(arc, 0, 90);
            arc.X = rect.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();
            return path;
        }

        private void ShowFlashcardDetail(int setId)
        {
            var form = this.FindForm();
            if (form == null) return;

            var mainPanel = FindControlRecursive(form, "mainContentPanel") as Panel;

            if (mainPanel == null)
            {
                mainPanel = this.Parent as Panel;
            }

            if (mainPanel == null) return;

            mainPanel.Controls.Clear();

            var detail = new FlashcardDetailControl(setId);
            detail.Dock = DockStyle.Fill;
            mainPanel.Controls.Add(detail);
        }

        private async void StartStudying(int setId)
        {
            // Kiểm tra xem bộ flashcard có thẻ nào không
            try
            {
                var flashcardSet = await _flashcardController.GetFlashcardSetByIdAsync(setId);

                if (flashcardSet == null)
                {
                    MessageBox.Show("Không tìm thấy bộ flashcard!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (flashcardSet.Flashcards.Count == 0)
                {
                    MessageBox.Show("Bộ flashcard này chưa có thẻ nào để học!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Mở form học flashcard
                var studyForm = new FlashcardStudyForm(setId);
                studyForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở flashcard: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowEmptyState()
        {
            var emptyPanel = new Panel
            {
                Size = new Size(800, 400),
                BackColor = Color.Transparent,
                Location = new Point(450, 200)
            };

            var lblIcon = new Label
            {
                Text = "📚",
                Font = new Font("Segoe UI", 72),
                ForeColor = Color.Gray,
                Location = new Point(330, 80),
                AutoSize = true
            };
            emptyPanel.Controls.Add(lblIcon);

            var lblTitle = new Label
            {
                Text = "Chưa có flashcard nào",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = Color.FromArgb(60, 60, 60),
                Location = new Point(220, 180),
                AutoSize = true
            };
            emptyPanel.Controls.Add(lblTitle);

            var lblMessage = new Label
            {
                Text = "Hãy quay lại sau để khám phá các bộ flashcard mới!",
                Font = new Font("Segoe UI", 12),
                ForeColor = Color.Gray,
                Location = new Point(180, 230),
                Size = new Size(440, 30),
                TextAlign = ContentAlignment.MiddleCenter
            };
            emptyPanel.Controls.Add(lblMessage);

            flowFlashcards.Controls.Add(emptyPanel);
        }

        private void FlashcardControl_Resize(object sender, EventArgs e)
        {
            if (flowFlashcards != null)
            {
                flowFlashcards.Size = new Size(this.Width - 40, this.Height - 180);
            }
        }

        private Control FindControlRecursive(Control parent, string name)
        {
            foreach (Control c in parent.Controls)
            {
                if (string.Equals(c.Name, name, StringComparison.OrdinalIgnoreCase)) return c;
                var found = FindControlRecursive(c, name);
                if (found != null) return found;
            }
            return null;
        }
    }
}
