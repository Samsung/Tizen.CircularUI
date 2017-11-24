using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

using Xamarin.Forms;

namespace Xamarin.Forms.CircularUI
{
    /// <summary>
    /// Enumeration for the style of the Check.
    /// </summary>
    public enum CheckDisplayStyle 
    {
        /// <summary>
        /// The default style is checkbox style.
        /// </summary>
        Default,

        /// <summary>
        /// The toggle switch style.
        /// </summary>
        Onoff,

        /// <summary>
        /// The small checkbox style. this style is only for circular devices 
        /// </summary>
        Small
    }
}
