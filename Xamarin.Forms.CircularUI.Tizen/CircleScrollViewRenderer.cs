using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElmSharp;
using Xamarin.Forms.Platform.Tizen;
using Xamarin.Forms.Platform.Tizen.Native;

[assembly: ExportRenderer(typeof(Xamarin.Forms.CircularUI.CircleScrollView), typeof(Xamarin.Forms.CircularUI.Tizen.CircleScrollViewRenderer))]

namespace Xamarin.Forms.CircularUI.Tizen
{
    class CircleScrollViewRenderer : ViewRenderer<CircleScrollView, ElmSharp.Wearable.CircleScroller>
    {
        ElmSharp.EvasObject _content;
        ElmSharp.SmartEvent _scrollAnimationStart, _scrollAnimationStop;
        bool _isAnimation = false;
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
                SetNativeControl(new ElmSharp.Wearable.CircleScroller(Platform.Tizen.Forms.NativeParent, surface));
                InitControl();
                Control.Scrolled += OnScrolled;
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
            }
            base.Dispose(disposing);
        }

        void UpdateAll()
        {
            UpdateOrientation();
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
            var region = new Rectangle(x, y, Element.Width, Element.Height).ToPixel();
            await ScrollToAsync(region, e.ShouldAnimate);
            Element.SendScrollFinished();
        }

        void OnScrolled(object sender, EventArgs e)
        {
            var region = Control.CurrentRegion.ToDP();
            ((IScrollViewController)Element).SetScrolledPosition(region.X, region.Y);
        }

        void OnContent(bool obj)
        {
            if (_content != null)
            {
                if (_content is Platform.Tizen.Native.Box contentBox)
                {
                    contentBox.LayoutUpdated -= OnContentLayoutUpdated;
                }
                Control.SetContent(null, true);
                _content.Unrealize();
                _content = null;
            }
            if (Element.Content != null)
            {
                _content = Platform.Tizen.Platform.GetOrCreateRenderer(Element.Content).NativeView;
                if (_content is Platform.Tizen.Native.Box contentBox)
                {
                    contentBox.LayoutUpdated += OnContentLayoutUpdated;
                }
                Control.SetContent(_content, true);
                UpdateContentSize();
            }
        }

        void UpdateContentSize()
        {
            if (_content == null) return;

            _content.MinimumWidth = Platform.Tizen.Forms.ConvertToScaledPixel(Element.ContentSize.Width);
            _content.MinimumHeight = Platform.Tizen.Forms.ConvertToScaledPixel(Element.ContentSize.Height);

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
