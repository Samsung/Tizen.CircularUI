using ElmSharp;
using ElmSharp.Wearable;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Xamarin.Forms.CircularUI;
using Xamarin.Forms.Platform.Tizen;
using Xamarin.Forms.Platform.Tizen.Native;

namespace Xamarin.Forms.CircularUI.Tizen
{
    class CirclePageWidget : ElmSharp.Layout, IContainable<EvasObject>
    {
        ElmSharp.Layout _circleOptionLayout;
        ElmSharp.Layout _circleLayout;
        ElmSharp.Layout _actionButtonLayout;
        ElmSharp.Wearable.CircleSurface _surface;
        ElmSharp.Wearable.MoreOption _moreOption;

        Xamarin.Forms.Platform.Tizen.Native.Page _content;

        Dictionary<ToolbarItem, ElmSharp.Wearable.MoreOptionItem> _toolbarItemMap;
        Dictionary<ICircleSurfaceItem, ElmSharp.Wearable.ICircleWidget> _circleSurfaceItems;

        ElmSharp.Button _actionButton;

        CirclePage _page;

        public CirclePageWidget(EvasObject parent, CirclePage element) : base(parent)
        {
            SetTheme("layout", "application", "default");

            _actionButtonLayout = new ElmSharp.Layout(this);
            _actionButtonLayout.SetTheme("layout", "bottom_button", "default");
            _actionButtonLayout.Show();
            SetContent(_actionButtonLayout);

            _content = new Xamarin.Forms.Platform.Tizen.Native.Page(_actionButtonLayout);
            _actionButtonLayout.SetContent(_content);
            _content.Show();

            _circleOptionLayout = new ElmSharp.Layout(this);
            _circleOptionLayout.SetTheme("layout", "application", "default");
            _circleOptionLayout.Show();
            _circleOptionLayout.Geometry = new Rect(0, 0, 360, 360);

            _circleLayout = new ElmSharp.Layout(_circleOptionLayout);
            _circleLayout.Show();
            _circleOptionLayout.SetPartContent("elm.swallow.bg", _circleLayout);

            _surface = new ElmSharp.Wearable.CircleSurface(_circleLayout);

            Console.WriteLine($"Circle Surface hash is {_surface.GetHashCode()}");

            Console.WriteLine($"element is {element.ToString()}");

            Console.WriteLine($"element.ToolbarItems is {element.ToolbarItems}");

            var items = element.ToolbarItems as ObservableCollection<ToolbarItem>;
            items.CollectionChanged += OnToolbarItemsChanged;
            PrepareToolbarItems(items);

            var surfaceItems = element.CircleSurfaceItems as ObservableCollection<ICircleSurfaceItem>;
            surfaceItems.CollectionChanged += OnCircleSurfaceItemsChanged;
            PrepareCircleSurfaceItems(surfaceItems);

            _page = element;
        }

        public event EventHandler<LayoutEventArgs> LayoutUpdated
        {
            add => _content.LayoutUpdated += value;
            remove => _content.LayoutUpdated -= value;
        }

        public event EventHandler ToolbarOpened;
        public event EventHandler ToolbarClosed;

        IList<EvasObject> IContainable<EvasObject>.Children => _content.Children;

        public CircleSurface CircleSurface => _surface;

        public override ElmSharp.Color BackgroundColor
        {
            get => _content.Color;
            set => _content.Color = value;
        }

        public string File
        {
            get => _content.File;
            set => _content.File = value;
        }

        public void ShowActionButton(string text, string image = null, Action action = null)
        {
            if (_actionButton != null)
                HideActionButton();

            _actionButton = new ElmSharp.Button(_actionButtonLayout)
            {
                Text = text,
                Style = "bottom"
            };
            if (image != null)
            {
                var path = ResourcePath.GetPath(image);
                var buttonImage = new ElmSharp.Image(_actionButton);
                buttonImage.LoadAsync(path);
                buttonImage.Show();
                _actionButton.SetPartContent("elm.swallow.content", buttonImage);
            }
            if (action != null)
            {
                _actionButton.Clicked += (s, e) => action();
            }
            _actionButtonLayout.SetPartContent("elm.swallow.button", _actionButton);

            Console.WriteLine($"ShowActionButton");
        }

        public void HideActionButton()
        {
            if (_actionButton != null)
            {
                _actionButton.Unrealize();
                _actionButtonLayout.SetPartContent("elm.swallow.button", null);
                _actionButton = null;
            }
            Console.WriteLine($"HideActionButton");
        }

        protected override void OnUnrealize()
        {
            var toolbarItems = _page.ToolbarItems as ObservableCollection<ToolbarItem>;
            if (null != toolbarItems)
            {
                toolbarItems.CollectionChanged -= OnToolbarItemsChanged;
            }

            var circleSurfaceItems = _page.CircleSurfaceItems as ObservableCollection<ICircleSurfaceItem>;
            if (null != circleSurfaceItems)
            {
                circleSurfaceItems.CollectionChanged -= OnCircleSurfaceItemsChanged;
            }
            base.OnUnrealize();
        }

