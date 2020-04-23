using System.Linq;
using Xamarin.Forms;
using Xaminals.Models;
using Tizen.Wearable.CircularUI.Forms;

namespace Xaminals.Views
{
    public partial class CatsPage : CirclePage
    {
        public CatsPage()
        {
            InitializeComponent();
        }

        async void OnItemSelected(object sender, ItemTappedEventArgs e)
        {
            string catName = (e.Item as Animal).Name;
            // This works because route names are unique in this application.
            await Shell.Current.GoToAsync($"catdetails?name={catName}");
            // The full route is shown below.
            // await Shell.Current.GoToAsync($"//animals/domestic/cats/catdetails?name={catName}");
        }
    }
}
