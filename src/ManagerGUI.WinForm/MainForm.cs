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
using SamLu.Tools.Wlan_edu_Manager.Login;
using SamLu.Tools.Wlan_edu_Manager.Logout;
using SamLu.Tools.Wlan_edu_Manager.Login.Implementation;
using SamLu.Tools.Wlan_edu_Manager.Logout.Implementation;

namespace SamLu.Tools.Wlan_edu_Manager.GUI
{
    public partial class MainForm : Form, IManagerPagePanelContainer, IManagerPageContainer<ManagerPageType>
    {
        private ICollection<ManagerPagePanel> panels = new Collection<ManagerPagePanel>();

        public ManagerPagePanel CurrentPagePanel { get; private set; }
        #region CurrentPagePanelChanged
        public event ManagerPagePanelChangedEventHandler CurrentPagePanelChanged;

        protected virtual void OnCurrentPagePanelChanged(ManagerPagePanelChangedEventArgs e) =>
            this.CurrentPagePanelChanged?.Invoke(this, e);
        #endregion

        public ManagerPageType CurrentPage { get; private set; }
        #region CurrentPageChanged
        public event ChangedEventHandler<ManagerPageType> CurrentPageChanged;

        protected virtual void OnCurrentPageChanged(ChangedEventArgs<ManagerPageType> e) =>
            this.CurrentPageChanged?.Invoke(this, e);
        #endregion

