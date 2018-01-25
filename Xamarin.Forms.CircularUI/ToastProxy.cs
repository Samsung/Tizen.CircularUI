using System;

namespace Xamarin.Forms.CircularUI
{
    /// <summary>
    /// This class is for the internal use by toast.
    /// </summary>
    /// <since_tizen> 4 </since_tizen>
    internal class ToastProxy : IToast
    {
        IToast _toastProxy = null;

        public ToastProxy()
        {
            _toastProxy = DependencyService.Get<IToast>(DependencyFetchTarget.NewInstance);

            if (_toastProxy == null)
                throw new Exception("RealObject is null, Internal instance via DependecyService was not created.");
        }

        /// <summary>
        /// Gets or sets duration of the Toast pop-up.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public int Duration
        {
            get
            {
                return _toastProxy.Duration;
            }

            set
            {
                _toastProxy.Duration = value;
            }
        }

        /// <summary>
        /// Gets or sets text of the Toast pop-up.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public string Text
        {
            get
            {
                return _toastProxy.Text;
            }

            set
            {
                _toastProxy.Text = value;
            }
        }

        /// <summary>
        /// Gets or sets icon of the Toast pop-up.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public FileImageSource Icon
        {
            get
            {
                return _toastProxy.Icon;
            }

            set
            {
                _toastProxy.Icon = value;
            }
        }

        /// <summary>
        /// Dismisses the Toast pop-up.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public void Dismiss()
        {
            _toastProxy.Dismiss();
        }

        /// <summary>
        /// Shows the Toast pop-up.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public void Show()
        {
            _toastProxy.Show();
        }
    }
}
