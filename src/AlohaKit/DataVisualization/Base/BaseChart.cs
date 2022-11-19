using AlohaKit.Models;
using System.Collections.ObjectModel;
namespace AlohaKit.Controls
{
	public abstract class BaseChart : GraphicsView, IDisposable
	{
		private BaseChartDrawable _drawable;

		/// <summary>
		/// Gets or sets current IDrawable surface.
		/// This property MUST be assigned before ChartEntries prop
		/// </summary>
		protected new BaseChartDrawable Drawable
		{
			get => _drawable; 
			set
			{
				if(value != null)
				{
					_drawable = value;
					base.Drawable = value;
					Drawable.OnInvalidateRequest += OnInvalidateRequest;
					if (Entries != null && Entries.Any()) Drawable.Entries = Entries;
				}
			}
		}

		public BaseChart()
		{
			Loaded += BarChartView_Loaded;
			if (EnableEntryAnimations)
				Opacity = 0;
		}
		private void BarChartView_Loaded(object sender, EventArgs e)
		{
			Drawable.IsInitialized = true;
			if (!EnableEntryAnimations)
			{
				Opacity = 1;
				Drawable.AnimationProgress = 100;
			}
			Drawable.Entries = Entries;
			if (EnableEntryAnimations)
			{
				Drawable.IsAnimating = true;
				Opacity = 1;
				AnimateProgress(0);
			}
		}

		void AnimateProgress(int progress)
		{
			var animation = new Animation(v =>
			{
				Drawable.AnimationProgress = (int)v;
				Invalidate();
				Drawable.IsAnimating = false;
			}, 1, 100, easing: Easing);
			animation.Commit(this, "AnimationProgress", length: (uint)AnimationInterval);
		}

		#region Dependency Properties
		public static readonly BindableProperty EasingProperty = BindableProperty.Create(nameof(Easing), typeof(Easing), typeof(BaseChart), Easing.BounceOut);
		/// <summary>
		/// Sets the Easing to use when triggering animations. Default is BounceOut
		/// </summary>
		public Easing Easing
		{
			get => (Easing)GetValue(EasingProperty);
			set => SetValue(EasingProperty, value);
		}

		public static readonly new BindableProperty BackgroundProperty = BindableProperty.Create(nameof(Background), typeof(Brush), typeof(BaseChart), null, propertyChanged: (bindableObject, oldValue, newValue) =>
		  {
			  var cc = (BaseChart)bindableObject;
			  if (cc.Drawable != null)
				  cc.Drawable.ColorBrush = (Brush)newValue;
		  });

		public new Brush Background
		{
			get => (Brush)GetValue(BackgroundProperty);
			set => SetValue(BackgroundProperty, value);
		}

		public static readonly BindableProperty AxisDashPatternProperty = BindableProperty.Create(nameof(AxisDashPattern), typeof(float[]), typeof(BaseChart), new float[] { 6, 6 }, propertyChanged: (bindableObject, oldValue, newValue) =>
		  {
			  var cc = (BaseChart)bindableObject;
			  if (cc.Drawable != null)
				  cc.Drawable.AxisDashPattern = (float[])newValue;
		  });

		public float[] AxisDashPattern
		{
			get => (float[])GetValue(AxisDashPatternProperty);
			set => SetValue(AxisDashPatternProperty, value);
		}

		public static readonly BindableProperty DisplayHeaderValuesProperty = BindableProperty.Create(nameof(DisplayHeaderValues), typeof(bool), typeof(BaseChart), true, propertyChanged: (bindableObject, oldValue, newValue) =>
		  {
			  var cc = (BaseChart)bindableObject;
			  if (cc.Drawable != null)
				  cc.Drawable.DisplayHeaderValues = (bool)newValue;
		  });

		public bool DisplayHeaderValues
		{
			get => (bool)GetValue(DisplayHeaderValuesProperty);
			set => SetValue(DisplayHeaderValuesProperty, value);
		}

		public static readonly BindableProperty PathsColorOpacityProperty = BindableProperty.Create(nameof(PathsColorOpacity), typeof(float), typeof(BaseChart), 0.6f, propertyChanged: (bindableObject, oldValue, newValue) =>
		{
			var cc = (BaseChart)bindableObject;
			if (cc.Drawable != null)
				cc.Drawable.PathsColorOpacity = (float)newValue;
		});

