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
			// Ngăn phím Tab nhảy vào ô nhập liệu nếu không cần thiết
			txtCurrentPage.TabStop = false;
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
			panelCenter.Left = (this.Width - panelCenter.Width) / 2;
		}

		private void UpdateUI()
		{
			// Chỉ cập nhật Text nếu giá trị thực sự thay đổi để tránh nháy focus
			string pageText = _currentPage.ToString();
			if (txtCurrentPage.Text != pageText)
			{
				txtCurrentPage.Text = pageText;
			}

			lblTotalPages.Text = $"/ {_totalPages}";

			btnPrevPage.Enabled = _currentPage > 1;
			btnNextPage.Enabled = _currentPage < _totalPages;

			StyleButton(btnPrevPage);
			StyleButton(btnNextPage);

			// Bỏ bôi xanh số trong TextBox
			txtCurrentPage.SelectionStart = txtCurrentPage.Text.Length;
			txtCurrentPage.SelectionLength = 0;
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

				// Ép focus vào nút sau khi kết thúc chu kỳ sự kiện
				this.BeginInvoke(new Action(() => btnPrevPage.Focus()));
			}
		}

		private void BtnNextPage_Click(object sender, EventArgs e)
		{
			if (_currentPage < _totalPages)
			{
				_currentPage++;
				UpdateUI();
				PageChanged?.Invoke(this, _currentPage);

				// Ép focus vào nút sau khi kết thúc chu kỳ sự kiện
				this.BeginInvoke(new Action(() => btnNextPage.Focus()));
			}
		}

		private void TxtCurrentPage_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
			{
				e.Handled = true;
				return;
			}

			if (e.KeyChar == (char)Keys.Enter)
			{
				e.Handled = true;
				JumpToPage();
				// Sau khi Enter, chuyển focus ra khỏi TextBox để trông tự nhiên hơn
				this.ActiveControl = null;
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
				if (newPage >= 1 && newPage <= _totalPages)
				{
					if (newPage != _currentPage)
					{
						_currentPage = newPage;
						UpdateUI();
						PageChanged?.Invoke(this, _currentPage);
					}
				}
				else
				{
					txtCurrentPage.Text = _currentPage.ToString();
				}
			}
			else
			{
				txtCurrentPage.Text = _currentPage.ToString();
			}
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