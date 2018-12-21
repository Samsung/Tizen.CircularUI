using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WearableUIGallery
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TCSubListPage : CirclePage
    {
		public TCSubListPage()
		{
			InitializeComponent ();
		}

        public void OnItemTapped(object sender, ItemTappedEventArgs args)
        {
            if (args.Item == null) return;

            var desc = args.Item as TCDescribe;
            if (desc != null && desc.Class != null)
            {
                Page page;
                if (desc.Class.Count == 1)
                {
                    Type pageType = desc.Class;
                    page = Activator.CreateInstance(pageType) as Page;
                }
                else
                {
                    var types = desc.Class;
                    page = new TCSubListPage();
                    page.BindingContext = types;
                }
                NavigationPage.SetHasNavigationBar(page, false);
                App.MainNavigation.PushAsync(page);
            }
        }
    }

}