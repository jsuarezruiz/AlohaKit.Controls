using static AlohaKit.Enums.ChartEnums;

namespace AlohaKit.Controls
{
	/// <summary>
	/// The LineChart provides a drawn control to visualize data as a line chart. 
	/// It allows data points to be represented by a continuous line, making it ideal for showing trends over time or relationships between variables. 
	///
	/// This class inherits from BaseChart, leveraging shared charting functionality while adding line-specific rendering features.
	/// </summary>
	public sealed class LineChart : BaseChart
    {
        private LineChartDrawable _currentChart = new LineChartDrawable();

        #region DependencyProperties

        public static readonly BindableProperty ExpandAndFillBackgroundCurvePathProperty = BindableProperty.Create(nameof(ExpandAndFillBackgroundCurvePath), typeof(bool), typeof(LineChart), false, propertyChanged: (bindableObject, oldValue, newValue) =>
       {
           var cc = (LineChart)bindableObject;
           cc._currentChart.ExpandAndFillBackgroundCurvePath = (bool)newValue;
       });

        /// <summary>
        /// If true Bezier curve Background Path color will expand and fill when drawn.
        /// </summary>
        public bool ExpandAndFillBackgroundCurvePath
        {
            get => (bool)GetValue(ExpandAndFillBackgroundCurvePathProperty);
            set => SetValue(ExpandAndFillBackgroundCurvePathProperty, value);
        }

        public static readonly BindableProperty ShowPointsForCurveStyleProperty = BindableProperty.Create(nameof(ShowPointsForCurveStyle), typeof(bool), typeof(LineChart), false, propertyChanged: (bindableObject, oldValue, newValue) =>
         {
             var cc = (LineChart)bindableObject;
             cc._currentChart.ShowPointsForCurveStyle = (bool)newValue;
         });

        /// <summary>
        /// If true and Style prop equals to 'Curve' points will be shown when drawing cubic bezier
        /// </summary>
        public bool ShowPointsForCurveStyle
        {
            get => (bool)GetValue(ShowPointsForCurveStyleProperty);
            set => SetValue(ShowPointsForCurveStyleProperty, value);
        }

        public static readonly BindableProperty IsCurveBackgroundFilledProperty = BindableProperty.Create(nameof(IsCurveBackgroundFilled), typeof(bool), typeof(LineChart), true, propertyChanged: (bindableObject, oldValue, newValue) =>
          {
              var cc = (LineChart)bindableObject;
              cc._currentChart.IsCurveBackgroundFilled = (bool)newValue;
          });

        /// <summary>
        /// Gets or sets if the curve path will have a solid color background when drawn.
        /// </summary>
        public bool IsCurveBackgroundFilled
        {
            get => (bool)GetValue(IsCurveBackgroundFilledProperty);
            set => SetValue(IsCurveBackgroundFilledProperty, value);
        }


        public static readonly BindableProperty CurveFactorProperty = BindableProperty.Create(nameof(CurveFactor), typeof(float), typeof(LineChart), 0.6f, propertyChanged: (bindableObject, oldValue, newValue) =>
        {
            var cc = (LineChart)bindableObject;
            cc._currentChart.CurveFactor = (float)newValue;
        });

        /// <summary>
        /// Sets how 'curvy' the bezier curve will be when drawn. Accepts values between 0-1. Default is 0.6
        /// </summary>
        public float CurveFactor
        {
            get => (float)GetValue(CurveFactorProperty);
            set => SetValue(CurveFactorProperty, value);
        }

        public static readonly BindableProperty ChartStyleProperty = BindableProperty.Create(nameof(ChartStyle), typeof(LineChartStyle), typeof(LineChart), LineChartStyle.Line, propertyChanged: (bindableObject, oldValue, newValue) =>
       {
           var cc = (LineChart)bindableObject;
           cc._currentChart.Style = (LineChartStyle)newValue;
       });

        /// <summary>
        /// Defines the style for the current LineChart
        /// </summary>
        public LineChartStyle ChartStyle
        {
            get => (LineChartStyle)GetValue(ChartStyleProperty);
            set => SetValue(ChartStyleProperty, value);
        }

        public static readonly BindableProperty PointSizeProperty = BindableProperty.Create(nameof(PointSize), typeof(float), typeof(LineChart), 5f, propertyChanged: (bindableObject, oldValue, newValue) =>
        {
            var cc = (LineChart)bindableObject;
            cc._currentChart.PointSize = (float)newValue;
        });

        /// <summary>
        /// Gets or sets the  radius for each point.
        /// </summary>
        public float PointSize
        {
            get => (float)GetValue(PointSizeProperty);
            set => SetValue(PointSizeProperty, value);
        }

        public static readonly BindableProperty PointColorProperty = BindableProperty.Create(nameof(PointColor), typeof(Color), typeof(LineChart), Color.FromArgb("#94B3FF"), propertyChanged: (bindableObject, oldValue, newValue) =>
         {
             var cc = (LineChart)bindableObject;
             cc._currentChart.PointColor = (Color)newValue;
         });

        /// <summary>
        /// Gets or sets the color for each dot.
        /// </summary>
        public Color PointColor
        {
            get => (Color)GetValue(PointColorProperty);
            set => SetValue(PointColorProperty, value);
        }

        public static readonly BindableProperty LineColorProperty = BindableProperty.Create(nameof(LineColor), typeof(Color), typeof(LineChart), Color.FromArgb("#94B3FF"), propertyChanged: (bindableObject, oldValue, newValue) =>
         {
             var cc = (LineChart)bindableObject;
             cc._currentChart.LineColor = (Color)newValue;
         });

        /// <summary>
        /// Gets or sets the color to use when drawing chart lines.
        /// </summary>
        public Color LineColor
        {
            get => (Color)GetValue(LineColorProperty);
            set => SetValue(LineColorProperty, value);
        }

        public static readonly BindableProperty FillCurveColorProperty = BindableProperty.Create(nameof(FillCurveColor), typeof(Color), typeof(LineChart), Color.FromArgb("#ECF1FF"), propertyChanged: (bindableObject, oldValue, newValue) =>
         {
             var cc = (LineChart)bindableObject;
             cc._currentChart.FillCurveColor = (Color)newValue;
         });

        /// <summary>
        /// Gets or sets the color to use when filling Curve serie
        /// </summary>
        public Color FillCurveColor
        {
            get => (Color)GetValue(FillCurveColorProperty);
            set => SetValue(FillCurveColorProperty, value);
        }
        #endregion

        public LineChart()
        {
            Drawable = _currentChart;
        }
    }
}