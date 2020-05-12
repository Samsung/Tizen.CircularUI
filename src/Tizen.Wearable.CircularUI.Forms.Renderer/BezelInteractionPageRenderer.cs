/*
 * Copyright (c) 2020 Samsung Electronics Co., Ltd All Rights Reserved
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

using ElmSharp.Wearable;
using System;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using ERotaryEventManager = ElmSharp.Wearable.RotaryEventManager;

[assembly: ExportRenderer(typeof(BezelInteractionPage), typeof(Tizen.Wearable.CircularUI.Forms.Renderer.BezelInteractionPageRenderer))]

namespace Tizen.Wearable.CircularUI.Forms.Renderer
{
    public class BezelInteractionPageRenderer : PageRenderer, IBezelInteractionController
    {
        IRotaryFocusable _currentRotaryFocusObject;

        new IBezelInteractionRouter Element => base.Element as IBezelInteractionRouter;

        public BezelInteractionPageRenderer()
        {
            RegisterPropertyHandler(BezelInteractionPage.RotaryFocusObjectProperty, UpdateRotaryFocusObject);
        }

        IRotaryFocusable IBezelInteractionController.RotaryFocusObject => _currentRotaryFocusObject;

        void IBezelInteractionController.Activate()
        {
            ActivateRotaryWidget();
        }

        void IBezelInteractionController.Deactivate()
        {
            DeactivateRotaryWidget();
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);
            e.NewElement.Appearing += OnPageAppearing;
            e.NewElement.Disappearing += OnPageDisappearing;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                base.Element.Appearing -= OnPageAppearing;
                base.Element.Disappearing -= OnPageDisappearing;
            }
            base.Dispose(disposing);
        }

        protected override void OnElementReady()
        {
            base.OnElementReady();
            // A Page created by with ContentTemplate of ShellContent, was appered before create a renderer
            // So need to call ActivateRotaryWidget() if page already appeared
            if (Element.Appeared)
            {
                ActivateRotaryWidget();
            }
        }

        //// TODO.Need to update
        //protected virtual void OnMoreOptionClosed()
        //{
        //    DeactivateRotaryWidget();
        //}

        //// TODO. Need to update 
        //protected virtual void OnMoreOptionOpened()
        //{
        //    ActivateRotaryWidget();
        //}

        void UpdateRotaryFocusObject(bool initialize)
        {
            if (initialize)
            {
                _currentRotaryFocusObject = Element.RotaryFocusObject;
            }
            else
            {
                DeactivateRotaryWidget();
                _currentRotaryFocusObject = Element.RotaryFocusObject;
                ActivateRotaryWidget();
            }
        }

        void ActivateRotaryWidget()
        {
            if (!Element.Appeared)
                return;

            if (_currentRotaryFocusObject is IRotaryEventReceiver)
            {
                ERotaryEventManager.Rotated += OnRotaryEventChanged;
            }
            else
            {
                GetRotaryWidget(_currentRotaryFocusObject)?.Activate();
            }
        }

        void DeactivateRotaryWidget()
        {
            if (_currentRotaryFocusObject is IRotaryEventReceiver)
            {
                ERotaryEventManager.Rotated -= OnRotaryEventChanged;
            }
            else if (_currentRotaryFocusObject is IRotaryFocusable)
            {
                GetRotaryWidget(_currentRotaryFocusObject)?.Deactivate();
            }
        }

        void OnPageDisappearing(object sender, EventArgs e)
        {
            DeactivateRotaryWidget();
        }

        void OnPageAppearing(object sender, EventArgs e)
        {
            ActivateRotaryWidget();
        }

        void OnRotaryEventChanged(ElmSharp.Wearable.RotaryEventArgs e)
        {
            if (_currentRotaryFocusObject is IRotaryEventReceiver receiver)
            {
                receiver.Rotate(new RotaryEventArgs { IsClockwise = e.IsClockwise });
            }
        }

        IRotaryActionWidget GetRotaryWidget(IRotaryFocusable focusable)
        {
            IRotaryActionWidget rotaryWidget = null;
            if (focusable is BindableObject consumer)
            {
                if (consumer is CircleSurfaceItem circleSlider)
                {
                    rotaryWidget = GetCircleWidget(circleSlider) as IRotaryActionWidget;
                }
                else
                {
                    rotaryWidget = Platform.GetRenderer(consumer)?.NativeView as IRotaryActionWidget;
                }
            }
            return rotaryWidget;
        }

        ICircleWidget GetCircleWidget(CircleSurfaceItem item)
        {
            if (item.Parent == null)
                return null;
            return (Platform.GetRenderer(item.Parent) as ICircleSurfaceItemRenderer)?.GetCircleWidget(item);
        }
    }
}
