using SamLu.Tools.Wlan_edu_Manager.GUI.Controls.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Interop;
using static PInvoke.User32;

namespace SamLu.Interop
{
    public class PenetrationProvider : DependencyObject
    {


        #region IsPenetrate
        public static readonly DependencyProperty IsPenetrateProperty =
            DependencyProperty.RegisterAttached(
                "IsPenetrate",
                typeof(bool),
                typeof(PenetrationProvider),
                new DataPropertyMetadata<PenetrationProvider>(
                    null,
                    false,
                    IsPenetratePropertyChangedCallback,
                    IsPenetrateCoerceValueCallback
                )
            );

        private static void IsPenetratePropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }

        private static object IsPenetrateCoerceValueCallback(DependencyObject d, object baseValue)
        {
            UIElement element = (UIElement)d;
            var provider = PenetrationProvider.getPenetrationProviderFormPropertyMetadata((UIElement)d);
            bool targetIsPenetrate = (bool)baseValue;
            if (provider != null && provider.UpdateWindowPenetration(targetIsPenetrate))
                return baseValue;
            else
                return PenetrationProvider.GetIsPenetrate(element);
        }

        public static bool GetIsPenetrate(UIElement element)
        {
            if (element == null) throw new ArgumentNullException(nameof(element));

            PenetrationProvider.initializePenetrationProvider(element);

            return (bool)element.GetValue(IsPenetrateProperty);
        }

        public static void SetIsPenetrate(UIElement element, bool value)
        {
            if (element == null) throw new ArgumentNullException(nameof(element));

            PenetrationProvider.initializePenetrationProvider(element);

            element.SetValue(IsPenetrateProperty, value);
        }

        private static void initializePenetrationProvider(UIElement element)
        {
            PenetrationProvider.getPenetrationProviderFormPropertyMetadata(element);
        }

        private static PenetrationProvider getPenetrationProviderFormPropertyMetadata(UIElement element)
        {
            var metadata = (DataPropertyMetadata<PenetrationProvider>)PenetrationProvider.IsPenetrateProperty.GetMetadata(element);
            if (metadata.Data == null)
            {
                IntPtr handle;
                if (element is Window)
                    handle = new WindowInteropHelper((Window)element).Handle;
                else
                {
                    if (PresentationSource.FromVisual(element) is HwndSource source)
                        handle = source.Handle;
                    else
                        return null;
                }

                PenetrationProvider provider = new PenetrationProvider(handle);
                provider.Initialize();
                return provider;
            }
            else
                return metadata.Data;
        }
        #endregion

        protected internal const SetWindowLongFlags bitsGWLEx = SetWindowLongFlags.WS_EX_TRANSPARENT | SetWindowLongFlags.WS_EX_LAYERED;

        private IntPtr handle;
        /// <summary>
        /// 获取窗口区域的句柄。
        /// </summary>
        public IntPtr WndHandle => this.handle;

        /// <summary>
        /// 保留的 <see cref="WindowLongIndexFlags.GWL_EXSTYLE"/> 位的 <see cref="SetWindowLongFlags"/> 值。
        /// </summary>
        protected internal SetWindowLongFlags preservedGWLEx = 0;

        public PenetrationProvider(IntPtr handle)
        {
            if (handle == IntPtr.Zero)
                throw new ArgumentOutOfRangeException(nameof(handle), handle, "句柄为空。");

            this.handle = handle;
        }

        public virtual void Initialize()
        {
            SetWindowLongFlags oldGWLEx = (SetWindowLongFlags)GetWindowLong(this.handle, WindowLongIndexFlags.GWL_EXSTYLE);

            this.preservedGWLEx = ~(oldGWLEx ^ bitsGWLEx);
        }

        public bool UpdateWindowPenetration(bool allowsPenetrate)
        {
            SetWindowLongFlags oldGWLEx = (SetWindowLongFlags)GetWindowLong(this.handle, WindowLongIndexFlags.GWL_EXSTYLE);

            SetWindowLongFlags rGWLEx;
            if (allowsPenetrate)
                rGWLEx = (SetWindowLongFlags)SetWindowLong(this.handle, WindowLongIndexFlags.GWL_EXSTYLE, oldGWLEx | bitsGWLEx);
            else
                rGWLEx = (SetWindowLongFlags)SetWindowLong(this.handle, WindowLongIndexFlags.GWL_EXSTYLE, (oldGWLEx & ~bitsGWLEx) | this.preservedGWLEx);

            if (rGWLEx == 0)
                return false;
            else
            {
                this.preservedGWLEx = ~(oldGWLEx ^ bitsGWLEx);
                return true;
            }
        }
    }
}
