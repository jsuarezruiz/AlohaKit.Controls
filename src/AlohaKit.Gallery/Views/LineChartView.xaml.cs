using AlohaKit.Models;
using System.Collections.ObjectModel;

namespace AlohaKit.Gallery.Views;

public partial class LineChartView : ContentPage
{
	ObservableCollection<ChartItem> _chartCollection = new ObservableCollection<ChartItem>()
		{
			{new ChartItem(){ Value= 1500000, Label = "Value 1", IsLabelBold = false}},
			{new ChartItem(){ Value= 1100000, Label = "Value 2"} },
			{new ChartItem(){ Value= 10000000, Label = "Value 3"} },
			{new ChartItem(){ Value= 3000000, Label = "Value 4"} },
			{new ChartItem(){ Value= 5000000, Label = "Value 5"} }
		};

	public ObservableCollection<ChartItem> ChartCollection
	{
		get => _chartCollection;
		set => _chartCollection = value;
	}

	public LineChartView()
	{
		InitializeComponent();
		BindingContext = this;

	}
}