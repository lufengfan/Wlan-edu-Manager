using HtmlAgilityPack;
using SamLu.Tools.Wlan_edu_Manager.Login;
using SamLu.Tools.Wlan_edu_Manager.Login.Implementation;
using SamLu.Tools.Wlan_edu_Manager.Logout;
using SamLu.Tools.Wlan_edu_Manager.Logout.Implementation;
using SamLu.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SamLu.Tools.Wlan_edu_Manager.Implementation
{
    public class Wlan_eduManager : IWlan_eduManager
    {
        protected IManagerPage page;

        public IManagerPage CurrentPage => this.page;

        protected Wlan_eduManager() { }

        public Wlan_eduManager(IManagerPage firstPage)
        {
            this.page = firstPage ?? throw new ArgumentNullException(nameof(firstPage));
        }

        public Wlan_eduManager(string wlanAcName, string wlanUserIp, string url) : this(wlanAcName, wlanUserIp, url, Encoding.UTF8) { }

        public Wlan_eduManager(string wlanAcName, string wlanUserIp, string url, Encoding encoding)
        {
            if (wlanAcName == null) throw new ArgumentNullException(nameof(wlanAcName));
            if (wlanUserIp == null) throw new ArgumentNullException(nameof(wlanUserIp));

            this.page = this.CreateLoginInfoPageInternal(wlanAcName, wlanUserIp, url, encoding);
            this.page.Initialize();
        }

        public virtual ILoginInfoPage CreateLoginInfoPage()
        {
            return this.CreateLoginInfoPageInternal(
                Components.Wlan_eduManagerComponent.DefaultWlanAcName,
                Components.Wlan_eduManagerComponent.DefaultWlanUserIp,
                Encoding.UTF8
            );
        }

        public virtual ILoginInfoPage CreateLoginInfoPage(string wlanAcName, string wlanUserIp)
        {
            if (wlanAcName == null) throw new ArgumentNullException(nameof(wlanAcName));
            if (wlanUserIp == null) throw new ArgumentNullException(nameof(wlanUserIp));

            return this.CreateLoginInfoPageInternal(wlanAcName, wlanUserIp, Encoding.UTF8);
        }

        protected virtual ILoginInfoPage CreateLoginInfoPageInternal(string wlanAcName, string wlanUserIp, Encoding encoding)
        {
            IDictionary<string, object> scriptVariants = Components.Wlan_eduManagerComponent.DefaultScriptVariants;
            var query = new
            {
                wlanacname = wlanAcName,
                wlanacip = "",
                wlanuserip = wlanUserIp,
                ssid = ""
            };
            return this.CreateLoginInfoPageInternal(
                wlanAcName,
                wlanUserIp,
                $"{scriptVariants["httpBase"]}{scriptVariants["ctxPath"]}/index.wlan?{query.SerializeData()}",
                encoding
            );
        }

        protected virtual ILoginInfoPage CreateLoginInfoPageInternal(string wlanAcName, string wlanUserIp, string url,  Encoding encoding)
        {
            return new LoginInfoPage(url, encoding)
            {
                wlanAcName = wlanAcName,
                wlanUserIp = wlanUserIp,
                scriptVariants = Components.Wlan_eduManagerComponent.DefaultScriptVariants
            };
        }
        
        public virtual ILogoutInfoPage CreateLogoutInfoPage()
        {
            return this.CreateLogoutInfoPageInternal(
                   Components.Wlan_eduManagerComponent.DefaultWlanAcName,
                   Components.Wlan_eduManagerComponent.DefaultWlanUserIp,
                   Encoding.UTF8
               );
        }

        public virtual ILogoutInfoPage CreateLogoutInfoPage(string wlanAcName, string wlanUserIp)
        {
            if (wlanAcName == null) throw new ArgumentNullException(nameof(wlanAcName));
            if (wlanUserIp == null) throw new ArgumentNullException(nameof(wlanUserIp));

            return this.CreateLogoutInfoPageInternal(wlanAcName, wlanUserIp, Encoding.UTF8);
        }

        protected virtual ILogoutInfoPage CreateLogoutInfoPageInternal(string wlanAcName, string wlanUserIp, Encoding encoding)
        {
            IDictionary<string, object> scriptVariants = Components.Wlan_eduManagerComponent.DefaultScriptVariants;
            
            var query = new
            {
                wlanacname = wlanAcName,
                wlanacip = "",
                wlanuserip = wlanUserIp,
                ssid = ""
            };
            return this.CreateLogoutInfoPageInternal(
                wlanAcName,
                wlanUserIp,
                $"{scriptVariants["httpBase"]}{scriptVariants["ctxPath"]}/indexForce.wlan?{query.SerializeData()}{Wlan_eduManager.DateTimeToUnixTimeStamp(DateTime.Now)}",
                encoding
            );
        }

        protected virtual ILogoutInfoPage CreateLogoutInfoPageInternal(string wlanAcName, string wlanUserIp, string url, Encoding encoding)
        {
            return new LogoutInfoPage(url, encoding)
            {
                wlanAcName = wlanAcName,
                wlanUserIp = wlanUserIp,
                scriptVariants = Components.Wlan_eduManagerComponent.DefaultScriptVariants,
                currentTime = DateTime.Now
            };
        }

        /// <summary>
        /// 通过网页重定向获取 <see cref="Wlan_eduManager"/> 实例。
        /// </summary>
        /// <returns>Wlan-edu 管理器实例。</returns>
        public static Wlan_eduManager CreateManagerFromRedirection()
        {
            string url;
            Encoding encoding = Encoding.UTF8;
            HtmlDocument document;

            // 通过重定向获取登录地址。
            url = "http://1.1.1.1/";
            document = new HtmlDocument();
            document.Load(HttpRequestUtil.GetHtmlContentStreamReader(url, encoding));
            string refresh_content = document.DocumentNode.SelectSingleNode(@"html/head/meta[@http-equiv='refresh']")?.GetAttributeValue("content", null);
            ;

            // 获取登录地址。
            url = Regex.Match(refresh_content.Split(';')[1], @"url=(?<Url>\S*)").Groups["Url"].Value;
            var queryArgs =
                Regex.Matches(
                    new Uri(url, UriKind.Absolute).Query,
                    @"(?<=^\?|&)(?<Key>\w+?)=(?<Value>\S*?)(?=&|$)"
                )
                .OfType<Match>()
                .ToDictionary(
                    (match => match.Groups["Key"].Value.ToLower()),
                    (match => match.Groups["Value"].Value)
                );
            string wlanAcName = queryArgs["wlanacname"];
            string wlanUserIp = queryArgs["wlanuserip"];
            ;

            return new Wlan_eduManager(wlanAcName, wlanUserIp, url, encoding);
        }
        
        /// <summary>
        /// 使管理器切换到下一页。
        /// </summary>
        /// <param name="changePage">页面切换的方法。</param>
        /// <param name="callback">页面切换成功的回调方法。</param>
        /// <returns>一个值，指示操作是否成功。</returns>
        public virtual bool NextPage(ChangePageHandler changePage, CallbackHandler callback = null)
        {
            if (changePage == null) throw new ArgumentNullException(nameof(changePage));

            CancelArgs e = new CancelArgs();
            this.page = changePage(this.page, e);
            if (!e.Cancel)
            {
                this.page.Initialize();
                callback?.Invoke(this.page, e);

                return !e.Cancel;
            }
            else return false;
        }

        /// <summary>
        /// 将指定的 <see cref="DateTime"/> 对象转换为 Unix 时间戳。
        /// </summary>
        /// <param name="dateTime">指定的时间对象。</param>
        /// <returns>由指定的 <see cref="DateTime"/> 对象转换的 Unix 时间戳。</returns>
        protected internal static ulong DateTimeToUnixTimeStamp(DateTime dateTime)
        {
            TimeSpan span = dateTime - TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            
            return (ulong)span.TotalSeconds;
        }

        /// <summary>
        /// 将指定的 Unix 时间戳转换为 <see cref="DateTime"/> 对象。
        /// </summary>
        /// <param name="unixTimeStamp">指定的 Unix 时间戳。</param>
        /// <returns>由指定的 Unix 时间戳转换的 <see cref="DateTime"/> 对象。</returns>
        protected internal static DateTime UnixTimeStampToDateTime(ulong unixTimeStamp)
        {
            TimeSpan span = TimeSpan.FromSeconds(unixTimeStamp);

            return TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1)) + span;
        }
    }
}
