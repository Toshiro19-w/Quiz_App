using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WinFormsApp1.Helpers;
using WinFormsApp1.Models.EF;
using Microsoft.EntityFrameworkCore;

namespace WinFormsApp1.View.User
{
    public partial class formLibrary : Form
    {
        private int? selectedFolderId;

        public formLibrary()
        {
            InitializeComponent();
            SetupListView();
        }

        private void SetupListView()
        {
            listViewItems.Columns.Add("Tên", 300);
            listViewItems.Columns.Add("Loại", 150);
            listViewItems.Columns.Add("Ngày thêm", 150);
            listViewItems.Columns.Add("Ghi chú", 300);
        }

        private async void formLibrary_Load(object sender, EventArgs e)
        {
            await LoadLibraryTree();
        }

        private async System.Threading.Tasks.Task LoadLibraryTree()
        {
            using var context = new LearningPlatformContext();
            var userId = AuthHelper.CurrentUser?.UserId;

            if (!userId.HasValue) return;

            var library = await context.Libraries
                .Include(l => l.Folders)
                .FirstOrDefaultAsync(l => l.OwnerId == userId.Value);

            if (library == null)
            {
                library = new Models.Entities.Library
                {
                    OwnerId = userId.Value,
                    Name = "Thư viện của tôi",
                    CreatedAt = DateTime.Now
                };
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
                LoadSubFolders(context, folderNode, folder.FolderId);
            }

            rootNode.Expand();
        }

        private void LoadSubFolders(LearningPlatformContext context, TreeNode parentNode, int parentFolderId)
        {
            var subFolders = context.Folders.Where(f => f.ParentFolderId == parentFolderId).ToList();
            foreach (var folder in subFolders)
            {
                var folderNode = new TreeNode(folder.Name) { Tag = folder.FolderId };
                parentNode.Nodes.Add(folderNode);
                LoadSubFolders(context, folderNode, folder.FolderId);
            }
        }

        private async void treeViewFolders_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag is int folderId)
            {
                selectedFolderId = folderId;
                await LoadFolderItems(folderId);
            }
        }

        private async System.Threading.Tasks.Task LoadFolderItems(int folderId)
        {
            using var context = new LearningPlatformContext();
            var items = await context.SavedItems
                .Where(si => si.FolderId == folderId)
                .ToListAsync();

            listViewItems.Items.Clear();
            foreach (var item in items)
            {
                var lvi = new ListViewItem(GetItemName(context, item));
                lvi.SubItems.Add(item.ContentType);
                lvi.SubItems.Add(item.AddedAt.ToString("dd/MM/yyyy"));
                lvi.SubItems.Add(item.Note ?? "");
                lvi.Tag = item.SavedItemId;
                listViewItems.Items.Add(lvi);
            }
        }

        private string GetItemName(LearningPlatformContext context, Models.Entities.SavedItem item)
        {
            switch (item.ContentType)
            {
                case "Course":
                    return context.Courses.Find(item.ContentId)?.Title ?? "Unknown";
                case "Test":
                    return context.Tests.Find(item.ContentId)?.Title ?? "Unknown";
                case "FlashcardSet":
                    return context.FlashcardSets.Find(item.ContentId)?.Title ?? "Unknown";
                default:
                    return "Unknown";
            }
        }

        private async void btnNewFolder_Click(object sender, EventArgs e)
        {
            var folderName = Microsoft.VisualBasic.Interaction.InputBox("Nhập tên thư mục:", "Tạo thư mục mới");
            if (string.IsNullOrEmpty(folderName)) return;

            using var context = new LearningPlatformContext();
            var userId = AuthHelper.CurrentUser?.UserId;
            if (!userId.HasValue) return;

            var library = await context.Libraries.FirstOrDefaultAsync(l => l.OwnerId == userId.Value);
            if (library == null) return;

            var folder = new Models.Entities.Folder
            {
                LibraryId = library.LibraryId,
                Name = folderName,
                ParentFolderId = selectedFolderId,
                CreatedAt = DateTime.Now
            };

            context.Folders.Add(folder);
            await context.SaveChangesAsync();
            await LoadLibraryTree();
            MessageBox.Show("Tạo thư mục thành công!");
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng thêm mục đang phát triển");
        }

        private void listViewItems_DoubleClick(object sender, EventArgs e)
        {
            if (listViewItems.SelectedItems.Count > 0)
            {
                var itemId = (int)listViewItems.SelectedItems[0].Tag;
                MessageBox.Show($"Mở mục ID: {itemId}");
            }
        }

        private void label1_Click(object sender, EventArgs e) { }
    }
}
