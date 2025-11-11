namespace WinFormsApp1.View.User
{
    partial class formSearch
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
            panel1 = new Panel();
            searchButton = new Button();
            searchTB = new TextBox();
            cmbSearchType = new ComboBox();
            flowLayoutResults = new FlowLayoutPanel();
            lblResults = new Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.AutoScroll = true;
            panel1.Controls.Add(lblResults);
            panel1.Controls.Add(flowLayoutResults);
            panel1.Controls.Add(cmbSearchType);
            panel1.Controls.Add(searchButton);
            panel1.Controls.Add(searchTB);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1142, 626);
            panel1.TabIndex = 0;
            // 
            // searchButton
            // 
            searchButton.Font = new Font("Segoe UI", 12F);
            searchButton.ForeColor = Color.Black;
            searchButton.Location = new Point(950, 20);
            searchButton.Name = "searchButton";
            searchButton.Size = new Size(150, 40);
            searchButton.TabIndex = 6;
            searchButton.Text = "Tìm kiếm";
            searchButton.UseVisualStyleBackColor = true;
            searchButton.Click += searchButton_Click;
            // 
            // searchTB
            // 
            searchTB.Font = new Font("Segoe UI", 12F);
            searchTB.Location = new Point(200, 20);
            searchTB.Name = "searchTB";
            searchTB.PlaceholderText = "Nhập từ khóa tìm kiếm...";
            searchTB.Size = new Size(500, 34);
            searchTB.TabIndex = 5;
            // 
            // cmbSearchType
            // 
            cmbSearchType.Font = new Font("Segoe UI", 12F);
            cmbSearchType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbSearchType.Location = new Point(720, 20);
            cmbSearchType.Name = "cmbSearchType";
            cmbSearchType.Size = new Size(200, 34);
            cmbSearchType.TabIndex = 7;
            // 
            // lblResults
            // 
            lblResults.AutoSize = true;
            lblResults.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblResults.Location = new Point(20, 80);
            lblResults.Name = "lblResults";
            lblResults.Size = new Size(150, 32);
            lblResults.TabIndex = 8;
            lblResults.Text = "Kết quả tìm kiếm";
            // 
            // flowLayoutResults
            // 
            flowLayoutResults.Location = new Point(20, 120);
            flowLayoutResults.Name = "flowLayoutResults";
            flowLayoutResults.Size = new Size(1100, 480);
            flowLayoutResults.TabIndex = 9;
            flowLayoutResults.AutoScroll = true;
            // 
            // formSearch
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1142, 626);
            Controls.Add(panel1);
            Name = "formSearch";
            Text = "formSearch";
            Load += formSearch_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Button searchButton;
        private TextBox searchTB;
        private ComboBox cmbSearchType;
        private FlowLayoutPanel flowLayoutResults;
        private Label lblResults;
    }
}