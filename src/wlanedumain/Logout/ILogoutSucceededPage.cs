using SamLu.Tools.Wlan_edu_Manager.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SamLu.Tools.Wlan_edu_Manager.Logout
{
    /// <summary>
    /// 登出成功页。
    /// </summary>
    public interface ILogoutSucceededPage : ILoginInfoPage
    {
        /// <summary>
        /// 退出。
        /// </summary>
        void Exit();
    }
}
