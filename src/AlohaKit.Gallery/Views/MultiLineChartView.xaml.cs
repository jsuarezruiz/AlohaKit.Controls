using AlohaKit.Models;
using System.Collections.ObjectModel;

namespace AlohaKit.Gallery.Views;

public partial class MultiLineChartView : ContentPage
{
	ObservableCollection<ChartItem> _multiSeriesCollection = new ObservableCollection<ChartItem>()
			{
				//Group #1 |ID = 2
				{new ChartItem(){ Value= 200, GroupId = 2, IsLabelBold = true} },
				{new ChartItem(){ Value= 190, GroupId = 2}},
				{new ChartItem(){ Value= 200, GroupId = 2} },
				{new ChartItem(){ Value= 400, GroupId = 2} },
				{new ChartItem(){ Value= 600, GroupId = 2} },

                //Group #2 |ID = 3
				{new ChartItem(){ Value= 300, GroupId = 3} },
				{new ChartItem(){ Value= 150, GroupId = 3} },
				{new ChartItem(){ Value= 400, GroupId = 3} },
				{new ChartItem(){ Value= 100, GroupId = 3} },
				{new ChartItem(){ Value= 700, GroupId = 3} },
			};

	ObservableCollection<string> _columnNames = new ObservableCollection<string>()
		{"Value 1","Value 2","Value 3","Value 4","Value 5" };

	ObservableCollection<ChartGroupStyle> _groupsStyles = new ObservableCollection<ChartGroupStyle>()
	{
		new ChartGroupStyle(){Id = 2, BackgroundColor = Colors.Red, Color = Colors.Red, },
		new ChartGroupStyle(){Id= 3 , BackgroundColor = Colors.Blue, Color = Colors.Blue,},
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

	public MultiLineChartView()
	{
		InitializeComponent();
		BindingContext = this;

	}
}