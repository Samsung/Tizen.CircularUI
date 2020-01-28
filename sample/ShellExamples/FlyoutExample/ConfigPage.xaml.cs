using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}