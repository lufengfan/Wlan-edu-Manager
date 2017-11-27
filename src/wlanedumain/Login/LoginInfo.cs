using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace SamLu.Tools.Wlan_edu_Manager.Login
{
    [Serializable]
    public class LoginInfo
    {
        public string wlanAcName;
        
        public string wlanAcIp;
        
        public string wlanUserIp;
        
        public string ssid;
        
        public string userName;
        
        public string _userPwd = "输入固定密码/临时密码";
        
        public string userPwd;
        
        public string verifyCode;
        
        public string verifyHidden;

        [NonSerialized]
        public string button = "  ";

        [NonSerialized]
        public string loginBtn = "  ";

        [DefaultValue(IsSaveInfo.NotSave)]
        public int idissaveinfo;
        
        public int passType;

        public static class IsSaveInfo
        {
            public const int NotSave = 0;
            public const int Save = 1;
        }
    }
}