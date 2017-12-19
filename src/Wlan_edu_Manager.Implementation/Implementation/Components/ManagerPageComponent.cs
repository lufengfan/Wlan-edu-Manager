using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SamLu.Tools.Wlan_edu_Manager.Implementation.Components
{
    public abstract class ManagerPageComponent : IManagerPage
    {
        protected Wlan_eduManagerComponent manager;

        protected ManagerPageComponent() { }

        protected ManagerPageComponent(Wlan_eduManagerComponent manager) =>
            this.manager = manager ?? throw new ArgumentNullException(nameof(manager));

        public abstract void Initialize();
    }
}
