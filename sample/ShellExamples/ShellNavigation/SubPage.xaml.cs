using System;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShellNavigation
{
    [QueryProperty("From", "from")]
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SubPage : CirclePage
	{
		public SubPage ()
		{
			InitializeComponent ();
            BindingContext = this;
        }

        string _from;
        public string From
        {
            get
            {
                return _from;
            }
            set
            {
                _from = Uri.UnescapeDataString(value);
                OnPropertyChanged("From");
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Device.BeginInvokeOnMainThread(() =>
            {
                Current.Text = $"Current : {Shell.Current?.CurrentState?.Location}";
            });
        }

        public Command OnMain { get; } = new Command(async () =>
        {
            await Shell.Current.GoToAsync("//Main");
        });
        public Command OnSetting { get; } = new Command(async () =>
        {
            await Shell.Current.GoToAsync("//Setting");
        });

        async void OnClicked(object sender, EventArgs e)
        {
            await Shell.Current.Navigation.PopAsync();
        }
    }
}