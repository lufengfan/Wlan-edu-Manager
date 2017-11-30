using SamLu.Tools.Wlan_edu_Manager.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SamLu.Tools.Wlan_edu_Manager.Logout
{
    /// <summary>
    /// 登出失败页。
    /// </summary>
    public interface ILogoutFailedPage : IManagerPage
    {
        /// <summary>
        /// 忽略错误并返回登入信息页。
        /// </summary>
        /// <returns>重定向到登入信息页。</returns>
        ILoginInfoPage Ignore();

        /// <summary>
        /// 退出登入。
        /// </summary>
        void Exit();
    }
}
