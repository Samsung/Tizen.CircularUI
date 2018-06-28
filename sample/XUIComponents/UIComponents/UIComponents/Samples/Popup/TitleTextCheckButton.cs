using System;
using Xamarin.Forms;
using Tizen.Wearable.CircularUI.Forms;
using WCheck = Tizen.Wearable.CircularUI.Forms.Check;

namespace UIComponents.Samples.Popup
{
    public class TitleTextCheckButton : TwoButtonPopup
    {
        public TitleTextCheckButton()
        {
            Title = "Popup title";

            FirstButton = new MenuItem()
            {
                Icon = new FileImageSource
                {
                    File = "b_option_list_icon_share.png",
                },
                Command = new Command(() =>
                {
                    Console.WriteLine("left button1 Command!!");
                    this.Dismiss();
                })
            };

            SecondButton = new MenuItem()
            {
                Icon = new FileImageSource
                {
                    File = "b_option_list_icon_delete.png",
                },
                Command = new Command(() =>
                {
                    Console.WriteLine("right button1 Command!!");
                    this.Dismiss();
                })
            };

            var checkbox = new WCheck
            {
                DisplayStyle = CheckDisplayStyle.Small
            };

            checkbox.Toggled += (s, e) =>
            {
                Console.WriteLine($"checkbox toggled. checkbox.IsToggled:{checkbox.IsToggled}");
            };

            Content = new StackLayout()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    new Label
                    {
                        Text = "Will be saved, and sound, only on the Gear.",
                    },
                    new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        Padding = new Thickness(0, 40, 0, 40),
                        Children =
                        {
                            checkbox,
                            new Label
                            {
                                Text = "Do not repeat",
                            }
                        }
                    }
                }
            };

            BackButtonPressed += (s, e) => { this.Dismiss(); };
        }
    }
}