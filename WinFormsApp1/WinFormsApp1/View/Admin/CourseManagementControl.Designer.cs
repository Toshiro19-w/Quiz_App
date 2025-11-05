using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp1.View.Admin
{
    partial class CourseManagementControl
    {
        private System.ComponentModel.IContainer components = null;
        private DataGridView dataGridView;
        private TextBox txtTitle;
        private TextBox txtDescription;
        private TextBox txtPrice;
        private CheckBox chkPublished;
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
            chkPublished = new CheckBox();
            txtPrice = new TextBox();
            txtDescription = new TextBox();
            descLabel = new Label();
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
            formPanel.Controls.Add(chkPublished);
            formPanel.Controls.Add(txtPrice);
            formPanel.Controls.Add(txtDescription);
            formPanel.Controls.Add(descLabel);
            formPanel.Controls.Add(txtTitle);
            formPanel.Location = new Point(817, 80);
            formPanel.Name = "formPanel";
            formPanel.Size = new Size(380, 500);
            formPanel.TabIndex = 2;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(107, 114, 128);
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(175, 350);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(155, 35);
            btnCancel.TabIndex = 9;
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
            btnSave.Location = new Point(10, 350);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(155, 35);
            btnSave.TabIndex = 8;
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
            btnDelete.Location = new Point(230, 300);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(100, 35);
            btnDelete.TabIndex = 7;
            btnDelete.Text = "🗑️ Xóa";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += BtnDelete_Click;
            // 
            // btnEdit
            // 
            btnEdit.BackColor = Color.FromArgb(34, 197, 94);
            btnEdit.FlatStyle = FlatStyle.Flat;
            btnEdit.ForeColor = Color.White;
            btnEdit.Location = new Point(120, 300);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(100, 35);
            btnEdit.TabIndex = 6;
            btnEdit.Text = "✏️ Sửa";
            btnEdit.UseVisualStyleBackColor = false;
            btnEdit.Click += BtnEdit_Click;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.FromArgb(52, 144, 220);
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.ForeColor = Color.White;
            btnAdd.Location = new Point(10, 300);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(100, 35);
            btnAdd.TabIndex = 5;
            btnAdd.Text = "➕ Thêm";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += BtnAdd_Click;
            // 
            // chkPublished
            // 
            chkPublished.AutoSize = true;
            chkPublished.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            chkPublished.ForeColor = Color.FromArgb(45, 55, 72);
            chkPublished.Location = new Point(180, 245);
            chkPublished.Name = "chkPublished";
            chkPublished.Size = new Size(129, 27);
            chkPublished.TabIndex = 4;
            chkPublished.Text = "Đã xuất bản";
            // 
            // txtPrice
            // 
            txtPrice.Location = new Point(10, 200);
            txtPrice.Name = "txtPrice";
            txtPrice.PlaceholderText = "Giá";
            txtPrice.Size = new Size(160, 27);
            txtPrice.TabIndex = 3;
            // 
            // txtDescription
            // 
            txtDescription.Location = new Point(10, 105);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(340, 80);
            txtDescription.TabIndex = 2;
            // 
            // descLabel
            // 
            descLabel.AutoSize = true;
            descLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            descLabel.ForeColor = Color.FromArgb(45, 55, 72);
            descLabel.Location = new Point(10, 80);
            descLabel.Name = "descLabel";
            descLabel.Size = new Size(62, 23);
            descLabel.TabIndex = 1;
            descLabel.Text = "Mô tả:";
            // 
            // txtTitle
            // 
            txtTitle.Location = new Point(10, 30);
            txtTitle.Name = "txtTitle";
            txtTitle.PlaceholderText = "Tên khóa học";
            txtTitle.Size = new Size(340, 27);
            txtTitle.TabIndex = 0;
            // 
            // dataGridView
            // 
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.Location = new Point(20, 80);
            dataGridView.Name = "dataGridView";
            dataGridView.RowHeadersWidth = 51;
            dataGridView.Size = new Size(791, 500);
            dataGridView.TabIndex = 1;
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            titleLabel.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            titleLabel.ForeColor = Color.FromArgb(45, 55, 72);
            titleLabel.Location = new Point(20, 20);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(296, 46);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "Quản lý khóa học";
            // 
            // CourseManagementControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 249, 250);
            Controls.Add(mainContainer);
            Name = "CourseManagementControl";
            Size = new Size(1200, 800);
            Load += CourseManagementControl_Load;
            Resize += CourseManagementControl_Resize;
            mainContainer.ResumeLayout(false);
            mainContainer.PerformLayout();
            formPanel.ResumeLayout(false);
            formPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            ResumeLayout(false);
        }
    }
}
