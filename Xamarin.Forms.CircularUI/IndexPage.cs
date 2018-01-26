using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Xamarin.Forms.Internals;

namespace Xamarin.Forms.CircularUI
{
    /// <summary>
    /// The IndexPage is extension of Xamarin.Forms.MultiPage.
    /// When Page is added/removed at Multipage. circular index is added/removed automatically at the top of window.
    /// </summary>
    /// <since_tizen> 4 </since_tizen>
    public class IndexPage : MultiPage<ContentPage>
    {
        protected override ContentPage CreateDefault(object item)
        {
            var page = new ContentPage();
            return page;
        }
    }
}
