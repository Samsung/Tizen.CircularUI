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

        public void OnSoundSelected(object sender, SelectedEventArgs args)
        {
            Console.WriteLine($"OnSoundSelected!! value:{args.Value}");
            if(args.Value)label.Text = "Selected : Sound";
        }

        public void OnVibrateSelected(object sender, SelectedEventArgs args)
        {
            Console.WriteLine($"OnVibrateSelected!! value:{args.Value}");
            if (args.Value) label.Text = "Selected : Vibrate";
        }

        public void OnMuteSelected(object sender, SelectedEventArgs args)
        {
            Console.WriteLine($"OnMuteSelected!! value:{args.Value}");
            if (args.Value) label.Text = "Selected : Mute";
        }
    }
}