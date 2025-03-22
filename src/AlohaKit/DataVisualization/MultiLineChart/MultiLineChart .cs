using System.Collections.ObjectModel;
using AlohaKit.Models;
using static AlohaKit.Enums.ChartEnums;

namespace AlohaKit.Controls
{
	/// <summary>
	/// The MultiLineChartView is a drawn control for displaying multiple line charts within a single view. 
	/// This enables the visualization of multiple data series, each represented by a separate line, on a shared coordinate system. 
	/// 
	/// The class is derived from BaseChart, allowing it to inherit essential charting properties and behaviors while adding specialized features for multi-line visualization.
	/// </summary>
	public sealed class MultiLineChartView : BaseChart
    {
        private MultiLineChartDrawable _currentChart = new MultiLineChartDrawable();
        
		#region DependencyProperties

        public static readonly BindableProperty ExpandAndFillBackgroundCurvePathProperty = BindableProperty.Create(nameof(ExpandAndFillBackgroundCurvePath), typeof(bool), typeof(MultiLineChartView), false, propertyChanged: (bindableObject, oldValue, newValue) =>
       {
           var cc = (MultiLineChartView)bindableObject;
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

        public static readonly BindableProperty ShowPointsForCurveStyleProperty = BindableProperty.Create(nameof(ShowPointsForCurveStyle), typeof(bool), typeof(MultiLineChartView), false, propertyChanged: (bindableObject, oldValue, newValue) =>
         {
             var cc = (MultiLineChartView)bindableObject;
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

        public static readonly BindableProperty IsCurveBackgroundFilledProperty = BindableProperty.Create(nameof(IsCurveBackgroundFilled), typeof(bool), typeof(MultiLineChartView), true, propertyChanged: (bindableObject, oldValue, newValue) =>
          {
              var cc = (MultiLineChartView)bindableObject;
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


        public static readonly BindableProperty CurveFactorProperty = BindableProperty.Create(nameof(CurveFactor), typeof(float), typeof(MultiLineChartView), 0.6f, propertyChanged: (bindableObject, oldValue, newValue) =>
        {
            var cc = (MultiLineChartView)bindableObject;
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

        public static readonly BindableProperty ChartStyleProperty = BindableProperty.Create(nameof(ChartStyle), typeof(LineChartStyle), typeof(MultiLineChartView), LineChartStyle.Line, propertyChanged: (bindableObject, oldValue, newValue) =>
       {
           var cc = (MultiLineChartView)bindableObject;
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

        public static readonly BindableProperty PointSizeProperty = BindableProperty.Create(nameof(PointSize), typeof(float), typeof(MultiLineChartView), 5f, propertyChanged: (bindableObject, oldValue, newValue) =>
        {
            var cc = (MultiLineChartView)bindableObject;
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

        public static readonly BindableProperty ColumnNamesProperty = BindableProperty.Create(nameof(ColumnNames), typeof(ObservableCollection<string>), typeof(MultiLineChartView), null, propertyChanged: (bindableObject, oldValue, newValue) =>
        {
            var cc = (MultiLineChartView)bindableObject;
            var columnNames = (ObservableCollection<string>)newValue;
            if (cc.Entries != null && cc.Entries.Any() && columnNames != null)
            {
                //in case we already have entries but no columnNames
                //re-assign column names to Entries internally so chart
                //can measure and then draw footer labels accordingly
                if (cc.Entries.Any(x => string.IsNullOrEmpty(x.Label)))
                {
                    var groups = cc.Entries.Select(x => x.GroupId).Distinct().ToList();
                    var lookableEntries = cc.Entries.ToLookup(p => p.GroupId);

                    foreach (var group in groups)
                    {
                        var currGroup = new ObservableCollection<ChartItem>(lookableEntries[group]);
                        for (int i = 0; i < currGroup.Count; i++)
                        {
                            if (string.IsNullOrEmpty(currGroup[i].Label))
                            {
                                var labelVal = cc.ColumnNames.ElementAtOrDefault(i);
                                currGroup[i].Label = string.IsNullOrEmpty(labelVal) ? "-" : labelVal;
                            }
                        }
                    }
                }
            }

            cc._currentChart.ColumnNames = columnNames;
        });
        public ObservableCollection<string> ColumnNames
        {
            get => (ObservableCollection<string>)GetValue(ColumnNamesProperty);
            set => SetValue(ColumnNamesProperty, value);
        }

        public static readonly BindableProperty GroupStylesProperty = BindableProperty.Create(nameof(GroupStyles), typeof(ObservableCollection<ChartGroupStyle>), typeof(MultiLineChartView), null, propertyChanged: (bindableObject, oldValue, newValue) =>
        {
            var cc = (MultiLineChartView)bindableObject;
            cc._currentChart.GroupStyles = (ObservableCollection<ChartGroupStyle>)newValue;
        });
        public ObservableCollection<ChartGroupStyle> GroupStyles
        {
            get => (ObservableCollection<ChartGroupStyle>)GetValue(GroupStylesProperty);
            set => SetValue(GroupStylesProperty, value);
        }

        public new static readonly BindableProperty EntriesProperty = BindableProperty.Create(nameof(Entries), typeof(ObservableCollection<ChartItem>), typeof(MultiLineChartView), null, propertyChanged: (bindableObject, oldValue, newValue) =>
        {
            var cc = (MultiLineChartView)bindableObject;
            if (newValue != null)
            {
                var newElements = (ObservableCollection<ChartItem>)newValue;
                if (cc.ColumnNames != null && cc.ColumnNames.Any())
                {

                    var groups = newElements.Select(x => x.GroupId).Distinct().ToList();
                    var lookableEntries = newElements.ToLookup(p => p.GroupId);

                    foreach (var group in groups)
                    {
                        var currGroup = new ObservableCollection<ChartItem>(lookableEntries[group]);
                        for (int i = 0; i < currGroup.Count; i++)
                        {
                            if (string.IsNullOrEmpty(currGroup[i].Label))
                            {
                                var labelVal = cc.ColumnNames.ElementAtOrDefault(i);
                                currGroup[i].Label = labelVal ?? "-";
                            }
                        }
                    }
                }
                else if (cc.ColumnNames is null)
                {
                    //if columns array is empty, check if we can get them from column names
                    var groups = newElements.Where(i => !string.IsNullOrEmpty(i.Label)).Select(x => x.Label)?.Distinct()?.ToList();
                    if (groups != null && groups.Any())
                    {
                        cc.ColumnNames = new ObservableCollection<string>(groups);
                    }
                }

                cc._currentChart.Entries = newElements;
            }
        });

        public new ObservableCollection<ChartItem> Entries
        {
            get => (ObservableCollection<ChartItem>)GetValue(EntriesProperty);
            set => SetValue(EntriesProperty, value);
        }
        #endregion

        public MultiLineChartView()
        {
            Drawable = _currentChart;
        }
    }
}