		/// <summary>
		/// Sets the Alpha modifier value to use when drawing solid color backgrounds.Default is 0.6 
		/// </summary>
		public float PathsColorOpacity
		{
			get => (float)GetValue(PathsColorOpacityProperty);
			set => SetValue(PathsColorOpacityProperty, value);
		}

		public static readonly BindableProperty AxisLinesFontSizeProperty = BindableProperty.Create(nameof(AxisLinesFontSize), typeof(float), typeof(BaseChart), 11.0f, propertyChanged: (bindableObject, oldValue, newValue) =>
		{
			var cc = (BaseChart)bindableObject;
			if (cc.Drawable != null)
				cc.Drawable.AxisFontSize = (float)newValue;
		});

		/// <summary>
		/// Axis lines font size. Default is 11
		/// </summary>
		public float AxisLinesFontSize
		{
			get => (float)GetValue(AxisLinesFontSizeProperty);
			set => SetValue(AxisLinesFontSizeProperty, value);
		}

		public static readonly BindableProperty AxisLineColorProperty = BindableProperty.Create(nameof(AxisLinesColor), typeof(Color), typeof(BaseChart), Colors.LightGray, propertyChanged: (bindableObject, oldValue, newValue) =>
		 {
			 var cc = (BaseChart)bindableObject;
			 if (cc.Drawable != null)
				 cc.Drawable.AxisLinesColor = (Color)newValue;
		 });

		/// <summary>
		/// Axis lines color. Default is Lightgray
		/// </summary>
		public Color AxisLinesColor
		{
			get => (Color)GetValue(AxisLineColorProperty);
			set => SetValue(AxisLineColorProperty, value);
		}

		public static readonly BindableProperty AxisLinesStrokeSizeProperty = BindableProperty.Create(nameof(AxisLinesStrokeSize), typeof(float), typeof(BaseChart), 0.9f, propertyChanged: (bindableObject, oldValue, newValue) =>
		{
			var cc = (BaseChart)bindableObject;
			if (cc.Drawable != null)
				cc.Drawable.AxisLinesStrokeSize = (float)newValue;
		});

		/// <summary>
		/// Axis lines stroke size. Default is 0.9
		/// </summary>
		public float AxisLinesStrokeSize
		{
			get => (float)GetValue(AxisLinesStrokeSizeProperty);
			set => SetValue(AxisLinesStrokeSizeProperty, value);
		}

		public static readonly BindableProperty DisplayVerticalAxisLinesProperty = BindableProperty.Create(nameof(DisplayVerticalAxisLines), typeof(bool), typeof(BaseChart), false, propertyChanged: (bindableObject, oldValue, newValue) =>
		  {
			  var cc = (BaseChart)bindableObject;
			  if (cc.Drawable != null)
				  cc.Drawable.DisplayVerticalAxisLines = (bool)newValue;
		  });

		/// <summary>
		/// If true vertical lines will be drawn as background along with horizontal lines. DisplayHorizontalAxisLines prop needs to be true as well
		/// </summary>
		public bool DisplayVerticalAxisLines
		{
			get => (bool)GetValue(DisplayVerticalAxisLinesProperty);
			set => SetValue(DisplayVerticalAxisLinesProperty, value);
		}

		public static readonly BindableProperty DisplayHorizontalAxisLinesProperty = BindableProperty.Create(nameof(DisplayHorizontalAxisLines), typeof(bool), typeof(BaseChart), false, propertyChanged: (bindableObject, oldValue, newValue) =>
		 {
			 var cc = (BaseChart)bindableObject;
			 if (cc.Drawable != null)
				 cc.Drawable.DisplayHorizontalAxisLines = (bool)newValue;
		 });

		/// <summary>
		/// If true header labels will be hidden and chart will draw horizontal step lines.
		/// </summary>
		public bool DisplayHorizontalAxisLines
		{
			get => (bool)GetValue(DisplayHorizontalAxisLinesProperty);
			set => SetValue(DisplayHorizontalAxisLinesProperty, value);
		}

		public static readonly BindableProperty AnimationIntervalProperty = BindableProperty.Create(nameof(AnimationInterval), typeof(int), typeof(BaseChart), 1250);
		/// <summary>
		/// Sets the interval in ms to use when triggering animations. Default is 1250
		/// </summary>
		public int AnimationInterval
		{
			get => (int)GetValue(AnimationIntervalProperty);
			set => SetValue(AnimationIntervalProperty, value);
		}


