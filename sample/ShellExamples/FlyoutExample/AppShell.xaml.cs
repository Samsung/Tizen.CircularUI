using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FlyoutExample
{
    class ShellModel : INotifyPropertyChanged
    {
        public static ShellModel Instance = new ShellModel();

        public event PropertyChangedEventHandler PropertyChanged;

        string _mainTitle = "Main";
        public string MainTitle
        {
            get => _mainTitle;
            set
            {
                _mainTitle = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MainTitle"));
            }
        }

        public Command OnMenu1 => new Command(() =>
        {
            Shell.Current.DisplayAlert("menu", "Menu1 clicked", "Ok");
        });

        public Command OnMenu2 => new Command(() =>
        {
            Shell.Current.DisplayAlert("menu", "Menu2 clicked", "Ok");
        });

    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            BindingContext = ShellModel.Instance;
        }

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