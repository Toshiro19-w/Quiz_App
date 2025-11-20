using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using WinFormsApp1.Models.EF;
using WinFormsApp1.Models.Entities;
using WinFormsApp1.ViewModels;

namespace WinFormsApp1.Controllers
{
    public class AdminController
    {
        // Dashboard Statistics
        public async Task<DashboardStats> GetDashboardStatsAsync()
        {
            using (var context = new LearningPlatformContext())
            {
                return new DashboardStats
                {
                    TotalUsers = await context.Users.CountAsync(),
                    TotalCourses = await context.Courses.CountAsync(),
                    TotalClasses = await context.CourseChapters.CountAsync(),
                    TotalTests = await context.Tests.CountAsync(),
                    TotalRevenue = await context.Payments.AnyAsync() ? await context.Payments.SumAsync(p => p.Amount) : 0,
                    TotalTestResults = await context.TestAttempts.CountAsync()
                };
            }
        }

        // User Management
        public async Task<List<User>> GetUsersAsync()
        {
            using (var context = new LearningPlatformContext())
            {
                return await context.Users.Include(u => u.UserProfile).ToListAsync();
            }
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            using (var context = new LearningPlatformContext())
            {
                return await context.Users.Include(u => u.UserProfile).FirstOrDefaultAsync(u => u.UserId == id);
            }
        }

        public async Task<bool> CreateUserAsync(User user)
        {
            using (var context = new LearningPlatformContext())
            {
                try
                {
                    context.Users.Add(user);
                    await context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception($"L?i khi t?o ng??i d�ng: {ex.Message}");
                }
            }
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            using (var context = new LearningPlatformContext())
            {
                try
                {
                    var dbUser = await context.Users.FindAsync(user.UserId);
                    if (dbUser == null) throw new Exception("Ng??i d�ng kh�ng t?n t?i.");

                    dbUser.Email = user.Email;
                    dbUser.Username = user.Username;
                    dbUser.FullName = user.FullName;
                    dbUser.RoleId = user.RoleId;
                    dbUser.Status = user.Status;
                    // User entity does not define UpdatedAt; do not set it here

                    context.Users.Update(dbUser);
                    await context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception($"L?i khi c?p nh?t ng??i d�ng: {ex.Message}");
                }
            }
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            using (var context = new LearningPlatformContext())
            {
                try
                {
                    var user = await context.Users.FindAsync(id);
                    if (user == null) throw new Exception("Ng??i d�ng kh�ng t?n t?i.");

                    context.Users.Remove(user);
                    await context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception($"L?i khi x�a ng??i d�ng: {ex.Message}");
                }
            }
        }

        // Course Management
        public async Task<List<Course>> GetCoursesAsync()
        {
            using (var context = new LearningPlatformContext())
            {
                return await context.Courses.Include(c => c.Category).ToListAsync();
            }
        }

        public async Task<Course> GetCourseByIdAsync(int id)
        {
            using (var context = new LearningPlatformContext())
            {
                return await context.Courses.Include(c => c.Category).FirstOrDefaultAsync(c => c.CourseId == id);
            }
        }

        public async Task<bool> CourseSlugExistsAsync(string slug, int? excludeId = null)
        {
            using (var context = new LearningPlatformContext())
            {
                return await context.Courses.AnyAsync(c => c.Slug == slug && (!excludeId.HasValue || c.CourseId != excludeId.Value));
            }
        }

