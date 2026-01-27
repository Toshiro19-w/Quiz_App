using System;
using System.Collections.Generic;

namespace WinFormsApp1.Models.Entities;

public partial class UserSubscription
{
    public int SubscriptionId { get; set; }

    public int UserId { get; set; }

    public string Status { get; set; } = null!;

    public DateTime SubscribedAt { get; set; }

    public DateTime ExpiresAt { get; set; }

    public virtual User User { get; set; } = null!;
}
