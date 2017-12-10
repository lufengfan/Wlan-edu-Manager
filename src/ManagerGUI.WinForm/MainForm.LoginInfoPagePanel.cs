using SamLu.Tools.Wlan_edu_Manager.GUI.Controls.WinForm;
using SamLu.Tools.Wlan_edu_Manager.Login.Implementation;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace SamLu.Tools.Wlan_edu_Manager.GUI
{
    partial class MainForm
    {
        private bool txtUserName_ValidationResult = false;
        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^\d{11}$", System.Text.RegularExpressions.RegexOptions.Compiled);

            this.txtUserName_ValidationResult = regex.IsMatch(this.txtUserName.Text);
            if (this.txtUserName_ValidationResult)
            {
                this.errorProvider.Clear();
            }
            else
            {
                if (string.IsNullOrEmpty(this.txtUserName.Text))
                    this.errorProvider.SetError(this.txtUserName, "请输入您的用户名。");
                else
                    this.errorProvider.SetError(this.txtUserName, "请输入您的11位手机号码。");
            }

            this.btnLogin.Enabled = this.txtUserName_ValidationResult && this.txtUserPwd_ValidationResult;
        }

        private bool txtUserPwd_ValidationResult = false;
        private void txtUserPwd_Validating(object sender, CancelEventArgs e)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"^[^\s~'!\$%\^\*\(\)\+<>=\|\\;:,\?/#@&`""\[\]\{\}\.]+$", System.Text.RegularExpressions.RegexOptions.Compiled);

            this.txtUserPwd_ValidationResult = regex.IsMatch(this.txtUserPwd.Text);
            if (this.txtUserPwd_ValidationResult)
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

            this.btnLogin.Enabled = this.txtUserName_ValidationResult && this.txtUserPwd_ValidationResult;
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

        private void btnFetchTemproraryPwd_Click(object sender, EventArgs e)
        {
            ((LoginInfoPage)this.manager.CurrentPage).FetchTemporaryPwd(this.loginInfoPagePanel.UserName);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            this.loginInfoPagePanel.Enabled = false;

            this.statusBar.StatusBarState = StatusBarState.Information;
            this.statusBar.Text = "正在登录……";
        }

    }
}