using System.Drawing;

namespace WinFormsApp1.Helpers
{
    /// <summary>
    /// Color Palette cho toàn bộ ứng dụng
    /// Palette: https://colorffy.com/ui-palette-generator?colors=d6bc84-84d693
    /// </summary>
    public static class ColorPalette
    {
        // Primary Colors
        public static readonly Color Primary = Color.FromArgb(214, 188, 132);      // #d6bc84
        public static readonly Color PrimaryLight = Color.FromArgb(234, 220, 180); // Lighter shade
        public static readonly Color PrimaryDark = Color.FromArgb(194, 168, 112);  // Darker shade

        // Secondary Colors
        public static readonly Color Secondary = Color.FromArgb(132, 214, 147);     // #84d693
        public static readonly Color SecondaryLight = Color.FromArgb(180, 234, 190);
        public static readonly Color SecondaryDark = Color.FromArgb(112, 194, 127);

        // Neutral Colors
        public static readonly Color Background = Color.FromArgb(248, 249, 250);
        public static readonly Color Surface = Color.White;
        public static readonly Color Border = Color.FromArgb(226, 232, 240);

        // Text Colors
        public static readonly Color TextPrimary = Color.FromArgb(30, 30, 30);
        public static readonly Color TextSecondary = Color.FromArgb(100, 100, 100);
        public static readonly Color TextLight = Color.FromArgb(150, 150, 150);

        // Status Colors
        public static readonly Color Success = Color.FromArgb(132, 214, 147);
        public static readonly Color Warning = Color.FromArgb(214, 188, 132);
        public static readonly Color Error = Color.FromArgb(239, 68, 68);
        public static readonly Color Info = Color.FromArgb(59, 130, 246);

        // UI Element Colors
        public static readonly Color ButtonPrimary = Color.FromArgb(214, 188, 132);
        public static readonly Color ButtonSecondary = Color.FromArgb(132, 214, 147);
        public static readonly Color ButtonHover = Color.FromArgb(194, 168, 112);
        public static readonly Color ButtonDisabled = Color.FromArgb(200, 200, 200);

        // Navigation Colors
        public static readonly Color NavBackground = Color.FromArgb(214, 188, 132);
        public static readonly Color NavText = Color.White;
        public static readonly Color NavHover = Color.FromArgb(194, 168, 112);

        // Card Colors
        public static readonly Color CardBackground = Color.White;
        public static readonly Color CardBorder = Color.FromArgb(226, 232, 240);
        public static readonly Color CardShadow = Color.FromArgb(50, 0, 0, 0);
    }
}
