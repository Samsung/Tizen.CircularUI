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
	public partial class TCIRotaryEventReceiver : CirclePage, IRotaryEventReceiver
	{
        bool _rotating;
        double _angle;
		public TCIRotaryEventReceiver ()
		{
			InitializeComponent ();
            _angle = 0;
		}

        public void Rotate(RotaryEventArgs args)
        {
            if (_rotating) return;

            _rotating = true;
            _angle += args.IsClockwise ? 30 : -30;
            Cat.RotateTo(_angle).ContinueWith(
                (b) =>
                {
                    _rotating = false;
                    if (_angle == 360)
                    {
                        Cat.Rotation = 0;
                        _angle = 0;
                    }
                });
        }
    }
}