using NativeWifi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace SamLu.Native.Wifi
{
    [DebuggerDisplay("{SSID}, SignalQuality = {WlanSignalQuality}%")]
    public class WIFISSID : IEquatable<WIFISSID>
    {
        public string SSID { get; set; } = "";
        public string Dot11DefaultAuthAlgorithm { get; set; } = "";
        public string Dot11DefaultCipherAlgorithm { get; set; } = "";
        public bool NetworkConnectable { get; set; } = true;
        public Wlan.WlanReasonCode WlanNotConnectableReason { get; set; } = Wlan.WlanReasonCode.Success;
        public uint WlanSignalQuality { get; set; } = 0U;
        public WlanClient.WlanInterface WlanInterface { get; set; } = null;

        public sealed override bool Equals(object obj)
        {
            return obj != null && obj is WIFISSID ssid && this.Equals(ssid);
        }

        public bool Equals(WIFISSID other)
        {
            return
                this.SSID == other.SSID &&
                this.WlanInterface == other.WlanInterface;
        }

        public static bool operator ==(WIFISSID lt, WIFISSID rt)
        {
            return object.Equals(lt, rt);
        }

        public static bool operator !=(WIFISSID lt, WIFISSID rt)
        {
            return !(lt == rt);
        }
    }
}
