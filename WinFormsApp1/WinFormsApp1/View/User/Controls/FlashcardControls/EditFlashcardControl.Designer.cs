using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp1.View.User.Controls.FlashcardControls
{
    partial class EditFlashcardControl
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
        private TextBox txtLanguage;
        private Panel tipsPanel;
        private Label lblTipsIcon;
        private Label lblTipsTitle;
        private Label lblTip1;
        private Label lblTip2;
        private Label lblTip3;
        private Label lblTip4;
        private Label lblTip5;
        private Panel cardsPanel;
        private Label lblCardsHeader;
        private FlowLayoutPanel flowCards;
        private Button btnAddCard;
        private Panel footerPanel;
        private Button btnCancel;
        private Label lblCardCount;
        private Button btnSave;

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
            this.mainContainer = new System.Windows.Forms.Panel();
            this.headerPanel = new System.Windows.Forms.Panel();
            this.lblHeader = new System.Windows.Forms.Label();
            this.contentPanel = new System.Windows.Forms.Panel();
            this.infoPanel = new System.Windows.Forms.Panel();
            this.lblTitleLabel = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.lblDescLabel = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblVisibilityLabel = new System.Windows.Forms.Label();
            this.cboVisibility = new System.Windows.Forms.ComboBox();
            this.lblLanguageLabel = new System.Windows.Forms.Label();
            this.txtLanguage = new System.Windows.Forms.TextBox();
            this.tipsPanel = new System.Windows.Forms.Panel();
            this.lblTipsIcon = new System.Windows.Forms.Label();
            this.lblTipsTitle = new System.Windows.Forms.Label();
            this.lblTip1 = new System.Windows.Forms.Label();
            this.lblTip2 = new System.Windows.Forms.Label();
            this.lblTip3 = new System.Windows.Forms.Label();
            this.lblTip4 = new System.Windows.Forms.Label();
            this.lblTip5 = new System.Windows.Forms.Label();
            this.cardsPanel = new System.Windows.Forms.Panel();
            this.lblCardsHeader = new System.Windows.Forms.Label();
            this.flowCards = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAddCard = new System.Windows.Forms.Button();
            this.footerPanel = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblCardCount = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.mainContainer.SuspendLayout();
            this.headerPanel.SuspendLayout();
            this.contentPanel.SuspendLayout();
            this.infoPanel.SuspendLayout();
            this.tipsPanel.SuspendLayout();
            this.cardsPanel.SuspendLayout();
            this.footerPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainContainer
            // 
            this.mainContainer.AutoScroll = true;
            this.mainContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.mainContainer.Controls.Add(this.footerPanel);
            this.mainContainer.Controls.Add(this.btnAddCard);
            this.mainContainer.Controls.Add(this.cardsPanel);
            this.mainContainer.Controls.Add(this.tipsPanel);
            this.mainContainer.Controls.Add(this.infoPanel);
            this.mainContainer.Controls.Add(this.headerPanel);
            this.mainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainContainer.Location = new System.Drawing.Point(0, 0);
            this.mainContainer.Name = "mainContainer";
            this.mainContainer.Padding = new System.Windows.Forms.Padding(30, 20, 30, 20);
            this.mainContainer.Size = new System.Drawing.Size(1200, 800);
            this.mainContainer.TabIndex = 0;
            // 
            // headerPanel
            // 
            this.headerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.headerPanel.Controls.Add(this.lblHeader);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Location = new System.Drawing.Point(30, 20);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(1140, 65);
            this.headerPanel.TabIndex = 0;
            // 
            // lblHeader
            // 
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(1140, 65);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "✏️ Chỉnh sửa bộ Flashcard";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // infoPanel
            // 
            this.infoPanel.BackColor = System.Drawing.Color.White;
            this.infoPanel.Controls.Add(this.lblTitleLabel);
            this.infoPanel.Controls.Add(this.txtTitle);
            this.infoPanel.Controls.Add(this.lblDescLabel);
            this.infoPanel.Controls.Add(this.txtDescription);
            this.infoPanel.Controls.Add(this.lblVisibilityLabel);
            this.infoPanel.Controls.Add(this.cboVisibility);
            this.infoPanel.Controls.Add(this.lblLanguageLabel);
            this.infoPanel.Controls.Add(this.txtLanguage);
            this.infoPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.infoPanel.Location = new System.Drawing.Point(30, 85);
            this.infoPanel.Name = "infoPanel";
            this.infoPanel.Padding = new System.Windows.Forms.Padding(35, 25, 35, 25);
            this.infoPanel.Size = new System.Drawing.Size(1140, 300);
            this.infoPanel.TabIndex = 1;
            // 
            // lblTitleLabel
            // 
            this.lblTitleLabel.AutoSize = true;
            this.lblTitleLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTitleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.lblTitleLabel.Location = new System.Drawing.Point(35, 25);
            this.lblTitleLabel.Name = "lblTitleLabel";
            this.lblTitleLabel.Size = new System.Drawing.Size(87, 23);
            this.lblTitleLabel.TabIndex = 0;
            this.lblTitleLabel.Text = "Tiêu đề *";
            // 
            // txtTitle
            // 
            this.txtTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTitle.Location = new System.Drawing.Point(35, 53);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.PlaceholderText = "Ví dụ: Từ vựng tiếng Anh cơ bản";
            this.txtTitle.Size = new System.Drawing.Size(1070, 30);
            this.txtTitle.TabIndex = 1;
            // 
            // lblDescLabel
            // 
            this.lblDescLabel.AutoSize = true;
            this.lblDescLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblDescLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.lblDescLabel.Location = new System.Drawing.Point(35, 100);
            this.lblDescLabel.Name = "lblDescLabel";
            this.lblDescLabel.Size = new System.Drawing.Size(51, 23);
            this.lblDescLabel.TabIndex = 2;
            this.lblDescLabel.Text = "Mô tả";
            // 
            // txtDescription
            // 
            this.txtDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDescription.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtDescription.Location = new System.Drawing.Point(35, 128);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.PlaceholderText = "Mô tả ngắn gọn về nội dung bộ flashcard";
            this.txtDescription.Size = new System.Drawing.Size(1070, 65);
            this.txtDescription.TabIndex = 3;
            // 
            // lblVisibilityLabel
            // 
            this.lblVisibilityLabel.AutoSize = true;
            this.lblVisibilityLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblVisibilityLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.lblVisibilityLabel.Location = new System.Drawing.Point(35, 210);
            this.lblVisibilityLabel.Name = "lblVisibilityLabel";
            this.lblVisibilityLabel.Size = new System.Drawing.Size(156, 23);
            this.lblVisibilityLabel.TabIndex = 4;
            this.lblVisibilityLabel.Text = "Chế độ hiển thị *";
            // 
            // cboVisibility
            // 
            this.cboVisibility.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboVisibility.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboVisibility.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboVisibility.FormattingEnabled = true;
            this.cboVisibility.Items.AddRange(new object[] {
            "Public",
            "Private"});
            this.cboVisibility.Location = new System.Drawing.Point(35, 238);
            this.cboVisibility.Name = "cboVisibility";
            this.cboVisibility.Size = new System.Drawing.Size(515, 31);
            this.cboVisibility.TabIndex = 5;
            // 
            // lblLanguageLabel
            // 
            this.lblLanguageLabel.AutoSize = true;
            this.lblLanguageLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblLanguageLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.lblLanguageLabel.Location = new System.Drawing.Point(590, 210);
            this.lblLanguageLabel.Name = "lblLanguageLabel";
            this.lblLanguageLabel.Size = new System.Drawing.Size(86, 23);
            this.lblLanguageLabel.TabIndex = 6;
            this.lblLanguageLabel.Text = "Ngôn ngữ";
            // 
            // txtLanguage
            // 
            this.txtLanguage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtLanguage.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtLanguage.Location = new System.Drawing.Point(590, 238);
            this.txtLanguage.Name = "txtLanguage";
            this.txtLanguage.PlaceholderText = "Ví dụ: Tiếng Việt, English";
            this.txtLanguage.Size = new System.Drawing.Size(515, 30);
            this.txtLanguage.TabIndex = 7;
            // 
            // tipsPanel
            // 
            this.tipsPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(245)))), ((int)(((byte)(253)))));
            this.tipsPanel.Controls.Add(this.lblTipsIcon);
            this.tipsPanel.Controls.Add(this.lblTipsTitle);
            this.tipsPanel.Controls.Add(this.lblTip1);
            this.tipsPanel.Controls.Add(this.lblTip2);
            this.tipsPanel.Controls.Add(this.lblTip3);
            this.tipsPanel.Controls.Add(this.lblTip4);
            this.tipsPanel.Controls.Add(this.lblTip5);
            this.tipsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.tipsPanel.Location = new System.Drawing.Point(30, 385);
            this.tipsPanel.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.tipsPanel.Name = "tipsPanel";
            this.tipsPanel.Padding = new System.Windows.Forms.Padding(35, 20, 35, 20);
            this.tipsPanel.Size = new System.Drawing.Size(1140, 180);
            this.tipsPanel.TabIndex = 2;
            // 
            // lblTipsIcon
            // 
            this.lblTipsIcon.AutoSize = true;
            this.lblTipsIcon.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.lblTipsIcon.Location = new System.Drawing.Point(30, 18);
            this.lblTipsIcon.Name = "lblTipsIcon";
            this.lblTipsIcon.Size = new System.Drawing.Size(35, 32);
            this.lblTipsIcon.TabIndex = 0;
            this.lblTipsIcon.Text = "💡";
            // 
            // lblTipsTitle
            // 
            this.lblTipsTitle.AutoSize = true;
            this.lblTipsTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTipsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(87)))), ((int)(((byte)(155)))));
            this.lblTipsTitle.Location = new System.Drawing.Point(68, 22);
            this.lblTipsTitle.Name = "lblTipsTitle";
            this.lblTipsTitle.Size = new System.Drawing.Size(292, 28);
            this.lblTipsTitle.TabIndex = 1;
            this.lblTipsTitle.Text = "Mẹo tạo bộ Flashcard hiệu quả";
            // 
            // lblTip1
            // 
            this.lblTip1.AutoSize = true;
            this.lblTip1.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblTip1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.lblTip1.Location = new System.Drawing.Point(40, 65);
            this.lblTip1.Name = "lblTip1";
            this.lblTip1.Size = new System.Drawing.Size(340, 21);
            this.lblTip1.TabIndex = 2;
            this.lblTip1.Text = "• Đặt tiêu đề rõ ràng, dễ tìm kiếm";
            // 
            // lblTip2
            // 
            this.lblTip2.AutoSize = true;
            this.lblTip2.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblTip2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.lblTip2.Location = new System.Drawing.Point(40, 93);
            this.lblTip2.Name = "lblTip2";
            this.lblTip2.Size = new System.Drawing.Size(406, 21);
            this.lblTip2.TabIndex = 3;
            this.lblTip2.Text = "• Mỗi thẻ nên chứa một ý chính, ngắn gọn";
            // 
            // lblTip3
            // 
            this.lblTip3.AutoSize = true;
            this.lblTip3.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblTip3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.lblTip3.Location = new System.Drawing.Point(40, 121);
            this.lblTip3.Name = "lblTip3";
            this.lblTip3.Size = new System.Drawing.Size(502, 21);
            this.lblTip3.TabIndex = 4;
            this.lblTip3.Text = "• Sử dụng ảnh minh họa để tăng khả năng ghi nhớ";
            // 
            // lblTip4
            // 
            this.lblTip4.AutoSize = true;
            this.lblTip4.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblTip4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.lblTip4.Location = new System.Drawing.Point(600, 65);
            this.lblTip4.Name = "lblTip4";
            this.lblTip4.Size = new System.Drawing.Size(381, 21);
            this.lblTip4.TabIndex = 5;
            this.lblTip4.Text = "• Thêm tag để dễ dàng phân loại và tìm kiếm";
            // 
            // lblTip5
            // 
            this.lblTip5.AutoSize = true;
            this.lblTip5.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblTip5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(66)))), ((int)(((byte)(66)))));
            this.lblTip5.Location = new System.Drawing.Point(600, 93);
            this.lblTip5.Name = "lblTip5";
            this.lblTip5.Size = new System.Drawing.Size(412, 21);
            this.lblTip5.TabIndex = 6;
            this.lblTip5.Text = "• Bắt đầu với 10-20 thẻ, sau đó bổ sung dần";
            // 
            // cardsPanel
            // 
            this.cardsPanel.BackColor = System.Drawing.Color.Transparent;
            this.cardsPanel.Controls.Add(this.flowCards);
            this.cardsPanel.Controls.Add(this.lblCardsHeader);
            this.cardsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.cardsPanel.Location = new System.Drawing.Point(30, 565);
            this.cardsPanel.Margin = new System.Windows.Forms.Padding(0, 15, 0, 0);
            this.cardsPanel.Name = "cardsPanel";
            this.cardsPanel.Padding = new System.Windows.Forms.Padding(0, 15, 0, 0);
            this.cardsPanel.Size = new System.Drawing.Size(1140, 500);
            this.cardsPanel.TabIndex = 3;
            // 
            // lblCardsHeader
            // 
            this.lblCardsHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(118)))), ((int)(((byte)(210)))));
            this.lblCardsHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCardsHeader.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblCardsHeader.ForeColor = System.Drawing.Color.White;
            this.lblCardsHeader.Location = new System.Drawing.Point(0, 15);
            this.lblCardsHeader.Name = "lblCardsHeader";
            this.lblCardsHeader.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.lblCardsHeader.Size = new System.Drawing.Size(1140, 55);
            this.lblCardsHeader.TabIndex = 0;
            this.lblCardsHeader.Text = "📋 Các thẻ Flashcard (0)";
            this.lblCardsHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // flowCards
            // 
            this.flowCards.AutoScroll = true;
            this.flowCards.BackColor = System.Drawing.Color.White;
            this.flowCards.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowCards.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowCards.Location = new System.Drawing.Point(0, 70);
            this.flowCards.Name = "flowCards";
            this.flowCards.Padding = new System.Windows.Forms.Padding(145, 25, 145, 25);
            this.flowCards.Size = new System.Drawing.Size(1140, 430);
            this.flowCards.TabIndex = 1;
            this.flowCards.WrapContents = false;
            // 
            // btnAddCard
            // 
            this.btnAddCard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(118)))), ((int)(((byte)(210)))));
            this.btnAddCard.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddCard.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnAddCard.FlatAppearance.BorderSize = 0;
            this.btnAddCard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddCard.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnAddCard.ForeColor = System.Drawing.Color.White;
            this.btnAddCard.Location = new System.Drawing.Point(30, 1065);
            this.btnAddCard.Margin = new System.Windows.Forms.Padding(0);
            this.btnAddCard.Name = "btnAddCard";
            this.btnAddCard.Size = new System.Drawing.Size(1140, 55);
            this.btnAddCard.TabIndex = 4;
            this.btnAddCard.Text = "➕ Thêm thẻ";
            this.btnAddCard.UseVisualStyleBackColor = false;
            this.btnAddCard.Click += new System.EventHandler(this.btnAddCard_Click);
            // 
            // footerPanel
            // 
            this.footerPanel.BackColor = System.Drawing.Color.White;
            this.footerPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.footerPanel.Controls.Add(this.btnCancel);
            this.footerPanel.Controls.Add(this.lblCardCount);
            this.footerPanel.Controls.Add(this.btnSave);
            this.footerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.footerPanel.Location = new System.Drawing.Point(30, 1120);
            this.footerPanel.Margin = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.footerPanel.Name = "footerPanel";
            this.footerPanel.Padding = new System.Windows.Forms.Padding(25, 12, 25, 12);
            this.footerPanel.Size = new System.Drawing.Size(1140, 75);
            this.footerPanel.TabIndex = 5;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(158)))), ((int)(((byte)(158)))));
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(25, 12);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(140, 48);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "✖ Hủy";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblCardCount
            // 
            this.lblCardCount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCardCount.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblCardCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.lblCardCount.Location = new System.Drawing.Point(25, 12);
            this.lblCardCount.Name = "lblCardCount";
            this.lblCardCount.Padding = new System.Windows.Forms.Padding(180, 0, 0, 0);
            this.lblCardCount.Size = new System.Drawing.Size(1088, 49);
            this.lblCardCount.TabIndex = 1;
            this.lblCardCount.Text = "🃏 0 thẻ";
            this.lblCardCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(913, 12);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(200, 49);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "💾 Lưu thay đổi";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // EditFlashcardControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.mainContainer);
            this.Name = "EditFlashcardControl";
            this.Size = new System.Drawing.Size(1200, 800);
            this.mainContainer.ResumeLayout(false);
            this.headerPanel.ResumeLayout(false);
            this.infoPanel.ResumeLayout(false);
            this.infoPanel.PerformLayout();
            this.tipsPanel.ResumeLayout(false);
            this.tipsPanel.PerformLayout();
            this.cardsPanel.ResumeLayout(false);
            this.footerPanel.ResumeLayout(false);
            this.ResumeLayout(false);

            // Set default values
            this.cboVisibility.SelectedIndex = 0; // Public
        }

        #endregion
    }
}
