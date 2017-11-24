using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WearableUIGallery.TC;
using Xamarin.Forms;

namespace WearableUIGallery
{
    public class AppViewModel
    {
        public AppViewModel()
        {
            TCs = new ObservableCollection<TCDescribe>();

            TCs.Add(new TCDescribe { Title = "", Class = null }); // for top padding
            TCs.Add(new TCDescribe { Title = "CirclePage", Class = typeof(TCCirclePage) });
            TCs.Add(new TCDescribe { Title = "CircleScroller", Class = typeof(TCCircleScroller) });
            TCs.Add(new TCDescribe { Title = "IndexPage", Class = typeof(TCIndexPage) });
            TCs.Add(new TCDescribe { Title = "Check", Class = typeof(TCCheck) });
            TCs.Add(new TCDescribe { Title = "", Class = null }); // for bottom padding
        }

        public IList<TCDescribe> TCs { get; private set; }
    }
}
