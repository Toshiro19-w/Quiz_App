using WinFormsApp1.Models.EF;
using WinFormsApp1.Models.Entities;
using Microsoft.EntityFrameworkCore;
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
                    throw new Exception($"L?i khi t?o ng??i dùng: {ex.Message}");
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
                    if (dbUser == null) throw new Exception("Ng??i dùng không t?n t?i.");

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
                    throw new Exception($"L?i khi c?p nh?t ng??i dùng: {ex.Message}");
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
                    if (user == null) throw new Exception("Ng??i dùng không t?n t?i.");

                    context.Users.Remove(user);
                    await context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception($"L?i khi xóa ng??i dùng: {ex.Message}");
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
                    if (course.Price < 0) throw new ArgumentException("Giá ph?i là s? l?n h?n ho?c b?ng 0.");
                    if (string.IsNullOrWhiteSpace(course.Title)) throw new ArgumentException("Tiêu ?? không ???c ?? tr?ng.");
                    if (course.Title.Length > 200) throw new ArgumentException("Tiêu ?? quá dài (t?i ?a 200 ký t?).");

                    if (string.IsNullOrWhiteSpace(course.Slug)) course.Slug = course.Title.ToLower().Replace(" ", "-");
                    var slug = course.Slug;
                    if (await CourseSlugExistsAsync(slug)) throw new ArgumentException("Slug ?ã t?n t?i. Vui lòng ??i tên tiêu ??.");

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
                    throw new Exception($"L?i khi t?o khóa h?c: {ex.Message}");
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
                    if (dbCourse == null) throw new Exception("Khóa h?c không t?n t?i.");

                    if (course.Price < 0) throw new ArgumentException("Giá ph?i là s? l?n h?n ho?c b?ng 0.");
                    if (string.IsNullOrWhiteSpace(course.Title)) throw new ArgumentException("Tiêu ?? không ???c ?? tr?ng.");
                    if (course.Title.Length > 200) throw new ArgumentException("Tiêu ?? quá dài (t?i ?a 200 ký t?).");

                    if (string.IsNullOrWhiteSpace(course.Slug)) course.Slug = course.Title.ToLower().Replace(" ", "-");
                    if (await CourseSlugExistsAsync(course.Slug, course.CourseId)) throw new ArgumentException("Slug ?ã t?n t?i cho khóa h?c khác.");

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
                    throw new Exception($"L?i khi c?p nh?t khóa h?c: {ex.Message}");
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
                    if (course == null) throw new Exception("Khóa h?c không t?n t?i.");

                    context.Courses.Remove(course);
                    await context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception($"L?i khi xóa khóa h?c: {ex.Message}");
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
                    if (string.IsNullOrWhiteSpace(test.Title)) throw new ArgumentException("Tiêu ?? bài ki?m tra không ???c ?? tr?ng.");
                    if (test.Title.Length > 200) throw new ArgumentException("Tiêu ?? quá dài (t?i ?a 200 ký t?).");

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
                    throw new Exception($"L?i khi t?o bài ki?m tra: {ex.Message}");
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
                    if (dbTest == null) throw new Exception("Bài ki?m tra không t?n t?i.");

                    if (string.IsNullOrWhiteSpace(test.Title)) throw new ArgumentException("Tiêu ?? bài ki?m tra không ???c ?? tr?ng.");
                    if (test.Title.Length > 200) throw new ArgumentException("Tiêu ?? quá dài (t?i ?a 200 ký t?).");

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
                    throw new Exception($"L?i khi c?p nh?t bài ki?m tra: {ex.Message}");
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
                    if (test == null) throw new Exception("Bài ki?m tra không t?n t?i.");

                    context.Tests.Remove(test);
                    await context.SaveChangesAsync();
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception($"L?i khi xóa bài ki?m tra: {ex.Message}");
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
                
                return new UserAnalytics
                {
                    AdminCount = await context.Users.CountAsync(u => u.RoleId == 1),
                    TeacherCount = await context.Users.CountAsync(u => u.RoleId == 2),
                    StudentCount = await context.Users.CountAsync(u => u.RoleId == 3),
                    NewUsersThisMonth = await context.Users.CountAsync(u => u.CreatedAt >= startOfMonth),
                    MaleCount = await context.UserProfiles.CountAsync(p => p.Gender == "Male"),
                    FemaleCount = await context.UserProfiles.CountAsync(p => p.Gender == "Female"),
                    OtherCount = await context.UserProfiles.CountAsync(p => p.Gender != "Male" && p.Gender != "Female"),
                    ActiveToday = await context.Users.CountAsync(u => u.LastLoginAt.HasValue && u.LastLoginAt.Value.Date == now.Date),
                    ActiveThisWeek = await context.Users.CountAsync(u => u.LastLoginAt.HasValue && u.LastLoginAt.Value >= now.AddDays(-7))
                };
            }
        }

        public async Task<LearningAnalytics> GetLearningAnalyticsAsync()
        {
            using (var context = new LearningPlatformContext())
            {
                var now = DateTime.Now;
                var startOfMonth = new DateTime(now.Year, now.Month, 1);
                var totalEnrollments = await context.CoursePurchases.CountAsync();
                var completedEnrollments = await context.CourseProgresses.CountAsync(e => e.IsCompleted);
                
                return new LearningAnalytics
                {
                    TotalCourses = await context.Courses.CountAsync(),
                    TotalClasses = await context.CourseChapters.CountAsync(),
                    TotalEnrollments = totalEnrollments,
                    CompletionRate = totalEnrollments > 0 ? (completedEnrollments * 100.0 / totalEnrollments) : 0,
                    TotalTests = await context.Tests.CountAsync(),
                    TestsThisMonth = await context.Tests.CountAsync(t => t.CreatedAt >= startOfMonth),
                    ActiveTeachers = await context.Users.CountAsync(u => u.RoleId == 2 && u.LastLoginAt.HasValue && u.LastLoginAt.Value >= now.AddDays(-30)),
                    ActiveStudents = await context.Users.CountAsync(u => u.RoleId == 3 && u.LastLoginAt.HasValue && u.LastLoginAt.Value >= now.AddDays(-30))
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
                
                return new SystemAnalytics
                {
                    TotalNotifications = await context.Notifications.CountAsync(),
                    NotificationsSent = await context.Notifications.CountAsync(n => n.IsRead),
                    NotificationsPending = await context.Notifications.CountAsync(n => !n.IsRead),
                    TotalAuditLogs = await context.AuditLogs.CountAsync(),
                    AuditLogsToday = await context.AuditLogs.CountAsync(a => a.CreatedAt.Date == today),
                    //TotalErrors = await context.ErrorLogs.CountAsync(),
                    //ErrorsToday = await context.ErrorLogs.CountAsync(e => e.CreatedAt.Date == today),
                    //ErrorsThisWeek = await context.ErrorLogs.CountAsync(e => e.CreatedAt >= startOfWeek),
                    RequestsToday = await context.AuditLogs.CountAsync(a => a.CreatedAt.Date == today)
                };
            }
        }

        internal void Dispose()
        {
            Application.Exit();
        }
    }
}