using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Tizen.Wearable.CircularUI.Forms;

namespace WearableUIGallery.TC
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FlyoutBackgroundColorTest : CirclePage
	{
		public FlyoutBackgroundColorTest ()
		{
			InitializeComponent ();
		}

        void Button_Clicked(object sender, EventArgs e)
        {
            Shell.Current.FlyoutBackgroundColor = Color.Default;
        }

        void Button_Clicked_1(object sender, EventArgs e)
        {
            Shell.Current.FlyoutBackgroundColor = (sender as Button).BackgroundColor;
        }

        void Button_Open(object sender, EventArgs e)
        {
            Shell.Current.FlyoutIsPresented = true;
        }

        void Button_Clicked_2(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//Main/TestRun/MainPage");
        }
    }
}