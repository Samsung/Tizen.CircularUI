namespace Xamarin.Forms.CircularUI
{
    public class CircleToolbarItem : ToolbarItem
    {
        public static readonly BindableProperty SubTextProperty = BindableProperty.Create(nameof(SubText), typeof(string), typeof(CircleToolbarItem), null);
        public string SubText
        {
            get => (string)GetValue(SubTextProperty);
            set => SetValue(SubTextProperty, value);
        }
    }
}