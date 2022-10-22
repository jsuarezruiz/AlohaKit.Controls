using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace AlohaKit.UI
{
    public class ElementsCollection : ObservableCollection<IElement>
    {
        readonly IElement _parent;

        public ElementsCollection()
        {

        }

        internal ElementsCollection(IElement parent)
        {
            _parent = parent;
        }

        protected override void ClearItems()
        {
            base.ClearItems();
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnCollectionChanged(e);

            if (e.OldItems != null)
            {
                foreach (var oldItem in e.OldItems)
                {
                    ((IElement)oldItem).AttachParent(null);
                }
            }

            if (e.NewItems != null)
            {
                foreach (var newItem in e.NewItems)
                {
                    ((IElement)newItem).AttachParent(_parent);
                }
            }
            
            Invalidate();
        }

        void Invalidate()
        {
            _parent?.Invalidate();
        }
    }
}
