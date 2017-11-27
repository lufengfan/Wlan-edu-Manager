using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SamLu.Tools.Wlan_edu_Manager.Login
{
    public interface ILogin
    {
        void Initialize();
        string Login(
            string userName,
            string userPwd,
            bool autoLogin
        );
    }
}
