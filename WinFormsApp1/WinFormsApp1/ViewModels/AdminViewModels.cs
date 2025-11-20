namespace WinFormsApp1.ViewModels
{
    public class DashboardStats
    {
        public int TotalUsers { get; set; }
        public int TotalCourses { get; set; }
        public int TotalClasses { get; set; }
        public int TotalTests { get; set; }
        public decimal TotalRevenue { get; set; }
        public int TotalTestResults { get; set; }
    }

    public class UserAnalytics
    {
        public int AdminCount { get; set; }
        public int TeacherCount { get; set; }
        public int StudentCount { get; set; }
        public int NewUsersThisMonth { get; set; }
        public int ActiveToday { get; set; }
        public int ActiveThisWeek { get; set; }
        public Dictionary<int, int> NewUsersByMonth { get; set; }
        public List<(string Username, DateTime? LastLogin)> RecentActiveUsers { get; set; }
    }

    public class LearningAnalytics
    {
        public int TotalCourses { get; set; }
        public int TotalClasses { get; set; }
        public int TotalEnrollments { get; set; }
        public double CompletionRate { get; set; }
        public int TotalTests { get; set; }
        public int TestsThisMonth { get; set; }
        public List<(string CourseTitle, int EnrollmentCount)> TopCourses { get; set; }
        public Dictionary<int, int> TestsByMonth { get; set; }
    }

    public class RevenueAnalytics
    {
        public decimal TotalRevenue { get; set; }
        public decimal RevenueThisMonth { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal PendingAmount { get; set; }
        public int PaidCount { get; set; }
        public int PendingCount { get; set; }
        public int RefundedCount { get; set; }
        public int VNPayCount { get; set; }
        public int StripeCount { get; set; }
        public int OtherPaymentCount { get; set; }
    }

    public class SystemAnalytics
    {
        public int TotalNotifications { get; set; }
        public int NotificationsSent { get; set; }
        public int NotificationsPending { get; set; }
        public int TotalAuditLogs { get; set; }
        public int AuditLogsToday { get; set; }
        public int RequestsToday { get; set; }
        public List<(string Action, string Username, DateTime CreatedAt)> RecentAuditLogs { get; set; }
    }
}