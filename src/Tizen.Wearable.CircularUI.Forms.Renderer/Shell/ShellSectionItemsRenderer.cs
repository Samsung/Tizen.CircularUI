using ElmSharp;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using Xamarin.Forms;
using ELayout = ElmSharp.Layout;
using XForms = Xamarin.Forms.Forms;

namespace Tizen.Wearable.CircularUI.Forms.Renderer
{
    public class ShellSectionItemsRenderer : IShellItemRenderer
    {
        const int ItemMaxCount = 20;
        const int OddMiddleItem = 10;
        const int EvenMiddleItem = 11;

        ELayout _mainLayout;
        Index _indexIndicator;
        Scroller _scroller;
        Box _innerContainer;

        List<EvasObject> _items = new List<EvasObject>();
        List<IndexItem> _indexItems = new List<IndexItem>();
        int _currentIndex = -1;

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
            _mainLayout = new ELayout(XForms.NativeParent)
            {
                AlignmentX = -1,
                AlignmentY = -1,
                WeightX = 1,
                WeightY = 1,
            };
            _mainLayout.Show();
            _mainLayout.SetTheme("layout", "application", "default");

            _indexIndicator = new Index(_mainLayout)
            {
                IsHorizontal = true,
                AutoHide = false,
                Style = IndexStyle.Circle
            };
            _indexIndicator.Show();
            _mainLayout.SetPartContent("elm.swallow.content", _indexIndicator);

            _scroller = new Scroller(_mainLayout);
            _scroller.PageScrolled += OnPageScrolled;

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

            _mainLayout.SetPartContent("elm.swallow.bg", _scroller);
        }

        void UpdateItems()
        {
            // TODO. Need to improve, Consider lazy creation of each pages
            _items.Clear();
            _indexItems.Clear();
            _indexIndicator.Clear();
            _innerContainer.UnPackAll();
            foreach (var item in ShellSection.Items)
            {
                var renderer = ShellRendererFactory.Default.CreateItemRenderer(item);
                renderer.NativeView.Show();
                _items.Add(renderer.NativeView);
                _innerContainer.PackEnd(renderer.NativeView);
                var indexItem = _indexIndicator.Append(null);
                indexItem.Style = GetItemStyle(ShellSection.Items.Count, _indexItems.Count);
                _indexItems.Add(indexItem);
            }
            _indexIndicator.Update(0);
            UpdateCurrentPage(ShellSection.Items.IndexOf(ShellSection.CurrentItem));
        }

        void UpdateCurrentPage(int index)
        {
            if (_currentIndex == index)
                return;
            _currentIndex = index;
            UpdateCurrentIndexIndicator();
        }

        void UpdateCurrentIndexIndicator()
        {
            if (_currentIndex >= 0 && _currentIndex < _indexItems.Count)
            {
                _indexItems[_currentIndex].Select(true);
            }
        }
        void OnItemsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateItems();
        }

        void OnPageScrolled(object sender, EventArgs e)
        {
            UpdateCurrentPage(_scroller.HorizontalPageIndex);
            if (_currentIndex < 0 || ShellSection.Items.Count <= _currentIndex)
            {
                return;
            }
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
                    _scroller.ScrollTo(_currentIndex, 0, false);
                }
            }
        }

        void OnInnerLayoutUpdate()
        {
            var layoutBound = _innerContainer.Geometry.Size;
            int baseX = _innerContainer.Geometry.X;

            Rect bound = _scroller.Geometry;
            int index = 0;
            foreach (var item in _items)
            {
                bound.X = baseX + index * bound.Width;
                item.Geometry = bound;
                (item as ElmSharp.Widget)?.AllowFocus(true);
                index++;
            }
            _innerContainer.MinimumWidth = _items.Count * bound.Width;

            if (_scroller.HorizontalPageIndex != _currentIndex && _currentIndex >= 0)
            {
                _scroller.ScrollTo(_currentIndex, 0, false);
            }
        }

        string GetItemStyle(int itemCount, int offset)
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

    }
}
