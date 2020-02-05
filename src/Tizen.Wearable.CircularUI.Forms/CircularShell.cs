using Xamarin.Forms;

namespace Tizen.Wearable.CircularUI.Forms
{
    public class CircularShell : Shell
    {
        public static readonly BindableProperty FlyoutIconBackgroundColorProperty = BindableProperty.CreateAttached("FlyoutIconBackgroundColor", typeof(Color), typeof(Shell), Color.Default);

        public static Color GetFlyoutIconBackgroundColor(BindableObject element)
        {
            return (Color)element.GetValue(FlyoutIconBackgroundColorProperty);
        }

        public static void SetFlyoutIconBackgroundColor(BindableObject element, Color color)
        {
            element.SetValue(FlyoutIconBackgroundColorProperty, color);
        }

        public Color FlyoutIconBackgroundColor
        {
            get => (Color)GetValue(FlyoutIconBackgroundColorProperty);
            set => SetValue(FlyoutIconBackgroundColorProperty, value);
        }

        protected override bool OnBackButtonPressed()
        {
            if (FlyoutIsPresented)
            {
                FlyoutIsPresented = false;
                return true;
            }
            return base.OnBackButtonPressed();
        }
    }
}
