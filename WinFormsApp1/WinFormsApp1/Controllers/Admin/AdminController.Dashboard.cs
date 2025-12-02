using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WinFormsApp1.Models.EF;
using WinFormsApp1.ViewModels;

namespace WinFormsApp1.Controllers
{
    /// <summary>
    /// Partial class containing Dashboard and Analytics methods for AdminController
    /// </summary>
    public partial class AdminController
    {
        // Dashboard Statistics
        public async Task<DashboardStats> GetDashboardStatsAsync(DateTime? startDate = null, DateTime? endDate = null)
        {
            using (var context = new LearningPlatformContext())
            {
                var usersQuery = context.Users.AsQueryable();
                var coursesQuery = context.Courses.AsQueryable();
                var testsQuery = context.Tests.AsQueryable();
                var revenueQuery = context.Payments.Where(p => p.Status == "Paid" || p.Status == "Completed");
                var testResultsQuery = context.TestAttempts.AsQueryable();

                if (startDate.HasValue)
                {
                    usersQuery = usersQuery.Where(u => u.CreatedAt >= startDate.Value);
                    coursesQuery = coursesQuery.Where(c => c.CreatedAt >= startDate.Value);
                    testsQuery = testsQuery.Where(t => t.CreatedAt >= startDate.Value);
                    revenueQuery = revenueQuery.Where(p => p.PaidAt >= startDate.Value);
                    testResultsQuery = testResultsQuery.Where(t => t.StartedAt >= startDate.Value);
                }

                if (endDate.HasValue)
                {
                    usersQuery = usersQuery.Where(u => u.CreatedAt <= endDate.Value);
                    coursesQuery = coursesQuery.Where(c => c.CreatedAt <= endDate.Value);
                    testsQuery = testsQuery.Where(t => t.CreatedAt <= endDate.Value);
                    revenueQuery = revenueQuery.Where(p => p.PaidAt <= endDate.Value);
                    testResultsQuery = testResultsQuery.Where(t => t.StartedAt <= endDate.Value);
                }

                return new DashboardStats
                {
                    TotalUsers = await usersQuery.CountAsync(),
                    TotalCourses = await coursesQuery.CountAsync(),
                    TotalClasses = await context.CourseChapters.CountAsync(),
                    TotalTests = await testsQuery.CountAsync(),
                    TotalRevenue = await revenueQuery.AnyAsync() ? await revenueQuery.SumAsync(p => p.Amount) : 0,
                    TotalTestResults = await testResultsQuery.CountAsync()
                };
            }
        }

        public async Task<Dictionary<string, decimal>> GetRevenueTrendAsync(DateTime? startDate = null, DateTime? endDate = null)
        {
            using (var context = new LearningPlatformContext())
            {
                var query = context.Payments.Where(p => (p.Status == "Paid" || p.Status == "Completed") && p.PaidAt.HasValue);

                if (startDate.HasValue) query = query.Where(p => p.PaidAt >= startDate.Value);
                if (endDate.HasValue) query = query.Where(p => p.PaidAt <= endDate.Value);

                var data = await query.Select(p => new { p.PaidAt, p.Amount }).ToListAsync();

                // Determine grouping
                bool groupByDay = false;
                if (startDate.HasValue && endDate.HasValue)
                {
                    if ((endDate.Value - startDate.Value).TotalDays <= 31) groupByDay = true;
                }
                else if (startDate.HasValue && !endDate.HasValue)
                {
                    if ((DateTime.Now - startDate.Value).TotalDays <= 31) groupByDay = true;
                }

                if (groupByDay)
                {
                    var grouped = data.GroupBy(x => x.PaidAt.Value.Date)
                                      .OrderBy(g => g.Key)
                                      .ToDictionary(g => g.Key.ToString("dd/MM/yyyy"), g => g.Sum(x => x.Amount));
                    return grouped;
                }
                else
                {
                    var grouped = data.GroupBy(x => new { x.PaidAt.Value.Year, x.PaidAt.Value.Month })
                                      .OrderBy(g => g.Key.Year).ThenBy(g => g.Key.Month)
                                      .ToDictionary(g => $"{g.Key.Month:00}/{g.Key.Year}", g => g.Sum(x => x.Amount));
                    return grouped;
                }
            }
        }

