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
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using Xamarin.Forms.Platform.Tizen.Native;
using ERotaryEventManager = ElmSharp.Wearable.RotaryEventManager;
using NPage = Xamarin.Forms.Platform.Tizen.Native.Page;
using XForms = Xamarin.Forms.Forms;

[assembly: ExportRenderer(typeof(CirclePage), typeof(Tizen.Wearable.CircularUI.Forms.Renderer.CirclePageRenderer))]

namespace Tizen.Wearable.CircularUI.Forms.Renderer
{
    public class CirclePageRenderer : PageRenderer, IBezelInteractionController
    {
        ElmSharp.Button _actionButton;
        ActionButtonItem _actionButtonItem;
        ElmSharp.Layout _surfaceLayout;
        CircleSurface _circleSurface;
        Dictionary<ICircleSurfaceItem, ICircleWidget> _circleSurfaceItems;
        IRotaryFocusable _currentRotaryFocusObject;
        bool _isInitialized;

        NPage Control => (NativeView as NPage);

        new CirclePage Element => base.Element as CirclePage;

        public CirclePageRenderer()
        {
            RegisterPropertyHandler(CirclePage.ActionButtonProperty, UpdateActionButton);
            RegisterPropertyHandler(CirclePage.RotaryFocusObjectProperty, UpdateRotaryFocusObject);
        }

        public CircleSurface CircleSurface => _circleSurface;

        IRotaryFocusable IBezelInteractionController.RotaryFocusObject => _currentRotaryFocusObject;

        void IBezelInteractionController.Activate()
        {
            ActivateRotaryWidget();
        }

        void IBezelInteractionController.Deactivate()
        {
            DeactivateRotaryWidget();
        }

        public void UpdateRotaryFocusObject(bool initialize)
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

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Page> e)
        {
            // It will create NativeView and it will be Native.Page
            base.OnElementChanged(e);
            if (!_isInitialized)
            {
                _isInitialized = true;
                InitializeComponent();
            }

            if (e.NewElement is CirclePage newElement)
            {
                newElement.Appearing += OnPageAppearing;
                newElement.Disappearing += OnPageDisappearing;
                newElement.CircleSurface = CircleSurface;
                foreach (var item in newElement.CircleSurfaceItems)
                {
                    AddCircleSurfaceItem(item);
                }
                if (newElement.CircleSurfaceItems is INotifyCollectionChanged collectionChanged)
                {
                    collectionChanged.CollectionChanged += OnCircleSurfaceItemsChanged;
                }
            }
        }

        protected override void OnElementReady()
        {
            base.OnElementReady();
            // A Page created by with ContentTemplate of ShellContent, was appered before create a renderer
            // So need to call OnPageAppearing if page already appeared
            if (Element.Appeared)
            {
                OnPageAppearing(Element, EventArgs.Empty);
            }
        }


        protected override void OnMoreOptionClosed()
        {
            DeactivateRotaryWidget();
        }


