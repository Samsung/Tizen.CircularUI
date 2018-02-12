using System;
using Xamarin.Forms;
using Xamarin.Forms.CircularUI;

namespace Xamarin.Forms.CircularUI
{
    public class InformationPopup : BindableObject
    {
        /// <summary>
        /// BindableProperty. Identifies the IsProgressRuning bindable property.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static readonly BindableProperty IsProgressRuningProperty = BindableProperty.Create(nameof(IsProgressRuning), typeof(bool), typeof(InformationPopup), false);

        /// <summary>
        /// BindableProperty. Identifies the title bindable property.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(InformationPopup), null);

        /// <summary>
        /// BindableProperty. Identifies the title bindable property.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(InformationPopup), null);

        /// <summary>
        /// BindableProperty. Identifies the first button bindable property.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static readonly BindableProperty BottomButtonProperty = BindableProperty.Create(nameof(BottomButton), typeof(MenuItem), typeof(InformationPopup), null);

        IInformationPopup _popUp = null;

        /// <summary>
        /// Occurs when the device's back button is pressed.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public event EventHandler BackButtonPressed;

        public InformationPopup()
        {
            _popUp = DependencyService.Get<IInformationPopup>(DependencyFetchTarget.NewInstance);
            if (_popUp == null)
            {
                throw new Exception("Object reference not set to an instance of a Popup.");
            }

            _popUp.BackButtonPressed += (s, e) =>
            {
                BackButtonPressed?.Invoke(this, EventArgs.Empty);
            };

            SetBinding(IsProgressRuningProperty, new Binding(nameof(IsProgressRuning), mode: BindingMode.OneWayToSource, source: _popUp));
            SetBinding(BottomButtonProperty, new Binding(nameof(BottomButton), mode: BindingMode.OneWayToSource, source: _popUp));
            SetBinding(TitleProperty, new Binding(nameof(Title), mode: BindingMode.OneWayToSource, source: _popUp));
            SetBinding(TextProperty, new Binding(nameof(Text), mode: BindingMode.OneWayToSource, source: _popUp));
        }

        /// <summary>
        /// Gets or sets progress visibility of the Popup.
        /// If this value is true. Popup displays circular progress and hides Title automatically.
        /// </summary>
        public bool IsProgressRuning
        {
            get { return (bool)GetValue(IsProgressRuningProperty); }
            set { SetValue(IsProgressRuningProperty, value); }
        }

        /// <summary>
        /// Gets or sets title of the Popup.
        /// </summary>
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        /// <summary>
        /// Gets or sets text of the Popup.
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        /// <summary>
        /// Gets or sets bottom button of the Popup.
        /// </summary>
        public MenuItem BottomButton
        {
            get { return (MenuItem)GetValue(BottomButtonProperty); }
            set { SetValue(BottomButtonProperty, value); }
        }

        /// <summary>
        /// Shows the Popup.
        /// </summary>
        public void Show()
        {
            _popUp.Show();
        }

        /// <summary>
        /// Dismisses the ConfirmationPopup.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public void Dismiss()
        {
            _popUp.Dismiss();
        }
    }
}
