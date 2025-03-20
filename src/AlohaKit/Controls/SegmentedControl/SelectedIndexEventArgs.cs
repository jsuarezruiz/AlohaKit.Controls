namespace AlohaKit.Controls
{
	/// <summary>
	/// The SelectedIndexEventArgs class provides data for events that report a change in the selected index of a SegmentedControl.
	/// </summary>
	public class SelectedIndexEventArgs : EventArgs
    {
        public SelectedIndexEventArgs(int selectedIndex)
        {
            SelectedIndex = selectedIndex;
        }

        public int SelectedIndex { get; set; }
    }
}