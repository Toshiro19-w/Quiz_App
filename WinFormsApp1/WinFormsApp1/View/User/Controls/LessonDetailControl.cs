using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp1.Helpers;
using WinFormsApp1.Models.EF;
using WinFormsApp1.Models.Entities;
using LibVLCSharp.Shared;
using LibVLCSharp.WinForms;

namespace WinFormsApp1.View.User.Controls
{
    public partial class LessonDetailControl : UserControl
    {
        private Course _currentCourse;
        private Lesson _currentLesson;
        private List<LessonContent> _currentContents;
        private int _currentContentIndex = 0;

        // Flashcard state
        private List<Flashcard> _flashcards;
        private int _currentFlashcardIndex = 0;
        private bool _isFlipped = false;

        // --- FLASHCARD UI COMPONENTS (MỚI) ---
        private Panel _pnlCardFace;      
        private Label _lblCardContent;   
        private Label _lblCardSide;      
        private Label _lblCardCounter;   
        private Button _btnFlip;         
        private Button _btnPrev;         
        private Button _btnNext;         

        // Test state
        private Test _currentTest;
        private List<Question> _questions;
        private Dictionary<int, List<int>> _selectedAnswers = new Dictionary<int, List<int>>();
        private DateTime _testStartTime;

        // Video tracking
        private System.Windows.Forms.Timer _videoProgressTimer;
        private int _totalWatchedSeconds = 0;

        // --- VLC Components ---
        private LibVLC _libVLC;
        private MediaPlayer _mediaPlayer;
        private VideoView _videoView;

        public LessonDetailControl()
        {
            InitializeComponent();
            if (!DesignMode)
            {
                Core.Initialize();
            }

            _libVLC = new LibVLC();
            _mediaPlayer = new MediaPlayer(_libVLC);

            _videoView = new VideoView
            {
                MediaPlayer = _mediaPlayer,
                Dock = DockStyle.Fill
            };

            // --- SỬA LẠI ĐOẠN NÀY ---
            // 1. Xóa hết các Label/Text cũ đang nằm trong pnlVideo (cái dòng chữ trắng trắng)
            pnlVideo.Controls.Clear();

            // 2. Sau đó mới thêm màn hình VLC vào
            pnlVideo.Controls.Add(_videoView);
            // ------------------------

            // Gán sự kiện theo dõi tiến độ
            _mediaPlayer.TimeChanged += MediaPlayer_TimeChanged;
            _mediaPlayer.EndReached += MediaPlayer_EndReached;
            InitializeEventHandlers();
        }

        private void InitializeEventHandlers()
        {
            // Video events - comment out until WMP is properly configured
            // videoPlayer.PlayStateChange += VideoPlayer_PlayStateChange;

            // Flashcard events
            btnFlipCard.Click += BtnFlipCard_Click;
            btnPrevCard.Click += BtnPrevCard_Click;
            btnNextCard.Click += BtnNextCard_Click;
            btnCompleteFlashcard.Click += BtnCompleteFlashcard_Click;

            // Test events
            btnSubmitTest.Click += BtnSubmitTest_Click;

            // Navigation events
            btnPrevLesson.Click += BtnPrevLesson_Click;
            btnNextLesson.Click += BtnNextLesson_Click;
            btnMarkComplete.Click += BtnMarkComplete_Click;

            if (btnSubmitTest != null) btnSubmitTest.Click += BtnSubmitTest_Click;

            // Navigation events
            if (btnPrevLesson != null) btnPrevLesson.Click += BtnPrevLesson_Click;
            if (btnNextLesson != null) btnNextLesson.Click += BtnNextLesson_Click;
            if (btnMarkComplete != null) btnMarkComplete.Click += BtnMarkComplete_Click;
        }

        /*private void SetupVideoProgressTimer()
        {
            _videoProgressTimer = new System.Windows.Forms.Timer();
            _videoProgressTimer.Interval = 5000; // 5 seconds
            _videoProgressTimer.Tick += VideoProgressTimer_Tick;
        }*/

