using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SamLu.Tools.Wlan_edu_Manager
{
    public interface IManagerPageContainer<TPage>
    {
        TPage CurrentPage{ get; }

        bool IsSupport(TPage page);
        void Switch(TPage page);
    }
}
