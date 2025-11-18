using System;

namespace WinFormsApp1.Models.ViewModels
{
    public class CourseRevenueViewModel
    {
        public int CourseId { get; set; }
        public string CourseTitle { get; set; }
        public decimal CoursePrice { get; set; }
        public int TotalPurchases { get; set; }
        public decimal GrossRevenue { get; set; }        // T?ng doanh thu (100%)
        public decimal InstructorRevenue { get; set; }   // Thu nh?p gi?ng viên (60%)
        public decimal PlatformFee { get; set; }         // Phí n?n t?ng (40%)
    }

    public class RevenueOverviewViewModel
    {
        public int TotalPurchases { get; set; }
        public decimal TotalGrossRevenue { get; set; }
        public decimal TotalInstructorRevenue { get; set; }
        public decimal TotalPlatformFee { get; set; }
    }
}
