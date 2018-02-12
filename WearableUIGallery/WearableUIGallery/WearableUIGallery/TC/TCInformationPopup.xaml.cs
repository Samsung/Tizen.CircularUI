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
    public partial class TCInformationPopup : ContentPage
    {
        InformationPopup _textPopUp = null;
        InformationPopup _textButtonPopUp = null;
        InformationPopup _progressPopUp = null;

        public TCInformationPopup()
        {
            InitializeComponent();

            _textPopUp = new InformationPopup();
            _textPopUp.Text = "This is text popup test";

            _textPopUp.BackButtonPressed += (s, e) =>
            {
                _textPopUp.Dismiss();
                label1.Text = "text popup is dismissed";
            };

            var bottomButton = new MenuItem()
            {
                Text = "OK",
                Command = new Command(() =>
                {
                    Console.WriteLine("bottom button Command!!");
                    _textButtonPopUp.Dismiss();
                })
            };

            _textButtonPopUp = new InformationPopup();
            _textButtonPopUp.BottomButton = bottomButton;
            _textButtonPopUp.Title = "Popup title";
            _textButtonPopUp.Text = "This is text and button popup test";
            _textButtonPopUp.BottomButton = bottomButton;

            _textButtonPopUp.BackButtonPressed += (s, e) =>
            {
                _textButtonPopUp.Dismiss();
                label1.Text = "text&button is dismissed";
            };

            _textButtonPopUp.BottomButton.Clicked += (s, e) =>
            {
                _textButtonPopUp.Dismiss();
                label1.Text = "text&button is dismissed";
            };

            _progressPopUp = new InformationPopup();
            _progressPopUp.Title = "Popup title";
            _progressPopUp.Text = "This is progress test";
            _progressPopUp.IsProgressRunning = true;

            _progressPopUp.BackButtonPressed += (s, e) =>
            {
                _progressPopUp.Dismiss();
                label1.Text = "progress is dismissed";
            };

        }


        private void OnButton1Clicked(object sender, EventArgs e)
        {
            _textPopUp.Show();
        }

        private void OnButton2Clicked(object sender, EventArgs e)
        {
            _textButtonPopUp.Show();
        }

        private void OnButton3Clicked(object sender, EventArgs e)
        {
            _progressPopUp.Show();
        }
    }
}