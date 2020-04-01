using System;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WearableUIGallery.TC
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContentButtonTestPage : ContentPage
    {
        public ContentButtonTestPage()
        {
            InitializeComponent();

            ClickCommand = new Command(execute: () =>
            {
                label.Text = "clicked";
            });
        }

        public ICommand ClickCommand { get; private set; }

        private void OnButtonClicked(object sender, EventArgs e)
        {
            Console.WriteLine($"ContentButton clicked event is invoked!!");
        }

        private void OnButtonPressed(object sender, EventArgs e)
        {
            Xamarin.Forms.PlatformConfiguration.TizenSpecific.Image.SetBlendColor(buttonBg, Color.Gray);
        }

        private void OnButtonReleased(object sender, EventArgs e)
        {
            Xamarin.Forms.PlatformConfiguration.TizenSpecific.Image.SetBlendColor(buttonBg, Color.Transparent);
        }
    }
}