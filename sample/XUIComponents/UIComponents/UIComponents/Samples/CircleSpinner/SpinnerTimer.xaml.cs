using Xamarin.Forms;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms.Xaml;

namespace UIComponents.Samples.CircleSpinner
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SpinnerTimer : CirclePage
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public SpinnerTimer()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Called when is focused on Hr spinner
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="args">Argument of ValueChangedEventArgs</param>
        void OnFocusedHr(object sender, ValueChangedEventArgs args)
        {
            RotaryFocusObject = StepperHr3;
        }
        /// <summary>
        /// Called when is focused on Mm spinner
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="args">Argument of ValueChangedEventArgs</param>
        void OnFocusedMm(object sender, ValueChangedEventArgs args)
        {
            RotaryFocusObject = StepperMm3;
        }
        /// <summary>
        /// Called when is focused on Sec spinner
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="args">Argument of ValueChangedEventArgs</param>
        void OnFocusedSec(object sender, ValueChangedEventArgs args)
        {
            RotaryFocusObject = StepperSec3;
        }
    }
}
