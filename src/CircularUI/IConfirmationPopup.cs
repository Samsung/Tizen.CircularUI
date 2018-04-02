using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CircularUI
{
    /// <summary>
    /// The IConfirmationPopup is an interface to describe confirmation pop-up which has circular two button, title, text, and content area
    /// </summary>
    /// <since_tizen> 4 </since_tizen>
    internal interface IConfirmationPopup
    {
        /// <summary>
        /// Occurs when the Back button is pressed.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        event EventHandler BackButtonPressed;

        /// <summary>
        /// Gets or sets content view of the Popup.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        View Content { get; set; }

        /// <summary>
        /// Gets or sets left button of the Popup
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        MenuItem FirstButton { get; set; }
        /// <summary>
        /// Gets or sets right button of the Popup
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        MenuItem SecondButton { get; set; }

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