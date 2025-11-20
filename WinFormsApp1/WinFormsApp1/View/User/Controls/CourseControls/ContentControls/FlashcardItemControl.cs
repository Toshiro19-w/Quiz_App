using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp1.View.User.Controls.CourseControls.ContentControls
{
    public partial class FlashcardItemControl : UserControl
    {
        public event Action<FlashcardItemControl>? DeleteRequested;

        public string FrontText => txtFront.Text;
        public string BackText => txtBack.Text;
        public string Hint => txtHint.Text;

        public FlashcardItemControl(string front = "", string back = "", string hint = "")
        {
            InitializeComponent();

            // set initial values
            txtFront.Text = front;
            txtBack.Text = back;
            txtHint.Text = hint;

            // wire delete
            btnDelete.Click += (s, e) => DeleteRequested?.Invoke(this);
        }

        // allow parent to set displayed index
        public void SetIndex(int index)
        {
            if (lblIndex != null)
            {
                lblIndex.Text = $"Thẻ #{index}";
            }
        }
    }
}
