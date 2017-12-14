using HtmlAgilityPack;
using SamLu.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SamLu.Tools.Wlan_edu_Manager.Implementation
{
    /// <summary>
    /// 管理页
    /// </summary>
    public abstract class ManagerPage : IManagerPage
    {
        /// <summary>
        /// <see cref="ManagerPage"/> 内部的 HTML 文档对象。
        /// </summary>
        protected HtmlDocument document;
        /// <summary>
        /// <see cref="ManagerPage"/> 内部的 URL 地址。
        /// </summary>
        protected internal string url;
        /// <summary>
        /// <see cref="ManagerPage"/> 内部的 HTML 源代码的代码。
        /// </summary>
        protected Encoding encoding;

        /// <summary>
        /// Wlan-edu 的 AC Name 。
        /// </summary>
        protected internal string wlanAcName;
        /// <summary>
        /// Wlan-edu 的用户 IP 地址。
        /// </summary>
        protected internal string wlanUserIp;
        /// <summary>
        /// Wlan-edu URL 的基本组成部分。
        /// </summary>
        protected internal IDictionary<string, object> scriptVariants;

        /// <summary>
        /// 获取 <see cref="ManagerPage"/> 的 HTML 源代码。
        /// </summary>
        public string Source { get => this.document?.ParsedText; }

        /// <summary>
        /// 获取 <see cref="ManagerPage"/> 的 URL 地址。
        /// </summary>
        public string Url => this.url;

        /// <summary>
        /// 初始化 <see cref="ManagerPage"/> 类的新实例。
        /// </summary>
        /// <param name="source">指定的 HTML 源代码。</param>
        protected ManagerPage(string source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            
            this.document = new HtmlDocument();
            this.document.LoadHtml(source);

            this.url = null;
            this.encoding = Encoding.UTF8;
        }

        /// <summary>
        /// 初始化 <see cref="ManagerPage"/> 类的新实例。
        /// </summary>
        /// <param name="url">网页的 URL 地址。</param>
        /// <param name="encoding">网页 HTML 源代码的编码。</param>
        protected ManagerPage(string url, Encoding encoding) :
            this(HttpRequestUtil.GetHtmlContent(
                url ?? throw new ArgumentNullException(nameof(url)),
                encoding ?? throw new ArgumentNullException(nameof(encoding))
            ))
        {
            this.url = url;
            this.encoding = encoding;
        }

        /// <summary>
        /// 初始化 <see cref="ManagerPage"/> 类的新实例。
        /// </summary>
        /// <param name="document">追定的内部 HTML 文档对象。</param>
        protected ManagerPage(HtmlDocument document)
        {
            if (document == null) throw new ArgumentNullException(nameof(document));

            this.document = document;
            this.url = null;
            this.encoding = Encoding.UTF8;
        }

        /// <summary>
        /// 初始化 <see cref="ManagerPage"/> 。
        /// </summary>
        public abstract void Initialize();
    }
}
