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
    [ToolboxItem(false)]
    public partial class ManagerPagePanel : Panel
    {
        public virtual ManagerPageType ManagerPageType { get; set; }

        protected ManagerPagePanel()
        {
            InitializeComponent();
        }
    }
}
