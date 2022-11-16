using AlohaKit.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace AlohaKit.Controls
{
	public sealed class MultiBarChartDrawable : BaseChartDrawable
	{
		#region Properties
		private bool _autoCalculateItemSeparationMargin = true;
		private float _barsCornerRadius = 6f;
		private Color _barsFillColor = Color.FromArgb("#3E75FF");
		private ObservableCollection<ChartGroupStyle> _groupStyles = new ObservableCollection<ChartGroupStyle>();
		private ObservableCollection<string> _columnNames = new ObservableCollection<string>();

		/// <summary>
		/// Indicates if control will calculate by itself left and right bar margins based on the group entries.
		/// </summary>
		public bool AutoCalculateItemSeparationMargin
		{
			get => _autoCalculateItemSeparationMargin;
			set
			{
				_autoCalculateItemSeparationMargin = value;
				RequestInvalidate();
			}
		}

		/// <summary>
		/// Border radius to use when drawing each bar
		/// </summary>
		public float BarsCornerRadius
		{
			get => _barsCornerRadius;
			set
			{
				_barsCornerRadius = value;
				RequestInvalidate();
			}
		}

		public ObservableCollection<ChartGroupStyle> GroupStyles
		{
			get => _groupStyles;
			set
			{
				_groupStyles = value;
				foreach (var item in _groupStyles)
				{
					item.PropertyChanged += Entry_PropertyChanged;
				}

				_groupStyles.CollectionChanged += Entries_CollectionChanged;
				RequestInvalidate();
			}
		}


		public ObservableCollection<string> ColumnNames
		{
			get => _columnNames;
			set
			{
				_columnNames = value;
				_columnNames.CollectionChanged += Entries_CollectionChanged;
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
		protected override void Entry_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			//Disable Entry.Label triggering Invalidate().
			//So the initial column names remains the same
			//This only applies for the scenario in which ColumnNames is empty/null
			if (_canvas != null && e.PropertyName != nameof(ChartItem.Label)) RequestInvalidate();
		}

		///<inheritdoc/>
		public override void DrawChart(ICanvas canvas, RectF dirtyRect)
		{

			if (canvas == null) return;
			var groups = Entries.Select(x => x.GroupId).Distinct().ToList();
			var lookableEntries = Entries.ToLookup(p => p.GroupId);
			var horizontalLinesDrawn = false;
			var verticalLinesDrawn = false;
			var footerDrawn = false;
			PointF[] points = null;
			var validationPassed = true;
			var columnIndex = 0;
			var oldCount = lookableEntries[groups.First()].Count();

			groups.ForEach(g =>
			{
				var currCount = lookableEntries[g].Count();
				if (currCount != oldCount) validationPassed = false;
			});

			if (!validationPassed) throw new ArgumentException("Entry groups must have the same size in oder to draw.");
			//if ((groups.Count != ColumnNames.Count)) throw new ArgumentException("Groups count must be equal to ColumnNames count");
			groups.ForEach(group =>
			{
				var groupEntries = lookableEntries[group].ToList();

				Width = dirtyRect.Width;
				Height = dirtyRect.Height;

				//Calculate coordinates for every item within canvas
				var valueLabelSizes = MeasureLabels();
				FooterHeight = CalculateFooterHeight(valueLabelSizes);
				HeaderHeight = CalculateHeaderHeight(valueLabelSizes);
				ItemSize = CalculateItemSize(groupEntries, Width, Height, FooterHeight, HeaderHeight, groups.Count);
				Origin = CalculateYOrigin(ItemSize.Height, HeaderHeight);

				points = CalculatePoints(groupEntries, ItemSize, Origin, HeaderHeight);

				//DisplayHeaderValues = !DisplayHorizontalAxisLines;
				if (DisplayHorizontalAxisLines && !horizontalLinesDrawn)
				{
					var initialXCoordinate = points[0].X + AxisXMargin;
					var finalXCoordinate = points.Last().X + (ItemSize.Width + AxisXMargin);

					PointF[] tPoints = (PointF[])points.Clone();
					DrawHorizontalStepLines(canvas, tPoints, Width, Height, Origin, initialXCoordinate, finalXCoordinate);
					horizontalLinesDrawn = true;
				}

				if (DisplayVerticalAxisLines && DisplayHorizontalAxisLines && !verticalLinesDrawn)
				{
					DrawVerticalStepLines(canvas, points, Origin, MaxYValueCoordinate);
					verticalLinesDrawn = true;
				}

				DrawGroupBars(canvas, points, groupEntries, Origin, columnIndex);

				if (!footerDrawn)
				{
					_displayHeaderValues = false;
					DrawLabels(groupEntries, canvas, points, ItemSize, Height, FooterHeight, Origin);
					footerDrawn = true;
				}

				columnIndex++;
			});
		}

        private SizeF CalculateItemSize(List<ChartItem> entries, float width, float height, float footerHeight, float headerHeight, int groupCount)
		{
			var total = groupCount;
			var w = (width - (float)ItemSeparationMargin - (DisplayHorizontalAxisLines ? AxisXMargin : 0) - total * ItemSeparationMargin / 2) / total;
			var h = height - Margin - footerHeight - headerHeight;
			return new SizeF(w, h);
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
		/// <param name="groupPoints">Array of points to draw</param>
		/// <param name="origin">Y axis origin coordinate</param>
		private void DrawGroupBars(ICanvas canvas, PointF[] groupPoints, List<ChartItem> groupEntries, float origin, int columnIndex)
		{
			if (groupPoints.Length > 0)
			{
				var barSidesMargin = AutoCalculateItemSeparationMargin ? groupEntries.Count() - 1 : ItemSeparationMargin;
				if (barSidesMargin < 0) barSidesMargin = 1;
				var barSize = (ItemSize.Width / groupPoints.Count()) - barSidesMargin;
				var maxBackgroundPoint = groupPoints.OrderBy(x => x.Y).First(); //closest value to Y:0 represents the greatest value
				maxBackgroundPoint.Y -= HeaderValuesMargin / 2;
				var maxY = Math.Min(origin, maxBackgroundPoint.Y);
				base.MaxYValueCoordinate = maxY;
				var maxHeight = Math.Max(2, Math.Abs(origin - maxBackgroundPoint.Y));

				float currItemOffset = groupPoints[columnIndex].X;
				var tempColor = FillColor;
				for (int i = 0; i < groupPoints.Length; i++)
				{
					var currStyle = GroupStyles?.FirstOrDefault(s => s.Id == groupEntries[i].StyleId);
					if (currStyle?.BackgroundColor != null)
					{
						_colorBrush = null;
						_barsFillColor = currStyle.BackgroundColor;
					}

					if (currStyle?.Background != null)
					{
						_barsFillColor = null;
						_colorBrush = currStyle.Background;
					}

					var height = Math.Max(2, Math.Abs(origin - groupPoints[i].Y));
					groupPoints[i].Y = Origin - (height * AnimationProgress / 100);

					var newRec = (new RectF(currItemOffset + (DisplayHorizontalAxisLines ? AxisXMargin : 0), Origin - (height * AnimationProgress / 100), barSize, height * AnimationProgress / 100));

					if (ColorBrush != null)
					{
						canvas.SetFillPaint(ColorBrush, newRec);
					}
					else
					{
						canvas.SetFillPaint(null, newRec);
						canvas.FillColor = BarsFillColor.WithAlpha(PathsColorOpacity);
					}

					canvas.FillRoundedRectangle(newRec, BarsCornerRadius, BarsCornerRadius, 0, 0);
					currItemOffset += barSize + barSidesMargin;
				}

				canvas.FillColor = tempColor;
			}
		}

		public override void Dispose()
		{
			base.Dispose();
			_columnNames.CollectionChanged -= Entries_CollectionChanged;
			foreach (var item in _groupStyles)
			{
				item.PropertyChanged -= Entry_PropertyChanged;
			}
		}
	}
}
