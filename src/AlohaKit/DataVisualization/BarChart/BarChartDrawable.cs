
namespace AlohaKit.Controls
{
    public sealed class BarChartDrawable : BaseChartDrawable
    {
        #region Properties
        private bool _showBackgroundBars = true;
        private Color _backgroundBarFillColor = Color.FromArgb("#ECF1FF");
        private Color _barsFillColor = Color.FromArgb("#3E75FF");

        /// <summary>
        /// If true chart will draw background bars and value bars. If not only value bars will be drawn. Default is true
        /// </summary>
        public bool ShowBackgroundBars
        {
            get => _showBackgroundBars;
            set
            {
                _showBackgroundBars = value;
                RequestInvalidate();
            }
        }

        /// <summary>
        /// Gets or sets the  background color to use when drawing each bar
        /// </summary>
        public Color BackgroundBarsFillColor
        {
            get => _backgroundBarFillColor;
            set
            {
                _backgroundBarFillColor = value;
                RequestInvalidate();
            }
        }

        /// <summary>
        /// Gets or sets the color to use when drawing each bar
        /// </summary>
        public Color BarsFillColor
        {
            get => _barsFillColor;
            set
            {
                _barsFillColor = value;
                RequestInvalidate();
            }
        }
        #endregion

        ///<inheritdoc/>
        public override void DrawChart(ICanvas canvas, RectF dirtyRect)
        {
            base.DrawChart(canvas, dirtyRect);
            var points = CalculatePoints(ItemSize, Origin, HeaderHeight);

            PointF[] tPoints = (PointF[])points.Clone();
            if (DisplayHorizontalAxisLines)
            {
                var initialXCoordinate = points[0].X + AxisXMargin;
                var finalXCoordinate = points.Last().X + (ItemSize.Width + AxisXMargin);

                DrawHorizontalStepLines(canvas, tPoints, Width, Height, Origin, initialXCoordinate, finalXCoordinate);
            }

            //vertical lines
            if (DisplayVerticalAxisLines && DisplayHorizontalAxisLines)
            {
                DrawVerticalStepLines(canvas, points, Origin, MaxYValueCoordinate);
            }

            DrawValueAreas(canvas, points, Origin);
            DrawLabels(canvas, points, ItemSize, Height, FooterHeight, Origin); //Footer
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
        /// Draws each bar without the top point chart horizontally one by one.
        /// </summary>
        /// <param name="canvas">Canvas to draw on</param>
        /// <param name="points">Array of points to draw</param>
        /// <param name="origin">Y axis origin coordinate</param>
        private void DrawValueAreas(ICanvas canvas, PointF[] points, float origin)
        {
            if (points.Length > 0)
            {
                var maxBackgroundPoint = points.OrderBy(x => x.Y).First(); //closest value to Y:0 represents the greatest value
                maxBackgroundPoint.Y -= HeaderValuesMargin / 2;
                var maxY = Math.Min(origin, maxBackgroundPoint.Y);
                MaxYValueCoordinate = maxY;
                var maxHeight = Math.Max(2, Math.Abs(origin - maxBackgroundPoint.Y));

                var tempColor = FillColor;
                for (int i = 0; i < points.Length; i++)
                {
                    var height = Math.Max(2, Math.Abs(origin - points[i].Y));
                    points[i].Y = Origin - height * AnimationProgress / 100;

                    if (ShowBackgroundBars)
                    {
                        canvas.SetFillPaint(null, new RectF());
                        //Draw background bar first
                        canvas.FillColor = BackgroundBarsFillColor.WithAlpha(PathsColorOpacity);
                        canvas.FillRectangle(new RectF(points[i].X + (DisplayHorizontalAxisLines ? AxisXMargin : 0), maxY, ItemSize.Width, maxHeight));
                    }
                    var newRec = new RectF(points[i].X + (DisplayHorizontalAxisLines ? AxisXMargin : 0), Origin - height * AnimationProgress / 100, ItemSize.Width, height * AnimationProgress / 100);

                    if (ColorBrush != null)
                    {
                        canvas.SetFillPaint(ColorBrush, newRec);
                    }
                    else
                    {
                        canvas.SetFillPaint(null, newRec);
                        canvas.FillColor = BarsFillColor.WithAlpha(PathsColorOpacity);
                    }
                    canvas.FillRectangle(newRec);
                }

                canvas.FillColor = tempColor;
            }
        }
    }
}
