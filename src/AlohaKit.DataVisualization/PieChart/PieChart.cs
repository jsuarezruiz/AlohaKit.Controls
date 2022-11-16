namespace AlohaKit.DataVisualization
{
	// TODO:
	// - Add option to detect when a slice is tapped.
	public class PieChart : GraphicsView
	{
		public PieChart()
		{
			HeightRequest = 300;
			WidthRequest = 300; 
			
			Drawable = PieChartDrawable = new PieChartDrawable();
		}

		public PieChartDrawable PieChartDrawable { get; set; }

		public static readonly new BindableProperty BackgroundProperty =
			BindableProperty.Create(nameof(Background), typeof(Brush), typeof(PieChart), null,
				propertyChanged: (bindableObject, oldValue, newValue) =>
				{
					if (newValue != null && bindableObject is PieChart pieChart)
					{
						pieChart.UpdateBackground();
					}
				});

		public new Brush Background
		{
			get => (Brush)GetValue(BackgroundProperty);
			set => SetValue(BackgroundProperty, value);
		}

		public static readonly BindableProperty ItemsSourceProperty =
			BindableProperty.Create(nameof(ItemsSource), typeof(Dictionary<string, float>), typeof(PieChart), null,
				propertyChanged: (bindableObject, oldValue, newValue) => 
				{	 
					if (newValue != null && bindableObject is PieChart pieChart)
					{
						pieChart.UpdateItemsSource();
					}		 
				});

		public Dictionary<string, float> ItemsSource
		{
			get => (Dictionary<string, float>)GetValue(ItemsSourceProperty);
			set => SetValue(ItemsSourceProperty, value);
		}

		public static readonly BindableProperty ShowLabelsProperty =
		   BindableProperty.Create(nameof(ShowLabels), typeof(bool), typeof(PieChart), true,
			   propertyChanged: (bindableObject, oldValue, newValue) =>
			   {
				   if (newValue != null && bindableObject is PieChart pieChart)
				   {
					   pieChart.UpdateShowLabels();
				   }
			   }); 
		
		public bool ShowLabels
		{
			get => (bool)GetValue(ShowLabelsProperty);
			set => SetValue(ShowLabelsProperty, value);
		}

		protected override void OnParentSet()
		{
			base.OnParentSet();

			if (Parent != null)
			{
				UpdateBackground();
				UpdateItemsSource();
				UpdateShowLabels();
			}
		}

		void UpdateBackground()
		{
			if (PieChartDrawable == null)
				return;

			PieChartDrawable.BackgroundPaint = Background;

			Invalidate();
		}

		void UpdateItemsSource()
		{
			if (PieChartDrawable == null)
				return;

			PieChartDrawable.ItemsSource = ItemsSource;

			Invalidate();
		}

		void UpdateShowLabels()
		{
			if (PieChartDrawable == null)
				return;

			PieChartDrawable.ShowLabels = ShowLabels;

			Invalidate();
		}
	}
}