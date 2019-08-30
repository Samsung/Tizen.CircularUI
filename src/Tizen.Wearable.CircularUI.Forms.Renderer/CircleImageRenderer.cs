/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd
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
using UIComponents.Tizen.Wearable.Renderers;
using Xamarin.Forms.Platform.Tizen;
using FormsNative = Xamarin.Forms.Platform.Tizen.Native;
using System.ComponentModel;
using Tizen.Wearable.CircularUI.Forms;
using Tizen.Wearable.CircularUI.Forms.Renderer;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(CircleImage), typeof(CircleImageRenderer))]
namespace UIComponents.Tizen.Wearable.Renderers
{
    public class CircleImageRenderer : ViewRenderer<CircleImage, FormsNative.Image>
    {
        ElmSharp.EvasImage _circleImage;
        ElmSharp.EvasObject _nativeParent;

        protected override void OnElementChanged(ElementChangedEventArgs<CircleImage> e)
        {
            if (Control == null)
            {
                _nativeParent = Platform.GetRenderer(Element.Parent).NativeView;
                _circleImage = new ElmSharp.EvasImage(_nativeParent);
                _circleImage.IsFilled = true;
                GetClipImage();

                var image = new FormsNative.Image(_nativeParent);
                SetNativeControl(image);
                Control.Resized += Control_Resized;
                Control.Moved += Control_Moved;
                Control.Deleted += Control_Deleted;
            }

            UpdateAll();
            base.OnElementChanged(e);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == Image.SourceProperty.PropertyName)
            {
                UpdateSource();
            }
            else if (e.PropertyName == Image.AspectProperty.PropertyName)
            {
                UpdateAspect();
            }
            else if (e.PropertyName == Image.IsOpaqueProperty.PropertyName)
            {
                UpdateIsOpaque();
            }
            else if (e.PropertyName == VisualElement.BackgroundColorProperty.PropertyName)
            {
                UpdateBackgroundColor();
            }
        }

        void UpdateAll()
        {
            UpdateBackgroundColor();
            UpdateSource();
            UpdateAspect();
        }

        async void GetClipImage()
        {
            var imageSource = ImageSource.FromResource("Tizen.Wearable.CircularUI.Forms.Renderer.res.circle.png", GetType().Assembly);
            var streamsource = imageSource as StreamImageSource;

            if (streamsource != null && streamsource.Stream != null)
            {
                using (var streamImage = await ((IStreamImageSource)streamsource).GetStreamAsync())
                {
                    if (streamImage != null)
                    {
                        _circleImage.SetStream(streamImage);
                    }
                    else
                    {
                        global::Tizen.Log.Error(FormsCircularUI.Tag, $"streamImage == null ");
                    }
                }
            }
        }

        async void UpdateSource()
        {
            ImageSource source = Element.Source;
            ((IImageController)Element).SetIsLoading(true);

            if (Control != null)
            {
                bool success = await Control.LoadFromImageSourceAsync(source);
                if (!IsDisposed && success)
                {
                    ((IVisualElementController)Element).NativeSizeChanged();
                    _circleImage.Show();
                    _circleImage.Geometry = Control.Geometry;
                    Control.SetClip(_circleImage);

                    UpdateAfterLoading();
                }
            }

            if (!IsDisposed)
                ((IImageController)Element).SetIsLoading(false);
        }

        protected virtual void UpdateAfterLoading()
        {
            UpdateIsOpaque();
        }

        void UpdateAspect()
        {
            Control.Aspect = Element.Aspect;
        }

        void UpdateIsOpaque()
        {
            Control.IsOpaque = Element.IsOpaque;
        }

        void UpdateBackgroundColor()
        {
            if (_nativeParent == null) return;

            if (Element.BackgroundColor.ToNative() != ElmSharp.Color.Transparent
                    && Element.BackgroundColor.ToNative() != ElmSharp.Color.Default)
            {
                _nativeParent.Color = Element.BackgroundColor.ToNative();
            }
        }

        private void Control_Deleted(object sender, EventArgs e)
        {
            _circleImage?.Unrealize();
            _circleImage = null;
        }

        private void Control_Moved(object sender, EventArgs e)
        {
            _circleImage.Geometry = Control.Geometry;
        }

        private void Control_Resized(object sender, EventArgs e)
        {
            _circleImage.Geometry = Control.Geometry;
        }
    }
}
