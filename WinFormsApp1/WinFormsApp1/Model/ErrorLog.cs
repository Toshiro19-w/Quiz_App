using System;
using System.Collections.Generic;

namespace WinFormsApp1.Model;

public partial class ErrorLog
{
    public int ErrorId { get; set; }

    public string Severity { get; set; } = null!;

    public string Message { get; set; } = null!;

    public string? Stack { get; set; }

    public string? Context { get; set; }

    public DateTime CreatedAt { get; set; }
}
