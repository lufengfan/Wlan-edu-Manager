using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace SamLu.Tools.Wlan_edu_Manager.GUI
{
    public class LightLoginSucceededPagePanel : Controls.WinForm.LoginSucceededPagePanel
    {
        internal string userName;
        [DefaultValue(null)]
        public override string UserName => this.userName;
    }
}
