using System;
using System.Collections.Generic;

namespace WinFormsApp1.ViewModels
{
    /// <summary>
    /// ViewModel cho hiển thị AuditLog trong danh sách
    /// </summary>
    public class AuditLogViewModel
    {
        public int AuditId { get; set; }
        public int? UserId { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Action { get; set; }
        public string ActionDisplay { get; set; }
        public string EntityType { get; set; }
        public string EntityTypeDisplay { get; set; }
        public int? EntityId { get; set; }
        public string Before { get; set; }
        public string After { get; set; }
        public DateTime CreatedAt { get; set; }
        public string IpAddress { get; set; }
        public string Severity { get; set; }
        
        // Computed properties for display
        public string TimeAgo => GetTimeAgo(CreatedAt);
        public string SeverityColor => GetSeverityColor(Severity);
        
        private static string GetTimeAgo(DateTime dateTime)
        {
            var span = DateTime.UtcNow - dateTime;
            
            if (span.TotalMinutes < 1)
                return "Vừa xong";
            if (span.TotalMinutes < 60)
                return $"{(int)span.TotalMinutes} phút trước";
            if (span.TotalHours < 24)
                return $"{(int)span.TotalHours} giờ trước";
            if (span.TotalDays < 7)
                return $"{(int)span.TotalDays} ngày trước";
            if (span.TotalDays < 30)
                return $"{(int)(span.TotalDays / 7)} tuần trước";
            
            return dateTime.ToString("dd/MM/yyyy HH:mm");
        }
        
        private static string GetSeverityColor(string severity)
        {
            return severity?.ToLower() switch
            {
                "critical" => "#DC2626",
                "error" => "#EF4444",
                "warning" => "#F59E0B",
                "info" => "#3B82F6",
                "success" => "#10B981",
                _ => "#6B7280"
            };
        }
    }

    /// <summary>
    /// Bộ lọc cho AuditLog
    /// </summary>
    public class AuditLogFilter
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? UserId { get; set; }
        public string Action { get; set; }
        public string EntityType { get; set; }
        public string Severity { get; set; }
        public string SearchKeyword { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 50;
    }

    /// <summary>
    /// Kết quả phân trang cho AuditLog
    /// </summary>
    public class AuditLogPagedResult
    {
        public List<AuditLogViewModel> Items { get; set; } = new();
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;
    }

    /// <summary>
    /// Thống kê AuditLog
    /// </summary>
    public class AuditLogStatistics
    {
        public int TotalLogs { get; set; }
        public int LogsToday { get; set; }
        public int LogsThisWeek { get; set; }
        public int LogsThisMonth { get; set; }
        
        // Thống kê theo loại hành động
        public Dictionary<string, int> ActionCounts { get; set; } = new();
        
        // Thống kê theo loại entity
        public Dictionary<string, int> EntityTypeCounts { get; set; } = new();
        
        // Thống kê theo mức độ
        public Dictionary<string, int> SeverityCounts { get; set; } = new();
        
        // Thống kê theo user
        public List<(string Username, int Count)> TopActiveUsers { get; set; } = new();
        
        // Thống kê theo ngày (7 ngày gần nhất)
        public Dictionary<string, int> LogsByDay { get; set; } = new();
        
        // Thống kê theo giờ (24 giờ gần nhất)
        public Dictionary<int, int> LogsByHour { get; set; } = new();
    }

    /// <summary>
    /// Các loại hành động được định nghĩa sẵn
    /// </summary>
    public static class AuditActions
    {
        // User actions
        public const string UserLogin = "USER_LOGIN";
        public const string UserLogout = "USER_LOGOUT";
        public const string UserCreate = "USER_CREATE";
        public const string UserUpdate = "USER_UPDATE";
        public const string UserDelete = "USER_DELETE";
        public const string UserPasswordChange = "USER_PASSWORD_CHANGE";
        public const string UserPasswordReset = "USER_PASSWORD_RESET";
        public const string UserStatusChange = "USER_STATUS_CHANGE";
        public const string UserRoleChange = "USER_ROLE_CHANGE";
        
        // Course actions
        public const string CourseCreate = "COURSE_CREATE";
        public const string CourseUpdate = "COURSE_UPDATE";
        public const string CourseDelete = "COURSE_DELETE";
        public const string CoursePublish = "COURSE_PUBLISH";
        public const string CourseUnpublish = "COURSE_UNPUBLISH";
        public const string CourseApprove = "COURSE_APPROVE";
        public const string CourseReject = "COURSE_REJECT";
        public const string CourseSubmitReview = "COURSE_SUBMIT_REVIEW";
        
        // Test actions
        public const string TestCreate = "TEST_CREATE";
        public const string TestUpdate = "TEST_UPDATE";
        public const string TestDelete = "TEST_DELETE";
        public const string TestAttemptStart = "TEST_ATTEMPT_START";
        public const string TestAttemptSubmit = "TEST_ATTEMPT_SUBMIT";
        public const string TestAttemptGrade = "TEST_ATTEMPT_GRADE";
        
