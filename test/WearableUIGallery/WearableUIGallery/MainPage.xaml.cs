using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Tizen.Wearable.CircularUI.Forms;
using System;

namespace WearableUIGallery
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : CirclePage
    {
        public MainPage ()
        {
            InitializeComponent();
        }

        void Button_Clicked(object sender, System.EventArgs e)
        {
            Shell.Current.GoToAsync(TCNameEntry.Text);
        }

        void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            Shell.Current.FlyoutBehavior = e.Value ? FlyoutBehavior.Flyout : FlyoutBehavior.Disabled;
        }

    }
}