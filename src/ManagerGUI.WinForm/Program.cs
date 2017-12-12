using SamLu.Tools.Wlan_edu_Manager.Login.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Windows.Forms;

namespace SamLu.Tools.Wlan_edu_Manager.GUI
{
    internal class Program
    {
        internal static Wlan_eduManager manager;

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

            try
            {
                Program.manager = Wlan_eduManager.CreateManagerFromRedirection();
            }
            catch (Wlan_eduNotConnectedException)
            {
                throw;
            }
            catch (Exception)
            {
                userName = null;
                userPwd = null;
                isAutoLogin = false;
                cancelAutoLogin = false;
                

                DateTime currentTime = DateTime.Now;
                Program.manager = new Wlan_eduManager(
                    new LoginInfoPage(string.Empty)
                    {
                        wlanAcName = "0434.0571.571.00",
                        wlanUserIp = "10.137.188.218",
                        scriptVariants = new Dictionary<string, object>()
                        {
                            { "httpBase", "https://211.138.125.52:7090" },
                            {"ctxPath", "/zmcc" }
                        },
                        currentTime = currentTime,
                        loginActionAddress = $"https://211.138.125.52:7090/zmcc/portalLogin.wlan?{Wlan_eduManager.GetMiliseconds(currentTime)}",
                        fetchTemporaryPwdAddress = $"https://211.138.125.52:7090/zmcc/portalApplyPwd.wlan"
                    }
                );
            }

            var form = new MainForm(userName, userPwd, isAutoLogin, cancelAutoLogin);
            Application.Run(form);
        }
    }
}
