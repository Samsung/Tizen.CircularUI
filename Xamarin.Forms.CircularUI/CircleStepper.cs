namespace Xamarin.Forms.CircularUI
{
    /// <summary>
    /// Extend Xamarin's Stepper with a View that represents the ElmSharp.Wearable.CircleSpinner.
    /// </summary>
    public class CircleStepper : Xamarin.Forms.Stepper, IRotaryFocusable
    {
        /// <summary>
        /// BindableProperty type of Marker color moving in Bezel Action
        /// </summary>
        public static readonly BindableProperty MarkerColorProperty = BindableProperty.Create(nameof(MarkerColor), typeof(Color), typeof(CircleStepper), Color.Default);
        /// <summary>
        /// BindableProperty type of Marker's length moving in Bezel Action
        /// </summary>
        public static readonly BindableProperty MarkerLineWidthProperty = BindableProperty.Create(nameof(MarkerLineWidth), typeof(int), typeof(CircleStepper), 23);
        /// <summary>
        /// BindableProperty type of the format in which the Value is shown
        /// </summary>
        public static readonly BindableProperty LabelFormatProperty = BindableProperty.Create(nameof(LabelFormat), typeof(string), typeof(CircleStepper), null);

        /// <summary>
        /// Gets or sets Marker color moving in Bezel Action
        /// </summary>
        public Color MarkerColor { get => (Color)GetValue(MarkerColorProperty); set => SetValue(MarkerColorProperty, value); }
        /// <summary>
        /// Gets or sets length of the Marker moving in Bezel Action
        /// </summary>
        public int MarkerLineWidth { get => (int)GetValue(MarkerLineWidthProperty); set => SetValue(MarkerLineWidthProperty, value); }
        /// <summary>
        /// Gets or sets format in which Value is shown
        /// </summary>
        public string LabelFormat { get => (string)GetValue(LabelFormatProperty); set => SetValue(LabelFormatProperty, value); }
    }
}
