using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WinFormsApp1.Service;
using WinFormsApp1.Service.IService;
using WinFormsApp1.ViewModels;

namespace WinFormsApp1.Controllers.Admin
{
    /// <summary>
    /// Controller quản lý AuditLog cho Admin
    /// </summary>
    public class AuditLogController
    {
        private readonly IAuditLogService _auditLogService;

        public AuditLogController()
        {
            _auditLogService = new AuditLogService();
        }

        public AuditLogController(IAuditLogService auditLogService)
        {
            _auditLogService = auditLogService;
        }

        #region Truy vấn

        /// <summary>
        /// Lấy danh sách log có phân trang
        /// </summary>
        public async Task<AuditLogPagedResult> GetLogsAsync(AuditLogFilter filter)
        {
            try
            {
                return await _auditLogService.GetLogsAsync(filter);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy danh sách log: {ex.Message}");
            }
        }

        /// <summary>
        /// Lấy chi tiết log
        /// </summary>
        public async Task<AuditLogViewModel> GetLogDetailAsync(int auditId)
        {
            try
            {
                var log = await _auditLogService.GetLogByIdAsync(auditId);
                if (log == null)
                    throw new Exception("Không tìm thấy log.");
                return log;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy chi tiết log: {ex.Message}");
            }
        }

        /// <summary>
        /// Lấy log theo User
        /// </summary>
        public async Task<List<AuditLogViewModel>> GetLogsByUserAsync(int userId, int count = 50)
        {
            try
            {
                return await _auditLogService.GetLogsByUserAsync(userId, count);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy log của user: {ex.Message}");
            }
        }

        /// <summary>
        /// Lấy log theo Entity
        /// </summary>
        public async Task<List<AuditLogViewModel>> GetLogsByEntityAsync(string entityType, int entityId, int count = 50)
        {
            try
            {
                return await _auditLogService.GetLogsByEntityAsync(entityType, entityId, count);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy log của entity: {ex.Message}");
            }
        }

        /// <summary>
        /// Lấy log gần đây
        /// </summary>
        public async Task<List<AuditLogViewModel>> GetRecentLogsAsync(int count = 20)
        {
            try
            {
                return await _auditLogService.GetRecentLogsAsync(count);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy log gần đây: {ex.Message}");
            }
        }

        /// <summary>
        /// Tìm kiếm log
        /// </summary>
        public async Task<List<AuditLogViewModel>> SearchLogsAsync(string keyword, int count = 50)
        {
            try
            {
                return await _auditLogService.SearchLogsAsync(keyword, count);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi tìm kiếm log: {ex.Message}");
            }
        }

        #endregion

        #region Thống kê

        /// <summary>
        /// Lấy thống kê tổng quan
        /// </summary>
        public async Task<AuditLogStatistics> GetStatisticsAsync(DateTime? startDate = null, DateTime? endDate = null)
        {
            try
            {
                return await _auditLogService.GetStatisticsAsync(startDate, endDate);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy thống kê: {ex.Message}");
            }
        }

        /// <summary>
        /// Lấy thống kê theo User
        /// </summary>
        public async Task<Dictionary<string, int>> GetStatsByUserAsync(DateTime? startDate = null, DateTime? endDate = null, int topCount = 10)
        {
            try
            {
                return await _auditLogService.GetStatsByUserAsync(startDate, endDate, topCount);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy thống kê user: {ex.Message}");
            }
        }

        /// <summary>
        /// Lấy thống kê theo Action
        /// </summary>
        public async Task<Dictionary<string, int>> GetStatsByActionAsync(DateTime? startDate = null, DateTime? endDate = null)
        {
            try
            {
                return await _auditLogService.GetStatsByActionAsync(startDate, endDate);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy thống kê action: {ex.Message}");
            }
        }

        /// <summary>
        /// Lấy thống kê theo Entity Type
        /// </summary>
        public async Task<Dictionary<string, int>> GetStatsByEntityTypeAsync(DateTime? startDate = null, DateTime? endDate = null)
        {
            try
            {
                return await _auditLogService.GetStatsByEntityTypeAsync(startDate, endDate);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy thống kê entity type: {ex.Message}");
            }
        }

        /// <summary>
        /// Lấy số lượng log theo ngày
        /// </summary>
        public async Task<Dictionary<string, int>> GetLogCountByDayAsync(int days = 7)
        {
            try
            {
                return await _auditLogService.GetLogCountByDayAsync(days);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy thống kê theo ngày: {ex.Message}");
            }
        }

        /// <summary>
        /// Lấy số lượng log theo giờ
        /// </summary>
        public async Task<Dictionary<int, int>> GetLogCountByHourAsync(DateTime date)
        {
            try
            {
                return await _auditLogService.GetLogCountByHourAsync(date);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy thống kê theo giờ: {ex.Message}");
            }
        }

        #endregion

        #region Quản lý

        /// <summary>
        /// Xóa log cũ
        /// </summary>
        public async Task<int> DeleteOldLogsAsync(int daysToKeep = 90)
        {
            try
            {
                return await _auditLogService.DeleteOldLogsAsync(daysToKeep);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi xóa log cũ: {ex.Message}");
            }
        }

        /// <summary>
        /// Xuất log ra file
        /// </summary>
        public async Task<byte[]> ExportLogsAsync(AuditLogFilter filter, string format = "csv")
        {
            try
            {
                return await _auditLogService.ExportLogsAsync(filter, format);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi xuất log: {ex.Message}");
            }
        }

        /// <summary>
        /// Lấy danh sách các action đã có
        /// </summary>
        public async Task<List<string>> GetDistinctActionsAsync()
        {
            try
            {
                return await _auditLogService.GetDistinctActionsAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy danh sách action: {ex.Message}");
            }
        }

        /// <summary>
        /// Lấy danh sách các entity type đã có
        /// </summary>
        public async Task<List<string>> GetDistinctEntityTypesAsync()
        {
            try
            {
                return await _auditLogService.GetDistinctEntityTypesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy danh sách entity type: {ex.Message}");
            }
        }

        #endregion
    }
}
