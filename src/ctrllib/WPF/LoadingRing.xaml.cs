using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SamLu.Tools.Wlan_edu_Manager.GUI.Controls.WPF
{
    partial class LoadingRing
    {
        private Grid grid;
        private ContentControl contentControl;
        private Canvas canvas;

        private void InitializeComponent()
        {
            this.grid = new Grid();
            this.contentControl = new ContentControl();
            this.canvas = new Canvas();
            this.canvas.SizeChanged += this.canvas_SizeChanged;

            this.grid.Children.Add(this.contentControl);
            this.grid.Children.Add(this.canvas);
            this.Content = this.grid;
        }

        private void canvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Size size = e.NewSize;
            foreach (var cell in this.cells)
            {
                Rect bounds = cell.DefiningGeometryBounds;
                double left = size.Width / 2 + bounds.Left;
                double top = size.Height / 2 + bounds.Top;
                Canvas.SetLeft(cell, left);
                Canvas.SetTop(cell, top);
            }
        }

        private void setCellFillColor(int index, Color color)
        {
            LoadingRingCell cell = this.cells[index];
            if (!(cell.Fill is SolidColorBrush brush && brush.Color == color))
                cell.Fill = new SolidColorBrush(color);
        }
    }
}