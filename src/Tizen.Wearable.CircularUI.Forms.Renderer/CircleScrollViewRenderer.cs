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
using System.Threading.Tasks;
using ElmSharp;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using Xamarin.Forms.Platform.Tizen.Native;

[assembly: ExportRenderer(typeof(Tizen.Wearable.CircularUI.Forms.CircleScrollView), typeof(Tizen.Wearable.CircularUI.Forms.Renderer.CircleScrollViewRenderer))]

namespace Tizen.Wearable.CircularUI.Forms.Renderer
{
    class CircleScrollViewRenderer : ViewRenderer<CircleScrollView, ElmSharp.Wearable.CircleScroller>
    {
        Xamarin.Forms.Platform.Tizen.Native.Box _scrollCanvas;
        ElmSharp.SmartEvent _scrollAnimationStart, _scrollAnimationStop;
        bool _isAnimation;
        TaskCompletionSource<bool> _animationTaskComplateSource;

        public CircleScrollViewRenderer()
        {
            RegisterPropertyHandler("Content", OnContent);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<CircleScrollView> e)
        {
            if (Control == null)
            {
                var surface = this.GetSurface();
                SetNativeControl(new ElmSharp.Wearable.CircleScroller(Xamarin.Forms.Platform.Tizen.Forms.NativeParent, surface));
                InitControl();
                Control.Scrolled += OnScrolled;
                _scrollCanvas = new Xamarin.Forms.Platform.Tizen.Native.Box(Control);
                _scrollCanvas.LayoutUpdated += OnContentLayoutUpdated;
                Control.SetContent(_scrollCanvas);
            }
            if (e.OldElement != null)
            {
                (e.OldElement as IScrollViewController).ScrollToRequested -= OnScrollRequestedAsync;
            }
            if (e.NewElement != null)
            {
                (e.NewElement as IScrollViewController).ScrollToRequested += OnScrollRequestedAsync;
            }
            UpdateAll();

            base.OnElementChanged(e);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (ScrollView.OrientationProperty.PropertyName == e.PropertyName)
            {
                UpdateOrientation();
            }
            else if (ScrollView.ContentSizeProperty.PropertyName == e.PropertyName)
            {
                UpdateContentSize();
            }
            else if (ScrollView.VerticalScrollBarVisibilityProperty.PropertyName == e.PropertyName)
            {
                UpdateVerticalScrollBarVisibility();
            }
            else if (ScrollView.HorizontalScrollBarVisibilityProperty.PropertyName == e.PropertyName)
            {
                UpdateHorizontalScrollBarVisibility();
            }
            base.OnElementPropertyChanged(sender, e);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (null != Element)
                {
                    (Element as IScrollViewController).ScrollToRequested -= OnScrollRequestedAsync;
                }
                if (Control != null)
                {
                    Control.Scrolled -= OnScrolled;
                }
                if (_scrollCanvas != null)
                {
                    _scrollCanvas.LayoutUpdated -= OnContentLayoutUpdated;
                }
                _scrollAnimationStart?.Dispose();
                _scrollAnimationStop?.Dispose();
            }
            base.Dispose(disposing);
        }

        void UpdateAll()
        {
            UpdateOrientation();
        }

        void UpdateVerticalScrollBarVisibility()
        {
            Control.VerticalScrollBarVisiblePolicy = ScrollBarVisibilityToTizen(Element.VerticalScrollBarVisibility);
        }

        void UpdateHorizontalScrollBarVisibility()
        {
            var orientation = Element.Orientation;
            if (orientation == ScrollOrientation.Horizontal || orientation == ScrollOrientation.Both)
                Control.HorizontalScrollBarVisiblePolicy = ScrollBarVisibilityToTizen(Element.HorizontalScrollBarVisibility);
        }

        ScrollBarVisiblePolicy ScrollBarVisibilityToTizen(ScrollBarVisibility visibility)
        {
            switch (visibility)
            {
                case ScrollBarVisibility.Always:
                    return ScrollBarVisiblePolicy.Visible;
                case ScrollBarVisibility.Never:
                    return ScrollBarVisiblePolicy.Invisible;
                default:
                    return ScrollBarVisiblePolicy.Auto;
            }
        }

