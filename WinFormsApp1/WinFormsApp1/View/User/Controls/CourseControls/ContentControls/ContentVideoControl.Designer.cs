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
			panel1 = new Panel();
			((System.ComponentModel.ISupportInitialize)videoView).BeginInit();
			SuspendLayout();
			// 
			// cboContentType
			// 
			cboContentType.DropDownStyle = ComboBoxStyle.DropDownList;
			cboContentType.Font = new Font("Segoe UI", 12F);
			cboContentType.Items.AddRange(new object[] { "Lý thuyết", "Video", "Bộ thẻ ghi nhớ", "Bài kiểm tra" });
			cboContentType.Location = new Point(39, 85);
			cboContentType.Name = "cboContentType";
			cboContentType.Size = new Size(300, 40);
			cboContentType.TabIndex = 0;
			// 
			// lblType
			// 
			lblType.AutoSize = true;
			lblType.Font = new Font("Segoe UI", 12F);
			lblType.Location = new Point(39, 47);
			lblType.Name = "lblType";
			lblType.Size = new Size(166, 32);
			lblType.TabIndex = 1;
			lblType.Text = "Loại nội dung:";
			// 
			// txtTitle
			// 
			txtTitle.Font = new Font("Segoe UI", 12F);
			txtTitle.Location = new Point(39, 169);
			txtTitle.Name = "txtTitle";
			txtTitle.Size = new Size(833, 39);
			txtTitle.TabIndex = 3;
			// 
			// lblTitle
			// 
			lblTitle.AutoSize = true;
			lblTitle.Font = new Font("Segoe UI", 12F);
			lblTitle.Location = new Point(39, 133);
			lblTitle.Name = "lblTitle";
			lblTitle.Size = new Size(99, 32);
			lblTitle.TabIndex = 2;
			lblTitle.Text = "Tiêu đề:";
			// 
			// txtVideoPath
			// 
			txtVideoPath.Font = new Font("Segoe UI", 12F);
			txtVideoPath.Location = new Point(39, 252);
			txtVideoPath.Name = "txtVideoPath";
			txtVideoPath.ReadOnly = true;
			txtVideoPath.Size = new Size(833, 39);
			txtVideoPath.TabIndex = 5;
			// 
			// lblVideo
			// 
			lblVideo.AutoSize = true;
			lblVideo.Font = new Font("Segoe UI", 12F);
			lblVideo.Location = new Point(39, 215);
			lblVideo.Name = "lblVideo";
			lblVideo.Size = new Size(81, 32);
			lblVideo.TabIndex = 4;
			lblVideo.Text = "Video:";
			// 
			// btnBrowse
			// 
			btnBrowse.Font = new Font("Segoe UI", 10F);
			btnBrowse.Location = new Point(39, 310);
			btnBrowse.Name = "btnBrowse";
			btnBrowse.Size = new Size(141, 40);
			btnBrowse.TabIndex = 6;
			btnBrowse.Text = "Chọn video";
			// 
			// btnDelete
			// 
			btnDelete.BackColor = Color.Red;
			btnDelete.FlatStyle = FlatStyle.Flat;
			btnDelete.Font = new Font("Segoe UI", 10F);
			btnDelete.ForeColor = Color.White;
			btnDelete.Location = new Point(1314, 13);
			btnDelete.Name = "btnDelete";
			btnDelete.Size = new Size(100, 35);
			btnDelete.TabIndex = 7;
			btnDelete.Text = "Xóa";
			btnDelete.UseVisualStyleBackColor = false;
			// 
			// videoView
			// 
			videoView.BackColor = Color.Black;
			videoView.Font = new Font("Segoe UI", 10F);
			videoView.Location = new Point(976, 70);
			videoView.MediaPlayer = null;
			videoView.Name = "videoView";
			videoView.Size = new Size(438, 244);
			videoView.TabIndex = 8;
			// 
			// btnPlay
			// 
			btnPlay.Font = new Font("Segoe UI", 10F);
			btnPlay.Location = new Point(1060, 333);
			btnPlay.Name = "btnPlay";
			btnPlay.Size = new Size(100, 35);
			btnPlay.TabIndex = 9;
			btnPlay.Text = "▶ Play";
			// 
			// btnReplay
			// 
			btnReplay.Font = new Font("Segoe UI", 10F);
			btnReplay.Location = new Point(1170, 333);
			btnReplay.Name = "btnReplay";
			btnReplay.Size = new Size(100, 35);
			btnReplay.TabIndex = 10;
			btnReplay.Text = "🔁 Replay";
			// 
			// btnMute
			// 
			btnMute.Font = new Font("Segoe UI", 10F);
			btnMute.Location = new Point(1280, 333);
			btnMute.Name = "btnMute";
			btnMute.Size = new Size(100, 35);
			btnMute.TabIndex = 11;
			btnMute.Text = "🔊 Mute";
			// 
			// panel1
			// 
			panel1.BackColor = Color.FromArgb(255, 128, 0);
			panel1.Dock = DockStyle.Left;
			panel1.Location = new Point(0, 0);
			panel1.Name = "panel1";
			panel1.Size = new Size(10, 398);
			panel1.TabIndex = 12;
			// 
			// ContentVideoControl
			// 
			BackColor = Color.White;
			BorderStyle = BorderStyle.FixedSingle;
			Controls.Add(panel1);
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
			Size = new Size(1448, 398);
			Load += ContentVideoControl_Load;
			((System.ComponentModel.ISupportInitialize)videoView).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}
		private Panel panel1;
	}
}