		public static readonly BindableProperty ReanimateOnPropertyChangedProperty = BindableProperty.Create(nameof(ReanimateOnPropertyChanged), typeof(bool), typeof(BaseChart), true);
		/// <summary>
		/// If true, chart will trigger entry animation whenever a chart property is changed and EnableEntryAnimations prop is set to true. Default is true
		/// </summary>
		public bool ReanimateOnPropertyChanged
		{
			get => (bool)GetValue(ReanimateOnPropertyChangedProperty);
			set => SetValue(ReanimateOnPropertyChangedProperty, value);
		}

		public static readonly BindableProperty EnableEntryAnimationsProperty = BindableProperty.Create(nameof(EnableEntryAnimations), typeof(bool), typeof(BaseChart), true);
		/// <summary>
		/// If true, chart will trigger animations when drawing chart content. Default is true
		/// </summary>
		public bool EnableEntryAnimations
		{
			get => (bool)GetValue(EnableEntryAnimationsProperty);
			set => SetValue(EnableEntryAnimationsProperty, value);
		}

		public static readonly BindableProperty EntriesProperty = BindableProperty.Create(nameof(Entries), typeof(ObservableCollection<ChartItem>), typeof(BaseChart), null, propertyChanged: (bindableObject, oldValue, newValue) =>
		{
			var cc = (BaseChart)bindableObject;
			var value = newValue as ObservableCollection<ChartItem>;
			if (value != null && value.Any() && cc.Drawable != null)
			{
				if (cc.Drawable != null && cc.Drawable.IsInitialized)
					cc.Drawable.Entries = value;
			}
		});

		public ObservableCollection<ChartItem> Entries
		{
			get => (ObservableCollection<ChartItem>)GetValue(EntriesProperty);
			set => SetValue(EntriesProperty, value);
		}

		public static readonly BindableProperty IsLabelTextTruncationEnabledProperty = BindableProperty.Create(nameof(IsLabelTextTruncationEnabled), typeof(bool), typeof(BaseChart), true, propertyChanged: (bindableObject, oldValue, newValue) =>
		{
			var cc = (BaseChart)bindableObject;
			if (cc.Drawable != null)
				cc.Drawable.IsLabelTextTruncationEnabled = (bool)newValue;
		});

		/// <summary>
		/// If true, chart labels text will be truncated to fit available size. Default is true
		/// </summary>
		public bool IsLabelTextTruncationEnabled
		{
			get => (bool)GetValue(IsLabelTextTruncationEnabledProperty);
			set => SetValue(IsLabelTextTruncationEnabledProperty, value);
		}

		public static readonly BindableProperty DisplayValueLabelsOnTopProperty = BindableProperty.Create(nameof(DisplayValueLabelsOnTop), typeof(bool), typeof(BaseChart), true, propertyChanged: (bindableObject, oldValue, newValue) =>
		{
			var cc = (BaseChart)bindableObject;
			if (cc.Drawable != null)
				cc.Drawable.DisplayValueLabelsOnTop = (bool)newValue;
		});

		/// <summary>
		/// If true, chart will show value labels on top of canvas. Default is true
		/// </summary>
		public bool DisplayValueLabelsOnTop
		{
			get => (bool)GetValue(DisplayValueLabelsOnTopProperty);
			set => SetValue(DisplayValueLabelsOnTopProperty, value);
		}

		public static readonly BindableProperty StrokeSizeProperty = BindableProperty.Create(nameof(StrokeSize), typeof(float), typeof(BaseChart), 2.5f, propertyChanged: (bindableObject, oldValue, newValue) =>
	   {
		   var cc = (BaseChart)bindableObject;
		   if (cc.Drawable != null)
			   cc.Drawable.StrokeSize = (float)newValue;
	   });

		/// <summary>
		/// Stroke size. Default is 2.5
		/// </summary>
		public float StrokeSize
		{
			get => (float)GetValue(StrokeSizeProperty);
			set => SetValue(StrokeSizeProperty, value);
		}

		public static readonly BindableProperty StrokeColorProperty = BindableProperty.Create(nameof(StrokeColor), typeof(Color), typeof(BaseChart), Colors.Black, propertyChanged: (bindableObject, oldValue, newValue) =>
		 {
			 var cc = (BaseChart)bindableObject;
			 if (cc.Drawable != null)
				 cc.Drawable.StrokeColor = (Color)newValue;
		 });

