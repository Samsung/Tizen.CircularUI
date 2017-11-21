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
        /// <remarks>
        /// Small style is only supported at circular devices, and Popup style is only supported at rectangular devices.
        /// </remarks>
        public CheckDisplayStyle DisplayStyle
        {
            get { return (CheckDisplayStyle)GetValue(DisplayStyleProperty); }
            set { SetValue(DisplayStyleProperty, value); }
        }
    }
}
