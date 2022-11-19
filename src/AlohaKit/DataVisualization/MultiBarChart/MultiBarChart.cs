using System.Collections.ObjectModel;
using AlohaKit.Models;

namespace AlohaKit.Controls
{
    public sealed class MultiBarChart : BaseChart
    {
        private MultiBarChartDrawable _currentChart = new MultiBarChartDrawable();

        public static readonly BindableProperty AutoCalculateItemSeparationMarginProperty = BindableProperty.Create(nameof(AutoCalculateItemSeparationMargin), typeof(bool), typeof(MultiBarChart), true, propertyChanged: (bindableObject, oldValue, newValue) =>
         {
             var cc = (MultiBarChart)bindableObject;
             if (cc._currentChart != null)
                 cc._currentChart.AutoCalculateItemSeparationMargin = (bool)newValue;
         });

        /// <summary>
        /// Gets or sets the separation margin between each item. Default is 8
        /// </summary>
        public bool AutoCalculateItemSeparationMargin
        {
            get => (bool)GetValue(AutoCalculateItemSeparationMarginProperty);
            set => SetValue(AutoCalculateItemSeparationMarginProperty, value);
        }

        public new static readonly BindableProperty ItemSeparationMarginProperty = BindableProperty.Create(nameof(ItemSeparationMargin), typeof(float), typeof(MultiBarChart), 8f, propertyChanged: (bindableObject, oldValue, newValue) =>
         {
             var cc = (MultiBarChart)bindableObject;
             if (cc._currentChart != null && !cc._currentChart.AutoCalculateItemSeparationMargin)
                 cc._currentChart.ItemSeparationMargin = (float)newValue;
         });

        /// <summary>
        /// Gets or sets the separation margin between each item. Default is 8
        /// </summary>
        public new float ItemSeparationMargin
        {
            get => (float)GetValue(ItemSeparationMarginProperty);
            set => SetValue(ItemSeparationMarginProperty, value);
        }

        public static readonly BindableProperty ColumnNamesProperty = BindableProperty.Create(nameof(ColumnNames), typeof(ObservableCollection<string>), typeof(MultiBarChart), null, propertyChanged: (bindableObject, oldValue, newValue) =>
        {
            var cc = (MultiBarChart)bindableObject;
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

        public static readonly BindableProperty BarsCornerRadiusProperty = BindableProperty.Create(nameof(GroupStyles), typeof(float), typeof(MultiBarChart), 6f, propertyChanged: (bindableObject, oldValue, newValue) =>
        {
            var cc = (MultiBarChart)bindableObject;
            cc._currentChart.BarsCornerRadius = (float)newValue;
        });
        public float BarsCornerRadius
        {
            get => (float)GetValue(BarsCornerRadiusProperty);
            set => SetValue(BarsCornerRadiusProperty, value);
        }


        public static readonly BindableProperty GroupStylesProperty = BindableProperty.Create(nameof(GroupStyles), typeof(ObservableCollection<ChartGroupStyle>), typeof(MultiBarChart), null, propertyChanged: (bindableObject, oldValue, newValue) =>
        {
            var cc = (MultiBarChart)bindableObject;
            cc._currentChart.GroupStyles = (ObservableCollection<ChartGroupStyle>)newValue;
        });
        public ObservableCollection<ChartGroupStyle> GroupStyles
        {
            get => (ObservableCollection<ChartGroupStyle>)GetValue(GroupStylesProperty);
            set => SetValue(GroupStylesProperty, value);
        }

        public new static readonly BindableProperty EntriesProperty = BindableProperty.Create(nameof(Entries), typeof(ObservableCollection<ChartItem>), typeof(MultiBarChart), null, propertyChanged: (bindableObject, oldValue, newValue) =>
        {
            var cc = (MultiBarChart)bindableObject;
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

        public MultiBarChart()
        {
            Drawable = _currentChart;
        }
    }
}
