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

using Tizen.Wearable.CircularUI.Forms;
using Tizen.Wearable.CircularUI.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using MMView = Tizen.Multimedia.MediaView;
using XForms = Xamarin.Forms.Forms;

[assembly: ExportRenderer(typeof(MediaView), typeof(MediaViewRenderer))]
namespace Tizen.Wearable.CircularUI.Renderer
{
    public class MediaViewRenderer : ViewRenderer<MediaView, LayoutCanvas>, IMediaViewProvider
    {
        MMView _mediaView;
        protected override void OnElementChanged(ElementChangedEventArgs<MediaView> e)
        {
            if (Control == null)
            {
                _mediaView = new MMView(XForms.NativeParent);
                SetNativeControl(new LayoutCanvas(XForms.NativeParent));
                Control.LayoutUpdated += (s, evt) => OnLayout();
                Control.Children.Add(_mediaView);
                Control.AllowFocus(true);
            }
            base.OnElementChanged(e);
        }

        MMView IMediaViewProvider.GetMediaView()
        {
            return _mediaView;
        }

        void OnLayout()
        {
            _mediaView.Geometry = Control.Geometry;
        }
    }
}
