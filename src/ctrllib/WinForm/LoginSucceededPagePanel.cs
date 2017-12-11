using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SamLu.Tools.Wlan_edu_Manager.GUI.Controls.WinForm
{
    public partial class LoginSucceededPagePanel : LogoutInfoPagePanel
    {
        [DefaultValue(ManagerPageType.LoginSucceeded)]
        public override ManagerPageType ManagerPageType
        {
            get => base.ManagerPageType;
            set => base.ManagerPageType = value;
        }

        public LoginSucceededPagePanel()
        {
            InitializeComponent();
        }
    }
}