        void PrepareCircleSurfaceItems(IList items)
        {
            if (null == _circleSurfaceItems)
            {
                _circleSurfaceItems = new Dictionary<ICircleSurfaceItem, ICircleWidget>();
            }
            foreach (ICircleSurfaceItem item in items)
            {
                if (item is CircleProgressBarSurfaceItem)
                {
                    var widget = new CircleProgressBarSurfaceItemImplements(item as CircleProgressBarSurfaceItem, _circleLayout, _surface);
                    _circleSurfaceItems[item] = widget;
                }
                else if (item is CircleSliderSurfaceItem)
                {
                    var widget = new CircleSliderSurfaceItemImplements(item as CircleSliderSurfaceItem, _circleLayout, _surface);
                    _circleSurfaceItems[item] = widget;
                }
            }
        }

        void OnCircleSurfaceItemsChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            if (args.Action == NotifyCollectionChangedAction.Add ||
                args.Action == NotifyCollectionChangedAction.Replace)
            {
                PrepareCircleSurfaceItems(args.NewItems);
            }
            if (args.Action == NotifyCollectionChangedAction.Remove ||
                args.Action == NotifyCollectionChangedAction.Replace)
            {
                foreach (ICircleSurfaceItem item in args.OldItems)
                {
                    ElmSharp.Wearable.ICircleWidget widget;
                    if (_circleSurfaceItems.TryGetValue(item, out widget))
                    {
                        ElmSharp.EvasObject obj = widget as ElmSharp.EvasObject;
                        obj?.Unrealize();
                        _circleSurfaceItems.Remove(item);
                    }
                }
            }
        }

        public ICircleWidget GetCircleWidget(ICircleSurfaceItem item)
        {
            ElmSharp.Wearable.ICircleWidget widget;
            if (_circleSurfaceItems.TryGetValue(item, out widget))
            {
                return widget;
            }
            return null;
        }

        void PrepareToolbarItems(IList items)
        {
            if (items == null || items.Count <= 0) return;

            if (_moreOption == null)
            {
                initToolbar();
            }
            foreach (ToolbarItem item in items)
            {
                var moreOptionItem = new ActionMoreOptionItem();
                var icon = item.Icon;
                if (!string.IsNullOrEmpty(icon.File))
                {
                    var img = new ElmSharp.Image(_moreOption);
                    img.LoadAsync(ResourcePath.GetPath(icon.File));
                    moreOptionItem.Icon = img;
                }
                var text = item.Text;
                if (!string.IsNullOrEmpty(text))
                {
                    moreOptionItem.MainText = text;
                }
                if (item is CircleToolbarItem)
                {
                    var subText = ((CircleToolbarItem)item).SubText;
                    if (!string.IsNullOrEmpty(subText))
                    {
                        moreOptionItem.SubText = subText;
                    }
                }
                moreOptionItem.Action = () => item.Activate();
                _moreOption.Items.Add(moreOptionItem);
                _toolbarItemMap[item] = moreOptionItem;
            }
        }

        void OnToolbarItemsChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            if (args.Action == NotifyCollectionChangedAction.Add ||
                args.Action == NotifyCollectionChangedAction.Replace)
            {
                PrepareToolbarItems(args.NewItems);
            }
            if (args.Action == NotifyCollectionChangedAction.Remove ||
                args.Action == NotifyCollectionChangedAction.Replace)
            {
                foreach (ToolbarItem item in args.OldItems)
                {
                    ElmSharp.Wearable.MoreOptionItem moreOptionItem;
                    if (_toolbarItemMap.TryGetValue(item, out moreOptionItem))
                    {
                        _moreOption?.Items.Remove(moreOptionItem);
                        _toolbarItemMap.Remove(item);
                    }
                }
            }
        }

        void initToolbar()
        {
            _moreOption = new MoreOption(_circleOptionLayout);
            _circleOptionLayout.SetPartContent("elm.swallow.content", _moreOption);
            _moreOption.Show();
            _toolbarItemMap = new Dictionary<ToolbarItem, MoreOptionItem>();
            _moreOption.Clicked += MoreOptionClicked;
            _moreOption.Opened += (s, e) => ToolbarOpened?.Invoke(this, EventArgs.Empty);
            _moreOption.Closed += (s, e) => ToolbarClosed?.Invoke(this, EventArgs.Empty);
        }

        void MoreOptionClicked(object sender, MoreOptionItemEventArgs e)
        {
            var item = e.Item as ActionMoreOptionItem;
            if (item != null)
            {
                item.Action?.Invoke();
            }
            _moreOption.IsOpened = false;
        }

        class ActionMoreOptionItem : MoreOptionItem
        {
            public Action Action { get; set; }
        }
    }
}
