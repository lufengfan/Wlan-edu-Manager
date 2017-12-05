using SamLu.Native.Wifi;
using SamLu.Tools.Wlan_edu_Manager.GUI.Controls.WPF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;

namespace SamLu.Tools.Wlan_edu_Manager.GUI
{
    /// <summary>
    /// DisconnectionWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DisconnectionWindow : PenetrableWindow
    {
        public DisconnectionWindow()
        {
            InitializeComponent();

            

            this.wifiWatcher.ScaningStarted += (sende, e) =>
            {
                this.Dispatcher.Invoke((Action<string>)this.changeWifiWatcherMsg, "正在搜索 Wifi 信号……");
            };
            this.wifiWatcher.ScanningCompleted += (sender, e) =>
            {
                var ssid = e.CurrentConnection;
                if (ssid == null)
                    this.Dispatcher.Invoke((Action<string>)this.changeWifiWatcherMsg, "未连接网络。");
                else
                {
                    this.Dispatcher.Invoke((Action<string>)this.changeWifiWatcherMsg, $"已连接 {ssid.SSID} 。");
                    this.Dispatcher.Invoke(new Action(() => this.Opacity = 0.5));
                }
            };
            this.wifiWatcher.Start();
        }

        private void changeWifiWatcherMsg(string message)
        {
            this.lblWifiWatcherMsg.Content = message;
        }
    }
}
