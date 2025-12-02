using System;
using WinFormsApp1.Models.EF;

namespace WinFormsApp1.Controllers
{
    /// <summary>
    /// AdminController - Base partial class
    /// 
    /// This controller is split into multiple partial classes for better organization:
    /// - AdminController.Dashboard.cs - Dashboard and Analytics methods
    /// - AdminController.Users.cs - User Management methods
    /// - AdminController.Tests.cs - Test and Question Management methods
    /// - AdminController.Courses.cs - Course, Chapter, Lesson, and Category Management methods
    /// 
    /// All partial files are located in Controllers/Admin/ folder
    /// </summary>
    public partial class AdminController
    {
        internal void Dispose()
        {
            // Cleanup resources if needed
        }
    }
}