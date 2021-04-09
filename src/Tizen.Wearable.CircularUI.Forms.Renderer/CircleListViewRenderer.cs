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
using ElmSharp;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using XForms = Xamarin.Forms.Forms;
using NListView = Xamarin.Forms.Platform.Tizen.Native.ListView;
using CircleListView = Tizen.Wearable.CircularUI.Forms.CircleListView;

[assembly: ExportRenderer(typeof(CircleListView), typeof(Tizen.Wearable.CircularUI.Forms.Renderer.CircleListViewRenderer))]

namespace Tizen.Wearable.CircularUI.Forms.Renderer
{
    public class CircleListViewRenderer : ListViewRenderer
    {
        new CircleListView Element => base.Element as CircleListView;
        WatchListView _listView;

        public CircleListViewRenderer()
        {
            RegisterPropertyHandler(CircleListView.BarColorProperty, UpdateBarColor);
        }

        protected override Xamarin.Forms.Platform.Tizen.Native.ListView CreateNativeControl()
        {
            _listView = new WatchListView(XForms.NativeParent, this.GetSurface());
            _listView.ItemLongPressed += OnItemLongPressed;
            _listView.ScrollAnimationStopped += OnScrollAnimationStopped;
            return _listView;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_listView != null)
                {
                    _listView.ItemLongPressed -= OnItemLongPressed;
                    _listView.ScrollAnimationStopped -= OnScrollAnimationStopped;
                }
            }
            base.Dispose(disposing);
        }

        void OnItemLongPressed(object sender, GenListItemEventArgs args)
        {
            if (args.Item.Data is NListView.ItemContext itemContext)
            {
                var obj = itemContext.Cell.BindingContext;
                var index = Element.TemplatedItems.GetGlobalIndexOfItem(obj);
                Element.NotifyItemLongPressed(obj, index);
            }
        }

        void OnScrollAnimationStopped(object sender, EventArgs args)
        {
            GenListItem item = Control.GetItemByPosition(180, 180, out int pos);
            if (item.Data is NListView.ItemContext itemContext && pos == 0)
            {
                var obj = itemContext.Cell.BindingContext;
                var index = Element.TemplatedItems.GetGlobalIndexOfItem(obj);
                Element.NotifyIteFocused(obj, index);
            }
        }

        void UpdateBarColor()
        {
            if (!Element.BarColor.IsDefault)
            {
                _listView.CircleGenList.VerticalScrollBarColor = Element.BarColor.ToNative();
            }
        }
    }
}
