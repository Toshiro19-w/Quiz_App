using Microsoft.Extensions.Configuration;
using System.Runtime.InteropServices;
using WinFormsApp1.Localization;

namespace WinFormsApp1
{
    internal static class Program
    {
		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		/// 
		public static IConfiguration Configuration;

        [DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();

        [STAThread]
        static void Main()
        {
            // Enable DPI Awareness for high-DPI displays
            if (Environment.OSVersion.Version.Major >= 6)
            {
                SetProcessDPIAware();
            }

			Configuration = new ConfigurationBuilder()
				.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.AddJsonFile("appsettings.Development.json", optional: true)
				.Build();

            // Configure application for high DPI
            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Initialize language/localization system
            LanguageHelper.Initialize();

			// To customize application configuration such as set high DPI settings or default font,
			// see https://aka.ms/applicationconfiguration.
			ApplicationConfiguration.Initialize();
            Application.Run(new dangnhap());
		}
    }
}