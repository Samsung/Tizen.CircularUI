using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Xamarin.Forms.CircularUI
{
    /// <summary>
    /// Interface for describe pop-up which has circular two button, title, text, and content area
    /// </summary>
    public interface IPopup
    {
        /// <summary>
        /// It will be triggered when the Back button is pressed.
        /// </summary>
        event EventHandler BackButtonPressed;

        /// <summary>
        /// Gets or sets the Content
        /// </summary>
        View Content { get; set; }

        /// <summary>
        /// Gets or sets the left button of popup
        /// </summary>
        MenuItem FirstButton { get; set; }
        /// <summary>
        /// Gets or sets the right button of popup
        /// </summary>
        MenuItem SecondButton { get; set; }

        /// <summary>
        /// Gets or sets the title of popup
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// Gets or sets the body text of popup
        /// </summary>
        string Text { get; set; }

        /// <summary>
        /// show popup
        /// </summary>
        void Show();
        /// <summary>
        /// Dismiss popup
        /// </summary>
        void Dismiss();
    }
}