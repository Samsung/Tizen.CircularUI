namespace Xamarin.Forms.CircularUI
{
    /// <summary>
    /// An item in a toolbar or displayed on a Xamarin.Forms.Page for Circle UI.
    /// </summary>
    public class CircleToolbarItem : ToolbarItem
    {
        /// <summary>
        /// BindableProperty type of Subtext to display on the menu item
        /// </summary>
        public static readonly BindableProperty SubTextProperty = BindableProperty.Create(nameof(SubText), typeof(string), typeof(CircleToolbarItem), null);
        /// <summary>
        /// Gets or sets Subtext to display on the menu item
        /// </summary>
        public string SubText
        {
            get => (string)GetValue(SubTextProperty);
            set => SetValue(SubTextProperty, value);
        }
    }
}