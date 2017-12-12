using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SamLu.Tools.Wlan_edu_Manager.GUI.Controls.WinForm
{
    public delegate void ManagerPagePanelChangedEventHandler(object sender, ManagerPagePanelChangedEventArgs e);

    public class ManagerPagePanelChangedEventArgs : ChangedEventArgs<ManagerPagePanel>
    {
        public ManagerPagePanelChangedEventArgs(ManagerPagePanel oldValue, ManagerPagePanel newValue) : base(oldValue, newValue) { }
    }
}
