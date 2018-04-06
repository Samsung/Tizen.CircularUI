/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using ElmSharp;
using System;
using CircularUI;
using Xamarin.Forms.Platform.Tizen;

[assembly: ExportRenderer(typeof(TwoButtonPage), typeof(CircularUI.Tizen.TwoButtonPageRenderer))]


namespace CircularUI.Tizen
{
    public class TwoButtonPageRenderer : VisualElementRenderer<TwoButtonPage>
    {
        TwoButtonPageWidget _widget;

        public TwoButtonPageRenderer()
        {
            RegisterPropertyHandler(TwoButtonPage.BackgroundImageProperty, UpdateBackgroundImage);
            RegisterPropertyHandler(TwoButtonPage.FirstButtonProperty, UpdateFirstButton);
            RegisterPropertyHandler(TwoButtonPage.SecondButtonProperty, UpdateSecondButton);
            RegisterPropertyHandler(TwoButtonPage.TitleProperty, UpdateTitle);
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
        }

        public override Rect GetNativeContentGeometry()
        {
            return _widget.Canvas.Geometry;
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
            Console.WriteLine($"Update Button2 init={initialize}, button={Element.SecondButton}");
            if (initialize && Element.SecondButton == null) return;
            if (Element.SecondButton != null)
            {
                var item = Element.SecondButton;
                _widget.ShowButton2(item.Text, item.Icon, () => item.Activate());
            }
            else
            {
                _widget.HideButton2();
            }
        }

        void UpdateFirstButton(bool initialize)
        {
            Console.WriteLine($"Update Button1 init={initialize}, button={Element.FirstButton}");
            if (initialize && Element.FirstButton == null) return;
            if (Element.FirstButton != null)
            {
                var item = Element.FirstButton;
                _widget.ShowButton1(item.Text, item.Icon, () => item.Activate());
            }
            else
            {
                _widget.HideButton1();
            }
        }
        void UpdateTitle()
        {
            _widget.Title = Element.Title;
        }
    }
}
