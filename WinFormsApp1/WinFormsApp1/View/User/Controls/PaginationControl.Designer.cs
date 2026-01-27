namespace WinFormsApp1.View.User.Controls
{
    partial class PaginationControl
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			btnPrevPage = new Button();
			btnNextPage = new Button();
			lblPageLabel = new Label();
			txtCurrentPage = new TextBox();
			lblTotalPages = new Label();
			panelCenter = new Panel();
			panelCenter.SuspendLayout();
			SuspendLayout();
			// 
			// btnPrevPage
			// 
			btnPrevPage.BackColor = Color.White;
			btnPrevPage.Cursor = Cursors.Hand;
			btnPrevPage.FlatAppearance.BorderColor = Color.Black;
			btnPrevPage.FlatStyle = FlatStyle.Flat;
			btnPrevPage.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
			btnPrevPage.ForeColor = Color.Black;
			btnPrevPage.Location = new Point(0, 0);
			btnPrevPage.Margin = new Padding(0);
			btnPrevPage.Name = "btnPrevPage";
			btnPrevPage.Size = new Size(50, 50);
			btnPrevPage.TabIndex = 0;
			btnPrevPage.Text = "←";
			btnPrevPage.UseVisualStyleBackColor = false;
			btnPrevPage.Click += BtnPrevPage_Click;
			// 
			// btnNextPage
			// 
			btnNextPage.BackColor = Color.White;
			btnNextPage.Cursor = Cursors.Hand;
			btnNextPage.FlatAppearance.BorderColor = Color.Black;
			btnNextPage.FlatStyle = FlatStyle.Flat;
			btnNextPage.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
			btnNextPage.ForeColor = Color.Black;
			btnNextPage.Location = new Point(610, 0);
			btnNextPage.Margin = new Padding(0);
			btnNextPage.Name = "btnNextPage";
			btnNextPage.Size = new Size(50, 50);
			btnNextPage.TabIndex = 1;
			btnNextPage.Text = "→";
			btnNextPage.UseVisualStyleBackColor = false;
			btnNextPage.Click += BtnNextPage_Click;
			// 
			// lblPageLabel
			// 
			lblPageLabel.AutoSize = true;
			lblPageLabel.BackColor = Color.White;
			lblPageLabel.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
			lblPageLabel.ForeColor = Color.Black;
			lblPageLabel.Location = new Point(213, 11);
			lblPageLabel.Margin = new Padding(0);
			lblPageLabel.Name = "lblPageLabel";
			lblPageLabel.Size = new Size(72, 30);
			lblPageLabel.TabIndex = 2;
			lblPageLabel.Text = "Trang";
			// 
			// txtCurrentPage
			// 
			txtCurrentPage.BackColor = Color.White;
			txtCurrentPage.BorderStyle = BorderStyle.FixedSingle;
			txtCurrentPage.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
			txtCurrentPage.ForeColor = Color.Black;
			txtCurrentPage.Location = new Point(308, 7);
			txtCurrentPage.Margin = new Padding(0);
			txtCurrentPage.Name = "txtCurrentPage";
			txtCurrentPage.Size = new Size(80, 39);
			txtCurrentPage.TabIndex = 3;
			txtCurrentPage.Text = "1";
			txtCurrentPage.TextAlign = HorizontalAlignment.Center;
			txtCurrentPage.KeyPress += TxtCurrentPage_KeyPress;
			txtCurrentPage.Leave += TxtCurrentPage_Leave;
			// 
			// lblTotalPages
			// 
			lblTotalPages.AutoSize = true;
			lblTotalPages.BackColor = Color.White;
			lblTotalPages.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
			lblTotalPages.ForeColor = Color.Black;
			lblTotalPages.Location = new Point(408, 11);
			lblTotalPages.Margin = new Padding(0);
			lblTotalPages.Name = "lblTotalPages";
			lblTotalPages.Size = new Size(42, 30);
			lblTotalPages.TabIndex = 4;
			lblTotalPages.Text = "/ 2";
			// 
			// panelCenter
			// 
			panelCenter.BackColor = Color.White;
			panelCenter.Controls.Add(lblPageLabel);
			panelCenter.Controls.Add(lblTotalPages);
			panelCenter.Controls.Add(txtCurrentPage);
			panelCenter.Controls.Add(btnPrevPage);
			panelCenter.Controls.Add(btnNextPage);
			panelCenter.Location = new Point(182, 0);
			panelCenter.Margin = new Padding(0);
			panelCenter.Name = "panelCenter";
			panelCenter.Size = new Size(660, 50);
			panelCenter.TabIndex = 5;
			// 
			// PaginationControl
			// 
			AutoScaleDimensions = new SizeF(10F, 25F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.White;
			Controls.Add(panelCenter);
			Margin = new Padding(0);
			Name = "PaginationControl";
			Size = new Size(1025, 50);
			Resize += PaginationControl_Resize;
			panelCenter.ResumeLayout(false);
			panelCenter.PerformLayout();
			ResumeLayout(false);
		}

		#endregion

		private Button btnPrevPage;
        private Button btnNextPage;
        private Label lblPageLabel;
        private TextBox txtCurrentPage;
        private Label lblTotalPages;
        private Panel panelCenter;
    }
}
