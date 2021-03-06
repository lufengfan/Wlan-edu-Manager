﻿using HtmlAgilityPack;
using SamLu.Tools.Wlan_edu_Manager.Implementation;
using SamLu.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SamLu.Tools.Wlan_edu_Manager.Login.Implementation
{
    public class LoginForcedPage : ManagerPage, ILoginForcedPage
    {
        protected internal const string FORM_NAME = "formForce";

        /// <summary>
        /// 初始化 <see cref="LoginForcedPage"/> 类的新实例。
        /// </summary>
        /// <param name="source">指定的 HTML 源代码。</param>
        public LoginForcedPage(string source) : base(source) { }

        /// <summary>
        /// 初始化 <see cref="LoginForcedPage"/> 类的新实例。
        /// </summary>
        /// <param name="url">网页的 URL 地址。</param>
        /// <param name="encoding">网页 HTML 源代码的编码。</param>
        public LoginForcedPage(string url, Encoding encoding) : base(url, encoding) { }

        /// <summary>
        /// 初始化 <see cref="LoginForcedPage"/> 类的新实例。
        /// </summary>
        /// <param name="document">追定的内部 HTML 文档对象。</param>
        public LoginForcedPage(HtmlDocument document) : base(document) { }

        /// <summary>
        /// 初始化 <see cref="LoginForcedPage"/> 。
        /// </summary>
        public override void Initialize() { }

        public ILoginInfoPage Cancel()
        {
            var query = new
            {
                wlanacname = this.wlanAcName,
                wlanacip = "",
                wlanuserip = this.wlanUserIp,
                ssid = ""
            };
            return new LoginInfoPage($"{this.scriptVariants["httpBase"]}{this.scriptVariants["ctxPath"]}/index.wlan?{query.SerializeData()}", this.encoding)
            {
                wlanAcName = this.wlanAcName,
                wlanUserIp = this.wlanUserIp,
                scriptVariants = this.scriptVariants
            };
        }

        public IManagerPage ForceLogin()
        {
            var form = this.document.DocumentNode.SelectSingleNode($"//form[@name='{LoginForcedPage.FORM_NAME}']");
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(form.Submit(this.encoding));

            form = doc.DocumentNode.SelectSingleNode($"//form[@name='{LoginingPage.FORM_NAME}']");
            if (form != null)
            {
                doc = new HtmlDocument();
                doc.LoadHtml(form.Submit(this.encoding));
                return new LoginingPage(doc)
                {
                    wlanAcName = this.wlanAcName,
                    wlanUserIp = this.wlanUserIp,
                    scriptVariants = this.scriptVariants
                };
            }
            else
                throw new NotImplementedException();
        }
    }
}
