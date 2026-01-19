using System;

namespace WinFormsApp1.ViewModels
{
    public class CertificateReportViewModel
    {
        public int CertId { get; set; }
        public string StudentName { get; set; }
        public string CourseTitle { get; set; }
        public string InstructorName { get; set; }
        public DateTime IssuedDate { get; set; }
        public string VerifyCode { get; set; }
        public string Serial { get; set; }
        
        // Format: "30 tháng 3 năm 2025"
        public string FormattedIssuedDate => $"{IssuedDate.Day} tháng {IssuedDate.Month} năm {IssuedDate.Year}";
        
        // URL xác thực chứng chỉ
        public string VerifyUrl => $"learningplatform.com/verify/{VerifyCode}";
    }
}

