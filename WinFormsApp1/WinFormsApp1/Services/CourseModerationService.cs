using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using WinFormsApp1.Models.EF;
using WinFormsApp1.Models.Entities;

namespace WinFormsApp1.Services
{
    /// <summary>
    /// Service xử lý kiểm duyệt khóa học tự động và thủ công
    /// </summary>
    public class CourseModerationService
    {
        /// <summary>
        /// Kết quả kiểm tra tự động
        /// </summary>
        public class AutoCheckResult
        {
            public bool Passed { get; set; }
            public string CheckName { get; set; }
            public string Message { get; set; }
            public string Severity { get; set; } // Info, Warning, Error
        }

        /// <summary>
        /// Kiểm tra tự động các tiêu chí cơ bản của khóa học
        /// </summary>
        public static List<AutoCheckResult> RunAutoChecks(Course course, LearningPlatformContext context)
        {
            var results = new List<AutoCheckResult>();

            // Load full course data with chapters and lessons
            var fullCourse = context.Courses
                .Include(c => c.CourseChapters)
                    .ThenInclude(ch => ch.Lessons)
                        .ThenInclude(l => l.LessonContents)
                .FirstOrDefault(c => c.CourseId == course.CourseId);

            if (fullCourse == null) return results;

            // 1. Kiểm tra tiêu đề
            if (string.IsNullOrWhiteSpace(fullCourse.Title))
            {
                results.Add(new AutoCheckResult
                {
                    Passed = false,
                    CheckName = "Tiêu đề",
                    Message = "Khóa học phải có tiêu đề",
                    Severity = "Error"
                });
            }
            else if (fullCourse.Title.Length < 10)
            {
                results.Add(new AutoCheckResult
                {
                    Passed = false,
                    CheckName = "Tiêu đề",
                    Message = "Tiêu đề quá ngắn (tối thiểu 10 ký tự)",
                    Severity = "Warning"
                });
            }
            else
            {
                results.Add(new AutoCheckResult
                {
                    Passed = true,
                    CheckName = "Tiêu đề",
                    Message = "Tiêu đề hợp lệ",
                    Severity = "Info"
                });
            }

            // 2. Kiểm tra mô tả
            if (string.IsNullOrWhiteSpace(fullCourse.Summary))
            {
                results.Add(new AutoCheckResult
                {
                    Passed = false,
                    CheckName = "Mô tả",
                    Message = "Khóa học cần có mô tả",
                    Severity = "Error"
                });
            }
            else if (fullCourse.Summary.Length < 50)
            {
                results.Add(new AutoCheckResult
                {
                    Passed = false,
                    CheckName = "Mô tả",
                    Message = "Mô tả quá ngắn (tối thiểu 50 ký tự)",
                    Severity = "Warning"
                });
            }
            else
            {
                results.Add(new AutoCheckResult
                {
                    Passed = true,
                    CheckName = "Mô tả",
                    Message = "Mô tả hợp lệ",
                    Severity = "Info"
                });
            }

            // 3. Kiểm tra ảnh bìa
            if (string.IsNullOrWhiteSpace(fullCourse.CoverUrl))
            {
                results.Add(new AutoCheckResult
                {
                    Passed = false,
                    CheckName = "Ảnh bìa",
                    Message = "Khóa học cần có ảnh bìa",
                    Severity = "Warning"
                });
            }
            else
            {
                results.Add(new AutoCheckResult
                {
                    Passed = true,
                    CheckName = "Ảnh bìa",
                    Message = "Có ảnh bìa",
                    Severity = "Info"
                });
            }

            // 4. Kiểm tra danh mục
            if (!fullCourse.CategoryId.HasValue)
            {
                results.Add(new AutoCheckResult
                {
                    Passed = false,
                    CheckName = "Danh mục",
                    Message = "Khóa học cần được phân loại",
                    Severity = "Warning"
                });
            }
            else
            {
                results.Add(new AutoCheckResult
                {
                    Passed = true,
                    CheckName = "Danh mục",
                    Message = "Đã phân loại",
                    Severity = "Info"
                });
            }

            // 5. Kiểm tra giá
            if (fullCourse.Price < 0)
            {
                results.Add(new AutoCheckResult
                {
                    Passed = false,
                    CheckName = "Giá",
                    Message = "Giá không được âm",
                    Severity = "Error"
                });
            }
            else if (fullCourse.Price == 0)
            {
                results.Add(new AutoCheckResult
                {
                    Passed = true,
                    CheckName = "Giá",
                    Message = "Khóa học miễn phí",
                    Severity = "Info"
                });
            }
            else
            {
                results.Add(new AutoCheckResult
                {
                    Passed = true,
                    CheckName = "Giá",
                    Message = $"Giá: {fullCourse.Price:N0} VNĐ",
                    Severity = "Info"
                });
            }

            // 6. Kiểm tra số chương
            var chapterCount = fullCourse.CourseChapters.Count;
            if (chapterCount == 0)
            {
                results.Add(new AutoCheckResult
                {
                    Passed = false,
                    CheckName = "Nội dung",
                    Message = "Khóa học phải có ít nhất 1 chương",
                    Severity = "Error"
                });
            }
            else if (chapterCount < 3)
            {
                results.Add(new AutoCheckResult
                {
                    Passed = false,
                    CheckName = "Nội dung",
                    Message = $"Chỉ có {chapterCount} chương (nên có ít nhất 3)",
                    Severity = "Warning"
                });
            }
            else
            {
                results.Add(new AutoCheckResult
                {
                    Passed = true,
                    CheckName = "Nội dung",
                    Message = $"Có {chapterCount} chương",
                    Severity = "Info"
                });
            }

            // 7. Kiểm tra số bài học
            var lessonCount = fullCourse.CourseChapters.Sum(ch => ch.Lessons.Count);
            if (lessonCount == 0)
            {
                results.Add(new AutoCheckResult
                {
                    Passed = false,
                    CheckName = "Bài học",
                    Message = "Khóa học phải có ít nhất 1 bài học",
                    Severity = "Error"
                });
            }
            else if (lessonCount < 5)
            {
                results.Add(new AutoCheckResult
                {
                    Passed = false,
                    CheckName = "Bài học",
                    Message = $"Chỉ có {lessonCount} bài học (nên có ít nhất 5)",
                    Severity = "Warning"
                });
            }
            else
            {
                results.Add(new AutoCheckResult
                {
                    Passed = true,
                    CheckName = "Bài học",
                    Message = $"Có {lessonCount} bài học",
                    Severity = "Info"
                });
            }

            // 8. Kiểm tra nội dung bài học
            var contentCount = fullCourse.CourseChapters
                .SelectMany(ch => ch.Lessons)
                .Sum(l => l.LessonContents.Count);

            if (contentCount == 0)
            {
                results.Add(new AutoCheckResult
                {
                    Passed = false,
                    CheckName = "Nội dung bài học",
                    Message = "Các bài học cần có nội dung (video, lý thuyết, etc.)",
                    Severity = "Error"
                });
            }
            else if (contentCount < lessonCount)
            {
                results.Add(new AutoCheckResult
                {
                    Passed = false,
                    CheckName = "Nội dung bài học",
                    Message = $"Một số bài học chưa có nội dung ({contentCount}/{lessonCount})",
                    Severity = "Warning"
                });
            }
            else
            {
                results.Add(new AutoCheckResult
                {
                    Passed = true,
                    CheckName = "Nội dung bài học",
                    Message = $"Tất cả bài học đều có nội dung ({contentCount} nội dung)",
                    Severity = "Info"
                });
            }

            // 9. Kiểm tra từ khóa nhạy cảm trong tiêu đề/mô tả
            var bannedWords = new[] { "scam", "lừa đảo", "hack", "crack", "cheat" };
            var textToCheck = $"{fullCourse.Title} {fullCourse.Summary}".ToLower();
            var foundBannedWords = bannedWords.Where(w => textToCheck.Contains(w)).ToList();

            if (foundBannedWords.Any())
            {
                results.Add(new AutoCheckResult
                {
                    Passed = false,
                    CheckName = "Nội dung nhạy cảm",
                    Message = $"Phát hiện từ khóa nhạy cảm: {string.Join(", ", foundBannedWords)}",
                    Severity = "Error"
                });
            }
            else
            {
                results.Add(new AutoCheckResult
                {
                    Passed = true,
                    CheckName = "Nội dung nhạy cảm",
                    Message = "Không phát hiện từ khóa nhạy cảm",
                    Severity = "Info"
                });
            }

            return results;
        }

