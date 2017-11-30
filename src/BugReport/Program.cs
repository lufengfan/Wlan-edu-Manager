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
        internal static string[] ReportFiles { get; private set; }

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length < 2) return;

            try
            {
                Program.ProductName = args[0];
                Program.ComName = args[1];
                Program.ExecutiveName = args.Length >= 3 ? args[2] : null;
                Program.RestartCmdLine = args.Length >= 4 ? args[3] : null;
                Program.ReportFiles = args.Length >= 4 ? args.Skip(4).ToArray() : null;

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
