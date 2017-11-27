using HtmlAgilityPack;
using SamLu.Native.Wifi;
using SamLu.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace SamLu.Tools.Wlan_edu_Manager.Login.Implementation
{
    public class LoginImplementation : ILogin, ILogin2
    {
        public const string SSID_Wlan_edu = "Wlan-edu";

        protected string wlanAcName;
        protected string wlanUserIp;
        protected string loginActionAddress;

        public LoginImplementation() { }

        public void Initialize()
        {
            WifiWatcher.GetNativeWifi(out WIFISSID currentSsid, out WIFISSID[] ssids);
            ;
            if (currentSsid == null || currentSsid.SSID != SSID_Wlan_edu)
                throw new Wlan_eduNotConnectedException();

            string url;
            Encoding encoding = Encoding.UTF8;
            HtmlDocument document;

            // 通过重定向获取登录地址。
            url = "http://1.1.1.1/";
            document = new HtmlDocument();
            document.Load(HttpRequestUtil.GetHtmlContentStreamReader(url, encoding));
            string refresh_content = document.DocumentNode.SelectSingleNode(@"html/head/meta[@http-equiv='refresh']")?.GetAttributeValue("content", null);
            
            // 获取登陆地址。
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
            this.wlanAcName = queryArgs["wlanacname"];
            this.wlanUserIp = queryArgs["wlanuserip"];

            document.Load(HttpRequestUtil.GetHtmlContentStreamReader(url, encoding));
            this.loginActionAddress = document.GetElementbyId("Wlan_Login")?.GetAttributeValue("action", null);
            ;
        }

        public string Login<TLoginInfo>(TLoginInfo loginInfo)
        {
            if (loginInfo == null) throw new ArgumentNullException(nameof(loginInfo));

            string dataStr = loginInfo.SerializeData();

            //return null;

            byte[] response_data = HttpRequestUtil.Post(
                this.loginActionAddress,
                Encoding.UTF8.GetBytes(dataStr)
            );

            return Encoding.UTF8.GetString(response_data);
        }

        public string Login(string userName, string userPwd, bool autoLogin)
        {
            LoginInfo loginInfo = new LoginInfo()
            {
                wlanAcName = this.wlanAcName,
                wlanUserIp = this.wlanUserIp,
                userName = userName,
                userPwd = userPwd,
                idissaveinfo = autoLogin ? LoginInfo.IsSaveInfo.Save : LoginInfo.IsSaveInfo.NotSave
            };

            return this.Login(loginInfo);
        }

        public string Login(string wlanAcName, string wlanAcIp, string wlanUserIp, string ssid, string userName, string userPwd, bool autoLogin)
        {
            LoginInfo loginInfo = new LoginInfo()
            {
                wlanAcName = wlanAcName,
                wlanAcIp = wlanAcIp,
                wlanUserIp = wlanUserIp,
                ssid = ssid,
                userName = userName,
                userPwd = userPwd,
                idissaveinfo = autoLogin ? LoginInfo.IsSaveInfo.Save : LoginInfo.IsSaveInfo.NotSave
            };

            return this.Login(loginInfo);
        }
    }
}
