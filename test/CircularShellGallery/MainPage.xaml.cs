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
            TestCases.ItemTapped += OnItemTapped;
        }

        void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            Shell.Current.GoToAsync($"//{(e.Item as string)}");
        }
    }
}