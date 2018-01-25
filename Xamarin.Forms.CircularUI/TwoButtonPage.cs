using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin.Forms.CircularUI
{
    /// <summary>
    /// The TwoButtonPage is a page that has a rectangular area inside the circle as contents area. It also has two buttons and a Title area.
    /// </summary>
    /// <since_tizen> 4 </since_tizen>
    public class TwoButtonPage : ContentPage
    {
        /// <summary>
        /// BindableProperty. Identifies the FirstButton bindable property.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static readonly BindableProperty FirstButtonProperty = BindableProperty.Create(nameof(FirstButton), typeof(MenuItem), typeof(TwoButtonPage),
            propertyChanged: (b, o, n) =>
            {
                if (n != null)
                    ((Element)n).Parent = (Element)b;
            });
        /// <summary>
        /// BindableProperty. Identifies the SecondButton bindable property.
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public static readonly BindableProperty SecondButtonProperty = BindableProperty.Create(nameof(SecondButton), typeof(MenuItem), typeof(TwoButtonPage),
            propertyChanged: (b, o, n) =>
            {
                if (n != null)
                    ((Element)n).Parent = (Element)b;
            });

        /// <summary>
        /// Gets or sets left button of TwoButtonPage
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public MenuItem FirstButton
        {
            get => (MenuItem)GetValue(FirstButtonProperty);
            set => SetValue(FirstButtonProperty, value);
        }
        /// <summary>
        /// Gets or sets right button of TwoButtonPage
        /// </summary>
        /// <since_tizen> 4 </since_tizen>
        public MenuItem SecondButton
        {
            get => (MenuItem)GetValue(SecondButtonProperty);
            set => SetValue(SecondButtonProperty, value);
        }
    }
}