        public async Task<bool> CreateCourseAsync(Course course)
        {
            using (var context = new LearningPlatformContext())
            {
                try
                {
                    if (course.Price < 0) throw new ArgumentException("Gi� ph?i l� s? l?n h?n ho?c b?ng 0.");
                    if (string.IsNullOrWhiteSpace(course.Title)) throw new ArgumentException("Ti�u ?? kh�ng ???c ?? tr?ng.");
                    if (course.Title.Length > 200) throw new ArgumentException("Ti�u ?? qu� d�i (t?i ?a 200 k� t?).");

                    if (string.IsNullOrWhiteSpace(course.Slug)) course.Slug = course.Title.ToLower().Replace(" ", "-");
                    var slug = course.Slug;
                    if (await CourseSlugExistsAsync(slug)) throw new ArgumentException("Slug ?� t?n t?i. Vui l�ng ??i t�n ti�u ??.");

                    if (course.CreatedAt == default) course.CreatedAt = DateTime.UtcNow;

                    context.Courses.Add(course);
                    await context.SaveChangesAsync();
                    return true;
                }
                catch (ArgumentException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    throw new Exception($"L?i khi t?o kh�a h?c: {ex.Message}");
                }
            }
        }

        public async Task<bool> UpdateCourseAsync(Course course)
        {
            using (var context = new LearningPlatformContext())
            {
                try
                {
                    var dbCourse = await context.Courses.FindAsync(course.CourseId);
                    if (dbCourse == null) throw new Exception("Kh�a h?c kh�ng t?n t?i.");

                    if (course.Price < 0) throw new ArgumentException("Gi� ph?i l� s? l?n h?n ho?c b?ng 0.");
                    if (string.IsNullOrWhiteSpace(course.Title)) throw new ArgumentException("Ti�u ?? kh�ng ???c ?? tr?ng.");
                    if (course.Title.Length > 200) throw new ArgumentException("Ti�u ?? qu� d�i (t?i ?a 200 k� t?).");

                    if (string.IsNullOrWhiteSpace(course.Slug)) course.Slug = course.Title.ToLower().Replace(" ", "-");
                    if (await CourseSlugExistsAsync(course.Slug, course.CourseId)) throw new ArgumentException("Slug ?� t?n t?i cho kh�a h?c kh�c.");

                    // update allowed fields
                    dbCourse.Title = course.Title;
                    dbCourse.Summary = course.Summary;
                    dbCourse.Slug = course.Slug;
                    dbCourse.Price = course.Price;
                    dbCourse.IsPublished = course.IsPublished;
                    dbCourse.OwnerId = course.OwnerId;
                    dbCourse.UpdatedAt = DateTime.UtcNow;

                    context.Courses.Update(dbCourse);
                    await context.SaveChangesAsync();
                    return true;
                }
                catch (ArgumentException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    throw new Exception($"L?i khi c?p nh?t kh�a h?c: {ex.Message}");
                }
            }
        }

        public async Task<bool> DeleteCourseAsync(int id)
        {
            using (var context = new LearningPlatformContext())
            {
                try
                {
                    var course = await context.Courses.FindAsync(id);
                    if (course == null) throw new Exception("Kh�a h?c kh�ng t?n t?i.");

                    context.Courses.Remove(course);
                    await context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception($"L?i khi x�a kh�a h?c: {ex.Message}");
                }
            }
        }

        // Test Management
        public async Task<List<Test>> GetTestsAsync()
        {
            using (var context = new LearningPlatformContext())
            {
                return await context.Tests.Include(t => t.Questions).ToListAsync();
            }
        }

        public async Task<Test> GetTestByIdAsync(int id)
        {
            using (var context = new LearningPlatformContext())
            {
                return await context.Tests.Include(t => t.Questions).FirstOrDefaultAsync(t => t.TestId == id);
            }
        }

        public async Task<bool> CreateTestAsync(Test test)
        {
            using (var context = new LearningPlatformContext())
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(test.Title)) throw new ArgumentException("Ti�u ?? b�i ki?m tra kh�ng ???c ?? tr?ng.");
                    if (test.Title.Length > 200) throw new ArgumentException("Ti�u ?? qu� d�i (t?i ?a 200 k� t?).");

