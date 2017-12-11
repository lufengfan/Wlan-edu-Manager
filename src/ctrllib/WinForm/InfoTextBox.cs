using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SamLu.Tools.Wlan_edu_Manager.GUI.Controls.WinForm
{
    public class InfoTextBox : TextBox
    {
        private string infoText = "";
        [Description("获取或设置提示信息。")]
        [DefaultValue("")]
        public string InfoText
        {
            get => this.infoText;
            set
            {
                this.infoText = value;

                if (this.isInfoShown)
                    base.Text = this.infoText;
            }
        }

        [Description("获取或设置默认的提示信息前景色。")]
        public virtual Color InfoForeColor { get; set; } = SystemColors.GrayText;

        private Color? infoBackColor = null;
        [Description("获取或设置默认的提示信息背景色。")]
        public virtual Color InfoBackColor
        {
            get => this.infoBackColor ?? this.BackColor;
            set => this.infoBackColor = value;
        }

        private Color foreColor;
        public new virtual Color ForeColor
        {
            get
            {
                //if (!this.isInfoShown)
                //    this.foreColor = base.ForeColor;

                return this.foreColor;
            }
            set
            {
                this.foreColor = value;

                if (!this.isInfoShown)
                    base.ForeColor = this.foreColor;
            }
        }

        private Color backColor;
        public new virtual Color BackColor
        {
            get
            {
                //if (!this.isInfoShown)
                //    this.backColor = base.BackColor;

                return this.backColor;
            }
            set
            {
                this.backColor = value;

                if (!this.isInfoShown)
                    base.BackColor = this.backColor;
            }
        }

        private char passwordChar;
        [DefaultValue('\0')]
        [Localizable(true)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [Category("行为")]
        [Description("指示当为单行编辑控件的密码输入显示的字符。")]
        new public char PasswordChar
        {
            get
            {
                //if (!this.isInfoShown)
                //    this.passwordChar = base.PasswordChar;

                return this.passwordChar;
            }
            set
            {
                this.passwordChar = value;

                if (!this.isInfoShown)
                    base.PasswordChar = this.passwordChar;
            }
        }

        private bool useSystemPasswordChar;
        [DefaultValue(false)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [Category("行为")]
        [Description("指示编辑控件中的文本是否以默认的密码字符显示。")]
        new public bool UseSystemPasswordChar
        {
            get
            {
                //if (!this.isInfoShown)
                //    this.useSystemPasswordChar = base.UseSystemPasswordChar;

                return this.useSystemPasswordChar;
            }
            set
            {
                this.useSystemPasswordChar = value;

                if (!this.isInfoShown)
                    base.UseSystemPasswordChar = this.useSystemPasswordChar;
            }
        }
        
        [DefaultValue("")]
        [Description("设置编辑控件的文本内容。")]
        public new virtual string Text
        {
            get
            {
                if (this.IsInfoShown)
                    return string.Empty;
                else
                    return base.Text;
            }
            set
            {
                if (this.IsInfoShown && !string.IsNullOrEmpty(value))
                {
                    this.IsInfoShown = false;
                    base.Text = value;
                }
            }
        }

        private bool isInfoShown = true;
        public bool IsInfoShown
        {
            get => this.isInfoShown;
            protected set
            {
                this.isInfoShown = value;

                if (value)
                {
                    base.Text = this.InfoText;
                    
                    base.ForeColor = this.InfoForeColor;
                    base.BackColor = this.InfoBackColor;

                    base.PasswordChar = '\0';
                    base.UseSystemPasswordChar = false;
                }
                else
                {
                    base.Text = string.Empty;

                    base.ForeColor = this.ForeColor;
                    base.BackColor = this.BackColor;

                    base.PasswordChar = this.PasswordChar;
                    base.UseSystemPasswordChar = this.UseSystemPasswordChar;
                }
            }
        }

        public InfoTextBox() : base()
        {
            this.IsInfoShown = true;

            this.foreColor = base.ForeColor;
            this.backColor = base.BackColor;

            this.passwordChar = base.PasswordChar;
            this.useSystemPasswordChar = base.UseSystemPasswordChar;
            
            this.Enter += this.InfoTextBox_Enter;
            this.Leave += this.InfoTextBox_Leave;
        }
        
        private void InfoTextBox_Enter(object sender, EventArgs e)
        {
            if (this.IsInfoShown)
            {
                this.IsInfoShown = false;
            }
        }

        private void InfoTextBox_Leave(object sender, EventArgs e)
        {
            if (!this.IsInfoShown && string.IsNullOrEmpty(this.Text))
            {
                this.IsInfoShown = true;
            }
        }

        public new void Clear()
        {
            this.IsInfoShown = true;
        }
    }
}
