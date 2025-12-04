using System.Collections.Generic;

namespace WinFormsApp1.Helpers
{
    public static class ContentTypeHelper
    {
        // Map từ tiếng Việt sang tiếng Anh (để lưu vào database)
        private static readonly Dictionary<string, string> ViToEn = new Dictionary<string, string>
        {
            { "Lý thuyết", "Theory" },
            { "Video", "Video" },
            { "Bộ thẻ ghi nhớ", "FlashcardSet" },
            { "Bài kiểm tra", "Test" }
        };

        // Map từ tiếng Anh sang tiếng Việt (để hiển thị)
        private static readonly Dictionary<string, string> EnToVi = new Dictionary<string, string>
        {
            { "Theory", "Lý thuyết" },
            { "Video", "Video" },
            { "FlashcardSet", "Bộ thẻ ghi nhớ" },
            { "Test", "Bài kiểm tra" }
        };

        /// <summary>
        /// Chuyển đổi từ tiếng Việt sang tiếng Anh
        /// </summary>
        public static string ToEnglish(string vietnameseName)
        {
            return ViToEn.TryGetValue(vietnameseName, out var english) ? english : vietnameseName;
        }

        /// <summary>
        /// Chuyển đổi từ tiếng Anh sang tiếng Việt
        /// </summary>
        public static string ToVietnamese(string englishName)
        {
            return EnToVi.TryGetValue(englishName, out var vietnamese) ? vietnamese : englishName;
        }

        /// <summary>
        /// Danh sách tất cả các loại nội dung bằng tiếng Việt
        /// </summary>
        public static string[] GetVietnameseTypes()
        {
            return new[] { "Lý thuyết", "Video", "Bộ thẻ ghi nhớ", "Bài kiểm tra" };
        }
    }
}
