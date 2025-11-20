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
        private ComboBox cmbCategory;
        private CheckBox chkPublished;
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
            chkPublished = new CheckBox();
            cmbCategory = new ComboBox();
            txtPrice = new TextBox();
            txtDescription = new TextBox();

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
            formPanel.Controls.Add(cmbCategory);
            formPanel.Controls.Add(txtPrice);
            formPanel.Controls.Add(txtDescription);

            formPanel.Controls.Add(txtTitle);
            // make formPanel anchored to top-right and limit max width
            formPanel.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom;
            formPanel.Location = new Point(1200, 60);
            formPanel.Name = "formPanel";
            formPanel.Size = new Size(350, 880);
            formPanel.TabIndex = 2;
            formPanel.MaximumSize = new Size(400, 2000);
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(156, 163, 175);
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(180, 245);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(155, 40);
            btnCancel.TabIndex = 9;
            btnCancel.Text = "❌ Hủy";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Visible = false;
            //btnCancel.Click += BtnCancel_Click;
            // 
            // btnSave
            // 
            btnSave.BackColor = Color.FromArgb(34, 197, 94);
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(15, 245);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(155, 40);
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
            btnDelete.Location = new Point(180, 295);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(155, 40);
            btnDelete.TabIndex = 7;
            btnDelete.Text = "🗑️ Xóa";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += BtnDelete_Click;
            // 
            // btnEdit
            // 
            btnEdit.BackColor = Color.FromArgb(59, 130, 246);
            btnEdit.FlatStyle = FlatStyle.Flat;
            btnEdit.ForeColor = Color.White;
            btnEdit.Location = new Point(15, 295);
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(155, 40);
            btnEdit.TabIndex = 6;
            btnEdit.Text = "✏️ Sửa";
            btnEdit.UseVisualStyleBackColor = false;
            btnEdit.Click += BtnEdit_Click;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.FromArgb(34, 197, 94);
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.ForeColor = Color.White;
            btnAdd.Location = new Point(15, 245);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(320, 40);
            btnAdd.TabIndex = 5;
            btnAdd.Text = "➕ Thêm khóa học";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += BtnAdd_Click;
            // 
            // chkPublished
            // 
            chkPublished.AutoSize = true;
            chkPublished.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            chkPublished.ForeColor = Color.FromArgb(45, 55, 72);
            chkPublished.Location = new Point(15, 205);
            chkPublished.Name = "chkPublished";
            chkPublished.Size = new Size(130, 27);
            chkPublished.TabIndex = 5;
            chkPublished.Text = "Đã xuất bản";
            // 
            // txtPrice
            // 
            txtPrice.Location = new Point(15, 155);
            txtPrice.Name = "txtPrice";
            txtPrice.Size = new Size(155, 35);
            txtPrice.TabIndex = 3;
            // 
            // cmbCategory
            // 
            cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategory.Location = new Point(180, 155);
            cmbCategory.Name = "cmbCategory";
            cmbCategory.Size = new Size(155, 35);
            cmbCategory.TabIndex = 4;
            // 
            // txtDescription
            // 
            txtDescription.Location = new Point(15, 80);
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.Size = new Size(320, 60);
            txtDescription.TabIndex = 2;
            // 
            // descLabel
            // 
            // 
            // txtTitle
            // 
            txtTitle.Location = new Point(15, 30);
            txtTitle.Name = "txtTitle";
            txtTitle.Size = new Size(320, 35);
            txtTitle.TabIndex = 0;
            // 
            // dataGridView
            // 
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.Location = new Point(20, 60);
            dataGridView.Name = "dataGridView";
            dataGridView.RowHeadersWidth = 51;
            dataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            dataGridView.Size = new Size(1160, 880);
            dataGridView.TabIndex = 1;
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            titleLabel.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            titleLabel.ForeColor = Color.FromArgb(45, 55, 72);
            titleLabel.Location = new Point(20, 10);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(300, 45);
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
