using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WinFormsApp1.Helpers;
using WinFormsApp1.Models.EF;
using WinFormsApp1.Models.Entities;
using WinFormsApp1.Service.IService;
using WinFormsApp1.ViewModels;

namespace WinFormsApp1.Service
{
    /// <summary>
    /// Service quản lý AuditLog
    /// </summary>
    public class AuditLogService : IAuditLogService
    {
        private readonly JsonSerializerOptions _jsonOptions = new()
        {
            WriteIndented = false,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        #region Ghi Log

        public async Task<bool> LogAsync(
            string action,
            string entityType,
            int? entityId = null,
            string beforeData = null,
            string afterData = null,
            string severity = "Info",
            int? userId = null)
        {
            try
            {
                using var context = new LearningPlatformContext();
                
                var auditLog = new AuditLog
                {
                    UserId = userId ?? AuthHelper.CurrentUser?.UserId,
                    Action = action,
                    EntityType = entityType,
                    EntityId = entityId,
                    Before = beforeData,
                    After = afterData,
                    IpAddress = GetClientIpAddress(),
                    CreatedAt = DateTime.UtcNow
                };

                context.AuditLogs.Add(auditLog);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[AuditLogService] Error logging: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> LogAsync<TBefore, TAfter>(
            string action,
            string entityType,
            int? entityId,
            TBefore beforeData,
            TAfter afterData,
            string severity = "Info",
            int? userId = null)
        {
            string beforeJson = beforeData != null ? JsonSerializer.Serialize(beforeData, _jsonOptions) : null;
            string afterJson = afterData != null ? JsonSerializer.Serialize(afterData, _jsonOptions) : null;

            return await LogAsync(action, entityType, entityId, beforeJson, afterJson, severity, userId);
        }

        public async Task<bool> LogLoginAsync(int userId, bool success, string details = null)
        {
            var action = success ? AuditActions.UserLogin : "USER_LOGIN_FAILED";
            var severity = success ? AuditSeverity.Success : AuditSeverity.Warning;
            
            return await LogAsync(
                action,
                AuditEntityTypes.User,
                userId,
                null,
                details ?? (success ? "Đăng nhập thành công" : "Đăng nhập thất bại"),
                severity,
                userId);
        }

        public async Task<bool> LogLogoutAsync(int userId)
        {
            return await LogAsync(
                AuditActions.UserLogout,
                AuditEntityTypes.User,
                userId,
                null,
                "Đăng xuất thành công",
                AuditSeverity.Info,
                userId);
        }

        public async Task<bool> LogErrorAsync(string action, string entityType, int? entityId, Exception ex)
        {
            var errorDetails = new
            {
                Message = ex.Message,
                StackTrace = ex.StackTrace?.Substring(0, Math.Min(ex.StackTrace?.Length ?? 0, 1000)),
                Type = ex.GetType().Name
            };

            return await LogAsync(
                action,
                entityType,
                entityId,
                null,
                JsonSerializer.Serialize(errorDetails, _jsonOptions),
                AuditSeverity.Error);
        }

        #endregion

        #region Truy vấn

        public async Task<AuditLogPagedResult> GetLogsAsync(AuditLogFilter filter)
        {
            using var context = new LearningPlatformContext();

            var query = context.AuditLogs
                .Include(a => a.User)
                .AsQueryable();

            // Apply filters
            if (filter.StartDate.HasValue)
                query = query.Where(a => a.CreatedAt >= filter.StartDate.Value);

            if (filter.EndDate.HasValue)
                query = query.Where(a => a.CreatedAt <= filter.EndDate.Value);

            if (filter.UserId.HasValue)
                query = query.Where(a => a.UserId == filter.UserId.Value);

            if (!string.IsNullOrEmpty(filter.Action))
                query = query.Where(a => a.Action == filter.Action);

            if (!string.IsNullOrEmpty(filter.EntityType))
                query = query.Where(a => a.EntityType == filter.EntityType);

            if (!string.IsNullOrEmpty(filter.SearchKeyword))
            {
                var keyword = filter.SearchKeyword.ToLower();
                query = query.Where(a =>
                    a.Action.ToLower().Contains(keyword) ||
                    a.EntityType.ToLower().Contains(keyword) ||
                    (a.After != null && a.After.ToLower().Contains(keyword)) ||
                    (a.User != null && (a.User.Username.ToLower().Contains(keyword) ||
                                         a.User.FullName.ToLower().Contains(keyword))));
            }

            // Get total count
            var totalCount = await query.CountAsync();

            // Apply paging
            var items = await query
                .OrderByDescending(a => a.CreatedAt)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .Select(a => new AuditLogViewModel
                {
                    AuditId = a.AuditId,
                    UserId = a.UserId,
                    Username = a.User != null ? a.User.Username : "System",
                    FullName = a.User != null ? a.User.FullName : "Hệ thống",
                    Action = a.Action,
                    ActionDisplay = AuditActions.GetDisplayName(a.Action),
                    EntityType = a.EntityType,
                    EntityTypeDisplay = AuditEntityTypes.GetDisplayName(a.EntityType),
                    EntityId = a.EntityId,
                    Before = a.Before,
                    After = a.After,
                    CreatedAt = a.CreatedAt,
                    IpAddress = a.IpAddress
                })
                .ToListAsync();

            return new AuditLogPagedResult
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize
            };
        }

        public async Task<AuditLogViewModel> GetLogByIdAsync(int auditId)
        {
            using var context = new LearningPlatformContext();

            return await context.AuditLogs
                .Include(a => a.User)
                .Where(a => a.AuditId == auditId)
                .Select(a => new AuditLogViewModel
                {
                    AuditId = a.AuditId,
                    UserId = a.UserId,
                    Username = a.User != null ? a.User.Username : "System",
                    FullName = a.User != null ? a.User.FullName : "Hệ thống",
                    Action = a.Action,
                    ActionDisplay = AuditActions.GetDisplayName(a.Action),
                    EntityType = a.EntityType,
                    EntityTypeDisplay = AuditEntityTypes.GetDisplayName(a.EntityType),
                    EntityId = a.EntityId,
                    Before = a.Before,
                    After = a.After,
                    CreatedAt = a.CreatedAt,
                    IpAddress = a.IpAddress
                })
                .FirstOrDefaultAsync();
        }

        public async Task<List<AuditLogViewModel>> GetLogsByUserAsync(int userId, int count = 50)
        {
            using var context = new LearningPlatformContext();

            return await context.AuditLogs
                .Include(a => a.User)
                .Where(a => a.UserId == userId)
                .OrderByDescending(a => a.CreatedAt)
                .Take(count)
                .Select(a => new AuditLogViewModel
                {
                    AuditId = a.AuditId,
                    UserId = a.UserId,
                    Username = a.User != null ? a.User.Username : "System",
                    FullName = a.User != null ? a.User.FullName : "Hệ thống",
                    Action = a.Action,
                    ActionDisplay = AuditActions.GetDisplayName(a.Action),
                    EntityType = a.EntityType,
                    EntityTypeDisplay = AuditEntityTypes.GetDisplayName(a.EntityType),
                    EntityId = a.EntityId,
                    Before = a.Before,
                    After = a.After,
                    CreatedAt = a.CreatedAt,
                    IpAddress = a.IpAddress
                })
                .ToListAsync();
        }

        public async Task<List<AuditLogViewModel>> GetLogsByEntityAsync(string entityType, int entityId, int count = 50)
        {
            using var context = new LearningPlatformContext();

            return await context.AuditLogs
                .Include(a => a.User)
                .Where(a => a.EntityType == entityType && a.EntityId == entityId)
                .OrderByDescending(a => a.CreatedAt)
                .Take(count)
                .Select(a => new AuditLogViewModel
                {
                    AuditId = a.AuditId,
                    UserId = a.UserId,
                    Username = a.User != null ? a.User.Username : "System",
                    FullName = a.User != null ? a.User.FullName : "Hệ thống",
                    Action = a.Action,
                    ActionDisplay = AuditActions.GetDisplayName(a.Action),
                    EntityType = a.EntityType,
                    EntityTypeDisplay = AuditEntityTypes.GetDisplayName(a.EntityType),
                    EntityId = a.EntityId,
                    Before = a.Before,
                    After = a.After,
                    CreatedAt = a.CreatedAt,
                    IpAddress = a.IpAddress
                })
                .ToListAsync();
        }

        public async Task<List<AuditLogViewModel>> GetRecentLogsAsync(int count = 20)
        {
            using var context = new LearningPlatformContext();

            return await context.AuditLogs
                .Include(a => a.User)
                .OrderByDescending(a => a.CreatedAt)
                .Take(count)
                .Select(a => new AuditLogViewModel
                {
                    AuditId = a.AuditId,
                    UserId = a.UserId,
                    Username = a.User != null ? a.User.Username : "System",
                    FullName = a.User != null ? a.User.FullName : "Hệ thống",
                    Action = a.Action,
                    ActionDisplay = AuditActions.GetDisplayName(a.Action),
                    EntityType = a.EntityType,
                    EntityTypeDisplay = AuditEntityTypes.GetDisplayName(a.EntityType),
                    EntityId = a.EntityId,
                    Before = a.Before,
                    After = a.After,
                    CreatedAt = a.CreatedAt,
                    IpAddress = a.IpAddress
                })
                .ToListAsync();
        }

        public async Task<List<AuditLogViewModel>> SearchLogsAsync(string keyword, int count = 50)
        {
            using var context = new LearningPlatformContext();

            var lowerKeyword = keyword.ToLower();

            return await context.AuditLogs
                .Include(a => a.User)
                .Where(a =>
                    a.Action.ToLower().Contains(lowerKeyword) ||
                    a.EntityType.ToLower().Contains(lowerKeyword) ||
                    (a.After != null && a.After.ToLower().Contains(lowerKeyword)) ||
                    (a.User != null && (a.User.Username.ToLower().Contains(lowerKeyword) ||
                                         a.User.FullName.ToLower().Contains(lowerKeyword))))
                .OrderByDescending(a => a.CreatedAt)
                .Take(count)
                .Select(a => new AuditLogViewModel
                {
                    AuditId = a.AuditId,
                    UserId = a.UserId,
                    Username = a.User != null ? a.User.Username : "System",
                    FullName = a.User != null ? a.User.FullName : "Hệ thống",
                    Action = a.Action,
                    ActionDisplay = AuditActions.GetDisplayName(a.Action),
                    EntityType = a.EntityType,
                    EntityTypeDisplay = AuditEntityTypes.GetDisplayName(a.EntityType),
                    EntityId = a.EntityId,
                    Before = a.Before,
                    After = a.After,
                    CreatedAt = a.CreatedAt,
                    IpAddress = a.IpAddress
                })
                .ToListAsync();
        }

        #endregion

        #region Thống kê

        public async Task<AuditLogStatistics> GetStatisticsAsync(DateTime? startDate = null, DateTime? endDate = null)
        {
            using var context = new LearningPlatformContext();

            var now = DateTime.UtcNow;
            var today = now.Date;
            var weekAgo = today.AddDays(-7);
            var monthAgo = today.AddMonths(-1);

            var query = context.AuditLogs.AsQueryable();

            if (startDate.HasValue)
                query = query.Where(a => a.CreatedAt >= startDate.Value);
            if (endDate.HasValue)
                query = query.Where(a => a.CreatedAt <= endDate.Value);

            var stats = new AuditLogStatistics
            {
                TotalLogs = await query.CountAsync(),
                LogsToday = await context.AuditLogs.CountAsync(a => a.CreatedAt >= today),
                LogsThisWeek = await context.AuditLogs.CountAsync(a => a.CreatedAt >= weekAgo),
                LogsThisMonth = await context.AuditLogs.CountAsync(a => a.CreatedAt >= monthAgo)
            };

            // Action counts
            var actionGroups = await query
                .GroupBy(a => a.Action)
                .Select(g => new { Action = g.Key, Count = g.Count() })
                .ToListAsync();
            stats.ActionCounts = actionGroups.ToDictionary(x => x.Action, x => x.Count);

            // Entity type counts
            var entityGroups = await query
                .GroupBy(a => a.EntityType)
                .Select(g => new { EntityType = g.Key, Count = g.Count() })
                .ToListAsync();
            stats.EntityTypeCounts = entityGroups.ToDictionary(x => x.EntityType, x => x.Count);

            // Top active users
            var userGroups = await query
                .Where(a => a.UserId.HasValue)
                .GroupBy(a => new { a.UserId, a.User.Username })
                .Select(g => new { Username = g.Key.Username ?? "Unknown", Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .Take(10)
                .ToListAsync();
            stats.TopActiveUsers = userGroups.Select(x => (x.Username, x.Count)).ToList();

            // Logs by day (last 7 days)
            var dayGroups = await context.AuditLogs
                .Where(a => a.CreatedAt >= weekAgo)
                .GroupBy(a => a.CreatedAt.Date)
                .Select(g => new { Date = g.Key, Count = g.Count() })
                .ToListAsync();
            stats.LogsByDay = dayGroups.ToDictionary(
                x => x.Date.ToString("dd/MM"),
                x => x.Count);

            return stats;
        }

        public async Task<Dictionary<string, int>> GetStatsByUserAsync(DateTime? startDate = null, DateTime? endDate = null, int topCount = 10)
        {
            using var context = new LearningPlatformContext();

            var query = context.AuditLogs.Where(a => a.UserId.HasValue);

            if (startDate.HasValue)
                query = query.Where(a => a.CreatedAt >= startDate.Value);
            if (endDate.HasValue)
                query = query.Where(a => a.CreatedAt <= endDate.Value);

            var groups = await query
                .GroupBy(a => new { a.UserId, a.User.Username })
                .Select(g => new { Username = g.Key.Username ?? "Unknown", Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .Take(topCount)
                .ToListAsync();

            return groups.ToDictionary(x => x.Username, x => x.Count);
        }

        public async Task<Dictionary<string, int>> GetStatsByActionAsync(DateTime? startDate = null, DateTime? endDate = null)
        {
            using var context = new LearningPlatformContext();

            var query = context.AuditLogs.AsQueryable();

            if (startDate.HasValue)
                query = query.Where(a => a.CreatedAt >= startDate.Value);
            if (endDate.HasValue)
                query = query.Where(a => a.CreatedAt <= endDate.Value);

            var groups = await query
                .GroupBy(a => a.Action)
                .Select(g => new { Action = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .ToListAsync();

            return groups.ToDictionary(x => x.Action, x => x.Count);
        }

        public async Task<Dictionary<string, int>> GetStatsByEntityTypeAsync(DateTime? startDate = null, DateTime? endDate = null)
        {
            using var context = new LearningPlatformContext();

            var query = context.AuditLogs.AsQueryable();

            if (startDate.HasValue)
                query = query.Where(a => a.CreatedAt >= startDate.Value);
            if (endDate.HasValue)
                query = query.Where(a => a.CreatedAt <= endDate.Value);

            var groups = await query
                .GroupBy(a => a.EntityType)
                .Select(g => new { EntityType = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .ToListAsync();

            return groups.ToDictionary(x => x.EntityType, x => x.Count);
        }

        public async Task<Dictionary<string, int>> GetLogCountByDayAsync(int days = 7)
        {
            using var context = new LearningPlatformContext();

            var startDate = DateTime.UtcNow.Date.AddDays(-days + 1);
            
            var groups = await context.AuditLogs
                .Where(a => a.CreatedAt >= startDate)
                .GroupBy(a => a.CreatedAt.Date)
                .Select(g => new { Date = g.Key, Count = g.Count() })
                .ToListAsync();

            // Fill in missing days with 0
            var result = new Dictionary<string, int>();
            for (int i = 0; i < days; i++)
            {
                var date = startDate.AddDays(i);
                var key = date.ToString("dd/MM");
                var group = groups.FirstOrDefault(g => g.Date == date);
                result[key] = group?.Count ?? 0;
            }

            return result;
        }

        public async Task<Dictionary<int, int>> GetLogCountByHourAsync(DateTime date)
        {
            using var context = new LearningPlatformContext();

            var startOfDay = date.Date;
            var endOfDay = startOfDay.AddDays(1);

            var groups = await context.AuditLogs
                .Where(a => a.CreatedAt >= startOfDay && a.CreatedAt < endOfDay)
                .GroupBy(a => a.CreatedAt.Hour)
                .Select(g => new { Hour = g.Key, Count = g.Count() })
                .ToListAsync();

            // Fill in missing hours with 0
            var result = new Dictionary<int, int>();
            for (int i = 0; i < 24; i++)
            {
                var group = groups.FirstOrDefault(g => g.Hour == i);
                result[i] = group?.Count ?? 0;
            }

            return result;
        }

        #endregion

        #region Quản lý

        public async Task<int> DeleteOldLogsAsync(int daysToKeep = 90)
        {
            using var context = new LearningPlatformContext();

            var cutoffDate = DateTime.UtcNow.AddDays(-daysToKeep);

            var oldLogs = await context.AuditLogs
                .Where(a => a.CreatedAt < cutoffDate)
                .ToListAsync();

            var count = oldLogs.Count;
            
            if (count > 0)
            {
                context.AuditLogs.RemoveRange(oldLogs);
                await context.SaveChangesAsync();
            }

            return count;
        }

        public async Task<byte[]> ExportLogsAsync(AuditLogFilter filter, string format = "csv")
        {
            var result = await GetLogsAsync(new AuditLogFilter
            {
                StartDate = filter.StartDate,
                EndDate = filter.EndDate,
                UserId = filter.UserId,
                Action = filter.Action,
                EntityType = filter.EntityType,
                SearchKeyword = filter.SearchKeyword,
                PageNumber = 1,
                PageSize = int.MaxValue
            });

            if (format.ToLower() == "csv")
            {
                return ExportToCsv(result.Items);
            }

            return ExportToJson(result.Items);
        }

        public async Task<List<string>> GetDistinctActionsAsync()
        {
            using var context = new LearningPlatformContext();

            return await context.AuditLogs
                .Select(a => a.Action)
                .Distinct()
                .OrderBy(a => a)
                .ToListAsync();
        }

        public async Task<List<string>> GetDistinctEntityTypesAsync()
        {
            using var context = new LearningPlatformContext();

            return await context.AuditLogs
                .Select(a => a.EntityType)
                .Distinct()
                .OrderBy(e => e)
                .ToListAsync();
        }

        #endregion

        #region Helper Methods

        private static string GetClientIpAddress()
        {
            // In WinForms, we don't have HTTP context
            // Return localhost or machine name
            try
            {
                return System.Net.Dns.GetHostName();
            }
            catch
            {
                return "127.0.0.1";
            }
        }

        private byte[] ExportToCsv(List<AuditLogViewModel> logs)
        {
            var sb = new StringBuilder();
            
            // Header
            sb.AppendLine("ID,Thời gian,Người dùng,Hành động,Loại đối tượng,ID đối tượng,Chi tiết,IP");

            // Data rows
            foreach (var log in logs)
            {
                sb.AppendLine($"{log.AuditId},{log.CreatedAt:yyyy-MM-dd HH:mm:ss},\"{log.Username}\",\"{log.ActionDisplay}\",\"{log.EntityTypeDisplay}\",{log.EntityId ?? 0},\"{EscapeCsv(log.After)}\",\"{log.IpAddress}\"");
            }

            return Encoding.UTF8.GetBytes(sb.ToString());
        }

        private byte[] ExportToJson(List<AuditLogViewModel> logs)
        {
            var json = JsonSerializer.Serialize(logs, new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            return Encoding.UTF8.GetBytes(json);
        }

        private static string EscapeCsv(string value)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            return value.Replace("\"", "\"\"").Replace("\n", " ").Replace("\r", " ");
        }

        #endregion
    }
}
