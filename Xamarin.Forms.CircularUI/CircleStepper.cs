namespace Xamarin.Forms.CircularUI
{
    public class CircleStepper : Xamarin.Forms.Stepper, IRotaryFocusable
    {
        public static readonly BindableProperty ColorProperty = BindableProperty.Create(nameof(Color), typeof(Color), typeof(CircleStepper), Color.Default);
        public static readonly BindableProperty RatioProperty = BindableProperty.Create(nameof(Ratio), typeof(double), typeof(CircleStepper), -1.0);

        public Color Color { get => (Color)GetValue(ColorProperty); set => SetValue(ColorProperty, value); }
        public double Ratio { get => (double)GetValue(RatioProperty); set => SetValue(RatioProperty, value); }

        public event RotaryEventHandler Rotated;
    }
}
