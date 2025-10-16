using System;
using System.Collections.Generic;

namespace WinFormsApp1.Model;

public partial class UserSetting
{
    public int UserId { get; set; }

    public string? UiTheme { get; set; }

    public string? Language { get; set; }

    public string? TimeZone { get; set; }

    public bool EmailOptIn { get; set; }

    public bool PushOptIn { get; set; }
}
