using AlohaKit.Gallery.Models;

namespace AlohaKit.Gallery.ViewModels.Base
{
    public abstract class BaseGalleryViewModel : BaseViewModel
    {
        public BaseGalleryViewModel()
        {
            var items = CreateItems();

            if (items != null)
                Items = items.ToList();
        }

        public IReadOnlyList<SectionModel> Items { get; }

        protected abstract IEnumerable<SectionModel> CreateItems();
    }
}