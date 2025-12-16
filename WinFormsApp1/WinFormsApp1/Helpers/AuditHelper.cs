using System;
using System.Text.Json;
using System.Threading.Tasks;
using WinFormsApp1.Models.EF;
using WinFormsApp1.Models.Entities;
using WinFormsApp1.Service;
using WinFormsApp1.Service.IService;
using WinFormsApp1.ViewModels;

namespace WinFormsApp1.Helpers
{
    /// <summary>
    /// Helper class cho ghi log các hành động trong hệ thống
    /// </summary>
    public static class AuditHelper
    {
        private static readonly IAuditLogService _auditService = new AuditLogService();
        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            WriteIndented = false,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        private static string ToJson(object obj) => obj != null ? JsonSerializer.Serialize(obj, _jsonOptions) : null;

        #region Basic Log Methods

        /// <summary>
        /// Ghi log hành động cơ bản
        /// </summary>
        public static async Task LogActionAsync(string action, string entityType, int? entityId = null, string details = null)
        {
            await _auditService.LogAsync(action, entityType, entityId, null, details, AuditSeverity.Info);
        }

        /// <summary>
        /// Ghi log với dữ liệu trước và sau khi thay đổi
        /// </summary>
        public static async Task LogChangeAsync<T>(string action, string entityType, int? entityId, T before, T after)
        {
            await _auditService.LogAsync(action, entityType, entityId, ToJson(before), ToJson(after), AuditSeverity.Info);
        }

        /// <summary>
        /// Ghi log lỗi
        /// </summary>
        public static async Task LogErrorAsync(string action, string entityType, int? entityId, Exception ex)
        {
            await _auditService.LogErrorAsync(action, entityType, entityId, ex);
        }

        #endregion

        #region User Actions

        public static async Task LogUserLoginAsync(int userId, bool success, string details = null)
        {
            await _auditService.LogLoginAsync(userId, success, details);
        }

        public static async Task LogUserLogoutAsync(int userId)
        {
            await _auditService.LogLogoutAsync(userId);
        }

        public static async Task LogUserActionAsync(string action, int? userId = null, string details = null)
        {
            await _auditService.LogAsync(action, AuditEntityTypes.User, userId, null, details, AuditSeverity.Info);
        }

        public static async Task LogUserCreateAsync(User user)
        {
            var userData = new { user.UserId, user.Username, user.Email, user.FullName, user.RoleId };
            await _auditService.LogAsync(AuditActions.UserCreate, AuditEntityTypes.User, user.UserId, null, ToJson(userData), AuditSeverity.Success);
        }

        public static async Task LogUserUpdateAsync(User before, User after)
        {
            var beforeData = new { before.Username, before.Email, before.FullName, before.RoleId, before.Status };
            var afterData = new { after.Username, after.Email, after.FullName, after.RoleId, after.Status };
            await _auditService.LogAsync(AuditActions.UserUpdate, AuditEntityTypes.User, after.UserId, ToJson(beforeData), ToJson(afterData), AuditSeverity.Info);
        }

        public static async Task LogUserDeleteAsync(User user)
        {
            var userData = new { user.UserId, user.Username, user.Email, user.FullName };
            await _auditService.LogAsync(AuditActions.UserDelete, AuditEntityTypes.User, user.UserId, ToJson(userData), null, AuditSeverity.Warning);
        }

        public static async Task LogUserStatusChangeAsync(int userId, int oldStatus, int newStatus)
        {
            await _auditService.LogAsync(
                AuditActions.UserStatusChange, 
                AuditEntityTypes.User, 
                userId, 
                ToJson(new { Status = oldStatus }), 
                ToJson(new { Status = newStatus }), 
                AuditSeverity.Info);
        }

        public static async Task LogUserRoleChangeAsync(int userId, int oldRoleId, int newRoleId)
        {
            await _auditService.LogAsync(
                AuditActions.UserRoleChange, 
                AuditEntityTypes.User, 
                userId, 
                ToJson(new { RoleId = oldRoleId }), 
                ToJson(new { RoleId = newRoleId }), 
                AuditSeverity.Warning);
        }

        public static async Task LogPasswordChangeAsync(int userId)
        {
            await _auditService.LogAsync(
                AuditActions.UserPasswordChange, 
                AuditEntityTypes.User, 
                userId, 
                null, 
                "Mật khẩu đã được thay đổi", 
                AuditSeverity.Info);
        }

        public static async Task LogPasswordResetAsync(int userId)
        {
            await _auditService.LogAsync(
                AuditActions.UserPasswordReset, 
                AuditEntityTypes.User, 
                userId, 
                null, 
                "Mật khẩu đã được đặt lại", 
                AuditSeverity.Warning);
        }

