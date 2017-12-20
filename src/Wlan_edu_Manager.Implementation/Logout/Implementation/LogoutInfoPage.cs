using HtmlAgilityPack;
using SamLu.Tools.Wlan_edu_Manager.Implementation;
using SamLu.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SamLu.Tools.Wlan_edu_Manager.Logout.Implementation
{
    public class LogoutInfoPage : ManagerPage, ILogoutInfoPage
    {
        protected internal string loginActionAddress;
        protected internal DateTime currentTime;

        /// <summary>
        /// 初始化 <see cref="LogoutInfoPage"/> 类的新实例。
        /// </summary>
        /// <param name="source">指定的 HTML 源代码。</param>
        public LogoutInfoPage(string source) : base(source) { }

        /// <summary>
        /// 初始化 <see cref="LogoutInfoPage"/> 类的新实例。
        /// </summary>
        /// <param name="url">网页的 URL 地址。</param>
        /// <param name="encoding">网页 HTML 源代码的编码。</param>
        public LogoutInfoPage(string url, Encoding encoding) : base(url, encoding) { }

        /// <summary>
        /// 初始化 <see cref="LogoutInfoPage"/> 类的新实例。
        /// </summary>
        /// <param name="document">追定的内部 HTML 文档对象。</param>
        public LogoutInfoPage(HtmlDocument document) : base(document) { }

        /// <summary>
        /// 初始化 <see cref="LogoutInfoPage"/> 。
        /// </summary>
        public override void Initialize()
        {
            this.scriptVariants = this.scriptVariants ??
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

            this.currentTime = DateTime.Now;
            this.loginActionAddress = $"{this.scriptVariants["httpBase"]}{this.scriptVariants["ctxPath"]}/portalLogout.wlan?isCloseWindow=N&{Wlan_eduManager.DateTimeToUnixTimeStamp(this.currentTime)}";
        }

        /// <summary>
        /// 登出。
        /// </summary>
        /// <param name="userName">用户名。</param>
        /// <param name="cancelAutoLogin">一个值，指示是否取消自动登入。</param>
        /// <returns></returns>
        public IManagerPage Logout(string userName, bool cancelAutoLogin)
        {
            LogoutInfo logoutInfo = new LogoutInfo()
            {
                wlanAcName = this.wlanAcName,
                wlanUserIp = this.wlanUserIp,
                userName = userName,
                encryUser = string.Format("{0}{1:D15}",
                    this.currentTime.ToString("yyyyMMddHHmmssfff"), // 当前日期时间作为前17位.
                    0 // 后跟15位0，补齐32位。
                )
            };
            string dataStr = logoutInfo.SerializeData();

            string response = this.encoding.GetString(
                HttpRequestUtil.Post(
                    this.loginActionAddress,
                    this.encoding.GetBytes(dataStr)
                )
            );

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(response);

            return new LogoutingPage(doc)
            {
                wlanAcName = this.wlanAcName,
                wlanUserIp = this.wlanUserIp,
                scriptVariants = this.scriptVariants
            };
        }
    }
}
