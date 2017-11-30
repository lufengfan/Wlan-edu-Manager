using SamLu.Tools.Wlan_edu_Manager.Login.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SamLu.Tools.Wlan_edu_Manager
{
    public class Wlan_eduManager
    {
        private IManagerPage page;

        public IManagerPage CurrentPage => this.page;
        
        public Wlan_eduManager(IManagerPage firstPage)
        {
            if (firstPage == null) throw new ArgumentNullException(nameof(firstPage));

            this.page = firstPage;
        }

        public Wlan_eduManager(string wlanAcName, string wlanUserIp, string url) : this(wlanAcName, wlanUserIp, url, Encoding.UTF8) { }

        public Wlan_eduManager(string wlanAcName, string wlanUserIp, string url, Encoding encoding)
        {
            this.page = new LoginInfoPage(url, encoding)
            {
                wlanAcName = wlanAcName,
                wlanUserIp = wlanUserIp
            };
            this.page.Initialize();
        }

        public bool NextPage(ChangePageHandler changePage, CallbackHandler callback = null)
        {
            if (changePage == null) throw new ArgumentNullException(nameof(changePage));

            CancelArgs e = new CancelArgs();
            this.page = changePage(this.page, e);
            if (!e.Cancel)
            {
                this.page.Initialize();
                callback?.Invoke(this.page, e);

                return !e.Cancel;
            }
            else return false;
        }

        internal static long GetMiliseconds()
        {
            DateTime logout_dt = DateTime.Now;
            TimeSpan span = logout_dt - new DateTime(1970, 1, 1);
            long miliseconds = 1;

            miliseconds *= (long)span.Days * 24 * 60 * 60 * 1000;

            return miliseconds;
        }

        public class CancelArgs
        {
            public bool Cancel { get; set; } = false;
        }

        public delegate IManagerPage ChangePageHandler(IManagerPage page, CancelArgs e);
        public delegate void CallbackHandler(IManagerPage page, CancelArgs e);
    }
}
