using System;
using Xamarin.Forms;
using Tizen.Wearable.CircularUI.Forms;

namespace UIComponents.Samples.Popup
{
    public class Text1Button : InformationPopup
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Text1Button()
        {
            Text = @"This is scrollable popup text.
This part is made by adding long text in popup. Popup internally added
scroller to this layout when size of text is greater than total popup
height. This has one button in action area and does not have title area";

            // Create button
            var button = new MenuItem()
            {
                Text = "OK",
                Command = new Command(() =>
                {
                    Console.WriteLine("Bottom botton Command!!");
                    this.Dismiss();
                })
            };

            // Set bottom button and add event handler of button
            BottomButton = button;
            BackButtonPressed += (s, e) => { this.Dismiss(); };
        }
    }
}