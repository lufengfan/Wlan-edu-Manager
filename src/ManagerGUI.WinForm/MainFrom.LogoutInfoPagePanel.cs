using SamLu.Tools.Wlan_edu_Manager.GUI.Controls.WinForm;
using System;

namespace SamLu.Tools.Wlan_edu_Manager.GUI
{
	partial class MainForm
    {
        private void logoutInfo_Initialize()
        {
            this.logoutInfo_txtUserName.Clear();
            this.logoutInfo_txtUserName.Tag = this.logoutInfo_btnLogout;
        }

    }
}