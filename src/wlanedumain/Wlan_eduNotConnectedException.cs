using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SamLu.Tools.Wlan_edu_Manager
{
    [Serializable]
    public class Wlan_eduNotConnectedException : Exception
    {
        public Wlan_eduNotConnectedException() : this("未连接到 Wlan-edu 。") { }
        public Wlan_eduNotConnectedException(string message) : base(message) { }
        public Wlan_eduNotConnectedException(string message, Exception inner) : base(message, inner) { }
        protected Wlan_eduNotConnectedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
