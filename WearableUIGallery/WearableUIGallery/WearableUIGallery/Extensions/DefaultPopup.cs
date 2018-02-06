using System;
using Xamarin.Forms;

namespace WearableUIGallery.Extensions
{
    public class DefaultPopup : BindableObject
    {
        public static readonly BindableProperty ContentProperty = BindableProperty.Create(nameof(Content), typeof(View), typeof(DefaultPopup), null);

        public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(DefaultPopup), null);

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(DefaultPopup), null);

        public static readonly BindableProperty BottomButtonProperty = BindableProperty.Create(nameof(BottomButton), typeof(MenuItem), typeof(DefaultPopup), null);

        IPopup _popUp = null;

        public DefaultPopup()
        {
            _popUp = DependencyService.Get<IPopup>(DependencyFetchTarget.NewInstance);
            if (_popUp == null)
            {
                throw new Exception("Object reference not set to an instance of a Popup.");
            }

            SetBinding(ContentProperty, new Binding(nameof(Content), mode: BindingMode.OneWayToSource, source: _popUp));
            SetBinding(BottomButtonProperty, new Binding(nameof(BottomButton), mode: BindingMode.OneWayToSource, source: _popUp));
            SetBinding(TitleProperty, new Binding(nameof(Title), mode: BindingMode.OneWayToSource, source: _popUp));
            SetBinding(TextProperty, new Binding(nameof(Text), mode: BindingMode.OneWayToSource, source: _popUp));
        }

        /// <summary>
        /// Gets or sets content view of the Popup.
        /// </summary>
        public View Content
        {
            get { return (View)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
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
    }
}