        #endregion

        #region Course Actions

        public static async Task LogCourseActionAsync(string action, int? courseId = null, string details = null)
        {
            await _auditService.LogAsync(action, AuditEntityTypes.Course, courseId, null, details, AuditSeverity.Info);
        }

        public static async Task LogCourseCreateAsync(Course course)
        {
            var data = new { course.CourseId, course.Title, course.OwnerId, course.Price, course.CategoryId };
            await _auditService.LogAsync(AuditActions.CourseCreate, AuditEntityTypes.Course, course.CourseId, null, ToJson(data), AuditSeverity.Success);
        }

        public static async Task LogCourseUpdateAsync(Course before, Course after)
        {
            var beforeData = new { before.Title, before.Price, before.IsPublished, before.CategoryId };
            var afterData = new { after.Title, after.Price, after.IsPublished, after.CategoryId };
            await _auditService.LogAsync(AuditActions.CourseUpdate, AuditEntityTypes.Course, after.CourseId, ToJson(beforeData), ToJson(afterData), AuditSeverity.Info);
        }

        public static async Task LogCourseDeleteAsync(Course course)
        {
            var data = new { course.CourseId, course.Title, course.OwnerId };
            await _auditService.LogAsync(AuditActions.CourseDelete, AuditEntityTypes.Course, course.CourseId, ToJson(data), null, AuditSeverity.Warning);
        }

        public static async Task LogCoursePublishAsync(int courseId, string title)
        {
            await _auditService.LogAsync(AuditActions.CoursePublish, AuditEntityTypes.Course, courseId, null, $"Khóa học '{title}' đã được xuất bản", AuditSeverity.Success);
        }

        public static async Task LogCourseApproveAsync(int courseId, string title, int reviewerId)
        {
            await _auditService.LogAsync(
                AuditActions.CourseApprove, 
                AuditEntityTypes.Course, 
                courseId, 
                null, 
                ToJson(new { Title = title, ReviewerId = reviewerId, Status = "Approved" }), 
                AuditSeverity.Success,
                reviewerId);
        }

        public static async Task LogCourseRejectAsync(int courseId, string title, int reviewerId, string reason)
        {
            await _auditService.LogAsync(
                AuditActions.CourseReject, 
                AuditEntityTypes.Course, 
                courseId, 
                null, 
                ToJson(new { Title = title, ReviewerId = reviewerId, Status = "Rejected", Reason = reason }), 
                AuditSeverity.Warning,
                reviewerId);
        }

        #endregion

        #region Test Actions

        public static async Task LogTestActionAsync(string action, int? testId = null, string details = null)
        {
            await _auditService.LogAsync(action, AuditEntityTypes.Test, testId, null, details, AuditSeverity.Info);
        }

        public static async Task LogTestCreateAsync(Test test)
        {
            var data = new { test.TestId, test.Title, test.OwnerId, test.Visibility };
            await _auditService.LogAsync(AuditActions.TestCreate, AuditEntityTypes.Test, test.TestId, null, ToJson(data), AuditSeverity.Success);
        }

        public static async Task LogTestUpdateAsync(int testId, string title)
        {
            await _auditService.LogAsync(AuditActions.TestUpdate, AuditEntityTypes.Test, testId, null, $"Bài kiểm tra '{title}' đã được cập nhật", AuditSeverity.Info);
        }

        public static async Task LogTestDeleteAsync(int testId, string title)
        {
            await _auditService.LogAsync(AuditActions.TestDelete, AuditEntityTypes.Test, testId, ToJson(new { Title = title }), null, AuditSeverity.Warning);
        }

        public static async Task LogTestAttemptStartAsync(int attemptId, int testId, int userId)
        {
            await _auditService.LogAsync(
                AuditActions.TestAttemptStart, 
                AuditEntityTypes.TestAttempt, 
                attemptId, 
                null, 
                ToJson(new { TestId = testId, UserId = userId }), 
                AuditSeverity.Info,
                userId);
        }

        public static async Task LogTestAttemptSubmitAsync(int attemptId, int testId, int userId, decimal? score)
        {
            await _auditService.LogAsync(
                AuditActions.TestAttemptSubmit, 
                AuditEntityTypes.TestAttempt, 
                attemptId, 
                null, 
                ToJson(new { TestId = testId, UserId = userId, Score = score }), 
                AuditSeverity.Success,
                userId);
        }

        #endregion

        #region Flashcard Actions

        public static async Task LogFlashcardSetCreateAsync(FlashcardSet set)
        {
            var data = new { set.SetId, set.Title, set.OwnerId, set.Visibility };
            await _auditService.LogAsync(AuditActions.FlashcardSetCreate, AuditEntityTypes.FlashcardSet, set.SetId, null, ToJson(data), AuditSeverity.Success);
        }

