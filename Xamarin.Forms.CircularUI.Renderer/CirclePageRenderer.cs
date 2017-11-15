using ElmSharp.Wearable;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Xamarin.Forms.CircularUI;
using Xamarin.Forms.Platform.Tizen;
using System.ComponentModel;

[assembly: ExportRenderer(typeof(CirclePage), typeof(Xamarin.Forms.CircularUI.Renderer.CirclePageRenderer))]

namespace Xamarin.Forms.CircularUI.Renderer
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
                _widget.Show();
                SetNativeControl(_widget);
            }
            base.OnElementChanged(args);
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

        protected override void Dispose(bool disposing)
        {
            GetRotaryWidget(_currentRotaryFocusObject)?.Deactivate();
            _currentRotaryFocusObject = null;
            base.Dispose(disposing);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == VisualElement.IsVisibleProperty.PropertyName)
            {
                if (Element.IsVisible)
                {
                    GetRotaryWidget(_currentRotaryFocusObject)?.Activate();
                }
                else
                {
                    GetRotaryWidget(_currentRotaryFocusObject)?.Deactivate();
                }
            }
        }

        protected override void OnElementReady()
        {
            base.OnElementReady();
            GetRotaryWidget(_currentRotaryFocusObject)?.Activate();
        }

        void OnToolbarClosed(object sender, EventArgs e)
        {
            GetRotaryWidget(_currentRotaryFocusObject)?.Activate();
        }

        void OnToolbarOpened(object sender, EventArgs e)
        {
            GetRotaryWidget(_currentRotaryFocusObject)?.Deactivate();
        }

        IRotaryActionWidget GetRotaryWidget(IRotaryFocusable focusable)
        {
            var consumer = focusable as BindableObject;
            if (consumer != null)
            {
                var consumerRenderer = Xamarin.Forms.Platform.Tizen.Platform.GetRenderer(consumer);
                if (consumerRenderer != null)
                {
                    var nativeView = consumerRenderer.NativeView;
                    var rotaryWidget = nativeView as ElmSharp.Wearable.IRotaryActionWidget;
                    if (rotaryWidget != null)
                    {
                        return rotaryWidget;
                    }
                }
            }
            return null;
        }

        void UpdateRotaryFocusObject(bool initialize)
        {
            if (!initialize)
                GetRotaryWidget(_currentRotaryFocusObject)?.Deactivate();
            _currentRotaryFocusObject = Element.RotaryFocusObject;
            if (!initialize)
                GetRotaryWidget(_currentRotaryFocusObject)?.Activate();
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
