using SamLu.Tools.Wlan_edu_Manager.Logout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SamLu.Tools.Wlan_edu_Manager.Implementation.Components
{
    public class LogoutInfoPageComponent : ManagerPageComponent, ILogoutInfoPage
    {
        public LogoutInfoPageComponent(Wlan_eduManagerComponent manager) : base(manager) { }

        public override void Initialize()
        {
            throw new NotImplementedException();
        }

        public IManagerPage Logout(string userName, bool cancelAutoLogin)
        {
            throw new NotImplementedException();
        }
    }
}
