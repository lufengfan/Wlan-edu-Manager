using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SamLu.Tools.Wlan_edu_Manager.Login
{
    /// <summary>
    /// 强制登入页。
    /// </summary>
    public interface ILoginForcedPage : IManagerPage
    {
        /// <summary>
        /// 强制登入。
        /// </summary>
        /// <returns>强制登入后的自动重定向页。</returns>
        /// <remarks>
        /// 返回值的取值可能为 <see cref="ILoginSucceededPage"/> 或 <see cref="ILoginFailedPage"/> 。
        /// </remarks>
        IManagerPage ForceLogin();

        /// <summary>
        /// 取消强制登入。
        /// </summary>
        /// <returns>取消强制登入后，重定向到登入信息页。</returns>
        ILoginInfoPage Cancel();
    }
}
