using System.Linq;
using Xamarin.Forms;
using Xaminals.Models;
using Tizen.Wearable.CircularUI.Forms;

namespace Xaminals.Views
{
    public partial class ElephantsPage : CirclePage
    {
        public ElephantsPage()
        {
            InitializeComponent();
        }

        async void OnItemSelected(object sender, ItemTappedEventArgs e)
        {
            string elephantName = (e.Item as Animal).Name;
            // This works because route names are unique in this application.
            await Shell.Current.GoToAsync($"elephantdetails?name={elephantName}");
            // The full route is shown below.
            // await Shell.Current.GoToAsync($"//animals/elephants/elephantdetails?name={elephantName}");
        }
    }
}
