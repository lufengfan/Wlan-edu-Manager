using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace SamLu.Tools.Wlan_edu_Manager.GUI.Controls.WPF
{
    public sealed class CamberLoadingRingCell : LoadingRingCell
    {
        private PathFigure figure;
        private LineSegment line1;
        private LineSegment line2;
        private ArcSegment arc1;
        private ArcSegment arc2;
        
        public CamberLoadingRingCell() : base()
        {
            this.line1 = new LineSegment();
            this.line2 = new LineSegment();
            this.arc1 = new ArcSegment();
            this.arc2 = new ArcSegment();
            this.figure = new PathFigure() {
                IsClosed = true,
                IsFilled = true,
                Segments = new PathSegmentCollection() { line1, arc1, line2, arc2 }
            };
            this.OnFactorChanged(this.CellRadian, this.Direction, this.InnerRadius, this.OuterRadius);

            this.geometry = new PathGeometry(new[] { this.figure });
        }

        private Point caculateInsectionPoint(double radius, double direction) => this.caculateInsectionPoint(radius, direction, new Point(0,0));

        private Point caculateInsectionPoint(double radius, double direction, Point centerPoint)
        {
            const double angle_per_circle = 360.0;
            double a = direction / angle_per_circle * Math.PI;

            double sinh = Math.Sinh(a);
            double cosh = Math.Cosh(a);
            Vector vector = new Vector(sinh, cosh);

            return centerPoint + vector * radius;
        }

        private void OnFactorChanged(double cellRadian, double direction, double innerRadius, double outerRadius)
        {
            this.figure.StartPoint = this.caculateInsectionPoint(outerRadius, direction - cellRadian / 2);

            this.line1.Point = this.caculateInsectionPoint(innerRadius, direction - cellRadian / 2);

            this.arc1.Size = new Size(innerRadius, innerRadius);
            this.arc1.Point = this.caculateInsectionPoint(innerRadius, direction + cellRadian / 2);
            this.arc1.SweepDirection = SweepDirection.Counterclockwise;

            this.line2.Point = this.caculateInsectionPoint(outerRadius, direction + cellRadian / 2);

            this.arc2.Size = new Size(outerRadius, outerRadius);
            this.arc2.Point = this.caculateInsectionPoint(outerRadius, direction - cellRadian / 2);
            this.arc2.SweepDirection = SweepDirection.Clockwise;
        }

        protected override void OnCellRadianChanged(double cellRadian)
        {
            double direction = this.Direction;
            double innerRadius = this.InnerRadius;
            double outerRadius = this.OuterRadius;

            this.OnFactorChanged(cellRadian, direction, innerRadius, outerRadius);
        }

        protected override void OnDirectionChanged(double direction)
        {
            double cellRadian = this.CellRadian;
            double innerRadius = this.InnerRadius;
            double outerRadius = this.OuterRadius;

            this.OnFactorChanged(cellRadian, direction, innerRadius, outerRadius);
        }

        protected override void OnInnerRadiusChanged(double innerRadius)
        {
            double cellRadian = this.CellRadian;
            double direction = this.Direction;
            double outerRadius = this.OuterRadius;

            this.OnFactorChanged(cellRadian, direction, innerRadius, outerRadius);
        }

        protected override void OnOuterRadiusChanged(double outerRadius)
        {
            double cellRadian = this.CellRadian;
            double direction = this.Direction;
            double innerRadius = this.InnerRadius;

            this.OnFactorChanged(cellRadian, direction, innerRadius, outerRadius);
        }
    }
}
