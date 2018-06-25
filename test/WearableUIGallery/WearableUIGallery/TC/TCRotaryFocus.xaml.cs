using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WearableUIGallery.TC
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TCRotaryFocus : CirclePage
	{
        TapGestureRecognizer _universeClicked;
        TapGestureRecognizer _spacemanClicked;
        TapGestureRecognizer _sliderClicked;

        public TCRotaryFocus ()
		{
			InitializeComponent ();

            var universe = new RotaryFocusProxy(Universe);

            RotaryFocusObject = universe;

            _universeClicked = new TapGestureRecognizer();
            _universeClicked.Command = new Command(() => RotaryFocusObject = universe);

            _spacemanClicked = new TapGestureRecognizer();
            _spacemanClicked.Command = new Command(() => RotaryFocusTargetName = "Spaceman");

            _sliderClicked = new TapGestureRecognizer()
            {
                Command = new Command(() => RotaryFocusTargetName = "Slider")
            };

            Universe.GestureRecognizers.Add(_universeClicked);
            Spaceman.GestureRecognizers.Add(_spacemanClicked);
            SliderTarget.GestureRecognizers.Add(_sliderClicked);
		}
	}

    class RotaryFocusImage : Image, IRotaryEventReceiver
    {
        public void Rotate(RotaryEventArgs args) => this.RelRotateTo(args.IsClockwise ? 30 : -30);
    }

    class RotaryFocusProxy : IRotaryEventReceiver
    {
        Image _img;
        public RotaryFocusProxy(Image img)
        {
            _img = img;
        }

        public void Rotate(RotaryEventArgs args) => _img.RelRotateTo(args.IsClockwise ? 30 : -30);
    }
}