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
using Xamarin.Forms;
using TForms = Xamarin.Forms.Platform.Tizen.Forms;
using XForms = Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;

[assembly: XForms.Dependency(typeof(CircularUI.Tizen.ConfirmationPopupImplementation))]

namespace CircularUI.Tizen
{
    public class ConfirmationPopupImplementation : IConfirmationPopup, IDisposable
    {
        View _content;
        StackLayout _contentView;
        MenuItem _firstMenuItem;
        MenuItem _secondMenuItem;

        ElmSharp.Popup _popUp;
        ElmSharp.Layout _layout;
        ElmSharp.Button _firstButton;
        ElmSharp.Button _secondButton;
        ElmSharp.EvasObject _nativeContent;

        string _title;
        string _text;
        bool _isDisposed = false;

        public event EventHandler BackButtonPressed;

        public ConfirmationPopupImplementation()
        {
            _popUp = new ElmSharp.Popup(TForms.NativeParent);
            _popUp.Style = "circle";

            _layout = new ElmSharp.Layout(_popUp);
            _layout.SetTheme("layout", "popup", "content/circle/buttons2");
            _popUp.SetContent(_layout);

            _popUp.BackButtonPressed += BackButtonPressedHandler;

            _contentView = new StackLayout();
        }

        ~ConfirmationPopupImplementation()
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
                if (_firstButton != null)
                {
                    _firstButton.Unrealize();
                    _firstButton = null;
                }
                if (_secondButton != null)
                {
                    _secondButton.Unrealize();
                    _secondButton = null;
                }
                if (_nativeContent != null)
                {
                    _nativeContent.Unrealize();
                    _nativeContent = null;
                }

                if (_popUp != null)
                {
                    _layout.Unrealize();
                    _layout = null;
                    _popUp.Unrealize();
                    _popUp = null;
                }
            }

            _isDisposed = true;
        }

        void BackButtonPressedHandler(object sender, EventArgs e)
        {
            BackButtonPressed?.Invoke(this, EventArgs.Empty);
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

        public MenuItem FirstButton
        {
            get
            {
                return _firstMenuItem;
            }
            set
            {
                if (_firstMenuItem == value) return;
                _firstMenuItem = value;
                UpdateFirstButton();
            }
        }

        public MenuItem SecondButton
        {
            get
            {
                return _secondMenuItem;
            }
            set
            {
                if (_secondMenuItem == value) return;
                _secondMenuItem = value;
                UpdateSecondButton();
            }
        }

        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                if (_title == value) return;
                _title = value;
                UpdateTitle();
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

        void UpdateContent()
        {
            if (Application.Current.Platform == null)
                return;

            _contentView.Children.Clear();
            if (Content != null)
            {
                _contentView.Children.Add(Content);
                _contentView.Platform = Application.Current.Platform;

                var renderer = Xamarin.Forms.Platform.Tizen.Platform.GetOrCreateRenderer(_contentView);
                (renderer as LayoutRenderer)?.RegisterOnLayoutUpdated();
                var sizeRequest = _contentView.Measure(TForms.NativeParent.Geometry.Width, TForms.NativeParent.Geometry.Height).Request.ToPixel();

                _nativeContent = renderer.NativeView;
                _nativeContent.MinimumHeight = sizeRequest.Height;

                _layout.SetPartContent("elm.swallow.content", _nativeContent, true);
            }
            else
            {
                _layout.SetPartContent("elm.swallow.content", null, true);
            }
        }

        void UpdateFirstButton()
        {
            _firstButton?.Hide();

            if (FirstButton != null)
            {
                _firstButton = new ElmSharp.Button(_popUp)
                {
                    WeightX = 1.0,
                    WeightY = 1.0,
                    Style = "popup/circle/left"
                };

                if (!string.IsNullOrEmpty(FirstButton.Text)) _firstButton.Text = FirstButton.Text;

                if (FirstButton.Icon != null)
                {
                    var iconPath = FirstButton.Icon.File;
                    if (!string.IsNullOrEmpty(iconPath))
                    {
                        var buttonImage = new ElmSharp.Image(_firstButton);
                        buttonImage.LoadAsync(ResourcePath.GetPath(iconPath));
                        buttonImage.Show();
                        _firstButton.SetPartContent("elm.swallow.content", buttonImage);
                    }
                }

                _firstButton.Clicked += (s, e) =>
                {
                    FirstButton.Activate();
                };
            }
            else
            {
                _firstButton = null;
            }

            _popUp.SetPartContent("button1", _firstButton);
        }

        void UpdateSecondButton()
        {
            _secondButton?.Hide();

            if (SecondButton != null)
            {
                _secondButton = new ElmSharp.Button(_popUp)
                {
                    WeightX = 1.0,
                    WeightY = 1.0,
                    Style = "popup/circle/right"
                };

                if (!string.IsNullOrEmpty(SecondButton.Text)) _secondButton.Text = SecondButton.Text;

                if (FirstButton.Icon != null)
                {
                    var iconPath = SecondButton.Icon.File;
                    if (!string.IsNullOrEmpty(iconPath))
                    {
                        var buttonImage = new ElmSharp.Image(_secondButton);
                        buttonImage.LoadAsync(ResourcePath.GetPath(iconPath));
                        buttonImage.Show();
                        _secondButton.SetPartContent("elm.swallow.content", buttonImage);
                    }
                }

                _secondButton.Clicked += (s, e) =>
                {
                    SecondButton.Activate();
                };
            }
            else
            {
                _secondButton = null;
            }

            _popUp.SetPartContent("button2", _secondButton);
        }

        void UpdateTitle()
        {
            _layout.SetPartText("elm.text.title", _title);
        }

        void UpdateText()
        {
            _layout.SetPartText("elm.text", _text);
        }

        public void Show()
        {
            if (Application.Current.Platform == null)
            {
                throw new Exception("When the Application's Platform is null, can not show the Dialog.");
            }
            if (_contentView.Platform == null)
            {
                UpdateContent();
            }
            _popUp.Show();
        }

        public void Dismiss()
        {
            _popUp.Hide();
        }
    }
}
