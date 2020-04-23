using ElmSharp;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using Xamarin.Forms;
using XForms = Xamarin.Forms.Forms;

namespace Tizen.Wearable.CircularUI.Forms.Renderer
{
    public class ShellSectionItemsRenderer : IShellItemRenderer
    {
        const int ItemMaxCount = 20;
        const int OddMiddleItem = 10;
        const int EvenMiddleItem = 11;

        Box _mainLayout;
        Index _indexIndicator;
        Scroller _scroller;
        Box _innerContainer;
        List<ItemHolder> _items = new List<ItemHolder>();

        int _currentIndex = -1;
        Rect _lastLayoutBound;
        int _updateByCode;


        public ShellSectionItemsRenderer(ShellSection shellSection)
        {
            ShellSection = shellSection;
            ShellSection.PropertyChanged += OnSectionPropertyChanged;
            (ShellSection.Items as INotifyCollectionChanged).CollectionChanged += OnItemsChanged;
            InitializeComponent();
            UpdateItems();
        }

        public ShellSection ShellSection { get; protected set; }

        public BaseShellItem Item => ShellSection;

        public EvasObject NativeView => _mainLayout;

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _mainLayout?.Unrealize();
                (ShellSection.Items as INotifyCollectionChanged).CollectionChanged -= OnItemsChanged;
                ShellSection.PropertyChanged -= OnSectionPropertyChanged;
            }
        }

        void InitializeComponent()
        {   
            _mainLayout = new Box(XForms.NativeParent)
            {
                AlignmentX = -1,
                AlignmentY = -1,
                WeightX = 1,
                WeightY = 1,
            };
            _mainLayout.Show();
            _mainLayout.SetLayoutCallback(OnLayout);

            _indexIndicator = new Index(_mainLayout)
            {
                IsHorizontal = true,
                AutoHide = false,
                Style = IndexStyle.Circle,
            };
            _indexIndicator.Show();

            _scroller = new Scroller(_mainLayout);
            _scroller.PageScrolled += OnPageScrolled;
            _scroller.DragStart += OnDragStarted;

            // Disables the visibility of the scrollbar in both directions:
            _scroller.HorizontalScrollBarVisiblePolicy = ScrollBarVisiblePolicy.Invisible;
            _scroller.VerticalScrollBarVisiblePolicy = ScrollBarVisiblePolicy.Invisible;
            // Sets the limit of scroll to one page maximum:
            _scroller.HorizontalPageScrollLimit = 1;
            _scroller.SetPageSize(1.0, 1.0);
            _scroller.SetAlignment(-1, -1);
            _scroller.SetWeight(1.0, 1.0);
            _scroller.Show();

            _innerContainer = new Box(_mainLayout);
            _innerContainer.SetLayoutCallback(OnInnerLayoutUpdate);
            _innerContainer.SetAlignment(-1, -1);
            _innerContainer.SetWeight(1.0, 1.0);
            _innerContainer.Show();
            _scroller.SetContent(_innerContainer);

            _mainLayout.PackEnd(_indexIndicator);
            _mainLayout.PackEnd(_scroller);
            _indexIndicator.StackAbove(_scroller);
        }

        void OnDragStarted(object sender, EventArgs e)
        {
            if (_currentIndex - 1 >= 0 && !_items[_currentIndex - 1].IsRealized)
            {
                RealizeItem(_items[_currentIndex - 1]);
            }
            if (_currentIndex + 1 < _items.Count && !_items[_currentIndex + 1].IsRealized)
            {
                RealizeItem(_items[_currentIndex + 1]);
            }
        }

        void UpdateItems()
        {
            _items.Clear();
            _indexIndicator.Clear();
            _innerContainer.UnPackAll();
            _lastLayoutBound = default(Rect);

            foreach (var item in ShellSection.Items)
            {
                var indexItem = _indexIndicator.Append(null);
                indexItem.Style = GetItemStyle(ShellSection.Items.Count, _items.Count);
                _items.Add(new ItemHolder
                {
                    IsRealized = false,
                    IndexItem = indexItem,
                    Item = item
                });
            }
            _indexIndicator.Update(0);
            UpdateCurrentPage(ShellSection.Items.IndexOf(ShellSection.CurrentItem));
        }

        void RealizeItem(ItemHolder item)
        {
            var renderer = ShellRendererFactory.Default.CreateItemRenderer(item.Item);
            renderer.NativeView.Show();
            item.NativeView = renderer.NativeView;
            item.IsRealized = true;
            _innerContainer.PackEnd(item.NativeView);
            item.NativeView.StackBelow(_indexIndicator);
            item.NativeView.Geometry = item.Bound;
        }

        void UpdateCurrentPage(int index)
        {
            if (_currentIndex == index)
                return;

            _currentIndex = index;
            UpdateCurrentIndexIndicator();
            if (!_items[index].IsRealized)
            {
                RealizeItem(_items[index]);
            }
            UpdateFocusPolicy();
        }

        void UpdateFocusPolicy()
        {
            foreach (var item in _items)
            {
                if (item.IsRealized)
                {
                    if (item.NativeView is ElmSharp.Widget widget)
                    {
                        widget.AllowTreeFocus = (_items[_currentIndex] == item);
                    }
                }
            }
        }

        void UpdateCurrentIndexIndicator()
        {
            if (_currentIndex >= 0 && _currentIndex < _items.Count)
            {
                _items[_currentIndex].IndexItem.Select(true);
            }
        }
        void OnItemsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateItems();
        }

        void OnPageScrolled(object sender, EventArgs e)
        {
            if (_updateByCode > 0)
            {
                return;
            }

            if (_currentIndex < 0 || ShellSection.Items.Count <= _currentIndex)
            {
                return;
            }

            UpdateCurrentPage(_scroller.HorizontalPageIndex);
            var currentItem = ShellSection.Items[_currentIndex];
            ShellSection.SetValueFromRenderer(ShellSection.CurrentItemProperty, currentItem);
        }

        void OnSectionPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ShellSection.CurrentItem))
            {
                var newIndex = ShellSection.Items.IndexOf(ShellSection.CurrentItem);
                if (_currentIndex != newIndex)
                {
                    UpdateCurrentPage(newIndex);
                    _updateByCode++;
                    _scroller.ScrollTo(newIndex, 0, false);
                    _updateByCode--;
                }
            }
        }

        void OnLayout()
        {
            _indexIndicator.Geometry = _mainLayout.Geometry;
            _scroller.Geometry = _mainLayout.Geometry;
        }

        void OnInnerLayoutUpdate()
        {
            if (_lastLayoutBound == _innerContainer.Geometry)
            {
                return;
            }
            _lastLayoutBound = _innerContainer.Geometry;

            var layoutBound = _innerContainer.Geometry.Size;
            int baseX = _innerContainer.Geometry.X;

            Rect bound = _scroller.Geometry;
            int index = 0;
            foreach (var item in _items)
            {
                bound.X = baseX + index * bound.Width;
                item.Bound = bound;
                if (item.IsRealized)
                {
                    item.NativeView.Geometry = bound;
                }
                index++;
            }
            _innerContainer.MinimumWidth = _items.Count * bound.Width;

            if (_items.Count > _currentIndex && _currentIndex >= 0)
            {
                _updateByCode++;
                _scroller.ScrollTo(_currentIndex, 0, false);
                _updateByCode--;
            }
        }

        static string GetItemStyle(int itemCount, int offset)
        {
            string returnValue = string.Empty;
            int startItem;
            int styleNumber;

            if (itemCount % 2 == 0)  //Item count is even.
            {
                startItem = EvenMiddleItem - itemCount / 2;
                styleNumber = startItem + offset;
                returnValue = "item/even_" + styleNumber;
            }
            else  //Item count is odd.
            {
                startItem = OddMiddleItem - itemCount / 2;
                styleNumber = startItem + offset;
                returnValue = "item/odd_" + styleNumber;
            }
            return returnValue;
        }

        class ItemHolder
        {
            public bool IsRealized { get; set; }
            public Rect Bound { get; set; }
            public EvasObject NativeView { get; set; }
            public IndexItem IndexItem { get; set; }
            public ShellContent Item { get; set; }
        }
    }
}