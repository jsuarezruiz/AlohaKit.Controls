namespace AlohaKit.Gallery.ViewModels
{
	public class ChartViewModel : BaseViewModel
	{
		public Dictionary<string, float> Items { get; set; } = new Dictionary<string, float>()
		{
			{"Samsung", 27.1f},
			{"Apple", 26.42f},
			{"Xiaomi", 11.38f},
			{"Huawei", 7.9f},
			{"Oppo", 5.61f},
			{"Vivo", 4.59f},
			{"Other", 10.55f}
		};
	}
}
