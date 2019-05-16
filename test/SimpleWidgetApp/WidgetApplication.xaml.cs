using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SimpleWidgetApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WidgetApplication : Application
    {
        public WidgetApplication()
        {
            InitializeComponent();

            MainPage = new SimpleWidgetApp.MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

