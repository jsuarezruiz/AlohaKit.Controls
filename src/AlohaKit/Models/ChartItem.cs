using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AlohaKit.Models
{
	public class ChartItem : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        #region private Members
        private int _styleId;
        private int _groupId;
        private float _value = 0f;
        private string _label = string.Empty;
        private bool _isValueBold = false;
        private bool _isLabelBold = false;
        #endregion

        /// <summary>
        /// Depending on the chart type, this property will be used to map each entry indivual style to matching GroupStyles.Id style
        /// For example, MultiBarChart will use this to set each bar colors
        /// </summary>
        public int StyleId
        {
            get => _styleId;
            set
            {
                _styleId = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Group identifier. This value will be used internally to group entries by id
        /// </summary>
        public int GroupId
        {
            get => _groupId;
            set
            {
                _groupId = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Use Bold system font for this entry label
        /// </summary>
        public bool IsLabelBold
        {
            get => _isLabelBold;
            set
            {
                _isLabelBold = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Use Bold system font for this entry value
        /// </summary>
        public bool IsValueBold
        {
            get => _isValueBold;
            set
            {
                _isValueBold = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Value to display
        /// </summary>
        public float Value
        {
            get => _value;
            set
            {
                _value = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Footer value associated to current value
        /// </summary>
        public string Label
        {
            get => _label;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _label = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
