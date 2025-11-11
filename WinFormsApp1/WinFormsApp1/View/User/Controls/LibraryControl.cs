using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinFormsApp1.Helpers;
using WinFormsApp1.Models.EF;
using Microsoft.EntityFrameworkCore;

namespace WinFormsApp1.View.User.Controls
{
    public partial class LibraryControl : UserControl
    {
        private TreeView treeViewFolders;
        private ListView listViewItems;
        private int? selectedFolderId;

        public LibraryControl()
        {
            InitializeComponent();
            SetupUI();
            LoadLibraryTree();
        }

        private void SetupUI()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = ColorPalette.Background;
            this.Padding = new Padding(0, 70, 0, 0);

            var lblTitle = new Label { Text = "📚 Thư viện của tôi", Font = new Font("Segoe UI", 18, FontStyle.Bold), Location = new Point(20, 10), AutoSize = true };

            var btnNewFolder = new Button { Text = "+ Thư mục mới", Location = new Point(20, 50), Size = new Size(150, 35), BackColor = ColorPalette.ButtonSecondary, ForeColor = Color.White, FlatStyle = FlatStyle.Flat, Cursor = Cursors.Hand };
            btnNewFolder.FlatAppearance.BorderSize = 0;
            btnNewFolder.Click += BtnNewFolder_Click;

            treeViewFolders = new TreeView { Location = new Point(20, 100), Size = new Size(300, 500), Font = new Font("Segoe UI", 10) };
            listViewItems = new ListView { Location = new Point(340, 100), Size = new Size(800, 500), View = System.Windows.Forms.View.Details, Font = new Font("Segoe UI", 10), FullRowSelect = true };
            listViewItems.Columns.Add("Tên", 350);
            listViewItems.Columns.Add("Loại", 120);
            listViewItems.Columns.Add("Ngày thêm", 150);
            listViewItems.Columns.Add("Ghi chú", 180);
            treeViewFolders.AfterSelect += TreeViewFolders_AfterSelect;
            listViewItems.DoubleClick += ListViewItems_DoubleClick;

            this.Controls.AddRange(new Control[] { lblTitle, btnNewFolder, treeViewFolders, listViewItems });
        }

        private async void BtnNewFolder_Click(object sender, EventArgs e)
        {
            var folderName = Microsoft.VisualBasic.Interaction.InputBox("Nhập tên thư mục:", "Tạo thư mục mới");
            if (string.IsNullOrEmpty(folderName)) return;

            using var context = new LearningPlatformContext();
            var userId = AuthHelper.CurrentUser?.UserId;
            if (!userId.HasValue) return;

            var library = await context.Libraries.FirstOrDefaultAsync(l => l.OwnerId == userId.Value);
            if (library == null) return;

            var folder = new Models.Entities.Folder { LibraryId = library.LibraryId, Name = folderName, ParentFolderId = selectedFolderId, CreatedAt = DateTime.Now };
            context.Folders.Add(folder);
            await context.SaveChangesAsync();
            LoadLibraryTree();
            MessageBox.Show("Tạo thư mục thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ListViewItems_DoubleClick(object sender, EventArgs e)
        {
            if (listViewItems.SelectedItems.Count > 0)
            {
                var itemName = listViewItems.SelectedItems[0].Text;
                MessageBox.Show($"Mở mục: {itemName}");
            }
        }

        private async void LoadLibraryTree()
        {
            using var context = new LearningPlatformContext();
            var userId = AuthHelper.CurrentUser?.UserId;
            if (!userId.HasValue) return;

            var library = await context.Libraries.Include(l => l.Folders).FirstOrDefaultAsync(l => l.OwnerId == userId.Value);
            if (library == null)
            {
                library = new Models.Entities.Library { OwnerId = userId.Value, Name = "Thư viện của tôi", CreatedAt = DateTime.Now };
                context.Libraries.Add(library);
                await context.SaveChangesAsync();
            }

            treeViewFolders.Nodes.Clear();
            var rootNode = new TreeNode(library.Name) { Tag = library.LibraryId };
            treeViewFolders.Nodes.Add(rootNode);
            foreach (var folder in library.Folders.Where(f => f.ParentFolderId == null))
            {
                var folderNode = new TreeNode(folder.Name) { Tag = folder.FolderId };
                rootNode.Nodes.Add(folderNode);
            }
            rootNode.Expand();
        }

        private async void TreeViewFolders_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag is int folderId)
            {
                selectedFolderId = folderId;
                using var context = new LearningPlatformContext();
                var items = await context.SavedItems.Where(si => si.FolderId == folderId).ToListAsync();
                listViewItems.Items.Clear();
                foreach (var item in items)
                {
                    var lvi = new ListViewItem(item.ContentType);
                    lvi.SubItems.Add(item.ContentType);
                    lvi.SubItems.Add(item.AddedAt.ToString("dd/MM/yyyy"));
                    lvi.SubItems.Add(item.Note ?? "");
                    listViewItems.Items.Add(lvi);
                }
            }
        }
    }
}
