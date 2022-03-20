using AlohaKit.Gallery.Models;
using System.Windows.Input;

namespace AlohaKit.Gallery.Views.Base
{
	public class BasePage : ContentPage
	{
		SectionModel _selectedItem;

		public BasePage()
		{
			NavigateCommand = new Command(async () =>
			{
				if (SelectedItem != null)
				{	
					await Navigation.PushAsync(PreparePage(SelectedItem));

					SelectedItem = null;
				}
			});
		}

		public ICommand NavigateCommand { get; }

		public SectionModel SelectedItem
		{
			get { return _selectedItem; }
			set
			{
				_selectedItem = value;
				OnPropertyChanged();
			}
		}

		Page PreparePage(SectionModel model)
		{
			var page = (Handler?.MauiContext?.Services?.GetService(model.Type) as Page) ?? (Page)Activator.CreateInstance(model.Type);
			page.Title = model.Title;

			return page;
		}
	}
}