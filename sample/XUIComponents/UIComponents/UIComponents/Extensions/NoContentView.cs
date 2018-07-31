using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace UIComponents.Extensions
{
    /// <summary>
    /// View for no content
    /// </summary>
    public class NoContentView : View
    {
        /// <summary>
        /// Title property
        /// </summary>
        public static readonly BindableProperty TitleProperty = BindableProperty.Create("Title", typeof(string), typeof(NoContentView), default(string));

        /// <summary>
        /// Title of view
        /// </summary>
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
    }
}