                    context.Tests.Add(test);
                    await context.SaveChangesAsync();
                    return true;
                }
                catch (ArgumentException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    throw new Exception($"L?i khi t?o b�i ki?m tra: {ex.Message}");
                }
            }
        }

        public async Task<bool> UpdateTestAsync(Test test)
        {
            using (var context = new LearningPlatformContext())
            {
                try
                {
                    var dbTest = await context.Tests.FindAsync(test.TestId);
                    if (dbTest == null) throw new Exception("B�i ki?m tra kh�ng t?n t?i.");

                    if (string.IsNullOrWhiteSpace(test.Title)) throw new ArgumentException("Ti�u ?? b�i ki?m tra kh�ng ???c ?? tr?ng.");
                    if (test.Title.Length > 200) throw new ArgumentException("Ti�u ?? qu� d�i (t?i ?a 200 k� t?).");

                    dbTest.Title = test.Title;
                    dbTest.Description = test.Description;
                    dbTest.TimeLimitSec = test.TimeLimitSec;
                    dbTest.UpdatedAt = DateTime.UtcNow;

                    context.Tests.Update(dbTest);
                    await context.SaveChangesAsync();
                    return true;
                }
                catch (ArgumentException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    throw new Exception($"L?i khi c?p nh?t b�i ki?m tra: {ex.Message}");
                }
            }
        }

        public async Task<bool> DeleteTestAsync(int id)
        {
            using (var context = new LearningPlatformContext())
            {
                try
                {
                    var test = await context.Tests.FindAsync(id);
                    if (test == null) throw new Exception("B�i ki?m tra kh�ng t?n t?i.");

                    context.Tests.Remove(test);
                    await context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception($"L?i khi x�a b�i ki?m tra: {ex.Message}");
                }
            }
        }

        // Analytics Methods
        public async Task<UserAnalytics> GetUserAnalyticsAsync()
        {
            using (var context = new LearningPlatformContext())
            {
                var now = DateTime.Now;
                var startOfMonth = new DateTime(now.Year, now.Month, 1);
                var startOfYear = new DateTime(now.Year, 1, 1);
                
                var newUsersByMonth = new Dictionary<int, int>();
                for (int i = 1; i <= 12; i++) newUsersByMonth[i] = 0;
                
                var monthlyUsers = await context.Users
                    .Where(u => u.CreatedAt >= startOfYear)
                    .GroupBy(u => u.CreatedAt.Month)
                    .Select(g => new { Month = g.Key, Count = g.Count() })
                    .ToListAsync();
                
                foreach (var item in monthlyUsers)
                    newUsersByMonth[item.Month] = item.Count;
                
                var recentActive = await context.Users
                    .Where(u => u.LastLoginAt.HasValue)
                    .OrderByDescending(u => u.LastLoginAt)
                    .Take(10)
                    .Select(u => new { u.Username, u.LastLoginAt })
                    .ToListAsync();
                
                return new UserAnalytics
                {
                    AdminCount = await context.Users.CountAsync(u => u.RoleId == 1),
                    TeacherCount = await context.Users.CountAsync(u => u.RoleId == 2),
                    StudentCount = await context.Users.CountAsync(u => u.RoleId == 3),
                    NewUsersThisMonth = await context.Users.CountAsync(u => u.CreatedAt >= startOfMonth),
                    ActiveToday = await context.Users.CountAsync(u => u.LastLoginAt.HasValue && u.LastLoginAt.Value.Date == now.Date),
                    ActiveThisWeek = await context.Users.CountAsync(u => u.LastLoginAt.HasValue && u.LastLoginAt.Value >= now.AddDays(-7)),
                    NewUsersByMonth = newUsersByMonth,
                    RecentActiveUsers = recentActive.Select(u => (u.Username, u.LastLoginAt)).ToList()
                };
            }
        }

        public async Task<LearningAnalytics> GetLearningAnalyticsAsync()
        {
            using (var context = new LearningPlatformContext())
            {
                var now = DateTime.Now;
                var startOfMonth = new DateTime(now.Year, now.Month, 1);
                var startOfYear = new DateTime(now.Year, 1, 1);
                var totalEnrollments = await context.CoursePurchases.CountAsync();
                var completedEnrollments = await context.CourseProgresses.CountAsync(e => e.IsCompleted);
                
                var topCourses = await context.CoursePurchases
                    .GroupBy(cp => new { cp.CourseId, cp.Course.Title })
                    .Select(g => new { g.Key.Title, Count = g.Count() })
                    .OrderByDescending(x => x.Count)
                    .Take(5)
                    .ToListAsync();
                
                var testsByMonth = new Dictionary<int, int>();
                for (int i = 1; i <= 12; i++) testsByMonth[i] = 0;
                
                var monthlyTests = await context.TestAttempts
                    .Where(t => t.StartedAt >= startOfYear)
                    .GroupBy(t => t.StartedAt.Month)
                    .Select(g => new { Month = g.Key, Count = g.Count() })
                    .ToListAsync();
                
                foreach (var item in monthlyTests)
                    testsByMonth[item.Month] = item.Count;
                
                return new LearningAnalytics
                {
                    TotalCourses = await context.Courses.CountAsync(),
                    TotalClasses = await context.CourseChapters.CountAsync(),
                    TotalEnrollments = totalEnrollments,
                    CompletionRate = totalEnrollments > 0 ? (completedEnrollments * 100.0 / totalEnrollments) : 0,
                    TotalTests = await context.Tests.CountAsync(),
                    TestsThisMonth = await context.Tests.CountAsync(t => t.CreatedAt >= startOfMonth),
                    TopCourses = topCourses.Select(c => (c.Title, c.Count)).ToList(),
                    TestsByMonth = testsByMonth
                };
            }
        }

        public async Task<RevenueAnalytics> GetRevenueAnalyticsAsync()
        {
            using (var context = new LearningPlatformContext())
            {
                var now = DateTime.Now;
                var startOfMonth = new DateTime(now.Year, now.Month, 1);
                
                return new RevenueAnalytics
                {
                    TotalRevenue = await context.Payments.AnyAsync() ? await context.Payments.SumAsync(p => p.Amount) : 0,
                    RevenueThisMonth = await context.Payments.Where(p => p.PaidAt.HasValue && p.PaidAt.Value >= startOfMonth).AnyAsync() ? await context.Payments.Where(p => p.PaidAt.HasValue && p.PaidAt.Value >= startOfMonth).SumAsync(p => p.Amount) : 0,
                    PaidAmount = await context.Payments.Where(p => p.Status == "Completed").AnyAsync() ? await context.Payments.Where(p => p.Status == "Completed").SumAsync(p => p.Amount) : 0,
                    PendingAmount = await context.Payments.Where(p => p.Status == "Pending").AnyAsync() ? await context.Payments.Where(p => p.Status == "Pending").SumAsync(p => p.Amount) : 0,
                    PaidCount = await context.Payments.CountAsync(p => p.Status == "Completed"),
                    PendingCount = await context.Payments.CountAsync(p => p.Status == "Pending"),
                    RefundedCount = await context.Payments.CountAsync(p => p.Status == "Refunded"),
                    VNPayCount = await context.Payments.CountAsync(p => p.Provider == "VNPay"),
                    StripeCount = await context.Payments.CountAsync(p => p.Provider == "Stripe"),
                    OtherPaymentCount = await context.Payments.CountAsync(p => p.Provider != "VNPay" && p.Provider != "Stripe")
                };
            }
        }

        public async Task<SystemAnalytics> GetSystemAnalyticsAsync()
        {
            using (var context = new LearningPlatformContext())
            {
                var now = DateTime.Now;
                var today = now.Date;
                var startOfWeek = now.AddDays(-7);
                
                var recentAuditLogs = await context.AuditLogs
                    .Include(a => a.User)
                    .OrderByDescending(a => a.CreatedAt)
                    .Take(10)
                    .Select(a => new { a.Action, Username = a.User != null ? a.User.Username : "System", a.CreatedAt })
                    .ToListAsync();
                
                return new SystemAnalytics
                {
                    TotalNotifications = await context.Notifications.CountAsync(),
                    NotificationsSent = await context.Notifications.CountAsync(n => n.IsRead),
                    NotificationsPending = await context.Notifications.CountAsync(n => !n.IsRead),
                    TotalAuditLogs = await context.AuditLogs.CountAsync(),
                    AuditLogsToday = await context.AuditLogs.CountAsync(a => a.CreatedAt.Date == today),
                    RequestsToday = await context.AuditLogs.CountAsync(a => a.CreatedAt.Date == today),
                    RecentAuditLogs = recentAuditLogs.Select(a => (a.Action, a.Username, a.CreatedAt)).ToList()
                };
            }
        }

        // Question Management
        public async Task<List<Question>> GetQuestionsByTestIdAsync(int testId)
        {
            using (var context = new LearningPlatformContext())
            {
                return await context.Questions
                    .Include(q => q.QuestionOptions)
                    .Where(q => q.TestId == testId)
                    .OrderBy(q => q.OrderIndex)
                    .ToListAsync();
            }
        }

        public async Task<Question> GetQuestionByIdAsync(int questionId)
        {
            using (var context = new LearningPlatformContext())
            {
                return await context.Questions
                    .Include(q => q.QuestionOptions)
                    .FirstOrDefaultAsync(q => q.QuestionId == questionId);
            }
        }

        public async Task<bool> CreateQuestionAsync(Question question)
        {
            using (var context = new LearningPlatformContext())
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(question.StemText)) 
                        throw new ArgumentException("Nội dung câu hỏi không được trống");
                    if (question.Points <= 0) 
                        throw new ArgumentException("Điểm số phải lớn hơn 0");

                    // Set order index if not provided
                    if (question.OrderIndex == 0)
                    {
                        var maxOrder = await context.Questions
                            .Where(q => q.TestId == question.TestId)
                            .MaxAsync(q => (int?)q.OrderIndex) ?? 0;
                        question.OrderIndex = maxOrder + 1;
                    }

                    context.Questions.Add(question);
                    await context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi tạo câu hỏi: {ex.Message}");
                }
            }
        }

        public async Task<bool> UpdateQuestionAsync(Question question)
        {
            using (var context = new LearningPlatformContext())
            {
                try
                {
                    var dbQuestion = await context.Questions
                        .Include(q => q.QuestionOptions)
                        .FirstOrDefaultAsync(q => q.QuestionId == question.QuestionId);
                    if (dbQuestion == null) throw new Exception("Câu hỏi không tồn tại");

                    if (string.IsNullOrWhiteSpace(question.StemText)) 
                        throw new ArgumentException("Nội dung câu hỏi không được trống");
                    if (question.Points <= 0) 
                        throw new ArgumentException("Điểm số phải lớn hơn 0");

                    dbQuestion.StemText = question.StemText;
                    dbQuestion.Type = question.Type;
                    dbQuestion.Points = question.Points;
                    dbQuestion.OrderIndex = question.OrderIndex;
                    dbQuestion.Metadata = question.Metadata;

                    // Update options
                    context.QuestionOptions.RemoveRange(dbQuestion.QuestionOptions);
                    if (question.QuestionOptions?.Any() == true)
                    {
                        foreach (var option in question.QuestionOptions)
                        {
                            option.QuestionId = question.QuestionId;
                            context.QuestionOptions.Add(option);
                        }
                    }

                    await context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi cập nhật câu hỏi: {ex.Message}");
                }
            }
        }

        public async Task<bool> DeleteQuestionAsync(int questionId)
        {
            using (var context = new LearningPlatformContext())
            {
                try
                {
                    var question = await context.Questions
                        .Include(q => q.QuestionOptions)
                        .FirstOrDefaultAsync(q => q.QuestionId == questionId);
                    if (question == null) throw new Exception("Câu hỏi không tồn tại");

                    context.QuestionOptions.RemoveRange(question.QuestionOptions);
                    context.Questions.Remove(question);
                    await context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi xóa câu hỏi: {ex.Message}");
                }
            }
        }

        public async Task<bool> ReorderQuestionsAsync(int testId, List<int> questionIds)
        {
            using (var context = new LearningPlatformContext())
            {
                try
                {
                    for (int i = 0; i < questionIds.Count; i++)
                    {
                        var question = await context.Questions.FindAsync(questionIds[i]);
                        if (question != null && question.TestId == testId)
                        {
                            question.OrderIndex = i + 1;
                        }
                    }
                    await context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi sắp xếp câu hỏi: {ex.Message}");
                }
            }
        }

        // Chapter Management
        public async Task<List<CourseChapter>> GetChaptersByCourseIdAsync(int courseId)
        {
            using (var context = new LearningPlatformContext())
            {
                return await context.CourseChapters
                    .Include(c => c.Lessons)
                    .Where(c => c.CourseId == courseId)
                    .OrderBy(c => c.OrderIndex)
                    .ToListAsync();
            }
        }

        public async Task<bool> CreateChapterAsync(CourseChapter chapter)
        {
            using (var context = new LearningPlatformContext())
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(chapter.Title))
                        throw new ArgumentException("Tiêu đề chương không được trống");

                    if (chapter.OrderIndex == 0)
                    {
                        var maxOrder = await context.CourseChapters
                            .Where(c => c.CourseId == chapter.CourseId)
                            .MaxAsync(c => (int?)c.OrderIndex) ?? 0;
                        chapter.OrderIndex = maxOrder + 1;
                    }

                    context.CourseChapters.Add(chapter);
                    await context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi tạo chương: {ex.Message}");
                }
            }
        }

        public async Task<bool> UpdateChapterAsync(CourseChapter chapter)
        {
            using (var context = new LearningPlatformContext())
            {
                try
                {
                    var dbChapter = await context.CourseChapters.FindAsync(chapter.ChapterId);
                    if (dbChapter == null) throw new Exception("Chương không tồn tại");

                    if (string.IsNullOrWhiteSpace(chapter.Title))
                        throw new ArgumentException("Tiêu đề chương không được trống");

                    dbChapter.Title = chapter.Title;
                    dbChapter.Description = chapter.Description;
                    dbChapter.OrderIndex = chapter.OrderIndex;

                    await context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi cập nhật chương: {ex.Message}");
                }
            }
        }

        public async Task<bool> DeleteChapterAsync(int chapterId)
        {
            using (var context = new LearningPlatformContext())
            {
                try
                {
                    var chapter = await context.CourseChapters
                        .Include(c => c.Lessons)
                        .FirstOrDefaultAsync(c => c.ChapterId == chapterId);
                    if (chapter == null) throw new Exception("Chương không tồn tại");

                    context.Lessons.RemoveRange(chapter.Lessons);
                    context.CourseChapters.Remove(chapter);
                    await context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi xóa chương: {ex.Message}");
                }
            }
        }

        // Lesson Management
        public async Task<List<Lesson>> GetLessonsByChapterIdAsync(int chapterId)
        {
            using (var context = new LearningPlatformContext())
            {
                return await context.Lessons
                    .Where(l => l.ChapterId == chapterId)
                    .OrderBy(l => l.OrderIndex)
                    .ToListAsync();
            }
        }

        public async Task<bool> CreateLessonAsync(Lesson lesson)
        {
            using (var context = new LearningPlatformContext())
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(lesson.Title))
                        throw new ArgumentException("Tiêu đề bài học không được trống");

                    if (lesson.OrderIndex == 0)
                    {
                        var maxOrder = await context.Lessons
                            .Where(l => l.ChapterId == lesson.ChapterId)
                            .MaxAsync(l => (int?)l.OrderIndex) ?? 0;
                        lesson.OrderIndex = maxOrder + 1;
                    }

                    lesson.CreatedAt = DateTime.UtcNow;
                    context.Lessons.Add(lesson);
                    await context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi tạo bài học: {ex.Message}");
                }
            }
        }

        public async Task<bool> UpdateLessonAsync(Lesson lesson)
        {
            using (var context = new LearningPlatformContext())
            {
                try
                {
                    var dbLesson = await context.Lessons.FindAsync(lesson.LessonId);
                    if (dbLesson == null) throw new Exception("Bài học không tồn tại");

                    if (string.IsNullOrWhiteSpace(lesson.Title))
                        throw new ArgumentException("Tiêu đề bài học không được trống");

                    dbLesson.Title = lesson.Title;
                    dbLesson.Description = lesson.Description;
                    dbLesson.OrderIndex = lesson.OrderIndex;
                    dbLesson.Visibility = lesson.Visibility;
                    dbLesson.UpdatedAt = DateTime.UtcNow;

                    await context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi cập nhật bài học: {ex.Message}");
                }
            }
        }

        public async Task<bool> DeleteLessonAsync(int lessonId)
        {
            using (var context = new LearningPlatformContext())
            {
                try
                {
                    var lesson = await context.Lessons.FindAsync(lessonId);
                    if (lesson == null) throw new Exception("Bài học không tồn tại");

                    context.Lessons.Remove(lesson);
                    await context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi xóa bài học: {ex.Message}");
                }
            }
        }

        // Category Management
        public async Task<List<CourseCategory>> GetCategoriesAsync()
        {
            using (var context = new LearningPlatformContext())
            {
                return await context.CourseCategories.ToListAsync();
            }
        }

        public async Task<CourseCategory> GetCategoryByIdAsync(int id)
        {
            using (var context = new LearningPlatformContext())
            {
                return await context.CourseCategories.FirstOrDefaultAsync(c => c.CategoryId == id);
            }
        }

        public async Task<bool> CreateCategoryAsync(CourseCategory category)
        {
            using (var context = new LearningPlatformContext())
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(category.Name))
                        throw new ArgumentException("Tên danh mục không được trống");

                    context.CourseCategories.Add(category);
                    await context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi tạo danh mục: {ex.Message}");
                }
            }
        }

        public async Task<bool> UpdateCategoryAsync(CourseCategory category)
        {
            using (var context = new LearningPlatformContext())
            {
                try
                {
                    var dbCategory = await context.CourseCategories.FindAsync(category.CategoryId);
                    if (dbCategory == null) throw new Exception("Danh mục không tồn tại");

                    if (string.IsNullOrWhiteSpace(category.Name))
                        throw new ArgumentException("Tên danh mục không được trống");

                    dbCategory.Name = category.Name;
                    dbCategory.Description = category.Description;

                    await context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi cập nhật danh mục: {ex.Message}");
                }
            }
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            using (var context = new LearningPlatformContext())
            {
                try
                {
                    var category = await context.CourseCategories.FindAsync(id);
                    if (category == null) throw new Exception("Danh mục không tồn tại");

                    // Kiểm tra xem có khóa học nào đang sử dụng danh mục này không
                    var coursesUsingCategory = await context.Courses.AnyAsync(c => c.CategoryId == id);
                    if (coursesUsingCategory)
                    {
                        throw new Exception("Không thể xóa danh mục vì có khóa học đang sử dụng");
                    }

                    context.CourseCategories.Remove(category);
                    await context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi xóa danh mục: {ex.Message}");
                }
            }
        }

        public async Task<Dictionary<int, decimal>> GetMonthlyRevenueAsync()
        {
            using (var context = new LearningPlatformContext())
            {
                var result = new Dictionary<int, decimal>();
                for (int i = 1; i <= 12; i++) result[i] = 0;

                var monthlyData = await context.Payments
                    .Where(p => p.PaidAt.HasValue && p.Status == "Completed")
                    .GroupBy(p => p.PaidAt.Value.Month)
                    .Select(g => new { Month = g.Key, Total = g.Sum(p => p.Amount) })
                    .ToListAsync();

                foreach (var item in monthlyData)
                    result[item.Month] = item.Total;

                return result;
            }
        }

        internal void Dispose()
        {
            // Cleanup resources if needed
        }
    }
}