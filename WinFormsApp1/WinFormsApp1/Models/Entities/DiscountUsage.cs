using System;

namespace WinFormsApp1.Models.Entities;

/// <summary>
/// Entity lưu lịch sử sử dụng mã giảm giá
/// </summary>
public partial class DiscountUsage
{
    public int UsageId { get; set; }

    public int DiscountId { get; set; }

    public int UserId { get; set; }

    public int OrderId { get; set; }

    /// <summary>
    /// Số tiền được giảm
    /// </summary>
    public decimal DiscountAmount { get; set; }

    public DateTime UsedAt { get; set; }

    // Navigation properties
    public virtual Discount Discount { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}
