using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SamLu.Tools.Wlan_edu_Manager.GUI.Controls.WinForm
{
    public interface IManagerPagePanelContainer : ICollection<ManagerPagePanel>
    {
        void Switch(ManagerPagePanel panel);
    }
}
