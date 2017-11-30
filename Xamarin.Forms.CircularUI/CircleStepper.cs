namespace Xamarin.Forms.CircularUI
{
    public class CircleStepper : Xamarin.Forms.Stepper, IRotaryFocusable
    {
        public static readonly BindableProperty MarkerColorProperty = BindableProperty.Create(nameof(MarkerColor), typeof(Color), typeof(CircleStepper), Color.Default);

        public Color MarkerColor { get => (Color)GetValue(MarkerColorProperty); set => SetValue(MarkerColorProperty, value); }
    }
}
