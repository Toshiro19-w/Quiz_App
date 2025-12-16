using System.Diagnostics;
using WinFormsApp1.Helpers;
using WinFormsApp1.Service.PaymentService;

namespace WinFormsApp1.View.Dialogs
{
    public partial class MoMoPaymentForm : Form
    {
        private readonly CartPaymentService _paymentService;
        private readonly int _userId;
        private readonly int? _courseId;
        private readonly bool _isCartPayment;
        private readonly int? _discountId;
        private readonly decimal _discountAmount;
        private string _currentOrderId = "";

        public bool PaymentCompleted { get; private set; }

        /// <summary>
        /// Constructor cho thanh toán giỏ hàng hoặc khóa học đơn lẻ
        /// </summary>
        /// <param name="userId">ID người dùng</param>
        /// <param name="courseId">ID khóa học (null nếu thanh toán giỏ hàng)</param>
        /// <param name="discountId">ID mã giảm giá (nếu có)</param>
        /// <param name="discountAmount">Số tiền giảm giá</param>
        public MoMoPaymentForm(int userId, int? courseId = null, int? discountId = null, decimal discountAmount = 0)
        {
            InitializeComponent();
            _paymentService = new CartPaymentService();
            _userId = userId;
            _courseId = courseId;
            _isCartPayment = courseId == null;
            _discountId = discountId;
            _discountAmount = discountAmount;

            SetupForm();
        }

        private void SetupForm()
        {
            this.Text = _isCartPayment ? "Thanh toán giỏ hàng - MoMo" : "Thanh toán khóa học - MoMo";
            this.Size = new Size(500, 400);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Panel chính
            var mainPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20)
            };

            // Logo MoMo
            var logoLabel = new Label
            {
                Text = "💳 MoMo Payment",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(169, 3, 41),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 50
            };

            // Thông tin thanh toán
            var infoText = _isCartPayment ? "Thanh toán toàn bộ giỏ hàng" : "Thanh toán khóa học";
            if (_discountAmount > 0)
            {
                infoText += $"\n(Đã áp dụng giảm giá: -{_discountAmount:N0} VND)";
            }
            
            var infoLabel = new Label
            {
                Text = infoText,
                Font = new Font("Segoe UI", 12),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = _discountAmount > 0 ? 50 : 30
            };

            // Status label
            var statusLabel = new Label
            {
                Name = "statusLabel",
                Text = "Đang khởi tạo thanh toán...",
                Font = new Font("Segoe UI", 10),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 40,
                ForeColor = Color.Blue
            };

            // Progress bar
            var progressBar = new ProgressBar
            {
                Name = "progressBar",
                Style = ProgressBarStyle.Marquee,
                Dock = DockStyle.Top,
                Height = 20,
                MarqueeAnimationSpeed = 50
            };

            // Button panel
            var buttonPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 60,
                Padding = new Padding(0, 10, 0, 0)
            };

            var cancelButton = new Button
            {
                Text = "Hủy",
                Size = new Size(100, 35),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right,
                Location = new Point(buttonPanel.Width - 120, 15)
            };
            cancelButton.Click += (s, e) => this.Close();

            buttonPanel.Controls.Add(cancelButton);

            mainPanel.Controls.Add(buttonPanel);
            mainPanel.Controls.Add(progressBar);
            mainPanel.Controls.Add(statusLabel);
            mainPanel.Controls.Add(infoLabel);
            mainPanel.Controls.Add(logoLabel);

            this.Controls.Add(mainPanel);

            this.Load += async (s, e) => await StartPaymentAsync();
        }

        private async Task StartPaymentAsync()
        {
            try
            {
                var statusLabel = this.Controls.Find("statusLabel", true)[0] as Label;

                statusLabel.Text = "Đang tạo đơn hàng...";
                statusLabel.ForeColor = Color.Blue;

                PaymentResult result;
                if (_isCartPayment)
                {
                    // Truyền discountId và discountAmount vào service
                    result = await _paymentService.PayCartWithMoMoAsync(_userId, _discountId, _discountAmount);
                }
                else
                {
                    result = await _paymentService.PaySingleCourseWithMoMoAsync(_userId, _courseId!.Value, _discountId, _discountAmount);
                }

                if (result.Success)
                {
                    _currentOrderId = result.OrderId;
                    statusLabel.Text = "Đang chuyển hướng đến MoMo...";

                    // Mở trình duyệt với URL thanh toán
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = result.PaymentUrl,
                        UseShellExecute = true
                    });

                    statusLabel.Text = "Vui lòng hoàn tất thanh toán trên MoMo";
                    statusLabel.ForeColor = Color.Green;

                    // Bắt đầu kiểm tra trạng thái thanh toán
                    await StartPaymentStatusCheckAsync();
                }
                else
                {
                    statusLabel.Text = $"Lỗi: {result.Message}";
                    statusLabel.ForeColor = Color.Red;

                    var progressBar = this.Controls.Find("progressBar", true)[0] as ProgressBar;
                    progressBar.Style = ProgressBarStyle.Blocks;
                    progressBar.Value = 0;
                }
            }
            catch (Exception ex)
            {
                var statusLabel = this.Controls.Find("statusLabel", true)[0] as Label;
                statusLabel.Text = $"Lỗi: {ex.Message}";
                statusLabel.ForeColor = Color.Red;
            }
        }

        private async Task StartPaymentStatusCheckAsync()
        {
            var statusLabel = this.Controls.Find("statusLabel", true)[0] as Label;
            var progressBar = this.Controls.Find("progressBar", true)[0] as ProgressBar;
            var momoService = new MoMoPaymentService();

            for (int i = 0; i < 30; i++)
            {
                if (this.IsDisposed || this.Disposing) break;

                await Task.Delay(5000);
                statusLabel.Text = $"Đang kiểm tra ({i + 1}/30)...";

                try
                {
                    var resp = await momoService.QueryPaymentStatusAsync(_currentOrderId);
                    System.Diagnostics.Debug.WriteLine($"Poll {i + 1}: resultCode={resp?.resultCode}, status={resp?.status}");

                    if (resp == null) continue;

                    // Kiểm tra resultCode = 0 (thành công)
                    if (resp.resultCode == 0)
                    {
                        if (await _paymentService.CompletePaymentAsync(_currentOrderId))
                        {
                            PaymentCompleted = true;
                            progressBar.Style = ProgressBarStyle.Blocks;
                            progressBar.Value = 100;
                            statusLabel.Text = "Thanh toán thành công!";
                            statusLabel.ForeColor = Color.Green;
                            ToastHelper.Show(this, "Thanh toán thành công!");
                            await Task.Delay(1500);
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                            return;
                        }
                    }

                    // Kiểm tra thất bại (1006 = user cancel)
                    if (resp.resultCode == 1006)
                    {
                        statusLabel.Text = "Người dùng hủy thanh toán";
                        statusLabel.ForeColor = Color.Red;
                        progressBar.Style = ProgressBarStyle.Blocks;
                        return;
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Poll error: {ex.Message}");
                }
            }

            if (!this.IsDisposed)
            {
                statusLabel.Text = "Hết thời gian chờ. Vui lòng kiểm tra lại.";
                statusLabel.ForeColor = Color.Orange;
                progressBar.Style = ProgressBarStyle.Blocks;
            }
        }
    }
}