using SamLu.Native.Wifi;
using System;
using System.Windows;

namespace SamLu.Tools.Wlan_edu_Manager.GUI
{
    partial class DisconnectionWindow
    {
        #region ConnectionState
        public static readonly DependencyProperty ConnectionStateProperty =
            DependencyProperty.Register(
                nameof(ConnectionState), typeof(ConnectionState), typeof(DisconnectionWindow),
                new FrameworkPropertyMetadata(ConnectionState.Disconnected, FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsParentArrange | FrameworkPropertyMetadataOptions.AffectsParentMeasure | FrameworkPropertyMetadataOptions.AffectsRender,
                    new PropertyChangedCallback(DisconnectionWindow.ConnectionStatePropertyChangedCallback)
                ),
                DisconnectionWindow.ConnectionStateValidateValueCallback
            );

        private static void ConnectionStatePropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
#warning 未实现。
        }

        private static bool ConnectionStateValidateValueCallback(object value)
        {
            return Enum.IsDefined(typeof(ConnectionState), value);
        }

        public ConnectionState ConnectionState
        {
            get => (ConnectionState)this.GetValue(ConnectionStateProperty);
            set => this.SetValue(ConnectionStateProperty, value);
        }
        #endregion
    }
}