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
using ElmSharp.Wearable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using XForms = Xamarin.Forms.Forms;
using ERotaryEventManager = ElmSharp.Wearable.RotaryEventManager;

[assembly: ExportEffect(typeof(Tizen.Wearable.CircularUI.Forms.Renderer.TizenCircleSurfaceEffect), "CircleSurfaceEffect")]
namespace Tizen.Wearable.CircularUI.Forms.Renderer
{
    public class TizenCircleSurfaceEffect : PlatformEffect
    {
        ElmSharp.Layout _surfaceLayout;
        ElmSharp.Wearable.CircleSurface _surface;

        EvasObjectEvent _showEvent;
        EvasObjectEvent _hideEvent;
        EvasObjectEvent _restackEvent;
        EvasObjectEvent _moveEvent;
        EvasObjectEvent _resizeEvent;

        IRotaryEventReceiver _rotaryReceiver;

        protected override void OnAttached()
        {
            var rect = Control.Geometry;

            _surfaceLayout = new ElmSharp.Layout(XForms.NativeParent);
            _surfaceLayout.Show();
            _surface = new ElmSharp.Wearable.CircleSurface(_surfaceLayout);
            _surfaceLayout.Geometry = rect;
            _surfaceLayout.StackAbove(Control);

            CircleSurfaceEffectBehavior.SetSurface(Element, _surface);

            _showEvent = new EvasObjectEvent(Control, EvasObjectCallbackType.Show);
            _hideEvent = new EvasObjectEvent(Control, EvasObjectCallbackType.Hide);
            _restackEvent = new EvasObjectEvent(Control, EvasObjectCallbackType.Restack);
            _moveEvent = new EvasObjectEvent(Control, EvasObjectCallbackType.Move);
            _resizeEvent = new EvasObjectEvent(Control, EvasObjectCallbackType.Resize);

            _showEvent.On += ControlShowed;
            _hideEvent.On += ControlHided;
            _restackEvent.On += ControlRestacked;
            _moveEvent.On += ControlChanged;
            _resizeEvent.On += ControlChanged;

            Element.PropertyChanging += ElementPropertyChanging;
            Element.PropertyChanged += ElementPropertyChanged;

            if (Element is Page)
            {
                (Element as Page).Appearing += (s, e) =>
                {
                    var obj = CircleSurfaceEffectBehavior.GetRotaryFocusObject(Element);
                    ActivateRotaryFocusable(obj);
                };
            }
            else
            {
                EcoreMainloop.Post(() =>
                {
                    var obj = CircleSurfaceEffectBehavior.GetRotaryFocusObject(Element);
                    ActivateRotaryFocusable(obj);
                });
            }
        }

        protected override void OnDetached()
        {
            var obj = CircleSurfaceEffectBehavior.GetRotaryFocusObject(Element);
            DeativateRotaryFocusable(obj);

            _surface.Delete();
            _surface = null;
            _surfaceLayout.Unrealize();
            _surfaceLayout = null;
            CircleSurfaceEffectBehavior.SetSurface(Element, null);

            _showEvent.Dispose();
            _hideEvent.Dispose();
            _restackEvent.Dispose();
            _moveEvent.Dispose();
            _resizeEvent.Dispose();
        }

        void ElementPropertyChanging(object sender, PropertyChangingEventArgs e)
        {
            if (e.PropertyName == CircleSurfaceEffectBehavior.RotaryFocusObjectProperty.PropertyName)
            {
                var obj = CircleSurfaceEffectBehavior.GetRotaryFocusObject(Element);
                DeativateRotaryFocusable(obj);
            }
        }

        void ElementPropertyChanged(object sender, global::System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == CircleSurfaceEffectBehavior.RotaryFocusObjectProperty.PropertyName)
            {
                var obj = CircleSurfaceEffectBehavior.GetRotaryFocusObject(Element);
                ActivateRotaryFocusable(obj);
            }
        }

        void OnRotaryEventChanged(ElmSharp.Wearable.RotaryEventArgs e)
        {
            _rotaryReceiver?.Rotate(new RotaryEventArgs { IsClockwise = e.IsClockwise });
        }

        void ControlShowed(object sender, EventArgs e)
        {
            _surfaceLayout.Show();
        }

        void ControlHided(object sender, EventArgs e)
        {
            _surfaceLayout.Hide();
        }

        void ControlRestacked(object sender, EventArgs e)
        {
            _surfaceLayout.StackAbove(Control);
        }

        void ControlChanged(object sender, EventArgs e)
        {
            _surfaceLayout.Geometry = Control.Geometry;
        }

        void ActivateRotaryFocusable(IRotaryFocusable focusable)
        {
            if (focusable is IRotaryEventReceiver)
            {
                _rotaryReceiver = focusable as IRotaryEventReceiver;
                ERotaryEventManager.Rotated += OnRotaryEventChanged;
            }
            else if (focusable is IRotaryFocusable)
            {
                var consumer = focusable as BindableObject;
                if (consumer != null)
                {
                    var renderer = Xamarin.Forms.Platform.Tizen.Platform.GetRenderer(consumer);
                    (renderer?.NativeView as IRotaryActionWidget)?.Activate();
                }
            }
        }

        void DeativateRotaryFocusable(IRotaryFocusable focusable)
        {
            if (focusable is IRotaryEventReceiver)
            {
                _rotaryReceiver = null;
                ERotaryEventManager.Rotated -= OnRotaryEventChanged;
            }
            else if (focusable is IRotaryFocusable)
            {
                var consumer = focusable as BindableObject;
                if (consumer != null)
                {
                    var renderer = Xamarin.Forms.Platform.Tizen.Platform.GetRenderer(consumer);
                    (renderer?.NativeView as IRotaryActionWidget)?.Deactivate();
                }
            }
        }
    }
}
