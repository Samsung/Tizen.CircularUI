using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms.Xaml;

namespace UIComponents.Samples
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BackgroundList : CirclePage
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public BackgroundList()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Called when item is tapped
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="args">Argument of ItemTappedEventArgs</param>
        public void OnItemTapped(object sender, ItemTappedEventArgs args)
        {
            if (args.Item == null) return;

            var desc = args.Item as Sample;
            Console.WriteLine($"OnItemTapped desc.Class:{desc.Class}");
            if (desc != null && desc.Class != null)
            {
                Type pageType = desc.Class;

                // Create page and push it to navigation stack
                var page = Activator.CreateInstance(pageType) as Page;
                NavigationPage.SetHasNavigationBar(page, false);
                Navigation.PushAsync(page as Page);
            }
        }
    }
}
