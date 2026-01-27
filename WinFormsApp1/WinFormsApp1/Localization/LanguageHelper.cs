using System;
using System.Collections.Generic;
using System.Globalization;
using System.Resources;
using System.Threading;
using System.Windows.Forms;

namespace WinFormsApp1.Localization
{
    /// <summary>
    /// Helper class to manage application language/localization
    /// </summary>
    public static class LanguageHelper
    {
        private static ResourceManager? _resourceManager;
        private static CultureInfo _currentCulture = CultureInfo.CurrentUICulture;
        
        /// <summary>
        /// Event raised when language changes
        /// </summary>
        public static event EventHandler? LanguageChanged;

        /// <summary>
        /// Available languages in the application
        /// </summary>
        public static readonly Dictionary<string, string> AvailableLanguages = new()
        {
            { "vi-VN", "Tiếng Việt" },
            { "en-US", "English" }
        };

        /// <summary>
        /// Default language code
        /// </summary>
        public const string DefaultLanguage = "vi-VN";

        /// <summary>
        /// Current language code
        /// </summary>
        public static string CurrentLanguageCode => _currentCulture.Name;

        /// <summary>
        /// Current culture for formatting
        /// </summary>
        public static CultureInfo CurrentCulture => _currentCulture;

        /// <summary>
        /// Current language display name
        /// </summary>
        public static string CurrentLanguageName => AvailableLanguages.ContainsKey(_currentCulture.Name) 
            ? AvailableLanguages[_currentCulture.Name] 
            : _currentCulture.DisplayName;

        /// <summary>
        /// Initialize the language helper with saved language preference
        /// </summary>
        public static void Initialize()
        {
            _resourceManager = new ResourceManager(
                "WinFormsApp1.Localization.Resources.Strings", 
                typeof(LanguageHelper).Assembly);

            // Load saved language preference
            string savedLanguage = Properties.Settings.Default.Language;
            if (string.IsNullOrEmpty(savedLanguage))
            {
                savedLanguage = DefaultLanguage;
            }

            SetLanguage(savedLanguage, raiseEvent: false);
        }

        /// <summary>
        /// Set the application language
        /// </summary>
        /// <param name="cultureCode">Culture code (e.g., "vi-VN", "en-US")</param>
        /// <param name="raiseEvent">Whether to raise the LanguageChanged event</param>
        public static void SetLanguage(string cultureCode, bool raiseEvent = true)
        {
            try
            {
                _currentCulture = new CultureInfo(cultureCode);
                Thread.CurrentThread.CurrentUICulture = _currentCulture;
                Thread.CurrentThread.CurrentCulture = _currentCulture;

                // Save preference
                Properties.Settings.Default.Language = cultureCode;
                Properties.Settings.Default.Save();

                if (raiseEvent)
                {
                    LanguageChanged?.Invoke(null, EventArgs.Empty);
                }
            }
            catch (CultureNotFoundException)
            {
                // Fallback to default if invalid culture
                _currentCulture = new CultureInfo(DefaultLanguage);
                Thread.CurrentThread.CurrentUICulture = _currentCulture;
                Thread.CurrentThread.CurrentCulture = _currentCulture;
            }
        }

        /// <summary>
        /// Get localized string by key
        /// </summary>
        /// <param name="key">Resource key</param>
        /// <returns>Localized string or key if not found</returns>
        public static string GetString(string key)
        {
            if (_resourceManager == null)
            {
                Initialize();
            }

            try
            {
                string? value = _resourceManager?.GetString(key, _currentCulture);
                return value ?? key;
            }
            catch
            {
                return key;
            }
        }

        /// <summary>
        /// Get localized string with format parameters
        /// </summary>
        /// <param name="key">Resource key</param>
        /// <param name="args">Format arguments</param>
        /// <returns>Formatted localized string</returns>
        public static string GetString(string key, params object[] args)
        {
            string format = GetString(key);
            try
            {
                return string.Format(format, args);
            }
            catch
            {
                return format;
            }
        }

        #region Format Helpers

        /// <summary>
        /// Format currency based on current language
        /// </summary>
        /// <param name="amount">Amount to format</param>
        /// <returns>Formatted currency string</returns>
        public static string FormatCurrency(decimal amount)
        {
            if (amount == 0)
            {
                return GetString("Free");
            }

            return IsVietnamese 
                ? $"{amount:N0} VND" 
                : $"${amount / 23000:N2}"; // Approximate conversion for display
        }

