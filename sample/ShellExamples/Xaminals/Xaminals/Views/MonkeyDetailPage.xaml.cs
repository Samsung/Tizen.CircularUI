using Xamarin.Forms;
using Xaminals.ViewModels;
using Tizen.Wearable.CircularUI.Forms;

namespace Xaminals.Views
{
    public partial class MonkeyDetailPage : CirclePage
    {
        public MonkeyDetailPage()
        {
            InitializeComponent();
            BindingContext = new MonkeyDetailViewModel();
        }
    }
}
