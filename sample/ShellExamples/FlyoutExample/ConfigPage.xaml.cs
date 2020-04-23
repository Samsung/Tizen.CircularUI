using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FlyoutExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConfigPage : ContentPage
    {
        ImageSource _imgSource1 = ImageSource.FromFile("home.png");
        ImageSource _imgSource2 = ImageSource.FromFile("play.png");
        public ConfigPage ()
        {
            InitializeComponent();
            FlyoutSwitch.IsToggled = Shell.Current.FlyoutBehavior == FlyoutBehavior.Flyout;
        }

        void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            Shell.Current.FlyoutBehavior = e.Value ? FlyoutBehavior.Flyout : FlyoutBehavior.Disabled;
        }

        void Button_Clicked(object sender, EventArgs e)
        {
            if (Shell.Current.FlyoutIcon != _imgSource1)
            {
                Shell.Current.FlyoutIcon = _imgSource1;
            }
            else
            {
                Shell.Current.FlyoutIcon = _imgSource2;
            }
        }

        void Button_Clicked_1(object sender, EventArgs e)
        {
            Shell.Current.FlyoutIcon = null;
        }

        void Button_Clicked_2(object sender, EventArgs e)
        {
            Random rnd = new Random();
            Shell.Current.FlyoutBackgroundColor = Color.FromRgb(rnd.Next(255), rnd.Next(255), rnd.Next(255));
        }

        void Button_Clicked_3(object sender, EventArgs e)
        {
            Random rnd = new Random();
            Tizen.Wearable.CircularUI.Forms.CircularShell.SetFlyoutIconBackgroundColor(Shell.Current, Color.FromRgb(rnd.Next(255), rnd.Next(255), rnd.Next(255)));
        }

        void Button_Clicked_4(object sender, EventArgs e)
        {
            Random rnd = new Random();
            Tizen.Wearable.CircularUI.Forms.CircularShell.SetFlyoutForegroundColor(Shell.Current, Color.FromRgb(rnd.Next(255), rnd.Next(255), rnd.Next(255)));
        }

        void Button_Clicked_5(object sender, EventArgs e)
        {
            Shell.Current.CurrentItem.Title = "UpdatedTitle";
            ShellModel.Instance.MainTitle = "MainUpdated";
        }
    }
}