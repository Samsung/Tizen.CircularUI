﻿/*
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

[assembly: Dependency(typeof(Tizen.Wearable.CircularUI.Forms.Renderer.InformationPopupImplementation))]

namespace Tizen.Wearable.CircularUI.Forms.Renderer
{
    public class InformationPopupImplementation : IInformationPopup, IDisposable
    {
        MenuItem _bottomMenuItem;
        Color _buttonBgColor;

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
            _popUp = new ElmSharp.Popup(XForms.NativeParent);
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
                    _popUp.BackButtonPressed -= BackButtonPressedHandler;
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

                if (value is ColorMenuItem)
                {
                    _buttonBgColor = ((ColorMenuItem)value).BackgroundColor;
                }
                else
                {
                    _buttonBgColor = Color.Default;
                }
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
            if(!XForms.IsInitialized)
            {
                Log.Debug(FormsCircularUI.Tag, "Tizen Forms is not initialized");
                return;
            }

            if (_isProgressRunning)
            {
                _box = new ElmSharp.Box(XForms.NativeParent);
                _box.Show();

                _progress = new ElmSharp.ProgressBar(XForms.NativeParent)
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

                if (!BottomButton.IconImageSource.IsNullOrEmpty())
                {
                    var iconSource = BottomButton.IconImageSource as FileImageSource;
                    var buttonImage = new ElmSharp.Image(_bottomButton);
                    buttonImage.LoadAsync(ResourcePath.GetPath(iconSource));
                    buttonImage.Show();
                    _bottomButton.SetPartContent("elm.swallow.content", buttonImage);
                }

                _bottomButton.Clicked += (s, e) =>
                {
                    ((IMenuItemController)BottomButton).Activate();
                };

                if (_buttonBgColor != Color.Default)
                {
                    Log.Debug(FormsCircularUI.Tag, $"InformationPopup set button background color:{_buttonBgColor.ToNative()}");
                    _bottomButton.BackgroundColor = _buttonBgColor.ToNative();
                }
            }
            else
            {
                _bottomButton.Unrealize();
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
                        _progressLabel = new ElmSharp.Label(XForms.NativeParent)
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

            if (_bottomButton != null)
            {
                _bottomButton.Unrealize();
                _bottomButton = null;
                _popUp.SetPartContent("button1", null);
            }

            if (_box != null)
            {
                _box.Unrealize();
                _box = null;
            }

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

            if (_popUp != null)
            {
                _layout.Unrealize();
                _layout = null;
                _popUp.BackButtonPressed -= BackButtonPressedHandler;
                _popUp.Unrealize();
                _popUp = null;
            }
        }

        void OnDismissed(object sender, EventArgs e)
        {
            Log.Debug(FormsCircularUI.Tag, $"OnDismissed called");
            Dispose();
        }
    }
}