        protected override void OnMoreOptionOpened()
        {
            ActivateRotaryWidget();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (Element != null)
                {
                    Element.Appearing -= OnPageAppearing;
                    Element.Disappearing -= OnPageDisappearing;
                    Element.CircleSurface = null;
                    if (_actionButtonItem != null)
                    {
                        _actionButtonItem.PropertyChanged -= OnActionButtonItemChanged;
                    }
                    if (Element.CircleSurfaceItems is INotifyCollectionChanged collectionChanged)
                    {
                        collectionChanged.CollectionChanged -= OnCircleSurfaceItemsChanged;
                    }
                }
            }
            base.Dispose(disposing);
        }

        void InitializeComponent()
        {
            Control.LayoutUpdated += OnLayoutUpdated;
            _surfaceLayout = new ElmSharp.Layout(XForms.NativeParent);
            _circleSurface = new CircleSurface(_surfaceLayout);
            Control.Children.Add(_surfaceLayout);
            _surfaceLayout.Show();
            _circleSurfaceItems = new Dictionary<ICircleSurfaceItem, ICircleWidget>();
        }

        void InitializeActionButton()
        {
            _actionButton = new ElmSharp.Button(XForms.NativeParent)
            {
                Style = "bottom"
            };
            _actionButton.Clicked += OnActionButtonClicked;
            Control.Children.Add(_actionButton);
        }

        void DeinitializeActionButton()
        {
            _actionButton.Clicked -= OnActionButtonClicked;
            Control.Children.Remove(_actionButton);
            _actionButton.Unrealize();
            _actionButton = null;
        }

        void UpdateActionButton(bool init)
        {
            if (_actionButtonItem != null)
            {
                _actionButtonItem.PropertyChanged -= OnActionButtonItemChanged;
                _actionButtonItem = null;
            }

            if (Element.ActionButton != null)
            {
                if (_actionButton == null)
                {
                    InitializeActionButton();
                }

                UpdateActionButtonVisible(Element.ActionButton.IsVisible);
                UpdateActionButtonText(Element.ActionButton.Text);
                UpdateActionButtonIcon(Element.ActionButton.IconImageSource);
                _actionButton.IsEnabled = Element.ActionButton.IsEnable;
                _actionButton.BackgroundColor = Element.ActionButton.BackgroundColor.ToNative();

                _actionButtonItem = Element.ActionButton;
                _actionButtonItem.PropertyChanged += OnActionButtonItemChanged;
            }
            else
            {
                if (_actionButton != null)
                {
                    DeinitializeActionButton();
                }
            }
            if (!init)
            {
                Device.BeginInvokeOnMainThread(() => OnLayout());
            }
        }

        void UpdateActionButtonVisible(bool visible)
        {
            if (_actionButton == null)
            {
                return;
            }
            if (visible) _actionButton.Show();
            else _actionButton.Hide();
        }

        void UpdateActionButtonText(string text)
        {
            _actionButton.Text = text?.Replace("&", "&amp;")
           .Replace("<", "&lt;")
           .Replace(">", "&gt;")
           .Replace(Environment.NewLine, "<br>") ?? string.Empty;
        }

        void UpdateActionButtonIcon(ImageSource source)
        {
            if (source is FileImageSource filesource)
            {
                var path = ResourcePath.GetPath(filesource);
                var buttonImage = new ElmSharp.Image(_actionButton);
                buttonImage.Load(path);
                buttonImage.Show();
                _actionButton.SetPartContent("elm.swallow.content", buttonImage);
            }
            else
            {
                _actionButton.SetPartContent("elm.swallow.content", null);
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

        void OnLayoutUpdated(object sender, LayoutEventArgs e)
        {
            OnLayout();
        }

        void OnLayout()
        {
            // Page layout was updated on base class
            // It only update ActionButton

            var content = (Element as IElementController).LogicalChildren.FirstOrDefault();
            if (content == null)
                return;

            var topmostView = Platform.GetRenderer(content)?.NativeView;

            var bound = Control.Geometry;

            if (_actionButton != null)
            {
                var btnRect = _actionButton.Geometry;
                var btnW = Math.Max(_actionButton.MinimumWidth, btnRect.Width);
                var btnH = Math.Max(_actionButton.MinimumHeight, btnRect.Height);
                var btnX = bound.X + (bound.Width - btnW) / 2;
                var btnY = bound.Y + bound.Height - btnH;
                _actionButton.Geometry = new Rect(btnX, btnY, btnW, btnH);
                _actionButton.StackAbove(topmostView);
                topmostView = _actionButton;
            }

            _surfaceLayout.Geometry = bound;
            _surfaceLayout.StackAbove(topmostView);
        }

        void OnActionButtonClicked(object sender, EventArgs e)
        {
            if (Element.ActionButton != null)
            {
                ((IMenuItemController)Element.ActionButton).Activate();
            }
        }

        void OnActionButtonItemChanged(object sender, PropertyChangedEventArgs e)
        {
            if (_actionButton == null)
            {
                return;
            }
            if (e.PropertyName == MenuItem.TextProperty.PropertyName)
            {
                UpdateActionButtonText(Element.ActionButton.Text);
            }
            else if (e.PropertyName == ActionButtonItem.IsEnableProperty.PropertyName)
            {
                _actionButton.IsEnabled = Element.ActionButton.IsEnable;
            }
            else if (e.PropertyName == ActionButtonItem.IsVisibleProperty.PropertyName)
            {
                UpdateActionButtonVisible(Element.ActionButton.IsVisible);
            }
            else if (e.PropertyName == ActionButtonItem.BackgroundColorProperty.PropertyName)
            {
                _actionButton.BackgroundColor = Element.ActionButton.BackgroundColor.ToNative();
            }
            else if (e.PropertyName == MenuItem.IconImageSourceProperty.PropertyName)
            {
                UpdateActionButtonIcon(Element.ActionButton.IconImageSource);
            }
        }

        void OnCircleSurfaceItemsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add ||
                e.Action == NotifyCollectionChangedAction.Replace)
            {
                foreach (ICircleSurfaceItem item in e.NewItems)
                    AddCircleSurfaceItem(item);
            }
            if (e.Action == NotifyCollectionChangedAction.Remove ||
                e.Action == NotifyCollectionChangedAction.Replace)
            {
                foreach (ICircleSurfaceItem item in e.OldItems)
                    RemoveCircleSurfaceItem(item);
            }
        }

        void OnRotaryEventChanged(ElmSharp.Wearable.RotaryEventArgs e)
        {
            if (_currentRotaryFocusObject is IRotaryEventReceiver receiver)
            {
                receiver.Rotate(new RotaryEventArgs { IsClockwise = e.IsClockwise });
            }
        }

        void AddCircleSurfaceItem(ICircleSurfaceItem item)
        {
            if (item is CircleProgressBarSurfaceItem progressbar)
            {
                _circleSurfaceItems[item] = new CircleProgressBarSurfaceItemImplements(progressbar, _surfaceLayout, CircleSurface);
            }
            else if (item is CircleSliderSurfaceItem slider)
            {
                _circleSurfaceItems[item] = new CircleSliderSurfaceItemImplements(slider, _surfaceLayout, CircleSurface);
            }
        }

        void RemoveCircleSurfaceItem(ICircleSurfaceItem item)
        {
            if (_circleSurfaceItems.TryGetValue(item, out var widget))
            {
                (widget as EvasObject)?.Unrealize();
                _circleSurfaceItems.Remove(item);
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

        IRotaryActionWidget GetRotaryWidget(IRotaryFocusable focusable)
        {
            IRotaryActionWidget rotaryWidget = null;
            if (focusable is BindableObject consumer)
            {
                if (consumer is ICircleSurfaceItem circleSurfaceItem)
                {
                    rotaryWidget = GetCircleWidget(circleSurfaceItem) as IRotaryActionWidget;
                }
                else
                {
                    rotaryWidget = Platform.GetRenderer(consumer)?.NativeView as IRotaryActionWidget;
                }
            }
            return rotaryWidget;
        }

        ICircleWidget GetCircleWidget(ICircleSurfaceItem item)
        {
            if (_circleSurfaceItems.TryGetValue(item, out ICircleWidget widget))
            {
                return widget;
            }
            return null;
        }
    }
}
