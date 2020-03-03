using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WearableUIGallery
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RandomColorPage : ContentPage
    {
        public RandomColorPage ()
        {
            InitializeComponent();
            var rand = new Random();
            var color = Color.FromRgb(rand.Next(255), rand.Next(255), rand.Next(255));
            BackgroundColor = color;
            label.Text = color.ToHex();
        }
    }
}