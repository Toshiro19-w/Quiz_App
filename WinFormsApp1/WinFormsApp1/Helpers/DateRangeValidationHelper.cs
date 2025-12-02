using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp1.Helpers
{
    /// <summary>
    /// Helper class for date range validation in admin dashboards
    /// </summary>
    public static class DateRangeValidationHelper
    {
        /// <summary>
        /// Setup date range validation for DateTimePickers
        /// </summary>
        /// <param name="startDatePicker">Start date picker</param>
        /// <param name="endDatePicker">End date picker</param>
        /// <param name="applyButton">Button to enable/disable based on validation</param>
        /// <param name="validColor">Color when valid (default: teal)</param>
        /// <param name="invalidColor">Color when invalid (default: gray)</param>
        public static void SetupDateRangeValidation(
            DateTimePicker startDatePicker,
            DateTimePicker endDatePicker,
            Button applyButton,
            Color? validColor = null,
            Color? invalidColor = null)
        {
            var validBtnColor = validColor ?? Color.FromArgb(56, 178, 172);
            var invalidBtnColor = invalidColor ?? Color.Gray;

            // Wire up events
            EventHandler validateHandler = (s, e) =>
            {
                // Only validate if both pickers are visible
                if (!startDatePicker.Visible || !endDatePicker.Visible)
                    return;

                ValidateDateRange(startDatePicker, endDatePicker, applyButton, validBtnColor, invalidBtnColor);
            };

            startDatePicker.ValueChanged += validateHandler;
            endDatePicker.ValueChanged += validateHandler;
        }

        /// <summary>
        /// Validate that start date is before end date
        /// </summary>
        /// <param name="startDatePicker">Start date picker</param>
        /// <param name="endDatePicker">End date picker</param>
        /// <param name="applyButton">Button to enable/disable</param>
        /// <param name="validColor">Color when valid</param>
        /// <param name="invalidColor">Color when invalid</param>
        /// <returns>True if valid, false otherwise</returns>
        public static bool ValidateDateRange(
            DateTimePicker startDatePicker,
            DateTimePicker endDatePicker,
            Button applyButton,
            Color validColor,
            Color invalidColor)
        {
            if (startDatePicker.Value.Date >= endDatePicker.Value.Date)
            {
                // Invalid range
                applyButton.Enabled = false;
                applyButton.BackColor = invalidColor;

                // Show tooltip
                var tooltip = new ToolTip();
                tooltip.Show(
                    "Ngày bắt đầu phải nhỏ hơn ngày kết thúc!",
                    applyButton,
                    0,
                    -30,
                    3000
                );

                return false;
            }
            else
            {
                // Valid range
                applyButton.Enabled = true;
                applyButton.BackColor = validColor;
                return true;
            }
        }

        /// <summary>
        /// Show validation error message box
        /// </summary>
        /// <param name="parent">Parent form/control</param>
        public static void ShowInvalidRangeMessage(IWin32Window parent = null)
        {
            MessageBox.Show(
                "Ngày bắt đầu phải nhỏ hơn ngày kết thúc!",
                "Lỗi khoảng thời gian",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );
        }

        /// <summary>
        /// Validate with message box if invalid
        /// </summary>
        /// <param name="startDatePicker">Start date picker</param>
        /// <param name="endDatePicker">End date picker</param>
        /// <param name="applyButton">Button to validate</param>
        /// <param name="parent">Parent for message box</param>
        /// <returns>True if valid, false otherwise</returns>
        public static bool ValidateWithMessage(
            DateTimePicker startDatePicker,
            DateTimePicker endDatePicker,
            Button applyButton,
            IWin32Window parent = null)
        {
            var validColor = Color.FromArgb(56, 178, 172);
            var invalidColor = Color.Gray;

            bool isValid = ValidateDateRange(
                startDatePicker,
                endDatePicker,
                applyButton,
                validColor,
                invalidColor
            );

            if (!isValid)
            {
                ShowInvalidRangeMessage(parent);
            }

            return isValid;
        }

        /// <summary>
        /// Initialize date pickers with default values
        /// </summary>
        /// <param name="startDatePicker">Start date picker</param>
        /// <param name="endDatePicker">End date picker</param>
        /// <param name="defaultRange">Default range in days (default: 30 days)</param>
        public static void InitializeDatePickers(
            DateTimePicker startDatePicker,
            DateTimePicker endDatePicker,
            int defaultRange = 30)
        {
            var now = DateTime.Now;
            startDatePicker.Value = now.AddDays(-defaultRange);
            endDatePicker.Value = now;
        }

        /// <summary>
        /// Get formatted date range string for display
        /// </summary>
        /// <param name="startDate">Start date</param>
        /// <param name="endDate">End date</param>
        /// <param name="format">Date format (default: dd/MM/yyyy)</param>
        /// <returns>Formatted string "DD/MM/YYYY - DD/MM/YYYY"</returns>
        public static string GetDateRangeString(
            DateTime startDate,
            DateTime endDate,
            string format = "dd/MM/yyyy")
        {
            return $"{startDate.ToString(format)} - {endDate.ToString(format)}";
        }
    }
}
