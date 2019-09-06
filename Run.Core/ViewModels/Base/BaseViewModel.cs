namespace Run.Core
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public abstract class BaseViewModel
    {
        public double Width { get; }
        public double Height { get; }

        protected BaseViewModel(double width, double height)
        {
            Width = width;
            Height = height;
        }

        protected BaseViewModel() : this(1200, 800) { }
    }
}