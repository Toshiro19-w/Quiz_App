using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WinFormsApp1.Helpers
{
    public static class SearchHelper
    {
        public static void FilterDataGridView(DataGridView dataGridView, string searchText, params string[] searchColumns)
        {
            if (dataGridView.DataSource == null) return;

            var bindingSource = dataGridView.DataSource as BindingSource;
            if (bindingSource == null)
            {
                bindingSource = new BindingSource { DataSource = dataGridView.DataSource };
                dataGridView.DataSource = bindingSource;
            }

            if (string.IsNullOrWhiteSpace(searchText))
            {
                bindingSource.RemoveFilter();
                return;
            }

            var filters = new List<string>();
            foreach (var column in searchColumns)
            {
                if (dataGridView.Columns.Contains(column))
                {
                    filters.Add($"Convert([{column}], 'System.String') LIKE '%{searchText.Replace("'", "''")}%'");
                }
            }

            if (filters.Any())
            {
                bindingSource.Filter = string.Join(" OR ", filters);
            }
        }

        public static TextBox CreateSearchBox(Control parent, EventHandler textChangedHandler)
        {
            var searchBox = new TextBox
            {
                Size = new Size(250, 25),
                Font = new Font("Segoe UI", 9),
                PlaceholderText = "🔍 Tìm kiếm..."
            };
            
            searchBox.TextChanged += textChangedHandler;
            return searchBox;
        }

        public static Panel CreateSearchPanel(TextBox searchBox, params Button[] additionalButtons)
        {
            var panel = new Panel
            {
                Height = 40,
                Dock = DockStyle.Top,
                Padding = new Padding(5)
            };

            searchBox.Location = new Point(10, 8);
            panel.Controls.Add(searchBox);

            int xPos = searchBox.Right + 10;
            foreach (var button in additionalButtons)
            {
                button.Location = new Point(xPos, 6);
                button.Size = new Size(80, 28);
                panel.Controls.Add(button);
                xPos += 90;
            }

            return panel;
        }
    }
}