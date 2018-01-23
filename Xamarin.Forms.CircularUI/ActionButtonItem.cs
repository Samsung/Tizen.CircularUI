namespace Xamarin.Forms.CircularUI
{
    /// <summary>
    /// presents a menu item and associates it with a command
    /// </summary>
    public class ActionButtonItem : MenuItem
    {
        /// <summary>
        /// BindableProperty type of IsEnable
        /// </summary>
        public static BindableProperty IsEnableProperty = BindableProperty.Create(nameof(IsEnable), typeof(bool), typeof(ActionButtonItem), true);
        /// <summary>
        /// BindableProperty type of IsVisible
        /// </summary>
        public static BindableProperty IsVisibleProperty = BindableProperty.Create(nameof(IsVisible), typeof(bool), typeof(ActionButtonItem), true);

        /// <summary>
        /// Gets or sets a boolean value that indicates whether this ActionButtonItem is enabled.
        /// </summary>
        public bool IsEnable
        {
            get => (bool)GetValue(IsEnableProperty);
            set => SetValue(IsEnableProperty, value);
        }

        /// <summary>
        /// Gets or sets a boolean value that indicates whether this ActionButtonItem is visible.
        /// </summary>
        public bool IsVisible
        {
            get => (bool)GetValue(IsVisibleProperty);
            set => SetValue(IsVisibleProperty, value);
        }
    }
}
