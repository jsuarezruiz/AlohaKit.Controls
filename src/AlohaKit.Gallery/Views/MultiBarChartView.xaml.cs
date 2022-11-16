using AlohaKit.Models;
using System.Collections.ObjectModel;

namespace AlohaKit.Gallery.Views;

public partial class MultiBarChartView : ContentPage
{
	ObservableCollection<ChartItem> _multiSeriesCollection = new ObservableCollection<ChartItem>()
			{
				//Group #1 |ID = 2
				{new ChartItem(){ Value= 100, GroupId = 2, StyleId = 2} },
				{new ChartItem(){ Value= 150, GroupId = 2, StyleId = 3}},
				{new ChartItem(){ Value= 200, GroupId = 2, StyleId = 4} },
				{new ChartItem(){ Value= 300, GroupId = 2, StyleId = 5} },
				{new ChartItem(){ Value= 900, GroupId = 2, StyleId = 6} },

                //Group #2 |ID = 3 
				{new ChartItem(){ Value= 200, GroupId = 3, StyleId = 2} },
				{new ChartItem(){ Value= 250, GroupId = 3, StyleId = 3} },
				{new ChartItem(){ Value= 300, GroupId = 3, StyleId = 4} },
				{new ChartItem(){ Value= 400, GroupId = 3, StyleId = 5} },
				{new ChartItem(){ Value= 900, GroupId = 3, StyleId = 6} },
			};

	ObservableCollection<string> _columnNames = new ObservableCollection<string>()
	{"Value 1","Value 2","Value 3","Value 4","Value 5" };

	ObservableCollection<ChartGroupStyle> _groupsStyles = new ObservableCollection<ChartGroupStyle>()
	{
		new ChartGroupStyle(){Id = 2, BackgroundColor = Colors.Pink },
		new ChartGroupStyle(){Id= 3 , BackgroundColor = Colors.Blue},
		new ChartGroupStyle(){Id = 4, BackgroundColor = Colors.Red},
		new ChartGroupStyle(){Id = 5, BackgroundColor = Colors.Brown},
		new ChartGroupStyle(){Id = 6, BackgroundColor = Colors.Yellow},
	};

	public ObservableCollection<ChartItem> MultiSeriesChartCollection
	{
		get => _multiSeriesCollection;
		set => _multiSeriesCollection = value;
	}

	public ObservableCollection<ChartGroupStyle> MultiSeriesChartStyles
	{
		get => _groupsStyles;
		set => _groupsStyles = value;
	}

	public ObservableCollection<string> ColumnNames
	{
		get => _columnNames;
		set => _columnNames = value;
	}

	public MultiBarChartView()
	{
		InitializeComponent();
		BindingContext = this;

	}
}