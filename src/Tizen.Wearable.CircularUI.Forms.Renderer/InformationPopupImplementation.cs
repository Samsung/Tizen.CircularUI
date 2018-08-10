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
using TForms = Xamarin.Forms.Platform.Tizen.Forms;
using Xamarin.Forms;
using XForms = Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;

[assembly: XForms.Dependency(typeof(Tizen.Wearable.CircularUI.Forms.Renderer.InformationPopupImplementation))]

namespace Tizen.Wearable.CircularUI.Forms.Renderer
{
    public class InformationPopupImplementation : IInformationPopup, IDisposable
    {
        MenuItem _bottomMenuItem;

        ElmSharp.Popup _popUp;
        ElmSharp.ProgressBar _progress;
        ElmSharp.Layout _layout;
        ElmSharp.Button _bottomButton;
        ElmSharp.Box _box;
        ElmSharp.Label _progressLabel;

        string _title;
        string _text;
        bool _isProgressRunning;
        bool _isDisposed;

        public event EventHandler BackButtonPressed;

        public InformationPopupImplementation()
        {
            _popUp = new ElmSharp.Popup(TForms.NativeParent);
            _popUp.Style = "circle";

            _layout = new ElmSharp.Layout(_popUp);
            _layout.SetTheme("layout", "popup", "content/circle");
            _popUp.SetContent(_layout);

            _popUp.BackButtonPressed += BackButtonPressedHandler;
            _popUp.Dismissed += OnDismissed;
        }

        ~InformationPopupImplementation()
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
                if (_bottomButton != null)
                {
                    _bottomButton.Unrealize();
                    _bottomButton = null;
                }

                if (_box != null)
                {
                    _box.Unrealize();
                    _box = null;
                }

                if(_progress != null)
                {
                    _progress.Unrealize();
                    _progress = null;
                }

                if (_progressLabel != null)
                {
                    _progressLabel.Unrealize();
                    _progressLabel = null;
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

        public bool IsProgressRunning
        {
            get
            {
                return _isProgressRunning;
            }
            set
            {
                _isProgressRunning = value;
                UpdateProcessVisibility();
            }
        }

        public MenuItem BottomButton
        {
            get
            {
                return _bottomMenuItem;
            }
            set
            {
                if (_bottomMenuItem == value) return;
                _bottomMenuItem = value;
                UpdateButton();
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

        void UpdateProcessVisibility()
        {
            if (Application.Current.Platform == null)
                return;

            if (_isProgressRunning)
            {
                _box = new ElmSharp.Box(TForms.NativeParent);
                _box.Show();

                _progress = new ElmSharp.ProgressBar(TForms.NativeParent)
                {
                    Style = "process/popup/small",
                };
                _progress.Show();
                _progress.PlayPulse();
                _box.PackEnd(_progress);

                _layout.SetPartContent("elm.swallow.content", _box, true);
            }
            else
            {
                if (_box != null)
                {
                    if (_progress != null)
                    {
                        _progress.Unrealize();
                        _progress = null;
                    }

                    if (_progressLabel != null)
                    {
                        _progressLabel.Unrealize();
                        _progressLabel = null;
                    }

                    _box.Unrealize();
                    _box = null;
                }
                _layout.SetPartContent("elm.swallow.content", null, true);
            }

            UpdateTitle();
            UpdateText();
        }

        void UpdateButton()
        {
            _bottomButton?.Hide();

            if (BottomButton != null)
            {
                _bottomButton = new ElmSharp.Button(_popUp)
                {
                    WeightX = 1.0,
                    WeightY = 1.0,
                    Style = "bottom"
                };

                if (!string.IsNullOrEmpty(BottomButton.Text))_bottomButton.Text = BottomButton.Text;

                if (BottomButton.Icon != null)
                {
                    var iconPath = BottomButton.Icon.File;
                    if (!string.IsNullOrEmpty(iconPath))
                    {
                        var buttonImage = new ElmSharp.Image(_bottomButton);
                        buttonImage.LoadAsync(ResourcePath.GetPath(iconPath));
                        buttonImage.Show();
                        _bottomButton.SetPartContent("elm.swallow.content", buttonImage);
                    }
                }

                _bottomButton.Clicked += (s, e) =>
                {
                    BottomButton.Activate();
                };
            }
            else
            {
                _bottomButton = null;
            }

            _popUp.SetPartContent("button1", _bottomButton);
        }

        void UpdateTitle()
        {
            if (!_isProgressRunning)
            {
                _layout.SetPartText("elm.text.title", _title);
            }
            else
            {
                _layout.SetPartText("elm.text.title", null);
            }
        }

        void UpdateText()
        {
            if (!_isProgressRunning)
            {
                _layout.SetPartText("elm.text", _text);
            }
            else
            {
                _layout.SetPartText("elm.text", null);
                if (!string.IsNullOrEmpty(_text))
                {
                    if (_progressLabel == null)
                    {
                        _progressLabel = new ElmSharp.Label(TForms.NativeParent)
                        {
                            TextStyle = "DEFAULT ='font=Tizen:style=Light color=#F9F9F9FF font_size=32 align=center valign=top wrap=word'",
                        };
                    }
                    _progressLabel.Text = _text;
                    _progressLabel.Show();
                    if (_box != null)
                    {
                        _box.PackEnd(_progressLabel);
                    }
                }
            }
        }

        public void Show()
        {
            if (Application.Current.Platform == null)
            {
                throw new InvalidOperationException("When the Application's Platform is null, can not show the Dialog.");
            }

            if (_popUp != null)
            {
                _popUp.Show();
            }
        }

        public void Dismiss()
        {
            if (_popUp != null)
            {
                _popUp.Dismiss();
            }
        }

        void OnDismissed(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
