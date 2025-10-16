using System;
using System.Collections.Generic;

namespace WinFormsApp1.Model;

public partial class TestAssignment
{
    public int TestAssignId { get; set; }

    public int AssignmentId { get; set; }

    public int TestId { get; set; }

    public DateTime? StartAt { get; set; }

    public DateTime? DueAt { get; set; }

    public int? AttemptsAllowed { get; set; }

    public int? OverrideTimeLimitSec { get; set; }
}
