using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp1.Helpers
{
    /// <summary>
    /// Helper class cho responsive layout
    /// </summary>
    public static class ResponsiveLayoutHelper
    {
        public static FlowLayoutPanel CreateResponsiveCardContainer(Control parent, int yPosition = 80)
        {
            var flowPanel = new FlowLayoutPanel
            {
                Location = new Point(20, yPosition),
                AutoSize = true,
                MaximumSize = new Size(parent.Width - 40, 0),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                WrapContents = true,
                FlowDirection = FlowDirection.LeftToRight
            };

            parent.Resize += (s, e) => flowPanel.MaximumSize = new Size(parent.Width - 40, 0);
            return flowPanel;
        }

        public static Panel CreateResponsivePanel(Point location, Size size, AnchorStyles anchor)
        {
            return new Panel
            {
                Location = location,
                Size = size,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Anchor = anchor
            };
        }

        public static Panel CreateResponsiveChartPanel(string title, Point location, Size size, AnchorStyles anchor)
        {
            var panel = new Panel
            {
                Location = location,
                Size = size,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Anchor = anchor
            };

            var titleLabel = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Location = new Point(10, 10),
                AutoSize = true
            };
            panel.Controls.Add(titleLabel);
            return panel;
        }

        public static void SetupResponsiveForm(Form form, params Control[] controls)
        {
            EventHandler resizeHandler = (s, e) =>
            {
                foreach (var control in controls)
                {
                    if (control is Panel panel)
                    {
                        panel.Width = form.ClientSize.Width - 40;
                    }
                    else if (control is DataGridView dgv)
                    {
                        dgv.Width = form.ClientSize.Width - 40;
                    }
                }
            };

            form.Load += resizeHandler;
            form.Resize += resizeHandler;
        }
    }
}
