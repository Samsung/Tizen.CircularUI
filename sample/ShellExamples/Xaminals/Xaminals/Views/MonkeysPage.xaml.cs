using System.Linq;
using Xamarin.Forms;
using Xaminals.Models;
using Tizen.Wearable.CircularUI.Forms;

namespace Xaminals.Views
{
    public partial class MonkeysPage : CirclePage
    {
        public MonkeysPage()
        {
            InitializeComponent();
        }

        async void OnItemSelected(object sender, ItemTappedEventArgs e)
        {
            string monkeyName = (e.Item as Animal).Name;
            // This works because route names are unique in this application.
            await Shell.Current.GoToAsync($"monkeydetails?name={monkeyName}");
            // The full route is shown below.
            // await Shell.Current.GoToAsync($"//animals/monkeys/monkeydetails?name={monkeyName}");
        }
    }
}
