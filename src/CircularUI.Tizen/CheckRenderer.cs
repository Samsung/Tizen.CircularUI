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
using Xamarin.Forms;
using CircularUI;
using CircularUI.Tizen;
using Xamarin.Forms.Platform.Tizen;
using ECheck = ElmSharp.Check;
using TForms = Xamarin.Forms.Platform.Tizen.Forms;

[assembly: ExportRenderer(typeof(Check), typeof(CheckRenderer))]

namespace CircularUI.Tizen
{
    internal static class CheckStyle
    {
        public const string Default = "default";
        public const string Onoff = "on&off";
        public const string Small = "small";
    }

    public class CheckRenderer : SwitchRenderer
    {
        public CheckRenderer()
        {
            RegisterPropertyHandler(Check.DisplayStyleProperty, UpdateStyle);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Switch> e)
        {
            if (Control == null)
            {
                SetNativeControl(new ECheck(TForms.NativeParent)
                {
                    PropagateEvents = false,
                    Style = CheckStyle.Default
                });
                Control.StateChanged += OnStateChanged;
            }
            base.OnElementChanged(e);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (Control != null)
                {
                    Control.StateChanged -= OnStateChanged;
                }
            }
            base.Dispose(disposing);
        }

        void UpdateStyle()
        {
            var style = ((Check)Element).DisplayStyle;
            switch (style)
            {
                case CheckDisplayStyle.Onoff:
                    Control.Style = CheckStyle.Onoff;
                    break;
                case CheckDisplayStyle.Small:
                    Control.Style = CheckStyle.Small;
                    break;
                case CheckDisplayStyle.Default:
                default:
                    Control.Style = CheckStyle.Default;
                    break;
            }
            ((IVisualElementController)Element).NativeSizeChanged();
        }

        void OnStateChanged(object sender, EventArgs e)
        {
            Element.SetValue(Switch.IsToggledProperty, Control.IsChecked);
        }
    }
}
