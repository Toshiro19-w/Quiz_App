using System;
using System.Collections.Generic;

namespace WinFormsApp1.Model;

public partial class NotificationChannel
{
    public int ChannelId { get; set; }

    public int UserId { get; set; }

    public string Channel { get; set; } = null!;

    public bool Enabled { get; set; }

    public string? AddressOrToken { get; set; }

    public DateTime CreatedAt { get; set; }
}
