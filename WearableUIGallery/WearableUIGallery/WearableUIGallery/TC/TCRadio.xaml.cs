using System;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.CircularUI;

namespace WearableUIGallery.TC
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TCRadio : CirclePage
    {
        public TCRadio()
        {
            InitializeComponent();
        }

        public void OnSelected(object sender, SelectedEventArgs args)
        {
            Console.WriteLine($"OnSoundSelected!! value:{args.Value}");
            Radio radio = sender as Radio;
            if (radio != null)
            {
                if (args.Value) label.Text = "Selected : " + radio.Value;
            }
        }
    }
}