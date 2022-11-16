using Microsoft.Maui.Graphics.Text;
using AlohaKit.Extensions;
using AlohaKit.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace AlohaKit.Controls
{
    public abstract class BaseChartDrawable : IDrawable, IDisposable
    {
        #region Public Properties
        private bool _maxAxisLabelWidthDetermined = false;
        protected ICanvas _canvas { get; set; } = null;
        protected bool _displayHeaderValues = true;
        protected float MaxYValueCoordinate = 0f;
        protected int AxisXMargin = 0; //Dynamically calculated from axis label max width
        protected RectF _dirtyRect;

        private float _fontSize = 11.0f;
        private float _axisFontSize = 11.0f;
        private float _footerLabelTextSize = 10.0f;
        private Color _strokeColor = Colors.Black;
        private Color _fillColor = Colors.Transparent;
        private Color _fontColor = Colors.Black;
        private Color _footerLabelsFontColor = Colors.Black;
        private float _margin = 15f;
        private float _footerLabelsMargin = 8f;
        private float _itemSeparationMargin = 8f;
        private float _headerValuesMargin = 20f;
        private bool _enableAntialias = true;
        private IFont _font = Microsoft.Maui.Graphics.Font.Default;
        private float _strokeSize = 2.5f;
        protected bool _displayValueLabelsOnTop = true;
        private bool _isLabelTextTruncationEnabled = true;
        private ObservableCollection<ChartItem> _entries;

        private bool _displayVerticalAxisLines = false;
        private bool _displayHorizontalAxisLines = false;
        private float _pathsColorOpacity = 0.6f;

        private float _axisLinesStrokeSize = 0.9f;
        private Color _axisLineColor = Colors.LightGray;
        private float[] _axisDashPattern = new float[] { 6, 6 };
        protected Brush _colorBrush = null;
        internal EventHandler OnInvalidateRequest;

        public bool IsInitialized = false;
        public bool IsAnimating = false;
        public Brush ColorBrush
        {
            get => _colorBrush;
            set
            {
                _colorBrush = value;
                RequestInvalidate();
            }
        }

        public float[] AxisDashPattern
        {
            get => _axisDashPattern;
            set
            {
                _axisDashPattern = value;
                RequestInvalidate();
            }
        }

        public bool DisplayHeaderValues
        {
            get => _displayHeaderValues;
            set
            {
                _displayHeaderValues = value;
                RequestInvalidate();
            }
        }

        /// <summary>
        /// Sets the Alpha modifier value to use when drawing solid color backgrounds.Default is 0.6 
        /// </summary>
        public float PathsColorOpacity
        {
            get => _pathsColorOpacity;
            set
            {
                if (value > 1f) _pathsColorOpacity = 1f;
                else if (value < 0f) _pathsColorOpacity = 0f;
                else _pathsColorOpacity = value;
                RequestInvalidate();
            }
        }

        /// <summary>
        /// If true header labels will be hidden and chart will draw horizontal step lines.
        /// </summary>
        public bool DisplayHorizontalAxisLines
        {
            get => _displayHorizontalAxisLines;
            set
            {
                _displayHorizontalAxisLines = value;
                RequestInvalidate();
            }
        }

        /// <summary>
        /// If true vertical lines will be drawn as background along with horizontal lines. DisplayHorizontalAxisLines prop needs to be true as well
        /// </summary>
        public bool DisplayVerticalAxisLines
        {
            get => _displayVerticalAxisLines;
            set
            {
                _displayVerticalAxisLines = value;
                RequestInvalidate();
            }
        }

        /// <summary>
        /// Represents the percentage of the current chart Animation from 1-100
        /// </summary>
        public int AnimationProgress { get; set; } = 1;

        /// <summary>
        /// If true, chart labels text will be truncated to fit available size. Default is true
        /// </summary>
        public bool IsLabelTextTruncationEnabled
        {
            get => _isLabelTextTruncationEnabled;
            set
            {
                _isLabelTextTruncationEnabled = value;
                RequestInvalidate();
            }
        }

        /// <summary>
        /// If true, chart will show value labels on top of canvas. Default is true
        /// </summary>
        public bool DisplayValueLabelsOnTop
        {
            get => _displayValueLabelsOnTop;
            set
            {
                _displayValueLabelsOnTop = value;
                RequestInvalidate();
            }
        }

        /// <summary>
        /// Stroke size. Default is 2.5
        /// </summary>
        public float StrokeSize
        {
            get => _strokeSize;
            set
            {
                _strokeSize = value;
                RequestInvalidate();
            }
        }

        /// <summary>
        /// Axis lines stroke size. Default is 0.9
        /// </summary>
        public float AxisLinesStrokeSize
        {
            get => _axisLinesStrokeSize;
            set
            {
                _axisLinesStrokeSize = value;
                RequestInvalidate();
            }
        }

        /// <summary>
        /// Axis lines color. Default is Lightgray
        /// </summary>
        public Color AxisLinesColor
        {
            get => _axisLineColor;
            set
            {
                _axisLineColor = value;
                RequestInvalidate();
            }
        }

        /// <summary>
        /// Stroke color. Default is Black
        /// </summary>
        public Color StrokeColor
        {
            get => _strokeColor;
            set
            {
                _strokeColor = value;
                RequestInvalidate();
            }
        }

        /// <summary>
        /// Gets or sets the separation margin between each item. Default is 8
        /// </summary>
        public float ItemSeparationMargin
        {
            get => _itemSeparationMargin;
            set
            {
                _itemSeparationMargin = value;
                RequestInvalidate();
            }
        }

        /// <summary>
        /// Gets or sets font size to use for footer label values. Default is 10
        /// </summary>
        public float FooterLabelsTextSize
        {
            get => _footerLabelTextSize;
            set
            {
                _footerLabelTextSize = value;
                RequestInvalidate();
            }
        }

        /// <summary>
        /// Gets or sets font margin to use when calculating footer labels coordinates. Default is 8
        /// </summary>
        public float FooterLabelsMargin
        {
            get => _footerLabelsMargin;
            set
            {
                _footerLabelsMargin = value;
                RequestInvalidate();
            }
        }

        /// <summary>
        /// Gets or sets font margin to use when calculating header value labels coordinates. Default is 30
        /// </summary>
        public float HeaderValuesMargin
        {
            get => _headerValuesMargin;
            set
            {
                _headerValuesMargin = value;
                RequestInvalidate();
            }
        }

        /// <summary>
        /// Gets or sets the canvas default fill color. Default is transparent
        /// </summary>
        public Color FillColor
        {
            get => _fillColor;
            set
            {
                _fillColor = value;
                RequestInvalidate();
            }
        }

        /// <summary>
        /// Gets or sets the canvas default Font to use when drawing strings. [10/10/22 -> GRAPHICS MISSING SUPPORT FOR CUSTOM FONTS]
        /// </summary>
        public IFont Font
        {
            get => _font;
            set
            {
                _font = value;
                RequestInvalidate();
            }
        }

        /// <summary>
        /// Gets or sets the items to display within canvas
        /// </summary>
        public ObservableCollection<ChartItem> Entries
        {
            get => _entries;
            set
            {
                if (value != null)
                {
                    _entries = value;
                    foreach (var entry in value)
                    {
                        entry.PropertyChanged += Entry_PropertyChanged;
                    }

                    _entries.CollectionChanged += Entries_CollectionChanged;
                }
            }
        }

        protected virtual void Entries_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (_canvas != null) RequestInvalidate();
        }

        protected virtual void Entry_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (_canvas != null) RequestInvalidate();
        }

        /// <summary>
        /// Gets or sets font size to use when drawing value labels. Default is 11
        /// </summary>
        public float FontSize
        {
            get => _fontSize;
            set
            {
                _fontSize = value;
                RequestInvalidate();
            }
        }

        /// <summary>
        /// Gets or sets font size to use when drawing chart axis labels. Default is 11
        /// </summary>
        public float AxisFontSize
        {
            get => _axisFontSize;
            set
            {
                _axisFontSize = value;
                RequestInvalidate();
            }
        }

        /// <summary>
        /// Gets or sets the default top and bottom internal margin to use when drawing canvas content. Default value is 15
        /// </summary>
        public float Margin
        {
            get => _margin;
            set
            {
                _margin = value;
                RequestInvalidate();
            }
        }

        /// <summary>
        /// Gets or sets the color to use when drawing value labels. Default is Black
        /// </summary>
        public Color FontColor
        {
            get => _fontColor;
            set
            {
                _fontColor = value;
                RequestInvalidate();
            }
        }

        /// <summary>
        /// Gets or sets the color to use when drawing footer labels. Default is Black
        /// </summary>
        public Color FooterLabelsFontColor
        {
            get => _footerLabelsFontColor;
            set
            {
                _footerLabelsFontColor = value;
                RequestInvalidate();
            }
        }

        /// <summary>
        /// Gets or sets if the current canvas will use Antialias mode or not when drawing content. Default is true
        /// </summary>
        public bool EnableAntialias
        {
            get => _enableAntialias;
            set
            {
                _enableAntialias = value;
                RequestInvalidate();
            }
        }
        #endregion

        #region Internal Properties
        protected PointF[] Points;

        /// <summary>
        /// Gets or sets the size for each value column
        /// </summary>
        protected SizeF ItemSize { get; set; }

        /// <summary>
        /// Gets the smallest value within ChartEntries collection
        /// </summary>
        protected float MinValue
        {
            get
            {
                //0 is the Min value by Default unless negative values are detected
                var minInEntries = (float)Entries.Min((x) => x.Value);
                return minInEntries < 0 ? minInEntries : 0;
            }
        }

        /// <summary>
        /// Gets the greatest value within ChartEntries collection
        /// </summary>
        protected float MaxValue
        {
            get
            {
                return Entries.Max((x) => x.Value);
            }
        }

        /// <summary>
        /// Gets or sets control Width
        /// </summary>
        protected float Width { get; set; }

        /// <summary>
        /// Gets or sets control height
        /// </summary>
        protected float Height { get; set; }

        /// <summary>
        /// gets or sets the origin in Y axis
        /// </summary>
        protected float Origin { get; set; }

        /// <summary>
        /// Gets or sets Header height
        /// </summary>
        protected float HeaderHeight { get; set; }

        /// <summary>
        /// Gets or sets Footer height
        /// </summary>
        protected float FooterHeight { get; set; }

        /// <summary>
        /// Gets or sets chart value range by subtracting MaxValue from MinValue.
        /// </summary>
        protected float ValueRange => MaxValue - MinValue;
        #endregion

        /// <summary>
        /// Trigger Invalidate() method on ChartView()
        /// </summary>
        protected void RequestInvalidate() => OnInvalidateRequest?.Invoke(this, null);

        /// <summary>
        /// Calculate the center for a given rectangle
        /// </summary>
        /// <param name="coordinateX">X axis coordinate</param>
        /// <param name="rect">Rectangle container to be centered</param>
        /// <returns>Number indicating the center X coordinate within input rectangle</returns>
        protected virtual float CalculatePaddingForLabelValue(float coordinateX, SizeF rect)
        {
            return coordinateX - rect.Width / 2;
        }

        /// <summary>
        /// For a given value, calculates its Y coordinate within current canvas
        /// </summary>
        /// <param name="value">Value to calculate</param>
        /// <param name="itemSize">Item available size</param>
        /// <param name="origin">Y coordinate origin coordinate</param>
        /// <param name="headerHeight">Header height</param>
        /// <returns>Y Coordinate of given value</returns>
        protected virtual PointF CalculateHorizontalYLineCoordinate(float value, SizeF itemSize, float origin, float headerHeight)
        {
            var y = headerHeight + (MaxValue - value) / ValueRange * itemSize.Height;
            return new PointF(0, y);
        }

        protected abstract void DrawVerticalStepLines(ICanvas canvas, PointF[] points, float origin, float maxPositiveValue);
        /// <summary>
        /// Draw horizontal lines in Y Axis
        /// </summary>
        /// <param name="canvas">Current canvas</param>
        /// <param name="points">Current chart calculated points</param>
        /// <param name="width">Current canvas total Width</param>
        /// <param name="height">Current Canvas total Height</param>
        /// <param name="origin">Y origin coordinate</param>
        /// <param name="originX">X coordinate that determines where the line should start</param>
        /// <param name="finalX">X coordinate that determines where the line should end</param>
        /// <param name="drawGrid">If true, complementary vertical lines will be drawn so background will look as a Grid</param>
        protected virtual void DrawHorizontalStepLines(ICanvas canvas, IEnumerable<PointF> points, float width, float height, float origin, float originX, float finalX)
        {
            canvas.FontSize = AxisFontSize;
            canvas.StrokeSize = AxisLinesStrokeSize;
            canvas.StrokeDashPattern = AxisDashPattern;
            canvas.StrokeColor = AxisLinesColor;

            var linesLimit = 3;
            var maxPoint = points.OrderBy(x => x.Y).First();
            var maxEntry = Entries.ElementAt(points.ToList().IndexOf(maxPoint)).Value;
            var entriesContainsNegativeValue = Entries.Any(value => value.Value < 0);
            var bounds = new Rect();
            var text = "0";
            var maxPositiveValue = 0f;
            var xOrigin = points.First().X;

            //look for negative values
            if (entriesContainsNegativeValue)
            {
                //Convert it into positive
                var maxMinValue = Entries.Min(point => point.Value) * -1;
                if (maxMinValue > maxEntry)
                {
                    maxEntry = maxMinValue;
                    maxPoint = points.OrderBy(x => x.Y).Last();
                }
            }

            var step = (float)(maxEntry / 3);
            maxEntry = Entries.Take(Entries.Count()).Max(point => point.Value);

            if (!entriesContainsNegativeValue)
            {
                step = maxEntry.GetStepForPositiveAxis();
                maxEntry += step;
            }

            var xLabelsOrigin = AxisXMargin;
            var measuredText = canvas.GetStringSize(text, Font, AxisFontSize);
            bounds = new Rect(CalculatePaddingForLabelValue(xLabelsOrigin / 2, measuredText), origin - measuredText.Height / 2, measuredText.Width, measuredText.Height);
            canvas.DrawString(text, bounds, HorizontalAlignment.Center, VerticalAlignment.Center, TextFlow.ClipBounds);

            if (step <= 0) step = 1;

            //Origin line
            canvas.DrawLine(originX, origin - 1, finalX, origin - 1);

            if (!entriesContainsNegativeValue)
                for (float i = step; i <= 3 * step; i += step)
                {
                    var newLineCoordinate = CalculateHorizontalYLineCoordinate(i, ItemSize, Origin, HeaderHeight);

                    if (i >= 1000) text = Convert.ToDecimal(i).ToKMBString(isRounded: true);
                    else text = Convert.ToDecimal(i).ToString("0.#");

                    maxPositiveValue = newLineCoordinate.Y;  //Remove Point Height to adjust line position
                    MaxYValueCoordinate = maxPositiveValue;
                    canvas.DrawLine(originX, maxPositiveValue, finalX, maxPositiveValue);
                    measuredText = canvas.GetStringSize(text + ".", Font, AxisFontSize);
                    if (measuredText.Width > AxisXMargin) AxisXMargin = (int)measuredText.Width;

                    bounds = new Rect(CalculatePaddingForLabelValue(xLabelsOrigin / 2, measuredText), origin - Math.Max(2, Math.Abs(origin - maxPositiveValue) + measuredText.Height / 2), measuredText.Width, measuredText.Height);
                    canvas.DrawString(text, bounds, HorizontalAlignment.Center, VerticalAlignment.Center, TextFlow.ClipBounds);

                    if (linesLimit-- == 0) break;
                }
            else
            {
                //TODO Add support for negative values
            }

            canvas.StrokeSize = StrokeSize;
            canvas.StrokeColor = StrokeColor;
            canvas.FontSize = FontSize;
            canvas.StrokeDashPattern = new float[] { };

            if (!_maxAxisLabelWidthDetermined)
            {
                _maxAxisLabelWidthDetermined = true;
                //AxisXMargin += 10;//add a little extra separator so we have enough space left and right
                RequestInvalidate();
            }
        }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            if (Entries is null || !Entries.Any() || !IsInitialized) return;
            _canvas = canvas;
            _dirtyRect = dirtyRect;

            _canvas.Font = Font;
            _canvas.FillColor = FillColor;
            _canvas.FontColor = FontColor;
            _canvas.StrokeSize = StrokeSize;
            _canvas.StrokeColor = StrokeColor;
            _canvas.FontSize = FontSize;

            ClearBackground(_canvas, _dirtyRect.Size, FillColor);
            _canvas.Antialias = EnableAntialias;
            DrawChart(_canvas, _dirtyRect);

        }

        /// <summary>
        /// Gets the size for labels container rectangle by measuring text length
        /// </summary>
        /// <returns>Array with labels rectangle containers</returns>
        protected SizeF[] MeasureLabels()
        {
            return Entries?.Select(entry =>
            {
                if (string.IsNullOrEmpty(entry.Label)) return SizeF.Zero;

                var textRectangle = new SizeF();
                var text = entry.Label;
                var measuredText = _canvas.GetStringSize(text, Font, FontSize);
                textRectangle.Width = measuredText.Width;
                textRectangle.Height = measuredText.Height;

                return textRectangle;
            }).ToArray();

        }

        /// <summary>
        /// Calculates footer total height
        /// </summary>
        /// <param name="valueLabelSizes">Label size</param>
        /// <returns>Labels height</returns>
        protected virtual float CalculateFooterHeight(SizeF[] valueLabelSizes)
        {
            var result = FooterLabelsMargin;

            if (Entries.Any(e => !string.IsNullOrEmpty(e.Label)))
            {
                result += FooterLabelsTextSize;
            }

            return result;
        }

        /// <summary>
        /// Calculates header height
        /// </summary>
        /// <param name="valueLabelSizes">Label size</param>
        /// <returns>Header total height</returns>
        protected virtual float CalculateHeaderHeight(SizeF[] valueLabelSizes)
        {
            var result = Margin;

            if (Entries.Any())
            {
                var maxValueHeight = valueLabelSizes.Max(x => x.Height);
                if (maxValueHeight > 0)
                {
                    //If showing axis, get rid of allocated header space so our chart values can grow higher
                    result += maxValueHeight + (DisplayHorizontalAxisLines ? HeaderValuesMargin / 2 : HeaderValuesMargin);
                }
            }

            return result;
        }

        /// <summary>
        /// Calculates the size chart is going to use when drawing each bar
        /// </summary>
        /// <param name="width">Control width</param>
        /// <param name="height">Control height</param>
        /// <param name="footerHeight">Chart footer label area height</param>
        /// <param name="headerHeight">Chart header area height</param>
        /// <returns>The size for each bar</returns>
        protected virtual SizeF CalculateItemSize(float width, float height, float footerHeight, float headerHeight)
        {
            var total = Entries.Count();
            var w = (width - (float)ItemSeparationMargin - (DisplayHorizontalAxisLines ? AxisXMargin : 0) - total * ItemSeparationMargin / 2) / total;
            var h = height - Margin - footerHeight - headerHeight;
            return new SizeF(w, h);
        }

        /// <summary>
        /// Calculates the size chart is going to use when drawing each bar
        /// </summary>
        /// <param name="width">Control width</param>
        /// <param name="height">Control height</param>
        /// <param name="footerHeight">Chart footer label area height</param>
        /// <param name="headerHeight">Chart header area height</param>
        /// <returns>The size for each bar</returns>
        protected virtual SizeF CalculateItemSize(List<ChartItem> entries, float width, float height, float footerHeight, float headerHeight)
        {
            var total = entries.Count();
            var w = (width - (float)ItemSeparationMargin - (DisplayHorizontalAxisLines ? AxisXMargin : 0) - total * ItemSeparationMargin / 2) / total;
            var h = height - Margin - footerHeight - headerHeight;
            return new SizeF(w, h);
        }

        /// <summary>
        /// Calculates and draws the current chart with the Entries[] data
        /// </summary>
        /// <param name="canvas">Current canvas</param>
        /// <param name="dirtyRect">Current canvas rect</param>
        public virtual void DrawChart(ICanvas canvas, RectF dirtyRect)
        {
            if (canvas == null) return;
            Width = dirtyRect.Width;
            Height = dirtyRect.Height;

            //Calculate coordinates for every item within canvas
            var valueLabelSizes = MeasureLabels();
            FooterHeight = CalculateFooterHeight(valueLabelSizes);
            HeaderHeight = CalculateHeaderHeight(valueLabelSizes);
            ItemSize = CalculateItemSize(Width, Height, FooterHeight, HeaderHeight);
            Origin = CalculateYOrigin(ItemSize.Height, HeaderHeight);
        }

        /// <summary>
        /// Change current canvas background fill color
        /// </summary>
        /// <param name="canvas">Current canvas</param>
        /// <param name="containerSize">Canvas size</param>
        /// <param name="fillColor">Fill color</param>
        public virtual void ClearBackground(ICanvas canvas, SizeF containerSize, Color fillColor)
        {
            canvas.FillColor = fillColor;
            canvas.FillRectangle(0, 0, containerSize.Width, containerSize.Height);
        }

        /// <summary>
        /// Calculates the Y axis origin value
        /// </summary>
        /// <param name="itemHeight">Each bar width</param>
        /// <param name="headerHeight">Control total height</param>
        /// <returns>Y axis origin coordinate</returns>
        protected float CalculateYOrigin(float itemHeight, float headerHeight)
        {
            if (MaxValue <= 0) return headerHeight;
            if (MinValue > 0) return headerHeight + itemHeight;
            return headerHeight + MaxValue / ValueRange * itemHeight;
        }

        /// <summary>
        /// Calculates the coordinates of each value within the chart canvas
        /// </summary>
        /// <param name="itemSize">Item size</param>
        /// <param name="origin">Y axis origin coordinate</param>
        /// <param name="headerHeight">Chart header area height</param>
        /// <returns>Array with the points coordinates</returns>
        protected virtual PointF[] CalculatePoints(SizeF itemSize, float origin, float headerHeight)
        {
            var result = new List<PointF>();

            for (int i = 0; i < Entries.Count(); i++)
            {
                var entry = Entries.ElementAt(i);

                var x = itemSize.Width / 2 + i * (itemSize.Width + ItemSeparationMargin / 2);
                x -= itemSize.Width / 2 - ItemSeparationMargin / 2;

                var y = headerHeight + (MaxValue - entry.Value) / ValueRange * itemSize.Height;

                var point = new PointF(x, y);
                result.Add(point);
            }

            return result.ToArray();
        }


        /// <summary>
        /// Calculates the coordinates of each value within the chart canvas
        /// </summary>
        /// <param name="itemSize">Item size</param>
        /// <param name="origin">Y axis origin coordinate</param>
        /// <param name="headerHeight">Chart header area height</param>
        /// <returns>Array with the points coordinates</returns>
        protected virtual PointF[] CalculatePoints(List<ChartItem> entries, SizeF itemSize, float origin, float headerHeight)
        {
            var result = new List<PointF>();

            for (int i = 0; i < entries.Count(); i++)
            {
                var entry = entries.ElementAt(i);

                var x = itemSize.Width / 2 + i * (itemSize.Width + ItemSeparationMargin / 2);
                x -= itemSize.Width / 2 - ItemSeparationMargin / 2;

                var y = headerHeight + (MaxValue - entry.Value) / ValueRange * itemSize.Height;

                var point = new PointF(x, y);
                result.Add(point);
            }

            return result.ToArray();
        }

        /// <summary>
        /// Draws footer and value labels on current canvas
        /// </summary>
        /// <param name="canvas">Canvas to draw on</param>
        /// <param name="points">Coordinates for each footer label</param>
        /// <param name="itemSize">Item size</param>
        /// <param name="height">Control height</param>
        /// <param name="footerHeight">Chart footer area height</param>
        /// <param name="origin">Y axis origin coordinate</param>
        protected virtual void DrawLabels(List<ChartItem> entries, ICanvas canvas, PointF[] points, SizeF itemSize, float height, float footerHeight, float origin)
        {
            var minPoint = points.Max(x => x.Y);
            var tempColor = FontColor;

            for (int i = 0; i < entries.Count(); i++)
            {
                var entry = entries.ElementAt(i);
                var point = points[i];
                var tmpFont = Font;

                if (!string.IsNullOrEmpty(entry.Label))
                {
                    if (entry.IsLabelBold)
                    {
                        canvas.Font = Microsoft.Maui.Graphics.Font.DefaultBold;
                    }

                    //BOTTOM label values
                    var bounds = new RectF();
                    var text = entry.Label;
                    var strSize = canvas.GetStringSize(text, Font, FooterLabelsTextSize);
                    bounds.Width = itemSize.Width;

                    bounds.Height = strSize.Height;
                    var labelY = height - (Margin + FooterLabelsTextSize / 2);
                    bounds.Y = labelY - bounds.Height / 2;
                    bounds.X = point.X + itemSize.Width / 2 - bounds.Width / 2;
                    //For vertical axis only move everything to the right so axis values are able to be displayed correctly
                    if (DisplayHorizontalAxisLines)
                    {
                        bounds.X += AxisXMargin;
                    }

                    if (strSize.Width >= Math.Round(itemSize.Width, MidpointRounding.AwayFromZero) - 1)
                    {
                        text = TruncateText(canvas, itemSize, text);
                    }

                    var footerLastSize = bounds.Height;
                    canvas.FontSize = FooterLabelsTextSize;
                    canvas.FontColor = FooterLabelsFontColor;
                    canvas.DrawString(text, bounds, HorizontalAlignment.Center, VerticalAlignment.Center, TextFlow.ClipBounds);
                    canvas.Font = tmpFont;

                    //TOP value labels
                    if (!DisplayHeaderValues)//not display header if an axis style is being used
                    {
                        continue;
                    }

                    if (entry.IsValueBold)
                    {
                        canvas.Font = Microsoft.Maui.Graphics.Font.DefaultBold;
                    }

                    bounds = new RectF();
                    text = entry.Value.ToString();
                    if (entry.Value >= 1000)
                    {
                        text = Convert.ToDecimal(entry.Value).ToKMBString(isRounded: true);
                    }

                    strSize = canvas.GetStringSize(text, Font, FontSize);
                    bounds.Width = itemSize.Width;
                    bounds.Height = strSize.Height;

                    if (DisplayValueLabelsOnTop)
                    {
                        labelY = height - origin - (HeaderValuesMargin / 2 + footerLastSize / 2);
                        bounds.Y = labelY - bounds.Height / 2;
                        bounds.X = point.X + itemSize.Width / 2 - bounds.Width / 2;
                    }
                    else
                    {
                        bounds.Y = point.Y - HeaderValuesMargin / 2 - bounds.Height;
                        bounds.X = point.X + itemSize.Width / 2 - bounds.Width / 2;
                    }

                    //For vertical axis only move everything to the right so axis values are able to be displayed correctly
                    if (DisplayHorizontalAxisLines)
                    {
                        bounds.X += AxisXMargin;
                    }

                    if (strSize.Width >= Math.Round(itemSize.Width, MidpointRounding.AwayFromZero) - 1)
                    {
                        text = TruncateText(canvas, itemSize, text);
                    }

                    canvas.FontColor = FontColor;
                    canvas.FontSize = FontSize;
                    canvas.DrawString(text, bounds, HorizontalAlignment.Center, VerticalAlignment.Center, TextFlow.ClipBounds);
                    canvas.Font = tmpFont;
                }
            }
        }

        /// <summary>
        /// Draws footer and value labels on current canvas
        /// </summary>
        /// <param name="canvas">Canvas to draw on</param>
        /// <param name="points">Coordinates for each footer label</param>
        /// <param name="itemSize">Item size</param>
        /// <param name="height">Control height</param>
        /// <param name="footerHeight">Chart footer area height</param>
        /// <param name="origin">Y axis origin coordinate</param>
        protected virtual void DrawLabels(ICanvas canvas, PointF[] points, SizeF itemSize, float height, float footerHeight, float origin)
        {
            var minPoint = points.Max(x => x.Y);
            var tempColor = FontColor;
            for (int i = 0; i < Entries.Count(); i++)
            {
                var entry = Entries.ElementAt(i);
                var point = points[i];
                var tmpFont = Font;

                if (!string.IsNullOrEmpty(entry.Label))
                {
                    if (entry.IsLabelBold)
                    {
                        canvas.Font = Microsoft.Maui.Graphics.Font.DefaultBold;
                    }

                    //BOTTOM label values
                    var bounds = new RectF();
                    var text = entry.Label;
                    var strSize = canvas.GetStringSize(text, Font, FooterLabelsTextSize);
                    bounds.Width = itemSize.Width;

                    bounds.Height = strSize.Height;
                    var labelY = height - (Margin + FooterLabelsTextSize / 2);
                    bounds.Y = labelY - bounds.Height / 2;
                    bounds.X = point.X + itemSize.Width / 2 - bounds.Width / 2;
                    //For vertical axis only move everything to the right so axis values are able to be displayed correctly
                    if (DisplayHorizontalAxisLines)
                    {
                        bounds.X += AxisXMargin;
                    }

                    if (strSize.Width >= Math.Round(itemSize.Width, MidpointRounding.AwayFromZero) - 1)
                    {
                        text = TruncateText(canvas, itemSize, text);
                    }

                    var footerLastSize = bounds.Height;
                    canvas.FontSize = FooterLabelsTextSize;
                    canvas.FontColor = FooterLabelsFontColor;
                    canvas.DrawString(text, bounds, HorizontalAlignment.Center, VerticalAlignment.Center, TextFlow.ClipBounds);
                    canvas.Font = tmpFont;

                    //TOP value labels
                    if (!DisplayHeaderValues)//not display header if an axis style is being used
                    {
                        continue;
                    }

                    if (entry.IsValueBold)
                    {
                        canvas.Font = Microsoft.Maui.Graphics.Font.DefaultBold;
                    }

                    bounds = new RectF();
                    text = entry.Value.ToString();
                    if (entry.Value >= 1000)
                    {
                        text = Convert.ToDecimal(entry.Value).ToKMBString(isRounded: true);
                    }

                    strSize = canvas.GetStringSize(text, Font, FontSize);
                    bounds.Width = itemSize.Width;
                    bounds.Height = strSize.Height;

                    if (DisplayValueLabelsOnTop)
                    {
                        labelY = height - origin - (HeaderValuesMargin / 2 + footerLastSize / 2);
                        bounds.Y = labelY - bounds.Height / 2;
                        bounds.X = point.X + itemSize.Width / 2 - bounds.Width / 2;
                    }
                    else
                    {
                        bounds.Y = point.Y - HeaderValuesMargin / 2 - bounds.Height;
                        bounds.X = point.X + itemSize.Width / 2 - bounds.Width / 2;
                    }

                    //For vertical axis only move everything to the right so axis values are able to be displayed correctly
                    if (DisplayHorizontalAxisLines)
                    {
                        bounds.X += AxisXMargin;
                    }

                    if (strSize.Width >= Math.Round(itemSize.Width, MidpointRounding.AwayFromZero) - 1)
                    {
                        text = TruncateText(canvas, itemSize, text);
                    }

                    canvas.FontColor = FontColor;
                    canvas.FontSize = FontSize;
                    canvas.DrawString(text, bounds, HorizontalAlignment.Center, VerticalAlignment.Center, TextFlow.ClipBounds);
                    canvas.Font = tmpFont;
                }
            }
        }

        /// <summary>
        /// Allows long words to be shortened by removing characters so it can fit within the provided area
        /// </summary>
        /// <param name="canvas">Canvas to draw on</param>
        /// <param name="availableSize">Represents the available width</param>
        /// <param name="text">Text to truncate within the available area</param>
        /// <returns>Truncated text</returns>
        protected string TruncateText(ICanvas canvas, SizeF availableSize, string text)
        {
            if (string.IsNullOrEmpty(text)) return "";
            if (!IsLabelTextTruncationEnabled) return text;
            var strSize = canvas.GetStringSize(text, Font, FooterLabelsTextSize);
            var differenceSize = strSize.Width - availableSize.Width;
            var charSizes = new List<SizeF>();
            text.ToList().ForEach(c =>
            {
                charSizes.Add(canvas.GetStringSize(c.ToString(), Font, FooterLabelsTextSize));
            });

            var removedCharsCount = 0f;
            for (int j = text.Length; j > 0; j--)
            {
                text = text.Substring(0, text.Length - 1);
                removedCharsCount += charSizes[j - 1].Width;
                if (removedCharsCount >= differenceSize) break;
            }

            try
            {
                if (text.Length > 2)
                {
                    //Replace the last char by '..'
                    text = text.Substring(0, text.Length - 2) + "..";
                }
            }
            catch
            {
                if (text.Length > 1)
                {
                    //Replace the last char by '..'
                    text = text.Substring(0, text.Length - 1) + "..";
                }
            }

            return text;
        }

        public virtual void Dispose()
        {
            _entries.CollectionChanged -= Entries_CollectionChanged;
            foreach (var entry in _entries)
            {
                entry.PropertyChanged -= Entry_PropertyChanged;
            }
        }
    }
}
