using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp1.Helpers
{
    /// <summary>
    /// Helper class cho center alignment
    /// </summary>
    public static class CenterLayoutHelper
    {
        public static void CenterSearchControls(Panel containerPanel, TextBox textBox, Button button, int spacing = 10, int minMargin = 10)
        {
            if (containerPanel == null || textBox == null || button == null)
                return;

            int availableWidth = containerPanel.ClientSize.Width;
            int totalWidth = textBox.Width + button.Width + spacing;
            int startX = Math.Max(minMargin, (availableWidth - totalWidth) / 2);

            textBox.Location = new Point(startX, textBox.Location.Y);
            button.Location = new Point(startX + textBox.Width + spacing, button.Location.Y);
        }

        public static void CenterFlowLayoutPanel(Panel containerPanel, FlowLayoutPanel flowLayoutPanel, bool keepYPosition = true)
        {
            if (containerPanel == null || flowLayoutPanel == null)
                return;

            int x = (containerPanel.Width - flowLayoutPanel.Width) / 2;
            int y = keepYPosition ? flowLayoutPanel.Location.Y : (containerPanel.Height - flowLayoutPanel.Height) / 2;
            
            flowLayoutPanel.Location = new Point(Math.Max(0, x), Math.Max(0, y));
        }

        public static void CenterLabel(Panel containerPanel, Label label, bool horizontalCenter = true, bool keepYPosition = true)
        {
            if (containerPanel == null || label == null)
                return;

            int x = horizontalCenter ? (containerPanel.Width - label.Width) / 2 : label.Location.X;
            int y = keepYPosition ? label.Location.Y : (containerPanel.Height - label.Height) / 2;
            
            label.Location = new Point(Math.Max(0, x), Math.Max(0, y));
        }

        public static void CenterControl(Panel containerPanel, Control control, bool horizontalCenter = true, bool verticalCenter = false, int minMargin = 10)
        {
            if (containerPanel == null || control == null)
                return;

            int x = horizontalCenter ? Math.Max(minMargin, (containerPanel.Width - control.Width) / 2) : control.Location.X;
            int y = verticalCenter ? Math.Max(minMargin, (containerPanel.Height - control.Height) / 2) : control.Location.Y;
            
            control.Location = new Point(x, y);
        }
    }
}
