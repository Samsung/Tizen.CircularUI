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
	public partial class TCCircleStepper : CirclePage
    {
		public TCCircleStepper()
		{
            InitializeComponent ();
		}

        void OnFocusedHr(object sender, ValueChangedEventArgs args)
        {
            RotaryFocusTargetName = "StepperHr";
        }
        void OnFocusedMm(object sender, ValueChangedEventArgs args)
        {
            RotaryFocusTargetName = "StepperMm";
        }
        void OnFocusedSec(object sender, ValueChangedEventArgs args)
        {
            RotaryFocusTargetName = "StepperSec";
        }

    }
}