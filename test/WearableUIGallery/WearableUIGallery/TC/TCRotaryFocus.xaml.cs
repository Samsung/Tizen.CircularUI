/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://floralicense.org/license/
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

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