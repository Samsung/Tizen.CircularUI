using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace UIComponents.Extensions
{
    public class NoContentView : View
    {
        public static readonly BindableProperty TitleProperty = BindableProperty.Create("Title", typeof(string), typeof(NoContentView), default(string));

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
    }
}
