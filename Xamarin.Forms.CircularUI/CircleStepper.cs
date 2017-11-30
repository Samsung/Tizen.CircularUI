namespace Xamarin.Forms.CircularUI
{
    public class CircleStepper : Xamarin.Forms.Stepper, IRotaryFocusable
    {
        public static readonly BindableProperty ColorProperty = BindableProperty.Create(nameof(Color), typeof(Color), typeof(CircleStepper), Color.Default);

        public Color Color { get => (Color)GetValue(ColorProperty); set => SetValue(ColorProperty, value); }

        public event RotaryEventHandler Rotated;
    }
}
