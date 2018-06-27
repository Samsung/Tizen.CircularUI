using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WearableUIGallery.TC
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TCCircleSurfaceItems1 : CirclePage
	{
        static Color[] colors = new Color[] {
            Color.IndianRed,
            Color.LightYellow,
            Color.LawnGreen
        };

        static Func<double, double>[] convFuncs = new Func<double, double>[]
        {
            v => v,
            v => v * v > 1.0 ? 1.0 : v * v,
            v => {
                double x = v<.5 ? 16*v*v*v*v*v : 1+16*(--v)*v*v*v*v;
                return x > 1.0 ? 1.0 : x;
            }
        };

        int index = 0;
        int inc = 1;
        double incValue = 0.01;
        double progressValue = 0.0;
        bool appeared;

        public double Value {
            get => progressValue;
            set
            {
                progressValue = value;
                OnPropertyChanged(nameof(Value));
            }
        }

		public TCCircleSurfaceItems1()
		{
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            appeared = true;
            Device.StartTimer(TimeSpan.FromMilliseconds(140), () =>
            {
                Value += incValue;
                if (Value >= 1.0) incValue = -0.01;
                else if (Value <= 0.0) incValue = 0.01;
                return this.appeared;
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            appeared = false;
        }

        void OnClick(object sender, EventArgs args)
        {
            var btn = sender as Button;

            if (inc > 0)
            {
                var item = new CircleProgressBarSurfaceItem
                {
                    BackgroundColor = Color.Black,
                    BarColor = colors[index],
                    BarLineWidth = 30,
                    BarRadius = (90 + 30 * index),
                    IsVisible = true
                };

                item.SetBinding(CircleProgressBarSurfaceItem.ValueProperty, "Value", BindingMode.Default, new MultiConverter(convFuncs[index]));
                item.BindingContext = this;

                CircleSurfaceItems.Add(item);
            }
            else
            {
                CircleSurfaceItems.RemoveAt(index-1);
            }

            index += inc;

            if (index > 2)
            {
                inc = -1;
            }
            else if (index < 1)
            {
                inc = 1;
            }

            btn.Text = $"{index}";
        }

        class MultiConverter : IValueConverter
        {
            Func<double, double> _func;
            public MultiConverter(Func<double, double> func) => _func = func;

            public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => _func((double)value);

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => value;
        }
    }
}