        /// <summary>
        /// Format currency with original value (no conversion)
        /// </summary>
        /// <param name="amount">Amount in VND</param>
        /// <returns>Formatted currency string</returns>
        public static string FormatVND(decimal amount)
        {
            if (amount == 0)
            {
                return GetString("Free");
            }

            return IsVietnamese 
                ? $"{amount:N0} VND" 
                : $"{amount:N0} VND";
        }

        /// <summary>
        /// Format date based on current language
        /// </summary>
        /// <param name="date">Date to format</param>
        /// <returns>Formatted date string</returns>
        public static string FormatDate(DateTime date)
        {
            return IsVietnamese 
                ? date.ToString("dd/MM/yyyy") 
                : date.ToString("MM/dd/yyyy");
        }

        /// <summary>
        /// Format date and time based on current language
        /// </summary>
        /// <param name="dateTime">DateTime to format</param>
        /// <returns>Formatted datetime string</returns>
        public static string FormatDateTime(DateTime dateTime)
        {
            return IsVietnamese 
                ? dateTime.ToString("dd/MM/yyyy HH:mm") 
                : dateTime.ToString("MM/dd/yyyy hh:mm tt");
        }

        /// <summary>
        /// Format date with full month name
        /// </summary>
        /// <param name="date">Date to format</param>
        /// <returns>Formatted date string with month name</returns>
        public static string FormatDateLong(DateTime date)
        {
            return date.ToString("D", _currentCulture);
        }

        /// <summary>
        /// Format number based on current language
        /// </summary>
        /// <param name="number">Number to format</param>
        /// <returns>Formatted number string</returns>
        public static string FormatNumber(decimal number)
        {
            return number.ToString("N0", _currentCulture);
        }

        /// <summary>
        /// Format percentage
        /// </summary>
        /// <param name="value">Value (0-100)</param>
        /// <returns>Formatted percentage string</returns>
        public static string FormatPercentage(double value)
        {
            return $"{value:N1}%";
        }

        /// <summary>
        /// Get currency symbol based on current language
        /// </summary>
        public static string CurrencySymbol => IsVietnamese ? "VND" : "$";

        /// <summary>
        /// Get date format pattern based on current language
        /// </summary>
        public static string DateFormatPattern => IsVietnamese ? "dd/MM/yyyy" : "MM/dd/yyyy";

        /// <summary>
        /// Get datetime format pattern based on current language
        /// </summary>
        public static string DateTimeFormatPattern => IsVietnamese ? "dd/MM/yyyy HH:mm" : "MM/dd/yyyy hh:mm tt";

        #endregion

        /// <summary>
        /// Apply localization to a form and all its controls
        /// </summary>
        /// <param name="form">The form to localize</param>
        public static void ApplyLocalization(Form form)
        {
            ApplyLocalizationToControl(form);
        }

        /// <summary>
        /// Apply localization to a control and all its children
        /// </summary>
        /// <param name="control">The control to localize</param>
        public static void ApplyLocalizationToControl(Control control)
        {
            // Try to localize the control's text using its Name as key
            if (!string.IsNullOrEmpty(control.Name))
            {
                string key = $"{control.FindForm()?.Name}_{control.Name}";
                string localizedText = GetString(key);
                
                // Only apply if a translation exists (not just returning the key)
                if (localizedText != key)
                {
                    control.Text = localizedText;
                }
            }

            // Recursively apply to child controls
            foreach (Control child in control.Controls)
            {
                ApplyLocalizationToControl(child);
            }
        }

        /// <summary>
        /// Check if current language is Vietnamese
        /// </summary>
        public static bool IsVietnamese => _currentCulture.Name.StartsWith("vi");

        /// <summary>
        /// Check if current language is English
        /// </summary>
        public static bool IsEnglish => _currentCulture.Name.StartsWith("en");

        /// <summary>
        /// Get flag emoji for current language
        /// </summary>
        public static string GetCurrentFlag()
        {
            return _currentCulture.Name switch
            {
                "vi-VN" => "🇻🇳",
                "en-US" => "🇺🇸",
                _ => "🌐"
            };
        }

        /// <summary>
        /// Get flag emoji for a language code
        /// </summary>
        public static string GetFlag(string cultureCode)
        {
            return cultureCode switch
            {
                "vi-VN" => "🇻🇳",
                "en-US" => "🇺🇸",
                _ => "🌐"
            };
        }
    }
}
