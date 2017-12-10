using HtmlAgilityPack;
using SamLu.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SamLu.Tools.Wlan_edu_Manager.Login.Implementation
{
    public class LoginInfoPage : ManagerPage, ILoginInfoPage
    {
        protected internal string loginActionAddress;
        protected internal string fetchTemporaryPwdAddress;

        /// <summary>
        /// 初始化 <see cref="LoginInfoPage"/> 类的新实例。
        /// </summary>
        /// <param name="source">指定的 HTML 源代码。</param>
        public LoginInfoPage(string source) : base(source) { }

        /// <summary>
        /// 初始化 <see cref="LoginInfoPage"/> 类的新实例。
        /// </summary>
        /// <param name="url">网页的 URL 地址。</param>
        /// <param name="encoding">网页 HTML 源代码的编码。</param>
        public LoginInfoPage(string url, Encoding encoding) : base(url, encoding) { }

        /// <summary>
        /// 初始化 <see cref="LoginInfoPage"/> 类的新实例。
        /// </summary>
        /// <param name="document">追定的内部 HTML 文档对象。</param>
        public LoginInfoPage(HtmlDocument document) : base(document) { }

        /// <summary>
        /// 初始化 <see cref="LoginInfoPage"/> 。
        /// </summary>
        public override void Initialize()
        {
            IDictionary<string, object> scriptVariants =
                Regex.Matches(
                    this.document.DocumentNode
                        .SelectSingleNode(@"html/head/script")
                        ?.InnerText,
                    @"var\s+(?<VariantName>\w+?)\s+=\s+(?<VariantValue>(\s|\S)+?);"
                )
                .OfType<Match>()
                .ToDictionary<Match, string, object>(
                    (match => match.Groups["VariantName"].Value),
                    (match =>
                    {
                        string value = match.Groups["VariantValue"].Value;
                        if (Regex.IsMatch(value, @"^(-)?\s*\d+$"))
                        {
                            return int.Parse(value);
                        }
                        else if (Regex.IsMatch(value, @"^\d*\.\d+$"))
                        {
                            return double.Parse(value);
                        }
                        else if (Regex.IsMatch(value, @"^""[^""]*""$"))
                        {
                            return value.Trim('"');
                        }
                        else
                            throw new NotSupportedException();
                    })
                );
            
            this.loginActionAddress = this.document.GetElementbyId("Wlan_Login")?.GetAttributeValue("action", null);
            this.fetchTemporaryPwdAddress = $"{scriptVariants["httpbase"]}{scriptVariants["ctxPath"]}/portalApplyPwd.wlan";
        }

        /// <summary>
        /// 收集指定的信息并登入。
        /// </summary>
        /// <param name="userName">用户名。</param>
        /// <param name="userPwd">用户密码。</param>
        /// <param name="autoLogin">一个值，指示是否自动登入。</param>
        /// <returns>登陆结果页。</returns>
        /// <remarks>
        /// 返回值的取值可能为 <see cref="LoginingPage"/> 或 <see cref="LoginForcedPage"/> 。
        /// </remarks>
        public IManagerPage Login(string userName, string userPwd, bool autoLogin)
        {
            LoginInfo loginInfo = new LoginInfo()
            {
                wlanAcName = this.wlanAcName,
                wlanUserIp = this.wlanUserIp,
                userName = userName,
                userPwd = userPwd,
                idissaveinfo = autoLogin ? LoginInfo.IsSaveInfo.Save : LoginInfo.IsSaveInfo.NotSave
            };
            string dataStr = loginInfo.SerializeData();

            string response = this.encoding.GetString(
                HttpRequestUtil.Post(
                    this.loginActionAddress,
                    this.encoding.GetBytes(dataStr)
                )
            );

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(response);
            
            var form = doc.DocumentNode.SelectSingleNode("//form");
            if (form == null) throw new InvalidOperationException("无法定位元素'form'。");
            switch (form.GetAttributeValue("name", null))
            {
                case LoginForcedPage.FORM_NAME:
                    return new LoginForcedPage(doc)
                    {
                        url = this.loginActionAddress,
                        wlanAcName = this.wlanAcName,
                        wlanUserIp = this.wlanUserIp
                    };
                case LoginingPage.FORM_NAME:
                    return new LoginingPage(doc)
                    {
                        url = this.loginActionAddress,
                        wlanAcName = this.wlanAcName,
                        wlanUserIp = this.wlanUserIp
                    };
                default:
                    throw new InvalidOperationException("无法识别页。");
            }
        }

        public void FetchTemporaryPwd(string userName)
        {
            var query = new
            {
                mobile = userName,
                wlanAcName = this.wlanAcName,
                wlanUserIp = this.wlanUserIp
            };
            string dataStr = query.SerializeData();

            string response = this.encoding.GetString(
                HttpRequestUtil.Post(
                    this.fetchTemporaryPwdAddress,
                    this.encoding.GetBytes(dataStr)
                )
            );

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(response);

            ;
        }
    }
}
