using SamLu.Tools.Wlan_edu_Manager.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SamLu.Tools.Wlan_edu_Manager.Implementation.Components
{
    public class LoginInfoPageComponent : ManagerPageComponent, ILoginInfoPage
    {
        public LoginInfoPageComponent(Wlan_eduManagerComponent manager) : base(manager) { }

        public override void Initialize()
        {
            throw new NotImplementedException();
        }

        public void FetchTemporaryPwd(string userName)
        {
            throw new NotImplementedException();
        }

        public IManagerPage Login(string userName, string userPwd, bool autoLogin)
        {
            throw new NotImplementedException();
        }
    }
}
