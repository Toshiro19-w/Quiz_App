using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;

namespace WinFormsApp1.Helpers
{
    public static class SlugHelper
    {
        public static string GenerateSlug(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return string.Empty;
            text = text.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();
            foreach (var ch in text)
            {
                var uc = CharUnicodeInfo.GetUnicodeCategory(ch);
                if (uc != UnicodeCategory.NonSpacingMark)
                    sb.Append(ch);
            }
            var cleaned = sb.ToString();
            cleaned = cleaned.ToLowerInvariant();
            // replace vietnamese-specific characters
            cleaned = cleaned.Replace('?', 'd').Replace('?', 'd');
            // remove invalid chars
            cleaned = Regex.Replace(cleaned, @"[^a-z0-9\s-]", "");
            cleaned = Regex.Replace(cleaned, @"\s+", "-").Trim('-');
            cleaned = Regex.Replace(cleaned, "-+", "-");
            return cleaned;
        }
    }
}
