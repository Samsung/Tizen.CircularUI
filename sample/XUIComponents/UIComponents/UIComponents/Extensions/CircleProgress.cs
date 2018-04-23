using System;
using Xamarin.Forms;

namespace UIComponents.Extensions
{
    public enum ProgressOptions
    {
        Small,
        Large,
    }

    public class CircleProgress : View
    {
        public static readonly BindableProperty OptionProperty = BindableProperty.Create("Option", typeof(ProgressOptions), typeof(CircleProgress), ProgressOptions.Small);

        public ProgressOptions Option
        {
            get { return (ProgressOptions)GetValue(OptionProperty); }
            set {
                Console.WriteLine($"Option Set:{value}");
                SetValue(OptionProperty, value); 
            }
        }
    }
}
