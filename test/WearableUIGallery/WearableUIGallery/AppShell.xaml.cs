using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WearableUIGallery
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : Shell
    {
        public string CurrentUri { get; set; }

        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("TCSubListPage", typeof(TCSubListPage));

            foreach(var tc in TCData.TCs)
            {
                if (!tc.IsGroup)
                {
                    Routing.RegisterRoute(tc.Title, tc.Class);
                }
                else
                {
                    foreach (var subTC in tc.Class)
                    {
                        var desc = subTC as TCDescribe;
                        var route = tc.Title + "_" + desc.Title;
                        Routing.RegisterRoute(route, desc.Class);
                    }
                }
            }
        }

        protected override bool OnBackButtonPressed()
        {
            if (FlyoutIsPresented)
            {
                FlyoutIsPresented = false;
                return true;
            }
            return base.OnBackButtonPressed();
        }

        void OnNavigated(object sender, ShellNavigatedEventArgs arg)
        {
            if(arg.Current != null)
                CurrentUri = arg.Current.Location.ToString();
        }
    }
}