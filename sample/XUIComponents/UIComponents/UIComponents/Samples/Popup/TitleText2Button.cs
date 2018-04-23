using System;
using Xamarin.Forms;
using Tizen.Wearable.CircularUI.Forms;

namespace UIComponents.Samples.Popup
{
    public class TitleText2Button : ConfirmationPopup
    {
        public TitleText2Button()
        {
            Title = "Popup title";
            Text = @"This is scrollable popup text.
This part is made by adding long text in popup. Popup internally added
scroller to this layout when size of text is greater than total popup
height. This has two button in action area and title text in title area";

            FirstButton = new MenuItem()
            {
                Icon = new FileImageSource
                {
                    File = "tw_ic_popup_btn_delete.png",
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
                    File = "tw_ic_popup_btn_check.png",
                },
                Command = new Command(() =>
                {
                    Console.WriteLine("right button1 Command!!");
                    this.Dismiss();
                })
            };

            BackButtonPressed += (s, e) => { this.Dismiss(); };
        }
    }
}