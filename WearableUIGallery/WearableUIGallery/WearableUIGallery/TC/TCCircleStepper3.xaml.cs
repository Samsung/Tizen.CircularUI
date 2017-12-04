using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.CircularUI;
using Xamarin.Forms.Xaml;

namespace WearableUIGallery.TC
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TCCircleStepper3 : CirclePage
    {
		public TCCircleStepper3()
		{
            InitializeComponent ();
		}

        void OnFocusedHr(object sender, ValueChangedEventArgs args)
        {
            RotaryFocusTargetName = "StepperHr3";
        }
        void OnFocusedMm(object sender, ValueChangedEventArgs args)
        {
            RotaryFocusTargetName = "StepperMm3";
        }
        void OnFocusedSec(object sender, ValueChangedEventArgs args)
        {
            RotaryFocusTargetName = "StepperSec3";
        }

    }
}