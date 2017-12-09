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
        [DefaultValue(null)]
        [Category("功能")]
        [Description("设置填写用户名的文本框。")]
        public TextBox UserNameTextBox { get; set; }

        [DefaultValue(null)]
        [Category("功能")]
        [Description("设置填写用户密码的文本框。")]
        public TextBox UserPwdTextBox { get; set; }

        [DefaultValue(null)]
        [Category("功能")]
        [Description("设置获取临时密码的按钮。")]
        public Button FetchTemporaryPwdButton { get; set; }

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
            this.Login?.Invoke(this, new LoginEventArgs(
                this.UserNameTextBox?.Text,
                this.UserPwdTextBox?.Text,
                this.RememberMeCheckBox?.Checked ?? false,
                this.AutoLoginCheckBox?.Checked ?? false
            ));
        }
        
        [Description("表示获取临时密码的事件。")]
        public event FetchTemporaryPwdEventHandler FetchTemporaryPwd;

        [Description("表示登录的事件。")]
        public event LoginEventHandler Login;

        public LoginInfoPagePanel() : base()
        {
            InitializeComponent();
        }
    }
}
