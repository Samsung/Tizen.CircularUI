using Xamarin.Forms;

namespace CircularUI
{
    /// <summary>
    /// This interface, which defines the ability to display simple text, is used internally.
    /// </summary>
    /// <since_tizen> 4 </since_tizen>
    internal interface IToast
    {
        /// <summary>
        /// Gets or sets the duration.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        int Duration { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        string Text { get; set; }

        /// <summary>
        /// Gets or sets the file path of icon.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        FileImageSource Icon { get; set; }

        /// <summary>
        /// Shows the view for the specified duration.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        void Show();

        /// <summary>
        /// Dismisses the specified view.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        void Dismiss();
    }
}
