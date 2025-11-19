using System;
using System.Linq;
using WinFormsApp1.Models.EF;
using WinFormsApp1.Models.Entities;

namespace WinFormsApp1.Helpers
{
    public static class AuthHelper
    {
        private static User? _currentUser;

        public static User? CurrentUser => _currentUser;

        public static bool Login(string email, string password)
        {
            using var context = new LearningPlatformContext();
            var user = context.Users
                .Where(u => u.Email == email && u.Status == 1)
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
    }
}