		/// <summary>
		/// Stroke color. Default is Black
		/// </summary>
		public Color StrokeColor
		{
			get => (Color)GetValue(StrokeColorProperty);
			set => SetValue(StrokeColorProperty, value);
		}

		public static readonly BindableProperty ItemSeparationMarginProperty = BindableProperty.Create(nameof(ItemSeparationMargin), typeof(float), typeof(BaseChart), 8f, propertyChanged: (bindableObject, oldValue, newValue) =>
		 {
			 var cc = (BaseChart)bindableObject;
			 if (cc.Drawable != null)
				 cc.Drawable.ItemSeparationMargin = (float)newValue;
		 });

		/// <summary>
		/// Gets or sets the separation margin between each item. Default is 8
		/// </summary>
		public float ItemSeparationMargin
		{
			get => (float)GetValue(ItemSeparationMarginProperty);
			set => SetValue(ItemSeparationMarginProperty, value);
		}

		public static readonly BindableProperty FooterLabelsTextSizeProperty = BindableProperty.Create(nameof(FooterLabelsTextSize), typeof(float), typeof(BaseChart), 10f, propertyChanged: (bindableObject, oldValue, newValue) =>
		{
			var cc = (BaseChart)bindableObject;
			if (cc.Drawable != null)
				cc.Drawable.FooterLabelsTextSize = (float)newValue;
		});

		/// <summary>
		/// Gets or sets font size to use for footer label values. Default is 10
		/// </summary>
		public float FooterLabelsTextSize
		{
			get => (float)GetValue(FooterLabelsTextSizeProperty);
			set => SetValue(FooterLabelsTextSizeProperty, value);
		}

		public static readonly BindableProperty FooterLabelsMarginProperty = BindableProperty.Create(nameof(FooterLabelsMargin), typeof(float), typeof(BaseChart), 8f, propertyChanged: (bindableObject, oldValue, newValue) =>
	   {
		   var cc = (BaseChart)bindableObject;
		   if (cc.Drawable != null)
			   cc.Drawable.FooterLabelsMargin = (float)newValue;
	   });

		/// <summary>
		/// Gets or sets font margin to use when calculating footer labels coordinates. Default is 8
		/// </summary>
		public float FooterLabelsMargin
		{
			get => (float)GetValue(FooterLabelsMarginProperty);
			set => SetValue(FooterLabelsMarginProperty, value);
		}

		public static readonly BindableProperty HeaderValuesMarginProperty = BindableProperty.Create(nameof(HeaderValuesMargin), typeof(float), typeof(BaseChart), 30f, propertyChanged: (bindableObject, oldValue, newValue) =>
		{
			var cc = (BaseChart)bindableObject;
			if (cc.Drawable != null)
				cc.Drawable.HeaderValuesMargin = (float)newValue;
		});

		/// <summary>
		/// Gets or sets font margin to use when calculating header value labels coordinates. Default is 30
		/// </summary>
		public float HeaderValuesMargin
		{
			get => (float)GetValue(HeaderValuesMarginProperty);
			set => SetValue(HeaderValuesMarginProperty, value);
		}

		public static readonly BindableProperty FillColorProperty = BindableProperty.Create(nameof(FillColor), typeof(Color), typeof(BaseChart), Colors.Transparent, propertyChanged: (bindableObject, oldValue, newValue) =>
		  {
			  var cc = (BaseChart)bindableObject;
			  if (cc.Drawable != null)
				  cc.Drawable.FillColor = (Color)newValue;
		  });

		/// <summary>
		/// Gets or sets the canvas default fill color. Default is transparent
		/// </summary>
		public Color FillColor
		{
			get => (Color)GetValue(FillColorProperty);
			set => SetValue(FillColorProperty, value);
		}

		public static readonly BindableProperty FontFamilyProperty =
		BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(BaseChart), null, BindingMode.TwoWay, propertyChanged: (bindableObject, oldValue, newValue) =>
		{
			var cc = (BaseChart)bindableObject;
			if (cc.Drawable != null)
				try
				{
					cc.Drawable.Font = new Microsoft.Maui.Graphics.Font(newValue as string);
				}
				catch
				{
					cc.Drawable.Font = Microsoft.Maui.Graphics.Font.Default;
				}
		});

