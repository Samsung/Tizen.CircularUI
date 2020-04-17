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
using Circular = Tizen.Wearable.CircularUI.Forms.FormsCircularUI;

[assembly: Dependency(typeof(Tizen.Wearable.CircularUI.Forms.Renderer.TwoButtonPopupImplementation))]

namespace Tizen.Wearable.CircularUI.Forms.Renderer
{
    public class TwoButtonPopupImplementation : ITwoButtonPopup, IDisposable
    {
        View _content;
        StackLayout _contentView;
        MenuItem _firstMenuItem;
        MenuItem _secondMenuItem;
        Color _firstButtonBgColor;
        Color _secondButtonBgColor;

        ElmSharp.Popup _popUp;
        ElmSharp.Layout _layout;
        ElmSharp.Button _firstButton;
        ElmSharp.Button _secondButton;
        ElmSharp.EvasObject _nativeContent;

        string _title;
        string _text;
        bool _isDisposed;

        public event EventHandler BackButtonPressed;

        public TwoButtonPopupImplementation()
        {
            _popUp = new ElmSharp.Popup(XForms.NativeParent);
            _popUp.Style = "circle";

            _layout = new ElmSharp.Layout(_popUp);
            _layout.SetTheme("layout", "popup", "content/circle/buttons2");
            _popUp.SetContent(_layout);

            _popUp.BackButtonPressed += BackButtonPressedHandler;
            _popUp.Dismissed += OnDismissed;

            _contentView = new StackLayout();
        }

        ~TwoButtonPopupImplementation()
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

                if (value is ColorMenuItem)
                {
                    _firstButtonBgColor = ((ColorMenuItem)value).BackgroundColor;
                }
                else
                {
                    _firstButtonBgColor = Color.Default;
                }

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

                if (value is ColorMenuItem)
                {
                    _secondButtonBgColor = ((ColorMenuItem)value).BackgroundColor;
                }
                else
                {
                    _secondButtonBgColor = Color.Default;
                }

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
            if (!XForms.IsInitialized)
            {
                Log.Debug(Circular.Tag, "Tizen Forms is not initialized");
                return;
            }

            _contentView.Children.Clear();
            if (Content != null)
            {
                _contentView.Children.Add(Content);

                var renderer = Xamarin.Forms.Platform.Tizen.Platform.GetOrCreateRenderer(_contentView);
                (renderer as LayoutRenderer)?.RegisterOnLayoutUpdated();
                var sizeRequest = _contentView.Measure(XForms.NativeParent.Geometry.Width, XForms.NativeParent.Geometry.Height).Request.ToPixel();

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

                if (!FirstButton.IconImageSource.IsNullOrEmpty())
                {
                    var iconSource = FirstButton.IconImageSource as FileImageSource;
                    if (!iconSource.IsNullOrEmpty())
                    {
                        var buttonImage = new ElmSharp.Image(_firstButton);
                        buttonImage.LoadAsync(ResourcePath.GetPath(iconSource));
                        buttonImage.Show();
                        _firstButton.SetPartContent("elm.swallow.content", buttonImage);
                    }
                }

                _firstButton.Clicked += (s, e) =>
                {
                    ((IMenuItemController)FirstButton).Activate();
                };

                if (_firstButtonBgColor != Color.Default)
                {
                    Log.Debug(Circular.Tag, $"TwoButtonPopup set first button background color:{_firstButtonBgColor.ToNative()}");
                    _firstButton.BackgroundColor = _firstButtonBgColor.ToNative();
                }
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

                if (!SecondButton.IconImageSource.IsNullOrEmpty())
                {
                    var iconSource = SecondButton.IconImageSource as FileImageSource;
                    if (!iconSource.IsNullOrEmpty())
                    {
                        var buttonImage = new ElmSharp.Image(_secondButton);
                        buttonImage.LoadAsync(ResourcePath.GetPath(iconSource));
                        buttonImage.Show();
                        _secondButton.SetPartContent("elm.swallow.content", buttonImage);
                    }
                }

                _secondButton.Clicked += (s, e) =>
                {
                    ((IMenuItemController)SecondButton).Activate();
                };

                if (_secondButtonBgColor != Color.Default)
                {
                    Log.Debug(Circular.Tag, $"TwoButtonPopup set second button background color:{_secondButtonBgColor.ToNative()}");
                    _secondButton.BackgroundColor = _secondButtonBgColor.ToNative();
                }

                _popUp.SetPartContent("button2", _secondButton);
            }
            else
            {
                /* This is workaround fix for Left button occupied whole window when right button is null*/
                _secondButton = new ElmSharp.Button(_popUp)
                {
                    WeightX = 1.0,
                    WeightY = 1.0,
                    Style = "popup/circle/right"
                };
                _popUp.SetPartContent("button2", _secondButton);
                _secondButton.Unrealize();
                _secondButton = null;
            }
        }

        void UpdateTitle()
        {
            string title = _title?.Replace("&", "&amp;")
                                  .Replace("<", "&lt;")
                                  .Replace(">", "&gt;")
                                  .Replace(Environment.NewLine, "<br>");
            _layout.SetPartText("elm.text.title", title);
        }

        void UpdateText()
        {
            string text = _text?.Replace("&", "&amp;")
                                .Replace("<", "&lt;")
                                .Replace(">", "&gt;")
                                .Replace(Environment.NewLine, "<br>");
            _layout.SetPartText("elm.text", text);
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
            if (_firstButton != null)
            {
                _firstButton.Unrealize();
                _firstButton = null;
                _popUp.SetPartContent("button1", null);
            }

            if (_secondButton != null)
            {
                _secondButton.Unrealize();
                _secondButton = null;
                _popUp.SetPartContent("button2", null);
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

        void OnDismissed(object sender, EventArgs e)
        {
            Log.Debug(Circular.Tag, $"OnDismissed called");
            Dispose();
        }
    }
}
