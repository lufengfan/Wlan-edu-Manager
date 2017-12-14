using HtmlAgilityPack;
using SamLu.Tools.Wlan_edu_Manager.Implementation;
using SamLu.Tools.Wlan_edu_Manager.Login.Implementation;
using SamLu.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SamLu.Tools.Wlan_edu_Manager.Logout.Implementation
{
    public class LogoutSucceededPage : LoginInfoPage, ILogoutSucceededPage
    {
        /// <summary>
        /// 初始化 <see cref="LogoutSucceededPage"/> 类的新实例。
        /// </summary>
        /// <param name="source">指定的 HTML 源代码。</param>
        public LogoutSucceededPage(string source) : base(source) { }

        /// <summary>
        /// 初始化 <see cref="LogoutSucceededPage"/> 类的新实例。
        /// </summary>
        /// <param name="url">网页的 URL 地址。</param>
        /// <param name="encoding">网页 HTML 源代码的编码。</param>
        public LogoutSucceededPage(string url, Encoding encoding) : base(url, encoding) { }

        /// <summary>
        /// 初始化 <see cref="LogoutSucceededPage"/> 类的新实例。
        /// </summary>
        /// <param name="document">追定的内部 HTML 文档对象。</param>
        public LogoutSucceededPage(HtmlDocument document) : base(document) { }

        /// <summary>
        /// 初始化 <see cref="LogoutSucceededPage"/> 。
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

            this.loginActionAddress = $"{this.scriptVariants["httpBase"]}{this.scriptVariants["ctxPath"]}/portalLogin.wlan?{Wlan_eduManager.GetMiliseconds(base.currentTime)}";
        }
        
        /// <summary>
        /// 退出。
        /// </summary>
        public void Exit() { }
    }
}
