using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.CircularUI;

namespace WearableUIExtGallery.TC
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TCToast : ContentPage
    {
        public TCToast()
		{
            InitializeComponent();
        }

        private void OnLongToastClicked(object sender, EventArgs e)
        {
            DependencyService.Get<IToast>().LongToast("This is a toast that is shown for 3.5s This is a toast that is shown for 3.5s This is a toast that is shown for 3.5s This is a toast that is shown for 3.5sThis is a toast that is shown for 3.5sThis is a toast that is shown for 3.5s This is a toast that is shown for 3.5s");
        }
        private void OnShortToastClicked(object sender, EventArgs e)
        {
            DependencyService.Get<IToast>().ShortToast("This is a toast that is shown for 2s");
        }
    }
}