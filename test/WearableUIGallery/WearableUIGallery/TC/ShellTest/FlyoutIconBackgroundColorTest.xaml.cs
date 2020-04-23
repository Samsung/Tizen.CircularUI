using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Tizen.Wearable.CircularUI.Forms;

namespace WearableUIGallery.TC
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FlyoutIconBackgroundColorTest : CirclePage
    {
        public FlyoutIconBackgroundColorTest ()
        {
            InitializeComponent ();
        }

        void Button_Clicked(object sender, EventArgs e)
        {
            CircularShell.SetFlyoutIconBackgroundColor(Shell.Current, Color.Default);
        }

        void Button_Clicked_1(object sender, EventArgs e)
        {
            CircularShell.SetFlyoutIconBackgroundColor(Shell.Current, (sender as Button).BackgroundColor);
        }

        void Button_Clicked_2(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//Main/TestRun/MainPage");
        }
    }
}