using System;
using Xamarin.Forms;
using Tizen.Wearable.CircularUI.Forms;
using WCheck = Tizen.Wearable.CircularUI.Forms.Check;

namespace UIComponents.Samples.Popup
{
    public class TitleTextCheckButton : TwoButtonPopup
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public TitleTextCheckButton()
        {
            Title = "Popup title";

            // Initialize FirstButton
            FirstButton = new MenuItem()
            {
                // Set icon
                Icon = new FileImageSource
                {
                    File = "b_option_list_icon_share.png",
                },
                //Set command
                Command = new Command(() =>
                {
                    Console.WriteLine("left button1 Command!!");
                    this.Dismiss();
                })
            };

            // Initialize SecondButton
            SecondButton = new MenuItem()
            {
                // Set icon
                Icon = new FileImageSource
                {
                    File = "b_option_list_icon_delete.png",
                },
                //Set command
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

            // Add label and checkbox with label
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

            // Add event handler
            BackButtonPressed += (s, e) => { this.Dismiss(); };
        }
    }
}