        public static async Task LogFlashcardSetUpdateAsync(int setId, string title)
        {
            await _auditService.LogAsync(AuditActions.FlashcardSetUpdate, AuditEntityTypes.FlashcardSet, setId, null, $"Bộ flashcard '{title}' đã được cập nhật", AuditSeverity.Info);
        }

        public static async Task LogFlashcardSetDeleteAsync(int setId, string title)
        {
            await _auditService.LogAsync(AuditActions.FlashcardSetDelete, AuditEntityTypes.FlashcardSet, setId, ToJson(new { Title = title }), null, AuditSeverity.Warning);
        }

        #endregion

        #region Category Actions

        public static async Task LogCategoryCreateAsync(CourseCategory category)
        {
            var data = new { category.CategoryId, category.Name, category.Slug };
            await _auditService.LogAsync(AuditActions.CategoryCreate, AuditEntityTypes.Category, category.CategoryId, null, ToJson(data), AuditSeverity.Success);
        }

        public static async Task LogCategoryUpdateAsync(int categoryId, string name)
        {
            await _auditService.LogAsync(AuditActions.CategoryUpdate, AuditEntityTypes.Category, categoryId, null, $"Danh mục '{name}' đã được cập nhật", AuditSeverity.Info);
        }

        public static async Task LogCategoryDeleteAsync(int categoryId, string name)
        {
            await _auditService.LogAsync(AuditActions.CategoryDelete, AuditEntityTypes.Category, categoryId, ToJson(new { Name = name }), null, AuditSeverity.Warning);
        }

        #endregion

        #region Payment Actions

        public static async Task LogPaymentCreateAsync(int paymentId, int orderId, decimal amount, string provider)
        {
            await _auditService.LogAsync(
                AuditActions.PaymentCreate, 
                AuditEntityTypes.Payment, 
                paymentId, 
                null, 
                ToJson(new { OrderId = orderId, Amount = amount, Provider = provider }), 
                AuditSeverity.Info);
        }

        public static async Task LogPaymentCompleteAsync(int paymentId, int orderId, decimal amount)
        {
            await _auditService.LogAsync(
                AuditActions.PaymentComplete, 
                AuditEntityTypes.Payment, 
                paymentId, 
                null, 
                ToJson(new { OrderId = orderId, Amount = amount, Status = "Completed" }), 
                AuditSeverity.Success);
        }

        public static async Task LogPaymentRefundAsync(int paymentId, int orderId, decimal amount, string reason)
        {
            await _auditService.LogAsync(
                AuditActions.PaymentRefund, 
                AuditEntityTypes.Payment, 
                paymentId, 
                null, 
                ToJson(new { OrderId = orderId, Amount = amount, Reason = reason }), 
                AuditSeverity.Warning);
        }

        #endregion

        #region System Actions

        public static async Task LogSystemSettingUpdateAsync(string settingName, string oldValue, string newValue)
        {
            await _auditService.LogAsync(
                AuditActions.SystemSettingUpdate, 
                AuditEntityTypes.System, 
                null, 
                ToJson(new { Setting = settingName, Value = oldValue }), 
                ToJson(new { Setting = settingName, Value = newValue }), 
                AuditSeverity.Warning);
        }

        public static async Task LogDataExportAsync(string exportType, int recordCount)
        {
            await _auditService.LogAsync(
                AuditActions.DataExport, 
                AuditEntityTypes.System, 
                null, 
                null, 
                ToJson(new { ExportType = exportType, RecordCount = recordCount }), 
                AuditSeverity.Info);
        }

        public static async Task LogDataImportAsync(string importType, int recordCount, int successCount)
        {
            await _auditService.LogAsync(
                AuditActions.DataImport, 
                AuditEntityTypes.System, 
                null, 
                null, 
                ToJson(new { ImportType = importType, RecordCount = recordCount, SuccessCount = successCount }), 
                AuditSeverity.Info);
        }

        #endregion

        #region Permission Checks

        public static bool HasPermission(string action, string resource = null)
        {
            if (AuthHelper.CurrentUser == null) return false;
            
            switch (AuthHelper.CurrentUser.RoleId)
            {
                case 1: // Admin
                    return true;
                case 2: // Teacher
                    return action != "DELETE" || resource != "User";
                case 3: // Student
                    return false;
                default:
                    return false;
            }
        }

        public static void CheckPermission(string action, string resource = null)
        {
            if (!HasPermission(action, resource))
            {
                throw new UnauthorizedAccessException($"Không có quyền thực hiện {action} trên {resource ?? "tài nguyên"}");
            }
        }

        #endregion
    }
}