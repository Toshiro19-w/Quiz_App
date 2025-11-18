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
    }
}
