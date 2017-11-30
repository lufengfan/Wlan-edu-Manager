using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SamLu.Tools.Wlan_edu_Manager
{
    /// <summary>
    /// Wlan-edu Manager 页。
    /// </summary>
    public interface IManagerPage
    {
        /// <summary>
        /// 获取 <see cref="IManagerPage"/> 的 HTML 源代码。
        /// </summary>
        string Source { get; }

        /// <summary>
        /// 初始化 <see cref="IManagerPage"/> 。
        /// </summary>
        void Initialize();
    }
}
