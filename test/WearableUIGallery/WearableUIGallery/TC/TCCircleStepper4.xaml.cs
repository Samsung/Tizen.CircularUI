using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using CircularUI;
using Xamarin.Forms.Xaml;

namespace WearableUIGallery.TC
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TCCircleStepper4 : CirclePage
    {
		public TCCircleStepper4()
		{
            InitializeComponent ();
		}

        void OnFocusedUnit(object sender, ValueChangedEventArgs args)
        {
            RotaryFocusTargetName = "StepperUnit";
        }
    }
}