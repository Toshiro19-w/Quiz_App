using System;
using System.Globalization;
using System.Windows.Forms;

namespace WinFormsApp1.Helpers
{
    /// <summary>
    /// Helper for Vietnamese DateTimePicker formatting
    /// </summary>
    public static class VietnameseDatePickerHelper
    {
        /// <summary>
        /// Configure DateTimePicker to use Vietnamese date format
        /// </summary>
        /// <param name="picker">DateTimePicker to configure</param>
        /// <param name="includeTime">Include time in format (default: false)</param>
        public static void SetVietnameseFormat(DateTimePicker picker, bool includeTime = false)
        {
            picker.Format = DateTimePickerFormat.Custom;
            picker.CustomFormat = includeTime ? "dd/MM/yyyy HH:mm" : "dd/MM/yyyy";
        }

        /// <summary>
        /// Configure multiple DateTimePickers with Vietnamese format
        /// </summary>
        /// <param name="includeTime">Include time in format</param>
        /// <param name="pickers">DateTimePickers to configure</param>
        public static void SetVietnameseFormat(bool includeTime, params DateTimePicker[] pickers)
        {
            foreach (var picker in pickers)
            {
                SetVietnameseFormat(picker, includeTime);
            }
        }

        /// <summary>
        /// Get Vietnamese day of week name
        /// </summary>
        public static string GetVietnameseDayOfWeek(DayOfWeek dayOfWeek)
        {
            return dayOfWeek switch
            {
                DayOfWeek.Monday => "Thứ Hai",
                DayOfWeek.Tuesday => "Thứ Ba",
                DayOfWeek.Wednesday => "Thứ Tư",
                DayOfWeek.Thursday => "Thứ Năm",
                DayOfWeek.Friday => "Thứ Sáu",
                DayOfWeek.Saturday => "Thứ Bảy",
                DayOfWeek.Sunday => "Chủ Nhật",
                _ => ""
            };
        }

        /// <summary>
        /// Get Vietnamese month name
        /// </summary>
        public static string GetVietnameseMonthName(int month)
        {
            return month switch
            {
                1 => "Tháng Một",
                2 => "Tháng Hai",
                3 => "Tháng Ba",
                4 => "Tháng Tư",
                5 => "Tháng Năm",
                6 => "Tháng Sáu",
                7 => "Tháng Bảy",
                8 => "Tháng Tám",
                9 => "Tháng Chín",
                10 => "Tháng Mười",
                11 => "Tháng Mười Một",
                12 => "Tháng Mười Hai",
                _ => ""
            };
        }

        /// <summary>
        /// Format DateTime to Vietnamese long format
        /// Example: "Thứ Hai, 15 Tháng Một 2024"
        /// </summary>
        public static string FormatVietnameseLong(DateTime date)
        {
            var dayOfWeek = GetVietnameseDayOfWeek(date.DayOfWeek);
            var month = GetVietnameseMonthName(date.Month);
            return $"{dayOfWeek}, {date.Day} {month} {date.Year}";
        }

        /// <summary>
        /// Format DateTime to Vietnamese short format
        /// Example: "15/01/2024"
        /// </summary>
        public static string FormatVietnameseShort(DateTime date)
        {
            return date.ToString("dd/MM/yyyy");
        }

        /// <summary>
        /// Format DateTime to Vietnamese with time
        /// Example: "15/01/2024 14:30"
        /// </summary>
        public static string FormatVietnameseWithTime(DateTime date)
        {
            return date.ToString("dd/MM/yyyy HH:mm");
        }

        /// <summary>
        /// Format DateTime to Vietnamese full format
        /// Example: "Thứ Hai, 15/01/2024 14:30"
        /// </summary>
        public static string FormatVietnameseFull(DateTime date)
        {
            var dayOfWeek = GetVietnameseDayOfWeek(date.DayOfWeek);
            return $"{dayOfWeek}, {date:dd/MM/yyyy HH:mm}";
        }

        /// <summary>
        /// Parse Vietnamese date string to DateTime
        /// Supports formats: dd/MM/yyyy, dd-MM-yyyy, dd.MM.yyyy
        /// </summary>
        public static bool TryParseVietnameseDate(string dateString, out DateTime result)
        {
            var formats = new[] { "dd/MM/yyyy", "dd-MM-yyyy", "dd.MM.yyyy", "dd/MM/yyyy HH:mm" };
            return DateTime.TryParseExact(
                dateString,
                formats,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out result);
        }
    }
}