        // Analytics Methods
        public async Task<UserAnalytics> GetUserAnalyticsAsync(DateTime? startDate = null, DateTime? endDate = null)
        {
            using (var context = new LearningPlatformContext())
            {
                var now = DateTime.Now;
                var startOfMonth = new DateTime(now.Year, now.Month, 1);
                
                var usersQuery = context.Users.AsQueryable();
                if (startDate.HasValue) usersQuery = usersQuery.Where(u => u.CreatedAt >= startDate.Value);
                if (endDate.HasValue) usersQuery = usersQuery.Where(u => u.CreatedAt <= endDate.Value);

                // For charts, we might want to respect the date range or default to this year if not specified
                var chartStartDate = startDate ?? new DateTime(now.Year, 1, 1);
                var chartEndDate = endDate ?? now;

                var newUsersByMonth = new Dictionary<int, int>();
                for (int i = 1; i <= 12; i++) newUsersByMonth[i] = 0;

                var monthlyUsers = await context.Users
                    .Where(u => u.CreatedAt >= chartStartDate && u.CreatedAt <= chartEndDate)
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
                    AdminCount = await usersQuery.CountAsync(u => u.RoleId == 1),
                    TeacherCount = await usersQuery.CountAsync(u => u.RoleId == 2),
                    StudentCount = await usersQuery.CountAsync(u => u.RoleId == 3),
                    NewUsersThisMonth = await context.Users.CountAsync(u => u.CreatedAt >= startOfMonth), // Keep as "This Month" metric
                    ActiveToday = await context.Users.CountAsync(u => u.LastLoginAt.HasValue && u.LastLoginAt.Value.Date == now.Date),
                    ActiveThisWeek = await context.Users.CountAsync(u => u.LastLoginAt.HasValue && u.LastLoginAt.Value >= now.AddDays(-7)),
                    NewUsersByMonth = newUsersByMonth,
                    RecentActiveUsers = recentActive.Select(u => (u.Username, u.LastLoginAt)).ToList()
                };
            }
        }

        public async Task<LearningAnalytics> GetLearningAnalyticsAsync(DateTime? startDate = null, DateTime? endDate = null, string category = null)
        {
            using (var context = new LearningPlatformContext())
            {
                var now = DateTime.Now;
                var startOfMonth = new DateTime(now.Year, now.Month, 1);
                var startOfYear = new DateTime(now.Year, 1, 1);

                var coursesQuery = context.Courses.AsQueryable();
                if (!string.IsNullOrEmpty(category) && category != "Tất cả")
                {
                    coursesQuery = coursesQuery.Where(c => c.Category.Name == category);
                }

                // Filter enrollments based on date and category
                var enrollmentsQuery = context.CoursePurchases.Include(cp => cp.Course).AsQueryable();
                if (startDate.HasValue) enrollmentsQuery = enrollmentsQuery.Where(cp => cp.PurchasedAt >= startDate.Value);
                if (endDate.HasValue) enrollmentsQuery = enrollmentsQuery.Where(cp => cp.PurchasedAt <= endDate.Value);
                if (!string.IsNullOrEmpty(category) && category != "Tất cả")
                {
                    enrollmentsQuery = enrollmentsQuery.Where(cp => cp.Course.Category.Name == category);
                }

                var totalEnrollments = await enrollmentsQuery.CountAsync();
                
                // Completion rate based on filtered enrollments
                // Note: CourseProgress doesn't strictly link to Purchase, but we can approximate or join.
                // For simplicity, let's count completed progresses for the filtered courses.
                var courseIds = await coursesQuery.Select(c => c.CourseId).ToListAsync();
                var completedEnrollments = await context.CourseProgresses
                    .Where(cp => courseIds.Contains(cp.CourseId) && cp.IsCompleted)
                    .CountAsync();

                var topCourses = await enrollmentsQuery
                    .GroupBy(cp => new { cp.CourseId, cp.Course.Title })
                    .Select(g => new { g.Key.Title, Count = g.Count() })
                    .OrderByDescending(x => x.Count)
                    .Take(5)
                    .ToListAsync();

                var testsByMonth = new Dictionary<int, int>();
                for (int i = 1; i <= 12; i++) testsByMonth[i] = 0;

                var monthlyTests = await context.TestAttempts
                    .Where(t => t.StartedAt >= startOfYear) // Keep chart annual for context, or use filter? Let's use filter if provided.
                    .Where(t => !startDate.HasValue || t.StartedAt >= startDate.Value)
                    .Where(t => !endDate.HasValue || t.StartedAt <= endDate.Value)
                    .GroupBy(t => t.StartedAt.Month)
                    .Select(g => new { Month = g.Key, Count = g.Count() })
                    .ToListAsync();

                foreach (var item in monthlyTests)
                    testsByMonth[item.Month] = item.Count;

                return new LearningAnalytics
                {
                    TotalCourses = await coursesQuery.CountAsync(),
                    TotalClasses = await context.CourseChapters.Where(cc => courseIds.Contains(cc.CourseId)).CountAsync(),
                    TotalEnrollments = totalEnrollments,
                    CompletionRate = totalEnrollments > 0 ? (completedEnrollments * 100.0 / totalEnrollments) : 0,
                    TotalTests = await context.Tests.CountAsync(), // Total tests in system
                    TestsThisMonth = await context.Tests.CountAsync(t => t.CreatedAt >= startOfMonth),
                    TopCourses = topCourses.Select(c => (c.Title, c.Count)).ToList(),
                    TestsByMonth = testsByMonth
                };
            }
        }

