namespace Xamarin.Forms.CircularUI
{
    public class ActionButtonItem : MenuItem
    {
        public static BindableProperty IsEnableProperty = BindableProperty.Create(nameof(IsEnable), typeof(bool), typeof(ActionButtonItem), true);
        public static BindableProperty IsVisibleProperty = BindableProperty.Create(nameof(IsVisible), typeof(bool), typeof(ActionButtonItem), true);

        public bool IsEnable
        {
            get => (bool)GetValue(IsEnableProperty);
            set => SetValue(IsEnableProperty, value);
        }

        public bool IsVisible
        {
            get => (bool)GetValue(IsVisibleProperty);
            set => SetValue(IsVisibleProperty, value);
        }
    }
}
