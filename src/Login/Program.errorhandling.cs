using System;

namespace SamLu.Tools.Wlan_edu_Manager.Login
{
    partial class Program
    {
		static Program()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ErrorHandleHelper.SetupBugReport(
                "Wlan-edu Manager",
                "Login",
                typeof(Program).Assembly.Location,
                exception: ((Exception)e.ExceptionObject)
            );
        }
    }
}