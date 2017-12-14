using HtmlAgilityPack;
using SamLu.Tools.Wlan_edu_Manager.Implementation;
using SamLu.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SamLu.Tools.Wlan_edu_Manager.Login.Implementation
{
    public class LoginingPage : ManagerPage, ILoginingPage
    {
        protected internal const string FORM_NAME = "submitForm";

        /// <summary>
        /// 初始化 <see cref="LoginingPage"/> 类的新实例。
        /// </summary>
        /// <param name="source">指定的 HTML 源代码。</param>
        public LoginingPage(string source) : base(source) { }

        /// <summary>
        /// 初始化 <see cref="LoginingPage"/> 类的新实例。
        /// </summary>
        /// <param name="url">网页的 URL 地址。</param>
        /// <param name="encoding">网页 HTML 源代码的编码。</param>
        public LoginingPage(string url, Encoding encoding) : base(url, encoding) { }

        /// <summary>
        /// 初始化 <see cref="LoginingPage"/> 类的新实例。
        /// </summary>
        /// <param name="document">追定的内部 HTML 文档对象。</param>
        public LoginingPage(HtmlDocument document) : base(document) { }
        
        /// <summary>
        /// 初始化 <see cref="LoginingPage"/> 。
        /// </summary>
        public override void Initialize() { }

        public ILoginSucceededPage Success()
        {
            var form = this.document.DocumentNode.SelectSingleNode($"//form[@name='{LoginingPage.FORM_NAME}']");
            return new LoginSucceededPage(form.Submit(this.encoding))
            {
                url = form.GetAttributeValue("action", null),
                wlanAcName = this.wlanAcName,
                wlanUserIp = this.wlanUserIp,
                scriptVariants = this.scriptVariants
            };
        }
    }
}
