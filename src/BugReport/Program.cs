using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace BugReport
{
    static class Program
    {
        internal static string ProductName { get; set; }
        internal static string ComName { get; set; }
        internal static string ExecutiveName { get; set; }
        internal static string RestartCmdLine { get; set; }
        internal static string[] ReportFiles { get; set; }

        public static void Run()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainWin());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
