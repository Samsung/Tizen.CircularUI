using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Tizen.Wearable.CircularUI.Forms;

namespace WearableUIGallery.TC
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FlyoutForegroundColorTest : CirclePage
    {
        public FlyoutForegroundColorTest ()
        {
            InitializeComponent ();
        }

        void Button_Clicked(object sender, EventArgs e)
        {
            CircularShell.SetFlyoutForegroundColor(Shell.Current, Color.Default);
        }

        void Button_Clicked_1(object sender, EventArgs e)
        {
            CircularShell.SetFlyoutForegroundColor(Shell.Current, (sender as Button).BackgroundColor);
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