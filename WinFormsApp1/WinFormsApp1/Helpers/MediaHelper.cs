using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp1.Services;

namespace WinFormsApp1.Helpers
{
	public static class MediaHelper
	{
		private const long MAX_VIDEO_SIZE = 100 * 1024 * 1024; // 100MB
		private const long MAX_PDF_SIZE = 50 * 1024 * 1024; // 50MB

		private static AzureBlobStorageService? _blobService;
		private static bool _useAzureStorage = true; // Default: sử dụng Azure

		// ============================================================
		// INITIALIZE AZURE STORAGE
		// ============================================================
		public static void InitializeAzureStorage(IConfiguration configuration)
		{
			try
			{
				var connectionString = configuration.GetConnectionString("AzureBlobConnectionString");
				if (!string.IsNullOrEmpty(connectionString))
				{
					_blobService = new AzureBlobStorageService(connectionString);
					_useAzureStorage = true;
				}
				else
				{
					_useAzureStorage = false;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Không thể kết nối Azure Storage: {ex.Message}\nSử dụng local storage.", 
					"Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				_useAzureStorage = false;
			}
		}

		// ============================================================
		// CHECK IF PATH IS AZURE URL
		// ============================================================
		public static bool IsAzureUrl(string pathOrUrl)
		{
			return !string.IsNullOrEmpty(pathOrUrl) &&
				   pathOrUrl.StartsWith("https://", StringComparison.OrdinalIgnoreCase) &&
				   pathOrUrl.Contains("blob.core.windows.net", StringComparison.OrdinalIgnoreCase);
		}

		// ============================================================
		// GET PROJECT ROOT (3 cấp lên từ bin/Debug/net8.0-windows)
		// ============================================================
		public static string GetProjectRoot()
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
			string pdf = Path.Combine(library, "Pdf");

			// Tự động tạo các thư mục nếu thiếu
			if (!Directory.Exists(library))
				Directory.CreateDirectory(library);

			if (!Directory.Exists(image))
				Directory.CreateDirectory(image);

			if (!Directory.Exists(video))
				Directory.CreateDirectory(video);

		if (!Directory.Exists(pdf))
			Directory.CreateDirectory(pdf);
	}

	// ============================================================
	// COPY VIDEO - ASYNC với Azure support
	// ============================================================
	public static async Task<string?> CopyVideoToLibraryAsync(string sourcePath, IProgress<int> progress = null)
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

			// Nếu sử dụng Azure Storage
			if (_useAzureStorage && _blobService != null)
			{
				string azureUrl = await _blobService.UploadVideoAsync(sourcePath, progress);
				return azureUrl;
			}
			else
			{
				// Fallback: Local storage (không có progress)
				return CopyVideoToLibrary(sourcePath);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show($"Lỗi upload video: {ex.Message}", "Lỗi",
				MessageBoxButtons.OK, MessageBoxIcon.Error);
			return null;
		}
	}

	// ============================================================
	// COPY VIDEO - SYNC (backward compatibility)
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

	// ============================================================
	// COPY PDF - ASYNC với Azure support
	// ============================================================
	public static async Task<string?> CopyPdfToLibraryAsync(string sourcePath, IProgress<int> progress = null)
	{
		try
		{
			var fileInfo = new FileInfo(sourcePath);

			// Kiểm tra kích thước
			if (fileInfo.Length > MAX_PDF_SIZE)
			{
				MessageBox.Show(
					$"File PDF quá lớn! Tối đa 50MB, file: {fileInfo.Length / (1024 * 1024)}MB",
					"Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);

				return null;
			}

			// Kiểm tra extension
			if (!fileInfo.Extension.Equals(".pdf", StringComparison.OrdinalIgnoreCase))
			{
				MessageBox.Show("Chỉ chấp nhận file PDF!", "Lỗi",
					MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return null;
			}

			// Nếu sử dụng Azure Storage
			if (_useAzureStorage && _blobService != null)
			{
				string azureUrl = await _blobService.UploadDocumentAsync(sourcePath, progress);
				return azureUrl;
			}
			else
			{
				// Fallback: Local storage
				return CopyPdfToLibrary(sourcePath);
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show($"Lỗi upload PDF: {ex.Message}", "Lỗi",
				MessageBoxButtons.OK, MessageBoxIcon.Error);
			return null;
		}
	}

	// ============================================================
	// COPY PDF - SYNC (backward compatibility)
	// ============================================================
	public static string? CopyPdfToLibrary(string sourcePath)
	{
		try
		{
			var fileInfo = new FileInfo(sourcePath);

			// Kiểm tra kích thước
			if (fileInfo.Length > MAX_PDF_SIZE)
			{
				MessageBox.Show(
					$"File PDF quá lớn! Tối đa 50MB, file: {fileInfo.Length / (1024 * 1024)}MB",
					"Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);

				return null;
			}

			// Kiểm tra extension
			if (!fileInfo.Extension.Equals(".pdf", StringComparison.OrdinalIgnoreCase))
			{
				MessageBox.Show("Chỉ chấp nhận file PDF!", "Lỗi",
					MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return null;
			}

				// Đảm bảo đủ thư mục
				EnsureLibraryStructure();

				string root = GetProjectRoot();
				string pdfDir = Path.Combine(root, "Library", "Pdf");

				// Tạo file name
				string fileName = $"{Guid.NewGuid()}.pdf";
				string destPath = Path.Combine(pdfDir, fileName);

				// Copy
				File.Copy(sourcePath, destPath, true);

				// Trả về path lưu DB (relative)
				return Path.Combine("Library", "Pdf", fileName);
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Lỗi copy PDF: {ex.Message}", "Lỗi",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				return null;
			}
		}
	}
}
