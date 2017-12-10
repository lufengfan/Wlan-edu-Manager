using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SamLu.Tools.Wlan_edu_Manager.Logout
{
    [Serializable]
    public class LogoutInfo
    {
        public string wlanAcName;

        public string wlanAcIp;

        public string wlanUserIp;

        public string ssid;

        public string userName;
        
        public string encryUser;
        
        public int passType = 1;

        public int isLocalUser = 1;
    }
}
