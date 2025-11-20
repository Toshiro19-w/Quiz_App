using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using WinFormsApp1.Models.Entities;
using WinFormsApp1.ViewModels;
using File = System.IO.File;

namespace WinFormsApp1.Helpers
{
    public static class ReportHelper
    {
        public static void GenerateUserReport(ReportViewer reportViewer, List<User> users)
        {
            var reportPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reports", "UserReport.rdlc");
            
            if (!File.Exists(reportPath))
            {
                throw new FileNotFoundException($"Không tìm thấy file báo cáo: {reportPath}");
            }
            
            reportViewer.LocalReport.ReportPath = reportPath;
            
            var dataTable = new DataTable();
            dataTable.Columns.Add("UserId", typeof(int));
            dataTable.Columns.Add("Username", typeof(string));
            dataTable.Columns.Add("Email", typeof(string));
            dataTable.Columns.Add("FullName", typeof(string));
            dataTable.Columns.Add("RoleName", typeof(string));
            dataTable.Columns.Add("CreatedAt", typeof(DateTime));

            foreach (var user in users)
            {
                dataTable.Rows.Add(
                    user.UserId,
                    user.Username,
                    user.Email,
                    user.FullName,
                    user.Role?.Name ?? "N/A",
                    user.CreatedAt
                );
            }

            var dataSource = new ReportDataSource("UserDataSet", dataTable);
            reportViewer.LocalReport.DataSources.Clear();
            reportViewer.LocalReport.DataSources.Add(dataSource);
            reportViewer.RefreshReport();
        }

        public static void ExportToPDF(ReportViewer reportViewer, string filePath)
        {
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;
            
            byte[] bytes = reportViewer.LocalReport.Render(
                "PDF", null, out mimeType, out encoding, out extension,
                out streamids, out warnings);
            
            File.WriteAllBytes(filePath, bytes);
        }

        public static void ExportToExcel(ReportViewer reportViewer, string filePath)
        {
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;
            
            byte[] bytes = reportViewer.LocalReport.Render(
                "EXCELOPENXML", null, out mimeType, out encoding, out extension,
                out streamids, out warnings);
            
            File.WriteAllBytes(filePath, bytes);
        }

        public static void GenerateCourseReport(ReportViewer reportViewer, List<Course> courses)
        {
            var reportPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reports", "CourseReport.rdlc");
            
            if (!File.Exists(reportPath))
            {
                throw new FileNotFoundException($"Không tìm thấy file báo cáo: {reportPath}");
            }
            
            reportViewer.LocalReport.ReportPath = reportPath;
            
            var dataTable = new DataTable();
            dataTable.Columns.Add("CourseId", typeof(int));
            dataTable.Columns.Add("Title", typeof(string));
            dataTable.Columns.Add("Price", typeof(decimal));
            dataTable.Columns.Add("IsPublished", typeof(string));
            dataTable.Columns.Add("CreatedAt", typeof(DateTime));

            foreach (var course in courses)
            {
                dataTable.Rows.Add(
                    course.CourseId,
                    course.Title,
                    course.Price,
                    course.IsPublished ? "Đã xuất bản" : "Chưa xuất bản",
                    course.CreatedAt
                );
            }

            var dataSource = new ReportDataSource("CourseDataSet", dataTable);
            reportViewer.LocalReport.DataSources.Clear();
            reportViewer.LocalReport.DataSources.Add(dataSource);
            reportViewer.RefreshReport();
        }

        public static void GenerateTestReport(ReportViewer reportViewer, List<Test> tests)
        {
            var reportPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reports", "TestReport.rdlc");
            
            if (!File.Exists(reportPath))
            {
                throw new FileNotFoundException($"Không tìm thấy file báo cáo: {reportPath}");
            }
            
            reportViewer.LocalReport.ReportPath = reportPath;
            
            var dataTable = new DataTable();
            dataTable.Columns.Add("TestId", typeof(int));
            dataTable.Columns.Add("Title", typeof(string));
            dataTable.Columns.Add("QuestionCount", typeof(int));
            dataTable.Columns.Add("CreatedAt", typeof(DateTime));

            foreach (var test in tests)
            {
                dataTable.Rows.Add(
                    test.TestId,
                    test.Title,
                    test.Questions?.Count ?? 0,
                    test.CreatedAt
                );
            }

            var dataSource = new ReportDataSource("TestDataSet", dataTable);
            reportViewer.LocalReport.DataSources.Clear();
            reportViewer.LocalReport.DataSources.Add(dataSource);
            reportViewer.RefreshReport();
        }

