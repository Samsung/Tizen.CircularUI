using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Xamarin.Forms.Internals;

namespace Xamarin.Forms.CircularUI
{
    public class IndexPage : MultiPage<ContentPage>
    {
        const int maxIndexItemCount = 20;

        /// <summary>
        /// BindableProperty. Identifies the SelectedIndex bindable property.
        /// </summary>
        public static readonly BindableProperty SelectedIndexProperty = BindableProperty.Create(nameof(SelectedIndex), typeof(int), typeof(IndexPage), defaultValue: 0,
            coerceValue: CoerceSelectedIndex);

        /// <summary>
        /// Gets or sets the index of the selected item of the Index.
        /// </summary>
        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }

        static object CoerceSelectedIndex(BindableObject bindable, object value)
        {
            return ((int)value).Clamp(0, maxIndexItemCount - 1);
        }

        protected override ContentPage CreateDefault(object item)
        {
            var page = new ContentPage();
            return page;
        }
    }
}
