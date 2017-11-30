using SamLu.Tools.Wlan_edu_Manager.Logout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SamLu.Tools.Wlan_edu_Manager.Login
{
    /// <summary>
    /// 登入成功页。
    /// </summary>
    public interface ILoginSucceededPage : ILogoutInfoPage
    {
        /// <summary>
        /// 获取 <see cref="ILoginSucceededPage"/> 登入成功的时间。
        /// </summary>
        DateTime SucceededTime { get; }

        /// <summary>
        /// 获取登入成功页显示的 Wlan-edu 信息。
        /// </summary>
        IDictionary<string, string> WlanInfos { get; }

        /// <summary>
        /// 登出。
        /// </summary>
        /// <param name="cancelAutoLogin">一个值，指示是否取消自动登入。</param>
        /// <returns></returns>
        IManagerPage Logout(
            bool cancelAutoLogin
        );
    }
}