        public async Task<RevenueAnalytics> GetRevenueAnalyticsAsync(DateTime? startDate = null, DateTime? endDate = null, string status = null, string provider = null)
        {
            using (var context = new LearningPlatformContext())
            {
                var now = DateTime.Now;
                var startOfMonth = new DateTime(now.Year, now.Month, 1);

                var query = context.Payments.AsQueryable();

                if (startDate.HasValue) query = query.Where(p => p.PaidAt >= startDate.Value);
                if (endDate.HasValue) query = query.Where(p => p.PaidAt <= endDate.Value);
                
                if (!string.IsNullOrEmpty(status) && status != "Tất cả")
                {
                    if (status == "Đã thanh toán") query = query.Where(p => p.Status == "Paid" || p.Status == "Completed");
                    else if (status == "Chờ thanh toán") query = query.Where(p => p.Status == "Pending");
                    else if (status == "Hoàn tiền") query = query.Where(p => p.Status == "Refunded");
                }

                if (!string.IsNullOrEmpty(provider) && provider != "Tất cả")
                {
                    if (provider == "Khác") query = query.Where(p => p.Provider != "VNPay" && p.Provider != "Stripe");
                    else query = query.Where(p => p.Provider == provider);
                }

                // Treat both "Paid" and "Completed" as successful statuses
                var completedStatuses = new[] { "Paid", "Completed" };

                var totalRevenue = await query.AnyAsync() ? await query.SumAsync(p => p.Amount) : 0;
                
                // Revenue this month (metric) - usually independent of filter, but let's respect filter if it's within the month?
                // Actually, "Revenue This Month" usually means "Current Calendar Month". Let's keep it as current month for the metric card.
                var revenueThisMonth = await context.Payments.Where(p => p.PaidAt.HasValue && p.PaidAt.Value >= startOfMonth).AnyAsync()
                    ? await context.Payments.Where(p => p.PaidAt.HasValue && p.PaidAt.Value >= startOfMonth).SumAsync(p => p.Amount)
                    : 0;

                var paidAmount = await query.Where(p => completedStatuses.Contains(p.Status)).AnyAsync()
                    ? await query.Where(p => completedStatuses.Contains(p.Status)).SumAsync(p => p.Amount)
                    : 0;

                var pendingAmount = await query.Where(p => p.Status == "Pending").AnyAsync()
                    ? await query.Where(p => p.Status == "Pending").SumAsync(p => p.Amount)
                    : 0;

                return new RevenueAnalytics
                {
                    TotalRevenue = totalRevenue,
                    RevenueThisMonth = revenueThisMonth,
                    PaidAmount = paidAmount,
                    PendingAmount = pendingAmount,
                    PaidCount = await query.CountAsync(p => completedStatuses.Contains(p.Status)),
                    PendingCount = await query.CountAsync(p => p.Status == "Pending"),
                    RefundedCount = await query.CountAsync(p => p.Status == "Refunded"),
                    VNPayCount = await query.CountAsync(p => p.Provider == "VNPay"),
                    StripeCount = await query.CountAsync(p => p.Provider == "Stripe"),
                    OtherPaymentCount = await query.CountAsync(p => p.Provider != "VNPay" && p.Provider != "Stripe")
                };
            }
        }

        public async Task<SystemAnalytics> GetSystemAnalyticsAsync(DateTime? startDate = null, DateTime? endDate = null)
        {
            using (var context = new LearningPlatformContext())
            {
                var now = DateTime.Now;
                var today = now.Date;

                var auditQuery = context.AuditLogs.AsQueryable();
                var notifQuery = context.Notifications.AsQueryable();

                if (startDate.HasValue)
                {
                    auditQuery = auditQuery.Where(a => a.CreatedAt >= startDate.Value);
                    notifQuery = notifQuery.Where(n => n.CreatedAt >= startDate.Value);
                }
                if (endDate.HasValue)
                {
                    auditQuery = auditQuery.Where(a => a.CreatedAt <= endDate.Value);
                    notifQuery = notifQuery.Where(n => n.CreatedAt <= endDate.Value);
                }

                var recentAuditLogs = await auditQuery
                    .Include(a => a.User)
                    .OrderByDescending(a => a.CreatedAt)
                    .Take(10)
                    .Select(a => new { a.Action, Username = a.User != null ? a.User.Username : "System", a.CreatedAt })
                    .ToListAsync();

                return new SystemAnalytics
                {
                    TotalNotifications = await notifQuery.CountAsync(),
                    NotificationsSent = await notifQuery.CountAsync(n => n.IsRead), // Assuming IsRead approximates sent/viewed for now
                    NotificationsPending = await notifQuery.CountAsync(n => !n.IsRead),
                    TotalAuditLogs = await auditQuery.CountAsync(),
                    AuditLogsToday = await context.AuditLogs.CountAsync(a => a.CreatedAt.Date == today), // Metric: Today
                    RequestsToday = await context.AuditLogs.CountAsync(a => a.CreatedAt.Date == today), // Metric: Today
                    RecentAuditLogs = recentAuditLogs.Select(a => (a.Action, a.Username, a.CreatedAt)).ToList()
                };
            }
        }
    }
}
