/*
 * Copyright (c) 2020 Samsung Electronics Co., Ltd All Rights Reserved
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
using XListViewRenderer = Xamarin.Forms.Platform.Tizen.ListViewRenderer;
using CListViewRenderer = Tizen.Wearable.CircularUI.Forms.Renderer.ListViewRenderer;

[assembly: ExportRenderer(typeof(ListView), typeof(CListViewRenderer))]
namespace Tizen.Wearable.CircularUI.Forms.Renderer
{
    public class ListViewRenderer : XListViewRenderer
    {
        protected override void UpdateRotaryInteraction(bool enable)
        {
            if (Element.FindBezelRouter() == null)
            {
                base.UpdateRotaryInteraction(enable);
            }
        }
    }
}
