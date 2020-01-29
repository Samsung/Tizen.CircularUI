using System;
using System.Windows.Input;
using Xamarin.Forms;
using Tizen.Wearable.CircularUI.Forms;
namespace Xaminals.Views
{
    public partial class AboutPage : CirclePage
    {
        public ICommand TapCommand => new Command<string>((url) => { /*Device.OpenUri(new Uri(url))*/});

        public AboutPage()
        {
            InitializeComponent();
            BindingContext = this;
        }
    }
}
