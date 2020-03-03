using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WearableUIGallery
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : Shell
    {
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
                    foreach(var subTC in tc.Class)
                    {
                        var desc = subTC as TCDescribe;
                        var route = tc.Title + "/" + desc.Title;
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
    }
}