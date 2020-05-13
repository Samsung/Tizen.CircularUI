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
using XForms = Xamarin.Forms.Forms;
using Watch = Xamarin.Forms.Platform.Tizen.Native.Watch;

[assembly: ExportRenderer(typeof(Tizen.Wearable.CircularUI.Forms.CircleScrollView), typeof(Tizen.Wearable.CircularUI.Forms.Renderer.CircleScrollViewRenderer))]

namespace Tizen.Wearable.CircularUI.Forms.Renderer
{
    public class CircleScrollViewRenderer : ScrollViewRenderer
    {
        protected override Xamarin.Forms.Platform.Tizen.Native.Scroller CreateNativeControl()
        {
            return new Watch.WatchScroller(XForms.NativeParent, this.GetSurface());
        }
    }
}
