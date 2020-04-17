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
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using XForms = Xamarin.Forms.Forms;

[assembly: Dependency(typeof(Tizen.Wearable.CircularUI.Forms.Renderer.ContentPopupImplementation))]

namespace Tizen.Wearable.CircularUI.Forms.Renderer
{
    public class ContentPopupImplementation : IContentPopup, IDisposable
    {
        View _content;
        ElmSharp.Popup _popUp;
        bool _isDisposed;

        public event EventHandler BackButtonPressed;

        public ContentPopupImplementation()
        {
            _popUp = new ElmSharp.Popup(XForms.NativeParent);
            _popUp.Style = "circle";

            _popUp.BackButtonPressed += BackButtonPressedHandler;
            _popUp.Dismissed += OnDismissed;
        }

        ~ContentPopupImplementation()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed)
                return;

            if (disposing)
            {
                if (Content != null)
                    Content = null;

                if (_popUp != null)
                {
                    _popUp.BackButtonPressed -= BackButtonPressedHandler;
                    _popUp.Dismissed -= OnDismissed;
                    _popUp.Unrealize();
                    _popUp = null;
                }
            }

            _isDisposed = true;
        }

        public View Content
        {
            get
            {
                return _content;
            }
            set
            {
                _content = value;
                UpdateContent();
            }
        }

        void BackButtonPressedHandler(object sender, EventArgs e)
        {
            BackButtonPressed?.Invoke(this, EventArgs.Empty);
        }

        void UpdateContent()
        {
            if (!XForms.IsInitialized)
            {
                Log.Debug(FormsCircularUI.Tag, "Tizen Forms is not initialized");
                return;
            }

            if (Content != null)
            {
                var renderer = Platform.GetOrCreateRenderer(Content);
                (renderer as LayoutRenderer)?.RegisterOnLayoutUpdated();
                var native = renderer.NativeView;
                var sizeRequest = Content.Measure(XForms.NativeParent.Geometry.Width, XForms.NativeParent.Geometry.Height).Request.ToPixel();
                native.MinimumHeight = sizeRequest.Height;
                _popUp.SetContent(native, false);
            }
            else
            {
                if (Content != null)
                    Content = null;
                _popUp.SetContent(null, false);
            }
        }

        public void Show()
        {
            if (!XForms.IsInitialized)
            {
                throw new InvalidOperationException("When the Application's Platform is not initialized, it can not show the Dialog.");
            }

            if (_popUp != null)
            {
                _popUp.Show();
            }
        }

        public void Dismiss()
        {
            _popUp.Hide();
            _popUp.Dismiss();
        }

        void OnDismissed(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
