using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace SamLu.Tools.Wlan_edu_Manager.GUI.Controls.WPF
{
    public class DataPropertyMetadata<TData> : PropertyMetadata
    {
        public TData Data { get; set; }

        public DataPropertyMetadata() : base() { }

        public DataPropertyMetadata(TData data) : this()
        {
            this.Data = data;
        }

        public DataPropertyMetadata(object defaultValue) : base(defaultValue) { }

        public DataPropertyMetadata(TData data, object defaultValue) : base(defaultValue)
        {
            this.Data = data;
        }

        public DataPropertyMetadata(PropertyChangedCallback propertyChangedCallback) : base(propertyChangedCallback) { }

        public DataPropertyMetadata(TData data, PropertyChangedCallback propertyChangedCallback) : base(propertyChangedCallback)
        {
            this.Data = data;
        }

        public DataPropertyMetadata(object defaultValue, PropertyChangedCallback propertyChangedCallback) : base(defaultValue, propertyChangedCallback) { }

        public DataPropertyMetadata(TData data, object defaultValue, PropertyChangedCallback propertyChangedCallback) : base(defaultValue, propertyChangedCallback)
        {
            this.Data = data;
        }

        public DataPropertyMetadata(object defaultValue, PropertyChangedCallback propertyChangedCallback, CoerceValueCallback coerceValueCallback) : base(defaultValue, propertyChangedCallback, coerceValueCallback) { }

        public DataPropertyMetadata(TData data, object defaultValue, PropertyChangedCallback propertyChangedCallback, CoerceValueCallback coerceValueCallback) : base(defaultValue, propertyChangedCallback, coerceValueCallback)
        {
            this.Data = data;
        }
    }
}
