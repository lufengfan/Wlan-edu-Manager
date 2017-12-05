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

                    cell.SetBinding(
                        Shape.FillProperty,
                        new Binding()
                        {
                            Source = lr,
                            Path = new PropertyPath(nameof(LoadingRing.Foreground))
                        }
                    );

                    lr.cells.Add(cell);
                    lr.grid.Children.Add(cell);
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

                cell.SetBinding(
                    Shape.FillProperty,
                    new Binding()
                    {
                        Source = lr,
                        Path = new PropertyPath(nameof(LoadingRing.Foreground))
                    }
                );

                lr.cells.Add(cell);
                lr.grid.Children.Add(cell);
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

        public Type CellType
        {
            get => (Type)this.GetValue(CellTypeProperty);
            set => this.SetValue(CellTypeProperty, value);
        }
        #endregion

        #region InnerRadius
        public static readonly DependencyProperty InnerRadiusProperty =
            LoadingRingCell.InnerRadiusProperty.AddOwner(
                typeof(LoadingRing),
                new PropertyMetadata(45.0,
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
                new PropertyMetadata(45.0,
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

    }
}