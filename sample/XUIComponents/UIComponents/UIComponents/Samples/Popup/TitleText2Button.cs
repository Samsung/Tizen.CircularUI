using System;
using Xamarin.Forms;
using Tizen.Wearable.CircularUI.Forms;

namespace UIComponents.Samples.Popup
{
    public class TitleText2Button : TwoButtonPopup
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public TitleText2Button()
        {
            Title = "Popup title";
            Text = @"This is scrollable popup text.
This part is made by adding long text in popup. Popup internally added
scroller to this layout when size of text is greater than total popup
height. This has two button in action area and title text in title area";

            // Initialize FirstButton
            FirstButton = new MenuItem()
            {
                // Set icon
                Icon = new FileImageSource
                {
                    File = "tw_ic_popup_btn_delete.png",
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
                    File = "tw_ic_popup_btn_check.png",
                },
                //Set command
                Command = new Command(() =>
                {
                    Console.WriteLine("right button1 Command!!");
                    this.Dismiss();
                })
            };

            // Add event handler
            BackButtonPressed += (s, e) => { this.Dismiss(); };
        }
    }
}