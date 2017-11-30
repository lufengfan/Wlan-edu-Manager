using HtmlAgilityPack;
using SamLu.Tools.Wlan_edu_Manager.Logout.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SamLu.Tools.Wlan_edu_Manager.Login.Implementation
{
    public class LoginSucceededPage : LogoutInfoPage, ILoginSucceededPage
    {
        private DateTime succeededTime;

        /// <summary>
        /// 获取 <see cref="LoginSucceededPage"/> 登入成功的时间。
        /// </summary>
        public DateTime SucceededTime => this.succeededTime;

        /// <summary>
        /// 获取登入成功页显示的 Wlan-edu 信息。
        /// </summary>
        public IDictionary<string, string> WlanInfos { get; } = new Dictionary<string, string>();

        /// <summary>
        /// 初始化 <see cref="LoginSucceededPage"/> 类的新实例。
        /// </summary>
        /// <param name="source">指定的 HTML 源代码。</param>
        public LoginSucceededPage(string source) : base(source) { }

        /// <summary>
        /// 初始化 <see cref="LoginSucceededPage"/> 类的新实例。
        /// </summary>
        /// <param name="url">网页的 URL 地址。</param>
        /// <param name="encoding">网页 HTML 源代码的编码。</param>
        public LoginSucceededPage(string url, Encoding encoding) : base(url, encoding) { }

        /// <summary>
        /// 初始化 <see cref="LoginSucceededPage"/> 类的新实例。
        /// </summary>
        /// <param name="document">追定的内部 HTML 文档对象。</param>
        public LoginSucceededPage(HtmlDocument document) : base(document) { }

        /// <summary>
        /// 初始化 <see cref="LoginSucceededPage"/> 。
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

            this.succeededTime = DateTime.Now;

            foreach (var node in this.document.DocumentNode.SelectNodes(@"//p[@class='tc_js']"))
            {
                Match match = Regex.Match(node.InnerText, @"(?<Name>^\S*?)：(?<Value>(\s|\S)*$)");
                this.WlanInfos.Add(
                    match.Groups["Name"].Value,
                    match.Groups["Value"].Value
                );
            }
        }

        /// <summary>
        /// 登出。
        /// </summary>
        /// <param name="cancelAutoLogin">一个值，指示是否取消自动登入。</param>
        /// <returns></returns>
        public IManagerPage Logout(bool cancelAutoLogin)
        {
            string userName =
                this.document.GetElementbyId("formLogout")
                    ?.SelectSingleNode(@"/input[@name='userName']")
                    ?.GetAttributeValue("value", null);

            return base.Logout(userName, cancelAutoLogin);
        }
    }
}
