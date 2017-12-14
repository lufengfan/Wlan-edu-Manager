using SamLu.Tools.Wlan_edu_Manager.Login;
using SamLu.Tools.Wlan_edu_Manager.Logout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SamLu.Tools.Wlan_edu_Manager
{
    public interface IWlan_eduManager
    {
        IManagerPage CurrentPage { get; }

        ILoginInfoPage CreateLoginInfoPage();
        ILoginInfoPage CreateLoginInfoPage(string wlanAcName, string wlanUserIp);

        ILogoutInfoPage CreateLogoutInfoPage();
        ILogoutInfoPage CreateLogoutInfoPage(string wlanAcName, string wlanUserIp);

        bool NextPage(ChangePageHandler changePage, CallbackHandler callback = null);
    }

    public delegate IManagerPage ChangePageHandler(IManagerPage page, CancelArgs e);
    public delegate void CallbackHandler(IManagerPage page, CancelArgs e);
}
