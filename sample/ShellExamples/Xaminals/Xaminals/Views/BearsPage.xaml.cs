using Xamarin.Forms;
using Xaminals.Models;
using Xaminals.ViewModels;
using Tizen.Wearable.CircularUI.Forms;

namespace Xaminals.Views
{
    public partial class BearsPage : CirclePage
    {
        public BearsPage()
        {
            InitializeComponent();
            BindingContext = new BearsViewModel();
        }

        async void OnItemSelected(object sender, ItemTappedEventArgs e)
        {
            string bearName = (e.Item as Animal).Name;
            // This works because route names are unique in this application.
            await Shell.Current.GoToAsync($"beardetails?name={bearName}");
            // The full route is shown below.
            // await Shell.Current.GoToAsync($"//animals/bears/beardetails?name={bearName}");
        }
    }
}