        async void OnScrollRequestedAsync(object sender, ScrollToRequestedEventArgs e)
        {
            var x = e.ScrollX;
            var y = e.ScrollY;
            if (e.Mode == ScrollToMode.Element)
            {
                var itemPosition = (Element as IScrollViewController).GetScrollPositionForElement(e.Element as VisualElement, e.Position);
                x = itemPosition.X;
                y = itemPosition.Y;
            }
            var region = new Xamarin.Forms.Rectangle(x, y, Element.Width, Element.Height).ToPixel();
            await ScrollToAsync(region, e.ShouldAnimate).ConfigureAwait(false);
            Element.SendScrollFinished();
        }

        void OnScrolled(object sender, EventArgs e)
        {
            var region = Control.CurrentRegion.ToDP();
            ((IScrollViewController)Element).SetScrolledPosition(region.X, region.Y);
        }

        void OnContent()
        {
            _scrollCanvas.UnPackAll();
            if (Element.Content != null)
            {
                _scrollCanvas.PackEnd(Platform.GetOrCreateRenderer(Element.Content).NativeView);
                UpdateContentSize();
            }
        }

        void UpdateContentSize()
        {
            _scrollCanvas.MinimumWidth = Xamarin.Forms.Platform.Tizen.Forms.ConvertToScaledPixel(Element.ContentSize.Width + Element.Padding.HorizontalThickness);
            _scrollCanvas.MinimumHeight = Xamarin.Forms.Platform.Tizen.Forms.ConvertToScaledPixel(Element.ContentSize.Height + Element.Padding.VerticalThickness);

            Device.BeginInvokeOnMainThread(() =>
            {
                if (Control != null)
                {
                    OnScrolled(Control, EventArgs.Empty);
                }
            });
        }

        void OnContentLayoutUpdated(object sender, LayoutEventArgs e)
        {
            UpdateContentSize();
        }

        void UpdateOrientation()
        {
            switch (Element.Orientation)
            {
                case ScrollOrientation.Horizontal:
                    Control.ScrollBlock = ElmSharp.ScrollBlock.Vertical;
                    Control.HorizontalScrollBarVisiblePolicy = ElmSharp.ScrollBarVisiblePolicy.Auto;
                    Control.VerticalScrollBarVisiblePolicy = ElmSharp.ScrollBarVisiblePolicy.Invisible;
                    break;
                case ScrollOrientation.Vertical:
                    Control.ScrollBlock = ElmSharp.ScrollBlock.Horizontal;
                    Control.HorizontalScrollBarVisiblePolicy = ElmSharp.ScrollBarVisiblePolicy.Invisible;
                    Control.VerticalScrollBarVisiblePolicy = ElmSharp.ScrollBarVisiblePolicy.Auto;
                    break;
                case ScrollOrientation.Both:
                    Control.ScrollBlock = ElmSharp.ScrollBlock.None;
                    Control.HorizontalScrollBarVisiblePolicy = ElmSharp.ScrollBarVisiblePolicy.Auto;
                    Control.VerticalScrollBarVisiblePolicy = ElmSharp.ScrollBarVisiblePolicy.Auto;
                    break;
                default:
                    throw new InvalidOperationException("Not supported orientation.");
            }
        }

        void InitControl()
        {
            _scrollAnimationStart = new ElmSharp.SmartEvent(Control, Control.RealHandle, "scroll,anim,start");
            _scrollAnimationStop = new ElmSharp.SmartEvent(Control, Control.RealHandle, "scroll,anim,stop");
            _scrollAnimationStart.On += (s, e) => _isAnimation = true;
            _scrollAnimationStop.On += (s, e) =>
            {
                if (_animationTaskComplateSource != null)
                {
                    _animationTaskComplateSource.TrySetResult(true);
                }
                _isAnimation = false;
            };
        }

        Task ScrollToAsync(Rect region, bool shouldAnimate)
        {
            CheckTaskCompletionSource();
            Control.ScrollTo(region, shouldAnimate);
            return shouldAnimate && _isAnimation ? _animationTaskComplateSource.Task : Task.CompletedTask;
        }

        void CheckTaskCompletionSource()
        {
            if (_animationTaskComplateSource != null)
            {
                if (_animationTaskComplateSource.Task.Status == TaskStatus.Running)
                {
                    _animationTaskComplateSource.TrySetCanceled();
                }
            }
            _animationTaskComplateSource = new TaskCompletionSource<bool>();
        }
    }
}
