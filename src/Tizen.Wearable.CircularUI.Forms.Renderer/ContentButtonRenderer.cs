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
using System.ComponentModel;
using Tizen.Wearable.CircularUI.Forms;
using Tizen.Wearable.CircularUI.Forms.Renderer;
using ElmSharp;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using Xamarin.Forms.Platform.Tizen.Native;
using EButton = ElmSharp.Button;
using EColor = ElmSharp.Color;
using NBox = Xamarin.Forms.Platform.Tizen.Native.Box;
using XForms = Xamarin.Forms.Forms;

[assembly: ExportRenderer(typeof(ContentButton), typeof(ContentButtonRenderer))]

namespace Tizen.Wearable.CircularUI.Forms.Renderer
{
    public class ContentButtonRenderer : ViewRenderer<ContentButton, NBox>
    {
        readonly int _defaultMinimumSize = 30;

        EvasObject _content;
        EButton _button;

        SmartEvent _pressed;
        SmartEvent _unpressed;
        SmartEvent _clicked;

        protected override void OnElementChanged(ElementChangedEventArgs<ContentButton> e)
        {
            if (Control == null)
            {
                var box = new NBox(XForms.NativeParent)
                {
                    MinimumWidth = _defaultMinimumSize,
                    MinimumHeight = _defaultMinimumSize
                };
                box.LayoutUpdated += OnLayout;

                _button = new EButton(XForms.NativeParent);
                _button.BackgroundColor = EColor.Transparent;
                _button.SetPartColor("effect", EColor.Transparent);
                _button.SetPartColor("effect_pressed", EColor.Transparent);
                _button.Show();

                _pressed = new SmartEvent(_button, "pressed");
                _unpressed = new SmartEvent(_button, "unpressed");
                _clicked = new SmartEvent(_button, "clicked");

                _pressed.On += OnPressed;
                _unpressed.On += OnReleased;
                _clicked.On += OnClicked;

                box.PackEnd(_button);

                SetNativeControl(box);
            }

            UpdateContent();
            base.OnElementChanged(e);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _pressed.On -= OnPressed;
                _unpressed.On -= OnReleased;
                _clicked.On -= OnClicked;
            }
            base.Dispose(disposing);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == ContentButton.ContentProperty.PropertyName)
            {
                UpdateContent();
            }
            base.OnElementPropertyChanged(sender, e);
        }

        protected override Xamarin.Forms.Size MinimumSize()
        {
            var size = base.MinimumSize();
            var height = XForms.ConvertToScaledPixel(Element.MinimumHeightRequest);
            var width = XForms.ConvertToScaledPixel(Element.MinimumWidthRequest);

            size.Width = (size.Width > width) ? size.Width : width;
            size.Height = (size.Height > height) ? size.Height : height;

            return size;
        }

        void OnPressed(object sender, EventArgs args)
        {
            Element?.SendPressed();
        }

        void OnReleased(object sender, EventArgs args)
        {
            Element?.SendReleased();
        }

        void OnClicked(object sender, EventArgs args)
        {
            Element?.SendClicked();
        }

        void OnLayout(object sender, LayoutEventArgs args)
        {
            if (Element.Content != null)
            {
                Element.Content.Layout(args.Geometry.ToDP());
            }

            _button.Geometry = args.Geometry;
            _button.RaiseTop();
        }

        void UpdateContent()
        {
            if (_content != null)
            {
                _content.Unrealize();
                _content = null;
            }

            if (Element.Content != null)
            {
                var renderer = Platform.GetOrCreateRenderer(Element.Content);
                (renderer as LayoutRenderer)?.RegisterOnLayoutUpdated();
                _content = renderer.NativeView;
                _content.Show();
                Control.PackEnd(_content);
            }
        }
    }
}
