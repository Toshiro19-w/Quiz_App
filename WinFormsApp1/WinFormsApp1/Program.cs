using Microsoft.Extensions.Configuration;
using WinFormsApp1.TestForm;
using WinFormsApp1.View.User;

namespace WinFormsApp1
{
    internal static class Program
    {
		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		/// 
		public static IConfiguration Configuration;
		[STAThread]
        static void Main()
        {
			// To customize application configuration such as set high DPI settings or default font,
			// see https://aka.ms/applicationconfiguration.

			// Load appsettings.json cho WinForms
			Configuration = new ConfigurationBuilder()
				.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.AddJsonFile("appsettings.Development.json", optional: true)
				.Build();

			ApplicationConfiguration.Initialize();
            //Application.Run(new dangnhap());

            Application.Run(new dangnhap());

		}
    }
}