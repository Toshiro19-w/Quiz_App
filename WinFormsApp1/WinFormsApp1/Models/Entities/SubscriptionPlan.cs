using System;
using System.Collections.Generic;

namespace WinFormsApp1.Models.Entities;

public partial class SubscriptionPlan
{
    public int PlanId { get; set; }

    public int DurationMonths { get; set; }

    public decimal Price { get; set; }
}
