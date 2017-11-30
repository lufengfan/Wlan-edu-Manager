using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SamLu.Tools.Wlan_edu_Manager.Logout
{
    /// <summary>
    /// 登出信息页。
    /// </summary>
    public interface ILogoutInfoPage : IManagerPage
    {
        /// <summary>
        /// 登出。
        /// </summary>
        /// <param name="userName">用户名。</param>
        /// <param name="cancelAutoLogin">一个值，指示是否取消自动登入。</param>
        /// <returns></returns>
        IManagerPage Logout(
            string userName,
            bool cancelAutoLogin
        );
    }
}
