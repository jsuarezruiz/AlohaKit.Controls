namespace AlohaKit.UI
{
    public interface IElement : IVisualElement
    {
        IElement Parent { get; set; }
        ElementsCollection Children { get; }

        void AttachParent(IElement parent);
    }

    [ContentProperty(nameof(Children))]
    public class Element : VisualElement, IElement
    {
        IElement _parent;
 
        public Element()
        {
            Children = new ElementsCollection(this);
        }

        public IElement Parent
        {
            get => _parent;
            set
            {
                if (_parent != value)
                {
                    _parent?.Children.Remove(this);
                    AttachParent(value);
                    _parent?.Children.Add(this);
                }
            }
        }

        public ElementsCollection Children { get; private set; }

        public override void Draw(ICanvas canvas, RectF bounds)
        {
            base.Draw(canvas, bounds);

            DrawChildren(canvas, bounds);
        }

        protected virtual void DrawChildren(ICanvas canvas, RectF bounds)
        {
            foreach (var child in Children)
            {
                child.Draw(canvas, bounds);
            }
        }

        public virtual void AttachParent(IElement parent)
        {
            _parent = parent;
        }

        public override void Invalidate()
        {
            base.Invalidate();

            Parent?.Invalidate();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            foreach (var child in Children)
            {
                if (child is BindableObject bChild)
                {
                    SetInheritedBindingContext(bChild, this.BindingContext);
                }
            }
        }
    }
}