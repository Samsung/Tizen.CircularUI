using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CircularUI;

namespace WearableUIGallery.TC
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TCToast : ContentPage
    {
        public TCToast()
        {
            InitializeComponent();
        }

        private void OnButton1Clicked(object sender, EventArgs e)
        {
            Toast.DisplayText("Toast popup", 3000);
        }

        private void OnButton2Clicked(object sender, EventArgs e)
        {
            Toast.DisplayIconText("Toast popup2", new FileImageSource { File = "image/tw_ic_popup_btn_check.png" }, 2000);
        }
    }
}