using AlohaKit.Models;
using System.Collections.ObjectModel;

namespace AlohaKit.Gallery.Views;

public partial class BarChartView : ContentPage
{
	ObservableCollection<ChartItem> _chartCollection = new ObservableCollection<ChartItem>()
		{
			{new ChartItem(){ Value= 1000000, Label = "Value 1", IsLabelBold = false}},
			{new ChartItem(){ Value= 1500000, Label = "Value 2"} },
			{new ChartItem(){ Value= 2000000, Label = "Value 3"} },
			{new ChartItem(){ Value= 3000000, Label = "Value 4"} },
			{new ChartItem(){ Value= 6000000, Label = "Value 5"} }
		};

	public ObservableCollection<ChartItem> ChartCollection
	{
		get => _chartCollection;
		set => _chartCollection = value;
	}

	public BarChartView()
	{
		InitializeComponent();
		BindingContext = this;

	}
}