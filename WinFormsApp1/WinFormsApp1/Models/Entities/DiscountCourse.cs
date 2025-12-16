using System;

namespace WinFormsApp1.Models.Entities;

/// <summary>
/// Entity liên kết mã giảm giá với các khóa học cụ thể
/// </summary>
public partial class DiscountCourse
{
    public int DiscountCourseId { get; set; }

    public int DiscountId { get; set; }

    public int CourseId { get; set; }

    // Navigation properties
    public virtual Discount Discount { get; set; } = null!;

    public virtual Course Course { get; set; } = null!;
}
