using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WearableUIGallery.TC
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TCRadialProgress : ContentPage
    {
        public TCRadialProgress()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            var ani = new Animation((d) =>
            {
                Progress.Progress = d * 0.45f;
                Progress2.Progress = d * 0.70;
                Progress3.Progress = d * 0.50;
            });
            ani.Commit(this, "Up", length: 1000, easing: Easing.SinIn);
        }
    }
}