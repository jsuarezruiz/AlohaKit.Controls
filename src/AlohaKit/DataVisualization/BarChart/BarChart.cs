
namespace AlohaKit.Controls
{
	/// <summary>
	/// The BarChart is a drawn control used to render bar charts, allowing for the visualization of data as rectangular bars. 
	/// Each bar's length or height corresponds to the value it represents. 
	/// 
	/// It extends the BaseChart class, providing additional functionality tailored to bar chart rendering while leveraging shared chart capabilities.
	/// </summary>
	public sealed class BarChart : BaseChart
    {

        private BarChartDrawable _currentChart = new BarChartDrawable();

        #region DependencyProperties
        public static readonly BindableProperty ShowBackgroundBarsProperty = BindableProperty.Create(nameof(ShowBackgroundBars), typeof(bool), typeof(BarChart), true, propertyChanged: (bindableObject, oldValue, newValue) =>
         {
             var cc = (BarChart)bindableObject;
             cc._currentChart.ShowBackgroundBars = (bool)newValue;
         });

        /// <summary>
        /// If true chart will draw background bars and value bars. If not only value bars will be drawn. Default is true
        /// </summary>
        public bool ShowBackgroundBars
        {
            get => (bool)GetValue(ShowBackgroundBarsProperty);
            set => SetValue(ShowBackgroundBarsProperty, value);
        }

        public static readonly BindableProperty BackgroundBarsFillColorProperty = BindableProperty.Create(nameof(BackgroundBarsFillColor), typeof(Color), typeof(BarChart), Color.FromArgb("#ECF1FF"), propertyChanged: (bindableObject, oldValue, newValue) =>
         {
             var cc = (BarChart)bindableObject;
             cc._currentChart.BackgroundBarsFillColor = (Color)newValue;
         });

        /// <summary>
        /// Gets or sets the  background color to use when drawing each bar
        /// </summary>
        public Color BackgroundBarsFillColor
        {
            get => (Color)GetValue(BackgroundBarsFillColorProperty);
            set => SetValue(BackgroundBarsFillColorProperty, value);
        }

        public static readonly BindableProperty BarsFillColorProperty = BindableProperty.Create(nameof(BarsFillColor), typeof(Color), typeof(BarChart), Color.FromArgb("#3E75FF"), propertyChanged: (bindableObject, oldValue, newValue) =>
        {
            var cc = (BarChart)bindableObject;
            cc._currentChart.BarsFillColor = (Color)newValue;
        });

        /// <summary>
        /// Gets or sets the color to use when drawing each bar
        /// </summary>
        public Color BarsFillColor
        {
            get => (Color)GetValue(BarsFillColorProperty);
            set => SetValue(BarsFillColorProperty, value);
        }
        #endregion

        public BarChart()
        {
            Drawable = _currentChart;

        }
    }
}

