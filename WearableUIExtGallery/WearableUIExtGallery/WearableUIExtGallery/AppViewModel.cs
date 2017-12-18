using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WearableUIExtGallery.TC;
using Xamarin.Forms;

namespace WearableUIExtGallery
{
    public class AppViewModel
    {
        public AppViewModel()
        {
            TCs = new ObservableCollection<TCDescribe>();
            TCs.Add(new TCDescribe { Title = "Toast", Class = typeof(TCToast) });

        }

        public IList<TCDescribe> TCs { get; private set; }
    }
}
