using System;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibVLCSharp.Shared;
using WinFormsApp1.Helpers;
using WinFormsApp1.ViewModels;

namespace WinFormsApp1.View.User.Controls.CourseControls.ContentControls
{
	public partial class ContentVideoControl : UserControl, IContentControl
	{
		public event Action<object, string>? ContentTypeChanged;
		public event Action<object>? DeleteRequested;

		private LibVLC _libVLC;
		private MediaPlayer _mediaPlayer;

		private string? _oldVideoPath = null;
		public ContentVideoControl()
		{
			InitializeComponent();
			Core.Initialize(); // Quan trọng

			// VLC init
			_libVLC = new LibVLC();
			_mediaPlayer = new MediaPlayer(_libVLC);

			videoView.MediaPlayer = _mediaPlayer;

			// Không auto play
			_mediaPlayer.EnableHardwareDecoding = true;

			// Nút sự kiện
			btnPlay.Click += (s, e) => TogglePlayPause();
			btnReplay.Click += (s, e) => ReplayVideo();
			btnMute.Click += (s, e) => ToggleMute();

			btnBrowse.Click += BtnBrowse_Click;
			btnDelete.Click += (s, e) => DeleteRequested?.Invoke(this);

			cboContentType.SelectedIndex = 1; // Video
			cboContentType.SelectedIndexChanged += (s, e) =>
			{
				var vietnameseType = cboContentType.SelectedItem?.ToString();
				if (vietnameseType != "Video")
				{
					// Chuyển sang tiếng Anh trước khi trigger event
					var englishType = ContentTypeHelper.ToEnglish(vietnameseType);
					ContentTypeChanged?.Invoke(this, englishType);
				}
			};
		}

		// ============================================================
		// TOGGLE PLAY / PAUSE
		// ============================================================
		private void TogglePlayPause()
		{
			if (_mediaPlayer.IsPlaying)
			{
				_mediaPlayer.Pause();
				btnPlay.Text = "▶ Play";
			}
			else
			{
				_mediaPlayer.Play();
				btnPlay.Text = "⏸ Pause";
			}
		}

		// ============================================================
		// REPLAY
		// ============================================================
		private void ReplayVideo()
		{
			_mediaPlayer.Stop();
			_mediaPlayer.Play();
		}

		// ============================================================
		// MUTE / UNMUTE
		// ============================================================
		private void ToggleMute()
		{
			_mediaPlayer.Mute = !_mediaPlayer.Mute;
			btnMute.Text = _mediaPlayer.Mute ? "🔇 Unmute" : "🔊 Mute";
		}

		// ============================================================
		// LOAD VIDEO (KHÔNG AUTOPLAY)
		// ============================================================
		private void LoadVideo(string relativePath)
		{
			string fullPath = Path.Combine(MediaHelper.GetProjectRoot(), relativePath);

			if (!File.Exists(fullPath))
			{
				MessageBox.Show("Không tìm thấy video:\n" + fullPath);
				return;
			}

			var media = new Media(_libVLC, new Uri(fullPath));
			_mediaPlayer.Media = media;

			btnPlay.Text = "▶ Play";
		}

