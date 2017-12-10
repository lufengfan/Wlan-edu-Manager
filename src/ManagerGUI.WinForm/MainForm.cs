using SamLu.Tools.Wlan_edu_Manager.GUI.Controls.WinForm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SamLu.Tools.Wlan_edu_Manager.GUI
{
    public partial class MainForm : Form, IManagerPagePanelContainer, IManagerPageContainer<ManagerPageType>
    {
        internal Wlan_eduManager manager;

        private ICollection<ManagerPagePanel> panels = new Collection<ManagerPagePanel>();

        public ManagerPagePanel CurrentPagePanel { get; private set; }

        public ManagerPageType CurrentPage { get; private set; }

        public MainForm()
        {
            InitializeComponent();
            
            IManagerPagePanelContainer container = this;
            container.Add(this.loginInfoPagePanel);
            container.Add(this.logoutInfoPagePanel);

#if DEBUG
            ((InfoTextBox)this.loginInfoPagePanel.UserNameTextBox).Text = "13735536357";
            ((InfoTextBox)this.loginInfoPagePanel.UserPwdTextBox).Text = "yh89e8w9";
            this.btnLogin.Enabled = true;
#endif

            this.Switch(ManagerPageType.LoginInfo);
        }

        public void Switch(ManagerPagePanel panel)
        {
            if (panel == null) throw new ArgumentNullException(nameof(panel));

            if (this.panels.Contains(panel))
            {
                this.CurrentPagePanel = panel;
                this.CurrentPage = panel.ManagerPageType;
                this.SwitchInternal(panel);
            }
        }

        private void SwitchInternal(ManagerPagePanel panel)
        {
            foreach (var p in this.panels)
            {
                p.Visible = p == panel;
            }
        }

        public bool IsSupport(ManagerPageType page)
        {
            if (Enum.IsDefined(typeof(ManagerPageType), page))
            {
                switch (page)
                {
                    case ManagerPageType.LoginInfo:
                    case ManagerPageType.LoginSucceeded:
                    case ManagerPageType.LogoutInfo:
                    case ManagerPageType.LogoutSucceeded:
                        return true;
                    default:
                        return false;
                }
            }
            else return false;
        }

        public void Switch(ManagerPageType page)
        {
            if (!this.IsSupport(page)) return;

            ManagerPagePanel panel = this.panels.FirstOrDefault(p => p.ManagerPageType == page);
            if (panel != null && this.panels.Contains(panel))
            {
                this.CurrentPagePanel = panel;
                this.CurrentPage = page;
                this.SwitchInternal(panel);
            }
        }

        #region ICollection<ManagerPagePanel> Implementation
        int ICollection<ManagerPagePanel>.Count => throw new NotImplementedException();

        bool ICollection<ManagerPagePanel>.IsReadOnly => throw new NotImplementedException();

        void ICollection<ManagerPagePanel>.Add(ManagerPagePanel item)
        {
            this.panels.Add(item ?? throw new ArgumentNullException(nameof(item)));
        }

        void ICollection<ManagerPagePanel>.Clear()
        {
            this.panels.Clear();
        }

        bool ICollection<ManagerPagePanel>.Contains(ManagerPagePanel item)
        {
            return this.panels.Contains(item);
        }

        void ICollection<ManagerPagePanel>.CopyTo(ManagerPagePanel[] array, int arrayIndex)
        {
            this.panels.CopyTo(array, arrayIndex);
        }

        bool ICollection<ManagerPagePanel>.Remove(ManagerPagePanel item)
        {
            return this.panels.Remove(item ?? throw new ArgumentNullException(nameof(item)));
        }

        IEnumerator<ManagerPagePanel> IEnumerable<ManagerPagePanel>.GetEnumerator()
        {
            return this.panels.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.panels.GetEnumerator();
        }
        #endregion
    }
}
