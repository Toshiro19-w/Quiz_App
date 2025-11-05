using System.Drawing;
using System.Windows.Forms;
using WinFormsApp1.Helpers;

namespace WinFormsApp1.View.Admin
{
    partial class UserManagementControl
    {
        private System.ComponentModel.IContainer components = null;
        private DataGridView dataGridView;
        private TextBox txtEmail;
        private TextBox txtUsername;
        private TextBox txtFullName;
        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnSave;
        private Button btnCancel;
        private Panel formPanel;
        private Panel mainContainer;
        private Label titleLabel;

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
            txtFullName = new TextBox();
            txtUsername = new TextBox();
            txtEmail = new TextBox();
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
            mainContainer.Size = new Size(1200, 800);
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
            formPanel.Controls.Add(txtFullName);
            formPanel.Controls.Add(txtUsername);
            formPanel.Controls.Add(txtEmail);
            formPanel.Location = new Point(847, 80);
            formPanel.Name = "formPanel";
            formPanel.Size = new Size(350, 450);
            formPanel.TabIndex = 2;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(107, 114, 128);
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(160, 310);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(140, 35);
            btnCancel.TabIndex = 7;
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
            btnSave.Location = new Point(10, 310);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(140, 35);
            btnSave.TabIndex = 6;
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
            btnDelete.Location = new Point(210, 260);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(90, 35);
            btnDelete.TabIndex = 5;
            btnDelete.Text = "🗑️ Xóa";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += BtnDelete_Click;
            // 
            // btnEdit
            // 
            btnEdit.BackColor = Color.FromArgb(34, 197, 94);
            btnEdit.FlatStyle = FlatStyle.Flat;
            btnEdit.ForeColor = Color.White;
            btnEdit.Location = new Point(110, 260);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(90, 35);
            btnEdit.TabIndex = 4;
            btnEdit.Text = "✏️ Sửa";
            btnEdit.UseVisualStyleBackColor = false;
            btnEdit.Click += BtnEdit_Click;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.FromArgb(52, 144, 220);
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.ForeColor = Color.White;
            btnAdd.Location = new Point(10, 260);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(90, 35);
            btnAdd.TabIndex = 3;
            btnAdd.Text = "➕ Thêm";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += BtnAdd_Click;
            // 
            // txtFullName
            // 
            txtFullName.Location = new Point(10, 190);
            txtFullName.Name = "txtFullName";
            txtFullName.PlaceholderText = "Họ tên";
            txtFullName.Size = new Size(310, 27);
            txtFullName.TabIndex = 2;
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(10, 110);
            txtUsername.Name = "txtUsername";
            txtUsername.PlaceholderText = "Username";
            txtUsername.Size = new Size(310, 27);
            txtUsername.TabIndex = 1;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(10, 30);
            txtEmail.Name = "txtEmail";
            txtEmail.PlaceholderText = "Email";
            txtEmail.Size = new Size(310, 27);
            txtEmail.TabIndex = 0;
            // 
            // dataGridView
            // 
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.Location = new Point(20, 80);
            dataGridView.Name = "dataGridView";
            dataGridView.RowHeadersWidth = 51;
            dataGridView.Size = new Size(821, 450);
            dataGridView.TabIndex = 1;
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            titleLabel.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            titleLabel.ForeColor = Color.FromArgb(45, 55, 72);
            titleLabel.Location = new Point(20, 20);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(341, 46);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "Quản lý người dùng";
            // 
            // UserManagementControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 249, 250);
            Controls.Add(mainContainer);
            Name = "UserManagementControl";
            Size = new Size(1200, 800);
            Load += UserManagementControl_Load;
            Resize += UserManagementControl_Resize;
            mainContainer.ResumeLayout(false);
            mainContainer.PerformLayout();
            formPanel.ResumeLayout(false);
            formPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            ResumeLayout(false);
        }
    }
}
