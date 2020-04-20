using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Tizen.Wearable.CircularUI.Forms;

namespace WearableUIGallery.TC
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FlyoutIconTest : CirclePage
    {
        public FlyoutIconTest ()
        {
            InitializeComponent ();
        }

        void Button_Clicked(object sender, EventArgs e)
        {
            Shell.Current.FlyoutIcon = null;
        }

        void Button_Clicked_1(object sender, EventArgs e)
        {
            Shell.Current.FlyoutIcon = "favorite.png";
        }

        void Button_Clicked_2(object sender, EventArgs e)
        {
            Shell.Current.FlyoutIcon = "home.png";
        }

        void Button_Clicked_3(object sender, EventArgs e)
        {
            Shell.Current.FlyoutIcon = "play.png";
        }

        void Button_Clicked_4(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//Main/TestRun/MainPage");
        }
    }
}