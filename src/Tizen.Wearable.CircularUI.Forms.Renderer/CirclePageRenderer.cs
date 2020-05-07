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
using Xamarin.Forms.Platform.Tizen.Native.Watch;
using ERotaryEventManager = ElmSharp.Wearable.RotaryEventManager;
using XForms = Xamarin.Forms.Forms;

[assembly: ExportRenderer(typeof(CirclePage), typeof(Tizen.Wearable.CircularUI.Forms.Renderer.CirclePageRenderer))]

namespace Tizen.Wearable.CircularUI.Forms.Renderer
{
    public class CirclePageRenderer : VisualElementRenderer<CirclePage>
    {
        ObservableBox _mainLayout;

        ElmSharp.Button _actionButton;
        ActionButtonItem _actionButtonItem;
        ElmSharp.Layout _surfaceLayout;
        CircleSurface _circleSurface;
        EvasImage _bgImageObject;
        MoreOption _moreOption;

        Dictionary<ICircleSurfaceItem, ICircleWidget> _circleSurfaceItems;
        IRotaryFocusable _currentRotaryFocusObject;


        IList<EvasObject> Children => (_mainLayout as IContainable<EvasObject>)?.Children;

        public CirclePageRenderer()
        {
            RegisterPropertyHandler(Xamarin.Forms.Page.BackgroundImageSourceProperty, UpdateBackgroundImage);
            RegisterPropertyHandler(CirclePage.ActionButtonProperty, UpdateActionButton);
            RegisterPropertyHandler(CirclePage.RotaryFocusObjectProperty, UpdateRotaryFocusObject);
        }

        public CircleSurface CircleSurface => _circleSurface;

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

