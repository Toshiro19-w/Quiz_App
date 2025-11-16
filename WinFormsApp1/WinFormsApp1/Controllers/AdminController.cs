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
            try
            {
                using (var context = new LearningPlatformContext())
                {
                    context.Users.Add(user);
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            catch { return false; }
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            try
            {
                using (var context = new LearningPlatformContext())
                {
                    context.Users.Update(user);
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            catch { return false; }
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            try
            {
                using (var context = new LearningPlatformContext())
                {
                    var user = await context.Users.FindAsync(id);
                    if (user != null)
                    {
                        context.Users.Remove(user);
                        await context.SaveChangesAsync();
                        return true;
                    }
                    return false;
                }
            }
            catch { return false; }
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

        public async Task<bool> CreateCourseAsync(Course course)
        {
            try
            {
                using (var context = new LearningPlatformContext())
                {
                    context.Courses.Add(course);
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            catch { return false; }
        }

        public async Task<bool> UpdateCourseAsync(Course course)
        {
            try
            {
                using (var context = new LearningPlatformContext())
                {
                    context.Courses.Update(course);
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            catch { return false; }
        }

        public async Task<bool> DeleteCourseAsync(int id)
        {
            try
            {
                using (var context = new LearningPlatformContext())
                {
                    var course = await context.Courses.FindAsync(id);
                    if (course != null)
                    {
                        context.Courses.Remove(course);
                        await context.SaveChangesAsync();
                        return true;
                    }
                    return false;
                }
            }
            catch { return false; }
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
            try
            {
                using (var context = new LearningPlatformContext())
                {
                    context.Tests.Add(test);
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            catch { return false; }
        }

        public async Task<bool> UpdateTestAsync(Test test)
        {
            try
            {
                using (var context = new LearningPlatformContext())
                {
                    context.Tests.Update(test);
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            catch { return false; }
        }

        public async Task<bool> DeleteTestAsync(int id)
        {
            try
            {
                using (var context = new LearningPlatformContext())
                {
                    var test = await context.Tests.FindAsync(id);
                    if (test != null)
                    {
                        context.Tests.Remove(test);
                        await context.SaveChangesAsync();
                        return true;
                    }
                    return false;
                }
            }
            catch { return false; }
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