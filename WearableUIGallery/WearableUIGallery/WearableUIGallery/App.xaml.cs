using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WearableUIGallery
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class App : Application
    {
        public App()
        {
            Datas = new ObservableCollection<TCDescribe>();
            InitializeComponent();

            Navigation = MainNavigation;
        }

        public static NavigationPage Navigation  { get; private set; }

        public ObservableCollection<TCDescribe> Datas { get; private set; }

        public void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (args.SelectedItem == null) return;

            var desc = args.SelectedItem as TCDescribe;
            if (desc != null && desc.Class != null)
            {
                Type pageType = desc.Class;

                var page = Activator.CreateInstance(pageType) as Page;
                NavigationPage.SetHasNavigationBar(page, false);
                MainNavigation.PushAsync(page as Page);
            }
        }
    }
}