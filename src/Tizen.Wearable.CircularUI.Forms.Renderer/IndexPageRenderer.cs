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
using System.Collections.Generic;
using Tizen.Wearable.CircularUI.Forms;
using Tizen.Wearable.CircularUI.Forms.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using ElmSharp;
using TForms = Xamarin.Forms.Platform.Tizen.Forms;
using System.Collections.Specialized;

[assembly: ExportRenderer(typeof(IndexPage), typeof(IndexPageRenderer))]

namespace Tizen.Wearable.CircularUI.Forms.Renderer
{
    internal static class IndexStyle
    {
        public const string Thumbnail = "thumbnail";
        public const string Circle = "circle";
    }

    public class IndexPageRenderer : VisualElementRenderer<IndexPage>
    {
        const int ItemMaxCount = 20;
        const int OddMiddleItem = 10;
        const int EvenMiddleItem = 11;
        private int _pageIndex = 0;
        private int _changedByScroll = 0;

        Index _index;
        List<IndexItem> _items = new List<IndexItem>();
        ElmSharp.Layout _outterLayout;
        ElmSharp.Box _innerContainer;
        Scroller _scroller;

        private ElmSharp.Size _layoutBound;
        bool _isInitalized = false;
        bool _isUpdateCarousel = false;

        protected override void OnElementChanged(ElementChangedEventArgs<IndexPage> e)
        {
            if (NativeView == null)
            {
                Initialize();
                SetNativeView(_outterLayout);
            }

            if (e.OldElement != null)
            {
                e.OldElement.CurrentPageChanged -= OnCurrentPageChanged;
                e.OldElement.PagesChanged -= OnPagesChanged;
                _isInitalized = false;
            }

            if (e.NewElement != null)
            {
                Element.CurrentPageChanged += OnCurrentPageChanged;
                Element.PagesChanged += OnPagesChanged;
            }

            base.OnElementChanged(e);
        }

        protected override void OnElementReady()
        {
            base.OnElementReady();
            _isInitalized = true;
            UpdateCarouselContent();
            UpdateIndexItem();
        }

        private void Initialize()
        {
            _outterLayout = new ElmSharp.Layout(TForms.NativeParent)
            {
                AlignmentX = -1,
                AlignmentY = -1,
                WeightX = 1,
                WeightY = 1,
            };
            _outterLayout.Show();
            _outterLayout.SetTheme("layout", "application", "default");

            _index = new Index(_outterLayout)
            {
                IsHorizontal = true,
                AutoHide = false,
            };
            _index.Show();
            _outterLayout.SetPartContent("elm.swallow.content", _index);

            _scroller = new Scroller(_outterLayout);
            _scroller.PageScrolled += OnPageScrolled;

            // Disables the visibility of the scrollbar in both directions:
            _scroller.HorizontalScrollBarVisiblePolicy = ElmSharp.ScrollBarVisiblePolicy.Invisible;
            _scroller.VerticalScrollBarVisiblePolicy = ElmSharp.ScrollBarVisiblePolicy.Invisible;
            // Sets the limit of scroll to one page maximum:
            _scroller.HorizontalPageScrollLimit = 1;
            _scroller.SetPageSize(1.0, 1.0);
            _scroller.SetAlignment(-1, -1);
            _scroller.SetWeight(1.0, 1.0);
            _scroller.Show();

            _innerContainer = new Box(_outterLayout);
            _innerContainer.SetLayoutCallback(OnInnerLayoutUpdate);
            _innerContainer.SetAlignment(-1, -1);
            _innerContainer.SetWeight(1.0, 1.0);
            _innerContainer.Show();
            _scroller.SetContent(_innerContainer);

            _outterLayout.SetPartContent("elm.swallow.bg", _scroller);
        }

