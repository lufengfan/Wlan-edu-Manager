using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SamLu.Tools.Wlan_edu_Manager.Login
{
    /// <summary>
    /// 登入信息页。
    /// </summary>
    public interface ILoginInfoPage : IManagerPage
    {
        /// <summary>
        /// 收集指定的信息并登入。
        /// </summary>
        /// <param name="userName">用户名。</param>
        /// <param name="userPwd">用户密码。</param>
        /// <param name="autoLogin">一个值，指示是否自动登入。</param>
        /// <returns>登陆结果页。</returns>
        /// <remarks>
        /// 返回值的取值可能为 <see cref="ILoginingPage"/> 或 <see cref="ILoginForcedPage"/> 。
        /// </remarks>
        IManagerPage Login(
            string userName,
            string userPwd,
            bool autoLogin
        );
    }
}