        /// <summary>
        /// Tính điểm tự động cho khóa học (0-100)
        /// </summary>
        public static int CalculateAutoScore(List<AutoCheckResult> results)
        {
            if (!results.Any()) return 0;

            var errorCount = results.Count(r => r.Severity == "Error" && !r.Passed);
            var warningCount = results.Count(r => r.Severity == "Warning" && !r.Passed);
            var totalChecks = results.Count;

            // Mỗi error -15 điểm, mỗi warning -5 điểm
            var score = 100 - (errorCount * 15) - (warningCount * 5);
            return Math.Max(0, Math.Min(100, score));
        }

        /// <summary>
        /// Kiểm tra xem khóa học có đủ điều kiện publish không
        /// </summary>
        public static bool CanPublish(List<AutoCheckResult> results)
        {
            // Không được có lỗi Error
            return !results.Any(r => r.Severity == "Error" && !r.Passed);
        }

        /// <summary>
        /// Gửi khóa học để kiểm duyệt
        /// </summary>
        public static bool SubmitForReview(int courseId, LearningPlatformContext context)
        {
            try
            {
                var course = context.Courses.Find(courseId);
                if (course == null) return false;

                // Chạy kiểm tra tự động
                var autoCheckResults = RunAutoChecks(course, context);
                var autoScore = CalculateAutoScore(autoCheckResults);

                // Lưu kết quả kiểm tra
                course.AutoCheckResults = JsonSerializer.Serialize(autoCheckResults);
                course.ModerationStatus = "Pending";
                course.SubmittedForReviewAt = DateTime.UtcNow;
                course.UpdatedAt = DateTime.UtcNow;

                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Admin phê duyệt khóa học
        /// </summary>
        public static bool ApproveCourse(int courseId, int adminUserId, LearningPlatformContext context)
        {
            try
            {
                var course = context.Courses.Find(courseId);
                if (course == null) return false;

                course.ModerationStatus = "Approved";
                course.ReviewedBy = adminUserId;
                course.ReviewedAt = DateTime.UtcNow;
                course.RejectionReason = null;
                course.IsPublished = true;
                course.UpdatedAt = DateTime.UtcNow;

                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Admin từ chối khóa học
        /// </summary>
        public static bool RejectCourse(int courseId, int adminUserId, string reason, LearningPlatformContext context)
        {
            try
            {
                var course = context.Courses.Find(courseId);
                if (course == null) return false;

                course.ModerationStatus = "Rejected";
                course.ReviewedBy = adminUserId;
                course.ReviewedAt = DateTime.UtcNow;
                course.RejectionReason = reason;
                course.IsPublished = false;
                course.UpdatedAt = DateTime.UtcNow;

                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Yêu cầu sửa đổi khóa học
        /// </summary>
        public static bool RequestRevision(int courseId, int adminUserId, string reason, LearningPlatformContext context)
        {
            try
            {
                var course = context.Courses.Find(courseId);
                if (course == null) return false;

                course.ModerationStatus = "NeedsRevision";
                course.ReviewedBy = adminUserId;
                course.ReviewedAt = DateTime.UtcNow;
                course.RejectionReason = reason;
                course.IsPublished = false;
                course.UpdatedAt = DateTime.UtcNow;

                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
