using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp1.View.User.Controls.CourseControls.ContentControls
{
    partial class TestQuestionItemControl
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

        #region Component Designer generated code
        private void InitializeComponent()
        {
            lblQuestionNumber = new Label();
            lblQuestionType = new Label();
            cboQuestionType = new ComboBox();
            lblQuestion = new Label();
            txtQuestion = new TextBox();
            lblPoint = new Label();
            numPoint = new NumericUpDown();
            lblAnswers = new Label();
            pnlAnswers = new Panel();
            btnAddAnswer = new Button();
            btnDelete = new Button();
            ((System.ComponentModel.ISupportInitialize)numPoint).BeginInit();
            SuspendLayout();
            // 
            // lblQuestionNumber
            // 
            lblQuestionNumber.AutoSize = true;
            lblQuestionNumber.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblQuestionNumber.ForeColor = Color.FromArgb(33, 150, 243);
            lblQuestionNumber.Location = new Point(8, 8);
            lblQuestionNumber.Name = "lblQuestionNumber";
            lblQuestionNumber.Size = new Size(100, 28);
            lblQuestionNumber.TabIndex = 0;
            lblQuestionNumber.Text = "Câu hỏi 1";
            // 
            // lblQuestionType
            // 
            lblQuestionType.AutoSize = true;
            lblQuestionType.Location = new Point(8, 44);
            lblQuestionType.Name = "lblQuestionType";
            lblQuestionType.Size = new Size(110, 25);
            lblQuestionType.TabIndex = 1;
            lblQuestionType.Text = "Loại câu hỏi:";
            // 
            // cboQuestionType
            // 
            cboQuestionType.DropDownStyle = ComboBoxStyle.DropDownList;
            cboQuestionType.FormattingEnabled = true;
            cboQuestionType.Items.AddRange(new object[] { "Trắc nghiệm (1 đáp án)", "Trắc nghiệm (nhiều đáp án)", "Đúng/Sai" });
            cboQuestionType.Location = new Point(124, 40);
            cboQuestionType.Name = "cboQuestionType";
            cboQuestionType.Size = new Size(280, 33);
            cboQuestionType.TabIndex = 2;
            // 
            // lblQuestion
            // 
            lblQuestion.AutoSize = true;
            lblQuestion.Location = new Point(8, 84);
            lblQuestion.Name = "lblQuestion";
            lblQuestion.Size = new Size(75, 25);
            lblQuestion.TabIndex = 3;
            lblQuestion.Text = "Câu hỏi:";
            // 
            // txtQuestion
            // 
            txtQuestion.Location = new Point(8, 112);
            txtQuestion.Multiline = true;
            txtQuestion.Name = "txtQuestion";
            txtQuestion.ScrollBars = ScrollBars.Vertical;
            txtQuestion.Size = new Size(600, 80);
            txtQuestion.TabIndex = 4;
            // 
            // lblPoint
            // 
            lblPoint.AutoSize = true;
            lblPoint.Location = new Point(8, 204);
            lblPoint.Name = "lblPoint";
            lblPoint.Size = new Size(60, 25);
            lblPoint.TabIndex = 5;
            lblPoint.Text = "Điểm:";
            // 
            // numPoint
            // 
            numPoint.DecimalPlaces = 1;
            numPoint.Location = new Point(74, 202);
            numPoint.Maximum = 100M;
            numPoint.Minimum = 0.5M;
            numPoint.Name = "numPoint";
            numPoint.Size = new Size(100, 31);
            numPoint.TabIndex = 6;
            numPoint.Value = 1M;
            // 
            // lblAnswers
            // 
            lblAnswers.AutoSize = true;
            lblAnswers.Location = new Point(8, 244);
            lblAnswers.Name = "lblAnswers";
            lblAnswers.Size = new Size(280, 25);
            lblAnswers.TabIndex = 7;
            lblAnswers.Text = "Các đáp án: (Chọn checkbox cho đáp án đúng)";
            // 
            // pnlAnswers
            // 
            pnlAnswers.AutoScroll = true;
            pnlAnswers.BorderStyle = BorderStyle.FixedSingle;
            pnlAnswers.Location = new Point(8, 272);
            pnlAnswers.Name = "pnlAnswers";
            pnlAnswers.Size = new Size(600, 150);
            pnlAnswers.TabIndex = 8;
            // 
            // btnAddAnswer
            // 
            btnAddAnswer.BackColor = Color.FromArgb(76, 175, 80);
            btnAddAnswer.FlatAppearance.BorderSize = 0;
            btnAddAnswer.FlatStyle = FlatStyle.Flat;
            btnAddAnswer.ForeColor = Color.White;
            btnAddAnswer.Location = new Point(8, 432);
            btnAddAnswer.Name = "btnAddAnswer";
            btnAddAnswer.Size = new Size(150, 36);
            btnAddAnswer.TabIndex = 9;
            btnAddAnswer.Text = "+ Thêm đáp án";
            btnAddAnswer.UseVisualStyleBackColor = false;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.FromArgb(244, 67, 54);
            btnDelete.FlatAppearance.BorderSize = 0;
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.ForeColor = Color.White;
            btnDelete.Location = new Point(528, 8);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(80, 30);
            btnDelete.TabIndex = 10;
            btnDelete.Text = "Xóa";
            btnDelete.UseVisualStyleBackColor = false;
            // 
            // TestQuestionItemControl
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.FromArgb(250, 250, 250);
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(btnDelete);
            Controls.Add(btnAddAnswer);
            Controls.Add(pnlAnswers);
            Controls.Add(lblAnswers);
            Controls.Add(numPoint);
            Controls.Add(lblPoint);
            Controls.Add(txtQuestion);
            Controls.Add(lblQuestion);
            Controls.Add(cboQuestionType);
            Controls.Add(lblQuestionType);
            Controls.Add(lblQuestionNumber);
            Name = "TestQuestionItemControl";
            Size = new Size(620, 480);
            ((System.ComponentModel.ISupportInitialize)numPoint).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
        #endregion

        public Label lblQuestionNumber;
        private Label lblQuestionType;
        private ComboBox cboQuestionType;
        private Label lblQuestion;
        public TextBox txtQuestion;
        private Label lblPoint;
        public NumericUpDown numPoint;
        private Label lblAnswers;
        private Panel pnlAnswers;
        private Button btnAddAnswer;
        private Button btnDelete;
    }
}
