using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UIComponents
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class App : Application
    {
        public App ()
        {
            InitializeComponent ();
        }

        public void OnItemTapped(object sender, ItemTappedEventArgs args)
        {
            if (args.Item == null) return;

            var dest = args.Item as Sample;
            if (dest != null && dest.Class != null)
            {
                Type pageType = dest.Class;

                var page = Activator.CreateInstance(pageType) as Page;
                NavigationPage.SetHasNavigationBar(page, false);
                MainNavigation.PushAsync(page as Page);
            }
        }
    }
}