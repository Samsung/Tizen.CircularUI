using System;
using TForms = Xamarin.Forms.Platform.Tizen.Forms;
using XForms = Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;

[assembly: XForms.Dependency(typeof(Xamarin.Forms.CircularUI.Tizen.TwoButtonPopupImplementation))]

namespace Xamarin.Forms.CircularUI.Tizen
{
    public class TwoButtonPopupImplementation : IPopup, IDisposable
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

        public TwoButtonPopupImplementation()
        {
            _popUp = new ElmSharp.Popup(TForms.Context.MainWindow);
            _popUp.Style = "circle";

            _layout = new ElmSharp.Layout(_popUp);
            _layout.SetTheme("layout", "popup", "content/circle/buttons2");
            _popUp.SetContent(_layout);

            _popUp.BackButtonPressed += BackButtonPressedHandler;

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
            //Console.WriteLine($"Dispose disposing->{disposing}, _isDisposed->{_isDisposed}, _firstButton->{_firstButton.ToString()}, _popUp->{_popUp.ToString()}");
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
                var sizeRequest = _contentView.Measure(TForms.Context.MainWindow.ScreenSize.Width, TForms.Context.MainWindow.ScreenSize.Height).Request.ToPixel();

                //Console.WriteLine($"_contentView.Platform ->{_contentView?.Platform}, renderer->{renderer?.ToString()}");
                _nativeContent = renderer.NativeView;
                _nativeContent.MinimumHeight = sizeRequest.Height;

                _layout.SetPartContent("elm.swallow.content", _nativeContent, true);
                //Console.WriteLine($"_nativeContent ->{_nativeContent}, sizeRequest->{sizeRequest.ToString()}");
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

                var iconPath = FirstButton.Icon.File;
                if (!string.IsNullOrEmpty(iconPath))
                {
                    var buttonImage = new ElmSharp.Image(_firstButton);
                    buttonImage.LoadAsync(ResourcePath.GetPath(iconPath));
                    buttonImage.Show();
                    _firstButton.SetPartContent("elm.swallow.content", buttonImage);
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

                var iconPath = SecondButton.Icon.File;
                if (!string.IsNullOrEmpty(iconPath))
                {
                    var buttonImage = new ElmSharp.Image(_secondButton);
                    buttonImage.LoadAsync(ResourcePath.GetPath(iconPath));
                    buttonImage.Show();
                    _secondButton.SetPartContent("elm.swallow.content", buttonImage);
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
