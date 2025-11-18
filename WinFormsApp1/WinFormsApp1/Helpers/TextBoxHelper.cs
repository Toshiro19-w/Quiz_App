using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WinFormsApp1.Helpers
{
    public static class TextBoxHelper
    {
        private const int EM_SETCUEBANNER = 0x1501;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

        /// <summary>
        /// Đặt Placeholder cho TextBox
        /// </summary>
        /// <param name="textBox">Control TextBox</param>
        /// <param name="placeholder">Nội dung placeholder</param>
        /// <param name="showOnFocus">True: Giữ placeholder khi focus (chỉ mất khi gõ). False: Mất ngay khi click vào.</param>
        public static void SetPlaceholder(TextBox textBox, string placeholder, bool showOnFocus = true)
        {
            // showOnFocus = 1 nghĩa là giữ hiển thị khi textbox có focus nhưng chưa nhập gì
            SendMessage(textBox.Handle, EM_SETCUEBANNER, showOnFocus ? 1 : 0, placeholder);
        }
    }
}