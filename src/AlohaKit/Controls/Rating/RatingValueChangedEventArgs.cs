namespace AlohaKit.Controls
{
    public class RatingValueChangedEventArgs : EventArgs
    {
        public RatingValueChangedEventArgs(double value)
        {
            Value = value;
        }

        public double Value { get; set; }
    }
}