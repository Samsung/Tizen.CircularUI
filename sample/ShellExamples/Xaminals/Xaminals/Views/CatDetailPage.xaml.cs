using System;
using System.Linq;
using Xamarin.Forms;
using Xaminals.Data;
using Tizen.Wearable.CircularUI.Forms;

namespace Xaminals.Views
{
    [QueryProperty("Name", "name")]
    public partial class CatDetailPage : CirclePage
    {
        public string Name
        {
            set
            {
                BindingContext = CatData.Cats.FirstOrDefault(m => m.Name == Uri.UnescapeDataString(value));
            }
        }

        public CatDetailPage()
        {
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            return base.OnBackButtonPressed();
        }
    }
}
