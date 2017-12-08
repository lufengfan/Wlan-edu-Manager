using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Threading;

namespace SamLu.Tools.Wlan_edu_Manager.GUI
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        static App()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ErrorHandleHelper.SetupBugReport(
                "Wlan-edu Manager",
                "ManagerGUI.WPF",
                typeof(App).Assembly.Location,
                exception: (Exception)e.ExceptionObject
            );
        }

        private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            e.Handled = true;
            ErrorHandleHelper.SetupBugReport(
                "Wlan-edu Manager",
                "ManagerGUI.WPF",
                typeof(App).Assembly.Location,
                exception: e.Exception
            );
        }
    }
}
