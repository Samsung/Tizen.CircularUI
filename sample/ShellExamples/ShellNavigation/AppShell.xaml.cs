using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShellNavigation
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : CircularShell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("SubPage", typeof(SubPage));
        }
    }
}