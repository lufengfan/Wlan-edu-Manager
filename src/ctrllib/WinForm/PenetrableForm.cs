using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SamLu.Tools.Wlan_edu_Manager.GUI.Controls.WinForm
{
    /// <summary>
    /// 提供可设置是否支持鼠标穿透的窗体或对话框的基类。
    /// </summary>
    public class PenetrableForm : Form
    {
        /// <summary>
        /// 获取或设置一个值，该值指示窗口的工作区是否支持鼠标穿透。
        /// </summary>
        /// <value>
        /// 如果窗口支持鼠标穿透则为 true ；否则为 false 。
        /// </value>
        [DefaultValue(false)]
        [Category("行为")]
        [Description("指示窗口的工作区是否支持鼠标穿透。")]
        public bool AllowsPenetrate { get; set; }
    }
}
