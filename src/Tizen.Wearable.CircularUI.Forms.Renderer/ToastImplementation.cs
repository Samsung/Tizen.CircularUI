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

using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;
using XForms = Xamarin.Forms.Forms;
using EPopup = ElmSharp.Popup;

[assembly: Dependency(typeof(Tizen.Wearable.CircularUI.Forms.Renderer.ToastImplementation))]

namespace Tizen.Wearable.CircularUI.Forms.Renderer
{
    class ToastImplementation : IToast, IDisposable
    {
        static readonly string DefaultStyle = "toast/circle";
        static readonly string IconPart = "toast,icon";

        EPopup _control;
        FileImageSource _icon;
        int _duration = 3000;
        string _text;
        bool _isDisposed;

        public ToastImplementation()
        {
            _control = new ElmSharp.Popup(XForms.NativeParent)
            {
                Style = DefaultStyle,
                AllowEvents = false,
            };

            _control.BackButtonPressed += (s, e) => _control.Dismiss();
            _control.TimedOut += (s, e) => _control.Dismiss();
            _control.OutsideClicked += (s, e) => _control.Dismiss();
            _control.Dismissed += OnDismissed;

            UpdateIcon();
            UpdateText();
            UpdateDuration();
        }

        ~ToastImplementation()
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
                if (_control != null)
                {
                    _control.Unrealize();
                    _control = null;
                }
            }

            _isDisposed = true;
        }

        public FileImageSource Icon
        {
            get
            {
                return _icon;
            }
            set
            {
                _icon = value;
                UpdateIcon();
            }
        }

        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                if (_text == value) return;
                _text = value;
                UpdateText();
            }
        }

        public int Duration
        {
            get
            {
                return _duration;
            }
            set
            {
                _duration = value;
                UpdateDuration();
            }
        }

        void UpdateDuration()
        {
            _control.Timeout = Duration / 1000.0;
        }

        void UpdateIcon()
        {
            if (Icon != null)
            {
                var path = ResourcePath.GetPath(_icon);
                var image = new ElmSharp.Image(_control);
                image.LoadAsync(path);
                image.Show();
                _control.SetPartContent(IconPart, image);
            }
        }

        void UpdateText()
        {
            _control.Text = _text;
        }

        public void Show()
        {
            _control.Show();
        }
        public void Dismiss()
        {
            _control.Dismiss();
        }

        void OnDismissed(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