		/// <summary>
		/// Gets or sets the canvas default Font to use when drawing strings
		/// </summary>
		public string FontFamily
		{
			get => (string)GetValue(FontFamilyProperty);
			set => SetValue(FontFamilyProperty, value);
		}

		public static readonly BindableProperty FontSizeProperty = BindableProperty.Create(nameof(FontSize), typeof(float), typeof(BaseChart), 11f, propertyChanged: (bindableObject, oldValue, newValue) =>
		{
			var cc = (BaseChart)bindableObject;
			if (cc.Drawable != null)
				cc.Drawable.FontSize = (float)newValue;
		});

		/// <summary>
		/// Gets or sets font size to use when drawing value labels. Default is 11
		/// </summary>
		public float FontSize
		{
			get => (float)GetValue(FontSizeProperty);
			set => SetValue(FontSizeProperty, value);
		}

		public static readonly BindableProperty ChartMarginProperty = BindableProperty.Create(nameof(ChartMargin), typeof(float), typeof(BaseChart), 15f, propertyChanged: (bindableObject, oldValue, newValue) =>
	   {
		   var cc = (BaseChart)bindableObject;
		   if (cc.Drawable != null)
			   cc.Drawable.Margin = (float)newValue;
	   });

		/// <summary>
		/// Gets or sets the default top and bottom internal margin to use when drawing canvas content. Default value is 15
		/// </summary>
		public float ChartMargin
		{
			get => (float)GetValue(ChartMarginProperty);
			set => SetValue(ChartMarginProperty, value);
		}

		public static readonly BindableProperty FontColorProperty = BindableProperty.Create(nameof(FontColor), typeof(Color), typeof(BaseChart), Colors.Black, propertyChanged: (bindableObject, oldValue, newValue) =>
		  {
			  var cc = (BaseChart)bindableObject;
			  if (cc.Drawable != null)
				  cc.Drawable.FontColor = (Color)newValue;
		  });

		/// <summary>
		/// Gets or sets the color to use when drawing value labels. Default is Black
		/// </summary>
		public Color FontColor
		{
			get => (Color)GetValue(FontColorProperty);
			set => SetValue(FontColorProperty, value);
		}

		public static readonly BindableProperty FooterLabelsFontColorProperty = BindableProperty.Create(nameof(FooterLabelsFontColor), typeof(Color), typeof(BaseChart), Colors.Black, propertyChanged: (bindableObject, oldValue, newValue) =>
		 {
			 var cc = (BaseChart)bindableObject;
			 if (cc.Drawable != null)
				 cc.Drawable.FooterLabelsFontColor = (Color)newValue;
		 });

		/// <summary>
		/// Gets or sets the color to use when drawing footer labels. Default is Black
		/// </summary>
		public Color FooterLabelsFontColor
		{
			get => (Color)GetValue(FooterLabelsFontColorProperty);
			set => SetValue(FooterLabelsFontColorProperty, value);
		}


		public static readonly BindableProperty EnableAntialiasProperty = BindableProperty.Create(nameof(EnableAntialias), typeof(bool), typeof(BaseChart), true, propertyChanged: (bindableObject, oldValue, newValue) =>
		{
			var cc = (BaseChart)bindableObject;
			if (cc.Drawable != null)
				cc.Drawable.EnableAntialias = (bool)newValue;
		});

		/// <summary>
		/// Gets or sets if the current canvas will use Antialias mode or not when drawing content. Default is true
		/// </summary>
		public bool EnableAntialias
		{
			get => (bool)GetValue(EnableAntialiasProperty);
			set => SetValue(EnableAntialiasProperty, value);
		}
		#endregion

		private void OnInvalidateRequest(object s, EventArgs e)
		{
			if (!EnableEntryAnimations || EnableEntryAnimations && !ReanimateOnPropertyChanged)
			{
				Invalidate();
			}
			else if (EnableEntryAnimations && Drawable.IsInitialized && !Drawable.IsAnimating && ReanimateOnPropertyChanged)
			{
				Drawable.IsAnimating = true;
				Drawable.AnimationProgress = 1;
				AnimateProgress(0);
			}
		}

		public void Dispose()
		{
			Drawable.OnInvalidateRequest -= OnInvalidateRequest;
		}
	}

}

