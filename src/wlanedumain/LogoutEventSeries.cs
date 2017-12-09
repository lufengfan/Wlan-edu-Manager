using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SamLu.Tools.Wlan_edu_Manager
{
    public delegate void LogoutEventHandler(object sender, LogoutEventArgs e);

    public class LogoutEventArgs : EventArgs
    {
        private string userName;
        private bool cancelAutoLogin;

        public string UserName => this.userName;
        public bool CancelAutoLogin => this.cancelAutoLogin;

        public LogoutEventArgs(string userName, bool cancelAutoLogin)
        {
            this.userName = userName ?? throw new ArgumentNullException(nameof(userName));
            this.cancelAutoLogin = cancelAutoLogin;
        }
    }
}
