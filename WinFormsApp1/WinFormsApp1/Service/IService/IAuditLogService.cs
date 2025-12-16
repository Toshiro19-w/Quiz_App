using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WinFormsApp1.Models.Entities;
using WinFormsApp1.ViewModels;

namespace WinFormsApp1.Service.IService
{
    /// <summary>
    /// Interface cho AuditLog Service
    /// </summary>
    public interface IAuditLogService
    {
        #region Ghi Log

        /// <summary>
        /// Ghi log hành động
        /// </summary>
        Task<bool> LogAsync(
            string action,
            string entityType,
            int? entityId = null,
            string beforeData = null,
            string afterData = null,
            string severity = "Info",
            int? userId = null);

        /// <summary>
        /// Ghi log hành động với object data (tự động serialize)
        /// </summary>
        Task<bool> LogAsync<TBefore, TAfter>(
            string action,
            string entityType,
            int? entityId,
            TBefore beforeData,
            TAfter afterData,
            string severity = "Info",
            int? userId = null);

        /// <summary>
        /// Ghi log đăng nhập
        /// </summary>
        Task<bool> LogLoginAsync(int userId, bool success, string details = null);

        /// <summary>
        /// Ghi log đăng xuất
        /// </summary>
        Task<bool> LogLogoutAsync(int userId);

        /// <summary>
        /// Ghi log lỗi
        /// </summary>
        Task<bool> LogErrorAsync(string action, string entityType, int? entityId, Exception ex);

        #endregion

        #region Truy vấn

        /// <summary>
        /// Lấy danh sách log có phân trang và lọc
        /// </summary>
        Task<AuditLogPagedResult> GetLogsAsync(AuditLogFilter filter);

        /// <summary>
        /// Lấy log theo ID
        /// </summary>
        Task<AuditLogViewModel> GetLogByIdAsync(int auditId);

        /// <summary>
        /// Lấy log theo User
        /// </summary>
        Task<List<AuditLogViewModel>> GetLogsByUserAsync(int userId, int count = 50);

        /// <summary>
        /// Lấy log theo Entity
        /// </summary>
        Task<List<AuditLogViewModel>> GetLogsByEntityAsync(string entityType, int entityId, int count = 50);

        /// <summary>
        /// Lấy log gần đây
        /// </summary>
        Task<List<AuditLogViewModel>> GetRecentLogsAsync(int count = 20);

        /// <summary>
        /// Tìm kiếm log
        /// </summary>
        Task<List<AuditLogViewModel>> SearchLogsAsync(string keyword, int count = 50);

        #endregion

        #region Thống kê

        /// <summary>
        /// Lấy thống kê tổng quan
        /// </summary>
        Task<AuditLogStatistics> GetStatisticsAsync(DateTime? startDate = null, DateTime? endDate = null);

        /// <summary>
        /// Lấy thống kê theo User
        /// </summary>
        Task<Dictionary<string, int>> GetStatsByUserAsync(DateTime? startDate = null, DateTime? endDate = null, int topCount = 10);

        /// <summary>
        /// Lấy thống kê theo Action
        /// </summary>
        Task<Dictionary<string, int>> GetStatsByActionAsync(DateTime? startDate = null, DateTime? endDate = null);

        /// <summary>
        /// Lấy thống kê theo Entity Type
        /// </summary>
        Task<Dictionary<string, int>> GetStatsByEntityTypeAsync(DateTime? startDate = null, DateTime? endDate = null);

        /// <summary>
        /// Lấy số lượng log theo ngày
        /// </summary>
        Task<Dictionary<string, int>> GetLogCountByDayAsync(int days = 7);

        /// <summary>
        /// Lấy số lượng log theo giờ trong ngày
        /// </summary>
        Task<Dictionary<int, int>> GetLogCountByHourAsync(DateTime date);

        #endregion

        #region Quản lý

        /// <summary>
        /// Xóa log cũ
        /// </summary>
        Task<int> DeleteOldLogsAsync(int daysToKeep = 90);

        /// <summary>
        /// Xuất log ra file
        /// </summary>
        Task<byte[]> ExportLogsAsync(AuditLogFilter filter, string format = "csv");

        /// <summary>
        /// Lấy danh sách các action đã ghi log
        /// </summary>
        Task<List<string>> GetDistinctActionsAsync();

        /// <summary>
        /// Lấy danh sách các entity type đã ghi log
        /// </summary>
        Task<List<string>> GetDistinctEntityTypesAsync();

        #endregion
    }
}
