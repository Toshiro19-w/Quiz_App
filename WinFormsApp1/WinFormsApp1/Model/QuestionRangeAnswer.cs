using System;
using System.Collections.Generic;

namespace WinFormsApp1.Model;

public partial class QuestionRangeAnswer
{
    public int RangeId { get; set; }

    public int QuestionId { get; set; }

    public decimal MinValue { get; set; }

    public decimal MaxValue { get; set; }

    public decimal? Tolerance { get; set; }

    public virtual Question Question { get; set; } = null!;
}
