using System.Drawing;
using System.IO;

namespace WinFormsApp1.Helpers
{
    public static class ImageHelper
    {
        public static Image LoadImage(string path)
        {
            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path)) return null;
            using var fs = File.OpenRead(path);
            return Image.FromStream(fs);
        }
    }
}