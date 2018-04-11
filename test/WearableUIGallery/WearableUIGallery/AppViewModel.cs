/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://floralicense.org/license/
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

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
            TCs.Add(new TCDescribe { Title = "CircleStackLayout", Class = typeof(TCCircleStackLayout) });
            TCs.Add(new TCDescribe { Title = "CirclePage", Class = typeof(TCCirclePage) });
            TCs.Add(new TCDescribe { Title = "ConfirmEffect", Class = typeof(TCConfirm) });
            TCs.Add(new TCDescribe { Title = "GroupList", Class = typeof(TCGroupList) });
            TCs.Add(new TCDescribe { Title = "CircleListView", Class = typeof(TCCircleListView) });
            TCs.Add(new TCDescribe { Title = "ViewCell", Class = typeof(TCViewCell) });
            TCs.Add(new TCDescribe { Title = "ListViewNormal", Class = typeof(TCListView) });
            TCs.Add(new TCDescribe { Title = "CircleScroller", Class = typeof(TCCircleScroller) });
            TCs.Add(new TCDescribe { Title = "CircleStepper", Class = typeof(TCCircleStepperList) });
            TCs.Add(new TCDescribe { Title = "IndexPage", Class = typeof(TCIndexPage) });
            TCs.Add(new TCDescribe { Title = "TwoButtonPage", Class = typeof(TCTwoButtonPage) });
            TCs.Add(new TCDescribe { Title = "ContextPopup", Class = typeof(TCContextPopup) });
            TCs.Add(new TCDescribe { Title = "ConfirmationPopup", Class = typeof(TCConfirmationPopup) });
            TCs.Add(new TCDescribe { Title = "InformationPopup", Class = typeof(TCInformationPopup) });
            TCs.Add(new TCDescribe { Title = "Toast", Class = typeof(TCToast) });
            TCs.Add(new TCDescribe { Title = "RotationReceiver", Class = typeof(TCIRotaryEventReceiver) });
            TCs.Add(new TCDescribe { Title = "Check", Class = typeof(TCCheck) });
            TCs.Add(new TCDescribe { Title = "Radio", Class = typeof(TCRadioList) });
            TCs.Add(new TCDescribe { Title = "Performance", Class = typeof(TCPerformance) });

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
