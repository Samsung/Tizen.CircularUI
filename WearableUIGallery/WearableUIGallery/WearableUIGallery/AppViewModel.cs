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
            TCs.Add(new TCDescribe { Title = "ConfirmEffect", Class = typeof(TCConfirm) });
            TCs.Add(new TCDescribe { Title = "GroupList", Class = typeof(TCGroupList) });
            TCs.Add(new TCDescribe { Title = "CircleListView", Class = typeof(TCCircleListView) });
            TCs.Add(new TCDescribe { Title = "ViewCell", Class = typeof(TCViewCell) });
            TCs.Add(new TCDescribe { Title = "ListViewNormal", Class = typeof(TCListView) });
            TCs.Add(new TCDescribe { Title = "CircleScroller", Class = typeof(TCCircleScroller) });
            TCs.Add(new TCDescribe { Title = "CircleStepper", Class = typeof(TCCircleStepperList) });
            TCs.Add(new TCDescribe { Title = "IndexPage", Class = typeof(TCIndexPage) });
            TCs.Add(new TCDescribe { Title = "Check", Class = typeof(TCCheck) });
            TCs.Add(new TCDescribe { Title = "ContextPopup", Class = typeof(TCContextPopup) });
            TCs.Add(new TCDescribe { Title = "TwoButtonPage", Class = typeof(TCTwoButtonPage) });
            TCs.Add(new TCDescribe { Title = "TwoButtonPopup", Class = typeof(TCTwoButtonPopup) });
            TCs.Add(new TCDescribe { Title = "RotationReceiver", Class = typeof(TCIRotaryEventReceiver) });
            TCs.Add(new TCDescribe { Title = "Radio", Class = typeof(TCRadioList) });
            TCs.Add(new TCDescribe { Title = "Toast", Class = typeof(TCToast) });

            // CircleStepper TCs
            CircleStepperTCs = new ObservableCollection<TCDescribe>();
            CircleStepperTCs.Add(new TCDescribe { Title = "Grid", Class = typeof(TCCircleStepper) });
            CircleStepperTCs.Add(new TCDescribe { Title = "AbsoluteLayout", Class = typeof(TCCircleStepper2) });
            CircleStepperTCs.Add(new TCDescribe { Title = "StackLayout", Class = typeof(TCCircleStepper3) });
            CircleStepperTCs.Add(new TCDescribe { Title = "LabelFormat", Class = typeof(TCCircleStepper4) });

            // Radio TCs
            RadioTCs = new ObservableCollection<TCDescribe>();
            RadioTCs.Add(new TCDescribe { Title = "StackLayout", Class = typeof(TCRadio) });
            RadioTCs.Add(new TCDescribe { Title = "ListView", Class = typeof(TCRadioGroup) });
        }

        public IList<TCDescribe> TCs { get; private set; }
        public IList<TCDescribe> CircleStepperTCs { get; private set; }
        public IList<TCDescribe> RadioTCs { get; private set; }
    }
}