        public static void GenerateRevenueReport(ReportViewer reportViewer, List<CourseRevenueViewModel> revenueData)
        {
            var reportPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reports", "RevenueReport.rdlc");
            
            if (!File.Exists(reportPath))
            {
                throw new FileNotFoundException($"Không tìm thấy file báo cáo: {reportPath}");
            }
            
            reportViewer.LocalReport.ReportPath = reportPath;
            
            var dataTable = new DataTable();
            dataTable.Columns.Add("CourseId", typeof(int));
            dataTable.Columns.Add("CourseTitle", typeof(string));
            dataTable.Columns.Add("CoursePrice", typeof(decimal));
            dataTable.Columns.Add("TotalPurchases", typeof(int));
            dataTable.Columns.Add("GrossRevenue", typeof(decimal));
            dataTable.Columns.Add("InstructorRevenue", typeof(decimal));
            dataTable.Columns.Add("PlatformFee", typeof(decimal));

            foreach (var item in revenueData)
            {
                dataTable.Rows.Add(
                    item.CourseId,
                    item.CourseTitle,
                    item.CoursePrice,
                    item.TotalPurchases,
                    item.GrossRevenue,
                    item.InstructorRevenue,
                    item.PlatformFee
                );
            }

            var dataSource = new ReportDataSource("RevenueDataSet", dataTable);
            reportViewer.LocalReport.DataSources.Clear();
            reportViewer.LocalReport.DataSources.Add(dataSource);
            reportViewer.RefreshReport();
        }

        public static void GenerateRevenueReport(ReportViewer reportViewer, RevenueAnalytics revenue)
        {
            var reportPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reports", "RevenueReport.rdlc");
            
            if (!File.Exists(reportPath))
            {
                throw new FileNotFoundException($"Không tìm thấy file báo cáo: {reportPath}");
            }
            
            reportViewer.LocalReport.ReportPath = reportPath;
            
            var dataTable = new DataTable();
            dataTable.Columns.Add("CourseId", typeof(int));
            dataTable.Columns.Add("CourseTitle", typeof(string));
            dataTable.Columns.Add("CoursePrice", typeof(decimal));
            dataTable.Columns.Add("TotalPurchases", typeof(int));
            dataTable.Columns.Add("GrossRevenue", typeof(decimal));
            dataTable.Columns.Add("InstructorRevenue", typeof(decimal));
            dataTable.Columns.Add("PlatformFee", typeof(decimal));

            dataTable.Rows.Add(0, "Tổng doanh thu", 0, revenue.PaidCount, revenue.TotalRevenue, revenue.PaidAmount, revenue.PendingAmount);

            var dataSource = new ReportDataSource("RevenueDataSet", dataTable);
            reportViewer.LocalReport.DataSources.Clear();
            reportViewer.LocalReport.DataSources.Add(dataSource);
            reportViewer.RefreshReport();
        }

        public static void GenerateSystemReport(ReportViewer reportViewer, SystemAnalytics system)
        {
            var reportPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reports", "SystemReport.rdlc");
            
            if (!File.Exists(reportPath))
            {
                throw new FileNotFoundException($"Không tìm thấy file báo cáo: {reportPath}");
            }
            
            reportViewer.LocalReport.ReportPath = reportPath;
            
            var dataTable = new DataTable();
            dataTable.Columns.Add("Metric", typeof(string));
            dataTable.Columns.Add("Value", typeof(string));

            dataTable.Rows.Add("Tổng thông báo", system.TotalNotifications.ToString());
            dataTable.Rows.Add("Đã gửi", system.NotificationsSent.ToString());
            dataTable.Rows.Add("Chờ gửi", system.NotificationsPending.ToString());
            dataTable.Rows.Add("Tổng nhật ký", system.TotalAuditLogs.ToString());
            dataTable.Rows.Add("Nhật ký hôm nay", system.AuditLogsToday.ToString());
            dataTable.Rows.Add("Yêu cầu hôm nay", system.RequestsToday.ToString());

            var dataSource = new ReportDataSource("SystemDataSet", dataTable);
            reportViewer.LocalReport.DataSources.Clear();
            reportViewer.LocalReport.DataSources.Add(dataSource);
            reportViewer.RefreshReport();
        }
    }
}
