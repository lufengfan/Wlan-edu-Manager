using SamLu.Tools.Wlan_edu_Manager.GUI.Controls.WinForm;
using SamLu.Tools.Wlan_edu_Manager.Login;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace SamLu.Tools.Wlan_edu_Manager.GUI
{
    partial class MainForm
    {
        private void loginInfo_Initialize()
        {
            this.loginInfo_txtUserName.Clear();
            this.loginInfo_txtUserName.Tag = this.btnLogin;
            this.txtUserPwd.Clear();
            this.btnLogin.Enabled = false;
        }
        
        private void txtUserPwd_Validating(object sender, CancelEventArgs e)
        {
            if (this.canAcceptUserPwd(this.txtUserPwd.Text))
            {
                this.errorProvider.Clear();
            }
            else
            {
                if (string.IsNullOrEmpty(this.txtUserPwd.Text))
                    this.errorProvider.SetError(this.txtUserPwd, "请输入您的密码。");
                else
                    this.errorProvider.SetError(this.txtUserPwd, "您输入的静态密码中含有非法字符，空格或（~'!$%^*()+<>=|\\;:,?/#@&`\"[]{}.）。");
            }
        }

        private void txtUserPwd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CancelEventArgs _e = new CancelEventArgs();
                this.txtUserPwd_Validating(sender, _e);
                this.CausesValidation = _e.Cancel;

                this.ProcessTabKey(true);
            }
        }

        private void lblPwdImg_MouseDown(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) != 0)
            {
                this.txtUserPwd.UseSystemPasswordChar = false;
                this.lblPwdImg.ImageIndex = 1;
            }
        }

        private void lblPwdImg_MouseUp(object sender, MouseEventArgs e)
        {
            this.txtUserPwd.UseSystemPasswordChar = true;
            this.lblPwdImg.ImageIndex = 0;
        }

        private void lblPwdImg_MouseHover(object sender, EventArgs e)
        {
            this.toolTip.Show(this.lblPwdImg.ImageIndex == 0 ? "显示密码" : "隐藏密码", this.lblPwdImg, 5000);
        }

        private void cbRememberMe_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbRememberMe.Checked == false)
                this.cbAutoLogin.Checked = false;
        }

        private void cbAutoLogin_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbAutoLogin.Checked == true)
                this.cbRememberMe.Checked = true;
        }

        private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            MessageBox.Show(
                "尊敬的客户，如您忘记密码，请发送“CZEDUMM”到10086，重置后的密码将以短信形式下发到你的手机；或拨打当地10086客服重置密码。",
                "忘记密码",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1
            );
        }

        private void loginInfoPagePanel_FetchTemporaryPwd(object sender, FetchTemporaryPwdEventArgs e)
        {
            ((ILoginInfoPage)this.manager.CurrentPage).FetchTemporaryPwd(e.UserName);
        }

        private void login(object sender, LoginEventArgs e)
        {
            if (!this.canAcceptUserInfo()) return;

            this.loginInfoPagePanel.Enabled = false;

            this.statusBar.StatusBarState = StatusBarState.Information;
            this.statusBar.Text = "正在登录……";

            this.manager.NextPage(
                (page, _e) =>
                {
                    var nextPage = ((ILoginInfoPage)page).Login(e.UserName, e.UserPwd, e.AutoLogin);
                    if (nextPage is ILoginForcedPage)
                    {
                        if (MessageBox.Show("您好，您当前登入的用户已在线，是否继续操作？", "登入", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        {
                            nextPage = ((ILoginForcedPage)nextPage).ForceLogin();
                            if (nextPage is ILoginingPage)
                                return nextPage;
                            else if (nextPage is ILoginFailedPage)
                            {
                                MessageBox.Show($"登入认证失败，用户( {e.UserName} )当前处于非正常状态！", "登入", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                                return ((ILoginFailedPage)nextPage).Ignore();
                            }
                            else throw new NotSupportedException();
                        }
                        else
                            return ((ILoginForcedPage)nextPage).Cancel();
                    }
                    else if (nextPage is ILoginingPage)
                    {
                        return ((ILoginingPage)nextPage).Success();
                    }
                    else
                    {
                        _e.Cancel = true;
                        return null;
                    }
                },
                this.manager_Callback
            );
        }
    }
}