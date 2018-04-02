using System;
using TForms = Xamarin.Forms.Platform.Tizen.Forms;
using Xamarin.Forms;
using XForms = Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;

[assembly: XForms.Dependency(typeof(CircularUI.Tizen.InformationPopupImplementation))]

namespace CircularUI.Tizen
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
        bool _isProgressRunning = false;
        bool _isDisposed = false;

        public event EventHandler BackButtonPressed;

        public InformationPopupImplementation()
        {
            _popUp = new ElmSharp.Popup(TForms.NativeParent);
            _popUp.Style = "circle";

            _layout = new ElmSharp.Layout(_popUp);
            _layout.SetTheme("layout", "popup", "content/circle");
            _popUp.SetContent(_layout);

            _popUp.BackButtonPressed += BackButtonPressedHandler;
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

                if (!string.IsNullOrEmpty(_text))
                {
                    var progressLabel = new ElmSharp.Label(TForms.NativeParent)
                    {
                        TextStyle = "DEFAULT = 'align=center'",
                    };
                    progressLabel.Text = _text;
                    progressLabel.Show();
                    _box.PackEnd(progressLabel);
                }

                _layout.SetPartContent("elm.swallow.content", _box, true);

                UpdateTitle();
                UpdateText();
            }
            else
            {
                _layout.SetPartContent("elm.swallow.content", null, true);
            }
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
            }
        }

        public void Show()
        {
            if (Application.Current.Platform == null)
            {
                throw new Exception("When the Application's Platform is null, can not show the Dialog.");
            }

            _popUp.Show();
        }

        public void Dismiss()
        {
            _popUp.Hide();
        }
    }
}
