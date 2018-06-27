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
	public partial class TCCircleSurfaceItems3 : CirclePage
	{
        CircleSliderSurfaceItem _slider1;
        CircleSliderSurfaceItem _slider2;
        string _focusedItem;

        public TCCircleSurfaceItems3()
		{
			InitializeComponent ();

            _slider1 = new CircleSliderSurfaceItem
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
                Increment = 1,
                Value = 1,
            };

            _slider2 = new CircleSliderSurfaceItem
            {
                BackgroundColor = Color.LightGray,
                BackgroundRadius = 140,
                BackgroundLineWidth = 15,
                BarColor = Color.Green,
                BarRadius = 140,
                BarLineWidth = 13,
                Maximum = 6,
                Minimum = 1,
                Increment = 0.5,
                Value = 3,
            };

            label1.SetBinding(Label.TextProperty, "Value", BindingMode.Default, new ValueConverter1());
            label2.SetBinding(Label.TextProperty, "Value", BindingMode.Default, new ValueConverter2());
            label1.BindingContext = _slider1;
            label2.BindingContext = _slider2;

            CircleSurfaceItems.Add(_slider1);
            CircleSurfaceItems.Add(_slider2);

            _focusedItem = "slider1";
            RotaryFocusObject = _slider1;
        }


        void OnClick(object sender, EventArgs args)
        {
            if(_focusedItem.Equals("slider1"))
            {
                RotaryFocusObject = _slider2;
                _focusedItem = "slider2";
            }
            else
            {
                RotaryFocusObject = _slider1;
                _focusedItem = "slider1";
            }
        }

        class ValueConverter1 : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                string str = $"Slider1 :{string.Format("{0:0}", (double)value)}";
                return str;
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => value;
        }

        class ValueConverter2 : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                string str = $"Slider2 :{string.Format("{0:F1}", (double)value)}";
                return str;
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => value;
        }

    }
}