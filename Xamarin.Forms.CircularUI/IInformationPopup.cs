using System;

namespace Xamarin.Forms.CircularUI
{
    /// <summary>
    /// The IInformationPopup is an interface to describe information pop-up which has circular bottom button, title, text, and content area
    /// </summary>
    /// <since_tizen> 4 </since_tizen>
    internal interface IInformationPopup
    {
        /// <summary>
        /// Occurs when the Back button is pressed.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        event EventHandler BackButtonPressed;

        /// <summary>
        /// Gets or sets progress visibility of the Popup.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        bool IsProgressRunning { get; set; }

        /// <summary>
        /// Gets or sets bottom button of the Popup
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        MenuItem BottomButton { get; set; }

        /// <summary>
        /// Gets or sets title of the Popup
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        string Title { get; set; }

        /// <summary>
        /// Gets or sets text of the Popup
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        string Text { get; set; }

        /// <summary>
        /// Shows the Popup
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        void Show();
        /// <summary>
        /// Dismisses the Popup
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        void Dismiss();
    }
}