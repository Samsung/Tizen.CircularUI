using System;
using TForms = Xamarin.Forms.Platform.Tizen.Forms;
using TPlatform = Xamarin.Forms.Platform.Tizen.Platform;
using XForms = Xamarin.Forms;
using WearableUIGallery.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Tizen;

[assembly: XForms.Dependency(typeof(WearableUIGallery.Tizen.Wearable.CustomRenderer.DefaultPopupImplementation))]

namespace WearableUIGallery.Tizen.Wearable.CustomRenderer
{
    public class DefaultPopupImplementation : IPopup, IDisposable
    {
        View _content;
        StackLayout _contentView;
        MenuItem _bottomMenuItem;

        ElmSharp.Popup _popUp;
        ElmSharp.Layout _layout;
        ElmSharp.Button _bottomButton;
        ElmSharp.EvasObject _nativeContent;

        string _title;
        string _text;
        bool _isDisposed = false;

        public DefaultPopupImplementation()
        {
            _popUp = new ElmSharp.Popup(TForms.NativeParent);
            _popUp.Style = "circle";

            _layout = new ElmSharp.Layout(_popUp);
            _layout.SetTheme("layout", "popup", "content/circle");
            _popUp.SetContent(_layout);

            _popUp.BackButtonPressed += (s, e) => _popUp.Dismiss();

            _contentView = new StackLayout();
        }

        ~DefaultPopupImplementation()
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

        void UpdateContent()
        {
            if (Application.Current.Platform == null)
                return;

            _contentView.Children.Clear();
            if (Content != null)
            {
                _contentView.Children.Add(Content);
                _contentView.Platform = Application.Current.Platform;

                var renderer = TPlatform.GetOrCreateRenderer(_contentView);
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

                string text = BottomButton.Text;
                if (!string.IsNullOrEmpty(text))_bottomButton.Text = text;

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
