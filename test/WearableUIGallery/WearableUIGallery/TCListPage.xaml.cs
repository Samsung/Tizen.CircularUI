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
	public partial class TCListPage : CirclePage
    {
		public TCListPage ()
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
                    page = new TCListPage();
                    page.BindingContext = types;
                }
                NavigationPage.SetHasNavigationBar(page, false);
                App.MainNavigation.PushAsync(page);
            }
        }
    }

    class DetailTextConveter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return "";

            var count = (int)value;
            return count > 1 ? $"{count} TC" : "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}