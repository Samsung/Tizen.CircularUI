namespace Xamarin.Forms.CircularUI
{
    public class CircleStepper : Xamarin.Forms.Stepper, IRotaryFocusable
    {
        public static readonly BindableProperty MarkerColorProperty = BindableProperty.Create(nameof(MarkerColor), typeof(Color), typeof(CircleStepper), Color.Default);
        public static readonly BindableProperty MarkerLineWidthProperty = BindableProperty.Create(nameof(MarkerLineWidth), typeof(int), typeof(CircleStepper), 23);
        public static readonly BindableProperty LabelFormatProperty = BindableProperty.Create(nameof(LabelFormat), typeof(string), typeof(CircleStepper), null);

        public Color MarkerColor { get => (Color)GetValue(MarkerColorProperty); set => SetValue(MarkerColorProperty, value); }
        public int MarkerLineWidth { get => (int)GetValue(MarkerLineWidthProperty); set => SetValue(MarkerLineWidthProperty, value); }
        public string LabelFormat { get => (string)GetValue(LabelFormatProperty); set => SetValue(LabelFormatProperty, value); }
    }
}
