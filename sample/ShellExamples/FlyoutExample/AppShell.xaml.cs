using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FlyoutExample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            BindingContext = this;
        }

        public Command OnMenu1 => new Command(() =>
        {
            DisplayAlert("menu", "Menu1 clicked", "Ok");
        });

        public Command OnMenu2 => new Command(() =>
        {
            DisplayAlert("menu", "Menu2 clicked", "Ok");
        });

        protected override bool OnBackButtonPressed()
        {
            if (FlyoutIsPresented)
            {
                FlyoutIsPresented = false;
                return true;
            }
            return base.OnBackButtonPressed();
        }

    }
}