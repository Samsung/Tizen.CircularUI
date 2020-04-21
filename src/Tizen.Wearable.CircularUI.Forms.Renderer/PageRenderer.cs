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
using Xamarin.Forms.Platform.Tizen.Native.Watch;
using XPageRenderer = Xamarin.Forms.Platform.Tizen.PageRenderer;
using CPageRenderer = Tizen.Wearable.CircularUI.Forms.Renderer.PageRenderer;

[assembly: ExportRenderer(typeof(Page), typeof(CPageRenderer))]
namespace Tizen.Wearable.CircularUI.Forms.Renderer
{
    public class PageRenderer : XPageRenderer
    {
        protected override FormsMoreOptionItem CreateMoreOptionItem(ToolbarItem item)
        {
            var moreOptionItem = base.CreateMoreOptionItem(item);
            if (item is CircleToolbarItem circleToolbarItem)
            {
                moreOptionItem.SubText = circleToolbarItem.SubText;
            }
            return moreOptionItem;
        }
    }
}
