using SamLu.Native.Wifi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SamLu.Native.Wifi
{
    public delegate void WifiScanningCompletedEventHandler(object sender, WifiScanningCompletedEventArgs e);

    public class WifiScanningCompletedEventArgs : EventArgs
    {
        private WIFISSID current;
        private WIFISSID[] ssids;

        public bool IsConnected => this.current != null;
        public WIFISSID CurrentConnection => this.current;
        public WIFISSID[] SSIDs => this.ssids;

        public WifiScanningCompletedEventArgs() : this(null, new WIFISSID[0]) { }

        public WifiScanningCompletedEventArgs(WIFISSID current, WIFISSID[] ssids)
        {
            this.current = current;
            this.ssids = ssids ?? throw new ArgumentNullException(nameof(ssids));
        }
    }
}
