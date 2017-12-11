using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SamLu.Tools.Wlan_edu_Manager.GUI
{
    internal class Program
    {
        static Program()
        {
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ErrorHandleHelper.SetupBugReport(
                "Wlan-edu Manager",
                "ManagerGUI.WinForm",
                typeof(Program).Assembly.Location,
                exception: (Exception)e.ExceptionObject
            );

            Application.Exit();
        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            ErrorHandleHelper.SetupBugReport(
                "Wlan-edu Manager",
                "ManagerGUI.WinForm",
                typeof(Program).Assembly.Location,
                exception: e.Exception
            );

            Application.Exit();
        }

        internal void Run(string userName, string userPwd, bool isAutoLogin, bool cancelAutoLogin)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm()
#if true
            {
                manager = Wlan_eduManager.CreateManagerFromRedirection()
            }
#endif
            );
        }
    }
}
