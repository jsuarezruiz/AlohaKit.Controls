using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AlohaKit.Models
{
	public class ChartGroupStyle : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        private int _id;
        private Color _color = null;
        private Color _backgroundColor = null;
        private Brush _background = null;

        public Color Color
        {
            get => _color;
            set
            {
                _color = value;
                OnPropertyChanged();

            }
        }

        /// <summary>
        /// Brush to be used for group background. Used for MultiLineChart
        /// </summary>
        public Brush Background
        {
            get => _background;
            set
            {
                _background = value;
                OnPropertyChanged();

            }
        }

        /// <summary>
        /// Color to be used for each group series background. Used for MultiLineChart
        /// </summary>
        public Color BackgroundColor
        {
            get => _backgroundColor;
            set
            {
                _backgroundColor = value;
                OnPropertyChanged();

            }
        }

        /// <summary>
        /// Style identifier. Depending on chart type, current style will be applied globally/individually to the group entries that matches this Id
        /// </summary>
        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged();
            }
        }

    }
}

