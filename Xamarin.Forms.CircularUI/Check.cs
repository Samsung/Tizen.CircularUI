using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

using Xamarin.Forms;

namespace Xamarin.Forms.CircularUI
{
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
