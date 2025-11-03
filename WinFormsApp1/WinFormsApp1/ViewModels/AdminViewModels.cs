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
        public int MaleCount { get; set; }
        public int FemaleCount { get; set; }
        public int OtherCount { get; set; }
        public int ActiveToday { get; set; }
        public int ActiveThisWeek { get; set; }
    }

    public class LearningAnalytics
    {
        public int TotalCourses { get; set; }
        public int TotalClasses { get; set; }
        public int TotalEnrollments { get; set; }
        public double CompletionRate { get; set; }
        public int TotalTests { get; set; }
        public int TestsThisMonth { get; set; }
        public int ActiveTeachers { get; set; }
        public int ActiveStudents { get; set; }
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
        public int TotalErrors { get; set; }
        public int ErrorsToday { get; set; }
        public int ErrorsThisWeek { get; set; }
        public int RequestsToday { get; set; }
    }
}