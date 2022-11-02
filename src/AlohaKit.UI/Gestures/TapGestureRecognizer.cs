using System.ComponentModel;
using System.Windows.Input;

namespace AlohaKit.UI
{
	public class TapGestureRecognizer : GestureRecognizer
	{
		public static readonly BindableProperty CommandProperty =
			BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(TapGestureRecognizer), null);

		public static readonly BindableProperty CommandParameterProperty =
			BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(TapGestureRecognizer), null);

		public ICommand Command
		{
			get { return (ICommand)GetValue(CommandProperty); }
			set { SetValue(CommandProperty, value); }
		}

		public object CommandParameter
		{
			get { return GetValue(CommandParameterProperty); }
			set { SetValue(CommandParameterProperty, value); }
		}

		public event EventHandler Tapped;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public void SendTapped(View sender)
		{
			ICommand cmd = Command;

			if (cmd != null && cmd.CanExecute(CommandParameter))
				cmd.Execute(CommandParameter);

			Tapped?.Invoke(sender, new TappedEventArgs(CommandParameter));
		}
	}
}