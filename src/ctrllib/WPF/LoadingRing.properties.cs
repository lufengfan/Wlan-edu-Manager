using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SamLu.Tools.Wlan_edu_Manager.GUI.Controls.WPF
{
    partial class LoadingRing
    {
        protected List<LoadingRingCell> cells = new List<LoadingRingCell>();

        #region Content
        private bool LoadingRing_fInitialize = false;
        protected override void OnContentChanged(object oldContent, object newContent)
        {
            if (this.LoadingRing_fInitialize)
            {
                //if (newContent == null)
                //{
                //    this.Content = this.grid;
                //    this.LoadingRing_fInitialize = false;
                //}
                //else
                    this.contentControl.Content = newContent;
            }
            else
            {
                base.OnContentChanged(oldContent, newContent);
                this.LoadingRing_fInitialize = true;
            }
        }
        #endregion

        #region Padding
        private static void PaddingPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((LoadingRing)d).contentControl.Padding = (Thickness)e.NewValue;
        }
        #endregion

        #region ActivatedCellFillColor
        public static readonly DependencyProperty ActivatedCellFillColorProperty =
            DependencyProperty.Register(
                nameof(ActivatedCellFillColor), typeof(Color), typeof(LoadingRing),
                new FrameworkPropertyMetadata(Colors.Transparent, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender,
                    new PropertyChangedCallback(LoadingRing.ActivatedCellFillColorPropertyChangedCallback)
                )
            );

        public Color ActivatedCellFillColor
        {
            get => (Color)this.GetValue(ActivatedCellFillColorProperty);
            set => this.SetValue(ActivatedCellFillColorProperty, value);
        }

        private static void ActivatedCellFillColorPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LoadingRing lr = (LoadingRing)d;
            int cellCount = lr.CellCount;
            double progressValue = lr.ProgressValue;
            Color activated = (Color)e.NewValue;

            int i = Convert.ToInt32(progressValue * cellCount);
            for (int index = 0; index < i; index++)
            {
                lr.setCellFillColor(index, activated);
            }
        }
        #endregion

        #region DeactivatedCellFillColor
        public static readonly DependencyProperty DeactivatedCellFillColorProperty =
            DependencyProperty.Register(
                nameof(DeactivatedCellFillColor), typeof(Color), typeof(LoadingRing),
                new FrameworkPropertyMetadata(Colors.Transparent, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender,
                    new PropertyChangedCallback(LoadingRing.DeactivatedCellFillColorPropertyChangedCallback)
                )
            );

        public Color DeactivatedCellFillColor
        {
            get => (Color)this.GetValue(DeactivatedCellFillColorProperty);
            set => this.SetValue(DeactivatedCellFillColorProperty, value);
        }

        private static void DeactivatedCellFillColorPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LoadingRing lr = (LoadingRing)d;
            int cellCount = lr.CellCount;
            double progressValue = lr.ProgressValue;
            Color deactivated = (Color)e.NewValue;

            int i = Convert.ToInt32(progressValue * cellCount);
            for (int index = i; index < cellCount; index++)
            {
                lr.setCellFillColor(index, deactivated);
            }
        }
        #endregion

        #region CellCount
        public static readonly DependencyProperty CellCountProperty =
            DependencyProperty.Register(
                nameof(CellCount), typeof(int), typeof(LoadingRing),
                new PropertyMetadata(60,
                    LoadingRing.CellCountPropertyChangedCallback,
                    LoadingRing.CellCountCoerceValueCallback
                ),
                LoadingRing.CellCountValidateValueCallback
            );

        public int CellCount
        {
            get => (int)this.GetValue(LoadingRing.CellCountProperty);
            set => this.SetValue(LoadingRing.CellCountProperty, value);
        }

        private static void CellCountPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LoadingRing lr = (LoadingRing)d;
            int count = (int)e.NewValue;
            if (lr.cells.Count < count)
            {
                double innerRadius = lr.InnerRadius;
                double outerRadius = lr.OuterRadius;
                Type type = lr.CellType;
                for (int i = lr.cells.Count; i < count; i++)
                {
                    LoadingRingCell cell = (LoadingRingCell)Activator.CreateInstance(type);
                    cell.InnerRadius = innerRadius;
                    cell.OuterRadius = outerRadius;
                    cell.HorizontalAlignment = HorizontalAlignment.Center;
                    cell.VerticalAlignment = VerticalAlignment.Center;

                    lr.cells.Add(cell);
                    lr.canvas.Children.Add(cell);
                }
            }
            else if (lr.cells.Count > count)
            {
                for (int i = lr.cells.Count; i > count; i--)
                {
                    lr.cells.RemoveAt(i - 1);
                    lr.grid.Children.RemoveAt(i - 1);
                }
            }
            else return;

            const double angle_per_circle = 360.0;
            double cellRadian = angle_per_circle / lr.CellCount / 2;
            for (int i = 0; i < count; i++)
            {
                LoadingRingCell cell = lr.cells[i];
                cell.CellRadian = cellRadian;
                cell.Direction = angle_per_circle / lr.CellCount * i;
            }
        }

        private static object CellCountCoerceValueCallback(DependencyObject d, object baseValue)
        {
            return baseValue;
        }

        private static bool CellCountValidateValueCallback(object value)
        {
            return (int)value >= 0;
        }
        #endregion

        #region CellType
        public static readonly DependencyProperty CellTypeProperty =
            DependencyProperty.Register(
                nameof(CellType), typeof(Type), typeof(LoadingRing),
                new PropertyMetadata(typeof(CamberLoadingRingCell),
                    LoadingRing.CellTypePropertyChangedCallback,
                    LoadingRing.CellTypeCoerceValueCallback
                ),
                LoadingRing.CellTypeValidateValueCallback
            );

        public Type CellType
        {
            get => (Type)this.GetValue(CellTypeProperty);
            set => this.SetValue(CellTypeProperty, value);
        }

        private static void CellTypePropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LoadingRing lr = (LoadingRing)d;
            int count = lr.CellCount;
            Type type = (Type)e.NewValue;

            const double angle_per_circle = 360.0;
            double cellRadian = angle_per_circle / count / 2;
            double innerRadius = lr.InnerRadius;
            double outerRadius = lr.OuterRadius;
            for (int i = 0; i < count; i++)
            {
                LoadingRingCell cell = (LoadingRingCell)Activator.CreateInstance(type);
                cell.CellRadian = cellRadian;
                cell.InnerRadius = innerRadius;
                cell.OuterRadius = outerRadius;
                cell.Direction = angle_per_circle / count * i;
                cell.HorizontalAlignment = HorizontalAlignment.Center;
                cell.VerticalAlignment = VerticalAlignment.Center;
                
                lr.cells.Add(cell);
                lr.canvas.Children.Add(cell);
            }
        }

        private static object CellTypeCoerceValueCallback(DependencyObject d, object baseValue)
        {
            return baseValue;
        }

        private static bool CellTypeValidateValueCallback(object value)
        {
            if (value == null) return false;

            Type type = (Type)value;
            return (typeof(LoadingRingCell).IsAssignableFrom(type) && !type.IsAbstract && type.GetConstructor(Type.EmptyTypes) != null);
        }
        #endregion

        #region LoadingRingStyle
        public static readonly DependencyProperty LoadingRingStyleProperty =
            DependencyProperty.Register(
                nameof(LoadingRingStyle), typeof(LoadingRingStyle), typeof(LoadingRing),
                new FrameworkPropertyMetadata(LoadingRingStyle.Whirling, FrameworkPropertyMetadataOptions.AffectsRender,
                    new PropertyChangedCallback(LoadingRing.LoadingRingStylePropertyChangedCallback)
                ),
                LoadingRing.LoadingRingStyleValidateValueCallback
            );

        public LoadingRingStyle LoadingRingStyle
        {
            get => (LoadingRingStyle)this.GetValue(LoadingRingStyleProperty);
            set => this.SetValue(LoadingRingStyleProperty, value);
        }

        private static void LoadingRingStylePropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            
        }

        private static bool LoadingRingStyleValidateValueCallback(object value)
        {
            return Enum.IsDefined(typeof(LoadingRingStyle), value);
        }
        #endregion

        #region InnerRadius
        public static readonly DependencyProperty InnerRadiusProperty =
            LoadingRingCell.InnerRadiusProperty.AddOwner(
                typeof(LoadingRing),
                new PropertyMetadata(
                    LoadingRingCell.InnerRadiusProperty.DefaultMetadata.DefaultValue,
                    LoadingRing.InnerRadiusPropertyChangedCallback,
                    LoadingRing.InnerRadiusCoerceValueCallback
                )
            );

        public double InnerRadius
        {
            get => (double)this.GetValue(LoadingRing.InnerRadiusProperty);
            set => this.SetValue(LoadingRing.InnerRadiusProperty, value);
        }

        private static void InnerRadiusPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LoadingRing lr = (LoadingRing)d;
            double innerRadian = (double)e.NewValue;
            foreach (var cell in lr.cells)
                cell.InnerRadius = innerRadian;
        }

        private static object InnerRadiusCoerceValueCallback(DependencyObject d, object baseValue)
        {
            LoadingRing lr = (LoadingRing)d;
            double innerRadius = (double)baseValue;
            if (innerRadius > lr.OuterRadius)
                lr.OuterRadius = innerRadius;

            return innerRadius;
        }

        private static bool InnerRadiusValidateValueCallback(object value)
        {
            return (double)value >= 0;
        }
        #endregion

        #region OuterRadius
        public static readonly DependencyProperty OuterRadiusProperty =
            LoadingRingCell.OuterRadiusProperty.AddOwner(
                typeof(LoadingRing),
                new PropertyMetadata(
                    LoadingRingCell.OuterRadiusProperty.DefaultMetadata.DefaultValue,
                    LoadingRing.OuterRadiusPropertyChangedCallback,
                    LoadingRing.OuterRadiusCoerceValueCallback
                )
            );

        public double OuterRadius
        {
            get => (double)this.GetValue(LoadingRing.OuterRadiusProperty);
            set => this.SetValue(LoadingRing.OuterRadiusProperty, value);
        }

        private static void OuterRadiusPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LoadingRing lr = (LoadingRing)d;
            double outerRadian = (double)e.NewValue;
            foreach (var cell in lr.cells)
                cell.OuterRadius = outerRadian;
        }

        private static object OuterRadiusCoerceValueCallback(DependencyObject d, object baseValue)
        {
            LoadingRing lr = (LoadingRing)d;
            double outerRadius = (double)baseValue;
            if (outerRadius < lr.InnerRadius)
                lr.InnerRadius = outerRadius;
            
            return outerRadius;
        }
        #endregion

        #region ProgressValue
        public static readonly DependencyProperty ProgressValueProperty =
            DependencyProperty.Register(
                nameof(ProgressValue), typeof(double), typeof(LoadingRing),
                new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender,
                    new PropertyChangedCallback(LoadingRing.ProgressValuePropertyChangedCallback)
                ),
                LoadingRing.ProgressValueValidateValueCallback
            );

        public double ProgressValue
        {
            get => (double)this.GetValue(ProgressValueProperty);
            set => this.SetValue(ProgressValueProperty, value);
        }

        private static void ProgressValuePropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LoadingRing lr = (LoadingRing)d;
            int cellCount = lr.CellCount;
            Color activated = lr.ActivatedCellFillColor;
            Color deactivated = lr.DeactivatedCellFillColor;
            
            int start = Convert.ToInt32((double)e.NewValue * cellCount);
            switch (lr.LoadingRingStyle)
            {
                case LoadingRingStyle.Progress:
                    for (int index = 0; index < cellCount; index++)
                    {
                        Color cellFillColor;
                        if (index < start)
                            cellFillColor = activated;
                        else
                            cellFillColor = deactivated;
                        lr.setCellFillColor(index, cellFillColor);
                    }
                    break;
                case LoadingRingStyle.Whirling:
                    const double angle_per_cycle = 360.0;
                    for (int index = 0; index < cellCount; index++)
                    {
                        Color? color = null;
                        double percent = 0;
                        if (start > cellCount / 2)
                        {
                            if (index > start - cellCount / 2 && index <= start)
                                percent = ((double)start - (double)index) / (double)cellCount;
                            else
                                color = deactivated;
                        }
                        else
                        {
                            if (index <= start || index > start + cellCount / 2)
                                percent = ((double)start - (double)((index > cellCount / 2) ? index - cellCount : index)) / (double)cellCount;
                            else
                                color = deactivated;
                        }
                        color = color ??
                            Color.FromArgb(
                                (byte)((1 - percent) * activated.A + percent * deactivated.A),
                                (byte)((1 - percent) * activated.R + percent * deactivated.R),
                                (byte)((1 - percent) * activated.G + percent * deactivated.G),
                                (byte)((1 - percent) * activated.B + percent * deactivated.B)
                            );


                        lr.setCellFillColor(index, color.Value);
                    }
                    break;

            }
        }

        private static bool ProgressValueValidateValueCallback(object value)
        {
            double progressValue = (double)value;
            return (!double.IsNaN(progressValue) && !double.IsInfinity(progressValue)) && ((progressValue >= 0 && progressValue <= 1));
        }
        #endregion

        #region WhirlingDuration
        public static readonly DependencyProperty WhirlingDurationProperty =
            DependencyProperty.Register(
                nameof(WhirlingDuration), typeof(Duration), typeof(LoadingRing),
                new FrameworkPropertyMetadata(new Duration(TimeSpan.FromMilliseconds(1000)), FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender,
                    new PropertyChangedCallback(LoadingRing.WhirlingDurationPropertyChangedCallback)
                )
            );

        public Duration WhirlingDuration
        {
            get { return (Duration)GetValue(WhirlingDurationProperty); }
            set { SetValue(WhirlingDurationProperty, value); }
        }
        
        private static void WhirlingDurationPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
#warning 未实现。
        }
        #endregion

        #region ActivationDuration
        public static readonly DependencyProperty ActivationDurationProperty =
            DependencyProperty.Register(
                nameof(ActivationDuration), typeof(Duration), typeof(LoadingRing),
                new FrameworkPropertyMetadata(new Duration(TimeSpan.FromMilliseconds(300)), FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender,
                    new PropertyChangedCallback(LoadingRing.ActivationDurationPropertyChangedCallback)
                )
            );

        public Duration ActivationDuration
        {
            get { return (Duration)GetValue(ActivationDurationProperty); }
            set { SetValue(ActivationDurationProperty, value); }
        }

        private static void ActivationDurationPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
#warning 未实现。
        }
        #endregion

    }
}