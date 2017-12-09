using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SamLu.Tools.Wlan_edu_Manager
{
    public delegate void FetchTemporaryPwdEventHandler(object sender, FetchTemporaryPwdEventArgs e);

    public class FetchTemporaryPwdEventArgs : EventArgs
    {
        private string userName;

        public string UserName => this.userName;

        public FetchTemporaryPwdEventArgs(string userName) =>
            this.userName = userName ?? throw new ArgumentNullException(nameof(ArgumentNullException));
    }
}
