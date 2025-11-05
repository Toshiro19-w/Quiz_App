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
            formPanel.Controls.Add(txtDescription);
            formPanel.Controls.Add(descLabel);
            formPanel.Controls.Add(txtTimeLimit);
            formPanel.Controls.Add(txtTitle);
            formPanel.Location = new Point(827, 80);
            formPanel.Name = "formPanel";
            formPanel.Size = new Size(350, 480);
            formPanel.TabIndex = 2;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(107, 114, 128);
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(160, 340);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(140, 35);
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
            btnSave.Location = new Point(10, 340);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(140, 35);
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
            btnDelete.Location = new Point(210, 290);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(90, 35);
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
            btnEdit.Location = new Point(110, 290);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(90, 35);
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
            btnAdd.Location = new Point(10, 290);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(90, 35);
            btnAdd.TabIndex = 4;
            btnAdd.Text = "➕ Thêm";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += BtnAdd_Click;
            // 
            // txtDescription
            // 
            txtDescription.Location = new Point(10, 185);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(310, 80);
            txtDescription.TabIndex = 3;
            // 
            // descLabel
            // 
            descLabel.AutoSize = true;
            descLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            descLabel.ForeColor = Color.FromArgb(45, 55, 72);
            descLabel.Location = new Point(10, 160);
            descLabel.Name = "descLabel";
            descLabel.Size = new Size(62, 23);
            descLabel.TabIndex = 2;
            descLabel.Text = "Mô tả:";
            // 
            // txtTimeLimit
            // 
            txtTimeLimit.Location = new Point(10, 110);
            txtTimeLimit.Name = "txtTimeLimit";
            txtTimeLimit.PlaceholderText = "Thời gian (phút)";
            txtTimeLimit.Size = new Size(310, 27);
            txtTimeLimit.TabIndex = 1;
            // 
            // txtTitle
            // 
            txtTitle.Location = new Point(10, 30);
            txtTitle.Name = "txtTitle";
            txtTitle.PlaceholderText = "Tên bài kiểm tra";
            txtTitle.Size = new Size(310, 27);
            txtTitle.TabIndex = 0;
            // 
            // dataGridView
            // 
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.Location = new Point(20, 80);
            dataGridView.Name = "dataGridView";
            dataGridView.RowHeadersWidth = 51;
            dataGridView.Size = new Size(792, 480);
            dataGridView.TabIndex = 1;
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            titleLabel.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            titleLabel.ForeColor = Color.FromArgb(45, 55, 72);
            titleLabel.Location = new Point(20, 20);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(342, 46);
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
            Size = new Size(1200, 800);
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
