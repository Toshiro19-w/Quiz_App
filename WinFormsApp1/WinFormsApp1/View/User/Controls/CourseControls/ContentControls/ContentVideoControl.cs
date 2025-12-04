using System;
using System.Drawing;
using System.IO;
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
		// BROWSE VIDEO
		// ============================================================
		private void BtnBrowse_Click(object? sender, EventArgs e)
		{
			using var ofd = new OpenFileDialog();
			ofd.Filter = "Video files|*.mp4;*.mkv;*.webm;*.avi;*.mov";

			if (ofd.ShowDialog() == DialogResult.OK)
			{
				// ================================
				// 1. XÓA VIDEO CŨ TRƯỚC KHI COPY
				// ================================
				//if (!string.IsNullOrEmpty(txtVideoPath.Text))
				//{
				//	string oldPath = Path.Combine(MediaHelper.GetProjectRoot(), txtVideoPath.Text.Replace("/", "\\"));

				//	try
				//	{
				//		if (File.Exists(oldPath))
				//			File.Delete(oldPath);
				//	}
				//	catch (Exception ex)
				//	{
				//		MessageBox.Show($"Không thể xóa video cũ:\n{ex.Message}");
				//	}
				//}

				// ================================
				// 2. COPY VIDEO MỚI
				// ================================
				var rel = MediaHelper.CopyVideoToLibrary(ofd.FileName);

				if (rel != null)
				{
					txtVideoPath.Text = rel;
					LoadVideo(rel);
				}
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
				LoadVideo(vm.VideoUrl);
		}

		public LessonContentBuilderViewModel SaveToViewModel()
		{
			if (!string.IsNullOrEmpty(_oldVideoPath) && _oldVideoPath != txtVideoPath.Text)
			{
				string fullPath = Path.Combine(MediaHelper.GetProjectRoot(), _oldVideoPath);
				if (File.Exists(fullPath)) File.Delete(fullPath);
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