        public MainForm(string userName, string userPwd, bool isAutoLogin, bool cancelAutoLogin)
        {
            InitializeComponent();
            this.Icon = Properties.Resources.manager_ico;
            this.notifyIcon.Icon = Properties.Resources.manager_ico;
            this.CurrentPagePanelChanged += this.MainForm_CurrentPagePanelChanged;

            this.common_Initialize(userName, userPwd, isAutoLogin, cancelAutoLogin);
            
            this.Switch(ManagerPageType.LoginInfo);

            if (isAutoLogin)
            {
                typeof(LoginInfoPagePanel)
                    .GetMethod("OnLogin", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                    .Invoke(this.loginInfoPagePanel, new object[] { new LoginEventArgs(userName, userPwd, true, true) });
            }
        }

        public void Switch(ManagerPagePanel panel)
        {
            if (panel == null) throw new ArgumentNullException(nameof(panel));

            ManagerPagePanel currentPagePanel = this.CurrentPagePanel;

            if (this.panels.Contains(panel))
            {
                panel.Enabled = true;
                if (currentPagePanel == panel) return;

                this.CurrentPagePanel = panel;
                this.OnCurrentPagePanelChanged(new ManagerPagePanelChangedEventArgs(currentPagePanel, panel));

                ManagerPageType currentPage = this.CurrentPage;
                this.CurrentPage = panel.ManagerPageType;
                this.OnCurrentPageChanged(new ChangedEventArgs<ManagerPageType>(currentPage, panel.ManagerPageType));

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

            ManagerPageType currentPage = this.CurrentPage;
            ManagerPagePanel panel = this.panels.FirstOrDefault(p => p.ManagerPageType == page);
            if (panel != null && this.panels.Contains(panel))
            {
                panel.Enabled = true;
                if (currentPage == page) return;

                ManagerPagePanel currentPagePanel = this.CurrentPagePanel;
                this.CurrentPagePanel = panel;
                this.OnCurrentPagePanelChanged(new ManagerPagePanelChangedEventArgs(currentPagePanel, panel));

                this.CurrentPage = page;
                this.OnCurrentPageChanged(new ChangedEventArgs<ManagerPageType>(currentPage, page));

                this.SwitchInternal(panel);
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            TimeSpan timeSpan = DateTime.Now - ((ILoginSucceededPage)Program.manager.CurrentPage).SucceededTime;
            this.lblLoginDuration.Text = 
                string.Format("{0:D2} : {1:D2} : {2:D2}",
                    (int)Math.Floor(timeSpan.TotalHours),
                    timeSpan.Minutes,
                    timeSpan.Seconds
                );

            this.notifyIcon.Text = string.Format("用户({0})登陆成功{2}登录时长：>{1}",
                this.loginSucceededPagePanel.UserName,
                string.Format("{0:D2}:{1:D2}:{2:D2}",
                    (int)Math.Floor(timeSpan.TotalHours),
                    timeSpan.Minutes,
                    timeSpan.Seconds
                ),
                Environment.NewLine
            );
        }

        private void notifyIcon_Click(object sender, EventArgs e)
        {
            this.Show();
            this.ShowInTaskbar = true;
        }

        private void cmsNotifyIcon_tsmiLogin_Click(object sender, EventArgs e)
        {
            this.Switch(this.loginInfoPagePanel);

            ManagerPage currentPage = (ManagerPage)Program.manager.CurrentPage;
            if (!(currentPage is ILoginInfoPage))
            {
                Program.manager.NextPage((page, _e) =>
                {
                    DateTime currentTime = DateTime.Now;
                    var scriptVariants = currentPage.scriptVariants;
                    return new LoginInfoPage(string.Empty)
                    {
                        wlanAcName = currentPage.wlanAcName,
                        wlanUserIp = currentPage.wlanUserIp,
                        scriptVariants = currentPage.scriptVariants,
                        currentTime = currentTime,
                        loginActionAddress = $"{scriptVariants["httpBase"]}{scriptVariants["ctxPath"]}/portalLogin.wlan?{Wlan_eduManager.GetMiliseconds(currentTime)}",
                        fetchTemporaryPwdAddress = $"{scriptVariants["httpBase"]}{scriptVariants["ctxPath"]}/portalApplyPwd.wlan"
                    };
                });
            }
        }

        private void cmsNotifyIcon_tsmiLogout_Click(object sender, EventArgs e)
        {
            this.Switch(this.logoutInfoPagePanel);

            ManagerPage currentPage = (ManagerPage)Program.manager.CurrentPage;
            if (!(currentPage is ILogoutInfoPage))
            {
                Program.manager.NextPage((page, _e) => {
                    DateTime currentTime = DateTime.Now;
                    var scriptVariants = currentPage.scriptVariants;
                    return new LogoutInfoPage(string.Empty)
                    {
                        wlanAcName = currentPage.wlanAcName,
                        wlanUserIp = currentPage.wlanUserIp,
                        scriptVariants = currentPage.scriptVariants,
                        currentTime = currentTime,
                        loginActionAddress = $"{scriptVariants["httpBase"]}{scriptVariants["ctxPath"]}/portalLogout.wlan?isCloseWindow=N&{Wlan_eduManager.GetMiliseconds(currentTime)}"
                    };
                });
            }
        }

        private void MainForm_CurrentPagePanelChanged(object sender, ManagerPagePanelChangedEventArgs e)
        {
            if (e.OldValue != null)
            {
                if (e.OldValue == this.loginSucceededPagePanel)
                {
                    this.timer.Stop();
                }
            }

            if (e.NewValue != null)
            {
                if (e.NewValue == this.loginSucceededPagePanel)
                {
                    this.cmsNotifyIcon_tsmiLogin.Enabled = false;
                    this.cmsNotifyIcon_tsmiLogout.Enabled = false;
                    this.cmsNotifyIcon_tsmiLogin.Checked = false;
                    this.cmsNotifyIcon_tsmiLogout.Checked = false;


                    #region 更新登录信息
                    var dic = CommandLine.Console.AccountDictionary;
                    CommandLine.Console._accounts._account account;
                    if (dic.ContainsKey(this.loginInfoPagePanel.UserName))
                    {
                        account = dic[this.loginInfoPagePanel.UserName];
                        if (this.loginInfoPagePanel.RememberMe)
                        {
                            if (this.loginInfoPagePanel.AutoLogin)
                            {
                                foreach (var _account in CommandLine.Console.Accounts.accounts)
                                {
                                    if (_account.autologin)
                                        _account.autologin = false;
                                }
                            }
                        }
                        else
                            dic.Remove(this.loginInfoPagePanel.UserName);
                    }
                    else
                        account = new CommandLine.Console._accounts._account();

                    account.username = this.loginInfoPagePanel.UserName;
                    account.userpwd = this.loginInfoPagePanel.UserPwd;
                    account.rememberme = this.loginInfoPagePanel.RememberMe;
                    account.autologin = this.loginInfoPagePanel.AutoLogin;
                    if (this.loginInfoPagePanel.RememberMe && !dic.ContainsKey(this.loginInfoPagePanel.UserName))
                        dic.Add(this.loginInfoPagePanel.UserName, account);

                    CommandLine.Console.SaveConfig();
                    #endregion


                    this.loginSucceededPagePanel.userName = this.loginInfoPagePanel.UserName;

                    this.statusBar.ShowStatus(5000, $"用户 {this.loginSucceededPagePanel.UserName} 成功登录。", this.Invoke);

                    this.timer.Start();
                    var wlanInfoLines = ((ILoginSucceededPage)Program.manager.CurrentPage).WlanInfos
                            .Select(pair => $"{pair.Key}：{pair.Value}");
                    
                    this.lblWlanInfos.Text = string.Join(Environment.NewLine, wlanInfoLines.ToArray());
                }
                else
                {
                    this.cmsNotifyIcon_tsmiLogin.Enabled = true;
                    this.cmsNotifyIcon_tsmiLogout.Enabled = true;

                    if (e.NewValue == this.loginInfoPagePanel)
                    {
                        this.cmsNotifyIcon_tsmiLogout.Checked = false;
                        this.cmsNotifyIcon_tsmiLogin.Checked = true;

                        this.notifyIcon.Text = "Wlan-edu 登录";
                    }
                    else if (e.NewValue == this.logoutInfoPagePanel)
                    {
                        this.cmsNotifyIcon_tsmiLogin.Checked = false;
                        this.cmsNotifyIcon_tsmiLogout.Checked = true;

                        this.notifyIcon.Text = "Wlan-edu 下线";
                    }
                    this.statusBar.StatusBarState = StatusBarState.None;
                }
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
    }
}
