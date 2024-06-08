using System;
using System.Windows.Forms;

namespace FingerPrintDetector
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            AttachConsole(-1);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

         [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool AttachConsole(int dwProcessId);
    }
}