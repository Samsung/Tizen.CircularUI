using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Xamarin.Forms.CircularUI
{
    /// <summary>
    /// The class for the items in a ContextPopup.
    /// </summary>
    public class ContextPopupItem : INotifyPropertyChanged
    {
        string _label;

        /// <summary>
        /// Creates a ContextPopupItem with only a label.
        /// </summary>
        /// <param name="label">The label of the ContextPopupItem.</param>
        public ContextPopupItem(string label)
        {
            _label = label;
        }

        /// <summary>
        /// Occurs when the label or an icon of a ContextPopupItem is changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Gets or sets the label of a ContextPopupItem.
        /// </summary>
        public string Label
        {
            get
            {
                return _label;
            }
            set
            {
                if (value != _label)
                {
                    _label = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Called when a bindable property has changed.
        /// </summary>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
