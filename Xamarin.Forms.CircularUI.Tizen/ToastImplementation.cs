using System;
using TForms = Xamarin.Forms.Platform.Tizen.Forms;
using XForms = Xamarin.Forms;
using EPopup = ElmSharp.Popup;
using Xamarin.Forms.Platform.Tizen;

[assembly: XForms.Dependency(typeof(Xamarin.Forms.CircularUI.Tizen.ToastImplementation))]

namespace Xamarin.Forms.CircularUI.Tizen
{
    class ToastImplementation : IToast, IDisposable
    {
        static readonly string DefaultStyle = "toast/circle";
        static readonly string IconPart = "toast,icon";

        EPopup _control;
        FileImageSource _icon;
        int _duration = 3000;
        string _text;
        bool _isDisposed = false;

        public ToastImplementation()
        {
            _control = new ElmSharp.Popup(TForms.NativeParent)
            {
                Style = DefaultStyle,
                AllowEvents = true,
            };
            _control.BackButtonPressed += (s, e) => _control.Dismiss();

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
    }
}
