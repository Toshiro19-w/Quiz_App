using System;
using System.Collections.Generic;

namespace WinFormsApp1.Model;

public partial class CoursePurchase
{
    public int PurchaseId { get; set; }

    public int CourseId { get; set; }

    public int BuyerId { get; set; }

    public decimal PricePaid { get; set; }

    public string Currency { get; set; } = null!;

    public string Status { get; set; } = null!;

    public DateTime PurchasedAt { get; set; }
}
