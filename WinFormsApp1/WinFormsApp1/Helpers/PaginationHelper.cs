using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WinFormsApp1.Helpers
{
    public class PaginationHelper
    {
        public int CurrentPage { get; private set; } = 1;
        public int PageSize { get; private set; } = 50;
        public int TotalItems { get; private set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);

        private Panel paginationPanel;
        private Action<int> onPageChanged;

        public PaginationHelper(int pageSize = 50)
        {
            PageSize = pageSize;
        }

        public Panel CreatePaginationPanel(Action<int> pageChangedCallback)
        {
            onPageChanged = pageChangedCallback;
            paginationPanel = new Panel
            {
                Height = 40,
                Dock = DockStyle.Bottom,
                BackColor = Color.FromArgb(248, 249, 250)
            };

            UpdatePaginationControls();
            return paginationPanel;
        }

        public void UpdatePagination(int totalItems)
        {
            TotalItems = totalItems;
            if (CurrentPage > TotalPages && TotalPages > 0)
                CurrentPage = TotalPages;
            UpdatePaginationControls();
        }

        public IEnumerable<T> GetPagedData<T>(IEnumerable<T> data)
        {
            return data.Skip((CurrentPage - 1) * PageSize).Take(PageSize);
        }

        private void UpdatePaginationControls()
        {
            if (paginationPanel == null) return;

            paginationPanel.Controls.Clear();

            var infoLabel = new Label
            {
                Text = $"Trang {CurrentPage} / {TotalPages} (Tổng: {TotalItems} mục)",
                Location = new Point(10, 10),
                AutoSize = true,
                Font = new Font("Segoe UI", 9)
            };
            paginationPanel.Controls.Add(infoLabel);

            if (TotalPages <= 1) return;

            int buttonY = 8;
            int buttonWidth = 30;
            int buttonHeight = 25;
            int spacing = 5;

            // First page button
            var firstBtn = CreatePageButton("<<", 1, buttonWidth, buttonHeight);
            firstBtn.Location = new Point(200, buttonY);
            firstBtn.Enabled = CurrentPage > 1;
            paginationPanel.Controls.Add(firstBtn);

            // Previous page button
            var prevBtn = CreatePageButton("<", CurrentPage - 1, buttonWidth, buttonHeight);
            prevBtn.Location = new Point(235, buttonY);
            prevBtn.Enabled = CurrentPage > 1;
            paginationPanel.Controls.Add(prevBtn);

            // Page number buttons
            int startPage = Math.Max(1, CurrentPage - 2);
            int endPage = Math.Min(TotalPages, CurrentPage + 2);
            int xPos = 270;

            for (int i = startPage; i <= endPage; i++)
            {
                var pageBtn = CreatePageButton(i.ToString(), i, buttonWidth, buttonHeight);
                pageBtn.Location = new Point(xPos, buttonY);
                pageBtn.BackColor = i == CurrentPage ? Color.FromArgb(52, 144, 220) : Color.White;
                pageBtn.ForeColor = i == CurrentPage ? Color.White : Color.Black;
                paginationPanel.Controls.Add(pageBtn);
                xPos += buttonWidth + spacing;
            }

            // Next page button
            var nextBtn = CreatePageButton(">", CurrentPage + 1, buttonWidth, buttonHeight);
            nextBtn.Location = new Point(xPos, buttonY);
            nextBtn.Enabled = CurrentPage < TotalPages;
            paginationPanel.Controls.Add(nextBtn);

            // Last page button
            var lastBtn = CreatePageButton(">>", TotalPages, buttonWidth, buttonHeight);
            lastBtn.Location = new Point(xPos + 35, buttonY);
            lastBtn.Enabled = CurrentPage < TotalPages;
            paginationPanel.Controls.Add(lastBtn);
        }

        private Button CreatePageButton(string text, int targetPage, int width, int height)
        {
            var button = new Button
            {
                Text = text,
                Size = new Size(width, height),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 8),
                Tag = targetPage
            };
            button.FlatAppearance.BorderSize = 1;
            button.Click += (s, e) => GoToPage(targetPage);
            return button;
        }

        public void GoToPage(int page)
        {
            if (page < 1 || page > TotalPages) return;
            CurrentPage = page;
            UpdatePaginationControls();
            onPageChanged?.Invoke(CurrentPage);
        }

        public void NextPage() => GoToPage(CurrentPage + 1);
        public void PreviousPage() => GoToPage(CurrentPage - 1);
        public void FirstPage() => GoToPage(1);
        public void LastPage() => GoToPage(TotalPages);
    }
}