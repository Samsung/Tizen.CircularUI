using System;
using WearableUIExtGallery.Tizen.Wearable;
using TForms = Xamarin.Forms.Platform.Tizen.Forms;
using ElmSharp;
using WearableUIExtGallery.TC;

[assembly: Xamarin.Forms.Dependency(typeof(ToastImplementation))]

namespace WearableUIExtGallery.Tizen.Wearable
{
    class ToastImplementation : IToast, IDisposable
    {
        ElmSharp.Popup _popUp;
        ElmSharp.Layout _layout;

        const double LONG_DELAY = 3.5;
        const double SHORT_DELAY = 2.0;
        bool _isDisposed = false;

        public ToastImplementation()
        {
            _popUp = new ElmSharp.Popup(TForms.Context.MainWindow);
            _popUp.Style = "toast/circle";

            _layout = new ElmSharp.Layout(_popUp);
            _layout.SetTheme("layout", "popup", "content/circle");
            _popUp.SetContent(_layout);
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

        public void LongToast(string message)
        {
            ShowToast(message, LONG_DELAY);
        }

        public void ShortToast(string message)
        {
            ShowToast(message, SHORT_DELAY);
        }

        void ShowToast(string message, double seconds)
        {
            _popUp.Orientation = PopupOrientation.Bottom;
            _popUp.Timeout = seconds;
            _popUp.Text = message;
            _popUp.Show();
        }
    }
}
