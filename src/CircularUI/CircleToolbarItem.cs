using Xamarin.Forms;

namespace CircularUI
{
    /// <summary>
    /// The CircleToolbarItem is a class that extends Xamarin.Forms.ToolbarItem for Circular UI.
    /// </summary>
    /// <since_tizen> 4 </since_tizen>
    public class CircleToolbarItem : ToolbarItem
    {
        /// <summary>
        /// BindableProperty. Identifies the Subtext bindable property to display on the menu item.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static readonly BindableProperty SubTextProperty = BindableProperty.Create(nameof(SubText), typeof(string), typeof(CircleToolbarItem), null);

        /// <summary>
        /// Gets or sets Subtext to display on the menu item
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public string SubText
        {
            get => (string)GetValue(SubTextProperty);
            set => SetValue(SubTextProperty, value);
        }
    }
}