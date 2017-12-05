using NativeWifi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace SamLu.Native.Wifi
{
    public class WifiWatcher
    {
        /// <summary>
        /// 获取或设置 <see cref="WifiWatcher"/> 的时间间隔。
        /// </summary>
        public double Interval
        {
            get => this.timer.Interval;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), value, "时间间隔不能小于零。");

                this.timer.Interval = value;
            }
        }

        private Timer timer = new Timer() { Enabled = false, Interval = 10000 };
        private WIFISSID ssid = null;
        
        public WIFISSID CurrentConnection { get => this.ssid; }
        public WIFISSID[] SSIDs { get; protected set; }

        #region Connected
        public event EventHandler Connected;

        protected virtual void OnConnected(EventArgs e)
        {
            this.Connected?.Invoke(this, e);
        }
        #endregion

        #region ScaningStarted
        public event EventHandler ScaningStarted;

        protected virtual void OnScanningStarted(EventArgs e)
        {
            this.ScaningStarted?.Invoke(this, e);
        }
        #endregion

        #region ScanningCompleted
        public event WifiScanningCompletedEventHandler ScanningCompleted;

        protected virtual void OnScanningCompleted(WifiScanningCompletedEventArgs e)
        {
            this.ScanningCompleted?.Invoke(this, e);
        }
        #endregion

        #region CurrentConnectionChanged
        public event EventHandler CurrentConnectionChanged;

        protected virtual void OnCurrentConnectionChanged(EventArgs e)
        {
            this.CurrentConnectionChanged?.Invoke(this, e);
        }
        #endregion

        #region Connected
        public event EventHandler Disconnected;

        protected virtual void OnDisconnected(EventArgs e)
        {
            this.Disconnected?.Invoke(this, e);
        }
        #endregion

        public WifiWatcher()
        {
            this.timer.Elapsed += this.Timer_Elapsed;
        }

        public void Start()
        {
            this.timer.Start();
        }

        public void Stop()
        {
            this.timer.Stop();
        }

        public static void GetNativeWifi(out WIFISSID currentSsid, out WIFISSID[] ssids)
        {
            List<WIFISSID> list = new List<WIFISSID>();
            currentSsid = null;
            var wlanClient = new WlanClient();
            foreach (var wlanInterface in wlanClient.Interfaces)
            {
                var networks = wlanInterface.GetAvailableNetworkList(0);
                foreach (var network in networks)
                {
                    WIFISSID targetSSID = new WIFISSID()
                    {
                        SSID = Encoding.UTF8.GetString(network.dot11Ssid.SSID, 0, (int)network.dot11Ssid.SSIDLength),
                        Dot11DefaultAuthAlgorithm = network.dot11DefaultAuthAlgorithm.ToString(),
                        Dot11DefaultCipherAlgorithm = network.dot11DefaultCipherAlgorithm.ToString(),
                        NetworkConnectable = network.networkConnectable,
                        WlanNotConnectableReason = network.wlanNotConnectableReason,
                        WlanSignalQuality = network.wlanSignalQuality,
                        WlanInterface = wlanInterface
                    };

                    if ((network.flags & Wlan.WlanAvailableNetworkFlags.Connected) != 0)
                    {
                        // 当前连接了此网络。
                        currentSsid = targetSSID;
                    }
                    else
                        list.Add(targetSSID);
                }
            }
            ssids = list.ToArray();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.timer.Start();
            this.OnScanningStarted(EventArgs.Empty);

            WifiWatcher.GetNativeWifi(out WIFISSID current, out WIFISSID[] ssids);
            this.SSIDs = ssids;

            if (this.ssid != current)
            {
                bool connected = this.ssid == null;

                this.ssid = current;
                this.OnCurrentConnectionChanged(EventArgs.Empty);

                if (connected)
                    this.OnDisconnected(EventArgs.Empty);
                else
                    this.OnConnected(EventArgs.Empty);
            }

            this.OnScanningCompleted(new WifiScanningCompletedEventArgs(current, ssids));
            this.timer.Start();
        }
    }
}
