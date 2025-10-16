using System;
using System.Collections.Generic;

namespace WinFormsApp1.Model;

public partial class Invitation
{
    public int InviteId { get; set; }

    public int InviterId { get; set; }

    public int? ClassId { get; set; }

    public string Email { get; set; } = null!;

    public string? RoleSuggested { get; set; }

    public string Token { get; set; } = null!;

    public DateTime ExpiresAt { get; set; }

    public DateTime? AcceptedAt { get; set; }
}
