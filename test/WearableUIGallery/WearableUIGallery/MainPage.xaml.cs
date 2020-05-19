using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WearableUIGallery
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage ()
        {
            InitializeComponent();
        }

        void Button_Clicked(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(TCNameEntry.Text))
                Shell.Current.GoToAsync("//Main/TCList/TCListPage");
            else
                Shell.Current.GoToAsync(TCNameEntry.Text);
        }

        void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            Shell.Current.FlyoutBehavior = e.Value ? FlyoutBehavior.Flyout : FlyoutBehavior.Disabled;
        }

    }
}