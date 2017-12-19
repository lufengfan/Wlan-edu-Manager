using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SamLu.Tools.Wlan_edu_Manager.Login;
using SamLu.Tools.Wlan_edu_Manager.Logout;

namespace SamLu.Tools.Wlan_edu_Manager.Implementation.Components
{
    public class Wlan_eduManagerComponent : Wlan_eduManager
    {
        public string WlanAcName { get; protected set; }
        public string WlanUserIp { get; protected set; }

        protected Wlan_eduManagerComponent() : base() { }

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
