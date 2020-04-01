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
using Tizen.Wearable.CircularUI.Forms;
using Tizen.Wearable.CircularUI.Forms.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using Xamarin.Forms.Platform.Tizen.Native;
using XForms = Xamarin.Forms.Forms;
using XFLayout = Xamarin.Forms.Layout;
using EButton = ElmSharp.Button;
using EColor = ElmSharp.Color;

[assembly: ExportRenderer(typeof(ContentButton), typeof(ContentButtonRenderer))]

namespace Tizen.Wearable.CircularUI.Forms.Renderer
{
    public class ContentButtonRenderer : LayoutRenderer
    {
        EButton _button;

        ContentButton Button => Element as ContentButton;

        protected override void OnElementChanged(ElementChangedEventArgs<XFLayout> e)
        {
            base.OnElementChanged(e);
            Initialize();
        }

        void Initialize()
        {
            if (_button == null)
            {
                _button = new EButton(XForms.NativeParent);
                _button.BackgroundColor = EColor.Transparent;
                _button.SetPartColor("effect", EColor.Transparent);
                _button.SetPartColor("effect_pressed", EColor.Transparent);
                _button.Show();

                _button.Pressed += OnPressed;
                _button.Released += OnReleased;
                _button.Clicked += OnClicked;

                Control.PackEnd(_button);

                Control.LayoutUpdated += OnLayoutUpdated;
            }
        }

        void OnLayoutUpdated(object sender, LayoutEventArgs e)
        {
            _button.Geometry = e.Geometry;
            _button.RaiseTop();
        }

        void OnPressed(object sender, EventArgs args)
        {
            Button?.SendPressed();
        }

        void OnReleased(object sender, EventArgs args)
        {
            Button?.SendReleased();
        }

        void OnClicked(object sender, EventArgs args)
        {
            Button?.SendClicked();
        }
    }
}
