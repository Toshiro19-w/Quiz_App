using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp1.Models.EF;
using WinFormsApp1.Models.Entities;

namespace WinFormsApp1.Helpers
{
    public static class AuditHelper
    {
        public static async Task LogActionAsync(string action, string entityType, int? entityId = null, string details = null)
        {
            try
            {
                using (var context = new LearningPlatformContext())
                {
                    var auditLog = new AuditLog
                    {
                        UserId = AuthHelper.CurrentUser?.UserId,
                        Action = action,
                        EntityType = entityType,
                        EntityId = entityId,
                        After = details, // Use After field for details
                        IpAddress = "127.0.0.1", // In production, get real IP
                        CreatedAt = DateTime.UtcNow
                    };

                    context.AuditLogs.Add(auditLog);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                // Log to file or event log in production
                System.Diagnostics.Debug.WriteLine($"Audit log failed: {ex.Message}");
            }
        }

        public static async Task LogUserActionAsync(string action, int? userId = null, string details = null)
        {
            await LogActionAsync(action, "User", userId, details);
        }

        public static async Task LogCourseActionAsync(string action, int? courseId = null, string details = null)
        {
            await LogActionAsync(action, "Course", courseId, details);
        }

        public static async Task LogTestActionAsync(string action, int? testId = null, string details = null)
        {
            await LogActionAsync(action, "Test", testId, details);
        }

        public static bool HasPermission(string action, string resource = null)
        {
            if (AuthHelper.CurrentUser == null) return false;
            
            // Simple role-based check - expand as needed
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
    }
}