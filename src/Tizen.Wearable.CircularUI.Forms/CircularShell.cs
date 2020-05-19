using Xamarin.Forms;

namespace Tizen.Wearable.CircularUI.Forms
{
    /// <summary>
    /// A subclass of Xamarin.Forms.Shell that have additional options for circular screen.
    /// </summary>
    public class CircularShell : Shell
    {
        /// <summary>
        /// BindableProperty. Identifies the FlyoutIconBackgroundColor bindable property.
        /// </summary>
        public static readonly BindableProperty FlyoutIconBackgroundColorProperty = BindableProperty.CreateAttached("FlyoutIconBackgroundColor", typeof(Color), typeof(Shell), Color.Default);

        /// <summary>
        /// BindableProperty. Identifies the FlyoutForegroundColor bindable property.
        /// </summary>
        public static readonly BindableProperty FlyoutForegroundColorProperty = BindableProperty.CreateAttached("FlyoutForegroundColor", typeof(Color), typeof(Shell), Color.Default);

        /// <summary>
        /// Get a color of FlyoutIconBackgroundColor
        /// </summary>
        /// <param name="element">Xamarin.Forms.Shell</param>
        /// <returns>Returns a Color of FlyoutIconBackgroundColor</returns>
        public static Color GetFlyoutIconBackgroundColor(BindableObject element)
        {
            return (Color)element.GetValue(FlyoutIconBackgroundColorProperty);
        }

        /// <summary>
        /// Set a color of FlyoutIconBackgroundColor
        /// </summary>
        /// <param name="element">Xamarin.Forms.Shell</param>
        /// <param name="color">Xamarin.Forms.Color</param>
        public static void SetFlyoutIconBackgroundColor(BindableObject element, Color color)
        {
            element.SetValue(FlyoutIconBackgroundColorProperty, color);
        }

        /// <summary>
        /// Get a color of FlyoutForegroundColor
        /// </summary>
        /// <param name="element">Xamarin.Forms.Shell</param>
        /// <returns>Returns a Color of FlyoutForegroundColor</returns>
        public static Color GetFlyoutForegroundColor(BindableObject element)
        {
            return (Color)element.GetValue(FlyoutForegroundColorProperty);
        }

        /// <summary>
        /// Set a color of FlyoutForegroundColor
        /// </summary>
        /// <param name="element">Xamarin.Forms.Shell</param>
        /// <param name="color">Xamarin.Forms.Color</param>
        public static void SetFlyoutForegroundColor(BindableObject element, Color color)
        {
            element.SetValue(FlyoutForegroundColorProperty, color);
        }

        /// <summary>
        /// Gets or sets FlyoutIconBackgroundColor
        /// </summary>
        public Color FlyoutIconBackgroundColor
        {
            get => (Color)GetValue(FlyoutIconBackgroundColorProperty);
            set => SetValue(FlyoutIconBackgroundColorProperty, value);
        }

        /// <summary>
        /// Gets or sets FlyoutForegroundColor
        /// </summary>
        public Color FlyoutForegroundColor
        {
            get => (Color)GetValue(FlyoutForegroundColorProperty);
            set => SetValue(FlyoutForegroundColorProperty, value);
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