        private void OnInnerLayoutUpdate()
        {
            if (!_isInitalized || _layoutBound == _innerContainer.Geometry.Size)
                return;

            _layoutBound = _innerContainer.Geometry.Size;
            int baseX = _innerContainer.Geometry.X;
            Rect bound = _scroller.Geometry;
            int index = 0;
            foreach (var page in Element.Children)
            {
                var nativeView = Xamarin.Forms.Platform.Tizen.Platform.GetRenderer(page).NativeView;
                bound.X = baseX + index * bound.Width;
                nativeView.Geometry = bound;
                (nativeView as ElmSharp.Widget)?.AllowFocus(true);
                index++;
            }
            _innerContainer.MinimumWidth = Element.Children.Count * bound.Width;

            if (_scroller.HorizontalPageIndex != _pageIndex)
            {
                _scroller.ScrollTo(_pageIndex, 0, false);
            }
        }

        private void OnPageScrolled(object sender, EventArgs e)
        {
            if(_isUpdateCarousel){
                _isUpdateCarousel = false;
                return;
            }

            _changedByScroll++;
            int previousIndex = _pageIndex;
            _pageIndex = _scroller.HorizontalPageIndex;
            if (previousIndex != _pageIndex)
            {
                (Element.Children[previousIndex] as IPageController)?.SendDisappearing();
                Element.CurrentPage = Element.Children[_pageIndex];
                (Element.CurrentPage as IPageController)?.SendAppearing();
                var selectIndex = _pageIndex;
                if (selectIndex >= ItemMaxCount) selectIndex = ItemMaxCount - 1;
                _items[selectIndex].Select(true);
            }
            _changedByScroll--;
        }

        void OnPagesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UpdateCarouselContent();

            //update Index items.
            _index.Clear();
            UpdateIndexItem();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (Element != null)
                {
                    Element.CurrentPageChanged -= OnCurrentPageChanged;
                    Element.PagesChanged -= OnPagesChanged;
                }
            }
            base.Dispose(disposing);
        }

        void OnCurrentPageChanged(object sender, EventArgs ea)
        {
            if (IsChangedByScroll())
                return;

            Element.UpdateFocusTreePolicy();
            if (Element.CurrentPage != Element.Children[_pageIndex])
            {
                var previousPageIndex = _pageIndex;
                _pageIndex = Element.Children.IndexOf(Element.CurrentPage);
                if (previousPageIndex != _pageIndex)
                {
                    // notify disappearing/appearing pages and scroll to the requested page
                    (Element.Children[previousPageIndex] as IPageController)?.SendDisappearing();
                    _scroller.ScrollTo(_pageIndex, 0, false);
                    (Element.CurrentPage as IPageController)?.SendAppearing();
                    var selectIndex = _pageIndex;
                    if (selectIndex >= ItemMaxCount) selectIndex = ItemMaxCount - 1;
                    _items[selectIndex].Select(true);
                }
            }
        }

        private bool IsChangedByScroll()
        {
            return _changedByScroll > 0;
        }

        void UpdateCarouselContent()
        {
            _innerContainer.UnPackAll();
            foreach (var page in Element.Children)
            {
                var nativeView = Xamarin.Forms.Platform.Tizen.Platform.GetRenderer(page).NativeView;
                _innerContainer.PackEnd(nativeView);
            }
            _pageIndex = Element.Children.IndexOf(Element.CurrentPage);
            _isUpdateCarousel = true;
            _scroller.ScrollTo(_pageIndex, 0, false);
            Element.UpdateFocusTreePolicy();
        }

        private void UpdateIndexItem()
        {
            _index.Style = IndexStyle.Circle;
            _items.Clear();

            var indexCount = Element.Children.Count;
            if (indexCount > ItemMaxCount) indexCount = ItemMaxCount;
            for (int i = 0; i < indexCount; i++)
            {
                var item = _index.Append(null);
                item.Style = getItemStyle(indexCount, i);
                _items.Add(item);
            }
            _index.Update(0);
            var selectIndex = _pageIndex;
            if (selectIndex >= ItemMaxCount) selectIndex = ItemMaxCount - 1;
            _items[selectIndex].Select(true);
        }

        string getItemStyle(int itemCount, int offset)
        {
            String returnValue = "";
            int startItem = 10;
            int styleNumber = 10;

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
