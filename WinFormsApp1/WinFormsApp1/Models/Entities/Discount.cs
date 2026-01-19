using System;
using System.Collections.Generic;

namespace WinFormsApp1.Models.Entities;

/// <summary>
/// Entity đại diện cho mã giảm giá/voucher
/// </summary>
public partial class Discount
{
    public int DiscountId { get; set; }

    /// <summary>
    /// Mã giảm giá (VD: "SALE20", "WELCOME10")
    /// </summary>
    public string Code { get; set; } = null!;

    /// <summary>
    /// Tên mã giảm giá
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Mô tả chi tiết
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Loại giảm giá: Percentage (%) hoặc FixedAmount (số tiền cố định)
    /// </summary>
    public string DiscountType { get; set; } = "Percentage";

    /// <summary>
    /// Giá trị giảm (VD: 20 cho 20% hoặc 50000 cho 50,000đ)
    /// </summary>
    public decimal DiscountValue { get; set; }

    /// <summary>
    /// Giá trị đơn hàng tối thiểu để áp dụng
    /// </summary>
    public decimal? MinOrderAmount { get; set; }

    /// <summary>
    /// Số tiền giảm tối đa (chỉ áp dụng cho Percentage)
    /// </summary>
    public decimal? MaxDiscountAmount { get; set; }

    /// <summary>
    /// Ngày bắt đầu hiệu lực
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Ngày kết thúc hiệu lực
    /// </summary>
    public DateTime EndDate { get; set; }

    /// <summary>
    /// Số lần sử dụng tối đa (null = không giới hạn)
    /// </summary>
    public int? UsageLimit { get; set; }

    /// <summary>
    /// Số lần đã sử dụng
    /// </summary>
    public int UsageCount { get; set; } = 0;

    /// <summary>
    /// Số lần sử dụng tối đa mỗi user (null = không giới hạn)
    /// </summary>
    public int? UsageLimitPerUser { get; set; }

    /// <summary>
    /// Áp dụng cho tất cả khóa học hay chỉ một số khóa học nhất định
    /// </summary>
    public bool ApplyToAllCourses { get; set; } = true;

    /// <summary>
    /// Trạng thái: Active, Inactive, Expired
    /// </summary>
    public string Status { get; set; } = "Active";

    /// <summary>
    /// Mã giảm giá có đang hoạt động không (computed from Status)
    /// </summary>
    public bool IsActive => Status == "Active";

    /// <summary>
    /// Người tạo mã giảm giá
    /// </summary>
    public int CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    // Navigation properties
    public virtual User Creator { get; set; } = null!;

    public virtual ICollection<DiscountUsage> DiscountUsages { get; set; } = new List<DiscountUsage>();

    public virtual ICollection<DiscountCourse> DiscountCourses { get; set; } = new List<DiscountCourse>();
}
