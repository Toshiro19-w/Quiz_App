using System;
using System.IO;
using System.Text.Json;
using WinFormsApp1.Models.Entities;
using File = System.IO.File;

namespace WinFormsApp1.Helpers
{
    public static class SessionHelper
    {
        private static readonly string SessionFilePath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "YMEDU", "session.json");

        public class SessionData
        {
            public int UserId { get; set; }
            public string Email { get; set; }
            public string FullName { get; set; }
            public DateTime LoginTime { get; set; }
            public bool RememberMe { get; set; }
        }

        public static void SaveSession(User user, bool rememberMe = false)
        {
            try
            {
                var sessionData = new SessionData
                {
                    UserId = user.UserId,
                    Email = user.Email,
                    FullName = user.FullName,
                    LoginTime = DateTime.Now,
                    RememberMe = rememberMe
                };

                Directory.CreateDirectory(Path.GetDirectoryName(SessionFilePath));
                var json = JsonSerializer.Serialize(sessionData, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(SessionFilePath, json);
            }
            catch (Exception ex)
            {
                // Log error but don't throw - session saving is not critical
                System.Diagnostics.Debug.WriteLine($"Failed to save session: {ex.Message}");
            }
        }

        public static SessionData LoadSession()
        {
            try
            {
                if (!File.Exists(SessionFilePath))
                    return null;

                var json = File.ReadAllText(SessionFilePath);
                var sessionData = JsonSerializer.Deserialize<SessionData>(json);

                // Check if session is expired (7 days for remember me, 1 day otherwise)
                var maxAge = sessionData?.RememberMe == true ? TimeSpan.FromDays(7) : TimeSpan.FromDays(1);
                if (sessionData != null && DateTime.Now - sessionData.LoginTime > maxAge)
                {
                    ClearSession();
                    return null;
                }

                return sessionData;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to load session: {ex.Message}");
                ClearSession(); // Clear corrupted session
                return null;
            }
        }

        public static void ClearSession()
        {
            try
            {
                if (File.Exists(SessionFilePath))
                    File.Delete(SessionFilePath);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to clear session: {ex.Message}");
            }
        }

        public static bool HasValidSession()
        {
            var session = LoadSession();
            return session != null;
        }
    }
}