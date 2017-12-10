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
    [ToolboxItem(true)]
    public partial class LoginInfoPagePanel : ManagerPagePanel
    {
        [DefaultValue(ManagerPageType.LoginInfo)]
        public override ManagerPageType ManagerPageType
        {
            get => base.ManagerPageType;
            set => base.ManagerPageType = value;
        }

        [Browsable(false)]
        public virtual string UserName => this.UserNameTextBox?.Text ?? string.Empty;
        [Browsable(false)]
        public virtual string UserPwd => this.UserPwdTextBox?.Text ?? string.Empty;
        [Browsable(false)]
        public virtual bool RememberMe => this.RememberMeCheckBox?.Checked ?? false;
        [Browsable(false)]
        public virtual bool AutoLogin => this.AutoLoginCheckBox?.Checked ?? false;
        
        [DefaultValue(null)]
        [Category("功能")]
        [Description("设置填写用户名的文本框。")]
        public TextBox UserNameTextBox { get; set; }

        [DefaultValue(null)]
        [Category("功能")]
        [Description("设置填写用户密码的文本框。")]
        public TextBox UserPwdTextBox { get; set; }

        private Button fetchTemporaryPwdButton;
        [DefaultValue(null)]
        [Category("功能")]
        [Description("设置获取临时密码的按钮。")]
        public Button FetchTemporaryPwdButton
        {
            get => this.fetchTemporaryPwdButton;
            set
            {
                if (this.fetchTemporaryPwdButton != null)
                {
                    this.fetchTemporaryPwdButton.Click -= this.FetchTemporaryPwdButton_Click;
                }

                this.fetchTemporaryPwdButton = value;
                if (value != null)
                {
                    value.Click += this.FetchTemporaryPwdButton_Click;
                }
            }
        }

        private void FetchTemporaryPwdButton_Click(object sender, EventArgs e)
        {
            this.OnFetchTemporaryPwd(new FetchTemporaryPwdEventArgs(this.UserName));
        }

        [DefaultValue(null)]
        [Category("功能")]
        [Description("设置指示是否记住用户密码的单选框。")]
        public CheckBox RememberMeCheckBox { get; set; }

        [DefaultValue(null)]
        [Category("功能")]
        [Description("设置指示是否自动登录的单选框。")]
        public CheckBox AutoLoginCheckBox { get; set; }

        private Button loginButton;
        [DefaultValue(null)]
        [Category("功能")]
        [Description("设置登录的按钮。")]
        public Button LoginButton
        {
            get => this.loginButton;
            set
            {
                if (this.loginButton != null)
                {
                    this.loginButton.Click -= this.LoginButton_Click;
                }

                this.loginButton = value;
                if (value != null)
                {
                    value.Click += this.LoginButton_Click;
                }
            }
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            this.OnLogin(new LoginEventArgs(
                this.UserName,
                this.UserPwd,
                this.RememberMe,
                this.AutoLogin
            ));
        }
        
        [Description("表示获取临时密码的事件。")]
        public event FetchTemporaryPwdEventHandler FetchTemporaryPwd;

        [Description("表示登录的事件。")]
        public event LoginEventHandler Login;

        protected virtual void OnFetchTemporaryPwd(FetchTemporaryPwdEventArgs e) =>
            this.FetchTemporaryPwd?.Invoke(this, e);

        protected virtual void OnLogin(LoginEventArgs e) =>
            this.Login?.Invoke(this, e);

        public LoginInfoPagePanel() : base()
        {
            InitializeComponent();
        }
    }
}
