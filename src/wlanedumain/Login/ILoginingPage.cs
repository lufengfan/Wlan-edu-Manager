using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SamLu.Tools.Wlan_edu_Manager.Login
{
    /// <summary>
    /// 正在登入页。
    /// </summary>
    public interface ILoginingPage : IManagerPage
    {
        /// <summary>
        /// 成功登入。
        /// </summary>
        /// <returns>登入成功页。</returns>
        ILoginSucceededPage Success();
    }
}
