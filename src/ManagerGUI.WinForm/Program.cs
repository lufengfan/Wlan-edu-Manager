using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SamLu.Tools.Wlan_edu_Manager.GUI
{
    internal class Program
    {
        internal void Run(string userName, string userPwd, bool isAutoLogin, bool cancelAutoLogin)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
