using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SamLu.Tools.Wlan_edu_Manager
{
    public delegate void LoginEventHandler(object sender, LoginEventArgs e);

    public class LoginEventArgs : EventArgs
    {
        private string userName;
        private string userPwd;
        private bool rememberMe;
        private bool autoLogin;

        public string UserName => this.userName;
        public string UserPwd => this.userPwd;
        public bool RememberMe => this.rememberMe;
        public bool AutoLogin => this.autoLogin;

        public LoginEventArgs(string userName, string userPwd, bool rememberMe, bool autoLogin)
        {
            this.userName = userName ?? throw new ArgumentNullException(nameof(userName));
            this.userPwd = userPwd ?? throw new ArgumentNullException(nameof(userPwd));
            this.rememberMe = rememberMe;
            this.autoLogin = autoLogin;
        }
    }
}
