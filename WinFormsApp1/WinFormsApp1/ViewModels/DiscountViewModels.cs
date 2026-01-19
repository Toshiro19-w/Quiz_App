using System;
using System.ComponentModel.DataAnnotations;

namespace WinFormsApp1.ViewModels
{
    /// <summary>
    /// ViewModel hiển thị thông tin mã giảm giá
    /// </summary>
    public class DiscountViewModel
    {
        public int DiscountId { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string DiscountType { get; set; } = "Percentage";
        public decimal DiscountValue { get; set; }
        public decimal? MinOrderAmount { get; set; }
        public decimal? MaxDiscountAmount { get; set; }
        public int? UsageLimit { get; set; }
        public int UsageCount { get; set; }
        public int? UsageLimitPerUser { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; } = "Active";
        public bool IsActive { get; set; }
        public bool ApplyToAllCourses { get; set; }
        public string CreatorName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Hiển thị giá trị giảm (VD: "20%" hoặc "50,000đ")
        /// </summary>
        public string DisplayValue => DiscountType == "Percentage" 
            ? $"{DiscountValue}%" 
            : $"{DiscountValue:N0}đ";

        /// <summary>
        /// Tính toán trạng thái thực tế dựa trên thời gian hiện tại
        /// </summary>
        public string RealTimeStatus
        {
            get
            {
                var now = DateTime.Now;
                
                // Nếu đã bị tắt thủ công (Inactive) thì giữ nguyên
                if (Status == "Inactive" || !IsActive)
                    return "Inactive";
                
                // Kiểm tra đã hết hạn chưa
                if (now > EndDate)
                    return "Expired";
                
                // Kiểm tra đã hết lượt sử dụng chưa
                if (UsageLimit.HasValue && UsageCount >= UsageLimit.Value)
                    return "Exhausted";
                
                // Kiểm tra chưa đến ngày bắt đầu
                if (now < StartDate)
                    return "Pending";
                
                // Đang hoạt động
                return "Active";
            }
        }

        /// <summary>
        /// Hiển thị trạng thái tiếng Việt (dựa trên thời gian thực)
        /// </summary>
        public string StatusDisplay => RealTimeStatus switch
        {
            "Active" => "Hoạt động",
            "Inactive" => "Tạm dừng",
            "Expired" => "Hết hạn",
            "Exhausted" => "Hết lượt",
            "Pending" => "Chưa bắt đầu",
            _ => Status
        };

        /// <summary>
        /// Hiển thị loại giảm giá
        /// </summary>
        public string TypeDisplay => DiscountType == "Percentage" ? "Phần trăm" : "Số tiền cố định";

        /// <summary>
        /// Còn hiệu lực không (dựa trên thời gian thực)
        /// </summary>
        public bool IsCurrentlyActive => RealTimeStatus == "Active";

        /// <summary>
        /// Còn lượt sử dụng không
        /// </summary>
        public bool HasAvailableUsage => !UsageLimit.HasValue || UsageCount < UsageLimit.Value;

        /// <summary>
        /// Số lượt còn lại
        /// </summary>
        public string RemainingUsage => UsageLimit.HasValue 
            ? $"{UsageLimit.Value - UsageCount}/{UsageLimit.Value}" 
            : "Không giới hạn";
        
        /// <summary>
        /// Thời gian còn lại (nếu đang hoạt động)
        /// </summary>
        public string TimeRemaining
        {
            get
            {
                if (RealTimeStatus != "Active" && RealTimeStatus != "Pending")
                    return "-";
                
                var now = DateTime.Now;
                var targetDate = RealTimeStatus == "Pending" ? StartDate : EndDate;
                var remaining = targetDate - now;
                
                if (remaining.TotalDays >= 1)
                    return $"{(int)remaining.TotalDays} ngày";
                if (remaining.TotalHours >= 1)
                    return $"{(int)remaining.TotalHours} giờ";
                if (remaining.TotalMinutes >= 1)
                    return $"{(int)remaining.TotalMinutes} phút";
                
                return "< 1 phút";
            }
        }
    }

    /// <summary>
    /// ViewModel tạo/sửa mã giảm giá
    /// </summary>
    public class CreateDiscountViewModel
    {
        [Required(ErrorMessage = "Mã giảm giá là bắt buộc")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Mã từ 3-50 ký tự")]
        [RegularExpression(@"^[A-Za-z0-9]+$", ErrorMessage = "Mã chỉ chứa chữ và số")]
        public string Code { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tên là bắt buộc")]
        [StringLength(200, ErrorMessage = "Tên tối đa 200 ký tự")]
        public string Name { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Mô tả tối đa 500 ký tự")]
        public string? Description { get; set; }

        [Required]
        public string DiscountType { get; set; } = "Percentage";

        [Required(ErrorMessage = "Giá trị giảm là bắt buộc")]
        [Range(0.01, 999999999, ErrorMessage = "Giá trị phải lớn hơn 0")]
        public decimal DiscountValue { get; set; }

        [Range(0, 999999999)]
        public decimal? MinOrderAmount { get; set; }

        [Range(0, 999999999)]
        public decimal? MaxDiscountAmount { get; set; }

        [Range(1, int.MaxValue)]
        public int? UsageLimit { get; set; }

        [Range(1, int.MaxValue)]
        public int? UsageLimitPerUser { get; set; }

        [Required(ErrorMessage = "Ngày bắt đầu là bắt buộc")]
        public DateTime StartDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Ngày kết thúc là bắt buộc")]
        public DateTime EndDate { get; set; } = DateTime.Now.AddMonths(1);

        public string Status { get; set; } = "Active";

        public bool ApplyToAllCourses { get; set; } = true;
    }

    /// <summary>
    /// Filter cho danh sách mã giảm giá
    /// </summary>
    public class DiscountFilterViewModel
    {
        public string? SearchKeyword { get; set; }
        public string? Status { get; set; }
        public string? DiscountType { get; set; }
        public DateTime? StartDateFrom { get; set; }
        public DateTime? StartDateTo { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }

    /// <summary>
    /// Thống kê mã giảm giá
    /// </summary>
    public class DiscountStatisticsViewModel
    {
        public int TotalDiscounts { get; set; }
        public int ActiveDiscounts { get; set; }
        public int ExpiredDiscounts { get; set; }
        public decimal TotalDiscountGiven { get; set; }
        public int TotalUsages { get; set; }
    }
}
