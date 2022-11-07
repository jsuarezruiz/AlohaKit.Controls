using AlohaKit.UI.Extensions;
using Microsoft.Maui.Graphics.Converters;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace AlohaKit.UI
{
    public interface IView : IElement
    {
        Brush Background { get; set; }

		void StartHoverInteraction(PointF[] points);
		void MoveHoverInteraction(PointF[] points);
		void EndHoverInteraction();
		void StartInteraction(PointF[] points);
		void DragInteraction(PointF[] points);
		void EndInteraction(PointF[] points, bool isInsideBounds);
		void CancelInteraction();
	}

    public class View : Element, IView
    {
        protected bool _drawBackground;

		readonly ObservableCollection<GestureRecognizer> _gestureRecognizers = new ObservableCollection<GestureRecognizer>();

		protected internal View()
		{
			_gestureRecognizers.CollectionChanged += (sender, args) =>
			{
				void AddItems()
				{
					foreach (IElement item in args.NewItems.OfType<Element>())
					{
						item.Parent = this;
					}
				}

				void RemoveItems()
				{
					foreach (IElement item in args.OldItems.OfType<IElement>())
					{
						item.Parent = null;
					}
				}

				switch (args.Action)
				{
					case NotifyCollectionChangedAction.Add:
						AddItems();
						break;
					case NotifyCollectionChangedAction.Remove:
						RemoveItems();
						break;
					case NotifyCollectionChangedAction.Replace:
						AddItems();
						RemoveItems();
						break;
					case NotifyCollectionChangedAction.Reset:
						foreach (IElement item in _gestureRecognizers.OfType<Element>())
							item.Parent = this;
						break;
				}
			};
		}

        public static readonly BindableProperty BackgroundProperty =
            BindableProperty.Create(nameof(Background), typeof(Brush), typeof(View), null,
                propertyChanged: BackgroundPropertyChanged);

        public static void BackgroundPropertyChanged(BindableObject bindableObject, object oldValue, object newValue)
        {
            ((View)bindableObject).Invalidate();
            ((View)bindableObject)._drawBackground = true;
        }

        [TypeConverter(typeof(ColorTypeConverter))]
        public Brush Background
        {
            get => (Color)GetValue(BackgroundProperty);
            set => SetValue(BackgroundProperty, value);
        }

		public IList<GestureRecognizer> GestureRecognizers => _gestureRecognizers;

		public override void Draw(ICanvas canvas, RectF bounds)
        {
            if (IsVisible)
            {
                if (_drawBackground)
                {
                    DrawBackground(canvas, bounds);
                }

                canvas.Alpha = (float)Opacity;
                canvas.Transform(Rotation, TranslationX, TranslationY, ScaleX, ScaleY);
                
                DrawShadow(canvas, bounds);

                base.Draw(canvas, bounds); 
            }
        }

		public virtual void CancelInteraction() { }

		public virtual void DragInteraction(PointF[] points) { }

		public virtual void EndHoverInteraction() { }

		public virtual void EndInteraction(PointF[] points, bool isInsideBounds) { }

		public virtual void StartHoverInteraction(PointF[] points) { }

		public virtual void MoveHoverInteraction(PointF[] points) { }

		public virtual void StartInteraction(PointF[] points) { }

		void DrawBackground(ICanvas canvas, RectF bounds)
        {
            if (Background is SolidColorBrush solidColorBrush)
                canvas.FillColor = solidColorBrush.Color;
            else
                canvas.SetFillPaint(Background, bounds);

            canvas.FillRectangle(bounds);
        }

        void DrawShadow(ICanvas canvas, RectF bounds)
        {
            if (Shadow != null)
            {
                var offset = new SizeF((float)Shadow.Offset.X, (float)Shadow.Offset.Y);
                var radius = Shadow.Radius;
                var color = Shadow.Color;

                canvas.SetShadow(offset, radius, color);
            }
        }
    }
}