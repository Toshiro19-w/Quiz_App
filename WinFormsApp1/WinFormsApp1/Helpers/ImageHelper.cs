using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Diagnostics;
using System.Reflection;

namespace WinFormsApp1.Helpers
{
    public static class ImageHelper
    {
        private const string IMAGE_DIRECTORY = "Library/Image";
        
        /// <summary>
        /// Get the project root directory (where .csproj is located)
        /// </summary>
        private static string GetProjectRootDirectory()
        {
            // Get the directory where the assembly is located
            var assemblyLocation = Assembly.GetExecutingAssembly().Location;
            var assemblyDirectory = Path.GetDirectoryName(assemblyLocation);
            
            Debug.WriteLine($"[ImageHelper] Assembly Location: {assemblyLocation}");
            Debug.WriteLine($"[ImageHelper] Assembly Directory: {assemblyDirectory}");
            
            // Navigate up from bin/Debug/net8.0 to project root
            var projectRoot = assemblyDirectory;
            
            // Keep going up until we find the .csproj file
            while (projectRoot != null && !string.IsNullOrEmpty(projectRoot))
            {
                var csprojFiles = Directory.GetFiles(projectRoot, "*.csproj");
                if (csprojFiles.Length > 0)
                {
                    Debug.WriteLine($"[ImageHelper] Found .csproj in: {projectRoot}");
                    return projectRoot;
                }
                
                var parentDir = Directory.GetParent(projectRoot);
                if (parentDir == null)
                {
                    Debug.WriteLine("[ImageHelper] Reached root without finding .csproj");
                    break;
                }
                
                projectRoot = parentDir.FullName;
            }
            
            // Fallback: Go up 3 levels from bin/Debug/net8.0
            var fallbackRoot = Directory.GetParent(assemblyDirectory)?.Parent?.Parent?.FullName;
            Debug.WriteLine($"[ImageHelper] Using fallback project root: {fallbackRoot}");
            
            return fallbackRoot ?? assemblyDirectory;
        }
        
        public static Image LoadImage(string path)
        {
            if (string.IsNullOrWhiteSpace(path) || !File.Exists(path)) return null;
            using var fs = File.OpenRead(path);
            return Image.FromStream(fs);
        }

        /// <summary>
        /// Save image to Library/Image folder and return relative path
        /// </summary>
        /// <param name="sourceFilePath">Source file path from OpenFileDialog</param>
        /// <param name="customFileName">Optional custom file name (without extension)</param>
        /// <returns>Relative path to the saved image</returns>
        public static string SaveImageToLibrary(string sourceFilePath, string customFileName = null)
        {
            if (string.IsNullOrWhiteSpace(sourceFilePath) || !File.Exists(sourceFilePath))
                throw new FileNotFoundException("Source file not found", sourceFilePath);

            // Use project root directory instead of bin directory
            var projectRoot = GetProjectRootDirectory();
            var imageDirectory = Path.Combine(projectRoot, IMAGE_DIRECTORY);
            
            Debug.WriteLine($"[ImageHelper] Project Root: {projectRoot}");
            Debug.WriteLine($"[ImageHelper] Image Directory: {imageDirectory}");
            
            if (!Directory.Exists(imageDirectory))
            {
                Directory.CreateDirectory(imageDirectory);
                Debug.WriteLine($"[ImageHelper] Created directory: {imageDirectory}");
            }

            // Generate unique file name
            var extension = Path.GetExtension(sourceFilePath);
            var fileName = string.IsNullOrWhiteSpace(customFileName)
                ? $"{Guid.NewGuid()}{extension}"
                : $"{customFileName}_{DateTime.Now:yyyyMMddHHmmss}{extension}";

            var destinationPath = Path.Combine(imageDirectory, fileName);
            
            Debug.WriteLine($"[ImageHelper] Destination Path: {destinationPath}");

            // Copy file to Library/Image
            File.Copy(sourceFilePath, destinationPath, overwrite: true);
            
            Debug.WriteLine($"[ImageHelper] File copied successfully");

            // Return relative path
            var relativePath = Path.Combine(IMAGE_DIRECTORY, fileName).Replace("\\", "/");
            Debug.WriteLine($"[ImageHelper] Relative Path: {relativePath}");
            
            return relativePath;
        }

        /// <summary>
        /// Get full path from relative path (using project root)
        /// </summary>
        public static string GetFullPath(string relativePath)
        {
            if (string.IsNullOrWhiteSpace(relativePath))
                return null;

            var projectRoot = GetProjectRootDirectory();
            var fullPath = Path.Combine(projectRoot, relativePath.Replace("/", "\\"));
            
            Debug.WriteLine($"[ImageHelper.GetFullPath] Project Root: {projectRoot}");
            Debug.WriteLine($"[ImageHelper.GetFullPath] Relative: {relativePath} -> Full: {fullPath}");
            
            return fullPath;
        }

        /// <summary>
        /// Delete image from library
        /// </summary>
        public static bool DeleteImageFromLibrary(string relativePath)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(relativePath))
                    return false;

