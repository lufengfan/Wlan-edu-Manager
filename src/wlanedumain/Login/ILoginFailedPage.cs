using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SamLu.Tools.Wlan_edu_Manager.Login
{
    /// <summary>
    /// 登入失败页。
    /// </summary>
    public interface ILoginFailedPage : IManagerPage
    {
        /// <summary>
        /// 忽略错误并返回登入信息页。
        /// </summary>
        /// <returns>重定向到登入信息页。</returns>
        ILoginInfoPage Ignore();
    }
}
