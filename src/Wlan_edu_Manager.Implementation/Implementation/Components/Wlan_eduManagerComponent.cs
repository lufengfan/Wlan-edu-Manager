using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SamLu.Tools.Wlan_edu_Manager.Login;
using SamLu.Tools.Wlan_edu_Manager.Logout;
using System.Net;
using System.Net.Sockets;

namespace SamLu.Tools.Wlan_edu_Manager.Implementation.Components
{
    public class Wlan_eduManagerComponent : Wlan_eduManager
    {
        public string WlanAcName { get; protected set; }
        public string WlanUserIp { get; protected set; }

        public static readonly string DefaultWlanAcName;
        public static readonly string DefaultWlanUserIp;
        public static readonly Encoding DefaultEncoding = Encoding.UTF8;
        public static readonly IDictionary<string, object> DefaultScriptVariants;

        static Wlan_eduManagerComponent()
        {
            Wlan_eduManagerComponent.DefaultWlanAcName = "0434.0571.571.00";

            string wlanUserIp = null;
            try
            {
                string HostName = Dns.GetHostName(); //得到主机名
                IPHostEntry IpEntry = Dns.GetHostEntry(HostName);

                //从IP地址列表中筛选出IPv4类型的IP地址
                //AddressFamily.InterNetwork表示此IP为IPv4,
                //AddressFamily.InterNetworkV6表示此地址为IPv6类型
                var ipv4s = IpEntry.AddressList.Where(address => address.AddressFamily == AddressFamily.InterNetwork);

                wlanUserIp = ipv4s.FirstOrDefault()?.ToString();
            }
            catch (Exception)
            {
                wlanUserIp = null;
            }
            finally
            {
                Wlan_eduManagerComponent.DefaultWlanUserIp = wlanUserIp;
            }

            var ctxPath = "/zmcc";
            var httpBase = "https://211.138.125.52:7090";
            var httpsBase = "https://211.138.125.52:7090";
            var vcUrl = "/zmcc/generateVerifyCode.wlan";
            var vys = 6;
            var selfServiceLink = "http://www.zj.10086.cn";
            var downloadClientLink = "http://wlan.10086.cn/download";
            var hotspotLink = "http://wlan.10086.cn";
            Wlan_eduManagerComponent.DefaultScriptVariants = new Dictionary<string, object>()
            {
                { nameof(ctxPath), ctxPath },
                { nameof(httpBase), httpBase },
                { nameof(httpsBase), httpsBase },
                { nameof(vcUrl), vcUrl },
                { nameof(vys), vys },
                { nameof(selfServiceLink), selfServiceLink },
                { nameof(downloadClientLink), downloadClientLink },
                { nameof(hotspotLink), hotspotLink }
            };
        }

        protected Wlan_eduManagerComponent() : base() { }

        public Wlan_eduManagerComponent(IManagerPage firstPage) : base(firstPage){ }

        public Wlan_eduManagerComponent(string wlanAcName, string wlanUserIp)
        {
            this.WlanAcName = wlanAcName ?? throw new ArgumentNullException(nameof(wlanAcName));
            this.WlanUserIp = wlanUserIp ?? throw new ArgumentNullException(nameof(wlanUserIp));
        }

        public override ILoginInfoPage CreateLoginInfoPage()
        {
            return new LoginInfoPageComponent(this);
        }

        public override ILoginInfoPage CreateLoginInfoPage(string wlanAcName, string wlanUserIp)
        {
            return new LoginInfoPageComponent(new Wlan_eduManagerComponent(
                wlanAcName ?? throw new ArgumentNullException(nameof(wlanAcName)),
                wlanUserIp ?? throw new ArgumentNullException(nameof(wlanUserIp))
            ));
        }

        public override ILogoutInfoPage CreateLogoutInfoPage()
        {
            return new LogoutInfoPageComponent(this);
        }

        public override ILogoutInfoPage CreateLogoutInfoPage(string wlanAcName, string wlanUserIp)
        {
            return new LogoutInfoPageComponent(new Wlan_eduManagerComponent(
                wlanAcName ?? throw new ArgumentNullException(nameof(wlanAcName)),
                wlanUserIp ?? throw new ArgumentNullException(nameof(wlanUserIp))
            ));
        }
    }
}
