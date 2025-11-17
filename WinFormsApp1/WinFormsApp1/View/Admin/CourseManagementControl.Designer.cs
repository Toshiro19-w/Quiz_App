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
            // make formPanel anchored to top-right and limit max width
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
            btnCancel.Location = new Point(340, 650);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(280, 45);
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
            btnSave.Location = new Point(20, 650);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(280, 45);
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
            btnDelete.Location = new Point(460, 580);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(150, 45);
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
            btnEdit.Location = new Point(300, 580);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(150, 45);
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
            btnAdd.Location = new Point(20, 580);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(200, 45);
            btnAdd.TabIndex = 5;
            btnAdd.Text = "➕ Thêm";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += BtnAdd_Click;
            // 
            // chkPublished
            // 
            chkPublished.AutoSize = true;
            chkPublished.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            chkPublished.ForeColor = Color.FromArgb(45, 55, 72);
            chkPublished.Location = new Point(380, 500);
            chkPublished.Name = "chkPublished";
            chkPublished.Size = new Size(160, 31);
            chkPublished.TabIndex = 4;
            chkPublished.Text = "Đã xuất bản";
            // 
            // txtPrice
            // 
            txtPrice.Location = new Point(20, 460);
            txtPrice.Name = "txtPrice";
            txtPrice.PlaceholderText = "Giá";
            txtPrice.Size = new Size(300, 35);
            txtPrice.TabIndex = 3;
            // 
            // txtDescription
            // 
            txtDescription.Location = new Point(20, 160);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(580, 280);
            txtDescription.TabIndex = 2;
            // 
            // descLabel
            // 
            descLabel.AutoSize = true;
            descLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            descLabel.ForeColor = Color.FromArgb(45, 55, 72);
            descLabel.Location = new Point(16, 120);
            descLabel.Name = "descLabel";
            descLabel.Size = new Size(78, 28);
            descLabel.TabIndex = 1;
            descLabel.Text = "Mô tả:";
            // 
            // txtTitle
            // 
            txtTitle.Location = new Point(20, 30);
            txtTitle.Name = "txtTitle";
            txtTitle.PlaceholderText = "Tên khóa học";
            txtTitle.Size = new Size(580, 35);
            txtTitle.TabIndex = 0;
            // 
            // dataGridView
            // 
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.Location = new Point(20, 80);
            dataGridView.Name = "dataGridView";
            dataGridView.RowHeadersWidth = 51;
            // make datagrid anchored and allow it to resize when formPanel width changes
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
            titleLabel.Size = new Size(360, 62);
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
            Size = new Size(1898, 1024);
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
