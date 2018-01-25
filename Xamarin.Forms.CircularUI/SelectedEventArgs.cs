using System;

namespace Xamarin.Forms.CircularUI
{
    /// <summary>
    /// Event arguments for events of RadioButton.
    /// </summary>
    /// <since_tizen> 4 </since_tizen>
    public class SelectedEventArgs : EventArgs
    {
        /// <summary>
        /// Creates a new SelectedEventArgs object that represents a change from RadioButton.
        /// </summary>
        /// <param name="value">The boolean value that checks whether the RadioButton is selected.</param>
        /// <since_tizen> 4 </since_tizen>
        public SelectedEventArgs(bool value)
        {
            Value = value;
        }

        /// <summary>
        /// Gets the value object for the SelectedEventArgs object.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public bool Value { get; private set; }
    }
}
