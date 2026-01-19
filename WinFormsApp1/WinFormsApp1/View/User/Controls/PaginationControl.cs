using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp1.View.User.Controls
{
	public partial class PaginationControl : UserControl
	{
		private int _currentPage = 1;
		private int _totalPages = 1;
		private int _totalItems = 0;
		private int _itemsPerPage = 12;

		public event EventHandler<int> PageChanged;

		public int CurrentPage => _currentPage;
		public int ItemsPerPage => _itemsPerPage;

		public PaginationControl()
		{
			InitializeComponent();
			CenterPanel();
			UpdateUI();
		}

		public void Initialize(int itemsPerPage = 12)
		{
			_itemsPerPage = itemsPerPage;
			_currentPage = 1;
			UpdateUI();
		}

		public void UpdatePagination(int totalItems)
		{
			_totalItems = totalItems;
			_totalPages = totalItems > 0 ? (int)Math.Ceiling((double)totalItems / _itemsPerPage) : 1;

			if (_currentPage > _totalPages)
				_currentPage = _totalPages;

			UpdateUI();
		}

		private void PaginationControl_Resize(object sender, EventArgs e)
		{
			CenterPanel();
		}

		private void CenterPanel()
		{
			// Center the panel horizontally
			panelCenter.Left = (this.Width - panelCenter.Width) / 2;
		}

		private void UpdateUI()
		{
			// Store current focus to restore after update
			var focusedControl = this.ActiveControl;
			
			txtCurrentPage.Text = _currentPage.ToString();
			lblTotalPages.Text = $"/ {_totalPages}";

			// Enable/disable buttons
			btnPrevPage.Enabled = _currentPage > 1;
			btnNextPage.Enabled = _currentPage < _totalPages;

			// Style disabled buttons
			StyleButton(btnPrevPage);
			StyleButton(btnNextPage);
			
			// Restore focus if it wasn't on the textbox
			if (focusedControl != null && focusedControl != txtCurrentPage)
			{
				focusedControl.Focus();
			}
		}

		private void StyleButton(Button btn)
		{
			if (btn.Enabled)
			{
				btn.BackColor = Color.White;
				btn.ForeColor = Color.Black;
				btn.FlatAppearance.BorderColor = Color.Black;
			}
			else
			{
				btn.BackColor = Color.FromArgb(243, 244, 246);
				btn.ForeColor = Color.FromArgb(156, 163, 175);
				btn.FlatAppearance.BorderColor = Color.FromArgb(209, 213, 219);
			}
		}

		private void BtnPrevPage_Click(object sender, EventArgs e)
		{
			if (_currentPage > 1)
			{
				_currentPage--;
				UpdateUI();
				PageChanged?.Invoke(this, _currentPage);
				
				// Prevent textbox from getting focus
				btnPrevPage.Focus();
			}
		}

		private void BtnNextPage_Click(object sender, EventArgs e)
		{
			if (_currentPage < _totalPages)
			{
				_currentPage++;
				UpdateUI();
				PageChanged?.Invoke(this, _currentPage);
				
				// Prevent textbox from getting focus
				btnNextPage.Focus();
			}
		}

		private void TxtCurrentPage_KeyPress(object sender, KeyPressEventArgs e)
		{
			// Only allow digits and control characters (backspace, etc.)
			if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
			{
				e.Handled = true;
				return;
			}

			// Handle Enter key
			if (e.KeyChar == (char)Keys.Enter)
			{
				e.Handled = true;
				JumpToPage();
			}
		}

		private void TxtCurrentPage_Leave(object sender, EventArgs e)
		{
			JumpToPage();
		}

		private void JumpToPage()
		{
			if (int.TryParse(txtCurrentPage.Text, out int newPage))
			{
				if (newPage >= 1 && newPage <= _totalPages && newPage != _currentPage)
				{
					_currentPage = newPage;
					UpdateUI();
					PageChanged?.Invoke(this, _currentPage);
				}
				else
				{
					// Reset to current page if invalid
					txtCurrentPage.Text = _currentPage.ToString();
				}
			}
			else
			{
				// Reset to current page if invalid input
				txtCurrentPage.Text = _currentPage.ToString();
			}
		}

		private void panelCenter_Paint(object sender, PaintEventArgs e)
		{

		}

		public T[] GetPageData<T>(T[] allData)
		{
			if (allData == null || allData.Length == 0)
				return Array.Empty<T>();

			int startIndex = (_currentPage - 1) * _itemsPerPage;
			int count = Math.Min(_itemsPerPage, allData.Length - startIndex);

			if (startIndex >= allData.Length)
				return Array.Empty<T>();

			T[] pageData = new T[count];
			Array.Copy(allData, startIndex, pageData, 0, count);
			return pageData;
		}
	}
}
