using SamLu.Tools.Wlan_edu_Manager.GUI.Controls.WinForm;
using SamLu.Tools.Wlan_edu_Manager.Login;
using SamLu.Tools.Wlan_edu_Manager.Logout;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace SamLu.Tools.Wlan_edu_Manager.GUI
{
    partial class MainForm
    {
        private void common_Initialize(string userName, string userPwd, bool isAutoLogin, bool cancelAutoLogin)
        {
            IManagerPagePanelContainer container = this;
            container.Add(this.loginInfoPagePanel);
            container.Add(this.loginSucceededPagePanel);
            container.Add(this.logoutInfoPagePanel);

            this.cmsNotifyIcon_tsmiLogin.Checked = true;

            this.loginInfo_Initialize(userName, userPwd, isAutoLogin, cancelAutoLogin);
            this.loginSucceeded_Initialize();
            this.logoutInfo_Initialize();
        }
        
        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            InfoTextBox txt = (InfoTextBox)sender;
            
            if (this.canAcceptUserName(txt.Text))
            {
                this.errorProvider.Clear();

                #region 自动填写登录信息
                // 如果设置文件中当前用户的状态为记住密码
                var dic = CommandLine.Console.AccountDictionary;
                CommandLine.Console._accounts._account account = null;
                if (dic.ContainsKey(txt.Text))
                    account = dic[txt.Text];

                if (txt == this.loginInfo_txtUserName)
                {
                    if (account != null)
                    {
                        this.txtUserPwd.Text = account.userpwd;
                        this.cbRememberMe.Checked = account.rememberme;
                        this.cbAutoLogin.Checked = account.autologin;
                    }
                }
                else if (txt == this.logoutInfo_txtUserName)
                {
                    if (account != null && account.autologin)
                    {
                        this.logoutInfo_cbCancelAutoLogin.Enabled = true;
                    }
                    else
                    {
                        this.logoutInfo_cbCancelAutoLogin.Enabled = false;
                        this.logoutInfo_cbCancelAutoLogin.Checked = false;
                    }
                }
                #endregion
            }
            else
            {
                if (string.IsNullOrEmpty(txt.Text))
                    this.errorProvider.SetError(txt, "请输入您的用户名。");
                else
                    this.errorProvider.SetError(txt, "请输入您的11位手机号码。");
            }
        }
        
        private void txtUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CancelEventArgs _e = new CancelEventArgs();
                this.txtUserName_Validating(sender, _e);
                this.CausesValidation = _e.Cancel;

                this.ProcessTabKey(true);
            }
        }
        
        private bool canAcceptUserName(string userName)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^\d{11}$", System.Text.RegularExpressions.RegexOptions.Compiled);
            return regex.IsMatch(userName);
        }

        private bool canAcceptUserPwd(string userPwd)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^[^\s~'!\$%\^\*\(\)\+<>=\|\\;:,\?/#@&`""\[\]\{\}\.]+$", System.Text.RegularExpressions.RegexOptions.Compiled);
            return regex.IsMatch(userPwd);
        }

        private bool canAcceptUserInfo()
        {
            if (this.CurrentPagePanel == this.loginInfoPagePanel)
            {
                return this.canAcceptUserName(this.loginInfoPagePanel.UserName) && this.canAcceptUserPwd(this.loginInfoPagePanel.UserPwd);
            }
            else if (this.CurrentPagePanel == this.logoutInfoPagePanel)
            {
                return this.canAcceptUserName(this.logoutInfoPagePanel.UserName);
            }
            else if (this.CurrentPagePanel == this.loginSucceededPagePanel)
                return true;
            else return false;
        }

        private void logout(object sender, LogoutEventArgs e)
        {
            if (!this.canAcceptUserInfo()) return;

            this.CurrentPagePanel.Enabled = false;

            this.statusBar.ShowStatus("正在下线……", StatusBarState.Information);

            Program.manager.NextPage(
                (page, _e) =>
                {
                    var nextPage = ((ILogoutInfoPage)page).Logout(e.UserName, e.CancelAutoLogin);
                    if (nextPage is ILogoutFailedPage)
                        return ((ILogoutFailedPage)nextPage).Ignore();
                    else if (nextPage is ILogoutingPage)
                        return ((ILogoutingPage)nextPage).Success();
                    else
                    {
                        _e.Cancel = true;
                        return null;
                    }
                },
                this.manager_Callback
            );
        }

        private void manager_Callback(IManagerPage page, CancelArgs _e)
        {
            if (page is ILogoutSucceededPage)
                this.Switch(ManagerPageType.LogoutSucceeded);
            else if (page is ILoginInfoPage)
                this.Switch(ManagerPageType.LoginInfo);
            else if (page is ILoginSucceededPage)
                this.Switch(ManagerPageType.LoginSucceeded);
            else if (page is ILogoutInfoPage)
                this.Switch(ManagerPageType.LogoutInfo);
            else
                _e.Cancel = true;
        }
    }
}