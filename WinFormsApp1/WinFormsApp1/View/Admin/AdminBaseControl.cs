using System;
using System.Drawing;
using System.Windows.Forms;
using WinFormsApp1.Controllers;

namespace WinFormsApp1.View.Admin
{
    /// <summary>
    /// Base control for admin UserControls to centralize styling, layout and controller lifetime.
    /// NOTE: AdjustResponsiveLayout has been changed to use a fixed large layout (no responsive behavior)
    /// to match the user UI size and maximize admin controls.
    /// </summary>
    public abstract class AdminBaseControl : UserControl
    {
        protected readonly AdminController _adminController;

        protected AdminBaseControl(AdminController controller = null)
        {
            _adminController = controller ?? new AdminController();
        }

        protected void ApplyModernStyling(DataGridView dataGridView, Panel formPanel)
        {
            if (dataGridView == null || formPanel == null) return;

            dataGridView.BorderStyle = BorderStyle.None;
            dataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dataGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 144, 220);
            dataGridView.DefaultCellStyle.SelectionForeColor = Color.White;
            dataGridView.BackgroundColor = Color.White;
            dataGridView.EnableHeadersVisualStyles = false;
            dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(52, 144, 220);
            dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            formPanel.BorderStyle = BorderStyle.FixedSingle;
        }

        // Replaced responsive logic with a fixed large layout that scales panels to occupy most of the control.
        protected void AdjustResponsiveLayout(DataGridView dataGridView, Panel formPanel, int breakpoint = 1100, int rightOffset = 420)
        {
            if (dataGridView == null || formPanel == null) return;

            // Use proportions to make admin area large like the user UI and disable small-breakpoint responsive behavior.
            // Data grid will occupy about 65% width, form panel takes the remainder.
            int padding = 20;
            int topOffset = 80;
            int availableWidth = Math.Max(800, this.Width - padding * 2);
            int availableHeight = Math.Max(600, this.Height - topOffset - padding);

            int dataGridWidth = (int)(availableWidth * 0.66);
            int formWidth = availableWidth - dataGridWidth - 20; // spacing

            dataGridView.Location = new Point(padding, topOffset);
            dataGridView.Size = new Size(Math.Max(700, dataGridWidth), Math.Max(400, availableHeight));

            formPanel.Location = new Point(dataGridView.Right + 20, topOffset);
            formPanel.Size = new Size(Math.Max(360, formWidth), Math.Max(500, availableHeight));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _adminController?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
