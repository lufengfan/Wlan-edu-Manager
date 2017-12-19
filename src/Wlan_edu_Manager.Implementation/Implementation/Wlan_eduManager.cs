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
        private IManagerPage page;

        public IManagerPage CurrentPage => this.page;

        protected Wlan_eduManager() { }

        public Wlan_eduManager(IManagerPage firstPage)
        {
            this.page = firstPage;
        }

        public Wlan_eduManager(string wlanAcName, string wlanUserIp, string url) : this(wlanAcName, wlanUserIp, url, Encoding.UTF8) { }

        public Wlan_eduManager(string wlanAcName, string wlanUserIp, string url, Encoding encoding)
        {
            this.page = Wlan_eduManager.CreateLoginInfoPageWithoutInitialize(wlanAcName, wlanUserIp, url, encoding);
            this.page.Initialize();
        }

        public virtual ILoginInfoPage CreateLoginInfoPage()
        {
            throw new NotImplementedException();
        }

        public virtual ILoginInfoPage CreateLoginInfoPage(string wlanAcName, string wlanUserIp)
        {
            throw new NotImplementedException();
        }

        public virtual ILogoutInfoPage CreateLogoutInfoPage()
        {
            throw new NotImplementedException();
        }

        public virtual ILogoutInfoPage CreateLogoutInfoPage(string wlanAcName, string wlanUserIp)
        {
            throw new NotImplementedException();
        }

        public static ILoginInfoPage CreateLoginInfoPageWithoutInitialize(string wlanAcName, string wlanUserIp, string url, Encoding encoding)
        {
            return new LoginInfoPage(url, encoding)
            {
                wlanAcName = wlanAcName,
                wlanUserIp = wlanUserIp
            };
        }

        public static ILogoutInfoPage CreateLogoutInfoPageWithoutInitialize(string wlanAcName, string wlanUserIp, string url, Encoding encoding)
        {
            return new LogoutInfoPage(url, encoding)
            {
                wlanAcName = wlanAcName,
                wlanUserIp = wlanUserIp
            };
        }

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

        protected internal static long GetMiliseconds(DateTime dateTime)
        {
            DateTime logout_dt = dateTime;
            TimeSpan span = logout_dt - new DateTime(1970, 1, 1);
            long miliseconds = 1;

            miliseconds *= (long)span.Days * 24 * 60 * 60 * 1000;

            return miliseconds;
        }
    }
}
