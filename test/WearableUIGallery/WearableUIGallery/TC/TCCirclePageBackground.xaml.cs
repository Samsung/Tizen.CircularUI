using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Tizen.Wearable.CircularUI.Forms;

namespace WearableUIGallery.TC
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TCCirclePageBackground : CirclePage
    {
        ImageSource _source = ImageSource.FromFile("image/stars_background.png");
        public TCCirclePageBackground()
        {
            BackgroundImageSource = _source;
            InitializeComponent();
        }

        private void OnUpdateBGImage(object sender, EventArgs e)
        {
            if (BackgroundImageSource == _source)
            {
                BackgroundImageSource = ImageSource.FromFile("image/100_1.jpg");
            }
            else
            {
                BackgroundImageSource = _source;
            }
        }

        private void OnUpdateBGColor(object sender, EventArgs e)
        {
            var rnd = new Random();
            BackgroundColor = Color.FromRgb(rnd.Next(255), rnd.Next(255), rnd.Next(255));
        }

        private void OnRemoveBGImage(object sender, EventArgs e)
        {
            BackgroundImageSource = null;
        }
    }
}