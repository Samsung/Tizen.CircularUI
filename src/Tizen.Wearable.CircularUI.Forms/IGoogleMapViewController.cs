using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Tizen.Wearable.CircularUI.Forms
{
    /// <summary>
    /// For internal use by renderers.
    /// </summary>
    public interface IGoogleMapViewController : IViewController
    {
        event EventHandler LoadMapRequested;
    }
}
