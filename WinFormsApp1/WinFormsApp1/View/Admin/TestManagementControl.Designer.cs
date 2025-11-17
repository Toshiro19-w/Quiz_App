using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp1.View.Admin
{
    partial class TestManagementControl
    {
        private System.ComponentModel.IContainer components = null;
        private DataGridView dataGridView;
        private TextBox txtTitle;
        private TextBox txtTimeLimit;
        private TextBox txtDescription;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnSave;
        private Button btnCancel;
        private Panel formPanel;
        private Panel mainContainer;
        private Label titleLabel;
        private Label descLabel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            mainContainer = new Panel();
            formPanel = new Panel();
            btnCancel = new Button();
            btnSave = new Button();
            btnDelete = new Button();
            btnEdit = new Button();
            btnAdd = new Button();
            txtDescription = new TextBox();
            descLabel = new Label();
            txtTimeLimit = new TextBox();
            txtTitle = new TextBox();
            dataGridView = new DataGridView();
            titleLabel = new Label();
            mainContainer.SuspendLayout();
            formPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            SuspendLayout();
            // 
            // mainContainer
            // 
            mainContainer.BackColor = Color.FromArgb(248, 249, 250);
            mainContainer.Controls.Add(formPanel);
            mainContainer.Controls.Add(dataGridView);
            mainContainer.Controls.Add(titleLabel);
            mainContainer.Dock = DockStyle.Fill;
            mainContainer.Location = new Point(0, 0);
            mainContainer.Name = "mainContainer";
            mainContainer.Padding = new Padding(20);
            mainContainer.TabIndex = 0;
            // 
            // formPanel
            // 
            formPanel.BackColor = Color.White;
            formPanel.Controls.Add(btnCancel);
            formPanel.Controls.Add(btnSave);
            formPanel.Controls.Add(btnDelete);
            formPanel.Controls.Add(btnEdit);
            formPanel.Controls.Add(btnAdd);
            formPanel.Controls.Add(txtDescription);
            formPanel.Controls.Add(descLabel);
            formPanel.Controls.Add(txtTimeLimit);
            formPanel.Controls.Add(txtTitle);
            formPanel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            formPanel.Location = new Point(1200, 80);
            formPanel.Name = "formPanel";
            formPanel.Size = new Size(640, 860);
            formPanel.TabIndex = 2;
            formPanel.MaximumSize = new Size(700, 2000);
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(107, 114, 128);
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(340, 640);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(280, 45);
            btnCancel.TabIndex = 8;
            btnCancel.Text = "❌ Hủy";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Visible = false;
            btnCancel.Click += BtnCancel_Click;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(52, 144, 220);
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(20, 640);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(280, 45);
            btnSave.TabIndex = 7;
            btnSave.Text = "💾 Lưu";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Visible = false;
            btnSave.Click += BtnSave_Click;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.FromArgb(239, 68, 68);
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.ForeColor = Color.White;
            btnDelete.Location = new Point(460, 580);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(150, 45);
            btnDelete.TabIndex = 6;
            btnDelete.Text = "🗑️ Xóa";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += BtnDelete_Click;
            // 
            // btnEdit
            // 
            btnEdit.BackColor = Color.FromArgb(34, 197, 94);
            btnEdit.FlatStyle = FlatStyle.Flat;
            btnEdit.ForeColor = Color.White;
            btnEdit.Location = new Point(300, 580);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(150, 45);
            btnEdit.TabIndex = 5;
            btnEdit.Text = "✏️ Sửa";
            btnEdit.UseVisualStyleBackColor = false;
            btnEdit.Click += BtnEdit_Click;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.FromArgb(52, 144, 220);
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.ForeColor = Color.White;
            btnAdd.Location = new Point(20, 580);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(200, 45);
            btnAdd.TabIndex = 4;
            btnAdd.Text = "➕ Thêm";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += BtnAdd_Click;
            // 
            // txtDescription
            // 
            txtDescription.Location = new Point(20, 160);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(580, 280);
            txtDescription.TabIndex = 3;
            // 
            // descLabel
            // 
            descLabel.AutoSize = true;
            descLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            descLabel.ForeColor = Color.FromArgb(45, 55, 72);
            descLabel.Location = new Point(16, 120);
            descLabel.Name = "descLabel";
            descLabel.Size = new Size(78, 28);
            descLabel.TabIndex = 2;
            descLabel.Text = "Mô tả:";
            // 
            // txtTimeLimit
            // 
            txtTimeLimit.Location = new Point(20, 110);
            txtTimeLimit.Name = "txtTimeLimit";
            txtTimeLimit.PlaceholderText = "Thời gian (phút)";
            txtTimeLimit.Size = new Size(580, 35);
            txtTimeLimit.TabIndex = 1;
            // 
            // txtTitle
            // 
            txtTitle.Location = new Point(20, 30);
            txtTitle.Name = "txtTitle";
            txtTitle.PlaceholderText = "Tên bài kiểm tra";
            txtTitle.Size = new Size(580, 35);
            txtTitle.TabIndex = 0;
            // 
            // dataGridView
            // 
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.Location = new Point(20, 80);
            dataGridView.Name = "dataGridView";
            dataGridView.RowHeadersWidth = 51;
            dataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            dataGridView.Size = new Size(1160, 860);
            dataGridView.TabIndex = 1;
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            titleLabel.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            titleLabel.ForeColor = Color.FromArgb(45, 55, 72);
            titleLabel.Location = new Point(20, 20);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(342, 62);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "Quản lý bài kiểm tra";
            // 
            // TestManagementControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 249, 250);
            Controls.Add(mainContainer);
            Name = "TestManagementControl";
            Size = new Size(1898, 1024);
            Load += TestManagementControl_Load;
            Resize += TestManagementControl_Resize;
            mainContainer.ResumeLayout(false);
            mainContainer.PerformLayout();
            formPanel.ResumeLayout(false);
            formPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            ResumeLayout(false);
        }
    }
}
