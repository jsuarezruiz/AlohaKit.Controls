using AlohaKit.Gallery.ViewModels;

namespace AlohaKit.Gallery;

public partial class PieChartView : ContentPage
{
	public PieChartView()
	{
		InitializeComponent();

		BindingContext = new ChartViewModel();
	}
}