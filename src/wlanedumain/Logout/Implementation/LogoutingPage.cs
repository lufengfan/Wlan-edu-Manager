using HtmlAgilityPack;
using SamLu.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SamLu.Tools.Wlan_edu_Manager.Logout.Implementation
{
    public class LogoutingPage : ManagerPage, ILogoutingPage
    {
        private const string FORM_NAME = "submitForm";

        /// <summary>
        /// 初始化 <see cref="LogoutingPage"/> 类的新实例。
        /// </summary>
        /// <param name="source">指定的 HTML 源代码。</param>
        public LogoutingPage(string source) : base(source) { }

        /// <summary>
        /// 初始化 <see cref="LogoutingPage"/> 类的新实例。
        /// </summary>
        /// <param name="url">网页的 URL 地址。</param>
        /// <param name="encoding">网页 HTML 源代码的编码。</param>
        public LogoutingPage(string url, Encoding encoding) : base(url, encoding) { }

        /// <summary>
        /// 初始化 <see cref="LogoutingPage"/> 类的新实例。
        /// </summary>
        /// <param name="document">追定的内部 HTML 文档对象。</param>
        public LogoutingPage(HtmlDocument document) : base(document) { }

        /// <summary>
        /// 初始化 <see cref="LogoutingPage"/> 。
        /// </summary>
        public override void Initialize() { }

        /// <summary>
        /// 成功登出。
        /// </summary>
        /// <returns>登出成功页。</returns>
        public ILogoutSucceededPage Success()
        {
            var form = this.document.DocumentNode.SelectSingleNode($"//form[@name='{LogoutingPage.FORM_NAME}']");
            return new LogoutSucceededPage(form.Submit(this.encoding))
            {
                url = form.GetAttributeValue("action", null),
                wlanAcName = this.wlanAcName,
                wlanUserIp = this.wlanUserIp
            };
        }
    }
}
