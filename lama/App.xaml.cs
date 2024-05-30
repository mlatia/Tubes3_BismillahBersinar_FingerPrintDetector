using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Fingerprint_Detection
{
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            // Call the database loader to perform database operations
            DatabaseTester.LoadDatabase();

            // Show the main window
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}

