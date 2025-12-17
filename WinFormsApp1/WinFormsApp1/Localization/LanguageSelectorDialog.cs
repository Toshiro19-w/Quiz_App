using System;
using System.Drawing;
using System.Windows.Forms;
using WinFormsApp1.Helpers;

namespace WinFormsApp1.Localization
{
    /// <summary>
    /// Dialog để chọn ngôn ngữ cho ứng dụng
    /// </summary>
    public class LanguageSelectorDialog : Form
    {
        private Panel mainPanel;
        private Label lblTitle;
        private RadioButton rbVietnamese;
        private RadioButton rbEnglish;
        private Button btnSave;
        private Button btnCancel;

        public LanguageSelectorDialog()
        {
            InitializeComponent();
            LoadCurrentLanguage();
        }

        private void InitializeComponent()
        {
            this.Text = LanguageHelper.GetString("SelectLanguage");
            this.Size = new Size(350, 250);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.White;

            mainPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20)
            };

            // Title
            lblTitle = new Label
            {
                Text = "🌐 " + LanguageHelper.GetString("SelectLanguage"),
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = ColorPalette.TextPrimary,
                Location = new Point(20, 20),
                AutoSize = true
            };

            // Vietnamese option
            rbVietnamese = new RadioButton
            {
                Text = "🇻🇳 Tiếng Việt",
                Font = new Font("Segoe UI", 11),
                Location = new Point(30, 70),
                AutoSize = true,
                Cursor = Cursors.Hand
            };

            // English option
            rbEnglish = new RadioButton
            {
                Text = "🇺🇸 English",
                Font = new Font("Segoe UI", 11),
                Location = new Point(30, 110),
                AutoSize = true,
                Cursor = Cursors.Hand
            };

            // Save button
            btnSave = new Button
            {
                Text = LanguageHelper.GetString("Save"),
                Size = new Size(100, 35),
                Location = new Point(120, 160),
                BackColor = ColorPalette.Primary,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Click += BtnSave_Click;

            // Cancel button
            btnCancel = new Button
            {
                Text = LanguageHelper.GetString("Cancel"),
                Size = new Size(100, 35),
                Location = new Point(230, 160),
                BackColor = Color.FromArgb(108, 117, 125),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10),
                Cursor = Cursors.Hand
            };
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Click += (s, e) => this.DialogResult = DialogResult.Cancel;

            mainPanel.Controls.AddRange(new Control[] { lblTitle, rbVietnamese, rbEnglish, btnSave, btnCancel });
            this.Controls.Add(mainPanel);
        }

        private void LoadCurrentLanguage()
        {
            if (LanguageHelper.IsVietnamese)
            {
                rbVietnamese.Checked = true;
            }
            else
            {
                rbEnglish.Checked = true;
            }
        }

        private void BtnSave_Click(object? sender, EventArgs e)
        {
            string selectedLanguage = rbVietnamese.Checked ? "vi-VN" : "en-US";
            
            if (selectedLanguage != LanguageHelper.CurrentLanguageCode)
            {
                LanguageHelper.SetLanguage(selectedLanguage);
                
                // Hiển thị thông báo cần khởi động lại
                string message = selectedLanguage == "vi-VN" 
                    ? "Ngôn ngữ đã được thay đổi. Một số thay đổi sẽ áp dụng ngay, một số khác cần khởi động lại ứng dụng."
                    : "Language has been changed. Some changes will apply immediately, others require restarting the application.";
                
                MessageBox.Show(message, 
                    selectedLanguage == "vi-VN" ? "Thông báo" : "Notice",
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Information);
            }

            this.DialogResult = DialogResult.OK;
        }
    }
}
