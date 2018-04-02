using Xamarin.Forms;

namespace CircularUI
{
    /// <summary>
    /// The ActionButtonItem is a class that presents a menu item and associates it with a command
    /// </summary>
    /// <since_tizen> 4 </since_tizen>
    public class ActionButtonItem : MenuItem
    {
        /// <summary>
        /// BindableProperty. Identifies the IsEnable bindable property.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static BindableProperty IsEnableProperty = BindableProperty.Create(nameof(IsEnable), typeof(bool), typeof(ActionButtonItem), true);
        /// <summary>
        /// BindableProperty. Identifies the IsVisible bindable property.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static BindableProperty IsVisibleProperty = BindableProperty.Create(nameof(IsVisible), typeof(bool), typeof(ActionButtonItem), true);

        /// <summary>
        /// Gets or sets a boolean value that indicates whether this ActionButtonItem is enabled.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public bool IsEnable
        {
            get => (bool)GetValue(IsEnableProperty);
            set => SetValue(IsEnableProperty, value);
        }

        /// <summary>
        /// Gets or sets a boolean value that indicates whether this ActionButtonItem is visible.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public bool IsVisible
        {
            get => (bool)GetValue(IsVisibleProperty);
            set => SetValue(IsVisibleProperty, value);
        }
    }
}
