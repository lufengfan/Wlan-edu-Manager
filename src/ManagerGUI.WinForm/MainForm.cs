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
            this.Icon = Properties.Resources.manager_ico;
            this.notifyIcon.Icon = Properties.Resources.manager_ico;
            
            this.common_Initialize();

#if DEBUG
            this.loginInfo_txtUserName.Text = "13735536357";
            this.txtUserPwd.Text = "yh89e8w9";
            this.btnLogin.Enabled = true;
#endif

            this.Switch(ManagerPageType.LoginInfo);
        }

        public void Switch(ManagerPagePanel panel)
        {
            if (panel == null) throw new ArgumentNullException(nameof(panel));

            panel.Enabled = true;
            if (this.CurrentPagePanel == panel) return;

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

            if (this.CurrentPage == page) return;

            ManagerPagePanel panel = this.panels.FirstOrDefault(p => p.ManagerPageType == page);
            if (panel != null && this.panels.Contains(panel))
            {
                this.CurrentPagePanel = panel;
                this.CurrentPage = page;
                this.SwitchInternal(panel);
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.notifyIcon.Visible = false;
        }

        #region 最小化到托盘功能
        protected override void WndProc(ref Message m)
        {
            const int WM_SYSCOMMAND = 0x112;
            const int SC_CLOSE = 0xF060;
            const int SC_MINIMIZE = 0xF020;
            const int SC_MAXIMIZE = 0xF030;

            if (m.Msg == WM_SYSCOMMAND)
            {
                if (m.WParam.ToInt32() == SC_MINIMIZE)
                {
                    this.ShowInTaskbar = false;
                    this.Hide();
                }
            }
            
            base.WndProc(ref m);
        }
        #endregion

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

        private void notifyIcon_Click(object sender, EventArgs e)
        {
            this.Show();
            this.ShowInTaskbar = true;
        }

        private void cmsNotifyIcon_tsmiLogin_Click(object sender, EventArgs e)
        {
            this.cmsNotifyIcon_tsmiLogout.Checked = false;
            this.Switch(this.loginInfoPagePanel);
        }

        private void cmsNotifyIcon_tsmiLogout_Click(object sender, EventArgs e)
        {
            this.cmsNotifyIcon_tsmiLogin.Checked = false;
            this.Switch(this.logoutInfoPagePanel);
        }
    }
}
