namespace WinFormsApp1.View.User.Controls
{
    partial class ShopControl
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) { components.Dispose(); } base.Dispose(disposing); }
        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // ShopControl
            // 
            Name = "ShopControl";
            Size = new Size(1200, 700);
            Load += ShopControl_Load;
            ResumeLayout(false);
        }
    }
}
