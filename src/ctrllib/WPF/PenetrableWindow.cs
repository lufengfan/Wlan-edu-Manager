using SamLu.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Interop;

namespace SamLu.Tools.Wlan_edu_Manager.GUI.Controls.WPF
{
    /// <summary>
    /// 提供可设置是否支持鼠标穿透的窗体或对话框的基类。
    /// </summary>
    public class PenetrableWindow : Window
    {
        #region IsPenetrate
        /// <summary>
        /// 标识 <see cref="IsPenetrate"/> 依赖属性。
        /// </summary>
        public static readonly DependencyProperty IsPenetrateProperty = 
            DependencyProperty.Register(
                nameof(IsPenetrate),
                typeof(bool),
                typeof(PenetrableWindow),
                new PropertyMetadata(
                    false,
                    IsPenetratePropertyChangedCallback,
                    IsPenetrateCoerceValueCallback
                )
            );

        /// <summary>
        /// 获取或设置一个值，该值指示窗口的工作区是否支持鼠标穿透。这是依赖项属性。
        /// </summary>
        /// <value>
        /// 如果窗口支持鼠标穿透则为 true ；否则为 false 。
        /// </value>
        public bool IsPenetrate
        {
            get => (bool)this.GetValue(PenetrableWindow.IsPenetrateProperty);
            set => this.SetValue(PenetrableWindow.IsPenetrateProperty, value);
        }

        /// <summary>
        /// 用户将要设置的 <see cref="IsPenetrate"/> 的值。
        /// </summary>
        private bool targetIsPenetrate = false;
        private PenetrationProvider penetrationProvider;

        private static void IsPenetratePropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }

        private static object IsPenetrateCoerceValueCallback(DependencyObject d, object baseValue)
        {
            PenetrableWindow window = (PenetrableWindow)d;

            window.targetIsPenetrate = (bool)baseValue;
            if (window.penetrationProvider != null && window.penetrationProvider.UpdateWindowPenetration(window.targetIsPenetrate))
                return baseValue;
            else
                return window.IsPenetrate;
        }
        #endregion

        /// <summary>
        /// 初始化 <see cref="PenetrableWindow"/> 类的新实例。
        /// </summary>
        public PenetrableWindow() : base()
        {
            this.Loaded += (sender, e) =>
            {
                if (sender is PenetrableWindow window && window.IsPenetrate != window.targetIsPenetrate)
                {
                    this.penetrationProvider = new PenetrationProvider(new WindowInteropHelper(window).Handle);

                    window.penetrationProvider.Initialize();
                    window.penetrationProvider.UpdateWindowPenetration(window.targetIsPenetrate);
                }
            };
            this.StateChanged += (sender, e) =>
            {
                if (sender is PenetrableWindow window && window.IsPenetrate != window.targetIsPenetrate)
                    window.penetrationProvider.UpdateWindowPenetration(window.targetIsPenetrate);
            };
        }
    }
}
