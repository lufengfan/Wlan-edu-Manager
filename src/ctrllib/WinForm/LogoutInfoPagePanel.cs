﻿using System;
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
    public partial class LogoutInfoPagePanel : ManagerPagePanel
    {
        [DefaultValue(ManagerPageType.LogoutInfo)]
        public override ManagerPageType ManagerPageType
        {
            get => base.ManagerPageType;
            set => base.ManagerPageType = value;
        }

        [Browsable(false)]
        public virtual string UserName => this.UserNameTextBox?.Text ?? string.Empty;
        [Browsable(false)]
        public virtual bool CancelAutoLogin => this.CancelAutoLoginCheckBox?.Checked ?? false;

        [DefaultValue(null)]
        [Category("功能")]
        [Description("设置填写用户名的文本框。")]
        public TextBox UserNameTextBox { get; set; }

        [DefaultValue(null)]
        [Category("功能")]
        [Description("设置指示是否取消自动登录的单选框。")]
        public CheckBox CancelAutoLoginCheckBox { get; set; }

        private Button logoutButton;
        [DefaultValue(null)]
        [Category("功能")]
        [Description("设置登录的按钮。")]
        public Button LogoutButton
        {
            get => this.logoutButton;
            set
            {
                if (this.logoutButton != null)
                {
                    this.logoutButton.Click -= this.LogoutButton_Click;
                }

                this.logoutButton = value;
                if (value != null)
                {
                    value.Click += this.LogoutButton_Click;
                }
            }
        }

        private void LogoutButton_Click(object sender, EventArgs e)
        {
            this.OnLogout(new LogoutEventArgs(
                this.UserName,
                this.CancelAutoLogin
            ));
        }

        protected internal virtual void OnLogout(LogoutEventArgs e) =>
            this.Logout?.Invoke(this, e);

        [Description("表示登出的事件。")]
        public event LogoutEventHandler Logout;

        public LogoutInfoPagePanel()
        {
            InitializeComponent();
        }
    }
}
