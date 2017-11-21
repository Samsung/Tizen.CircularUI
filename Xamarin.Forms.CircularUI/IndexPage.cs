using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Xamarin.Forms.Internals;

namespace Xamarin.Forms.CircularUI
{
    public class IndexPage : MultiPage<ContentPage>
    {
        protected override ContentPage CreateDefault(object item)
        {
            var page = new ContentPage();
            return page;
        }
    }
}
