namespace SamLu.Tools.Wlan_edu_Manager.GUI
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.loginInfoPagePanel = new SamLu.Tools.Wlan_edu_Manager.GUI.Controls.WinForm.LoginInfoPagePanel();
            this.cbAutoLogin = new System.Windows.Forms.CheckBox();
            this.linkLabel = new System.Windows.Forms.LinkLabel();
            this.btnLogin = new System.Windows.Forms.Button();
            this.cbRememberMe = new System.Windows.Forms.CheckBox();
            this.btnFetchTemproraryPwd = new System.Windows.Forms.Button();
            this.lblPwdImg = new System.Windows.Forms.Label();
            this.ilPwdBoxOption = new System.Windows.Forms.ImageList(this.components);
            this.txtUserPwd = new SamLu.Tools.Wlan_edu_Manager.GUI.Controls.WinForm.InfoTextBox();
            this.loginInfo_txtUserName = new SamLu.Tools.Wlan_edu_Manager.GUI.Controls.WinForm.InfoTextBox();
            this.loginInfo_lblInfo = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.logoutInfoPagePanel = new SamLu.Tools.Wlan_edu_Manager.GUI.Controls.WinForm.LogoutInfoPagePanel();
            this.logoutInfo_cbCancelAutoLogin = new System.Windows.Forms.CheckBox();
            this.lblLogoutInfoInfo = new System.Windows.Forms.Label();
            this.logoutInfo_btnLogout = new System.Windows.Forms.Button();
            this.logoutInfo_txtUserName = new SamLu.Tools.Wlan_edu_Manager.GUI.Controls.WinForm.InfoTextBox();
            this.logoutInfo_lblInfo = new System.Windows.Forms.Label();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.cmsNotifyIcon = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsNotifyIcon_tsmiLogin = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsNotifyIcon_tsmiLogout = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsNotifyIcon_tsmiSeperator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cmsNotifyIcon_tsmiSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsNotifyIcon_tsmiAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsNotifyIcon_tsmiSeperator2 = new System.Windows.Forms.ToolStripSeparator();
            this.cmsNotifyIcon_tsmiExit = new System.Windows.Forms.ToolStripMenuItem();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.statusBar = new SamLu.Tools.Wlan_edu_Manager.GUI.Controls.WinForm.StatusBar();
            this.loginSucceededPagePanel = new SamLu.Tools.Wlan_edu_Manager.GUI.LightLoginSucceededPagePanel();
            this.loginSucceeded_cbCancelAutoLogin = new System.Windows.Forms.CheckBox();
            this.lblWlanInfos = new System.Windows.Forms.Label();
            this.lblLoginDuration = new System.Windows.Forms.Label();
            this.loginSucceeded_btnLogout = new System.Windows.Forms.Button();
            this.loginInfoPagePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.logoutInfoPagePanel.SuspendLayout();
            this.cmsNotifyIcon.SuspendLayout();
            this.loginSucceededPagePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // loginInfoPagePanel
            // 
            this.loginInfoPagePanel.AutoLoginCheckBox = this.cbAutoLogin;
            this.loginInfoPagePanel.BackColor = System.Drawing.Color.White;
            this.loginInfoPagePanel.Controls.Add(this.linkLabel);
            this.loginInfoPagePanel.Controls.Add(this.btnLogin);
            this.loginInfoPagePanel.Controls.Add(this.cbAutoLogin);
            this.loginInfoPagePanel.Controls.Add(this.cbRememberMe);
            this.loginInfoPagePanel.Controls.Add(this.btnFetchTemproraryPwd);
            this.loginInfoPagePanel.Controls.Add(this.lblPwdImg);
            this.loginInfoPagePanel.Controls.Add(this.txtUserPwd);
            this.loginInfoPagePanel.Controls.Add(this.loginInfo_txtUserName);
            this.loginInfoPagePanel.Controls.Add(this.loginInfo_lblInfo);
            this.loginInfoPagePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loginInfoPagePanel.FetchTemporaryPwdButton = this.btnFetchTemproraryPwd;
            this.loginInfoPagePanel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.loginInfoPagePanel.Location = new System.Drawing.Point(0, 0);
            this.loginInfoPagePanel.LoginButton = this.btnLogin;
            this.loginInfoPagePanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.loginInfoPagePanel.Name = "loginInfoPagePanel";
            this.loginInfoPagePanel.RememberMeCheckBox = this.cbRememberMe;
            this.loginInfoPagePanel.Size = new System.Drawing.Size(464, 336);
            this.loginInfoPagePanel.TabIndex = 0;
            this.loginInfoPagePanel.UserNameTextBox = this.loginInfo_txtUserName;
            this.loginInfoPagePanel.UserPwdTextBox = this.txtUserPwd;
            this.loginInfoPagePanel.FetchTemporaryPwd += new SamLu.Tools.Wlan_edu_Manager.FetchTemporaryPwdEventHandler(this.loginInfoPagePanel_FetchTemporaryPwd);
            this.loginInfoPagePanel.Login += new SamLu.Tools.Wlan_edu_Manager.LoginEventHandler(this.login);
            // 
            // cbAutoLogin
            // 
            this.cbAutoLogin.AutoSize = true;
            this.cbAutoLogin.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.cbAutoLogin.Location = new System.Drawing.Point(139, 230);
            this.cbAutoLogin.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbAutoLogin.Name = "cbAutoLogin";
            this.cbAutoLogin.Size = new System.Drawing.Size(84, 24);
            this.cbAutoLogin.TabIndex = 5;
            this.cbAutoLogin.Text = "自动登录";
            this.cbAutoLogin.UseVisualStyleBackColor = true;
            this.cbAutoLogin.CheckedChanged += new System.EventHandler(this.cbAutoLogin_CheckedChanged);
            // 
            // linkLabel
            // 
            this.linkLabel.ActiveLinkColor = System.Drawing.Color.Gray;
            this.linkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel.AutoSize = true;
            this.linkLabel.DisabledLinkColor = System.Drawing.Color.Silver;
            this.linkLabel.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.linkLabel.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.linkLabel.Location = new System.Drawing.Point(349, 232);
            this.linkLabel.Name = "linkLabel";
            this.linkLabel.Size = new System.Drawing.Size(65, 20);
            this.linkLabel.TabIndex = 1;
            this.linkLabel.TabStop = true;
            this.linkLabel.Text = "忘记密码";
            this.linkLabel.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.linkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_LinkClicked);
            // 
            // btnLogin
            // 
            this.btnLogin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLogin.BackColor = System.Drawing.Color.SandyBrown;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Location = new System.Drawing.Point(107, 269);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(249, 58);
            this.btnLogin.TabIndex = 0;
            this.btnLogin.Text = "登录";
            this.btnLogin.UseVisualStyleBackColor = false;
            // 
            // cbRememberMe
            // 
            this.cbRememberMe.AutoSize = true;
            this.cbRememberMe.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.cbRememberMe.Location = new System.Drawing.Point(49, 230);
            this.cbRememberMe.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbRememberMe.Name = "cbRememberMe";
            this.cbRememberMe.Size = new System.Drawing.Size(84, 24);
            this.cbRememberMe.TabIndex = 4;
            this.cbRememberMe.Text = "记住密码";
            this.cbRememberMe.UseVisualStyleBackColor = true;
            this.cbRememberMe.CheckedChanged += new System.EventHandler(this.cbRememberMe_CheckedChanged);
            // 
            // btnFetchTemproraryPwd
            // 
            this.btnFetchTemproraryPwd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFetchTemproraryPwd.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.btnFetchTemproraryPwd.Location = new System.Drawing.Point(265, 182);
            this.btnFetchTemproraryPwd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnFetchTemproraryPwd.Name = "btnFetchTemproraryPwd";
            this.btnFetchTemproraryPwd.Size = new System.Drawing.Size(149, 38);
            this.btnFetchTemproraryPwd.TabIndex = 3;
            this.btnFetchTemproraryPwd.Text = "获取临时密码";
            this.btnFetchTemproraryPwd.UseVisualStyleBackColor = true;
            // 
            // lblPwdImg
            // 
            this.lblPwdImg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPwdImg.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lblPwdImg.ImageIndex = 0;
            this.lblPwdImg.ImageList = this.ilPwdBoxOption;
            this.lblPwdImg.Location = new System.Drawing.Point(230, 188);
            this.lblPwdImg.Name = "lblPwdImg";
            this.lblPwdImg.Size = new System.Drawing.Size(29, 29);
            this.lblPwdImg.TabIndex = 5;
            this.lblPwdImg.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblPwdImg_MouseDown);
            this.lblPwdImg.MouseHover += new System.EventHandler(this.lblPwdImg_MouseHover);
            this.lblPwdImg.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lblPwdImg_MouseUp);
            // 
            // ilPwdBoxOption
            // 
            this.ilPwdBoxOption.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilPwdBoxOption.ImageStream")));
            this.ilPwdBoxOption.TransparentColor = System.Drawing.Color.Transparent;
            this.ilPwdBoxOption.Images.SetKeyName(0, "eye_close.png");
            this.ilPwdBoxOption.Images.SetKeyName(1, "eye_open.png");
            // 
            // txtUserPwd
            // 
            this.txtUserPwd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUserPwd.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.txtUserPwd.ForeColor = System.Drawing.SystemColors.WindowText;
            this.errorProvider.SetIconAlignment(this.txtUserPwd, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
            this.txtUserPwd.InfoBackColor = System.Drawing.SystemColors.Window;
            this.txtUserPwd.InfoForeColor = System.Drawing.SystemColors.GrayText;
            this.txtUserPwd.InfoText = "输入固定密码/临时密码";
            this.txtUserPwd.Location = new System.Drawing.Point(49, 188);
            this.txtUserPwd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtUserPwd.Name = "txtUserPwd";
            this.txtUserPwd.Size = new System.Drawing.Size(175, 29);
            this.txtUserPwd.TabIndex = 2;
            this.txtUserPwd.Text = "输入固定密码/临时密码";
            this.txtUserPwd.UseSystemPasswordChar = true;
            this.txtUserPwd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUserPwd_KeyDown);
            this.txtUserPwd.Validating += new System.ComponentModel.CancelEventHandler(this.txtUserPwd_Validating);
            // 
            // loginInfo_txtUserName
            // 
            this.loginInfo_txtUserName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loginInfo_txtUserName.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.loginInfo_txtUserName.ForeColor = System.Drawing.SystemColors.WindowText;
            this.errorProvider.SetIconAlignment(this.loginInfo_txtUserName, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
            this.loginInfo_txtUserName.InfoBackColor = System.Drawing.SystemColors.Window;
            this.loginInfo_txtUserName.InfoForeColor = System.Drawing.SystemColors.GrayText;
            this.loginInfo_txtUserName.InfoText = "输入手机号码";
            this.loginInfo_txtUserName.Location = new System.Drawing.Point(49, 145);
            this.loginInfo_txtUserName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.loginInfo_txtUserName.MaxLength = 11;
            this.loginInfo_txtUserName.Name = "loginInfo_txtUserName";
            this.loginInfo_txtUserName.Size = new System.Drawing.Size(210, 29);
            this.loginInfo_txtUserName.TabIndex = 1;
            this.loginInfo_txtUserName.Text = "输入手机号码";
            this.loginInfo_txtUserName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUserName_KeyDown);
            this.loginInfo_txtUserName.Validating += new System.ComponentModel.CancelEventHandler(this.txtUserName_Validating);
            // 
            // loginInfo_lblInfo
            // 
            this.loginInfo_lblInfo.AutoEllipsis = true;
            this.loginInfo_lblInfo.BackColor = System.Drawing.Color.SteelBlue;
            this.loginInfo_lblInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.loginInfo_lblInfo.Font = new System.Drawing.Font("微软雅黑 Light", 32F, System.Drawing.FontStyle.Bold);
            this.loginInfo_lblInfo.ForeColor = System.Drawing.Color.White;
            this.loginInfo_lblInfo.Location = new System.Drawing.Point(0, 0);
            this.loginInfo_lblInfo.Name = "loginInfo_lblInfo";
            this.loginInfo_lblInfo.Size = new System.Drawing.Size(464, 113);
            this.loginInfo_lblInfo.TabIndex = 0;
            this.loginInfo_lblInfo.Text = "Wlan-edu 登录";
            this.loginInfo_lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // logoutInfoPagePanel
            // 
            this.logoutInfoPagePanel.BackColor = System.Drawing.Color.White;
            this.logoutInfoPagePanel.CancelAutoLoginCheckBox = this.logoutInfo_cbCancelAutoLogin;
            this.logoutInfoPagePanel.Controls.Add(this.logoutInfo_cbCancelAutoLogin);
            this.logoutInfoPagePanel.Controls.Add(this.lblLogoutInfoInfo);
            this.logoutInfoPagePanel.Controls.Add(this.logoutInfo_btnLogout);
            this.logoutInfoPagePanel.Controls.Add(this.logoutInfo_txtUserName);
            this.logoutInfoPagePanel.Controls.Add(this.logoutInfo_lblInfo);
            this.logoutInfoPagePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logoutInfoPagePanel.Location = new System.Drawing.Point(0, 0);
            this.logoutInfoPagePanel.LogoutButton = this.logoutInfo_btnLogout;
            this.logoutInfoPagePanel.Name = "logoutInfoPagePanel";
            this.logoutInfoPagePanel.Size = new System.Drawing.Size(464, 336);
            this.logoutInfoPagePanel.TabIndex = 7;
            this.logoutInfoPagePanel.UserNameTextBox = this.logoutInfo_txtUserName;
            this.logoutInfoPagePanel.Logout += new SamLu.Tools.Wlan_edu_Manager.LogoutEventHandler(this.logout);
            // 
            // logoutInfo_cbCancelAutoLogin
            // 
            this.logoutInfo_cbCancelAutoLogin.AutoSize = true;
            this.logoutInfo_cbCancelAutoLogin.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.logoutInfo_cbCancelAutoLogin.Location = new System.Drawing.Point(302, 150);
            this.logoutInfo_cbCancelAutoLogin.Name = "logoutInfo_cbCancelAutoLogin";
            this.logoutInfo_cbCancelAutoLogin.Size = new System.Drawing.Size(112, 24);
            this.logoutInfo_cbCancelAutoLogin.TabIndex = 4;
            this.logoutInfo_cbCancelAutoLogin.Text = "取消自动登录";
            this.logoutInfo_cbCancelAutoLogin.UseVisualStyleBackColor = true;
            // 
            // lblLogoutInfoInfo
            // 
            this.lblLogoutInfoInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLogoutInfoInfo.AutoEllipsis = true;
            this.lblLogoutInfoInfo.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.lblLogoutInfoInfo.ForeColor = System.Drawing.Color.Red;
            this.lblLogoutInfoInfo.Location = new System.Drawing.Point(46, 182);
            this.lblLogoutInfoInfo.Name = "lblLogoutInfoInfo";
            this.lblLogoutInfoInfo.Size = new System.Drawing.Size(368, 84);
            this.lblLogoutInfoInfo.TabIndex = 3;
            this.lblLogoutInfoInfo.Text = "　　输入11位手机号码，然后点击“下线”按钮，指定账户下线。\r\n　　如果当前处于非正常登录状态下，可以使用此功能下线。";
            // 
            // logoutInfo_btnLogout
            // 
            this.logoutInfo_btnLogout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logoutInfo_btnLogout.BackColor = System.Drawing.Color.SandyBrown;
            this.logoutInfo_btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.logoutInfo_btnLogout.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.logoutInfo_btnLogout.ForeColor = System.Drawing.Color.White;
            this.logoutInfo_btnLogout.Location = new System.Drawing.Point(107, 269);
            this.logoutInfo_btnLogout.Name = "logoutInfo_btnLogout";
            this.logoutInfo_btnLogout.Size = new System.Drawing.Size(249, 58);
            this.logoutInfo_btnLogout.TabIndex = 2;
            this.logoutInfo_btnLogout.Text = "下线";
            this.logoutInfo_btnLogout.UseVisualStyleBackColor = false;
            // 
            // logoutInfo_txtUserName
            // 
            this.logoutInfo_txtUserName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logoutInfo_txtUserName.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.logoutInfo_txtUserName.ForeColor = System.Drawing.SystemColors.WindowText;
            this.logoutInfo_txtUserName.InfoBackColor = System.Drawing.SystemColors.Window;
            this.logoutInfo_txtUserName.InfoForeColor = System.Drawing.SystemColors.GrayText;
            this.logoutInfo_txtUserName.InfoText = "输入手机号码";
            this.logoutInfo_txtUserName.Location = new System.Drawing.Point(49, 145);
            this.logoutInfo_txtUserName.Name = "logoutInfo_txtUserName";
            this.logoutInfo_txtUserName.Size = new System.Drawing.Size(210, 29);
            this.logoutInfo_txtUserName.TabIndex = 1;
            this.logoutInfo_txtUserName.Text = "输入手机号码";
            this.logoutInfo_txtUserName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUserName_KeyDown);
            this.logoutInfo_txtUserName.Validating += new System.ComponentModel.CancelEventHandler(this.txtUserName_Validating);
            // 
            // logoutInfo_lblInfo
            // 
            this.logoutInfo_lblInfo.BackColor = System.Drawing.Color.SteelBlue;
            this.logoutInfo_lblInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.logoutInfo_lblInfo.Font = new System.Drawing.Font("微软雅黑 Light", 32F, System.Drawing.FontStyle.Bold);
            this.logoutInfo_lblInfo.ForeColor = System.Drawing.Color.White;
            this.logoutInfo_lblInfo.Location = new System.Drawing.Point(0, 0);
            this.logoutInfo_lblInfo.Name = "logoutInfo_lblInfo";
            this.logoutInfo_lblInfo.Size = new System.Drawing.Size(464, 113);
            this.logoutInfo_lblInfo.TabIndex = 0;
            this.logoutInfo_lblInfo.Text = "Wlan-edu 下线";
            this.logoutInfo_lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.cmsNotifyIcon;
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseClick);
            // 
            // cmsNotifyIcon
            // 
            this.cmsNotifyIcon.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsNotifyIcon_tsmiLogin,
            this.cmsNotifyIcon_tsmiLogout,
            this.cmsNotifyIcon_tsmiSeperator1,
            this.cmsNotifyIcon_tsmiSettings,
            this.cmsNotifyIcon_tsmiAbout,
            this.cmsNotifyIcon_tsmiSeperator2,
            this.cmsNotifyIcon_tsmiExit});
            this.cmsNotifyIcon.Name = "cmsNotifyIcon";
            this.cmsNotifyIcon.Size = new System.Drawing.Size(200, 126);
            // 
            // cmsNotifyIcon_tsmiLogin
            // 
            this.cmsNotifyIcon_tsmiLogin.CheckOnClick = true;
            this.cmsNotifyIcon_tsmiLogin.Image = global::SamLu.Tools.Wlan_edu_Manager.GUI.Properties.Resources.login_png;
            this.cmsNotifyIcon_tsmiLogin.Name = "cmsNotifyIcon_tsmiLogin";
            this.cmsNotifyIcon_tsmiLogin.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.I)));
            this.cmsNotifyIcon_tsmiLogin.Size = new System.Drawing.Size(199, 22);
            this.cmsNotifyIcon_tsmiLogin.Text = "登录(&I)";
            this.cmsNotifyIcon_tsmiLogin.Click += new System.EventHandler(this.cmsNotifyIcon_tsmiLogin_Click);
            // 
            // cmsNotifyIcon_tsmiLogout
            // 
            this.cmsNotifyIcon_tsmiLogout.CheckOnClick = true;
            this.cmsNotifyIcon_tsmiLogout.Image = global::SamLu.Tools.Wlan_edu_Manager.GUI.Properties.Resources.logout_png;
            this.cmsNotifyIcon_tsmiLogout.Name = "cmsNotifyIcon_tsmiLogout";
            this.cmsNotifyIcon_tsmiLogout.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.O)));
            this.cmsNotifyIcon_tsmiLogout.Size = new System.Drawing.Size(199, 22);
            this.cmsNotifyIcon_tsmiLogout.Text = "下线(&O)";
            this.cmsNotifyIcon_tsmiLogout.Click += new System.EventHandler(this.cmsNotifyIcon_tsmiLogout_Click);
            // 
            // cmsNotifyIcon_tsmiSeperator1
            // 
            this.cmsNotifyIcon_tsmiSeperator1.Name = "cmsNotifyIcon_tsmiSeperator1";
            this.cmsNotifyIcon_tsmiSeperator1.Size = new System.Drawing.Size(196, 6);
            // 
            // cmsNotifyIcon_tsmiSettings
            // 
            this.cmsNotifyIcon_tsmiSettings.Image = global::SamLu.Tools.Wlan_edu_Manager.GUI.Properties.Resources.settings_png;
            this.cmsNotifyIcon_tsmiSettings.Name = "cmsNotifyIcon_tsmiSettings";
            this.cmsNotifyIcon_tsmiSettings.Size = new System.Drawing.Size(199, 22);
            this.cmsNotifyIcon_tsmiSettings.Text = "设置(&S)";
            // 
            // cmsNotifyIcon_tsmiAbout
            // 
            this.cmsNotifyIcon_tsmiAbout.Name = "cmsNotifyIcon_tsmiAbout";
            this.cmsNotifyIcon_tsmiAbout.Size = new System.Drawing.Size(199, 22);
            this.cmsNotifyIcon_tsmiAbout.Text = "关于(&A)";
            // 
            // cmsNotifyIcon_tsmiSeperator2
            // 
            this.cmsNotifyIcon_tsmiSeperator2.Name = "cmsNotifyIcon_tsmiSeperator2";
            this.cmsNotifyIcon_tsmiSeperator2.Size = new System.Drawing.Size(196, 6);
            // 
            // cmsNotifyIcon_tsmiExit
            // 
            this.cmsNotifyIcon_tsmiExit.Image = global::SamLu.Tools.Wlan_edu_Manager.GUI.Properties.Resources.exit_png;
            this.cmsNotifyIcon_tsmiExit.Name = "cmsNotifyIcon_tsmiExit";
            this.cmsNotifyIcon_tsmiExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.cmsNotifyIcon_tsmiExit.Size = new System.Drawing.Size(199, 22);
            this.cmsNotifyIcon_tsmiExit.Text = "退出(&X)";
            this.cmsNotifyIcon_tsmiExit.Click += new System.EventHandler(this.cmsNotifyIcon_tsmiExit_Click);
            // 
            // timer
            // 
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // statusBar
            // 
            this.statusBar.BackColor = System.Drawing.Color.ForestGreen;
            this.statusBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.statusBar.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.statusBar.ForeColor = System.Drawing.Color.White;
            this.statusBar.Location = new System.Drawing.Point(0, 336);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(464, 25);
            this.statusBar.TabIndex = 6;
            this.statusBar.Text = "就绪";
            this.statusBar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.statusBar.Visible = false;
            // 
            // loginSucceededPagePanel
            // 
            this.loginSucceededPagePanel.CancelAutoLoginCheckBox = this.loginSucceeded_cbCancelAutoLogin;
            this.loginSucceededPagePanel.Controls.Add(this.loginSucceeded_cbCancelAutoLogin);
            this.loginSucceededPagePanel.Controls.Add(this.lblWlanInfos);
            this.loginSucceededPagePanel.Controls.Add(this.lblLoginDuration);
            this.loginSucceededPagePanel.Controls.Add(this.loginSucceeded_btnLogout);
            this.loginSucceededPagePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loginSucceededPagePanel.Location = new System.Drawing.Point(0, 0);
            this.loginSucceededPagePanel.LogoutButton = this.loginSucceeded_btnLogout;
            this.loginSucceededPagePanel.Name = "loginSucceededPagePanel";
            this.loginSucceededPagePanel.Size = new System.Drawing.Size(464, 336);
            this.loginSucceededPagePanel.TabIndex = 10;
            this.loginSucceededPagePanel.Logout += new SamLu.Tools.Wlan_edu_Manager.LogoutEventHandler(this.logout);
            // 
            // loginSucceeded_cbCancelAutoLogin
            // 
            this.loginSucceeded_cbCancelAutoLogin.AutoSize = true;
            this.loginSucceeded_cbCancelAutoLogin.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.loginSucceeded_cbCancelAutoLogin.Location = new System.Drawing.Point(49, 235);
            this.loginSucceeded_cbCancelAutoLogin.Name = "loginSucceeded_cbCancelAutoLogin";
            this.loginSucceeded_cbCancelAutoLogin.Size = new System.Drawing.Size(112, 24);
            this.loginSucceeded_cbCancelAutoLogin.TabIndex = 5;
            this.loginSucceeded_cbCancelAutoLogin.Text = "取消自动登录";
            this.loginSucceeded_cbCancelAutoLogin.UseVisualStyleBackColor = true;
            // 
            // lblWlanInfos
            // 
            this.lblWlanInfos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblWlanInfos.AutoEllipsis = true;
            this.lblWlanInfos.ForeColor = System.Drawing.Color.Red;
            this.lblWlanInfos.Location = new System.Drawing.Point(73, 145);
            this.lblWlanInfos.Name = "lblWlanInfos";
            this.lblWlanInfos.Size = new System.Drawing.Size(318, 87);
            this.lblWlanInfos.TabIndex = 4;
            // 
            // lblLoginDuration
            // 
            this.lblLoginDuration.AutoEllipsis = true;
            this.lblLoginDuration.BackColor = System.Drawing.Color.SteelBlue;
            this.lblLoginDuration.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblLoginDuration.Font = new System.Drawing.Font("微软雅黑 Light", 36F, System.Drawing.FontStyle.Bold);
            this.lblLoginDuration.ForeColor = System.Drawing.Color.White;
            this.lblLoginDuration.Location = new System.Drawing.Point(0, 0);
            this.lblLoginDuration.Name = "lblLoginDuration";
            this.lblLoginDuration.Size = new System.Drawing.Size(464, 113);
            this.lblLoginDuration.TabIndex = 3;
            this.lblLoginDuration.Text = "00 : 00 : 00";
            this.lblLoginDuration.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // loginSucceeded_btnLogout
            // 
            this.loginSucceeded_btnLogout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.loginSucceeded_btnLogout.BackColor = System.Drawing.Color.SandyBrown;
            this.loginSucceeded_btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.loginSucceeded_btnLogout.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.loginSucceeded_btnLogout.ForeColor = System.Drawing.Color.White;
            this.loginSucceeded_btnLogout.Location = new System.Drawing.Point(107, 269);
            this.loginSucceeded_btnLogout.Name = "loginSucceeded_btnLogout";
            this.loginSucceeded_btnLogout.Size = new System.Drawing.Size(249, 58);
            this.loginSucceeded_btnLogout.TabIndex = 2;
            this.loginSucceeded_btnLogout.Text = "下线";
            this.loginSucceeded_btnLogout.UseVisualStyleBackColor = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 361);
            this.Controls.Add(this.loginSucceededPagePanel);
            this.Controls.Add(this.logoutInfoPagePanel);
            this.Controls.Add(this.loginInfoPagePanel);
            this.Controls.Add(this.statusBar);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximumSize = new System.Drawing.Size(2000, 400);
            this.MinimumSize = new System.Drawing.Size(480, 400);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.loginInfoPagePanel.ResumeLayout(false);
            this.loginInfoPagePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.logoutInfoPagePanel.ResumeLayout(false);
            this.logoutInfoPagePanel.PerformLayout();
            this.cmsNotifyIcon.ResumeLayout(false);
            this.loginSucceededPagePanel.ResumeLayout(false);
            this.loginSucceededPagePanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.WinForm.LoginInfoPagePanel loginInfoPagePanel;
        private Controls.WinForm.InfoTextBox loginInfo_txtUserName;
        private System.Windows.Forms.Label loginInfo_lblInfo;
        private System.Windows.Forms.Button btnFetchTemproraryPwd;
        private System.Windows.Forms.Label lblPwdImg;
        private Controls.WinForm.InfoTextBox txtUserPwd;
        private System.Windows.Forms.CheckBox cbRememberMe;
        private System.Windows.Forms.CheckBox cbAutoLogin;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.LinkLabel linkLabel;
        private System.Windows.Forms.ImageList ilPwdBoxOption;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private Controls.WinForm.LogoutInfoPagePanel logoutInfoPagePanel;
        private System.Windows.Forms.Label logoutInfo_lblInfo;
        private System.Windows.Forms.Button logoutInfo_btnLogout;
        private Controls.WinForm.InfoTextBox logoutInfo_txtUserName;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip cmsNotifyIcon;
        private System.Windows.Forms.ToolStripMenuItem cmsNotifyIcon_tsmiLogin;
        private System.Windows.Forms.ToolStripMenuItem cmsNotifyIcon_tsmiLogout;
        private System.Windows.Forms.ToolStripSeparator cmsNotifyIcon_tsmiSeperator1;
        private System.Windows.Forms.ToolStripMenuItem cmsNotifyIcon_tsmiSettings;
        private System.Windows.Forms.Label lblLogoutInfoInfo;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.ToolStripMenuItem cmsNotifyIcon_tsmiAbout;
        private System.Windows.Forms.ToolStripSeparator cmsNotifyIcon_tsmiSeperator2;
        private System.Windows.Forms.ToolStripMenuItem cmsNotifyIcon_tsmiExit;
        private Controls.WinForm.StatusBar statusBar;
        private System.Windows.Forms.CheckBox logoutInfo_cbCancelAutoLogin;
        private LightLoginSucceededPagePanel loginSucceededPagePanel;
        private System.Windows.Forms.Label lblWlanInfos;
        private System.Windows.Forms.Label lblLoginDuration;
        private System.Windows.Forms.Button loginSucceeded_btnLogout;
        private System.Windows.Forms.CheckBox loginSucceeded_cbCancelAutoLogin;
    }
}