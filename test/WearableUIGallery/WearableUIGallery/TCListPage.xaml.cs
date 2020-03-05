/*
 * Copyright (c) 2018 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Flora License, Version 1.1 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://floralicense.org/license/
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Globalization;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WearableUIGallery
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TCListPage : CirclePage
    {
        public TCListPage ()
        {
            InitializeComponent ();
            this.AutomationId = "HomePage";
            BindingContext = TCData.TCs;
        }

        public void OnItemTapped(object sender, ItemTappedEventArgs args)
        {
            if (args.Item == null) return;

            var desc = args.Item as TCDescribe;
            if(desc != null)
            {
                if(desc.IsGroup)
                {
                    Shell.Current.GoToAsync($"TCSubListPage?tc={desc.Title}");
                }
                else
                {
                    Shell.Current.GoToAsync(desc.Title);
                }
            }
        }
    }

    class DetailTextConveter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return "";

            var count = (int)value;
            return count > 1 ? $"{count} TC" : "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class AutomationBinding
    {
        #region AutomationId Attached Property
        public static readonly BindableProperty AutomationIdProperty = BindableProperty.CreateAttached (nameof(AutomationIdProperty), typeof(string),
           typeof(AutomationBinding), string.Empty, propertyChanged: OnAutomationIdChanged);

        public static string GetAutomationId(BindableObject target)
        {
            return (string)target.GetValue(AutomationIdProperty);
        }

        public static void SetAutomationId(BindableObject target, string value)
        {
            target.SetValue(AutomationIdProperty, value);
        }

        #endregion

        static void OnAutomationIdChanged(BindableObject bindable, object oldValue, object newValue)
        {
            // Element has the AutomationId property
            var element = bindable as Element;
            string id = (newValue == null) ? "" : newValue.ToString();

            // we can only set the AutomationId once, so only set it when we have a reasonable value since
            // sometimes bindings will fire with null the first time
            if (element != null && element.AutomationId == null && !string.IsNullOrEmpty(id))
            {
                element.AutomationId = id;
            }
        }
    }
}