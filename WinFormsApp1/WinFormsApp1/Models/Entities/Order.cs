using System;
using System.Collections.Generic;

namespace WinFormsApp1.Models.Entities;

public partial class Order
{
    public int OrderId { get; set; }

    public int BuyerId { get; set; }

    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Tổng tiền gốc trước khi giảm giá
    /// </summary>
    public decimal? OriginalAmount { get; set; }

    /// <summary>
    /// Số tiền được giảm
    /// </summary>
    public decimal? DiscountAmount { get; set; }

    /// <summary>
    /// Mã giảm giá đã áp dụng (nếu có)
    /// </summary>
    public int? DiscountId { get; set; }

    public string Currency { get; set; } = null!;

    public string Status { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? PaidAt { get; set; }

    public virtual User Buyer { get; set; } = null!;

    public virtual Discount? Discount { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<DiscountUsage> DiscountUsages { get; set; } = new List<DiscountUsage>();
}
