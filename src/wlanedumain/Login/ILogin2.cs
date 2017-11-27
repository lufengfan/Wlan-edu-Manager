using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SamLu.Tools.Wlan_edu_Manager.Login
{
    public interface ILogin2
    {
        void Initialize();

        string Login(
            string wlanAcName,
            string wlanAcIp,
            string wlanUserIp,
            string ssid,
            string userName,
            string userPwd,
            bool autoLogin
        );
    }
}
