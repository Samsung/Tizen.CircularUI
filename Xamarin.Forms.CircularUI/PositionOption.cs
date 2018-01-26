using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin.Forms.CircularUI
{
    /// <summary>
    /// Enumeration for position type of popup
    /// </summary>
    /// <since_tizen> 4 </since_tizen>
    public enum PositionOption
    {
        /// <summary>
        /// The popup appears at the bottom of the View using the Effect. The position is changed by Offset in the center of View.
        /// </summary>
        BottomOfView,
        /// <summary>
        /// In the center of the screen, move by the Offset in the Popup.
        /// </summary>
        CenterOfParent,
        /// <summary>
        /// The value of Offset is X, Y and popup is placed on the screen.
        /// </summary>
        Absolute,
        /// <summary>
        /// Set Offset.X * Window.Width, Offset.Y * Window.Height.
        /// </summary>
        Relative
    }
}