        // Flashcard actions
        public const string FlashcardSetCreate = "FLASHCARD_SET_CREATE";
        public const string FlashcardSetUpdate = "FLASHCARD_SET_UPDATE";
        public const string FlashcardSetDelete = "FLASHCARD_SET_DELETE";
        
        // Category actions
        public const string CategoryCreate = "CATEGORY_CREATE";
        public const string CategoryUpdate = "CATEGORY_UPDATE";
        public const string CategoryDelete = "CATEGORY_DELETE";
        
        // Payment actions
        public const string PaymentCreate = "PAYMENT_CREATE";
        public const string PaymentComplete = "PAYMENT_COMPLETE";
        public const string PaymentRefund = "PAYMENT_REFUND";
        public const string PaymentFailed = "PAYMENT_FAILED";
        
        // System actions
        public const string SystemSettingUpdate = "SYSTEM_SETTING_UPDATE";
        public const string SystemBackup = "SYSTEM_BACKUP";
        public const string SystemRestore = "SYSTEM_RESTORE";
        public const string DataExport = "DATA_EXPORT";
        public const string DataImport = "DATA_IMPORT";

        /// <summary>
        /// Lấy tên hiển thị cho action
        /// </summary>
        public static string GetDisplayName(string action)
        {
            return action switch
            {
                UserLogin => "Đăng nhập",
                UserLogout => "Đăng xuất",
                UserCreate => "Tạo người dùng",
                UserUpdate => "Cập nhật người dùng",
                UserDelete => "Xóa người dùng",
                UserPasswordChange => "Đổi mật khẩu",
                UserPasswordReset => "Đặt lại mật khẩu",
                UserStatusChange => "Thay đổi trạng thái",
                UserRoleChange => "Thay đổi vai trò",
                
                CourseCreate => "Tạo khóa học",
                CourseUpdate => "Cập nhật khóa học",
                CourseDelete => "Xóa khóa học",
                CoursePublish => "Xuất bản khóa học",
                CourseUnpublish => "Hủy xuất bản khóa học",
                CourseApprove => "Phê duyệt khóa học",
                CourseReject => "Từ chối khóa học",
                CourseSubmitReview => "Gửi duyệt khóa học",
                
                TestCreate => "Tạo bài kiểm tra",
                TestUpdate => "Cập nhật bài kiểm tra",
                TestDelete => "Xóa bài kiểm tra",
                TestAttemptStart => "Bắt đầu làm bài",
                TestAttemptSubmit => "Nộp bài",
                TestAttemptGrade => "Chấm điểm",
                
                FlashcardSetCreate => "Tạo bộ flashcard",
                FlashcardSetUpdate => "Cập nhật bộ flashcard",
                FlashcardSetDelete => "Xóa bộ flashcard",
                
                CategoryCreate => "Tạo danh mục",
                CategoryUpdate => "Cập nhật danh mục",
                CategoryDelete => "Xóa danh mục",
                
                PaymentCreate => "Tạo thanh toán",
                PaymentComplete => "Hoàn tất thanh toán",
                PaymentRefund => "Hoàn tiền",
                PaymentFailed => "Thanh toán thất bại",
                
                SystemSettingUpdate => "Cập nhật cài đặt hệ thống",
                SystemBackup => "Sao lưu hệ thống",
                SystemRestore => "Khôi phục hệ thống",
                DataExport => "Xuất dữ liệu",
                DataImport => "Nhập dữ liệu",
                
                _ => action
            };
        }
    }

    /// <summary>
    /// Các loại Entity
    /// </summary>
    public static class AuditEntityTypes
    {
        public const string User = "User";
        public const string Course = "Course";
        public const string CourseChapter = "CourseChapter";
        public const string Lesson = "Lesson";
        public const string LessonContent = "LessonContent";
        public const string Test = "Test";
        public const string Question = "Question";
        public const string TestAttempt = "TestAttempt";
        public const string FlashcardSet = "FlashcardSet";
        public const string Flashcard = "Flashcard";
        public const string Category = "Category";
        public const string Payment = "Payment";
        public const string Order = "Order";
        public const string System = "System";

        /// <summary>
        /// Lấy tên hiển thị cho entity type
        /// </summary>
        public static string GetDisplayName(string entityType)
        {
            return entityType switch
            {
                User => "Người dùng",
                Course => "Khóa học",
                CourseChapter => "Chương học",
                Lesson => "Bài học",
                LessonContent => "Nội dung bài học",
                Test => "Bài kiểm tra",
                Question => "Câu hỏi",
                TestAttempt => "Lượt làm bài",
                FlashcardSet => "Bộ flashcard",
                Flashcard => "Thẻ flashcard",
                Category => "Danh mục",
                Payment => "Thanh toán",
                Order => "Đơn hàng",
                System => "Hệ thống",
                _ => entityType
            };
        }
    }

    /// <summary>
    /// Mức độ nghiêm trọng của log
    /// </summary>
    public static class AuditSeverity
    {
        public const string Info = "Info";
        public const string Success = "Success";
        public const string Warning = "Warning";
        public const string Error = "Error";
        public const string Critical = "Critical";
    }
}
