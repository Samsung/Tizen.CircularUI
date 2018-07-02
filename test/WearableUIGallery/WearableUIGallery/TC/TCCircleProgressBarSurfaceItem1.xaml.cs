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
    public partial class TCCircleProgressBarSurfaceItem1 : CirclePage
    {
        bool _timerFinished;
        double _increment = 0.02;

        public TCCircleProgressBarSurfaceItem1 ()
        {
            InitializeComponent ();

            ProgressBar.Value = 0;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _timerFinished = false;
            Device.StartTimer(TimeSpan.FromMilliseconds(200), () =>
            {
                ProgressBar.Value += _increment;
                if (ProgressBar.Value == 0 || ProgressBar.Value == 1) _increment *= -1;
                return !_timerFinished;
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _timerFinished = true;
        }
    }
}