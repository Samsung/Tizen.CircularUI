using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.CircularUI;

namespace WearableUIGallery.TC
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TCTwoButtonPopup : ContentPage
    {
        TwoButtonPopup _popUp1 = null;
        TwoButtonPopup _popUp2 = null;
        public TCTwoButtonPopup()
        {
            InitializeComponent();

            var leftButton = new MenuItem()
            {
                Text = "Share",
                Icon = new FileImageSource
                {
                    File = "image/b_option_list_icon_share.png",
                },
                Command = new Command(() =>
                {
                    Console.WriteLine("left button1 Command!!");
                })
            };

            var rightButton = new MenuItem()
            {
                Text = "Delete",
                Icon = new FileImageSource
                {
                    File = "image/b_option_list_icon_delete.png",
                },
                Command = new Command(() =>
                {
                    Console.WriteLine("right button1 Command!!");
                })
            };

            var checkbox = new Check
            {
                DisplayStyle = CheckDisplayStyle.Small
            };

            checkbox.Toggled += (s, e) =>
            {
                Console.WriteLine($"checkbox toggled. checkbox.IsToggled:{checkbox.IsToggled}");
            };

            _popUp1 = new TwoButtonPopup();
            _popUp1.FirstButton = leftButton;
            _popUp1.SecondButton = rightButton;
            _popUp1.Title = "Popup title";
            _popUp1.Content = new StackLayout()
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
                        Padding = new Thickness(0, 30, 0, 30),
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

            _popUp1.BackButtonPressed += (s, e) =>
            {
                _popUp1.Dismiss();
                label1.Text = "Popup1 is dismissed";
            };

            var leftButton2 = new MenuItem()
            {
                Text = "OK",
                Icon = new FileImageSource
                {
                    File = "image/tw_ic_popup_btn_check.png",
                },
                Command = new Command(() =>
                {
                    Console.WriteLine("left button2 Command!!");
                })
            };

            var rightButton2 = new MenuItem()
            {
                Text = "Cancle",
                Icon = new FileImageSource
                {
                    File = "image/tw_ic_popup_btn_delete.png",
                },
                Command = new Command(() =>
                {
                    Console.WriteLine("right button2 Command!!");
                })
            };

            _popUp2 = new TwoButtonPopup();
            _popUp2.FirstButton = leftButton2;
            _popUp2.SecondButton = rightButton2;
            _popUp2.Title = "Popup title";
            _popUp2.Text = @"This is scrollable popup text.
This part is made by adding long text in popup. Popup internally added
scroller to this layout when size of text is greater than total popup
height. This has two button in action area and title text in title area";

            _popUp2.BackButtonPressed += (s, e) =>
            {
                _popUp2.Dismiss();
                label1.Text = "Popup2 is dismissed";
            };

            _popUp1.FirstButton.Clicked += (s, e) => Console.WriteLine("First(share) button clicked!");
            _popUp1.SecondButton.Clicked += (s, e) => Console.WriteLine("Second(delete) button clicked!");

        }


        private void OnButton1Clicked(object sender, EventArgs e)
        {
            _popUp1.Show();
        }

        private void OnButton2Clicked(object sender, EventArgs e)
        {
            _popUp2.Show();
        }
    }
}