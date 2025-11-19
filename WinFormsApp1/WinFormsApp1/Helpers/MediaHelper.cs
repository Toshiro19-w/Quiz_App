using System;
using System.IO;
using System.Windows.Forms;

namespace WinFormsApp1.Helpers
{
	public static class MediaHelper
	{
		private const long MAX_VIDEO_SIZE = 100 * 1024 * 1024; // 100MB

		// ============================================================
		// GET PROJECT ROOT (3 cấp lên từ bin/Debug/net8.0-windows)
		// ============================================================
		private static string GetProjectRoot()
		{
			string dir = AppDomain.CurrentDomain.BaseDirectory;
			string projectRoot = Path.GetFullPath(Path.Combine(dir, @"..\..\.."));
			return projectRoot;
		}

		// ============================================================
		// ENSURE LIBRARY STRUCTURE EXISTS
		// ============================================================
		private static void EnsureLibraryStructure()
		{
			string root = GetProjectRoot();

			string library = Path.Combine(root, "Library");
			string image = Path.Combine(library, "Image");
			string video = Path.Combine(library, "Video");

			// Tự động tạo các thư mục nếu thiếu
			if (!Directory.Exists(library))
				Directory.CreateDirectory(library);

			if (!Directory.Exists(image))
				Directory.CreateDirectory(image);

			if (!Directory.Exists(video))
				Directory.CreateDirectory(video);
		}

		// ============================================================
		// COPY VIDEO
		// ============================================================
		public static string? CopyVideoToLibrary(string sourcePath)
		{
			try
			{
				var fileInfo = new FileInfo(sourcePath);

				// Kiểm tra kích thước
				if (fileInfo.Length > MAX_VIDEO_SIZE)
				{
					MessageBox.Show(
						$"Video quá lớn! Tối đa 100MB, file: {fileInfo.Length / (1024 * 1024)}MB",
						"Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);

					return null;
				}

				// Đảm bảo đủ thư mục
				EnsureLibraryStructure();

				string root = GetProjectRoot();
				string videoDir = Path.Combine(root, "Library", "Video");

				// Tạo file name
				string fileName = $"{Guid.NewGuid()}{fileInfo.Extension}";
				string destPath = Path.Combine(videoDir, fileName);

				// Copy
				File.Copy(sourcePath, destPath, true);

				// Trả về path lưu DB (relative)
				return Path.Combine("Library", "Video", fileName);
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Lỗi copy video: {ex.Message}", "Lỗi",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				return null;
			}
		}

		// ============================================================
		// COPY IMAGE
		// ============================================================
		public static string? CopyImageToLibrary(string sourcePath)
		{
			try
			{
				var fileInfo = new FileInfo(sourcePath);

				// Đảm bảo đủ thư mục
				EnsureLibraryStructure();

				string root = GetProjectRoot();
				string imageDir = Path.Combine(root, "Library", "Image");

				// Tạo file name
				string fileName = $"{Guid.NewGuid()}{fileInfo.Extension}";
				string destPath = Path.Combine(imageDir, fileName);

				// Copy
				File.Copy(sourcePath, destPath, true);

				// Trả về path lưu DB
				return Path.Combine("Library", "Image", fileName);
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Lỗi copy hình ảnh: {ex.Message}", "Lỗi",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				return null;
			}
		}
	}
}
