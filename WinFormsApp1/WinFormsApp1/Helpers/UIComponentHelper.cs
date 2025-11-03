using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp1.Helpers
{
    /// <summary>
    /// Helper class cho UI components
    /// </summary>
    public static class UIComponentHelper
    {
        public static Button CreateModernButton(string text, Color backColor, Color foreColor, Size size, EventHandler clickHandler = null)
        {
            var button = new Button
            {
                Text = text,
                Size = size,
                BackColor = backColor,
                ForeColor = foreColor,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            button.FlatAppearance.BorderSize = 0;
            button.FlatAppearance.MouseOverBackColor = ControlPaint.Light(backColor, 0.2f);
            button.FlatAppearance.MouseDownBackColor = ControlPaint.Dark(backColor, 0.1f);
            
            if (clickHandler != null)
                button.Click += clickHandler;
            
            return button;
        }

        public static TextBox CreateModernTextBox(string placeholder, Size size)
        {
            var textBox = new TextBox
            {
                Size = size,
                Font = new Font("Segoe UI", 10),
                BorderStyle = BorderStyle.FixedSingle,
                ForeColor = Color.Gray,
                Text = placeholder,
                Tag = placeholder
            };

            textBox.GotFocus += (s, e) =>
            {
                if (textBox.Text == placeholder)
                {
                    textBox.Text = "";
                    textBox.ForeColor = Color.Black;
                }
            };

            textBox.LostFocus += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    textBox.Text = placeholder;
                    textBox.ForeColor = Color.Gray;
                }
            };

            return textBox;
        }

        public static DataGridView CreateModernDataGridView()
        {
            var dgv = new DataGridView
            {
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None,
                EnableHeadersVisualStyles = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                Font = new Font("Segoe UI", 9)
            };

            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 144, 220);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgv.ColumnHeadersDefaultCellStyle.Padding = new Padding(10);
            dgv.ColumnHeadersHeight = 40;

            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(220, 237, 250);
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgv.DefaultCellStyle.Padding = new Padding(5);
            dgv.RowTemplate.Height = 35;

            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 249, 250);

            return dgv;
        }

        public static Panel CreateCardPanel(Size size, Point location)
        {
            var panel = new Panel
            {
                Size = size,
                Location = location,
                BackColor = Color.White,
                Padding = new Padding(20)
            };

            panel.Paint += (s, e) =>
            {
                ControlPaint.DrawBorder(e.Graphics, panel.ClientRectangle,
                    Color.FromArgb(226, 232, 240), 1, ButtonBorderStyle.Solid,
                    Color.FromArgb(226, 232, 240), 1, ButtonBorderStyle.Solid,
                    Color.FromArgb(226, 232, 240), 1, ButtonBorderStyle.Solid,
                    Color.FromArgb(226, 232, 240), 1, ButtonBorderStyle.Solid);
            };

            return panel;
        }

        public static Panel CreateActionButtonsPanel(EventHandler editHandler, EventHandler deleteHandler, EventHandler viewHandler = null)
        {
            var panel = new Panel
            {
                Size = new Size(viewHandler != null ? 180 : 120, 35)
            };

            int xPos = 0;

            if (viewHandler != null)
            {
                var viewBtn = CreateModernButton("👁️", Color.FromArgb(52, 144, 220), Color.White, new Size(35, 30), viewHandler);
                viewBtn.Location = new Point(xPos, 0);
                panel.Controls.Add(viewBtn);
                xPos += 40;
            }

            var editBtn = CreateModernButton("✏️", Color.FromArgb(34, 197, 94), Color.White, new Size(35, 30), editHandler);
            editBtn.Location = new Point(xPos, 0);
            panel.Controls.Add(editBtn);
            xPos += 40;

            var deleteBtn = CreateModernButton("🗑️", Color.FromArgb(239, 68, 68), Color.White, new Size(35, 30), deleteHandler);
            deleteBtn.Location = new Point(xPos, 0);
            panel.Controls.Add(deleteBtn);

            return panel;
        }

        public static Panel CreateStatsCard(string title, string value, Color color, Point location, Size size)
        {
            var cardPanel = new Panel
            {
                Size = size,
                Location = location,
                BackColor = color,
                Padding = new Padding(20)
            };

            var valueLabel = new Label
            {
                Text = value,
                Font = new Font("Segoe UI", 28, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(20, 20),
                AutoSize = true
            };

            var titleLabel = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 12),
                ForeColor = Color.White,
                Location = new Point(20, 70),
                AutoSize = true
            };

            cardPanel.Controls.AddRange(new Control[] { valueLabel, titleLabel });
            
            return cardPanel;
        }
    }
}
