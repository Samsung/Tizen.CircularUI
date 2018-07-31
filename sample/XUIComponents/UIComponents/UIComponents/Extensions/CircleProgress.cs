using System;
using Xamarin.Forms;

namespace UIComponents.Extensions
{
    /// <summary>
    /// ProgressOptions types
    /// </summary>
    public enum ProgressOptions
    {
        Small,
        Large,
    }

    /// <summary>
    /// Progress for Circle
    /// </summary>
    public class CircleProgress : View
    {
        /// <summary>
        /// ProgressOptions type property
        /// </summary>
        public static readonly BindableProperty OptionProperty = BindableProperty.Create("Option", typeof(ProgressOptions), typeof(CircleProgress), ProgressOptions.Small);

        /// <summary>
        /// ProgressOptions type
        /// </summary>
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