        public async Task LoadLessonAsync(string courseSlug, int lessonId, int? openContentId = null)
        {
            try
            {
                using var context = new LearningPlatformContext();

                // Load course with full details
                _currentCourse = await context.Courses
                    .Include(c => c.CourseChapters)
                        .ThenInclude(ch => ch.Lessons)
                            .ThenInclude(l => l.LessonContents)
                    .FirstOrDefaultAsync(c => c.Slug == courseSlug);

                if (_currentCourse == null) return;

                // Sort chapters and lessons
                _currentCourse.CourseChapters = _currentCourse.CourseChapters.OrderBy(ch => ch.OrderIndex).ToList();
                foreach (var chapter in _currentCourse.CourseChapters)
                {
                    chapter.Lessons = chapter.Lessons.OrderBy(l => l.OrderIndex).ToList();
                }

                // Load specific lesson
                _currentLesson = _currentCourse.CourseChapters
                    .SelectMany(ch => ch.Lessons)
                    .FirstOrDefault(l => l.LessonId == lessonId);

                if (_currentLesson == null) return;

                _currentContents = _currentLesson.LessonContents.OrderBy(lc => lc.OrderIndex).ToList();

                // Update UI
                if (lblCourseTitle != null) lblCourseTitle.Text = _currentCourse.Title;

                await LoadSidebarAsync();
                await UpdateProgressAsync();

                // --- ĐOẠN CODE MỚI: XÁC ĐỊNH NỘI DUNG CẦN MỞ ---
                int targetIndex = 0;
                if (openContentId.HasValue)
                {
                    // Tìm vị trí của contentId được yêu cầu trong bài học này
                    var index = _currentContents.FindIndex(c => c.ContentId == openContentId.Value);
                    if (index >= 0) targetIndex = index;
                }

                await LoadContentAsync(targetIndex);
                // ------------------------------------------------
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải bài học: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadSidebarAsync()
        {
            flowLessons.Controls.Clear();

            foreach (var chapter in _currentCourse.CourseChapters.OrderBy(ch => ch.OrderIndex))
            {
                // --- BẮT ĐẦU SỬA PHẦN DESIGN CHƯƠNG ---

                // 1. Tạo Panel chứa tiêu đề chương (để làm nền to hơn)
                var pnlChapterHeader = new Panel
                {
                    Width = 320, // Bằng chiều rộng sidebar
                    Height = 50, // Tăng chiều cao lên (trước đây là tự động theo chữ)
                    BackColor = Color.FromArgb(248, 249, 250), // Màu nền xám nhẹ cho sang
                    Margin = new Padding(0, 10, 0, 0), // Cách đoạn trên ra một chút
                    Padding = new Padding(15, 0, 0, 0) // Thụt lề trái cho chữ
                };

                // 2. Tạo Label tiêu đề chương to và đậm hơn
                var lblChapter = new Label
                {
                    Text = chapter.Title.ToUpper(), // Viết hoa toàn bộ cho nổi bật (tùy chọn)
                    Font = new Font("Segoe UI", 12, FontStyle.Bold), // Tăng size từ 11 lên 12 hoặc 13
                    ForeColor = ColorPalette.Primary, // Hoặc dùng màu tối: Color.FromArgb(50, 50, 50)
                    AutoSize = false, // Tắt tự động co giãn để căn chỉnh theo Panel
                    Dock = DockStyle.Fill, // Lấp đầy Panel cha
                    TextAlign = ContentAlignment.MiddleLeft // Căn giữa theo chiều dọc
                };

                // 3. Thêm đường gạch chân (Optional - cho giống thiết kế hiện đại)
                var bottomBorder = new Panel
                {
                    Height = 1,
                    Dock = DockStyle.Bottom,
                    BackColor = Color.FromArgb(220, 220, 220)
                };

                pnlChapterHeader.Controls.Add(bottomBorder);
                pnlChapterHeader.Controls.Add(lblChapter);
                flowLessons.Controls.Add(pnlChapterHeader);

                // --- KẾT THÚC SỬA PHẦN DESIGN CHƯƠNG ---

                // Phần render bài học giữ nguyên (hoặc dùng CreateExpandableLessonItem như code cũ của bạn)
                foreach (var lesson in chapter.Lessons.OrderBy(l => l.OrderIndex))
                {
                    var isCompleted = await IsLessonCompletedAsync(lesson.LessonId);
                    var isCurrent = lesson.LessonId == _currentLesson.LessonId;

                    var pnlLesson = CreateExpandableLessonItem(lesson, isCompleted, isCurrent);

                    flowLessons.Controls.Add(pnlLesson); // Thêm vào lần 1

                }
            }
            // Add separator
            var separator = new Panel
            {
                Height = 2,
                Width = 320,
                BackColor = Color.FromArgb(230, 230, 230),
                Margin = new Padding(10, 20, 10, 10)
            };
            flowLessons.Controls.Add(separator);

            // Add "Related Content" section
            await LoadRelatedContentAsync();
        }

        private Panel CreateExpandableLessonItem(Lesson lesson, bool isCompleted, bool isCurrent)
        {
            // 1. Dùng FlowLayoutPanel cho container chính để nó tự co giãn chiều cao
            var mainPanel = new FlowLayoutPanel
            {
                Width = 320,
                AutoSize = true, // Tự động giãn chiều cao theo nội dung bên trong
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                FlowDirection = FlowDirection.TopDown, // Xếp các phần tử từ trên xuống
                WrapContents = false,
                BackColor = Color.Transparent,
                Margin = new Padding(5, 2, 5, 2)
            };

            // 2. Header Panel (Phần hiển thị tên bài học)
            var headerPanel = new Panel
            {
                Width = 320, // Khớp với chiều rộng cha
                Height = 80,
                BackColor = isCurrent ? Color.FromArgb(225, 239, 254) : Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Cursor = Cursors.Hand,
                Padding = new Padding(10),
                Margin = new Padding(0), // Không margin để khít với contents
                Tag = false // Lưu trạng thái đang đóng/mở (false = đóng)
            };

            // 3. Các thành phần bên trong Header (Giữ nguyên logic cũ của bạn)
            // Status icon
            var lblStatus = new Label
            {
                Text = isCompleted ? "✓" : "○",
                Location = new Point(15, 15),
                Size = new Size(30, 30),
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = isCompleted ? ColorPalette.Success : ColorPalette.TextSecondary,
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Expand icon
            var lblExpand = new Label
            {
                Text = "▼",
                Location = new Point(285, 15),
                Size = new Size(20, 20),
                Font = new Font("Segoe UI", 10),
                ForeColor = ColorPalette.TextSecondary,
                TextAlign = ContentAlignment.MiddleCenter,
            };

            // Content Type Icon
            var contentIcon = GetContentTypeIcon(lesson);
            var lblContentIcon = new Label
            {
                Text = contentIcon.Icon,
                Location = new Point(15, 50),
                Size = new Size(30, 20),
                Font = new Font("Segoe UI", 10),
                ForeColor = contentIcon.Color,
                TextAlign = ContentAlignment.MiddleLeft
            };

            // Title
            var lblTitle = new Label
            {
                Text = lesson.Title,
                Location = new Point(55, 15),
                Size = new Size(220, 35),
                Font = new Font("Segoe UI", 10, isCurrent ? FontStyle.Bold : FontStyle.Regular),
                ForeColor = isCurrent ? ColorPalette.Primary : ColorPalette.TextPrimary
            };

            // Content Count
            var lblContentType = new Label
            {
                Text = $"{lesson.LessonContents.Count} nội dung",
                Location = new Point(55, 50),
                Size = new Size(150, 20),
                Font = new Font("Segoe UI", 8),
                ForeColor = ColorPalette.TextSecondary
            };

            headerPanel.Controls.AddRange(new Control[] {
                lblStatus, lblExpand, lblContentIcon, lblTitle, lblContentType
            });

            // 4. Contents Panel (Danh sách xổ xuống)
            var contentsPanel = new FlowLayoutPanel
            {
                Width = 320,
                AutoSize = true, // Quan trọng: Tự giãn theo số lượng item con
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                FlowDirection = FlowDirection.TopDown,
                BackColor = Color.FromArgb(248, 249, 250), // Màu nền xám nhẹ
                Padding = new Padding(0),
                Margin = new Padding(0),
                Visible = isCurrent, // Mặc định hiện nếu là bài đang học
                WrapContents = false
            };

            // Add lesson contents items
            var sortedContents = lesson.LessonContents.OrderBy(lc => lc.OrderIndex).ToList();
            for (int i = 0; i < sortedContents.Count; i++)
            {
                var content = sortedContents[i];

                // THAY ĐỔI Ở ĐÂY: Truyền thêm lesson.LessonId vào tham số thứ 3
                var contentItem = CreateContentItem(content, i + 1, lesson.LessonId);

                contentItem.Margin = new Padding(10, 0, 10, 0);
                contentsPanel.Controls.Add(contentItem);
            }

            // 5. Xử lý sự kiện Click để đóng/mở
            EventHandler toggleExpand = (s, e) =>
            {
                bool isExpanded = (bool)headerPanel.Tag;
                bool newExpandedState = !isExpanded;

                // Cập nhật trạng thái
                headerPanel.Tag = newExpandedState;
                contentsPanel.Visible = newExpandedState; // Ẩn/Hiện panel con
                lblExpand.Text = newExpandedState ? "▲" : "▼";

                // QUAN TRỌNG: Vì mainPanel set AutoSize = true, 
                // khi contentsPanel ẩn/hiện, mainPanel sẽ tự động co giãn,
                // và đẩy các bài học phía dưới chạy theo.
            };

            headerPanel.Click += toggleExpand;
            lblTitle.Click += toggleExpand;
            lblExpand.Click += toggleExpand;
            lblStatus.Click += toggleExpand;

            // Khởi tạo trạng thái ban đầu
            if (isCurrent)
            {
                headerPanel.Tag = true;
                lblExpand.Text = "▲";
            }

            // Thêm vào mainPanel (Thứ tự quan trọng: Header trước, Content sau)
            mainPanel.Controls.Add(headerPanel);
            mainPanel.Controls.Add(contentsPanel);

            return mainPanel;
        }

        // Thêm tham số int parentLessonId
        private Panel CreateContentItem(LessonContent content, int index, int parentLessonId)
        {
            var panel = new Panel
            {
                Width = 300,
                Height = 50,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(5, 2, 5, 2),
                Cursor = Cursors.Hand,
                Padding = new Padding(5)
            };

            var (icon, color, label) = GetContentTypeIconFromType(content.ContentType);

            var lblIcon = new Label
            {
                Text = icon,
                Location = new Point(10, 10),
                Size = new Size(25, 25),
                Font = new Font("Segoe UI", 11),
                ForeColor = color,
                TextAlign = ContentAlignment.MiddleCenter,
                Cursor = Cursors.Hand
            };

            var lblTitle = new Label
            {
                Text = content.Title ?? label,
                Location = new Point(45, 5),
                Size = new Size(240, 20),
                Font = new Font("Segoe UI", 9),
                ForeColor = ColorPalette.TextPrimary,
                Cursor = Cursors.Hand
            };

            var lblType = new Label
            {
                Text = label,
                Location = new Point(45, 25),
                Size = new Size(100, 15),
                Font = new Font("Segoe UI", 7),
                ForeColor = ColorPalette.TextSecondary,
                Cursor = Cursors.Hand
            };

            Label lblCheck = null;
            if (IsContentCompleted(content.ContentId))
            {
                lblCheck = new Label
                {
                    Text = "✓",
                    Location = new Point(270, 15),
                    Size = new Size(20, 20),
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    ForeColor = ColorPalette.Success,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Cursor = Cursors.Hand
                };
                panel.Controls.Add(lblCheck);
            }

            panel.Controls.AddRange(new Control[] { lblIcon, lblTitle, lblType });

            // --- LOGIC CLICK MỚI ---
            EventHandler clickHandler = async (s, e) =>
            {
                // Kiểm tra xem có đang ở đúng bài học chứa nội dung này không
                if (_currentLesson != null && _currentLesson.LessonId == parentLessonId)
                {
                    // Nếu đang ở đúng bài -> Chỉ chuyển nội dung (Nhanh hơn)
                    var contentIndex = _currentContents.FindIndex(c => c.ContentId == content.ContentId);
                    if (contentIndex >= 0) await LoadContentAsync(contentIndex);
                }
                else
                {
                    // Nếu đang ở bài khác -> Load bài học mới và nhảy tới nội dung này
                    await LoadLessonAsync(_currentCourse.Slug, parentLessonId, content.ContentId);
                }
            };

            // Gắn sự kiện
            panel.Click += clickHandler;
            lblIcon.Click += clickHandler;
            lblTitle.Click += clickHandler;
            lblType.Click += clickHandler;
            if (lblCheck != null) lblCheck.Click += clickHandler;

            return panel;
        }

        private (string Icon, Color Color, string Label) GetContentTypeIconFromType(string contentType)
        {
            return contentType switch
            {
                "Video" => ("▶️", Color.FromArgb(220, 53, 69), "Video"),
                "Theory" => ("📖", Color.FromArgb(52, 144, 220), "Lý thuyết"),
                "FlashcardSet" => ("🗂️", Color.FromArgb(255, 193, 7), "Flashcard"),
                "Test" => ("✍️", Color.FromArgb(40, 167, 69), "Kiểm tra"),
                _ => ("📄", ColorPalette.TextSecondary, "Nội dung")
            };
        }

        private bool IsContentCompleted(int contentId)
        {
            try
            {
                using var context = new LearningPlatformContext();
                var userId = AuthHelper.CurrentUser?.UserId;
                if (!userId.HasValue) return false;

                var progress = context.CourseProgresses
                    .FirstOrDefault(cp =>
                        cp.UserId == userId.Value &&
                        cp.ContentId == contentId &&
                        cp.IsCompleted);

                return progress != null;
            }
            catch
            {
                return false;
            }
        }
        private async Task<bool> IsLessonCompletedAsync(int lessonId)
        {
            using var context = new LearningPlatformContext();
            var userId = AuthHelper.CurrentUser?.UserId;
            if (!userId.HasValue) return false;

            var lessonContentIds = await context.LessonContents
                .Where(lc => lc.LessonId == lessonId)
                .Select(lc => lc.ContentId)
                .ToListAsync();

            var completedCount = await context.CourseProgresses
                .Where(cp => cp.UserId == userId.Value &&
                            cp.CourseId == _currentCourse.CourseId &&
                            lessonContentIds.Contains(cp.ContentId) &&
                            cp.IsCompleted)
                .CountAsync();

            return completedCount == lessonContentIds.Count;
        }

        private async Task UpdateProgressAsync()
        {
            using var context = new LearningPlatformContext();
            var userId = AuthHelper.CurrentUser?.UserId;
            if (!userId.HasValue) return;

            // --- BƯỚC 1: Lấy tổng số bài học ---
            var allLessonIds = _currentCourse.CourseChapters
                .SelectMany(ch => ch.Lessons)
                .Select(l => l.LessonId)
                .ToList();

            // Lấy ContentId thực tế (tránh đếm ảo)
            var validContentIds = await context.LessonContents
                .Where(lc => allLessonIds.Contains(lc.LessonId))
                .Select(lc => lc.ContentId)
                .ToListAsync();

            var totalContents = validContentIds.Count;

            // --- BƯỚC 2: Đếm số bài đã hoàn thành ---
            // Dùng .Distinct() để tránh lỗi đếm trùng (ví dụ 1 bài lưu 2 lần hoàn thành) gây ra lỗi > 100%
            var completedContents = await context.CourseProgresses
                .Where(cp => cp.UserId == userId.Value &&
                             validContentIds.Contains(cp.ContentId) &&
                             cp.IsCompleted)
                .Select(cp => cp.ContentId)
                .Distinct() // <--- QUAN TRỌNG: Loại bỏ trùng lặp
                .CountAsync();

            // Tính phần trăm
            var progress = totalContents > 0 ? (int)((double)completedContents / totalContents * 100) : 0;

            // --- BƯỚC 3: SỬA LỖI CRASH ---
            // Giới hạn giá trị trong khoảng 0 - 100
            if (progress > 100) progress = 100;
            if (progress < 0) progress = 0;

            // Cập nhật UI
            progressBar.Value = progress;
            lblProgress.Text = $"Tiến độ: {progress}% ({completedContents}/{totalContents} hoàn thành)";
        }

        private async Task LoadContentAsync(int contentIndex)
        {
            if (contentIndex < 0 || contentIndex >= _currentContents.Count)
                return;

            _currentContentIndex = contentIndex;
            var content = _currentContents[contentIndex];

            // Hide all content panels
            pnlVideo.Visible = false;
            pnlTheory.Visible = false;
            pnlFlashcard.Visible = false;
            pnlTest.Visible = false;

            switch (content.ContentType)
            {
                case "Video":
                    await LoadVideoContentAsync(content);
                    break;
                case "Theory":
                    await LoadTheoryContentAsync(content);
                    break;
                case "FlashcardSet":
                    await LoadFlashcardContentAsync(content);
                    break;
                case "Test":
                    await LoadTestContentAsync(content);
                    break;
            }
        }

        #region Video Content

        private async Task LoadVideoContentAsync(LessonContent content)
        {
            pnlVideo.Visible = true;
            _videoView.Visible = true;
            _totalWatchedSeconds = 0;

            if (!string.IsNullOrEmpty(content.VideoUrl))
            {
                // 1. Xử lý đường dẫn từ Database (VD: "Library/Video/ten_file.mp4")
                string dbPath = content.VideoUrl.Replace("/", "\\").TrimStart('\\');

                // 2. ĐƯỜNG DẪN CỐ ĐỊNH (HARDCODED PATH)
                // Lưu ý: Dấu @ đằng trước để nhận diện dấu \ trong chuỗi
                string projectRoot = @"D:\BTL web\BTL web game\APP_Quiz\Quiz_App\WinFormsApp1\WinFormsApp1";

                // Ghép đường dẫn gốc + đường dẫn trong DB
                // Kết quả sẽ là: D:\...\WinFormsApp1\Library\Video\ten_file.mp4
                string fullPath = System.IO.Path.Combine(projectRoot, dbPath);

                // 3. Kiểm tra và chạy video
                if (System.IO.File.Exists(fullPath))
                {
                    // Tìm thấy file -> Chạy
                    using var media = new Media(_libVLC, fullPath, FromType.FromPath);
                    _mediaPlayer.Play(media);

                    // Resume lại đoạn đã xem (nếu có)
                    int watchedSec = await GetWatchedDurationAsync(content.ContentId);
                    if (watchedSec > 0)
                    {
                        _mediaPlayer.Time = (long)watchedSec * 1000;
                    }
                }
                else
                {
                    // Nếu vẫn không thấy -> Báo lỗi chi tiết để bạn biết sai ở đâu
                    MessageBox.Show(
                        $"Không tìm thấy video!\n\n" +
                        $"Đường dẫn phần mềm đang tìm:\n{fullPath}\n\n" +
                        $"Hãy kiểm tra xem file '{System.IO.Path.GetFileName(fullPath)}' có thực sự nằm ở đó không?",
                        "Lỗi File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Sự kiện: Khi video đang chạy (Gọi liên tục khi thời gian thay đổi)
        private void MediaPlayer_TimeChanged(object sender, MediaPlayerTimeChangedEventArgs e)
        {
            // Vì VLC chạy trên luồng riêng, phải dùng Invoke để tương tác với biến của Form
            this.Invoke((MethodInvoker)delegate {
                // e.Time là mili-giây -> chia 1000 ra giây
                int currentSeconds = (int)(e.Time / 1000);

                // Chỉ lưu mỗi 5 giây một lần để đỡ nặng Database
                if (currentSeconds > _totalWatchedSeconds + 5)
                {
                    _totalWatchedSeconds = currentSeconds;
                    _ = SaveVideoProgressAsync(); // Lưu tiến độ
                }
            });
        }

        // Sự kiện: Khi video chạy hết
        private void MediaPlayer_EndReached(object sender, EventArgs e)
        {
            this.Invoke((MethodInvoker)delegate {
                // Đánh dấu hoàn thành 100%
                _ = SaveVideoProgressAsync(forceComplete: true);
                MessageBox.Show("Bạn đã xem hết video!", "Hoàn thành");
            });
        }

        // Hàm lưu tiến độ (Đã chỉnh sửa cho VLC)
        private async Task SaveVideoProgressAsync(bool forceComplete = false)
        {
            try
            {
                var content = _currentContents[_currentContentIndex];
                var userId = AuthHelper.CurrentUser?.UserId;
                if (!userId.HasValue) return;

                // Lấy tổng thời lượng (ms -> sec)
                long lengthMs = _mediaPlayer.Length;
                if (lengthMs <= 0) return; // Video chưa load xong info

                int totalDurationSec = (int)(lengthMs / 1000);

                // Logic hoàn thành: Xem > 90% hoặc video đã chạy hết
                bool isCompleted = forceComplete || (_totalWatchedSeconds >= (totalDurationSec * 0.9));

                using var context = new LearningPlatformContext();
                var progress = await context.CourseProgresses
                    .FirstOrDefaultAsync(cp => cp.UserId == userId.Value && cp.ContentId == content.ContentId);

                if (progress == null)
                {
                    progress = new CourseProgress
                    {
                        UserId = userId.Value,
                        CourseId = _currentCourse.CourseId,
                        LessonId = _currentLesson.LessonId,
                        ContentType = content.ContentType,
                        ContentId = content.ContentId,
                        IsCompleted = isCompleted,
                        DurationSec = _totalWatchedSeconds,
                        LastViewedAt = DateTime.UtcNow
                    };
                    context.CourseProgresses.Add(progress);
                }
                else
                {
                    if (_totalWatchedSeconds > progress.DurationSec)
                        progress.DurationSec = _totalWatchedSeconds;

                    if (!progress.IsCompleted && isCompleted)
                    {
                        progress.IsCompleted = true;
                        progress.CompletionAt = DateTime.UtcNow;
                    }
                    progress.LastViewedAt = DateTime.UtcNow;
                }

                await context.SaveChangesAsync();
                await UpdateProgressAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private async Task<int> GetWatchedDurationAsync(int contentId)
        {
            using var context = new LearningPlatformContext();
            var userId = AuthHelper.CurrentUser?.UserId;
            if (!userId.HasValue) return 0;

            var progress = await context.CourseProgresses
                .FirstOrDefaultAsync(cp =>
                    cp.UserId == userId.Value &&
                    cp.ContentId == contentId);

            return progress?.DurationSec ?? 0;
        }
        
        #endregion

        #region Theory Content

        private async Task LoadTheoryContentAsync(LessonContent content)
        {
            pnlTheory.Visible = true;

            var html = $@"
                <!DOCTYPE html>
                <html>
                <head>
                    <meta charset='utf-8'>
                    <style>
                        body {{ font-family: 'Segoe UI', Arial, sans-serif; padding: 20px; line-height: 1.6; }}
                        h1, h2, h3 {{ color: #3490dc; }}
                        code {{ background: #f4f4f4; padding: 2px 6px; border-radius: 3px; }}
                        pre {{ background: #f4f4f4; padding: 15px; border-radius: 5px; overflow-x: auto; }}
                    </style>
                </head>
                <body>
                    <h2>{content.Title}</h2>
                    {content.Body}
                </body>
                </html>";

            webBrowser.DocumentText = html;

            // Mark as viewed
            await MarkContentViewedAsync(content.ContentId);
        }

        private async Task MarkContentViewedAsync(int contentId)
        {
            try
            {
                var userId = AuthHelper.CurrentUser?.UserId;
                if (!userId.HasValue) return;

                using var context = new LearningPlatformContext();

                var progress = await context.CourseProgresses
                    .FirstOrDefaultAsync(cp =>
                        cp.UserId == userId.Value &&
                        cp.ContentId == contentId);

                if (progress == null)
                {
                    var content = _currentContents.First(c => c.ContentId == contentId);
                    progress = new CourseProgress
                    {
                        UserId = userId.Value,
                        CourseId = _currentCourse.CourseId,
                        LessonId = _currentLesson.LessonId,
                        ContentType = content.ContentType,
                        ContentId = contentId,
                        IsCompleted = true,
                        CompletionAt = DateTime.UtcNow,
                        LastViewedAt = DateTime.UtcNow
                    };
                    context.CourseProgresses.Add(progress);
                }
                else if (!progress.IsCompleted)
                {
                    progress.IsCompleted = true;
                    progress.CompletionAt = DateTime.UtcNow;
                    progress.LastViewedAt = DateTime.UtcNow;
                }

                await context.SaveChangesAsync();
                await UpdateProgressAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error marking content viewed: {ex.Message}");
            }
        }

        #endregion

        #region Flashcard Content

        private async Task LoadFlashcardContentAsync(LessonContent content)
        {
            pnlFlashcard.Visible = true;
            pnlFlashcard.Controls.Clear();
            pnlFlashcard.Dock = DockStyle.Fill;
            pnlFlashcard.BackColor = Color.FromArgb(248, 249, 250);

            if (!content.RefId.HasValue) return;

            using var context = new LearningPlatformContext();
            _flashcards = await context.Flashcards
                .Where(f => f.SetId == content.RefId.Value)
                .OrderBy(f => f.OrderIndex)
                .ToListAsync();

            _currentFlashcardIndex = 0;
            _isFlipped = false;

            // --- 1. TẠO UI THẺ (CARD) - ĐÃ PHÓNG TO ---
            int cardWidth = 1000;  // Tăng từ 600 -> 1000
            int cardHeight = 550;  // Tăng từ 350 -> 550

            _pnlCardFace = new Panel
            {
                Width = cardWidth,
                Height = cardHeight,
                BackColor = Color.White,
                Location = new Point((pnlFlashcard.Width - cardWidth) / 2, 40), // Cách top 40px
                Anchor = AnchorStyles.Top,
                Cursor = Cursors.Hand
            };

            // Vẽ viền bo tròn
            _pnlCardFace.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                var rect = _pnlCardFace.ClientRectangle;
                rect.Width--; rect.Height--;
                using var pen = new Pen(Color.LightGray, 1);
                using var path = GetRoundedPath(rect, 30); // Bo góc tròn hơn (30px)
                e.Graphics.DrawPath(pen, path);
            };
            _pnlCardFace.Click += BtnFlipCard_Click;

            // Label "Mặt trước"
            _lblCardSide = new Label
            {
                Text = "MẶT TRƯỚC",
                ForeColor = Color.Gray,
                Font = new Font("Segoe UI", 10, FontStyle.Bold), // Font to hơn chút
                Location = new Point(30, 25),
                AutoSize = true
            };

            // Label đếm số
            _lblCardCounter = new Label
            {
                Text = $"1/{_flashcards.Count}",
                ForeColor = Color.Gray,
                Font = new Font("Segoe UI", 10, FontStyle.Regular), // Font to hơn chút
                Location = new Point(cardWidth - 60, 25),
                AutoSize = true,
                TextAlign = ContentAlignment.TopRight
            };

            // Label nội dung chính (To nhất)
            _lblCardContent = new Label
            {
                AutoSize = false,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 36, FontStyle.Regular), // Tăng Font từ 24 -> 36
                ForeColor = Color.FromArgb(50, 50, 50),
                Padding = new Padding(20)
            };
            _lblCardContent.Click += BtnFlipCard_Click;

            _pnlCardFace.Controls.Add(_lblCardSide);
            _pnlCardFace.Controls.Add(_lblCardCounter);
            _pnlCardFace.Controls.Add(_lblCardContent);

            // --- 2. TẠO NÚT ĐIỀU KHIỂN (TO HƠN) ---
            var controlsPanel = new Panel
            {
                Width = cardWidth,
                Height = 100, // Tăng chiều cao vùng nút
                Location = new Point((pnlFlashcard.Width - cardWidth) / 2, cardHeight + 60), // Đặt dưới thẻ
                Anchor = AnchorStyles.Top
            };

            // Tính toán vị trí các nút cho cân đối trong chiều rộng 1000px
            int btnFlipWidth = 300;
            int btnNavWidth = 100;
            int btnHeight = 60; // Nút cao hơn cho dễ bấm
            int spacing = 20;

            // Tọa độ X để căn giữa cụm 3 nút
            int centerX = cardWidth / 2;

            // Nút Lật (Nằm giữa)
            _btnFlip = CreateStyledButton("⟳ Lật thẻ", btnFlipWidth, btnHeight, ColorPalette.Primary);
            _btnFlip.Location = new Point(centerX - (btnFlipWidth / 2), 10);
            _btnFlip.Font = new Font("Segoe UI", 12, FontStyle.Bold); // Chữ nút to hơn
            _btnFlip.Click += BtnFlipCard_Click;

            // Nút Trước (Bên trái nút lật)
            _btnPrev = CreateStyledButton("❮", btnNavWidth, btnHeight, Color.Gray);
            _btnPrev.Location = new Point(_btnFlip.Left - btnNavWidth - spacing, 10);
            _btnPrev.Font = new Font("Segoe UI", 14, FontStyle.Bold); // Mũi tên to
            _btnPrev.Click += BtnPrevCard_Click;

            // Nút Sau (Bên phải nút lật)
            _btnNext = CreateStyledButton("❯", btnNavWidth, btnHeight, Color.Gray);
            _btnNext.Location = new Point(_btnFlip.Right + spacing, 10);
            _btnNext.Font = new Font("Segoe UI", 14, FontStyle.Bold); // Mũi tên to
            _btnNext.Click += BtnNextCard_Click;

            controlsPanel.Controls.AddRange(new Control[] { _btnPrev, _btnFlip, _btnNext });

            // --- 3. THÊM VÀO PANEL CHÍNH ---
            pnlFlashcard.Controls.Add(_pnlCardFace);
            pnlFlashcard.Controls.Add(controlsPanel);

            // Xử lý Resize để luôn căn giữa khi phóng to/thu nhỏ cửa sổ
            pnlFlashcard.Resize += (s, e) =>
            {
                _pnlCardFace.Left = (pnlFlashcard.Width - _pnlCardFace.Width) / 2;
                controlsPanel.Left = (pnlFlashcard.Width - controlsPanel.Width) / 2;
                // Cập nhật lại vị trí label đếm số khi resize (nếu cần)
                _lblCardCounter.Left = _pnlCardFace.Width - 60;
            };

            ShowFlashcard();
        }

        // Hàm hỗ trợ tạo nút đẹp
        private Button CreateStyledButton(string text, int w, int h, Color bgColor)
        {
            var btn = new Button
            {
                Text = text,
                Size = new Size(w, h),
                FlatStyle = FlatStyle.Flat,
                BackColor = bgColor,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, w, h, 10, 10)); // Bo tròn nút
            return btn;
        }

        // Import DLL để bo tròn nút (Button Region)
        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        // Hàm vẽ đường dẫn bo tròn (cho Panel)
        private System.Drawing.Drawing2D.GraphicsPath GetRoundedPath(Rectangle rect, int radius)
        {
            var path = new System.Drawing.Drawing2D.GraphicsPath();
            float curveSize = radius * 2F;
            path.StartFigure();
            path.AddArc(rect.X, rect.Y, curveSize, curveSize, 180, 90);
            path.AddArc(rect.Right - curveSize, rect.Y, curveSize, curveSize, 270, 90);
            path.AddArc(rect.Right - curveSize, rect.Bottom - curveSize, curveSize, curveSize, 0, 90);
            path.AddArc(rect.X, rect.Bottom - curveSize, curveSize, curveSize, 90, 90);
            path.CloseFigure();
            return path;
        }

        private void ShowFlashcard()
        {
            if (_flashcards == null || _flashcards.Count == 0) return;

            var card = _flashcards[_currentFlashcardIndex];

            // Cập nhật nội dung
            if (!_isFlipped)
            {
                // Mặt trước
                _lblCardContent.Text = card.FrontText;
                _lblCardSide.Text = "MẶT TRƯỚC";
                _lblCardSide.ForeColor = ColorPalette.Primary; // Màu xanh chủ đạo
                _lblCardContent.ForeColor = Color.Black;
            }
            else
            {
                // Mặt sau
                _lblCardContent.Text = card.BackText;
                _lblCardSide.Text = "MẶT SAU";
                _lblCardSide.ForeColor = Color.FromArgb(40, 167, 69); // Màu xanh lá (kết quả)
                _lblCardContent.ForeColor = Color.FromArgb(50, 50, 50);
            }

            // Cập nhật số đếm
            _lblCardCounter.Text = $"{_currentFlashcardIndex + 1}/{_flashcards.Count}";

            // Cập nhật trạng thái nút
            _btnPrev.Enabled = _currentFlashcardIndex > 0;
            _btnPrev.BackColor = _btnPrev.Enabled ? ColorPalette.Primary : Color.LightGray;

            _btnNext.Enabled = _currentFlashcardIndex < _flashcards.Count - 1;
            _btnNext.BackColor = _btnNext.Enabled ? ColorPalette.Primary : Color.LightGray;

            // Nút hoàn thành (nếu muốn hiển thị ở thẻ cuối khi đã lật)
            if (_currentFlashcardIndex == _flashcards.Count - 1 && _isFlipped)
            {
                // Bạn có thể thay đổi nút Next thành nút Hoàn thành hoặc thêm nút mới
                _btnNext.Text = "✓";
                _btnNext.BackColor = ColorPalette.Success;
                _btnNext.Enabled = true;

                // Gỡ event cũ và gán event hoàn thành tạm thời (cần xử lý kỹ hơn nếu muốn chuẩn)
                // Ở đây để đơn giản ta dùng nút btnCompleteFlashcard cũ nếu nó có trong form,
                // hoặc hiển thị nó lên
                btnCompleteFlashcard.Visible = true;
                btnCompleteFlashcard.Location = new Point(_btnFlip.Right + 20, _btnFlip.Top);
                // Add vào panel controlsPanel nếu cần
            }
            else
            {
                _btnNext.Text = "❯";
                btnCompleteFlashcard.Visible = false;
            }
        }

        private void BtnFlipCard_Click(object sender, EventArgs e)
        {
            _isFlipped = !_isFlipped;
            ShowFlashcard();
        }

        private void BtnPrevCard_Click(object sender, EventArgs e)
        {
            if (_currentFlashcardIndex > 0)
            {
                _currentFlashcardIndex--;
                _isFlipped = false;
                ShowFlashcard();
            }
        }

        private async void BtnNextCard_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra xem nút có phải đang là nút "Hoàn thành" (dấu tích) không
            if (_btnNext.Text == "✓")
            {
                // Lấy nội dung bài học hiện tại
                var content = _currentContents[_currentContentIndex];

                // Gọi hàm lưu vào Database
                await MarkContentCompleteAsync(content.ContentId);

                // Thông báo thành công
                MessageBox.Show("Đã hoàn thành luyện tập flashcard!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 2. Logic chuyển thẻ cũ (giữ nguyên)
            if (_currentFlashcardIndex < _flashcards.Count - 1)
            {
                _currentFlashcardIndex++;
                _isFlipped = false;
                ShowFlashcard();
            }
        }

        private async void BtnCompleteFlashcard_Click(object sender, EventArgs e)
        {
            var content = _currentContents[_currentContentIndex];
            await MarkContentCompleteAsync(content.ContentId);
            MessageBox.Show("Đã hoàn thành luyện tập flashcard!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

        #region Test Content

        private async Task LoadTestContentAsync(LessonContent content)
        {
            pnlTest.Visible = true;

            if (!content.RefId.HasValue) return;

            using var context = new LearningPlatformContext();
            _currentTest = await context.Tests
                .Include(t => t.Questions.OrderBy(q => q.OrderIndex))
                    .ThenInclude(q => q.QuestionOptions.OrderBy(o => o.OrderIndex))
                .FirstOrDefaultAsync(t => t.TestId == content.RefId.Value);

            if (_currentTest == null) return;

            lblTestTitle.Text = _currentTest.Title;
            _questions = _currentTest.Questions.OrderBy(q => q.OrderIndex).ToList();
            _selectedAnswers.Clear();
            _testStartTime = DateTime.UtcNow;

            LoadQuestions();
        }

        private void LoadQuestions()
        {
            flowQuestions.Controls.Clear();

            for (int i = 0; i < _questions.Count; i++)
            {
                var question = _questions[i];
                var questionPanel = CreateQuestionPanel(question, i + 1);
                flowQuestions.Controls.Add(questionPanel);
            }
        }

        private Panel CreateQuestionPanel(Question question, int number)
        {
            // 1. Panel chính của câu hỏi (Rộng hơn, thoáng hơn)
            var panel = new Panel
            {
                Width = 1100, // Tăng chiều rộng để tận dụng màn hình
                AutoSize = true,
                BackColor = Color.White,
                Margin = new Padding(0, 0, 0, 20), // Khoảng cách giữa các câu hỏi
                Padding = new Padding(20) // Khoảng đệm bên trong
            };

            // 2. Tiêu đề câu hỏi (Ví dụ: "Câu 1: ...")
            var lblQuestionNumber = new Label
            {
                Text = $"Câu {number}",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = ColorPalette.Primary, // Màu xanh thương hiệu
                AutoSize = true,
                Location = new Point(20, 20)
            };

            var lblQuestionText = new Label
            {
                Text = question.StemText,
                Font = new Font("Segoe UI", 12, FontStyle.Regular),
                ForeColor = Color.Black,
                AutoSize = true,
                MaximumSize = new Size(1000, 0), // Tự xuống dòng nếu quá dài
                Location = new Point(20, 50) // Nằm dưới số câu
            };

            panel.Controls.Add(lblQuestionNumber);
            panel.Controls.Add(lblQuestionText);

            int yPos = lblQuestionText.Bottom + 20;

            // 3. Hiển thị hướng dẫn (nếu là câu chọn nhiều)
            if (question.Type == "MCQ_Multi" || question.Type == "TrueFalse")
            {
                // Khởi tạo list nếu chưa có
                if (!_selectedAnswers.ContainsKey(question.QuestionId))
                    _selectedAnswers[question.QuestionId] = new List<int>();

                string guideText = question.Type == "MCQ_Multi" ? "(Chọn nhiều đáp án)" : "";
                if (!string.IsNullOrEmpty(guideText))
                {
                    var lblGuide = new Label
                    {
                        Text = guideText,
                        Font = new Font("Segoe UI", 10, FontStyle.Italic),
                        ForeColor = Color.Gray,
                        Location = new Point(80, 23), // Nằm cạnh số câu
                        AutoSize = true
                    };
                    panel.Controls.Add(lblGuide);
                }

                // Render Checkbox (Thiết kế phẳng, to dễ bấm)
                foreach (var option in question.QuestionOptions.OrderBy(o => o.OrderIndex))
                {
                    CheckBox chk = new CheckBox
                    {
                        Text = option.OptionText,
                        Font = new Font("Segoe UI", 11),
                        AutoSize = false,
                        Width = 1000,
                        Height = 40, // Tăng chiều cao để dễ click
                        Location = new Point(40, yPos),
                        Tag = option.OptionId,
                        Cursor = Cursors.Hand,
                        Padding = new Padding(10, 0, 0, 0) // Cách lề chữ ra chút
                    };

                    // Hiệu ứng Hover
                    chk.MouseEnter += (s, e) => chk.BackColor = Color.FromArgb(240, 248, 255); // Xanh nhạt
                    chk.MouseLeave += (s, e) => chk.BackColor = Color.White;

                    chk.CheckedChanged += (s, e) =>
                    {
                        if (chk.Checked)
                        {
                            if (!_selectedAnswers[question.QuestionId].Contains(option.OptionId))
                                _selectedAnswers[question.QuestionId].Add(option.OptionId);
                        }
                        else
                        {
                            _selectedAnswers[question.QuestionId].Remove(option.OptionId);
                        }
                    };

                    panel.Controls.Add(chk);
                    yPos += 45; // Khoảng cách giữa các đáp án
                }
            }
            else // Render RadioButton (Chọn 1)
            {
                if (!_selectedAnswers.ContainsKey(question.QuestionId))
                    _selectedAnswers[question.QuestionId] = new List<int>();

                foreach (var option in question.QuestionOptions.OrderBy(o => o.OrderIndex))
                {
                    RadioButton radio = new RadioButton
                    {
                        Text = option.OptionText,
                        Font = new Font("Segoe UI", 11),
                        AutoSize = false,
                        Width = 1000,
                        Height = 40,
                        Location = new Point(40, yPos),
                        Tag = option.OptionId,
                        Cursor = Cursors.Hand,
                        Padding = new Padding(10, 0, 0, 0)
                    };

                    // Hiệu ứng Hover
                    radio.MouseEnter += (s, e) => radio.BackColor = Color.FromArgb(240, 248, 255);
                    radio.MouseLeave += (s, e) => radio.BackColor = Color.White;

                    radio.CheckedChanged += (s, e) =>
                    {
                        if (radio.Checked)
                        {
                            _selectedAnswers[question.QuestionId].Clear();
                            _selectedAnswers[question.QuestionId].Add(option.OptionId);
                        }
                    };

                    panel.Controls.Add(radio);
                    yPos += 45;
                }
            }

            // 4. Đường kẻ phân cách mờ bên dưới mỗi câu hỏi
            Panel separator = new Panel
            {
                Height = 1,
                Width = 1060,
                BackColor = Color.FromArgb(230, 230, 230), // Màu xám rất nhạt
                Location = new Point(20, yPos + 10)
            };
            panel.Controls.Add(separator);

            return panel;
        }

        private async void BtnSubmitTest_Click(object sender, EventArgs e)
        {
            if (_selectedAnswers.Count < _questions.Count)
            {
                var result = MessageBox.Show(
                    $"Bạn chưa trả lời {_selectedAnswers.Count}/{_questions.Count} câu. Bạn có chắc muốn nộp bài?",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes) return;
            }

            await SubmitTestAsync();
        }

        private async Task SubmitTestAsync()
        {
            try
            {
                var userId = AuthHelper.CurrentUser?.UserId;
                if (!userId.HasValue) return;

                using var context = new LearningPlatformContext();
                var timeSpent = (int)(DateTime.UtcNow - _testStartTime).TotalSeconds;

                decimal totalScore = 0;
                decimal maxScore = _currentTest.MaxScore ?? _questions.Sum(q => q.Points);

                // --- LOGIC CHẤM ĐIỂM MỚI ---
                foreach (var question in _questions)
                {
                    // Lấy danh sách các đáp án ĐÚNG trong DB của câu này
                    var correctOptionIds = question.QuestionOptions
                        .Where(o => o.IsCorrect)
                        .Select(o => o.OptionId)
                        .ToList();

                    // Lấy danh sách đáp án NGƯỜI DÙNG chọn
                    if (_selectedAnswers.TryGetValue(question.QuestionId, out List<int> userSelectedIds))
                    {
                        // Kiểm tra:
                        // 1. Số lượng chọn phải bằng số lượng đáp án đúng
                        // 2. Không được chứa đáp án sai
                        // 3. Phải chứa tất cả đáp án đúng

                        bool isCorrect = false;

                        if (userSelectedIds.Count == correctOptionIds.Count &&
                            !userSelectedIds.Except(correctOptionIds).Any())
                        {
                            isCorrect = true;
                        }

                        if (isCorrect)
                        {
                            totalScore += question.Points;
                        }
                    }
                }
                // -----------------------------

                // Tạo lịch sử làm bài
                var attempt = new TestAttempt
                {
                    TestId = _currentTest.TestId,
                    UserId = userId.Value,
                    StartedAt = _testStartTime,
                    SubmittedAt = DateTime.UtcNow,
                    Status = "Graded",
                    TimeSpentSec = timeSpent,
                    Score = totalScore,
                    MaxScore = maxScore
                };

                context.TestAttempts.Add(attempt);
                await context.SaveChangesAsync();

                // Lưu chi tiết từng câu trả lời
                foreach (var kvp in _selectedAnswers)
                {
                    if (kvp.Value != null && kvp.Value.Count > 0)
                    {
                        // Với câu nhiều đáp án, ta lưu dạng chuỗi JSON hoặc nối chuỗi: "1,5,9"
                        string answerString = string.Join(",", kvp.Value);

                        // Kiểm tra đúng sai để lưu vào DB
                        var question = _questions.First(q => q.QuestionId == kvp.Key);
                        var correctOptionIds = question.QuestionOptions.Where(o => o.IsCorrect).Select(o => o.OptionId).ToList();
                        bool isCorrect = kvp.Value.Count == correctOptionIds.Count && !kvp.Value.Except(correctOptionIds).Any();

                        var answer = new AttemptAnswer
                        {
                            AttemptId = attempt.AttemptId,
                            QuestionId = kvp.Key,
                            AnswerPayload = $"{{\"selectedOptions\": [{answerString}]}}", // Lưu dạng JSON cho chuyên nghiệp
                            IsCorrect = isCorrect,
                            Score = isCorrect ? question.Points : 0,
                            GradedAt = DateTime.UtcNow
                        };
                        context.AttemptAnswers.Add(answer);
                    }
                }

                await context.SaveChangesAsync();

                // Mark complete content... (Giữ nguyên code cũ)
                var content = _currentContents[_currentContentIndex];
                await MarkContentCompleteAsync(content.ContentId, totalScore);

                // Show result
                var percentage = maxScore > 0 ? (totalScore / maxScore) * 100 : 0;
                MessageBox.Show(
                    $"Kết quả bài kiểm tra:\n\n" +
                    $"Điểm: {totalScore}/{maxScore} ({percentage:F1}%)\n" +
                    $"Thời gian: {timeSpent / 60} phút {timeSpent % 60} giây",
                    "Kết quả", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi nộp bài: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Navigation

        private async void BtnPrevLesson_Click(object sender, EventArgs e)
        {
            var allLessons = _currentCourse.CourseChapters
                .OrderBy(ch => ch.OrderIndex)
                .SelectMany(ch => ch.Lessons.OrderBy(l => l.OrderIndex))
                .ToList();

            var currentIndex = allLessons.FindIndex(l => l.LessonId == _currentLesson.LessonId);
            if (currentIndex > 0)
            {
                await LoadLessonAsync(_currentCourse.Slug, allLessons[currentIndex - 1].LessonId);
            }
        }

        private async void BtnNextLesson_Click(object sender, EventArgs e)
        {
            var allLessons = _currentCourse.CourseChapters
                .OrderBy(ch => ch.OrderIndex)
                .SelectMany(ch => ch.Lessons.OrderBy(l => l.OrderIndex))
                .ToList();

            var currentIndex = allLessons.FindIndex(l => l.LessonId == _currentLesson.LessonId);
            if (currentIndex < allLessons.Count - 1)
            {
                await LoadLessonAsync(_currentCourse.Slug, allLessons[currentIndex + 1].LessonId);
            }
        }

        private async void BtnMarkComplete_Click(object sender, EventArgs e)
        {
            var content = _currentContents[_currentContentIndex];
            await MarkContentCompleteAsync(content.ContentId);
            MessageBox.Show("Đã đánh dấu hoàn thành!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async Task MarkContentCompleteAsync(int contentId, decimal? score = null)
        {
            try
            {
                var userId = AuthHelper.CurrentUser?.UserId;
                if (!userId.HasValue) return;

                using var context = new LearningPlatformContext();

                var progress = await context.CourseProgresses
                    .FirstOrDefaultAsync(cp =>
                        cp.UserId == userId.Value &&
                        cp.ContentId == contentId);

                var content = _currentContents.First(c => c.ContentId == contentId);

                if (progress == null)
                {
                    progress = new CourseProgress
                    {
                        UserId = userId.Value,
                        CourseId = _currentCourse.CourseId,
                        LessonId = _currentLesson.LessonId,
                        ContentType = content.ContentType,
                        ContentId = contentId,
                        IsCompleted = true,
                        CompletionAt = DateTime.UtcNow,
                        LastViewedAt = DateTime.UtcNow,
                        Score = score
                    };
                    context.CourseProgresses.Add(progress);
                }
                else if (!progress.IsCompleted)
                {
                    progress.IsCompleted = true;
                    progress.CompletionAt = DateTime.UtcNow;
                    progress.Score = score;
                }

                await context.SaveChangesAsync();
                await UpdateProgressAsync();
                await LoadSidebarAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error marking content complete: {ex.Message}");
            }
        }

        #endregion

        private async Task LoadRelatedContentAsync()
        {
            var lblRelatedTitle = new Label
            {
                Text = "Nội dung khác",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = ColorPalette.TextPrimary,
                AutoSize = true,
                Padding = new Padding(10, 10, 10, 10),
                Width = 320
            };
            flowLessons.Controls.Add(lblRelatedTitle);

            using var context = new LearningPlatformContext();

            // Get related courses from the same category - Fix LINQ
            var relatedCourses = await context.Courses
                .Include(c => c.CourseChapters)
                    .ThenInclude(ch => ch.Lessons)
                .Where(c => c.CategoryId == _currentCourse.CategoryId &&
                           c.CourseId != _currentCourse.CourseId &&
                           c.IsPublished)
                .OrderByDescending(c => c.AverageRating)
                .Take(3)
                .ToListAsync();

            foreach (var course in relatedCourses)
            {
                var relatedCard = CreateRelatedCourseCard(course);
                flowLessons.Controls.Add(relatedCard);
            }
        }

        private Panel CreateRelatedCourseCard(Course course)
        {
            var panel = new Panel
            {
                Width = 320,
                Height = 120,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(10, 5, 10, 5),
                Cursor = Cursors.Hand
            };

            // Icon/Image placeholder
            var lblIcon = new Label
            {
                Text = "📚",
                Location = new Point(10, 10),
                Size = new Size(40, 40),
                Font = new Font("Segoe UI", 20),
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Course title
            var lblTitle = new Label
            {
                Text = course.Title,
                Location = new Point(60, 10),
                Size = new Size(240, 40),
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = ColorPalette.TextPrimary
            };

            // Rating
            var lblRating = new Label
            {
                Text = $"⭐ {course.AverageRating:F1} ({course.TotalReviews} đánh giá)",
                Location = new Point(60, 55),
                Size = new Size(240, 20),
                Font = new Font("Segoe UI", 8),
                ForeColor = ColorPalette.TextSecondary
            };

            // Button
            var btnView = new Button
            {
                Text = "Xem khóa học",
                Location = new Point(60, 80),
                Size = new Size(240, 30),
                BackColor = ColorPalette.ButtonSecondary,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9),
                Cursor = Cursors.Hand
            };
            btnView.FlatAppearance.BorderSize = 0;
            btnView.Click += (s, e) => OnRelatedCourseClick(course);

            panel.Controls.AddRange(new Control[] { lblIcon, lblTitle, lblRating, btnView });
            panel.Click += (s, e) => OnRelatedCourseClick(course);
            lblTitle.Click += (s, e) => OnRelatedCourseClick(course);

            return panel;
        }

        private void OnRelatedCourseClick(Course course)
        {
            var result = MessageBox.Show(
                $"Bạn có muốn chuyển sang khóa học:\n\n{course.Title}?",
                "Chuyển khóa học",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Navigate to course detail or first lesson
                using var context = new LearningPlatformContext();
                var firstLesson = context.Lessons
                    .Where(l => l.Chapter.CourseId == course.CourseId)
                    .OrderBy(l => l.Chapter.OrderIndex)
                    .ThenBy(l => l.OrderIndex)
                    .FirstOrDefault();

                if (firstLesson != null)
                {
                    _ = LoadLessonAsync(course.Slug, firstLesson.LessonId);
                }
            }
        }

        private Panel CreateLessonItem(Lesson lesson, bool isCompleted, bool isCurrent)
        {
            var panel = new Panel
            {
                Width = 320,
                Height = 80,
                BackColor = isCurrent ? Color.FromArgb(225, 239, 254) : Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Margin = new Padding(5),
                Cursor = Cursors.Hand,
                Padding = new Padding(10)
            };

            // Status icon (left side)
            var lblStatus = new Label
            {
                Text = isCompleted ? "✓" : "○",
                Location = new Point(15, 15),
                Size = new Size(30, 30),
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = isCompleted ? ColorPalette.Success : ColorPalette.TextSecondary,
                TextAlign = ContentAlignment.MiddleCenter
            };

            // Content type icon
            var contentIcon = GetContentTypeIcon(lesson);
            var lblContentIcon = new Label
            {
                Text = contentIcon.Icon,
                Location = new Point(15, 50),
                Size = new Size(30, 20),
                Font = new Font("Segoe UI", 10),
                ForeColor = contentIcon.Color,
                TextAlign = ContentAlignment.MiddleLeft
            };

            // Lesson title
            var lblTitle = new Label
            {
                Text = lesson.Title,
                Location = new Point(55, 15),
                Size = new Size(250, 35),
                Font = new Font("Segoe UI", 10, isCurrent ? FontStyle.Bold : FontStyle.Regular),
                ForeColor = isCurrent ? ColorPalette.Primary : ColorPalette.TextPrimary
            };

            // Content type label
            var lblContentType = new Label
            {
                Text = contentIcon.Label,
                Location = new Point(55, 50),
                Size = new Size(150, 20),
                Font = new Font("Segoe UI", 8),
                ForeColor = ColorPalette.TextSecondary
            };

            // Duration/Info label (if available)
            var lblDuration = new Label
            {
                Text = GetLessonDuration(lesson),
                Location = new Point(210, 50),
                Size = new Size(95, 20),
                Font = new Font("Segoe UI", 8),
                ForeColor = ColorPalette.TextSecondary,
                TextAlign = ContentAlignment.MiddleRight
            };

            panel.Controls.AddRange(new Control[] {
                lblStatus, lblContentIcon, lblTitle, lblContentType, lblDuration
            });

            panel.Click += async (s, e) => await LoadLessonAsync(_currentCourse.Slug, lesson.LessonId);
            lblTitle.Click += async (s, e) => await LoadLessonAsync(_currentCourse.Slug, lesson.LessonId);

            return panel;
        }

        private (string Icon, Color Color, string Label) GetContentTypeIcon(Lesson lesson)
        {
            // Get first content type
            var firstContent = lesson.LessonContents.OrderBy(lc => lc.OrderIndex).FirstOrDefault();

            if (firstContent == null)
                return ("📄", ColorPalette.TextSecondary, "Nội dung");

            return firstContent.ContentType switch
            {
                "Video" => ("▶️", Color.FromArgb(220, 53, 69), "Video"),
                "Theory" => ("📖", Color.FromArgb(52, 144, 220), "Lý thuyết"),
                "FlashcardSet" => ("🗂️", Color.FromArgb(255, 193, 7), "Flashcard"),
                "Test" => ("✍️", Color.FromArgb(40, 167, 69), "Kiểm tra"),
                _ => ("📄", ColorPalette.TextSecondary, "Nội dung")
            };
        }

        private string GetLessonDuration(Lesson lesson)
        {
            // Calculate total duration based on content
            var totalContents = lesson.LessonContents.Count;

            if (totalContents == 0)
                return "";

            if (totalContents == 1)
            {
                var content = lesson.LessonContents.First();
                return content.ContentType switch
                {
                    "Video" => "~10 phút",
                    "Theory" => "~5 phút",
                    "FlashcardSet" => "~15 phút",
                    "Test" => "~20 phút",
                    _ => ""
                };
            }

            return $"{totalContents} nội dung";
        }

        private void lblCourseTitle_Click(object sender, EventArgs e)
        {

        }

        private void pnlMain_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblVideoPlaceholder_Click(object sender, EventArgs e)
        {

        }

        private void pnlHeader_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
