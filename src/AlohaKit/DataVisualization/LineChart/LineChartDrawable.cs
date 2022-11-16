using AlohaKit.Enums;
using static AlohaKit.Enums.ChartEnums;

namespace AlohaKit.Controls
{
    public sealed class LineChartDrawable : BaseChartDrawable
    {
        #region Properties
        private LineChartStyle _style;
        private float _pointSize = 5f;
        private Color _pointColor = Color.FromArgb("#94B3FF");
        private Color _lineColor = Color.FromArgb("#94B3FF");
        private Color _fillCurveColor = Color.FromArgb("#ECF1FF");
        private bool _isCurveBackgroundFilled = true;
        private float _curveFactor = 0.6f;
        private bool _showPointsForCurveStyle = false;
        private bool _expandBackgroundCurvePath = false;

        /// <summary>
        /// If true Bezier curve Background Path color will expand and fill when drawn.
        /// </summary>
        public bool ExpandAndFillBackgroundCurvePath
        {
            get => _expandBackgroundCurvePath;
            set
            {
                _expandBackgroundCurvePath = value;
                RequestInvalidate();
            }
        }

        /// <summary>
        /// If true and Style prop equals to 'Curve' points will be shown when drawing cubic bezier
        /// </summary>
        public bool ShowPointsForCurveStyle
        {
            get => _showPointsForCurveStyle;
            set
            {
                _showPointsForCurveStyle = value;
                RequestInvalidate();
            }
        }

        /// <summary>
        /// Sets how 'curvy' the bezier curve will be when drawn. Accepts values between 0-1. Default is 0.6
        /// </summary>
        public float CurveFactor
        {
            get => _curveFactor;
            set
            {
                if (value > 1f) _curveFactor = 1;
                else if (value < 0f) _curveFactor = 0;
                else _curveFactor = value;
                RequestInvalidate();
            }
        }

        /// <summary>
        /// Gets or sets if the curve will have a solid color background when drawn.
        /// </summary>
        public bool IsCurveBackgroundFilled
        {
            get => _isCurveBackgroundFilled;
            set
            {
                _isCurveBackgroundFilled = value;
                RequestInvalidate();
            }
        }

        /// <summary>
        /// Defines the style for the current LineChart
        /// </summary>
        public LineChartStyle Style
        {
            get => _style;
            set
            {
                _style = value;
                RequestInvalidate();
            }
        }

        /// <summary>
        /// Gets or sets the  radius for each point.
        /// </summary>
        public float PointSize
        {
            get => _pointSize;
            set
            {
                _pointSize = value;
                RequestInvalidate();
            }
        }

        /// <summary>
        /// Gets or sets the color for each dot.
        /// </summary>
        public Color PointColor
        {
            get => _pointColor;
            set
            {
                _pointColor = value;
                RequestInvalidate();
            }
        }

        /// <summary>
        /// Gets or sets the color to use when drawing chart lines.
        /// </summary>
        public Color LineColor
        {
            get => _lineColor;
            set
            {
                _lineColor = value;
                RequestInvalidate();
            }
        }

        /// <summary>
        /// Gets or sets the color to use when filling Curve serie
        /// </summary>
        public Color FillCurveColor
        {
            get => _fillCurveColor;
            set
            {
                _fillCurveColor = value;
                RequestInvalidate();
            }
        }
        #endregion

        ///<inheritdoc/>
        public override void DrawChart(ICanvas canvas, RectF dirtyRect)
        {
            //Calculates base properties(FooterH,FooterW,ItemSize,origin)
            base.DrawChart(canvas, dirtyRect);
            var points = CalculatePoints(ItemSize, Origin, HeaderHeight);
            Points = points;
            //Start drawing
            var centeredPoints = DrawPointsAndCurves(canvas, points, true); //TODO refactor this method to draw points and lines by separate

            //DisplayHeaderValues = !DisplayHorizontalAxisLines;
            if (DisplayHorizontalAxisLines)
            {
                var p1 = centeredPoints.First();
                var p2 = centeredPoints.Last();
                if (Style == LineChartStyle.Line)
                {
                    p1.X -= ItemSize.Width / 2;
                    p2.X += ItemSize.Width / 2;
                }

                DrawHorizontalStepLines(canvas, centeredPoints, Width, Height, Origin, p1.X, p2.X);
            }

            if (DisplayVerticalAxisLines && DisplayHorizontalAxisLines)
            {
                DrawVerticalStepLines(canvas, points, Origin, MaxYValueCoordinate);
            }

            DrawPointsAndCurves(canvas, points);
            DrawLabels(canvas, points, ItemSize, Height, FooterHeight, Origin);


        }

        protected override void DrawVerticalStepLines(ICanvas canvas, PointF[] points, float origin, float maxPositiveValue)
        {
            canvas.FontSize = AxisFontSize;
            canvas.StrokeSize = AxisLinesStrokeSize;
            canvas.StrokeDashPattern = AxisDashPattern;
            canvas.StrokeColor = AxisLinesColor;

            for (int i = 0; i < points.Count(); i++)
            {
                canvas.DrawLine(points.ElementAt(i).X + ItemSize.Width / 2 + (DisplayHorizontalAxisLines ? AxisXMargin : 0), origin, points.ElementAt(i).X + ItemSize.Width / 2 + (DisplayHorizontalAxisLines ? AxisXMargin : 0), maxPositiveValue);
            }

            canvas.StrokeSize = StrokeSize;
            canvas.StrokeColor = StrokeColor;
            canvas.FontSize = FontSize;
            canvas.StrokeDashPattern = new float[] { };
        }

