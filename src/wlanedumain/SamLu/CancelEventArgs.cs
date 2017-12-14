using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SamLu
{
    public class CancelArgs : EventArgs
    {
        public bool Cancel { get; set; }

        public CancelArgs() : this(false) { }

        public CancelArgs(bool cancel)
        {
            this.Cancel = cancel;
        }
    }
}
