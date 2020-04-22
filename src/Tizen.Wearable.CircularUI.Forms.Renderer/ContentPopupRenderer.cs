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

using System;
using System.ComponentModel;
using Tizen.Wearable.CircularUI.Forms;
using Tizen.Wearable.CircularUI.Forms.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using XForms = Xamarin.Forms.Forms;

[assembly: ExportRenderer(typeof(ContentPopup), typeof(ContentPopupRenderer))]

namespace Tizen.Wearable.CircularUI.Forms.Renderer
{
    public class ContentPopupRenderer : IContentPopupRenderer
    {
        ElmSharp.Popup _popup;
        ContentPopup _element;

        public void SetElement(Element element)
        {
            element.Parent = Application.Current.Parent;
            element.PropertyChanged += OnElementPropertyChanged;

            _element = element as ContentPopup;

            UpdateContent();
            UpdateIsShow();
        }

        public ContentPopupRenderer()
        {
            _popup = new ElmSharp.Popup(XForms.NativeParent);
            _popup.Style = "circle";

            _popup.BackButtonPressed += OnBackButtonPressed;
            _popup.Dismissed += OnDismissed;
        }

        ~ContentPopupRenderer()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dismiss()
        {
            _popup?.Hide();
            _popup?.Dismiss();
        }

        public void Show()
        {
            _popup?.Show();
        }

        protected void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == ContentPopup.ContentProperty.PropertyName)
            {
                UpdateContent();
            }
            if (e.PropertyName == ContentPopup.IsShowProperty.PropertyName)
            {
                UpdateIsShow();
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_popup != null)
                {
                    _popup.BackButtonPressed -= OnBackButtonPressed;
                    _popup.Dismissed -= OnDismissed;
                    _popup.Unrealize();
                    _popup = null;
                }
                if (_element != null)
                {
                    _element.PropertyChanged -= OnElementPropertyChanged;
                    _element = null;
                }
            }
        }

        void OnBackButtonPressed(object sender, EventArgs e)
        {
            _element.SendBackButtonPressed();
        }

        void OnDismissed(object sender, EventArgs e)
        {
            _element.SendDismissed();
            Dispose();
        }

        void UpdateContent()
        {
            if (_element.Content != null)
            {
                var renderer = Platform.GetOrCreateRenderer(_element.Content);
                (renderer as LayoutRenderer)?.RegisterOnLayoutUpdated();
                var native = renderer.NativeView;
                var sizeRequest = _element.Content.Measure(XForms.NativeParent.Geometry.Width, XForms.NativeParent.Geometry.Height).Request.ToPixel();
                native.MinimumHeight = sizeRequest.Height;
                _popup.SetContent(native, false);
            }
            else
            {
                _popup.SetContent(null, false);
            }
        }

        void UpdateIsShow()
        {
            if (_element.IsShow)
                Show();
            else
                Dismiss();
        }
    }
}
