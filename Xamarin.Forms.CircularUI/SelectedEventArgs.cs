using System;

namespace Xamarin.Forms.CircularUI
{
    /// <summary>
    /// Event arguments for events of RadioButton.
    /// </summary>
    public class SelectedEventArgs : EventArgs
    {
        /// <summary>
        /// Creates a new SelectedEventArgs object that represents a change from RadioButton.
        /// </summary>
        /// <param name="value">The boolean value that checks whether the RadioButton is selected.</param>
        public SelectedEventArgs(bool value)
        {
            Value = value;
        }

        /// <summary>
        /// Gets the value object for the SelectedEventArgs object.
        /// </summary>
        public bool Value { get; private set; }
    }
}
