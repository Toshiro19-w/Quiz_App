using System;
using System.Collections.Generic;

namespace WinFormsApp1.Model;

public partial class CourseSection
{
    public int SectionId { get; set; }

    public int CourseId { get; set; }

    public string Title { get; set; } = null!;

    public int OrderIndex { get; set; }
}