        // ============================================================
        // BROWSE VIDEO - Upload lên Azure với Progress Bar
        // ============================================================
        private async void BtnBrowse_Click(object? sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog();
            ofd.Filter = "Video files|*.mp4;*.mkv;*.webm;*.avi;*.mov";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                // Tạo Progress Form
                Form progressForm = null;
                ProgressBar progressBar = null;
                Label lblStatus = null;

                try
                {
                    // Tạo form hiển thị tiến trình
                    progressForm = new Form
                    {
                        Text = "Đang upload video...",
                        Size = new Size(400, 150),
                        FormBorderStyle = FormBorderStyle.FixedDialog,
                        StartPosition = FormStartPosition.CenterParent,
                        MaximizeBox = false,
                        MinimizeBox = false,
                        ControlBox = false
                    };

                    progressBar = new ProgressBar
                    {
                        Location = new Point(20, 30),
                        Size = new Size(340, 30),
                        Style = ProgressBarStyle.Continuous,
                        Minimum = 0,
                        Maximum = 100
                    };

                    lblStatus = new Label
                    {
                        Location = new Point(20, 70),
                        Size = new Size(340, 40),
                        Text = "Đang chuẩn bị upload...",
                        TextAlign = ContentAlignment.MiddleCenter
                    };

                    progressForm.Controls.Add(progressBar);
                    progressForm.Controls.Add(lblStatus);
                    progressForm.Show();

                    // Disable button
                    btnBrowse.Enabled = false;

                    // Progress reporter
                    var progress = new Progress<int>(percent =>
                    {
                        if (progressForm != null && !progressForm.IsDisposed)
                        {
                            progressForm.Invoke((MethodInvoker)delegate
                            {
                                progressBar.Value = percent;
                                lblStatus.Text = $"Đang upload: {percent}%";
                            });
                        }
                    });

                    // Upload video lên Azure
                    var videoUrl = await MediaHelper.CopyVideoToLibraryAsync(ofd.FileName, progress);

                    if (videoUrl != null)
                    {
                        txtVideoPath.Text = videoUrl;

                        // Load video để preview
                        if (MediaHelper.IsAzureUrl(videoUrl))
                        {
                            // VLC stream từ Azure URL
                            LoadVideoFromUrl(videoUrl);
                        }
                        else
                        {
                            LoadVideo(videoUrl);
                        }

                        MessageBox.Show("Upload video thành công!", "Thành công",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi upload video: {ex.Message}", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    // Enable button
                    btnBrowse.Enabled = true;

                    // Close progress form
                    if (progressForm != null && !progressForm.IsDisposed)
                    {
                        progressForm.Close();
                        progressForm.Dispose();
                    }
                }
            }
        }

        /// <summary>
        /// Load video từ Azure URL - Download về temp folder rồi play
        /// </summary>
        private async void LoadVideoFromUrl(string url)
        {
            try
            {
                btnPlay.Text = "⏳ Đang tải...";
                btnPlay.Enabled = false;
                btnBrowse.Enabled = false;

                // Tạo tên file từ URL (lấy phần cuối và loại bỏ ký tự không hợp lệ)
                string fileName = Path.GetFileName(new Uri(url).LocalPath);
                // Loại bỏ các ký tự không hợp lệ trong tên file
                foreach (char c in Path.GetInvalidFileNameChars())
                {
                    fileName = fileName.Replace(c, '_');
                }
                
                // Tạo unique temp path để tránh xung đột
                string tempPath = Path.Combine(Path.GetTempPath(), $"preview_{fileName}");

                // Kiểm tra nếu đã download trước đó thì không cần download lại
                if (!File.Exists(tempPath))
                {
                    using (var httpClient = new HttpClient())
                    {
                        httpClient.Timeout = TimeSpan.FromMinutes(5);
                        var response = await httpClient.GetAsync(url);
                        response.EnsureSuccessStatusCode();

                        using (var fs = new FileStream(tempPath, FileMode.Create, FileAccess.Write, FileShare.None))
                        {
                            await response.Content.CopyToAsync(fs);
                        }
                    }
                }

                // Play video từ temp file
                if (File.Exists(tempPath))
                {
                    var media = new Media(_libVLC, tempPath, FromType.FromPath);
                    _mediaPlayer.Media = media;
                    btnPlay.Text = "▶ Play";
                }
                else
                {
                    MessageBox.Show("Không thể tải video từ Azure.", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnPlay.Text = "▶ Play";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể load video: {ex.Message}", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnPlay.Text = "▶ Play";
            }
            finally
            {
                btnPlay.Enabled = true;
                btnBrowse.Enabled = true;
            }
        }

        // ============================================================
        // LOAD & SAVE VIEWMODEL
        // ============================================================
        public void LoadFromViewModel(LessonContentBuilderViewModel vm)
		{
			txtTitle.Text = vm.Title ?? "";
			txtVideoPath.Text = vm.VideoUrl ?? "";
			_oldVideoPath = vm.VideoUrl;
			if (!string.IsNullOrEmpty(vm.VideoUrl))
			{
				// Kiểm tra xem có phải Azure URL không
				if (MediaHelper.IsAzureUrl(vm.VideoUrl))
				{
					LoadVideoFromUrl(vm.VideoUrl);
				}
				else
				{
					LoadVideo(vm.VideoUrl);
				}
			}
		}

		public LessonContentBuilderViewModel SaveToViewModel()
		{
			// Chỉ xóa video cũ nếu là local file (không phải Azure URL)
			if (!string.IsNullOrEmpty(_oldVideoPath) && _oldVideoPath != txtVideoPath.Text)
			{
				if (!MediaHelper.IsAzureUrl(_oldVideoPath))
				{
					string fullPath = Path.Combine(MediaHelper.GetProjectRoot(), _oldVideoPath);
					if (File.Exists(fullPath)) File.Delete(fullPath);
				}
				// Note: Không xóa file trên Azure để tránh mất dữ liệu nếu có share
			}
			return new LessonContentBuilderViewModel
			{
				ContentType = "Video", // Lưu bằng tiếng Anh
				Title = txtTitle.Text.Trim(),
				VideoUrl = txtVideoPath.Text.Trim()
			};
		}

		private void ContentVideoControl_Load(object sender, EventArgs e)
		{

		}
	}
}
