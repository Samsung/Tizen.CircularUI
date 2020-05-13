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

using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using XForms = Xamarin.Forms.Forms;

using CCircularListView = global::Tizen.Wearable.CircularUI.Forms.CircleListView;

[assembly: ExportRenderer(typeof(CCircularListView), typeof(Tizen.Wearable.CircularUI.Forms.Renderer.CircleListViewRenderer))]

namespace Tizen.Wearable.CircularUI.Forms.Renderer
{
    public class CircleListViewRenderer : ListViewRenderer
    {
        new CCircularListView Element => base.Element as CircleListView;

        public CircleListViewRenderer()
        {
            RegisterPropertyHandler(CCircularListView.BarColorProperty, UpdateBarColor);
        }

        protected override Xamarin.Forms.Platform.Tizen.Native.ListView CreateNativeControl()
        {
            return new WatchListView(XForms.NativeParent, this.GetSurface());
        }

        void UpdateBarColor()
        {
            if (!Element.BarColor.IsDefault)
            {
                (Control as WatchListView).CircleGenList.VerticalScrollBarColor = Element.BarColor.ToNative();
            }
        }
    }
}
