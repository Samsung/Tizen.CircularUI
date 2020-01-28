using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Tizen.Wearable.CircularUI.Forms;

namespace FlyoutExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : CircularShell
    {
        public AppShell()
        {
            InitializeComponent();
            BindingContext = this;
        }


        public Command OnMenu1 => new Command(() =>
        {
            DisplayAlert("menu", "Menu1 clicked", "Ok");
        });

        public Command OnMenu2 => new Command(() =>
        {
            DisplayAlert("menu", "Menu2 clicked", "Ok");
        });
    }
}