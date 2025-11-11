using System.Windows.Forms;

namespace WinFormsApp1.Helpers
{
    public static class PermissionHelper
    {
        public static void CheckAdminAccess(Form form)
        {
            if (!AuthHelper.IsAdmin())
            {
                MessageBox.Show("Bạn không có quyền truy cập chức năng này!", "Từ chối truy cập", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                form.Close();
            }
        }

        public static bool CanManageUsers()
        {
            return AuthHelper.IsAdmin();
        }

        public static bool CanManageCourses()
        {
            return AuthHelper.IsAdmin();
        }

        public static bool CanManageTests()
        {
            return AuthHelper.IsAdmin();
        }

        public static bool CanViewReports()
        {
            return AuthHelper.IsAdmin();
        }

        public static void DisableControlIfNoPermission(Control control, bool hasPermission)
        {
            control.Enabled = hasPermission;
            if (!hasPermission && control is Button btn)
            {
                btn.BackColor = System.Drawing.Color.Gray;
                btn.Cursor = Cursors.No;
            }
        }
    }
}
