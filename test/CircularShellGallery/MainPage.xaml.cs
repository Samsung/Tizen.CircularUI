using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Tizen.Wearable.CircularUI.Forms;

namespace CircularShellGallery
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : CirclePage
    {
        public MainPage ()
        {
            InitializeComponent();
        }

        void Button_Clicked(object sender, System.EventArgs e)
        {
            Shell.Current.GoToAsync($"//{TCName.Text}");
        }
    }
}