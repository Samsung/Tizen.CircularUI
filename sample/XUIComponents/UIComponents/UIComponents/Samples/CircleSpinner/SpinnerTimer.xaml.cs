using Xamarin.Forms;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms.Xaml;

namespace UIComponents.Samples.CircleSpinner
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SpinnerTimer : CirclePage
    {
        public SpinnerTimer()
        {
            InitializeComponent();
        }

        void OnFocusedHr(object sender, ValueChangedEventArgs args)
        {
            RotaryFocusObject = StepperHr3;
        }
        void OnFocusedMm(object sender, ValueChangedEventArgs args)
        {
            RotaryFocusObject = StepperMm3;
        }
        void OnFocusedSec(object sender, ValueChangedEventArgs args)
        {
            RotaryFocusObject = StepperSec3;
        }
    }
}
