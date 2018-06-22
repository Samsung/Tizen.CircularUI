using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WearableUIGallery.TC
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TCActionButton : CirclePage
	{
		public TCActionButton ()
		{
			InitializeComponent ();
		}

        void OnClickEnable(object sender, EventArgs args)
        {
            var btn = sender as Button;

            if (btn.Text == "Enable")
            {
                btn.Text = "Disable";
                ActionButton.IsEnable = false;
            }
            else
            {
                btn.Text = "Enable";
                ActionButton.IsEnable = true;
            }
        }

        void OnClickVisible(object sender, EventArgs args)
        {
            var btn = sender as Button;

            if (btn.Text == "Invisible")
            {
                btn.Text = "Visible";
                ActionButton.IsVisible = true;
            }
            else
            {
                btn.Text = "Invisible";
                ActionButton.IsVisible = false;
            }
        }

    }
}