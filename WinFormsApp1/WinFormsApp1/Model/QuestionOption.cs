using System;
using System.Collections.Generic;

namespace WinFormsApp1.Model;

public partial class QuestionOption
{
    public int OptionId { get; set; }

    public int QuestionId { get; set; }

    public string OptionText { get; set; } = null!;

    public int? OptionMediaId { get; set; }

    public bool IsCorrect { get; set; }

    public int OrderIndex { get; set; }

    public virtual File? OptionMedia { get; set; }

    public virtual Question Question { get; set; } = null!;
}
