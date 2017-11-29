using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin.Forms.CircularUI
{
    public class TwoButtonPage : ContentPage
    {
        public static readonly BindableProperty FirstButtonProperty = BindableProperty.Create(nameof(FirstButton), typeof(MenuItem), typeof(TwoButtonPage),
            propertyChanged: (b, o, n) =>
            {
                if (n != null)
                    ((Element)n).Parent = (Element)b;
            });
        public static readonly BindableProperty SecondButtonProperty = BindableProperty.Create(nameof(SecondButton), typeof(MenuItem), typeof(TwoButtonPage),
            propertyChanged: (b, o, n) =>
            {
                if (n != null)
                    ((Element)n).Parent = (Element)b;
            });

        public MenuItem FirstButton
        {
            get => (MenuItem)GetValue(FirstButtonProperty);
            set => SetValue(FirstButtonProperty, value);
        }
        public MenuItem SecondButton
        {
            get => (MenuItem)GetValue(SecondButtonProperty);
            set => SetValue(SecondButtonProperty, value);
        }
    }
}
