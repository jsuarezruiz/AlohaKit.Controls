namespace AlohaKit.Controls
{
	/// <summary>
	/// The RatingValueChangedEventArgs class provides data for events that report a change in the rating value of a Rating control.
	/// </summary>
	public class RatingValueChangedEventArgs : EventArgs
    {
        public RatingValueChangedEventArgs(double value)
        {
            Value = value;
        }

        public double Value { get; set; }
    }
}