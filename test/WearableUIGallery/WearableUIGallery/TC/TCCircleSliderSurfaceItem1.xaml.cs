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
    public partial class TCCircleSliderSurfaceItem1 : CirclePage
    {

        public TCCircleSliderSurfaceItem1()
        {
            InitializeComponent ();
        }

        void OnClick(object sender, EventArgs args)
        {
            var btn = sender as Button;
            if (btn.Text.Equals("Enable"))
            {
                slider.Value = 12;
                slider.IsEnabled = true;
                btn.Text = "Disable";
            }
            else
            {
                slider.BarAngle = 60;
                slider.IsEnabled = false;
                btn.Text = "Enable";
            }
        }
    }
}