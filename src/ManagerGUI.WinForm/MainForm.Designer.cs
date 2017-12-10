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
            this.txtUserName = new SamLu.Tools.Wlan_edu_Manager.GUI.Controls.WinForm.InfoTextBox();
            this.lblInfo = new System.Windows.Forms.Label();
            this.statusBar = new SamLu.Tools.Wlan_edu_Manager.GUI.Controls.WinForm.StatusBar();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.logoutInfoPagePanel = new SamLu.Tools.Wlan_edu_Manager.GUI.Controls.WinForm.LogoutInfoPagePanel();
            this.loginInfoPagePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
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
            this.loginInfoPagePanel.Controls.Add(this.txtUserName);
            this.loginInfoPagePanel.Controls.Add(this.lblInfo);
            this.loginInfoPagePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.loginInfoPagePanel.FetchTemporaryPwdButton = this.btnFetchTemproraryPwd;
            this.loginInfoPagePanel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.loginInfoPagePanel.Location = new System.Drawing.Point(0, 0);
            this.loginInfoPagePanel.LoginButton = this.btnLogin;
            this.loginInfoPagePanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.loginInfoPagePanel.Name = "loginInfoPagePanel";
            this.loginInfoPagePanel.Size = new System.Drawing.Size(464, 336);
            this.loginInfoPagePanel.TabIndex = 0;
            this.loginInfoPagePanel.UserNameTextBox = this.txtUserName;
            this.loginInfoPagePanel.UserPwdTextBox = this.txtUserPwd;
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
            this.btnLogin.Enabled = false;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold);
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Location = new System.Drawing.Point(107, 269);
            this.btnLogin.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(249, 58);
            this.btnLogin.TabIndex = 0;
            this.btnLogin.Text = "登录";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
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
            this.btnFetchTemproraryPwd.Click += new System.EventHandler(this.btnFetchTemproraryPwd_Click);
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
            this.txtUserPwd.UseSystemPasswordChar = true;
            this.txtUserPwd.Validating += new System.ComponentModel.CancelEventHandler(this.txtUserPwd_Validating);
            // 
            // txtUserName
            // 
            this.txtUserName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUserName.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.txtUserName.ForeColor = System.Drawing.SystemColors.WindowText;
            this.errorProvider.SetIconAlignment(this.txtUserName, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
            this.txtUserName.InfoBackColor = System.Drawing.SystemColors.Window;
            this.txtUserName.InfoForeColor = System.Drawing.SystemColors.GrayText;
            this.txtUserName.InfoText = "输入手机号码";
            this.txtUserName.Location = new System.Drawing.Point(49, 145);
            this.txtUserName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtUserName.MaxLength = 11;
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(210, 29);
            this.txtUserName.TabIndex = 1;
            this.txtUserName.Validating += new System.ComponentModel.CancelEventHandler(this.txtUserName_Validating);
            // 
            // lblInfo
            // 
            this.lblInfo.AutoEllipsis = true;
            this.lblInfo.BackColor = System.Drawing.Color.SteelBlue;
            this.lblInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblInfo.Font = new System.Drawing.Font("微软雅黑 Light", 32F, System.Drawing.FontStyle.Bold);
            this.lblInfo.ForeColor = System.Drawing.Color.White;
            this.lblInfo.Location = new System.Drawing.Point(0, 0);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(464, 113);
            this.lblInfo.TabIndex = 0;
            this.lblInfo.Text = "Wlan-edu 登录";
            this.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // logoutInfoPagePanel
            // 
            this.logoutInfoPagePanel.BackColor = System.Drawing.Color.White;
            this.logoutInfoPagePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logoutInfoPagePanel.Location = new System.Drawing.Point(0, 0);
            this.logoutInfoPagePanel.Name = "logoutInfoPagePanel";
            this.logoutInfoPagePanel.Size = new System.Drawing.Size(464, 336);
            this.logoutInfoPagePanel.TabIndex = 7;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 361);
            this.Controls.Add(this.logoutInfoPagePanel);
            this.Controls.Add(this.loginInfoPagePanel);
            this.Controls.Add(this.statusBar);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximumSize = new System.Drawing.Size(2000, 400);
            this.MinimumSize = new System.Drawing.Size(480, 400);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.loginInfoPagePanel.ResumeLayout(false);
            this.loginInfoPagePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.WinForm.LoginInfoPagePanel loginInfoPagePanel;
        private Controls.WinForm.InfoTextBox txtUserName;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Button btnFetchTemproraryPwd;
        private System.Windows.Forms.Label lblPwdImg;
        private Controls.WinForm.InfoTextBox txtUserPwd;
        private System.Windows.Forms.CheckBox cbRememberMe;
        private System.Windows.Forms.CheckBox cbAutoLogin;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.LinkLabel linkLabel;
        private Controls.WinForm.StatusBar statusBar;
        private System.Windows.Forms.ImageList ilPwdBoxOption;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private Controls.WinForm.LogoutInfoPagePanel logoutInfoPagePanel;
    }
}