using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace AlohaKit.UI
{
    public class ElementsCollection : ObservableCollection<IElement>
    {
        readonly IElement _parent;
        readonly ICanvasView _canvasView;

        public ElementsCollection()
        {

        }

        internal ElementsCollection(IElement parent)
        {
            _parent = parent;
		}

		internal ElementsCollection(ICanvasView canvasView)
		{
			_canvasView = canvasView;
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
            _canvasView?.Invalidate();
            _parent?.Invalidate();
        }
    }
}
