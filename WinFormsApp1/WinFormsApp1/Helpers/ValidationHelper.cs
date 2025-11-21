using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WinFormsApp1.Helpers
{
    public static class ValidationHelper
    {
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        public static bool IsValidUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username)) return false;
            return username.Length >= 3 && Regex.IsMatch(username, @"^[a-zA-Z0-9_]+$");
        }

        public static bool IsValidFullName(string fullName)
        {
            return !string.IsNullOrWhiteSpace(fullName) && fullName.Length >= 2;
        }

        public static bool IsValidPrice(decimal price)
        {
            return price >= 0;
        }

        public static bool IsValidTitle(string title)
        {
            return !string.IsNullOrWhiteSpace(title) && title.Length <= 200;
        }

        public static string ValidateUser(string email, string username, string fullName)
        {
            if (!IsValidEmail(email)) return "Email không hợp lệ";
            if (!IsValidUsername(username)) return "Username phải có ít nhất 3 ký tự và chỉ chứa chữ, số, _";
            if (!IsValidFullName(fullName)) return "Họ tên phải có ít nhất 2 ký tự";
            return null;
        }

        public static string ValidateCourse(string title, decimal price, string slug = null)
        {
            if (!IsValidTitle(title)) return "Tiêu đề không được trống và tối đa 200 ký tự";
            if (!IsValidPrice(price)) return "Giá phải lớn hơn hoặc bằng 0";
            if (!string.IsNullOrEmpty(slug) && slug.Length > 200) return "Slug quá dài";
            return null;
        }

        public static string ValidateTitle(string title)
        {
            if (!IsValidTitle(title)) return "Tiêu đề không được trống và tối đa 200 ký tự";
            return null;
        }

        /// <summary>
        /// Mật khẩu tối thiểu 6 ký tự, bao gồm chữ hoa, chữ thường, số và ký tự đặc biệt
        /// </summary>
        public static bool IsValidPassword(string password)
        {
            if (string.IsNullOrEmpty(password)) return false;
            // At least one lowercase, one uppercase, one digit, one special char, minimum length 6
            var pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^A-Za-z0-9]).{6,}$";
            return Regex.IsMatch(password, pattern);
        }

        /// <summary>
        /// Số điện thoại phải có đúng 10 chữ số
        /// </summary>
        public static bool IsValidPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) return false;
            return Regex.IsMatch(phone.Trim(), "^\\d{10}$");
        }

        public static void ShowValidationError(Form parentForm, string message)
        {
            ToastHelper.Show(parentForm, $"❌ {message}");
        }
    }
}