        protected override void OnElementChanged(ElementChangedEventArgs<CirclePage> e)
        {
            if (NativeView == null)
            {
                InitializeComponent();
            }
            if (e.NewElement != null)
            {
                e.NewElement.Appearing += OnPageAppearing;
                e.NewElement.Disappearing += OnPageDisappearing;
                e.NewElement.CircleSurface = CircleSurface;
                foreach (var item in e.NewElement.CircleSurfaceItems)
                {
                    AddCircleSurfaceItem(item);
                }
                if (e.NewElement.CircleSurfaceItems is INotifyCollectionChanged collectionChanged)
                {
                    collectionChanged.CollectionChanged += OnCircleSurfaceItemsChanged;
                }
            }
            base.OnElementChanged(e);
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
            if (Element.ToolbarItems is INotifyCollectionChanged toolbarCollectionChanged)
            {
                toolbarCollectionChanged.CollectionChanged += OnToolbarCollectionChanged;
            }
            if (Element.ToolbarItems.Count > 0)
            {
                Device.BeginInvokeOnMainThread(() => UpdateToolbarItems());
            }
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
                    if (Element.ToolbarItems is INotifyCollectionChanged toolbarCollectionChanged)
                    {
                        toolbarCollectionChanged.CollectionChanged -= OnToolbarCollectionChanged;
                    }
                }
            }
            base.Dispose(disposing);
        }

        protected override void UpdateLayout()
        {
            // empty on purpose
        }

        protected virtual void OnMoreOptionClosed()
        {
            ActivateRotaryWidget();
        }

        protected virtual void OnMoreOptionOpened()
        {
            DeactivateRotaryWidget();
        }

        protected virtual FormsMoreOptionItem CreateMoreOptionItem(Xamarin.Forms.ToolbarItem item)
        {
            var moreOptionItem = new FormsMoreOptionItem
            {
                MainText = item.Text,
                ToolbarItem = item
            };
            var icon = item.IconImageSource as FileImageSource;
            if (icon != null)
            {
                var img = new ElmSharp.Image(_moreOption);
                img.Load(ResourcePath.GetPath(icon));
                moreOptionItem.Icon = img;
            }
            if (item is CircleToolbarItem circleToolbarItem)
            {
                moreOptionItem.SubText = circleToolbarItem.SubText;
            }
            return moreOptionItem;
        }

        void InitializeComponent()
        {
            _mainLayout = new ObservableBox(XForms.NativeParent);
            _mainLayout.SetLayoutCallback(OnLayout);

            _surfaceLayout = new ElmSharp.Layout(XForms.NativeParent);
            _surfaceLayout.Show();
            Children.Add(_surfaceLayout);

            _circleSurface = new CircleSurface(_surfaceLayout);
            _circleSurfaceItems = new Dictionary<ICircleSurfaceItem, ICircleWidget>();

            SetNativeView(_mainLayout);
        }

        void InitializeActionButton()
        {
            _actionButton = new ElmSharp.Button(XForms.NativeParent)
            {
                Style = "bottom"
            };
            _actionButton.Clicked += OnActionButtonClicked;
            Children.Add(_actionButton);
        }

        void DeinitializeActionButton()
        {
            _actionButton.Clicked -= OnActionButtonClicked;
            Children.Remove(_actionButton);
            _actionButton.Unrealize();
            _actionButton = null;
        }

        void InitializeBgImageObject()
        {
            _bgImageObject = new EvasImage(XForms.NativeParent)
            {
                IsFilled = true
            };
            _bgImageObject.Show();
            Children.Add(_bgImageObject);
            _bgImageObject.Lower();
        }

        void DeinitializeBgImageObject()
        {
            Children.Remove(_bgImageObject);
            _bgImageObject.Unrealize();
            _bgImageObject = null;
        }

        void InitializeMoreOption()
        {
            _moreOption = new MoreOption(_mainLayout);
            _moreOption.Show();
            _moreOption.Clicked += OnMoreOptionItemClicked;
            _moreOption.Closed += (s, e) => OnMoreOptionClosed();
            _moreOption.Opened += (s, e) => OnMoreOptionOpened();
            Children.Add(_moreOption);
        }

        void DeinitializeMoreOption()
        {
            Children.Remove(_moreOption);
            _moreOption.Unrealize();
            _moreOption = null;
        }

        void UpdateBackgroundImage(bool initialize)
        {
            if (initialize && Element.BackgroundImageSource.IsNullOrEmpty())
                return;

            if (Element.BackgroundImageSource is FileImageSource fileImageSource)
            {
                if (_bgImageObject == null)
                {
                    InitializeBgImageObject();
                }
                _bgImageObject.File = ResourcePath.GetPath(fileImageSource);
            }
            else
            {
                if (_bgImageObject != null)
                {
                    DeinitializeBgImageObject();
                }
            }
        }

        void UpdateActionButton()
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
        }

        void UpdateToolbarItems()
        {
            if (Element.ToolbarItems.Count > 0)
            {
                if (_moreOption == null)
                {
                    InitializeMoreOption();
                }
                _moreOption.Items.Clear();
                foreach (var item in Element.ToolbarItems)
                {
                    _moreOption.Items.Add(CreateMoreOptionItem(item));
                }
            } 
            else
            {
                if (_moreOption != null)
                {
                    DeinitializeMoreOption();
                }
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

        void OnPageDisappearing(object sender, EventArgs e)
        {
            DeactivateRotaryWidget();
        }

        void OnPageAppearing(object sender, EventArgs e)
        {
            ActivateRotaryWidget();
        }

        void OnLayout()
        {
            var bound = NativeView.Geometry;
            Element.Layout(bound.ToDP());

            var content = (Element as IElementController).LogicalChildren.FirstOrDefault();
            if (content == null)
                return;

            var topmostView = Platform.GetRenderer(content)?.NativeView;

            if (_bgImageObject != null)
            {
                _bgImageObject.Geometry = bound;
                _bgImageObject.Lower();
            }

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

            if (_moreOption != null)
            {
                _moreOption.Geometry = bound;
                _moreOption.RaiseTop();
            }
        }

        void OnToolbarCollectionChanged(object sender, EventArgs eventArgs)
        {
            UpdateToolbarItems();
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

        void OnMoreOptionItemClicked(object sender, MoreOptionItemEventArgs e)
        {
            var formsMoreOptionItem = e.Item as FormsMoreOptionItem;
            if (formsMoreOptionItem != null)
            {
                ((IMenuItemController)formsMoreOptionItem.ToolbarItem)?.Activate();
            }
            _moreOption.IsOpened = false;
        }

        IRotaryActionWidget GetRotaryWidget(IRotaryFocusable focusable)
        {
            IRotaryActionWidget rotaryWidget = null;
            if (focusable is BindableObject consumer)
            {
                if (consumer is CircleSliderSurfaceItem circleSlider)
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

        ICircleWidget GetCircleWidget(ICircleSurfaceItem item)
        {
            ICircleWidget widget;
            if (_circleSurfaceItems.TryGetValue(item, out widget))
            {
                return widget;
            }
            return null;
        }
    }
}
