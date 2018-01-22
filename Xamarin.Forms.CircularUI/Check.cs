using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

using Xamarin.Forms;

namespace Xamarin.Forms.CircularUI
{
    /// <summary>
    /// The check widget allows for toggling a value between true and false.
    /// The check is extension of Xamarin.Forms.Switch.
    /// </summary>
    /// <example>
    /// <code>
    /// var check = new Check
    /// {
    ///     HorizontalOptions = LayoutOptions.Center,
    ///     VerticalOptions = LayoutOptions.Center,
    ///     DisplayStyle = "Onoff"
    /// }
    /// </code>
    /// </example>
    public class Check : Switch
    {
        /// <summary>
        /// BindableProperty. Identifies the DisplayStyle bindable property.
        /// </summary>
        public static readonly BindableProperty DisplayStyleProperty = BindableProperty.Create(nameof(DisplayStyle), typeof(CheckDisplayStyle), typeof(Check), defaultValue: CheckDisplayStyle.Default);

        /// <summary>
        /// Gets or sets display style of Check.
        /// </summary>
        public CheckDisplayStyle DisplayStyle
        {
            get { return (CheckDisplayStyle)GetValue(DisplayStyleProperty); }
            set { SetValue(DisplayStyleProperty, value); }
        }
    }
}
