using SamLu.Tools.Wlan_edu_Manager.GUI.Controls.WinForm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

        public MainForm()
        {
            InitializeComponent();
            
            IManagerPagePanelContainer container = this;
            container.Add(this.loginInfoPagePanel);

            this.Switch(ManagerPageType.LoginInfo);
        }

        public void Switch(ManagerPagePanel panel)
        {
            if (!this.panels.Contains(panel)) return;

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
            if (panel != null)
                this.Switch(panel);
        }

        #region loginInfoPagePanel
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

        private void btnLogin_Click(object sender, EventArgs e)
        {
            this.loginInfoPagePanel.Enabled = false;

            this.statusBar.StatusBarState = StatusBarState.Information;
            this.statusBar.Text = "正在登录……";
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
