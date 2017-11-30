using ElmSharp.Wearable;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Xamarin.Forms.CircularUI;
using Xamarin.Forms.Platform.Tizen;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(CirclePage), typeof(Xamarin.Forms.CircularUI.Tizen.CirclePageRenderer))]

namespace Xamarin.Forms.CircularUI.Tizen
{
    public class CirclePageRenderer : VisualElementRenderer<CirclePage>
    {
        CirclePageWidget _widget;
        IRotaryFocusable _currentRotaryFocusObject;

        public CirclePageRenderer()
        {
            RegisterPropertyHandler(Page.BackgroundImageProperty, UpdateBackgroundImage);
            RegisterPropertyHandler(CirclePage.ActionButtonProperty, UpdateActionButton);
            RegisterPropertyHandler(CirclePage.RotaryFocusObjectProperty, UpdateRotaryFocusObject);
        }

        public ElmSharp.Wearable.CircleSurface CircleSurface => _widget.CircleSurface;

        protected override void OnElementChanged(ElementChangedEventArgs<CirclePage> args)
        {
            if (null == _widget)
            {
                _widget = new CirclePageWidget(Xamarin.Forms.Platform.Tizen.Forms.Context.MainWindow, Element);
                _widget.LayoutUpdated += OnLayoutUpdated;
                _widget.ToolbarOpened += OnToolbarOpened;
                _widget.ToolbarClosed += OnToolbarClosed;
                SetNativeView(_widget);
            }
            if (args.NewElement != null)
            {
                args.NewElement.Appearing += OnCirclePageAppearing;
                args.NewElement.Disappearing += OnCirclePageDisappearing;
            }
            if (args.OldElement != null)
            {
                args.OldElement.Appearing -= OnCirclePageAppearing;
                args.OldElement.Disappearing -= OnCirclePageDisappearing;
            }
            base.OnElementChanged(args);
        }

        protected void OnCirclePageAppearing(object sender, EventArgs e)
        {
            ActivateRotaryWidget();
        }

        protected void OnCirclePageDisappearing(object sender, EventArgs e)
        {
            DeactivateRotaryWidget();
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
        IRotaryActionWidget GetRotaryWidget(IRotaryFocusable focusable)
        {
            var consumer = focusable as BindableObject;
            IRotaryActionWidget rotaryWidget = null;
            if (consumer != null)
            {
                if (consumer is CircleSliderSurfaceItem)
                {
                    ICircleSurfaceItem item = consumer as ICircleSurfaceItem;
                    rotaryWidget = _widget.GetCircleWidget(item) as IRotaryActionWidget;
                }
                else
                {
                    var consumerRenderer = Platform.Tizen.Platform.GetRenderer(consumer);
                    rotaryWidget = consumerRenderer?.NativeView as IRotaryActionWidget;
                }
            }
            return rotaryWidget;
        }

        void ActivateRotaryWidget()
        {
            if (_currentRotaryFocusObject is IRotaryEventReceiver)
            {
                RotaryEventManager.Rotated += OnRotaryEventChanged;
            }
            else if (_currentRotaryFocusObject is IRotaryFocusable)
            {
                GetRotaryWidget(_currentRotaryFocusObject)?.Activate();
            }
        }

        void DeactivateRotaryWidget()
        {
            if (_currentRotaryFocusObject is IRotaryEventReceiver)
            {
                RotaryEventManager.Rotated -= OnRotaryEventChanged;
            }
            else if (_currentRotaryFocusObject is IRotaryFocusable)
            {
                GetRotaryWidget(_currentRotaryFocusObject)?.Deactivate();
            }
        }

        void OnRotaryEventChanged(ElmSharp.Wearable.RotaryEventArgs args)
        {
            if (_currentRotaryFocusObject is IRotaryEventReceiver)
            {
                var receiver = _currentRotaryFocusObject as IRotaryEventReceiver;
                receiver.Rotate(new RotaryEventArgs { IsClockwise = args.IsClockwise });
            }
        }

        void OnToolbarClosed(object sender, EventArgs e)
        {
            ActivateRotaryWidget();
        }

        void OnToolbarOpened(object sender, EventArgs e)
        {
            DeactivateRotaryWidget();
        }

        void UpdateRotaryFocusObject(bool initialize)
        {
            if (!initialize)
            {
                DeactivateRotaryWidget();
            }
            _currentRotaryFocusObject = Element.RotaryFocusObject;
            if (!initialize)
            {
                ActivateRotaryWidget();
            }
        }

        void OnLayoutUpdated(object sender, Platform.Tizen.Native.LayoutEventArgs args)
        {
            DoLayout(args);
        }

        void UpdateBackgroundImage(bool initialize)
        {
            if (initialize && string.IsNullOrWhiteSpace(Element.BackgroundImage))
                return;
            if (string.IsNullOrWhiteSpace(Element.BackgroundImage))
            {
                _widget.File = null;
            }
            else
            {
                _widget.File = ResourcePath.GetPath(Element.BackgroundImage);
            }
        }

        void UpdateActionButton(bool initialize)
        {
            if (initialize && Element.ActionButton == null) return;

            if (Element.ActionButton != null)
            {
                var item = Element.ActionButton;
                _widget.ShowActionButton(item.Text, item.Icon, () => item.Activate());
            }
            else
            {
                _widget.HideActionButton();
            }
        }
    }
}
