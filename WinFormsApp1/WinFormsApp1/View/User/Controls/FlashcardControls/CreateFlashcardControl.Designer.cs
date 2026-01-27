using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp1.View.User.Controls.FlashcardControls
{
    partial class CreateFlashcardControl
    {
        private System.ComponentModel.IContainer components = null;

        private Panel mainContainer;
        private Panel headerPanel;
        private Label lblHeader;
        private Panel contentPanel;
        private Panel infoPanel;
        private Label lblTitleLabel;
        private TextBox txtTitle;
        private Label lblDescLabel;
        private TextBox txtDescription;
        private Label lblVisibilityLabel;
        private ComboBox cboVisibility;
        private Label lblLanguageLabel;
        private ComboBox cboLanguage;
        private Panel cardsPanel;
        private Label lblCardsHeader;
        private FlowLayoutPanel flowCards;
        private Panel paginationPanel;
        private Button btnPrevPage;
        private Label lblPageInfo;
        private Button btnNextPage;
        private Button btnAddCard;
        private Panel footerPanel;
        private Button btnCancel;
        private Label lblCardCount;
        private Button btnCreate;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

		#region Component Designer generated code

		private void InitializeComponent()
		{
			mainContainer = new Panel();
			footerPanel = new Panel();
			btnCancel = new Button();
			lblCardCount = new Label();
			btnCreate = new Button();
			btnAddCard = new Button();
			paginationPanel = new Panel();
			btnPrevPage = new Button();
			lblPageInfo = new Label();
			btnNextPage = new Button();
			cardsPanel = new Panel();
			flowCards = new FlowLayoutPanel();
			lblCardsHeader = new Label();
			infoPanel = new Panel();
			lblTitleLabel = new Label();
			txtTitle = new TextBox();
			lblDescLabel = new Label();
			txtDescription = new TextBox();
			lblVisibilityLabel = new Label();
			cboVisibility = new ComboBox();
			lblLanguageLabel = new Label();
			cboLanguage = new ComboBox();
			headerPanel = new Panel();
			lblHeader = new Label();
			contentPanel = new Panel();
			mainContainer.SuspendLayout();
			footerPanel.SuspendLayout();
			paginationPanel.SuspendLayout();
			cardsPanel.SuspendLayout();
			infoPanel.SuspendLayout();
			headerPanel.SuspendLayout();
			SuspendLayout();
			// 
			// mainContainer
			// 
			mainContainer.AutoScroll = true;
			mainContainer.BackColor = Color.FromArgb(245, 247, 250);
			mainContainer.Controls.Add(footerPanel);
			mainContainer.Controls.Add(btnAddCard);
			mainContainer.Controls.Add(paginationPanel);
			mainContainer.Controls.Add(cardsPanel);
			mainContainer.Controls.Add(infoPanel);
			mainContainer.Controls.Add(headerPanel);
			mainContainer.Dock = DockStyle.Fill;
			mainContainer.Location = new Point(0, 0);
			mainContainer.Margin = new Padding(4, 4, 4, 4);
			mainContainer.Name = "mainContainer";
			mainContainer.Padding = new Padding(38, 25, 38, 25);
			mainContainer.Size = new Size(1500, 1000);
			mainContainer.TabIndex = 0;
			// 
			// footerPanel
			// 
			footerPanel.BackColor = Color.White;
			footerPanel.BorderStyle = BorderStyle.FixedSingle;
			footerPanel.Controls.Add(btnCancel);
			footerPanel.Controls.Add(lblCardCount);
			footerPanel.Controls.Add(btnCreate);
			footerPanel.Dock = DockStyle.Top;
			footerPanel.Location = new Point(38, 1250);
			footerPanel.Margin = new Padding(0, 12, 0, 0);
			footerPanel.Name = "footerPanel";
			footerPanel.Padding = new Padding(31, 15, 31, 15);
			footerPanel.Size = new Size(1398, 93);
			footerPanel.TabIndex = 6;
			// 
			// btnCancel
			// 
			btnCancel.BackColor = Color.FromArgb(158, 158, 158);
			btnCancel.Cursor = Cursors.Hand;
			btnCancel.FlatAppearance.BorderSize = 0;
			btnCancel.FlatStyle = FlatStyle.Flat;
			btnCancel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			btnCancel.ForeColor = Color.White;
			btnCancel.Location = new Point(31, 15);
			btnCancel.Margin = new Padding(4, 4, 4, 4);
			btnCancel.Name = "btnCancel";
			btnCancel.Size = new Size(175, 60);
			btnCancel.TabIndex = 0;
			btnCancel.Text = "✖ Hủy";
			btnCancel.UseVisualStyleBackColor = false;
			btnCancel.Click += btnCancel_Click;
			// 
			// lblCardCount
			// 
			lblCardCount.Dock = DockStyle.Fill;
			lblCardCount.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
			lblCardCount.ForeColor = Color.FromArgb(51, 51, 51);
			lblCardCount.Location = new Point(31, 15);
			lblCardCount.Margin = new Padding(4, 0, 4, 0);
			lblCardCount.Name = "lblCardCount";
			lblCardCount.Padding = new Padding(225, 0, 0, 0);
			lblCardCount.Size = new Size(1084, 61);
			lblCardCount.TabIndex = 1;
			lblCardCount.Text = "🃏 1 thẻ";
			lblCardCount.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// btnCreate
			// 
			btnCreate.BackColor = Color.FromArgb(0, 172, 193);
			btnCreate.Cursor = Cursors.Hand;
			btnCreate.Dock = DockStyle.Right;
			btnCreate.FlatAppearance.BorderSize = 0;
			btnCreate.FlatStyle = FlatStyle.Flat;
			btnCreate.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
			btnCreate.ForeColor = Color.White;
			btnCreate.Location = new Point(1115, 15);
			btnCreate.Margin = new Padding(4, 4, 4, 4);
			btnCreate.Name = "btnCreate";
			btnCreate.Size = new Size(250, 61);
			btnCreate.TabIndex = 2;
			btnCreate.Text = "✓ Tạo bộ Flashcard";
			btnCreate.UseVisualStyleBackColor = false;
			btnCreate.Click += btnCreate_Click;
			// 
			// btnAddCard
			// 
			btnAddCard.BackColor = Color.FromArgb(25, 118, 210);
			btnAddCard.Cursor = Cursors.Hand;
			btnAddCard.Dock = DockStyle.Top;
			btnAddCard.FlatAppearance.BorderSize = 0;
			btnAddCard.FlatStyle = FlatStyle.Flat;
			btnAddCard.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
			btnAddCard.ForeColor = Color.White;
			btnAddCard.Location = new Point(38, 1181);
			btnAddCard.Margin = new Padding(0);
			btnAddCard.Name = "btnAddCard";
			btnAddCard.Size = new Size(1398, 69);
			btnAddCard.TabIndex = 5;
			btnAddCard.Text = "➕ Thêm thẻ";
			btnAddCard.UseVisualStyleBackColor = false;
			btnAddCard.Click += btnAddCard_Click;
			// 
			// paginationPanel
			// 
			paginationPanel.BackColor = Color.White;
			paginationPanel.Controls.Add(btnPrevPage);
			paginationPanel.Controls.Add(lblPageInfo);
			paginationPanel.Controls.Add(btnNextPage);
			paginationPanel.Dock = DockStyle.Top;
			paginationPanel.Location = new Point(38, 1106);
			paginationPanel.Margin = new Padding(4, 4, 4, 4);
			paginationPanel.Name = "paginationPanel";
			paginationPanel.Size = new Size(1398, 75);
			paginationPanel.TabIndex = 4;
			// 
			// btnPrevPage
			// 
			btnPrevPage.BackColor = Color.FromArgb(25, 118, 210);
			btnPrevPage.Cursor = Cursors.Hand;
			btnPrevPage.FlatAppearance.BorderSize = 0;
			btnPrevPage.FlatStyle = FlatStyle.Flat;
			btnPrevPage.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			btnPrevPage.ForeColor = Color.White;
			btnPrevPage.Location = new Point(475, 12);
			btnPrevPage.Margin = new Padding(4, 4, 4, 4);
			btnPrevPage.Name = "btnPrevPage";
			btnPrevPage.Size = new Size(150, 50);
			btnPrevPage.TabIndex = 0;
			btnPrevPage.Text = "◄ Trước";
			btnPrevPage.UseVisualStyleBackColor = false;
			btnPrevPage.Click += btnPrevPage_Click;
			// 
			// lblPageInfo
			// 
			lblPageInfo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			lblPageInfo.ForeColor = Color.FromArgb(51, 51, 51);
			lblPageInfo.Location = new Point(638, 12);
			lblPageInfo.Margin = new Padding(4, 0, 4, 0);
			lblPageInfo.Name = "lblPageInfo";
			lblPageInfo.Size = new Size(150, 50);
			lblPageInfo.TabIndex = 1;
			lblPageInfo.Text = "Trang 1 / 1";
			lblPageInfo.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// btnNextPage
			// 
			btnNextPage.BackColor = Color.FromArgb(25, 118, 210);
			btnNextPage.Cursor = Cursors.Hand;
			btnNextPage.FlatAppearance.BorderSize = 0;
			btnNextPage.FlatStyle = FlatStyle.Flat;
			btnNextPage.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			btnNextPage.ForeColor = Color.White;
			btnNextPage.Location = new Point(800, 12);
			btnNextPage.Margin = new Padding(4, 4, 4, 4);
			btnNextPage.Name = "btnNextPage";
			btnNextPage.Size = new Size(150, 50);
			btnNextPage.TabIndex = 2;
			btnNextPage.Text = "Sau ►";
			btnNextPage.UseVisualStyleBackColor = false;
			btnNextPage.Click += btnNextPage_Click;
			// 
			// cardsPanel
			// 
			cardsPanel.BackColor = Color.Transparent;
			cardsPanel.Controls.Add(flowCards);
			cardsPanel.Controls.Add(lblCardsHeader);
			cardsPanel.Dock = DockStyle.Top;
			cardsPanel.Location = new Point(38, 481);
			cardsPanel.Margin = new Padding(0, 19, 0, 0);
			cardsPanel.Name = "cardsPanel";
			cardsPanel.Padding = new Padding(0, 19, 0, 0);
			cardsPanel.Size = new Size(1398, 625);
			cardsPanel.TabIndex = 3;
			// 
			// flowCards
			// 
			flowCards.AutoScroll = true;
			flowCards.BackColor = Color.White;
			flowCards.Dock = DockStyle.Fill;
			flowCards.FlowDirection = FlowDirection.TopDown;
			flowCards.Location = new Point(0, 88);
			flowCards.Margin = new Padding(4, 4, 4, 4);
			flowCards.Name = "flowCards";
			flowCards.Padding = new Padding(181, 31, 181, 31);
			flowCards.Size = new Size(1398, 537);
			flowCards.TabIndex = 1;
			flowCards.WrapContents = false;
			// 
			// lblCardsHeader
			// 
			lblCardsHeader.BackColor = Color.FromArgb(25, 118, 210);
			lblCardsHeader.Dock = DockStyle.Top;
			lblCardsHeader.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
			lblCardsHeader.ForeColor = Color.White;
			lblCardsHeader.Location = new Point(0, 19);
			lblCardsHeader.Margin = new Padding(4, 0, 4, 0);
			lblCardsHeader.Name = "lblCardsHeader";
			lblCardsHeader.Padding = new Padding(25, 0, 0, 0);
			lblCardsHeader.Size = new Size(1398, 69);
			lblCardsHeader.TabIndex = 0;
			lblCardsHeader.Text = "📋 Các thẻ Flashcard (1)";
			lblCardsHeader.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// infoPanel
			// 
			infoPanel.BackColor = Color.White;
			infoPanel.Controls.Add(lblTitleLabel);
			infoPanel.Controls.Add(txtTitle);
			infoPanel.Controls.Add(lblDescLabel);
			infoPanel.Controls.Add(txtDescription);
			infoPanel.Controls.Add(lblVisibilityLabel);
			infoPanel.Controls.Add(cboVisibility);
			infoPanel.Controls.Add(lblLanguageLabel);
			infoPanel.Controls.Add(cboLanguage);
			infoPanel.Dock = DockStyle.Top;
			infoPanel.Location = new Point(38, 106);
			infoPanel.Margin = new Padding(4, 4, 4, 4);
			infoPanel.Name = "infoPanel";
			infoPanel.Padding = new Padding(44, 31, 44, 31);
			infoPanel.Size = new Size(1398, 375);
			infoPanel.TabIndex = 1;
			// 
			// lblTitleLabel
			// 
			lblTitleLabel.AutoSize = true;
			lblTitleLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			lblTitleLabel.ForeColor = Color.FromArgb(51, 51, 51);
			lblTitleLabel.Location = new Point(44, 31);
			lblTitleLabel.Margin = new Padding(4, 0, 4, 0);
			lblTitleLabel.Name = "lblTitleLabel";
			lblTitleLabel.Size = new Size(98, 28);
			lblTitleLabel.TabIndex = 0;
			lblTitleLabel.Text = "Tiêu đề *";
			// 
			// txtTitle
			// 
			txtTitle.BorderStyle = BorderStyle.FixedSingle;
			txtTitle.Font = new Font("Segoe UI", 10F);
			txtTitle.Location = new Point(44, 66);
			txtTitle.Margin = new Padding(4, 4, 4, 4);
			txtTitle.Name = "txtTitle";
			txtTitle.PlaceholderText = "Ví dụ: Từ vựng tiếng Anh cơ bản";
			txtTitle.Size = new Size(1337, 34);
			txtTitle.TabIndex = 1;
			// 
			// lblDescLabel
			// 
			lblDescLabel.AutoSize = true;
			lblDescLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			lblDescLabel.ForeColor = Color.FromArgb(51, 51, 51);
			lblDescLabel.Location = new Point(44, 125);
			lblDescLabel.Margin = new Padding(4, 0, 4, 0);
			lblDescLabel.Name = "lblDescLabel";
			lblDescLabel.Size = new Size(68, 28);
			lblDescLabel.TabIndex = 2;
			lblDescLabel.Text = "Mô tả";
			// 
			// txtDescription
			// 
			txtDescription.BorderStyle = BorderStyle.FixedSingle;
			txtDescription.Font = new Font("Segoe UI", 10F);
			txtDescription.Location = new Point(44, 160);
			txtDescription.Margin = new Padding(4, 4, 4, 4);
			txtDescription.Multiline = true;
			txtDescription.Name = "txtDescription";
			txtDescription.PlaceholderText = "Mô tả ngắn gọn về nội dung bộ flashcard";
			txtDescription.Size = new Size(1337, 81);
			txtDescription.TabIndex = 3;
			// 
			// lblVisibilityLabel
			// 
			lblVisibilityLabel.AutoSize = true;
			lblVisibilityLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			lblVisibilityLabel.ForeColor = Color.FromArgb(51, 51, 51);
			lblVisibilityLabel.Location = new Point(44, 262);
			lblVisibilityLabel.Margin = new Padding(4, 0, 4, 0);
			lblVisibilityLabel.Name = "lblVisibilityLabel";
			lblVisibilityLabel.Size = new Size(172, 28);
			lblVisibilityLabel.TabIndex = 4;
			lblVisibilityLabel.Text = "Chế độ hiển thị *";
			// 
			// cboVisibility
			// 
			cboVisibility.DropDownStyle = ComboBoxStyle.DropDownList;
			cboVisibility.FlatStyle = FlatStyle.Flat;
			cboVisibility.Font = new Font("Segoe UI", 10F);
			cboVisibility.FormattingEnabled = true;
			cboVisibility.Items.AddRange(new object[] { "Public", "Private" });
			cboVisibility.Location = new Point(44, 298);
			cboVisibility.Margin = new Padding(4, 4, 4, 4);
			cboVisibility.Name = "cboVisibility";
			cboVisibility.Size = new Size(643, 36);
			cboVisibility.TabIndex = 5;
			// 
			// lblLanguageLabel
			// 
			lblLanguageLabel.AutoSize = true;
			lblLanguageLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
			lblLanguageLabel.ForeColor = Color.FromArgb(51, 51, 51);
			lblLanguageLabel.Location = new Point(738, 262);
			lblLanguageLabel.Margin = new Padding(4, 0, 4, 0);
			lblLanguageLabel.Name = "lblLanguageLabel";
			lblLanguageLabel.Size = new Size(107, 28);
			lblLanguageLabel.TabIndex = 6;
			lblLanguageLabel.Text = "Ngôn ngữ";
			// 
			// cboLanguage
			// 
			cboLanguage.DropDownStyle = ComboBoxStyle.DropDownList;
			cboLanguage.FlatStyle = FlatStyle.Flat;
			cboLanguage.Font = new Font("Segoe UI", 10F);
			cboLanguage.FormattingEnabled = true;
			cboLanguage.Items.AddRange(new object[] { "Tiếng Việt", "English" });
			cboLanguage.Location = new Point(738, 298);
			cboLanguage.Margin = new Padding(4, 4, 4, 4);
			cboLanguage.Name = "cboLanguage";
			cboLanguage.Size = new Size(643, 36);
			cboLanguage.TabIndex = 7;
			// 
			// headerPanel
			// 
			headerPanel.BackColor = Color.FromArgb(0, 172, 193);
			headerPanel.Controls.Add(lblHeader);
			headerPanel.Dock = DockStyle.Top;
			headerPanel.Location = new Point(38, 25);
			headerPanel.Margin = new Padding(4, 4, 4, 4);
			headerPanel.Name = "headerPanel";
			headerPanel.Size = new Size(1398, 81);
			headerPanel.TabIndex = 0;
			// 
			// lblHeader
			// 
			lblHeader.Dock = DockStyle.Fill;
			lblHeader.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
			lblHeader.ForeColor = Color.White;
			lblHeader.Location = new Point(0, 0);
			lblHeader.Margin = new Padding(4, 0, 4, 0);
			lblHeader.Name = "lblHeader";
			lblHeader.Size = new Size(1398, 81);
			lblHeader.TabIndex = 0;
			lblHeader.Text = "Thông tin bộ Flashcard";
			lblHeader.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// contentPanel
			// 
			contentPanel.Location = new Point(0, 0);
			contentPanel.Name = "contentPanel";
			contentPanel.Size = new Size(200, 100);
			contentPanel.TabIndex = 0;
			// 
			// CreateFlashcardControl
			// 
			AutoScaleDimensions = new SizeF(10F, 25F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.White;
			Controls.Add(mainContainer);
			Margin = new Padding(4, 4, 4, 4);
			Name = "CreateFlashcardControl";
			Size = new Size(1500, 1000);
			mainContainer.ResumeLayout(false);
			footerPanel.ResumeLayout(false);
			paginationPanel.ResumeLayout(false);
			cardsPanel.ResumeLayout(false);
			infoPanel.ResumeLayout(false);
			infoPanel.PerformLayout();
			headerPanel.ResumeLayout(false);
			ResumeLayout(false);
		}

		#endregion
	}
}