                var fullPath = GetFullPath(relativePath);
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                    Debug.WriteLine($"[ImageHelper] Deleted: {fullPath}");
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ImageHelper] Delete error: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Check if image file exists
        /// </summary>
        public static bool ImageExists(string relativePath)
        {
            if (string.IsNullOrWhiteSpace(relativePath))
                return false;

            var fullPath = GetFullPath(relativePath);
            var exists = File.Exists(fullPath);
            
            Debug.WriteLine($"[ImageHelper.ImageExists] Path: {relativePath} -> Exists: {exists}");
            
            return exists;
        }

        /// <summary>
        /// Resize and optimize image before saving
        /// </summary>
        public static string SaveAndOptimizeImage(string sourceFilePath, int maxWidth = 1920, int maxHeight = 1080, string customFileName = null)
        {
            Debug.WriteLine($"[ImageHelper.SaveAndOptimizeImage] ========== START ==========");
            Debug.WriteLine($"[ImageHelper] Source File: {sourceFilePath}");
            Debug.WriteLine($"[ImageHelper] Custom Name: {customFileName ?? "NULL"}");
            
            if (string.IsNullOrWhiteSpace(sourceFilePath) || !File.Exists(sourceFilePath))
                throw new FileNotFoundException("Source file not found", sourceFilePath);

            // Use project root directory instead of bin directory
            var projectRoot = GetProjectRootDirectory();
            var imageDirectory = Path.Combine(projectRoot, IMAGE_DIRECTORY);
            
            Debug.WriteLine($"[ImageHelper] Project Root: {projectRoot}");
            Debug.WriteLine($"[ImageHelper] Image Directory: {imageDirectory}");
            
            if (!Directory.Exists(imageDirectory))
            {
                Directory.CreateDirectory(imageDirectory);
                Debug.WriteLine($"[ImageHelper] Created directory: {imageDirectory}");
            }
            else
            {
                Debug.WriteLine($"[ImageHelper] Directory already exists");
            }

            // Generate unique file name
            var extension = Path.GetExtension(sourceFilePath).ToLower();
            var fileName = string.IsNullOrWhiteSpace(customFileName)
                ? $"{Guid.NewGuid()}{extension}"
                : $"{customFileName}_{DateTime.Now:yyyyMMddHHmmss}{extension}";

            var destinationPath = Path.Combine(imageDirectory, fileName);
            
            Debug.WriteLine($"[ImageHelper] File Name: {fileName}");
            Debug.WriteLine($"[ImageHelper] Destination Path: {destinationPath}");

            try
            {
                // Load and resize image
                using (var originalImage = Image.FromFile(sourceFilePath))
                {
                    Debug.WriteLine($"[ImageHelper] Original Size: {originalImage.Width}x{originalImage.Height}");
                    
                    var newWidth = originalImage.Width;
                    var newHeight = originalImage.Height;

                    // Calculate new dimensions while maintaining aspect ratio
                    if (originalImage.Width > maxWidth || originalImage.Height > maxHeight)
                    {
                        var ratioX = (double)maxWidth / originalImage.Width;
                        var ratioY = (double)maxHeight / originalImage.Height;
                        var ratio = Math.Min(ratioX, ratioY);

                        newWidth = (int)(originalImage.Width * ratio);
                        newHeight = (int)(originalImage.Height * ratio);
                        
                        Debug.WriteLine($"[ImageHelper] Resizing to: {newWidth}x{newHeight}");
                    }

                    // Create resized image
                    using (var resizedImage = new Bitmap(newWidth, newHeight))
                    {
                        using (var graphics = Graphics.FromImage(resizedImage))
                        {
                            graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                            graphics.DrawImage(originalImage, 0, 0, newWidth, newHeight);
                        }

                        // Save with appropriate format
                        ImageFormat format = extension switch
                        {
                            ".jpg" or ".jpeg" => ImageFormat.Jpeg,
                            ".png" => ImageFormat.Png,
                            ".gif" => ImageFormat.Gif,
                            ".bmp" => ImageFormat.Bmp,
                            _ => ImageFormat.Jpeg
                        };

                        Debug.WriteLine($"[ImageHelper] Saving with format: {format}");
                        resizedImage.Save(destinationPath, format);
                        Debug.WriteLine($"[ImageHelper] Image saved successfully to: {destinationPath}");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ImageHelper] ERROR: {ex.Message}");
                Debug.WriteLine($"[ImageHelper] Stack Trace: {ex.StackTrace}");
                throw;
            }

            // Verify file was created
            if (File.Exists(destinationPath))
            {
                var fileInfo = new FileInfo(destinationPath);
                Debug.WriteLine($"[ImageHelper] ✓ File created successfully!");
                Debug.WriteLine($"[ImageHelper] ✓ File size: {fileInfo.Length} bytes");
                Debug.WriteLine($"[ImageHelper] ✓ Full path: {destinationPath}");
            }
            else
            {
                Debug.WriteLine($"[ImageHelper] ✗ WARNING: File not found after save!");
            }

            // Return relative path
            var relativePath = Path.Combine(IMAGE_DIRECTORY, fileName).Replace("\\", "/");
            Debug.WriteLine($"[ImageHelper] Relative Path: {relativePath}");
            Debug.WriteLine($"[ImageHelper.SaveAndOptimizeImage] ========== END ==========");
            
            return relativePath;
        }
    }
}