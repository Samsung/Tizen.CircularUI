using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace FlyoutExample
{
    public class RandomColorPage : ContentPage
    {
        Label Title;

        public RandomColorPage()
        {
            Console.WriteLine("Create RandomColorPage");
            var rand = new Random();
            var color = Color.FromRgb(rand.Next(255), rand.Next(255), rand.Next(255));
            BackgroundColor = color;
            Title = new Label();
            Content = new StackLayout
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Children =
                {
                    new Label
                    {
                        Text = $"Color : {color.ToHex()}",
                    },
                    Title,
                }
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Device.BeginInvokeOnMainThread(() =>
            {
                Title.Text = Shell.Current.CurrentState.Location.ToString();
            });
        }
    }
}
