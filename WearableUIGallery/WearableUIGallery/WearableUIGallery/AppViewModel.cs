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
            TCs.Add(new TCDescribe { Title = "CirclePage", Class = typeof(TCCirclePage) });
            TCs.Add(new TCDescribe { Title = "CircleListView", Class = typeof(TCCircleListView) });
            TCs.Add(new TCDescribe { Title = "CircleScroller", Class = typeof(TCCircleScroller) });
            TCs.Add(new TCDescribe { Title = "CircleStepper", Class = typeof(TCCircleStepper) });
            TCs.Add(new TCDescribe { Title = "IndexPage", Class = typeof(TCIndexPage) });
            TCs.Add(new TCDescribe { Title = "Check", Class = typeof(TCCheck) });
            TCs.Add(new TCDescribe { Title = "ContextPopup", Class = typeof(TCContextPopup) });
            TCs.Add(new TCDescribe { Title = "TwoButtonPage", Class = typeof(TCTwoButtonPage) });
            TCs.Add(new TCDescribe { Title = "TwoButtonPopup", Class = typeof(TCTwoButtonPopup) });
            TCs.Add(new TCDescribe { Title = "RotationReceiver", Class = typeof(TCIRotaryEventReceiver) });
        }

        public IList<TCDescribe> TCs { get; private set; }
    }
}
