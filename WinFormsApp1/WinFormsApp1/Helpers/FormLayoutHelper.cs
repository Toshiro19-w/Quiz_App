using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp1.Helpers
{
    /// <summary>
    /// Helper class để quản lý layout và căn chỉnh các controls trong forms
    /// </summary>
    public static class FormLayoutHelper
    {
        /// <summary>
        /// Căn giữa search controls (TextBox và Button) trong một panel
        /// </summary>
        /// <param name="containerPanel">Panel chứa các controls</param>
        /// <param name="textBox">TextBox search</param>
        /// <param name="button">Button search</param>
        /// <param name="spacing">Kho?ng cách gi?a TextBox và Button (m?c ??nh 10px)</param>
        /// <param name="minMargin">Margin tối thiểu t? c?nh panel (m?c ??nh 10px)</param>
        public static void CenterSearchControls(Panel containerPanel, TextBox textBox, Button button, int spacing = 10, int minMargin = 10)
        {
            if (containerPanel == null || textBox == null || button == null)
                return;

            // Tính toán chi?u r?ng kh? d?ng c?a container
            int availableWidth = containerPanel.ClientSize.Width;
            
            // Tính toán t?ng chi?u r?ng c?a controls
            int totalWidth = textBox.Width + button.Width + spacing;
            
            // Tính toán v? trí b?t ??u ?? c?n gi?a
            int startX = (availableWidth - totalWidth) / 2;
            
            // ??m b?o không âm và có margin t?i thi?u
            startX = Math.Max(minMargin, startX);

            // ??t v? trí cho textBox
            textBox.Location = new Point(startX, textBox.Location.Y);
            
            // ??t v? trí cho button ngay sau textBox
            button.Location = new Point(startX + textBox.Width + spacing, button.Location.Y);
        }

        /// <summary>
        /// C?n gi?a FlowLayoutPanel trong m?t container panel
        /// </summary>
        /// <param name="containerPanel">Panel ch?a FlowLayoutPanel</param>
        /// <param name="flowLayoutPanel">FlowLayoutPanel c?n c?n gi?a</param>
        /// <param name="keepYPosition">Có gi? nguyên v? trí Y không (m?c ??nh true)</param>
        public static void CenterFlowLayoutPanel(Panel containerPanel, FlowLayoutPanel flowLayoutPanel, bool keepYPosition = true)
        {
            if (containerPanel == null || flowLayoutPanel == null)
                return;

            int x = (containerPanel.Width - flowLayoutPanel.Width) / 2;
            int y = keepYPosition ? flowLayoutPanel.Location.Y : (containerPanel.Height - flowLayoutPanel.Height) / 2;
            
            flowLayoutPanel.Location = new Point(Math.Max(0, x), Math.Max(0, y));
        }

        /// <summary>
        /// C?n gi?a nhi?u FlowLayoutPanel cùng lúc
        /// </summary>
        /// <param name="containerPanel">Panel ch?a các FlowLayoutPanel</param>
        /// <param name="flowLayoutPanels">M?ng các FlowLayoutPanel c?n c?n gi?a</param>
        /// <param name="keepYPosition">Có gi? nguyên v? trí Y không (m?c ??nh true)</param>
        public static void CenterMultipleFlowLayoutPanels(Panel containerPanel, FlowLayoutPanel[] flowLayoutPanels, bool keepYPosition = true)
        {
            if (containerPanel == null || flowLayoutPanels == null)
                return;

            foreach (var flowPanel in flowLayoutPanels)
            {
                CenterFlowLayoutPanel(containerPanel, flowPanel, keepYPosition);
            }
        }

        /// <summary>
        /// ??ng ký các s? ki?n resize ?? t? ??ng c?n ch?nh search controls
        /// </summary>
        /// <param name="form">Form ch?a controls</param>
        /// <param name="containerPanel">Panel ch?a search controls</param>
        /// <param name="textBox">TextBox search</param>
        /// <param name="button">Button search</param>
        /// <param name="spacing">Kho?ng cách gi?a controls</param>
        /// <param name="minMargin">Margin t?i thi?u</param>
        public static void SetupSearchControlsAutoCenter(Form form, Panel containerPanel, TextBox textBox, Button button, int spacing = 10, int minMargin = 10)
        {
            if (form == null || containerPanel == null || textBox == null || button == null)
                return;

            // ??ng ký s? ki?n resize
            EventHandler resizeHandler = (sender, e) => CenterSearchControls(containerPanel, textBox, button, spacing, minMargin);
            
            form.Resize += resizeHandler;
            containerPanel.Resize += resizeHandler;
            form.Load += resizeHandler;
            
            // Override methods cho form
            form.Shown += (sender, e) => CenterSearchControls(containerPanel, textBox, button, spacing, minMargin);
            form.SizeChanged += (sender, e) => CenterSearchControls(containerPanel, textBox, button, spacing, minMargin);
        }

        /// <summary>
        /// ??ng ký các s? ki?n resize ?? t? ??ng c?n ch?nh FlowLayoutPanels
        /// </summary>
        /// <param name="form">Form ch?a controls</param>
        /// <param name="containerPanel">Panel ch?a FlowLayoutPanels</param>
        /// <param name="flowLayoutPanels">M?ng các FlowLayoutPanel</param>
        /// <param name="keepYPosition">Có gi? nguyên v? trí Y không</param>
        public static void SetupFlowLayoutPanelsAutoCenter(Form form, Panel containerPanel, FlowLayoutPanel[] flowLayoutPanels, bool keepYPosition = true)
        {
            if (form == null || containerPanel == null || flowLayoutPanels == null)
                return;

            // ??ng ký s? ki?n resize
            EventHandler resizeHandler = (sender, e) => CenterMultipleFlowLayoutPanels(containerPanel, flowLayoutPanels, keepYPosition);
            
            form.Resize += resizeHandler;
            containerPanel.Resize += resizeHandler;
            form.Load += resizeHandler;
            
            // Override methods cho form
            form.Shown += (sender, e) => CenterMultipleFlowLayoutPanels(containerPanel, flowLayoutPanels, keepYPosition);
            form.SizeChanged += (sender, e) => CenterMultipleFlowLayoutPanels(containerPanel, flowLayoutPanels, keepYPosition);
        }

        /// <summary>
        /// Setup t? ??ng c?n ch?nh cho form có c? search controls và FlowLayoutPanels
        /// </summary>
        /// <param name="form">Form ch?a controls</param>
        /// <param name="containerPanel">Panel ch?a t?t c? controls</param>
        /// <param name="searchTextBox">TextBox search</param>
        /// <param name="searchButton">Button search</param>
        /// <param name="flowLayoutPanels">M?ng các FlowLayoutPanel</param>
        public static void SetupCompleteAutoLayout(Form form, Panel containerPanel, TextBox searchTextBox, Button searchButton, FlowLayoutPanel[] flowLayoutPanels)
        {
            if (form == null || containerPanel == null)
                return;

            EventHandler layoutHandler = (sender, e) =>
            {
                // C?n ch?nh FlowLayoutPanels tr??c
                if (flowLayoutPanels != null)
                {
                    CenterMultipleFlowLayoutPanels(containerPanel, flowLayoutPanels, true);
                }

                // Sau ?ó c?n ch?nh search controls
                if (searchTextBox != null && searchButton != null)
                {
                    CenterSearchControls(containerPanel, searchTextBox, searchButton);
                }
            };

            // ??ng ký t?t c? s? ki?n
            form.Load += layoutHandler;
            form.Resize += layoutHandler;
            form.Shown += layoutHandler;
            form.SizeChanged += layoutHandler;
            containerPanel.Resize += layoutHandler;
        }

        /// <summary>
        /// C?n gi?a Label trong container panel
        /// </summary>
        /// <param name="containerPanel">Panel ch?a label</param>
        /// <param name="label">Label c?n c?n gi?a</param>
        /// <param name="horizontalCenter">Có c?n gi?a theo chi?u ngang không (m?c ??nh true)</param>
        /// <param name="keepYPosition">Có gi? nguyên v? trí Y không (m?c ??nh true)</param>
        public static void CenterLabel(Panel containerPanel, Label label, bool horizontalCenter = true, bool keepYPosition = true)
        {
            if (containerPanel == null || label == null)
                return;

            int x = horizontalCenter ? (containerPanel.Width - label.Width) / 2 : label.Location.X;
            int y = keepYPosition ? label.Location.Y : (containerPanel.Height - label.Height) / 2;
            
            label.Location = new Point(Math.Max(0, x), Math.Max(0, y));
        }

        /// <summary>
        /// Setup layout chuyên bi?t cho formShop v?i search controls, tag panel, courses panel và label
        /// </summary>
        /// <param name="form">formShop</param>
        /// <param name="containerPanel">panel1 chính</param>
        /// <param name="searchTextBox">searchTB</param>
        /// <param name="searchButton">searchButton</param>
        /// <param name="titleLabel">label1 - tiêu ??</param>
        /// <param name="tagPanel">tagPanel - FlowLayoutPanel ch?a các tag buttons</param>
        /// <param name="coursesPanel">coursesPanel - FlowLayoutPanel ch?a các course cards</param>
        public static void SetupShopLayoutAutoCenter(Form form, Panel containerPanel, TextBox searchTextBox, Button searchButton, 
            Label titleLabel, FlowLayoutPanel tagPanel, FlowLayoutPanel coursesPanel)
        {
            if (form == null || containerPanel == null)
                return;

            EventHandler layoutHandler = (sender, e) =>
            {
                // 1. C?n ch?nh search controls
                if (searchTextBox != null && searchButton != null)
                {
                    CenterSearchControls(containerPanel, searchTextBox, searchButton);
                }

                // 2. C?n ch?nh title label (gi? v? trí Y, ch? c?n gi?a theo X)
                if (titleLabel != null)
                {
                    CenterLabel(containerPanel, titleLabel, true, true);
                }

                // 3. C?n ch?nh tag panel
                if (tagPanel != null)
                {
                    CenterFlowLayoutPanel(containerPanel, tagPanel, true);
                }

                // 4. C?n ch?nh courses panel
                if (coursesPanel != null)
                {
                    CenterFlowLayoutPanel(containerPanel, coursesPanel, true);
                }
            };

            // ??ng ký t?t c? s? ki?n
            form.Load += layoutHandler;
            form.Resize += layoutHandler;
            form.Shown += layoutHandler;
            form.SizeChanged += layoutHandler;
            containerPanel.Resize += layoutHandler;
        }

        /// <summary>
        /// C?n ch?nh responsive cho courses panel - t? ??ng ?i?u ch?nh s? c?t theo chi?u r?ng
        /// </summary>
        /// <param name="coursesPanel">FlowLayoutPanel ch?a các course cards</param>
        /// <param name="cardWidth">Chi?u r?ng c?a m?i course card (m?c ??nh 273px)</param>
        /// <param name="minSpacing">Kho?ng cách t?i thi?u gi?a các card (m?c ??nh 10px)</param>
        public static void MakeCoursePanelResponsive(FlowLayoutPanel coursesPanel, int cardWidth = 273, int minSpacing = 10)
        {
            if (coursesPanel == null)
                return;

            // Tính toán s? c?t phù h?p
            int availableWidth = coursesPanel.Parent?.Width ?? coursesPanel.Width;
            int maxColumns = Math.Max(1, availableWidth / (cardWidth + minSpacing));
            
            // Tính toán chi?u r?ng t?i ?u cho panel
            int optimalWidth = (maxColumns * cardWidth) + ((maxColumns - 1) * minSpacing);
            
            // C?n gi?a panel v?i chi?u r?ng m?i
            coursesPanel.Width = Math.Min(optimalWidth, availableWidth - 20); // -20 ?? có margin
            
            if (coursesPanel.Parent != null)
            {
                int centerX = (coursesPanel.Parent.Width - coursesPanel.Width) / 2;
                coursesPanel.Location = new Point(Math.Max(10, centerX), coursesPanel.Location.Y);
            }
        }

        /// <summary>
        /// Setup ??y ?? layout cho formShop v?i responsive courses panel
        /// </summary>
        /// <param name="form">formShop</param>
        /// <param name="containerPanel">panel1 chính</param>
        /// <param name="searchTextBox">searchTB</param>
        /// <param name="searchButton">searchButton</param>
        /// <param name="titleLabel">label1 - tiêu ??</param>
        /// <param name="tagPanel">tagPanel - FlowLayoutPanel ch?a các tag buttons</param>
        /// <param name="coursesPanel">coursesPanel - FlowLayoutPanel ch?a các course cards</param>
        public static void SetupShopCompleteLayout(Form form, Panel containerPanel, TextBox searchTextBox, Button searchButton,
            Label titleLabel, FlowLayoutPanel tagPanel, FlowLayoutPanel coursesPanel)
        {
            if (form == null || containerPanel == null)
                return;

            EventHandler layoutHandler = (sender, e) =>
            {
                // 1. C?n ch?nh search controls
                if (searchTextBox != null && searchButton != null)
                {
                    CenterSearchControls(containerPanel, searchTextBox, searchButton);
                }

                // 2. C?n ch?nh title label
                if (titleLabel != null)
                {
                    CenterLabel(containerPanel, titleLabel, true, true);
                }

                // 3. C?n ch?nh tag panel
                if (tagPanel != null)
                {
                    CenterFlowLayoutPanel(containerPanel, tagPanel, true);
                }

                // 4. C?n ch?nh courses panel v?i responsive
                if (coursesPanel != null)
                {
                    MakeCoursePanelResponsive(coursesPanel);
                }
            };

            // ??ng ký t?t c? s? ki?n
            form.Load += layoutHandler;
            form.Resize += layoutHandler;
            form.Shown += layoutHandler;
            form.SizeChanged += layoutHandler;
            containerPanel.Resize += layoutHandler;
        }

        /// <summary>
        /// C?n gi?a m?t control b?t k? trong container panel
        /// </summary>
        /// <param name="containerPanel">Panel ch?a control</param>
        /// <param name="control">Control c?n c?n gi?a</param>
        /// <param name="horizontalCenter">Có c?n gi?a theo chi?u ngang không</param>
        /// <param name="verticalCenter">Có c?n gi?a theo chi?u d?c không</param>
        /// <param name="minMargin">Margin t?i thi?u t? c?nh panel</param>
        public static void CenterControl(Panel containerPanel, Control control, bool horizontalCenter = true, bool verticalCenter = false, int minMargin = 10)
        {
            if (containerPanel == null || control == null)
                return;

            int x = horizontalCenter ? Math.Max(minMargin, (containerPanel.Width - control.Width) / 2) : control.Location.X;
            int y = verticalCenter ? Math.Max(minMargin, (containerPanel.Height - control.Height) / 2) : control.Location.Y;
            
            control.Location = new Point(x, y);
        }

        /// <summary>
        /// C?n gi?a nhi?u controls cùng lúc
        /// </summary>
        /// <param name="containerPanel">Panel ch?a các controls</param>
        /// <param name="controls">M?ng các controls c?n c?n gi?a</param>
        /// <param name="horizontalCenter">Có c?n gi?a theo chi?u ngang không</param>
        /// <param name="verticalCenter">Có c?n gi?a theo chi?u d?c không</param>
        public static void CenterMultipleControls(Panel containerPanel, Control[] controls, bool horizontalCenter = true, bool verticalCenter = false)
        {
            if (containerPanel == null || controls == null)
                return;

            foreach (var control in controls)
            {
                CenterControl(containerPanel, control, horizontalCenter, verticalCenter);
            }
        }

        /// <summary>
        /// Setup layout tùy ch?n cho b?t k? form nào v?i các controls t? ??nh ngh?a
        /// </summary>
        /// <param name="form">Form c?n setup</param>
        /// <param name="containerPanel">Panel ch?a controls</param>
        /// <param name="controlsToCenter">M?ng các controls c?n c?n gi?a</param>
        /// <param name="horizontalCenter">Có c?n gi?a theo chi?u ngang không</param>
        /// <param name="verticalCenter">Có c?n gi?a theo chi?u d?c không</param>
        public static void SetupCustomLayout(Form form, Panel containerPanel, Control[] controlsToCenter, bool horizontalCenter = true, bool verticalCenter = false)
        {
            if (form == null || containerPanel == null || controlsToCenter == null)
                return;

            EventHandler layoutHandler = (sender, e) =>
            {
                CenterMultipleControls(containerPanel, controlsToCenter, horizontalCenter, verticalCenter);
            };

            // ??ng ký t?t c? s? ki?n
            form.Load += layoutHandler;
            form.Resize += layoutHandler;
            form.Shown += layoutHandler;
            form.SizeChanged += layoutHandler;
            containerPanel.Resize += layoutHandler;
        }

        /// <summary>
        /// Tạo responsive cho test panel - tự động điều chỉnh chiều rộng theo container
        /// </summary>
        /// <param name="testPanel">FlowLayoutPanel chứa các test cards</param>
        /// <param name="cardWidth">Chiều rộng của mỗi test card (mặc định 270px)</param>
        /// <param name="minSpacing">Khoảng cách tối thiểu giữa các card (mặc định 10px)</param>
        /// <param name="containerPanel">Panel chứa testPanel để tính toán chiều rộng</param>
        public static void MakeTestPanelResponsive(FlowLayoutPanel testPanel, Panel containerPanel, int cardWidth = 270, int minSpacing = 10)
        {
            if (testPanel == null || containerPanel == null)
                return;

            // Tính toán số cột phù hợp
            int availableWidth = containerPanel.Width;
            int maxColumns = Math.Max(1, availableWidth / (cardWidth + minSpacing));
            
            // Tính toán chiều rộng tối ưu cho panel
            int optimalWidth = (maxColumns * cardWidth) + ((maxColumns - 1) * minSpacing);
            
            // Căn giữa panel với chiều rộng mới
            testPanel.Width = Math.Min(optimalWidth, availableWidth - 40); // -40 để có margin
            
            int centerX = (containerPanel.Width - testPanel.Width) / 2;
            testPanel.Location = new Point(Math.Max(20, centerX), testPanel.Location.Y);
        }

        /// <summary>
        /// Setup layout chuyên biệt cho formTest với title labels và test panels
        /// </summary>
        /// <param name="form">formTest</param>
        /// <param name="containerPanel">mainPanel chính</param>
        /// <param name="titleLabel1">label1 - "Bài tập đã giao"</param>
        /// <param name="titleLabel2">label2 - "Bài tập sắp tới hạn"</param>
        /// <param name="titleLabel3">label17 - "Bài tập quá hạn"</param>
        /// <param name="assignedPanel">AssignedPanel - FlowLayoutPanel chứa bài tập đã giao</param>
        /// <param name="dueDatePanel">dueDatePanel - FlowLayoutPanel chứa bài tập sắp tới hạn</param>
        /// <param name="overDuePanel">overDuePanel - FlowLayoutPanel chứa bài tập quá hạn</param>
        public static void SetupTestLayoutAutoCenter(Form form, Panel containerPanel, 
            Label titleLabel1, Label titleLabel2, Label titleLabel3,
            FlowLayoutPanel assignedPanel, FlowLayoutPanel dueDatePanel, FlowLayoutPanel overDuePanel)
        {
            if (form == null || containerPanel == null)
                return;

            EventHandler layoutHandler = (sender, e) =>
            {
                // 1. Căn giữa các title labels (giữ vị trí Y, chỉ căn giữa theo X)
                if (titleLabel1 != null)
                {
                    CenterLabel(containerPanel, titleLabel1, true, true);
                }
                if (titleLabel2 != null)
                {
                    CenterLabel(containerPanel, titleLabel2, true, true);
                }
                if (titleLabel3 != null)
                {
                    CenterLabel(containerPanel, titleLabel3, true, true);
                }

                // 2. Căn giữa và tạo responsive cho các FlowLayoutPanels
                if (assignedPanel != null)
                {
                    MakeTestPanelResponsive(assignedPanel, containerPanel);
                }
                if (dueDatePanel != null)
                {
                    MakeTestPanelResponsive(dueDatePanel, containerPanel);
                }
                if (overDuePanel != null)
                {
                    MakeTestPanelResponsive(overDuePanel, containerPanel);
                }
            };

            // Đăng ký tất cả sự kiện
            form.Load += layoutHandler;
            form.Resize += layoutHandler;
            form.Shown += layoutHandler;
            form.SizeChanged += layoutHandler;
            containerPanel.Resize += layoutHandler;
        }
    }
}