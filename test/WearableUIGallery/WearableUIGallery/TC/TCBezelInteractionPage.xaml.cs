using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Tizen.Wearable.CircularUI.Forms;

namespace WearableUIGallery.TC
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TCBezelInteractionPage : BezelInteractionPage
    {
        TapGestureRecognizer _universeClicked;
        TapGestureRecognizer _spacemanClicked;
        TapGestureRecognizer _sliderClicked;

        public TCBezelInteractionPage()
        {
            InitializeComponent();
            var universe = new RotaryFocusProxy(Universe);

            _universeClicked = new TapGestureRecognizer();
            _universeClicked.Command = new Command(() => RotaryFocusObject = universe);

            _spacemanClicked = new TapGestureRecognizer();
            _spacemanClicked.Command = new Command(() => RotaryFocusObject = Spaceman);

            _sliderClicked = new TapGestureRecognizer
            {
                Command = new Command(() => RotaryFocusObject = Slider)
            };

            Universe.GestureRecognizers.Add(_universeClicked);
            Spaceman.GestureRecognizers.Add(_spacemanClicked);
            SliderTarget.GestureRecognizers.Add(_sliderClicked);

        }
    }
}