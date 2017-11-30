using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SamLu.Tools.Wlan_edu_Manager.Logout
{
    /// <summary>
    /// 正在登出页。
    /// </summary>
    public interface ILogoutingPage : IManagerPage
    {
        /// <summary>
        /// 成功登出。
        /// </summary>
        /// <returns>登出成功页。</returns>
        ILogoutSucceededPage Success();
    }
}
