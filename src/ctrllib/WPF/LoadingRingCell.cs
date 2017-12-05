using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SamLu.Tools.Wlan_edu_Manager.GUI.Controls.WPF
{
    public abstract class LoadingRingCell : Shape
    {
        protected PathGeometry geometry;
        
        protected override Geometry DefiningGeometry => this.geometry ?? Geometry.Empty;

        public Rect DefiningGeometryBounds => this.DefiningGeometry.Bounds;

        #region CellRadian
        public static readonly DependencyProperty CellRadianProperty =
            DependencyProperty.Register(
                nameof(CellRadian), typeof(double), typeof(LoadingRingCell),
                new PropertyMetadata(6.0,
                    LoadingRingCell.CellRadianPropertyChangedCallback,
                    LoadingRingCell.CellRadianCoerceValueCallback
                ),
                LoadingRingCell.CellRadianValidateValueCallback
            );

        private static void CellRadianPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((LoadingRingCell)d).OnCellRadianChanged((double)e.NewValue);
        }

        protected abstract void OnCellRadianChanged(double cellRadian);

        private static object CellRadianCoerceValueCallback(DependencyObject d, object baseValue)
        {
            const double angle_per_cycle = 360.0;
            double cellRadian = (double)baseValue;
            if (cellRadian > angle_per_cycle)
                return angle_per_cycle;
            else
                return cellRadian;
        }

        private static bool CellRadianValidateValueCallback(object value)
        {
            double cellRadian = (double)value;
            return !double.IsNaN(cellRadian) && !double.IsInfinity(cellRadian);
        }

        public double CellRadian
        {
            get => (double)this.GetValue(LoadingRingCell.CellRadianProperty);
            set => this.SetValue(LoadingRingCell.CellRadianProperty, value);
        }
        #endregion

        #region Direction

        public static readonly DependencyProperty DirectionProperty =
            DependencyProperty.Register(
                nameof(Direction), typeof(double), typeof(LoadingRingCell),
                new PropertyMetadata(0.0,
                    LoadingRingCell.DirectionPropertyChangedCallback,
                    LoadingRingCell.DirectionCoerceValueCallback
                ),
                LoadingRingCell.DirectionValidateCallback
            );
        public double Direction
        {
            get => (double)this.GetValue(LoadingRingCell.DirectionProperty);
            set => this.SetValue(LoadingRingCell.DirectionProperty, value);
        }

        private static void DirectionPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((LoadingRingCell)d).OnDirectionChanged((double)e.NewValue);
        }

        protected abstract void OnDirectionChanged(double direction);

        private static object DirectionCoerceValueCallback(DependencyObject d, object baseValue)
        {
            const double angle_per_circle = 360.0;
            double direction = (double)baseValue;
            return Math.Abs(direction) % angle_per_circle;
        }

        private static bool DirectionValidateCallback(object value)
        {
            double direction = (double)value;
            return !double.IsNaN(direction) && !double.IsInfinity(direction);
        }
        #endregion

        #region InnerRadius
        public static readonly DependencyProperty InnerRadiusProperty =
            DependencyProperty.Register(
                nameof(InnerRadius), typeof(double), typeof(LoadingRingCell),
                new PropertyMetadata(35.0,
                    LoadingRingCell.InnerRadiusPropertyChangedCallback,
                    LoadingRingCell.InnerRadiusCoerceValueCallback
                ),
                LoadingRingCell.InnerRadiusValidateValueCallback
            );

        public double InnerRadius
        {
            get => (double)this.GetValue(LoadingRingCell.InnerRadiusProperty);
            set => this.SetValue(LoadingRingCell.InnerRadiusProperty, value);
        }

        private static void InnerRadiusPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((LoadingRingCell)d).OnInnerRadiusChanged((double)e.NewValue);
        }

        protected abstract void OnInnerRadiusChanged(double innerRadius);

        private static object InnerRadiusCoerceValueCallback(DependencyObject d, object baseValue)
        {
            LoadingRingCell lr = (LoadingRingCell)d;
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
            DependencyProperty.Register(
                nameof(OuterRadius), typeof(double), typeof(LoadingRingCell),
                new PropertyMetadata(45.0,
                    LoadingRingCell.OuterRadiusPropertyChangedCallback,
                    LoadingRingCell.OuterRadiusCoerceValueCallback
                ),
                LoadingRingCell.OuterRadiusValidateValueCallback
            );

        public double OuterRadius
        {
            get => (double)this.GetValue(LoadingRingCell.OuterRadiusProperty);
            set => this.SetValue(LoadingRingCell.OuterRadiusProperty, value);
        }

        private static void OuterRadiusPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((LoadingRingCell)d).OnOuterRadiusChanged((double)e.NewValue);
        }

        protected abstract void OnOuterRadiusChanged(double outerRadius);

        private static object OuterRadiusCoerceValueCallback(DependencyObject d, object baseValue)
        {
            LoadingRingCell lr = (LoadingRingCell)d;
            double outerRadius = (double)baseValue;
            if (outerRadius < lr.InnerRadius)
                lr.InnerRadius = outerRadius;

            return outerRadius;
        }

        private static bool OuterRadiusValidateValueCallback(object value)
        {
            return (double)value >= 0;
        }
        #endregion

        protected LoadingRingCell() : base() { }
    }
}
