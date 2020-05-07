using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Tizen.Wearable.CircularUI.Chart.Forms
{
    public class GroupBarChartView : BarChartView
    {
        public static readonly BindableProperty GroupDataCountProperty = BindableProperty.Create(nameof(GroupDataCount), typeof(int), typeof(GroupBarChartView), 1, coerceValue: (bindable, value) =>
        {
            return ((int)value).Clamp(1, 5); //DataCount max is 5, min is 1
        });

        public static readonly BindableProperty GroupBarMarginProperty = BindableProperty.Create(nameof(GroupBarMargin), typeof(double), typeof(GroupBarChartView), 5d);

        public int GroupDataCount
        {
            get { return (int)GetValue(GroupDataCountProperty); }
            set { SetValue(GroupDataCountProperty, value); }
        }

        public double GroupBarMargin
        {
            get { return (double)GetValue(GroupBarMarginProperty); }
            set { SetValue(GroupBarMarginProperty, value); }
        }
    }
}
