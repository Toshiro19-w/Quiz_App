using System.Runtime.InteropServices;

namespace WinFormsApp1.Helpers
{
    public static class DpiHelper
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("user32.dll")]
        private static extern int ReleaseDC(IntPtr hwnd, IntPtr hdc);

        [DllImport("gdi32.dll")]
        private static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

        private const int LOGPIXELSX = 88;
        private const int LOGPIXELSY = 90;

        public static float GetDpiScaleFactor()
        {
            IntPtr hdc = GetDC(IntPtr.Zero);
            int dpiX = GetDeviceCaps(hdc, LOGPIXELSX);
            ReleaseDC(IntPtr.Zero, hdc);
            
            return dpiX / 96f; // 96 DPI is 100% scaling
        }

        public static int ScaleValue(int value)
        {
            return (int)(value * GetDpiScaleFactor());
        }

        public static Size ScaleSize(Size size)
        {
            float scale = GetDpiScaleFactor();
            return new Size(
                (int)(size.Width * scale),
                (int)(size.Height * scale)
            );
        }

        public static Font ScaleFont(Font font)
        {
            float scale = GetDpiScaleFactor();
            return new Font(font.FontFamily, font.Size * scale, font.Style);
        }
    }
}