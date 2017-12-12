using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SamLu
{
    public delegate void ChangedEventHandler<T>(object sender, ChangedEventArgs<T> e);
    
    public class ChangedEventArgs<T> : EventArgs
    {
        private T oldValue;
        private T newValue;

        public T OldValue => this.oldValue;
        public T NewValue => this.newValue;

        public ChangedEventArgs(T oldValue, T newValue)
        {
            this.oldValue = oldValue;
            this.newValue = newValue;
        }
    }
}
