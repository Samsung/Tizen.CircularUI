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
using WatchScroller = Xamarin.Forms.Platform.Tizen.Native.Watch.WatchScroller;

[assembly: ExportRenderer(typeof(Tizen.Wearable.CircularUI.Forms.CircleScrollView), typeof(Tizen.Wearable.CircularUI.Forms.Renderer.CircleScrollViewRenderer))]

namespace Tizen.Wearable.CircularUI.Forms.Renderer
{
    public class CircleScrollViewRenderer : ScrollViewRenderer
    {
        public CircleScrollView CircleElement => Element as CircleScrollView;
        WatchScroller _scroller;

        public CircleScrollViewRenderer()
        {
            RegisterPropertyHandler(CircleScrollView.BarColorProperty, UpdateBarColor);
        }

        protected override Xamarin.Forms.Platform.Tizen.Native.Scroller CreateNativeControl()
        {
            return _scroller = new WatchScroller(XForms.NativeParent, this.GetSurface());
        }

        void UpdateBarColor()
        {
            var color = CircleElement.BarColor;
            if (!color.IsDefault)
            {
                _scroller.CircleScroller.VerticalScrollBarColor = color.ToNative();
                _scroller.CircleScroller.HorizontalScrollBarColor = color.ToNative();
            }
        }
    }
}
