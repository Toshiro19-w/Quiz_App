namespace WinFormsApp1.View.User
{
    partial class formLibrary
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            treeViewFolders = new TreeView();
            listViewItems = new ListView();
            btnNewFolder = new Button();
            btnAddItem = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            label1.Location = new Point(20, 20);
            label1.Name = "label1";
            label1.Size = new Size(150, 37);
            label1.TabIndex = 0;
            label1.Text = "Thư viện";
            // 
            // treeViewFolders
            // 
            treeViewFolders.Location = new Point(20, 100);
            treeViewFolders.Name = "treeViewFolders";
            treeViewFolders.Size = new Size(250, 450);
            treeViewFolders.TabIndex = 1;
            treeViewFolders.AfterSelect += treeViewFolders_AfterSelect;
            // 
            // listViewItems
            // 
            listViewItems.Location = new Point(290, 100);
            listViewItems.Name = "listViewItems";
            listViewItems.Size = new Size(850, 450);
            listViewItems.TabIndex = 2;
            listViewItems.View = System.Windows.Forms.View.Details;
            listViewItems.FullRowSelect = true;
            listViewItems.GridLines = true;
            listViewItems.DoubleClick += listViewItems_DoubleClick;
            // 
            // btnNewFolder
            // 
            btnNewFolder.Location = new Point(20, 60);
            btnNewFolder.Name = "btnNewFolder";
            btnNewFolder.Size = new Size(120, 30);
            btnNewFolder.TabIndex = 3;
            btnNewFolder.Text = "Tạo thư mục";
            btnNewFolder.UseVisualStyleBackColor = true;
            btnNewFolder.Click += btnNewFolder_Click;
            // 
            // btnAddItem
            // 
            btnAddItem.Location = new Point(150, 60);
            btnAddItem.Name = "btnAddItem";
            btnAddItem.Size = new Size(120, 30);
            btnAddItem.TabIndex = 4;
            btnAddItem.Text = "Thêm mục";
            btnAddItem.UseVisualStyleBackColor = true;
            btnAddItem.Click += btnAddItem_Click;
            // 
            // formLibrary
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1160, 580);
            Controls.Add(btnAddItem);
            Controls.Add(btnNewFolder);
            Controls.Add(listViewItems);
            Controls.Add(treeViewFolders);
            Controls.Add(label1);
            Name = "formLibrary";
            Text = "Library";
            Load += formLibrary_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TreeView treeViewFolders;
        private ListView listViewItems;
        private Button btnNewFolder;
        private Button btnAddItem;
    }
}