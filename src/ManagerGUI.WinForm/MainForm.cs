﻿using SamLu.Tools.Wlan_edu_Manager.GUI.Controls.WinForm;
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
            this.CurrentPageChanged += this.MainForm_CurrentPageChanged;

            this.common_Initialize(userName, userPwd, isAutoLogin, cancelAutoLogin);
            
            this.Switch(ManagerPageType.LoginInfo);

            if (isAutoLogin)
            {
                this.loginInfoPagePanel.OnLogin(new LoginEventArgs(userName, userPwd, true, true));
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
            ManagerPagePanel panel = this.panels.FirstOrDefault(p => {
                if (page == ManagerPageType.LogoutSucceeded)
                    return p.ManagerPageType == ManagerPageType.LoginInfo;
                else
                    return p.ManagerPageType == page;
            });
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
            try
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
            catch (Exception) { }
        }
        
        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Show();
                this.ShowInTaskbar = true;
            }
        }

        private void cmsNotifyIcon_tsmiLogin_Click(object sender, EventArgs e)
        {
            this.Switch(this.loginInfoPagePanel);
            
            if (!(Program.manager.CurrentPage is ILoginInfoPage))
            {
                Program.manager.NextPage((page, _e) =>Program.manager.CreateLoginInfoPage());
            }
        }

        private void cmsNotifyIcon_tsmiLogout_Click(object sender, EventArgs e)
        {
            this.Switch(this.logoutInfoPagePanel);
            
            if (!(Program.manager.CurrentPage is ILogoutInfoPage))
            {
                Program.manager.NextPage((page, _e) => Program.manager.CreateLogoutInfoPage());
            }
        }

        private void cmsNotifyIcon_tsmiExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MainForm_CurrentPageChanged(object sender, ChangedEventArgs<ManagerPageType> e)
        {
            if (e.OldValue == ManagerPageType.LoginSucceeded)
            {
                this.timer.Stop();
            }

            if (e.NewValue == ManagerPageType.LogoutSucceeded)
            {
                // 在下线成功时更新用户信息。
                #region 更新用户信息
                var dic = CommandLine.Console.AccountDictionary;
                string userName = null;
                bool cancelAutoLogin = false;
                if (e.OldValue == ManagerPageType.LoginSucceeded)
                {
                    userName = this.loginSucceededPagePanel.UserName;
                    cancelAutoLogin = this.loginSucceededPagePanel.CancelAutoLogin;
                }
                else if (e.OldValue == ManagerPageType.LogoutInfo)
                {
                    userName = this.logoutInfoPagePanel.UserName;
                    cancelAutoLogin = this.logoutInfoPagePanel.CancelAutoLogin;
                }
                if (dic.TryGetValue(userName, out var account))
                {
                    if (cancelAutoLogin)
                        account.autologin = false;
                    CommandLine.Console.SaveConfig();
                }
                #endregion
            }
        }

        private void MainForm_CurrentPagePanelChanged(object sender, ManagerPagePanelChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                if (e.NewValue == this.loginSucceededPagePanel)
                {
                    this.cmsNotifyIcon_tsmiLogin.Enabled = false;
                    this.cmsNotifyIcon_tsmiLogout.Enabled = false;
                    this.cmsNotifyIcon_tsmiLogin.Checked = false;
                    this.cmsNotifyIcon_tsmiLogout.Checked = false;


                    // 在登录成功时更新用户信息。
                    #region 更新用户信息
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
                    if (this.loginInfoPagePanel.AutoLogin)
                    {
                        this.loginSucceeded_cbCancelAutoLogin.Enabled = true;
                    }
                    else
                    {
                        this.loginSucceeded_cbCancelAutoLogin.Enabled = false;
                        this.loginSucceeded_cbCancelAutoLogin.Checked = false;
                    }

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

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.CurrentPage == ManagerPageType.LoginSucceeded)
            {
                var panel = (LoginSucceededPagePanel)this.CurrentPagePanel;
                switch (
                    MessageBox.Show(
                        $"用户{panel.UserName}处于登录状态，是否强制下线？{Environment.NewLine}\t是——强制下线{Environment.NewLine}\t否——正常下线{Environment.NewLine}\t取消——取消下线", "退出",
                        MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.Warning,
                        MessageBoxDefaultButton.Button2)
                )
                {
                    case DialogResult.Yes:
                        e.Cancel = false;
                        break;
                    case DialogResult.No:
                        try
                        {
                            panel.OnLogout(new LogoutEventArgs(panel.UserName, panel.CancelAutoLogin));
                        }
                        catch (Exception)
                        {
                            new System.Threading.Thread(() =>
                                MessageBox.Show("下线过程中发生错误。", "下线", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
                            )
                            { IsBackground = true }.Start();
                        }
                        e.Cancel = false;
                        break;
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
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
