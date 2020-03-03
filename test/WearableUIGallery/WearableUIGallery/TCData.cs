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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WearableUIGallery.TC;

namespace WearableUIGallery
{
    public static class TCData
    {
        public static IList<TCDescribe> TCs { get; private set; }

        static TCData()
        {
            TCs = new ObservableCollection<TCDescribe>();
            TCs.Add(new TCDescribe
            {
                Title = "ShellTest",
                Class = new TCTypes
                {
                    new TCDescribe { Title = "FlyoutIconTest", Class = typeof(FlyoutIconTest) },
                    new TCDescribe { Title = "FlyoutIconBackgroundColorTest", Class = typeof(FlyoutIconBackgroundColorTest) },
                    new TCDescribe { Title = "FlyoutBackgroundColorTest", Class = typeof(FlyoutBackgroundColorTest) },
                    new TCDescribe { Title = "FlyoutForegroundColorTest", Class = typeof(FlyoutForegroundColorTest) },
                }
            });
            TCs.Add(new TCDescribe
            {
                Title = "ListView",
                Class = new TCTypes
                {
                    new TCDescribe { Title = "NoFishEyeHeaderList", Class = typeof(TCCircleListViewNoEffect) },
                    new TCDescribe { Title = "CircleListBehavior", Class = typeof(TCListAppender) },
                    new TCDescribe { Title = "GroupList", Class = typeof(TCGroupList) },
                    new TCDescribe { Title = "CircleListView", Class = typeof(TCCircleListView) },
                    new TCDescribe { Title = "ViewCell", Class = typeof(TCViewCell) },
                    new TCDescribe { Title = "ListViewNormal", Class = typeof(TCListView) },
                    new TCDescribe { Title = "HeaderWithGroup", Class = typeof(TCCircleListViewGroupHeader) },
                    new TCDescribe { Title = "HeaderWithoutGroup", Class = typeof(TCCircleListViewHeaderWithoutGroup) },
                    new TCDescribe { Title = "ChangeBarColor", Class = typeof(TCCircleListViewBarColor) },
                }
            });
            TCs.Add(new TCDescribe
            {
                Title = "CirclePage",
                Class = new TCTypes
                {
                    new TCDescribe { Title = "CirclePageBehavior", Class = typeof(TCCirclePage) },
                    new TCDescribe { Title = "RotaryFocus", Class = typeof(TCRotaryFocus) },
                    new TCDescribe { Title = "ActionButton", Class = typeof(TCActionButton) }
                }
            });
            TCs.Add(new TCDescribe { Title = "DateTimeSelector", Class = typeof(TCCircleDateTimeSelector) });
            TCs.Add(new TCDescribe
            {
                Title = "CircleSurfaceItem",
                Class = new TCTypes
                {
                    new TCDescribe { Title = "Add/Remove SurfaceItems", Class = typeof(TCCircleSurfaceItems) },
                    new TCDescribe { Title = "CircleProgressBar1", Class = typeof(TCCircleProgressBarSurfaceItem1) },
                    new TCDescribe { Title = "CircleProgressBar2", Class = typeof(TCCircleProgressBarSurfaceItem2) },
                    new TCDescribe { Title = "CircleSlider1", Class = typeof(TCCircleSliderSurfaceItem1) },
                    new TCDescribe { Title = "CircleSlider2", Class = typeof(TCCircleSliderSurfaceItem2) }
                }
            });
            TCs.Add(new TCDescribe { Title = "PopupEntry", Class = typeof(TCPopupEntry) });
            TCs.Add(new TCDescribe { Title = "CircleStackLayout", Class = typeof(TCCircleStackLayout) });
            TCs.Add(new TCDescribe
            {
                Title = "ContextPopupBehavior",
                Class = new TCTypes
                {
                    new TCDescribe { Title = "Position", Class = typeof(TCCtxPopup1) },
                    new TCDescribe { Title = "CommandParameter", Class = typeof(TCCtxPopup2) },
                    new TCDescribe { Title = "Attach/Detach/Visibility", Class = typeof(TCCtxPopup3) },
                    new TCDescribe { Title = "Label with TapCtx", Class = typeof(TCCtxPopup4) }
                }
            });
            TCs.Add(new TCDescribe
            {
                Title = "CircleScroller",
                Class = new TCTypes
                {
                     new TCDescribe { Title = "Vertical", Class = typeof(TCCircleScroller) },
                     new TCDescribe { Title = "Horizontal", Class = typeof(TCCircleScroller2) },
                     new TCDescribe { Title = "Remove/Add", Class = typeof(TCCircleScroller3) },
                     new TCDescribe { Title = "ChangeBarColor", Class = typeof(TCCircleScrollerBarColor) }
                }
            });
            TCs.Add(new TCDescribe
            {
                Title = "CircleStepper",
                Class = new TCTypes
                {
                    new TCDescribe { Title = "Grid", Class = typeof(TCCircleStepper) },
                    new TCDescribe { Title = "AbsoluteLayout", Class = typeof(TCCircleStepper2) },
                    new TCDescribe { Title = "Title", Class = typeof(TCCircleStepper3) },
                    new TCDescribe { Title = "LabelFormat", Class = typeof(TCCircleStepper4) },
                    new TCDescribe { Title = "Wrap", Class = typeof(TCCircleStepper5) }
                }
            });
            TCs.Add(new TCDescribe
            {
                Title = "IndexPage",
                Class = new TCTypes
                {
                    new TCDescribe { Title = "Add/Move", Class = typeof(TCIndexPage) },
                    new TCDescribe { Title = "Add (base 1)", Class = typeof(TCIndexPageSingleStart) },
                    new TCDescribe { Title = "Template", Class = typeof(TCIndexPageTemplate) },
                    new TCDescribe { Title = "Focus", Class = typeof(TCIndexPageFocus) },
                    new TCDescribe { Title = "ActionButton", Class = typeof(TCIndexPageActionButton) }
                }
            });
            TCs.Add(new TCDescribe
            {
                Title = "TwoButtonPage",
                Class = new TCTypes
                {
                    new TCDescribe { Title = "TwoButtonPageBehavior", Class = typeof(TCTwoButtonPage) },
                    new TCDescribe { Title = "TwoButtonPageListView", Class = typeof(TCTwoButtonPageListView) },
                    new TCDescribe { Title = "TwoButtonPopup", Class = typeof(TCTwoButtonPopup) },
                    new TCDescribe { Title = "TwoButtonPopupCmd", Class = typeof(TCTwoButtonPopupCmd) }
                }
            });
            TCs.Add(new TCDescribe { Title = "InformationPopup", Class = typeof(TCInformationPopup) });
            TCs.Add(new TCDescribe { Title = "Toast", Class = typeof(TCToast) });
            TCs.Add(new TCDescribe { Title = "RotationReceiver", Class = typeof(TCIRotaryEventReceiver) });
            TCs.Add(new TCDescribe { Title = "Check", Class = typeof(TCCheck) });
            TCs.Add(new TCDescribe
            {
                Title = "Radio",
                Class = new TCTypes
                {
                    new TCDescribe { Title = "StackLayout", Class = typeof(TCRadioStackLayout) },
                    new TCDescribe { Title = "ListView", Class = typeof(TCRadioListView) }
                }
            });
            TCs.Add(new TCDescribe { Title = "Performance", Class = typeof(TCPerformance) });
            TCs.Add(new TCDescribe
            {
                Title = "MediaPlayer",
                Class = new TCTypes
                {
                    new TCDescribe { Title = "MediaView StackLayout", Class = typeof(TCMediaViewStackLayout) },
                    new TCDescribe { Title = "MediaView AbsoluteLayout", Class = typeof(TCMediaViewAbsoluteLayout) },
                }
            });
            TCs.Add(new TCDescribe
            {
                Title = "Map",
                Class = new TCTypes
                {
                    new TCDescribe { Title = "MapView StackLayout1 No GoogleMapOption", Class = typeof(TCMapViewStackLayout1) },
                    new TCDescribe { Title = "MapView StackLayout2 Hybrid", Class = typeof(TCMapViewStackLayout2) },
                    new TCDescribe { Title = "MapView StackLayout3 Satellite", Class = typeof(TCMapViewStackLayout3) },
                    new TCDescribe { Title = "MapView AbsoluteLayout Hybrid", Class = typeof(TCMapViewAbsoluteLayout) },
                    new TCDescribe { Title = "MapView Pins code", Class = typeof(TCMapViewPins1) },
                    new TCDescribe { Title = "MapView Pins Xaml data binding", Class = typeof(TCMapViewPins2) },
                    new TCDescribe { Title = "MapView display static current position", Class = typeof(TCMapViewCurrentPosition) },
                }
            });
            TCs.Add(new TCDescribe
            {
                Title = "CircleImage",
                Class = new TCTypes
                {
                    new TCDescribe { Title = "One Page", Class = typeof(TCCircleImage) },
                    new TCDescribe { Title = "Multi Page", Class = typeof(TCCircleImageIndexPage) },
                    new TCDescribe { Title = "Grid", Class = typeof(TCCircleImageGrid) },
                    new TCDescribe { Title = "ListView", Class = typeof(TCCircleImageListView) },
                }
            });
        }
    }
}
