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

using ElmSharp;
using System;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms.Platform.Tizen;

[assembly: ExportRenderer(typeof(TwoButtonPage), typeof(Tizen.Wearable.CircularUI.Forms.Renderer.TwoButtonPageRenderer))]


namespace Tizen.Wearable.CircularUI.Forms.Renderer
{
    public class TwoButtonPageRenderer : VisualElementRenderer<TwoButtonPage>
    {
        TwoButtonPageWidget _widget;

        public TwoButtonPageRenderer()
        {
            RegisterPropertyHandler(TwoButtonPage.BackgroundImageProperty, UpdateBackgroundImage);
            RegisterPropertyHandler(TwoButtonPage.FirstButtonProperty, UpdateFirstButton);
            RegisterPropertyHandler(TwoButtonPage.SecondButtonProperty, UpdateSecondButton);
            RegisterPropertyHandler(TwoButtonPage.OverlapProperty, UpdateOverlap);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<TwoButtonPage> e)
        {
            if (_widget == null)
            {
                _widget = new TwoButtonPageWidget(Xamarin.Forms.Platform.Tizen.Forms.NativeParent);
                _widget.Canvas.LayoutUpdated += OnLayoutUpdated;
                SetNativeView(_widget);
            }

            base.OnElementChanged(e);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_widget != null)
                {
                    _widget.Canvas.LayoutUpdated -= OnLayoutUpdated;
                }
            }
            base.Dispose(disposing);
        }

        protected override void UpdateBackgroundColor(bool initialize)
        {
            if (initialize && Element.BackgroundColor.IsDefault) return;

            if (Element.BackgroundColor.A == 0)
            {
                _widget.BackgroundColor = ElmSharp.Color.Transparent;
            }
            else
            {
                _widget.BackgroundColor = Element.BackgroundColor.ToNative();
            }
        }

        protected override void UpdateLayout()
        {
            // Do Nothing because ignore base UpdateLayout
        }

        public override Rect GetNativeContentGeometry()
        {
            return _widget.Canvas.Geometry;
        }

        void UpdateOverlap()
        {
            _widget.Overlap = Element.Overlap;
        }

        void OnLayoutUpdated(object sender, Xamarin.Forms.Platform.Tizen.Native.LayoutEventArgs e)
        {
            Element.Layout(e.Geometry.ToDP());
        }

        void UpdateBackgroundImage(bool initialize)
        {
            if (initialize && string.IsNullOrEmpty(Element.BackgroundImage))
                return;

            if (string.IsNullOrEmpty(Element.BackgroundImage))
                _widget.File = null;
            else
                _widget.File = ResourcePath.GetPath(Element.BackgroundImage);
        }

        void UpdateSecondButton(bool initialize)
        {
            if (initialize && Element.SecondButton == null) return;
            if (Element.SecondButton != null)
            {
                if (Element.SecondButton is ColorMenuItem)
                {
                    var item = ((ColorMenuItem)Element.SecondButton);
                    _widget.ShowButton2(item.Text, item.Icon, item.BackgroundColor.ToNative(), () => item.Activate());
                }
                else
                {
                    var item = Element.SecondButton;
                    _widget.ShowButton2(item.Text, item.Icon, () => item.Activate());
                }
            }
            else
            {
                _widget.HideButton2();
            }
        }

        void UpdateFirstButton(bool initialize)
        {
            if (initialize && Element.FirstButton == null) return;
            if (Element.FirstButton != null)
            {
                if (Element.FirstButton is ColorMenuItem)
                {
                    var item = ((ColorMenuItem)Element.FirstButton);
                    _widget.ShowButton1(item.Text, item.Icon, item.BackgroundColor.ToNative(), () => item.Activate());
                }
                else
                {
                    var item = Element.FirstButton;
                    _widget.ShowButton1(item.Text, item.Icon, () => item.Activate());
                }
            }
            else
            {
                _widget.HideButton1();
            }
        }
    }
}
