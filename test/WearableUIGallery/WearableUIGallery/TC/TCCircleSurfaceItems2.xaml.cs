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
	public partial class TCCircleSurfaceItems2 : CirclePage
	{
        double _incValue = 0.02;
        double _progressValue = 0.0;
        bool _startProgress;

        static Func<double,double>  convFuncs = (v) => v;

        public double Value
        {
            get => _progressValue;
            set
            {
                _progressValue = value;
                OnPropertyChanged(nameof(Value));
            }
        }

		public TCCircleSurfaceItems2()
		{
			InitializeComponent ();

            var item1 = new CircleProgressBarSurfaceItem
            {
                BackgroundColor = Color.Blue,
                BackgroundRadius = 170,
                BackgroundLineWidth = 20,
                BackgroundAngleOffset = 90,
                BackgroundAngle = 270,
                BarColor = Color.Red,
                BarRadius = 170,
                BarLineWidth = 18,
                BarAngleOffset = 90,
                BarAngleMaximum = 180,
                BarAngleMinimum = 10,
                IsVisible = true
            };

            item1.SetBinding(CircleProgressBarSurfaceItem.ValueProperty, "Value", BindingMode.Default, new ValueConverter());
            percent.SetBinding(Label.TextProperty, "Value", BindingMode.Default, new ProgressConverter());
            item1.BindingContext = this;
            percent.BindingContext = this;

            CircleSurfaceItems.Add(item1);
            _startProgress = false;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _startProgress = false;
        }

        void OnClick(object sender, EventArgs args)
        {
            var btn = sender as Button;
            if(_startProgress)
            {
                _startProgress = false;
                btn.Text = "start";
            }
            else
            {
                if (Value == 1.0) Value = 0;
                _startProgress = true;
                btn.Text = "stop";
                Device.StartTimer(TimeSpan.FromMilliseconds(200), () =>
                {
                    Value += _incValue;
                    Console.WriteLine($"Value:{string.Format("{0:f2}", Value)}");
                    if (Value > 1.0)
                    {
                        _startProgress = false;
                        Value = 1.0;
                        btn.Text = "start";
                    }
                    return this._startProgress;
                });
            }
        }

        class ValueConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => convFuncs((double)value);

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => value;
        }

        class ProgressConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                double percent = (double)value * 100;
                string str = $"{string.Format("{0:0}", percent)} %";
                return str;
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => value;
        }
    }
}