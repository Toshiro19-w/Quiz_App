using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WinFormsApp1.Models.EF;
using WinFormsApp1.Models.Entities;

namespace WinFormsApp1.Helpers
{
    public static class AuthHelper
    {
        private static User? _currentUser;

        public static User? CurrentUser => _currentUser;

        public static bool Login(string username, string password)
        {
            using var context = new LearningPlatformContext();
            var user = context.Users
                .Where(u => u.Username == username && u.Status == 1)
                .FirstOrDefault();

            if (user != null && PasswordHelper.VerifyPassword(password, user.PasswordHash))
            {
                _currentUser = user;
                user.LastLoginAt = DateTime.Now;
                context.SaveChanges();
                return true;
            }
            return false;
        }

        public static void Logout()
        {
            _currentUser = null;
            SessionHelper.ClearSession();
        }

        // For testing purposes only
        public static void SetTestUser(User user)
        {
            _currentUser = user;
        }

        public static bool IsAdmin()
        {
            if (_currentUser == null) return false;
            using var context = new LearningPlatformContext();
            var role = context.Roles.FirstOrDefault(r => r.RoleId == _currentUser.RoleId);
            return role?.Name == "Admin";
        }

        public static bool IsUser()
        {
            if (_currentUser == null) return false;
            using var context = new LearningPlatformContext();
            var role = context.Roles.FirstOrDefault(r => r.RoleId == _currentUser.RoleId);
            return role?.Name == "User";
        }

        public static string GetRoleName()
        {
            if (_currentUser == null) return "Guest";
            using var context = new LearningPlatformContext();
            var role = context.Roles.FirstOrDefault(r => r.RoleId == _currentUser.RoleId);
            return role?.Name ?? "Unknown";
        }

        public static bool Register(string username, string email, string fullName, string password, string phone = null)
        {
            using var context = new LearningPlatformContext();
            
            // Kiểm tra email đã tồn tại
            if (context.Users.Any(u => u.Email == email))
                return false;

            // Kiểm tra username đã tồn tại
            if (context.Users.Any(u => u.Username == username))
                return false;

            // Tạo user mới
            var user = new User
            {
                Username = username,
                Email = email,
                FullName = fullName,
                Phone = phone,
                PasswordHash = PasswordHelper.HashPassword(password),
                RoleId = 2, // User role
                Status = 1, // Active
                CreatedAt = DateTime.Now
            };

            context.Users.Add(user);
            context.SaveChanges();
            return true;
        }

        /// <summary>
        /// Kiểm tra người dùng hiện tại có subscription còn hiệu lực không
        /// </summary>
        public static bool HasActiveSubscription()
        {
            if (_currentUser == null) return false;

            using var context = new LearningPlatformContext();
            var activeSubscription = context.UserSubscriptions
                .Where(s => s.UserId == _currentUser.UserId 
                    && s.Status == "Active" 
                    && s.ExpiresAt > DateTime.UtcNow)
                .FirstOrDefault();

            return activeSubscription != null;
        }

        /// <summary>
        /// Kiểm tra người dùng có quyền truy cập khóa học không (đã mua hoặc có subscription)
        /// </summary>
        public static bool CanAccessCourse(int courseId)
        {
            if (_currentUser == null) return false;

            using var context = new LearningPlatformContext();

            // Kiểm tra người dùng là owner của khóa học
            var course = context.Courses.Find(courseId);
            if (course != null && course.OwnerId == _currentUser.UserId)
                return true;

            // Kiểm tra đã mua khóa học
            var hasPurchased = context.CoursePurchases
                .Any(p => p.BuyerId == _currentUser.UserId 
                    && p.CourseId == courseId 
                    && p.Status == "Paid");

            if (hasPurchased) return true;

            // Kiểm tra subscription còn hiệu lực
            return HasActiveSubscription();
        }

        /// <summary>
        /// Lấy thông tin subscription còn bao nhiêu ngày hết hạn
        /// Trả về null nếu không có subscription hoặc đã hết hạn
        /// </summary>
        public static int? GetSubscriptionDaysRemaining()
        {
            if (_currentUser == null) return null;

            using var context = new LearningPlatformContext();
            var activeSubscription = context.UserSubscriptions
                .Where(s => s.UserId == _currentUser.UserId 
                    && s.Status == "Active" 
                    && s.ExpiresAt > DateTime.UtcNow)
                .OrderByDescending(s => s.ExpiresAt)
                .FirstOrDefault();

            if (activeSubscription == null) return null;

            var timeRemaining = activeSubscription.ExpiresAt - DateTime.UtcNow;
            return (int)Math.Ceiling(timeRemaining.TotalDays);
        }

        /// <summary>
        /// Kiểm tra subscription có sắp hết hạn không (còn dưới X ngày)
        /// </summary>
        public static bool IsSubscriptionExpiringSoon(int daysThreshold = 3)
        {
            var daysRemaining = GetSubscriptionDaysRemaining();
            return daysRemaining.HasValue && daysRemaining.Value <= daysThreshold && daysRemaining.Value > 0;
        }
    }
}
