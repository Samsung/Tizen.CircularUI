
using Xamarin.Forms;
using Xamarin.Forms.CircularUI;
using Xamarin.Forms.Xaml;

namespace WearableUIGallery.TC
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TCCircleStepper2 : CirclePage
    {
		public TCCircleStepper2()
		{
            InitializeComponent ();
		}

        void OnFocusedHr(object sender, ValueChangedEventArgs args)
        {
            RotaryFocusTargetName = "StepperT1";
        }
        void OnFocusedMm(object sender, ValueChangedEventArgs args)
        {
            RotaryFocusTargetName = "StepperT2";
        }
        void OnFocusedSec(object sender, ValueChangedEventArgs args)
        {
            RotaryFocusTargetName = "StepperT3";
        }

    }
}