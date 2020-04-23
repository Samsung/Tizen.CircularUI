using System.Linq;
using Xamarin.Forms;
using Xaminals.Models;
using Tizen.Wearable.CircularUI.Forms;

namespace Xaminals.Views
{
    public partial class DogsPage : CirclePage
    {
        public DogsPage()
        {
            InitializeComponent();
        }

        async void OnItemSelected(object sender, ItemTappedEventArgs e)
        {
            string dogName = (e.Item as Animal).Name;
            // This works because route names are unique in this application.
            await Shell.Current.GoToAsync($"dogdetails?name={dogName}");
            // The full route is shown below.
            // await Shell.Current.GoToAsync($"//animals/domestic/dogs/dogdetails?name={dogName}");
        }
    }
}
