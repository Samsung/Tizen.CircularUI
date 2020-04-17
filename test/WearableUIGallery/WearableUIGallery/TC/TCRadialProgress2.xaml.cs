using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WearableUIGallery.TC
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TCRadialProgress2 : ContentPage
    {
        public TCRadialProgress2()
        {
            InitializeComponent();
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            var ani = new Animation((d) =>
            {
                _progress.Progress = d * 0.85f;
            });
            ani.Commit(this, "Up", length: 1000, easing: Easing.SinIn);
        }
    }
}