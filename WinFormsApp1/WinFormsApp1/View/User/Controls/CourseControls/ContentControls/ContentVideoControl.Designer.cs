using System.Windows.Forms;
using LibVLCSharp.WinForms;
using System.Drawing;

namespace WinFormsApp1.View.User.Controls.CourseControls.ContentControls
{
	partial class ContentVideoControl
	{
		private ComboBox cboContentType;
		private Label lblType;
		private TextBox txtTitle;
		private Label lblTitle;
		private TextBox txtVideoPath;
		private Label lblVideo;
		private Button btnBrowse;
		private Button btnDelete;
		private VideoView videoView;

		private Button btnPlay;
		private Button btnReplay;
		private Button btnMute;

		private void InitializeComponent()
		{
			cboContentType = new ComboBox();
			lblType = new Label();
			txtTitle = new TextBox();
			lblTitle = new Label();
			txtVideoPath = new TextBox();
			lblVideo = new Label();
			btnBrowse = new Button();
			btnDelete = new Button();
			videoView = new VideoView();
			btnPlay = new Button();
			btnReplay = new Button();
			btnMute = new Button();
			((System.ComponentModel.ISupportInitialize)videoView).BeginInit();
			SuspendLayout();
			// 
			// cboContentType
			// 
			cboContentType.DropDownStyle = ComboBoxStyle.DropDownList;
			cboContentType.Items.AddRange(new object[] { "Theory", "Video", "FlashcardSet", "Test" });
			cboContentType.Location = new Point(20, 50);
			cboContentType.Name = "cboContentType";
			cboContentType.Size = new Size(214, 33);
			cboContentType.TabIndex = 0;
			// 
			// lblType
			// 
			lblType.AutoSize = true;
			lblType.Location = new Point(20, 20);
			lblType.Name = "lblType";
			lblType.Size = new Size(125, 25);
			lblType.TabIndex = 1;
			lblType.Text = "Loại nội dung:";
			// 
			// txtTitle
			// 
			txtTitle.Location = new Point(20, 130);
			txtTitle.Name = "txtTitle";
			txtTitle.Size = new Size(438, 31);
			txtTitle.TabIndex = 3;
			// 
			// lblTitle
			// 
			lblTitle.AutoSize = true;
			lblTitle.Location = new Point(20, 100);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new Size(73, 25);
			lblTitle.TabIndex = 2;
			lblTitle.Text = "Tiêu đề:";
			// 
			// txtVideoPath
			// 
			txtVideoPath.Location = new Point(20, 210);
			txtVideoPath.Name = "txtVideoPath";
			txtVideoPath.ReadOnly = true;
			txtVideoPath.Size = new Size(438, 31);
			txtVideoPath.TabIndex = 5;
			// 
			// lblVideo
			// 
			lblVideo.AutoSize = true;
			lblVideo.Location = new Point(20, 180);
			lblVideo.Name = "lblVideo";
			lblVideo.Size = new Size(62, 25);
			lblVideo.TabIndex = 4;
			lblVideo.Text = "Video:";
			// 
			// btnBrowse
			// 
			btnBrowse.Location = new Point(20, 258);
			btnBrowse.Name = "btnBrowse";
			btnBrowse.Size = new Size(120, 32);
			btnBrowse.TabIndex = 6;
			btnBrowse.Text = "Chọn video";
			// 
			// btnDelete
			// 
			btnDelete.BackColor = Color.Red;
			btnDelete.FlatStyle = FlatStyle.Flat;
			btnDelete.ForeColor = Color.White;
			btnDelete.Location = new Point(730, 10);
			btnDelete.Name = "btnDelete";
			btnDelete.Size = new Size(80, 30);
			btnDelete.TabIndex = 7;
			btnDelete.Text = "Xóa";
			btnDelete.UseVisualStyleBackColor = false;
			// 
			// videoView
			// 
			videoView.BackColor = Color.Black;
			videoView.Location = new Point(490, 64);
			videoView.MediaPlayer = null;
			videoView.Name = "videoView";
			videoView.Size = new Size(320, 177);
			videoView.TabIndex = 8;
			// 
			// btnPlay
			// 
			btnPlay.Location = new Point(490, 258);
			btnPlay.Name = "btnPlay";
			btnPlay.Size = new Size(100, 32);
			btnPlay.TabIndex = 9;
			btnPlay.Text = "▶ Play";
			// 
			// btnReplay
			// 
			btnReplay.Location = new Point(600, 258);
			btnReplay.Name = "btnReplay";
			btnReplay.Size = new Size(100, 32);
			btnReplay.TabIndex = 10;
			btnReplay.Text = "🔁 Replay";
			// 
			// btnMute
			// 
			btnMute.Location = new Point(710, 258);
			btnMute.Name = "btnMute";
			btnMute.Size = new Size(100, 32);
			btnMute.TabIndex = 11;
			btnMute.Text = "🔊 Mute";
			// 
			// ContentVideoControl
			// 
			Controls.Add(cboContentType);
			Controls.Add(lblType);
			Controls.Add(lblTitle);
			Controls.Add(txtTitle);
			Controls.Add(lblVideo);
			Controls.Add(txtVideoPath);
			Controls.Add(btnBrowse);
			Controls.Add(btnDelete);
			Controls.Add(videoView);
			Controls.Add(btnPlay);
			Controls.Add(btnReplay);
			Controls.Add(btnMute);
			Name = "ContentVideoControl";
			Size = new Size(830, 319);
			((System.ComponentModel.ISupportInitialize)videoView).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
	}
}