        /// <summary>
        /// Draw points and lines or Bezier curves depending on the Style property value
        /// </summary>
        /// <param name="canvas">Current canvas</param>
        /// <param name="points">Calculated points</param>
        private PointF[] DrawPointsAndCurves(ICanvas canvas, PointF[] points, bool performCalculationsOnly = false)
        {
            List<PointF> positionatedPoints = new List<PointF>();
            if (points.Any())
            {
                //Animate
                for (int i = 0; i < points.Count(); i++)
                {
                    var height = Math.Max(2, Math.Abs(Origin - points[i].Y));
                    points[i].Y = Origin - height * AnimationProgress / 100;
                }

                canvas.StrokeSize = StrokeSize;

                //Draw Lines
                for (int i = 0; i < points.Length - 1; i++)
                {
                    var point1 = points[i];
                    var point2 = points[i + 1];
                    canvas.StrokeColor = LineColor;
                    if (Style == LineChartStyle.Line && !performCalculationsOnly)
                        canvas.DrawLine(point1.X + ItemSize.Width / 2 + (DisplayHorizontalAxisLines ? AxisXMargin : 0), point1.Y, point2.X + ItemSize.Width / 2 + (DisplayHorizontalAxisLines ? AxisXMargin : 0), point2.Y);
                }

                //Draw Points
                for (int i = 0; i < points.Length; i++)
                {
                    var point = points[i];
                    canvas.FillColor = PointColor;
                    if (Style == LineChartStyle.Line && !performCalculationsOnly)
                        canvas.FillCircle(point.X + ItemSize.Width / 2 + (DisplayHorizontalAxisLines ? AxisXMargin : 0), point.Y, PointSize);
                    positionatedPoints.Add(new PointF(point.X + ItemSize.Width / 2 + (DisplayHorizontalAxisLines ? AxisXMargin : 0), point.Y));
                }

                if (Style == LineChartStyle.Curve)
                {
                    var path = new PathF();
                    if (ExpandAndFillBackgroundCurvePath)
                    {
                        positionatedPoints[0] = new PointF(positionatedPoints[0].X - ItemSize.Width / 2, positionatedPoints[0].Y);
                        var lastPoint = positionatedPoints.Last();
                        positionatedPoints[positionatedPoints.Count - 1] = new PointF(lastPoint.X + ItemSize.Width / 2, lastPoint.Y);
                    }

                    path.MoveTo(positionatedPoints.First());
                    for (int i = 0; i < points.Length - 1; i++)
                    {
                        canvas.FillColor = PointColor;
                        var cubicInfo = CalculateCubicBezierCurve(positionatedPoints[i], positionatedPoints[i + 1], ItemSize);
                        path.CurveTo(cubicInfo.currentControl, cubicInfo.nextControl, cubicInfo.nextPoint);
                    }

                    var newRect = new RectF(0.0f, Origin, Width, Height - 100);
                    //Solid color bezier background
                    if (IsCurveBackgroundFilled)
                    {
                        var backgroundPath = new PathF(path);

                        backgroundPath.LineTo(new PointF(positionatedPoints.Last().X + 1, positionatedPoints.Last().Y));
                        backgroundPath.LineTo(new PointF(positionatedPoints.Last().X + 1, Origin));
                        backgroundPath.LineTo(new PointF(positionatedPoints[0].X - 1, Origin));
                        backgroundPath.LineTo(new PointF(positionatedPoints[0].X - 1, positionatedPoints[0].Y));
                        if (ColorBrush != null)
                        {
                            canvas.SetFillPaint(ColorBrush, newRect);
                        }
                        else
                            canvas.FillColor = FillCurveColor.WithAlpha(PathsColorOpacity);

                        if (!performCalculationsOnly)
                            canvas.FillPath(backgroundPath);

                        canvas.SetFillPaint(null, newRect);
                    }

                    //Connected curved lines
                    canvas.FillColor = LineColor;
                    if (!performCalculationsOnly)
                        canvas.DrawPath(path);

                    //After drawing paths, check if we should draw points on style =='Curve'
                    if (Style == LineChartStyle.Curve && ShowPointsForCurveStyle)
                    {
                        for (int i = 0; i < points.Length; i++)
                        {
                            var point = positionatedPoints[i];
                            canvas.FillColor = PointColor;
                            if (!performCalculationsOnly)
                                canvas.FillCircle(point.X, point.Y, PointSize);
                        }
                    }
                }
            }

            canvas.StrokeColor = StrokeColor;
            canvas.FillColor = FillColor;
            return positionatedPoints.ToArray();
        }

        /// <summary>
        /// Calculate the bezier curve for a given pair of points
        /// </summary>
        /// <param name="currentPoint">Point 0</param>
        /// <param name="nextPoint">Point 1</param>
        /// <param name="itemSize">Item size</param>
        /// <returns>Points representing the cubic bezier</returns>
        private (PointF currentControl, PointF nextPoint, PointF nextControl) CalculateCubicBezierCurve(PointF currentPoint, PointF nextPoint, SizeF itemSize)
        {
            var controlOffset = new PointF(itemSize.Width * CurveFactor, 0);
            var currentControl = new PointF(currentPoint.X + controlOffset.X, currentPoint.Y + controlOffset.Y);
            var nextControl = new PointF(nextPoint.X - controlOffset.X, nextPoint.Y - controlOffset.Y);
            return (new PointF(currentControl.X, currentControl.Y), new PointF(nextPoint.X, nextPoint.Y), new PointF(nextControl.X, nextControl.Y));
        }

    }
}
