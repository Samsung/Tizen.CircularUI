/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using WearableUIGallery.Extensions;
using Tizen.Extension.Sample;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using CircularUI.Tizen;
using ElmSharp;

[assembly: ExportCell(typeof(RenderCircleListView), typeof(RenderListViewRenderer))]

namespace Tizen.Extension.Sample
{
    public class RenderListViewRenderer : CircleListViewRenderer
    {
        new RenderCircleListView Element => base.Element as RenderCircleListView;

        protected override void OnElementChanged(ElementChangedEventArgs<CircularUI.CircleListView> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                Control.Changed -= OnChanged;
                Control.RenderPost -= OnRenderPost;
                Control.ScrollAnimationStarted -= OnScrollStarted;
                Control.ScrollAnimationStopped -= OnScrollStopped;
            }

            if (e.NewElement != null)
            {
                Control.Style = "solid/default";
                Control.AllowFocus(false);
                Control.RenderPost += OnRenderPost;
                Control.Changed += OnChanged;
                Control.ScrollAnimationStarted += OnScrollStarted;
                Control.ScrollAnimationStopped += OnScrollStopped;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (Element != null)
                {
                    Control.Changed -= OnChanged;
                    Control.RenderPost -= OnRenderPost;
                    Control.ScrollAnimationStarted -= OnScrollStarted;
                    Control.ScrollAnimationStopped -= OnScrollStopped;
                }
            }
            base.Dispose(disposing);
        }

        private void OnRenderPost(object sender, EventArgs e)
        {
            Element.SendRenderPost();
        }

        private void OnScrollStopped(object sender, EventArgs e)
        {
            Element.SendScrollStopped();
        }

        private void OnScrollStarted(object sender, EventArgs e)
        {
            Element.SendScrollStarted();
        }

        private void OnChanged(object sender, EventArgs e)
        {
            Element.SendChanged();
        }
    }
}
