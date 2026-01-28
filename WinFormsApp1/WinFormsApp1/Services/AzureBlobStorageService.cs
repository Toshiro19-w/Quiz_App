using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace WinFormsApp1.Services
{
    /// <summary>
    /// Service ?? qu?n lý upload/download files t? Azure Blob Storage
    /// </summary>
    public class AzureBlobStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;
        
        // Container names trong Azure Blob Storage
        private const string VIDEOS_CONTAINER = "videos";
        private const string DOCUMENTS_CONTAINER = "documents";

        public AzureBlobStorageService(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException(nameof(connectionString), "Azure Blob Connection String is required");

            _blobServiceClient = new BlobServiceClient(connectionString);
            
            // Ensure containers exist
            InitializeContainersAsync().Wait();
        }

        /// <summary>
        /// T?o containers n?u ch?a t?n t?i
        /// </summary>
        private async Task InitializeContainersAsync()
        {
            await CreateContainerIfNotExistsAsync(VIDEOS_CONTAINER);
            await CreateContainerIfNotExistsAsync(DOCUMENTS_CONTAINER);
        }

        /// <summary>
        /// T?o container v?i public access
        /// </summary>
        private async Task CreateContainerIfNotExistsAsync(string containerName)
        {
            try
            {
                var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
                await containerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating container '{containerName}': {ex.Message}", ex);
            }
        }

        #region Upload Methods

        /// <summary>
        /// Upload video file lên Azure Blob Storage v?i progress tracking
        /// </summary>
        /// <param name="localFilePath">???ng d?n file local</param>
        /// <param name="progress">Progress reporter (0-100)</param>
        /// <returns>Azure Blob URL c?a file ?ã upload</returns>
        public async Task<string> UploadVideoAsync(string localFilePath, IProgress<int> progress = null)
        {
            if (!File.Exists(localFilePath))
                throw new FileNotFoundException($"File not found: {localFilePath}");

            return await UploadFileAsync(localFilePath, VIDEOS_CONTAINER, progress);
        }

        /// <summary>
        /// Upload document file lên Azure Blob Storage v?i progress tracking
        /// </summary>
        /// <param name="localFilePath">???ng d?n file local</param>
        /// <param name="progress">Progress reporter (0-100)</param>
        /// <returns>Azure Blob URL c?a file ?ã upload</returns>
        public async Task<string> UploadDocumentAsync(string localFilePath, IProgress<int> progress = null)
        {
            if (!File.Exists(localFilePath))
                throw new FileNotFoundException($"File not found: {localFilePath}");

            return await UploadFileAsync(localFilePath, DOCUMENTS_CONTAINER, progress);
        }

        /// <summary>
        /// Generic upload method v?i progress tracking
        /// </summary>
        private async Task<string> UploadFileAsync(string localFilePath, string containerName, IProgress<int> progress)
        {
            try
            {
                // Generate unique file name
                string originalFileName = Path.GetFileName(localFilePath);
                string uniqueFileName = $"{Guid.NewGuid()}_{originalFileName}";

                var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
                var blobClient = containerClient.GetBlobClient(uniqueFileName);

                // Set content type
                var blobHttpHeaders = new BlobHttpHeaders
                {
                    ContentType = GetContentType(localFilePath)
                };

                // Get file size for progress calculation
                var fileInfo = new FileInfo(localFilePath);
                long fileSize = fileInfo.Length;

                // Upload with progress tracking
                using (var fileStream = File.OpenRead(localFilePath))
                {
                    // Create upload options
                    var uploadOptions = new BlobUploadOptions
                    {
                        HttpHeaders = blobHttpHeaders,
                        TransferOptions = new Azure.Storage.StorageTransferOptions
                        {
                            // Use 4MB chunks for better progress reporting
                            InitialTransferSize = 4 * 1024 * 1024,
                            MaximumTransferSize = 4 * 1024 * 1024
                        }
                    };

                    // Track progress
                    if (progress != null)
                    {
                        long totalBytesUploaded = 0;
                        var progressHandler = new Progress<long>(bytesUploaded =>
                        {
                            totalBytesUploaded = bytesUploaded;
                            int percentComplete = (int)((double)totalBytesUploaded / fileSize * 100);
                            if (percentComplete > 100) percentComplete = 100;
                            progress.Report(percentComplete);
                        });

                        uploadOptions.ProgressHandler = progressHandler;
                    }

                    await blobClient.UploadAsync(fileStream, uploadOptions);
                }

                // Return the full Azure Blob URL
                return blobClient.Uri.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error uploading file to Azure Blob Storage: {ex.Message}", ex);
            }
        }

        #endregion

        #region Download Methods

        /// <summary>
        /// Download file t? Azure Blob Storage v? stream
        /// </summary>
        public async Task<Stream> DownloadFileAsync(string blobUrl)
        {
            try
            {
                var blobClient = new BlobClient(new Uri(blobUrl));
                var response = await blobClient.DownloadAsync();
                return response.Value.Content;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error downloading file from Azure: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Download file t? Azure v? local path
        /// </summary>
        public async Task DownloadFileToPathAsync(string blobUrl, string localFilePath)
        {
            try
            {
                var blobClient = new BlobClient(new Uri(blobUrl));
                await blobClient.DownloadToAsync(localFilePath);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error downloading file to path: {ex.Message}", ex);
            }
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Xác ??nh Content-Type d?a vào extension
        /// </summary>
        private string GetContentType(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToLowerInvariant();
            return extension switch
            {
                // Videos
                ".mp4" => "video/mp4",
                ".avi" => "video/x-msvideo",
                ".mov" => "video/quicktime",
                ".wmv" => "video/x-ms-wmv",
                ".flv" => "video/x-flv",
                ".webm" => "video/webm",
                ".mkv" => "video/x-matroska",

                // Documents
                ".pdf" => "application/pdf",
                ".doc" => "application/msword",
                ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                ".xls" => "application/vnd.ms-excel",
                ".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                ".ppt" => "application/vnd.ms-powerpoint",
                ".pptx" => "application/vnd.openxmlformats-officedocument.presentationml.presentation",
                ".txt" => "text/plain",

                _ => "application/octet-stream"
            };
        }

        /// <summary>
        /// Ki?m tra file có t?n t?i trên Azure không
        /// </summary>
        public async Task<bool> FileExistsAsync(string blobUrl)
        {
            try
            {
                var blobClient = new BlobClient(new Uri(blobUrl));
                return await blobClient.ExistsAsync();
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// L?y kích th??c file t? Azure
        /// </summary>
        public async Task<long> GetFileSizeAsync(string blobUrl)
        {
            try
            {
                var blobClient = new BlobClient(new Uri(blobUrl));
                var properties = await blobClient.GetPropertiesAsync();
                return properties.Value.ContentLength;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting file size: {ex.Message}", ex);
            }
        }

        #endregion
    }
}
